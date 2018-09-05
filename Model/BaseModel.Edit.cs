using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tools;

namespace Model
{
    public partial class BaseModel
    {
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public RdfMsg Edit()
        {
            RdfMsg msg = Bll.EditValiDate();
            if (!msg.Success)
                return msg;

            bool bllTrans = false;
            if (Bll.Trans && !RdfTransaction.IsStart)
            {
                bllTrans = true;
                RdfTransaction.Begin();
            }
            try
            {
                msg = Bll.BeforeEdit();
                if (!msg.Success)
                {
                    if (bllTrans)
                        RdfTransaction.Rollback();
                    return msg;
                }
                string sql = GetEditSql();
                if (!string.IsNullOrWhiteSpace(sql))
                {
                    msg.Success = ExecuteNonQuery(sql) > 0;
                    if (!msg.Success)
                    {
                        if (bllTrans)
                            RdfTransaction.Rollback();
                        msg.Error = "修改失败!";
                        return msg;
                    }
                }
                msg = Bll.AfterEdit();
            }
            catch (Exception ex)
            {
                msg.Error = ex.Message;
                if (ex.Message.Length > 9 && ex.Message.Substring(0, 9) == "不能在具有唯一索引")
                    msg.Error = "编号已存在!";
                msg.Success = false;
                RdfLog.WriteException(ex, "修改时异常", GetType().FullName, "Edit");
            }
            if (bllTrans)
            {
                if (msg.Success)
                    RdfTransaction.Commit();
                else
                    RdfTransaction.Rollback();
            }
            return msg;
        }

