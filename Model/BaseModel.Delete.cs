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
        /// 根据条件删除(走业务)
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public RdfMsg Delete(Condition condition,bool singleTab = true)
        {
            RdfMsg msg = Bll.DeleteValiDate(condition);
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
                msg = Bll.BeforeDelete(condition);
                if (msg.Success)
                {
                   ExecuteNonQuery(GetDeleteSql(condition, singleTab));
                   msg = Bll.AfterDelete(condition);
                }
            }
            catch (Exception ex)
            {
                msg.Error = ex.Message;
                msg.Success = false;
                RdfLog.WriteException(ex, "删除时异常", GetType().FullName, "Delete");
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
        /// 根据条件批量删除(不走业务)
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public RdfMsg DeleteBatch(Condition condition,bool singleTab = true)
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
                ExecuteNonQuery(GetDeleteSql(condition, singleTab));
                msg.Success =true;
            }
            catch (Exception ex)
            {
                msg.Error = ex.Message;
                msg.Success = false;
                RdfLog.WriteException(ex, "删除时异常", GetType().FullName, "DeleteBatch");
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
        /// 根据条件批量删除(不走业务)
        /// </summary>
        /// <param name="listCon">条件列表</param>
        /// <returns></returns>
        public RdfMsg DeleteBatch(List<Condition> listCon, bool singleTab = true)
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
                ExecuteNonQuery(GetDeleteSql(listCon, singleTab));
                msg.Success = true;
            }
            catch (Exception ex)
            {
                msg.Error = ex.Message;
                msg.Success = false;
                RdfLog.WriteException(ex, "删除时异常", GetType().FullName, "DeleteBatch");
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
        /// 根据条件批量删除(不走业务)
        /// </summary>
        /// <param name="area">条件组</param>
        /// <returns></returns>
        public RdfMsg DeleteBatch(ConditionArea area, bool singleTab = true)
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
                ExecuteNonQuery(GetDeleteSql(area, singleTab));
                msg.Success = true;
            }
            catch (Exception ex)
            {
                msg.Error = ex.Message;
                msg.Success = false;
                RdfLog.WriteException(ex, "批量删除时异常", GetType().FullName, "DeleteBatch");
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
        /// 根据条件批量删除(不走业务)
        /// </summary>
        /// <param name="func">lambda表达式</param>
        /// <returns></returns>
        public RdfMsg DeleteBatch<T>(Expression<Func<T, bool>> func,bool singleTab = true)
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
                ExecuteNonQuery(GetDeleteSql(func, singleTab));
                msg.Success = true;
            }
            catch (Exception ex)
            {
                msg.Error = ex.Message;
                msg.Success = false;
                RdfLog.WriteException(ex, "批量删除时异常", GetType().FullName, "DeleteBatch");
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
        /// 根据主键获取删除语句
        /// </summary>
        /// <returns></returns>
        public string GetDeleteSql(bool singleTab = true)
        {
            return GetDeleteSql(new Condition(Cfg.Tab.PrimaryKey, this[Cfg.Tab.PrimaryKey]), singleTab);
        }
        /// <summary>
        /// 根据条件获取删除语句
        /// </summary>
        /// <returns></returns>
        public string GetDeleteSql(string where,bool singleTab=true)
        {
            if (singleTab)
                Cfg.NotSelect.AddRange(Cfg.JoinTab.ConvertAll(item => item.TabAs));
            string columns, joins;
            SetSelectSql(out columns,out joins);
            return string.Format(@"delete {3} from {0} {3} {1} {2}", Cfg.Tab.TabName, joins, where, Cfg.Tab.TabAs);
        }
        /// <summary>
        /// 根据条件获取删除语句
        /// </summary>
        /// <returns></returns>
        public string GetDeleteSql(Condition condition, bool singleTab=true)
        {
            return GetDeleteSql(ConditionHandle.GetSqlWhere(Cfg, condition), singleTab);
        }
        /// <summary>
        /// 根据条件组获取删除语句
        /// </summary>
        /// <returns></returns>
        public string GetDeleteSql(List<Condition> listCon, bool singleTab = true)
        {
            return GetDeleteSql(ConditionHandle.GetSqlWhere(Cfg, listCon), singleTab);
        }
        /// <summary>
        /// 根据条件区域获取删除语句
        /// </summary>
        /// <returns></returns>
        public string GetDeleteSql(ConditionArea area, bool singleTab = true)
        {
            return GetDeleteSql(ConditionHandle.GetSqlWhere(Cfg, area), singleTab);
        }
        /// <summary>
        /// 根据条件区域获取删除语句
        /// </summary>
        /// <returns></returns>
        public string GetDeleteSql<T>(Expression<Func<T, bool>> func, bool singleTab = true)
        {
            return GetDeleteSql(ConditionHandle.GetSqlWhere(Cfg, func), singleTab);
        }
    }
}