        /// <summary>
        /// 批量修改(不走业务)
        /// </summary>
        /// <param name="fieldDic">修改字段信息</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public RdfMsg BatchEdit(Dictionary<string, object> fieldDic, Condition condition = null)
        {
            RdfMsg msg = new RdfMsg(false);
            bool bllTrans = false;
            if (!RdfTransaction.IsStart)
            {
                bllTrans = true;
                RdfTransaction.Begin();
            }
            try
            {
                ExecuteNonQuery(GetEditBatchSql(fieldDic, condition));
                msg.Success = true;
            }
            catch (Exception ex)
            {
                msg.Error = ex.Message;
                msg.Success = false;
                RdfLog.WriteException(ex, "批量修改时异常", GetType().FullName, "EditBatch");
            }
            finally
            {
                if (bllTrans)
                {
                    if (msg.Success)
                        RdfTransaction.Commit();
                    else
                        RdfTransaction.Rollback();
                }
            }
            return msg;
        }
        /// <summary>
        /// 批量修改(不走业务)
        /// </summary>
        /// <param name="fieldDic">修改字段信息</param>
        /// <param name="conditionList">条件</param>
        /// <returns></returns>
        public RdfMsg BatchEdit(Dictionary<string, object> fieldDic, List<Condition> conditionList)
        {
            RdfMsg msg = new RdfMsg(false);
            bool bllTrans = false;
            if (!RdfTransaction.IsStart)
            {
                bllTrans = true;
                RdfTransaction.Begin();
            }
            try
            {
                ExecuteNonQuery(GetEditBatchSql(fieldDic, conditionList));
                msg.Success = true;
            }
            catch (Exception ex)
            {
                msg.Error = ex.Message;
                msg.Success = false;
                RdfLog.WriteException(ex, "批量修改时异常", GetType().FullName, "EditBatch");
            }
            finally
            {
                if (bllTrans)
                {
                    if (msg.Success)
                        RdfTransaction.Commit();
                    else
                        RdfTransaction.Rollback();
                }
            }
            return msg;
        }
        /// <summary>
        /// 批量修改(不走业务)
        /// </summary>
        /// <param name="fieldDic">修改字段信息</param>
        /// <param name="area">条件</param>
        /// <returns></returns>
        public RdfMsg BatchEdit(Dictionary<string, object> fieldDic, ConditionArea area)
        {
            RdfMsg msg = new RdfMsg(false);
            bool bllTrans = false;
            if (!RdfTransaction.IsStart)
            {
                bllTrans = true;
                RdfTransaction.Begin();
            }
            try
            {
                ExecuteNonQuery(GetEditBatchSql(fieldDic, area));
                msg.Success = true;
            }
            catch (Exception ex)
            {
                msg.Error = ex.Message;
                msg.Success = false;
                RdfLog.WriteException(ex, "批量修改时异常", GetType().FullName, "EditBatch");
            }
            finally
            {
                if (bllTrans)
                {
                    if (msg.Success)
                        RdfTransaction.Commit();
                    else
                        RdfTransaction.Rollback();
                }
            }
            return msg;
        }
        /// <summary>
        /// 批量修改(不走业务)
        /// </summary>
        /// <param name="fieldDic">修改字段信息</param>
        /// <param name="func">条件</param>
        /// <returns></returns>
        public RdfMsg BatchEdit<T>(Dictionary<string, object> fieldDic, Expression<Func<T, bool>> func)
        {
            RdfMsg msg = new RdfMsg(false);
            bool bllTrans = false;
            if (!RdfTransaction.IsStart)
            {
                bllTrans = true;
                RdfTransaction.Begin();
            }
            try
            {
                ExecuteNonQuery(GetEditBatchSql(fieldDic, func));
                msg.Success = true;
            }
            catch (Exception ex)
            {
                msg.Error = ex.Message;
                msg.Success = false;
                RdfLog.WriteException(ex, "批量修改时异常", GetType().FullName, "EditBatch");
            }
            finally
            {
                if (bllTrans)
                {
                    if (msg.Success)
                        RdfTransaction.Commit();
                    else
                        RdfTransaction.Rollback();
                }
            }
            return msg;
        }
        /// <summary>
        /// 根据主键获取修改语句
        /// </summary>
        /// <returns></returns>
        public string GetEditSql()
        {
            string columnVal = "";
            Field keyField = null;
            for (int i = 0; i < Cfg.FieldList.Count; i++)
            {
                Field field = Cfg.FieldList[i];
               if (!field.Save || field.TabAs!=Cfg.Tab.TabAs)
                    continue;
                if (field.ColumnAs == Cfg.Tab.PrimaryKey)
                {
                    keyField = field;
                    if (Cfg.Tab.Identity)
                        continue;
                }
                object newValue = this[field.ColumnAs];
                if (newValue == null && field.OldVal == null)
                    continue;
                if ((newValue == null && field.OldVal != null) || !newValue.Equals(field.OldVal))
                {
                    if (columnVal != "")
                        columnVal += ",";
                    columnVal += field.Column + "=" + GetColumnValue(field);
                }
            }
            if (!string.IsNullOrWhiteSpace(columnVal))
                return string.Format(@"update {0} set {1} where {2}={3}", Cfg.Tab.TabName, columnVal, Cfg.Tab.PrimaryKey, GetColumnValue(keyField));
            return "";
        }
        /// <summary>
        /// 获取批量修改语句
        /// </summary>
        /// <returns></returns>
        public string GetEditBatchSql(Dictionary<string, object> fieldDic, string where)
        {
            if (Cfg == null || Cfg.FieldList == null || Cfg.FieldList.Count == 0)
                throw new Exception(GetType().Name + "没有配置字段！");
            string columnVal = "";
            if (fieldDic == null || fieldDic.Count == 0)
                return "";
            foreach (var item in fieldDic.Keys)
            {
                Field field = Cfg.FieldList.FirstOrDefault(en => en.Save && en.Column.Equals(item));
                if (field != null)
                {
                    if (columnVal != "")
                        columnVal += ",";
                    columnVal += "t1." + item + ConditionHandle.GetCompareVal(field.Type, Compare.Equals, fieldDic[item]);
                }
            }
            string columns, joins;
            SetSelectSql(out columns, out joins);
            if (!string.IsNullOrWhiteSpace(columnVal))
                return string.Format(@"update {4} set {0} from {1} {4} {2} {3}", columnVal, Cfg.Tab.TabName, joins, where, Cfg.Tab.TabAs);
            return "";
        }
        /// <summary>
        /// 获取批量修改语句
        /// </summary>
        /// <returns></returns>
        public string GetEditBatchSql(Dictionary<string, object> fieldDic, Condition condition = null)
        {
            return GetEditBatchSql(fieldDic, ConditionHandle.GetSqlWhere(Cfg, condition));
        }
        /// <summary>
        /// 获取批量修改语句
        /// </summary>
        /// <returns></returns>
        public string GetEditBatchSql(Dictionary<string, object> fieldDic, List<Condition> conditionList)
        {
            return GetEditBatchSql(fieldDic, ConditionHandle.GetSqlWhere(Cfg, conditionList));
        }
        /// <summary>
        /// 获取批量修改语句
        /// </summary>
        /// <returns></returns>
        public string GetEditBatchSql(Dictionary<string, object> fieldDic, ConditionArea area)
        {
            return GetEditBatchSql(fieldDic, ConditionHandle.GetSqlWhere(Cfg, area));
        }
        /// <summary>
        /// 获取批量修改语句
        /// </summary>
        /// <returns></returns>
        public string GetEditBatchSql<T>(Dictionary<string, object> fieldDic, Expression<Func<T, bool>> func)
        {
            return GetEditBatchSql(fieldDic, ConditionHandle.GetSqlWhere(Cfg, func));
        }
    }
}
