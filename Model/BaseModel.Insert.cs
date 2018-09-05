using System;
using Tools;

namespace Model
{
    public partial class BaseModel
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="setIdentity">返回自增</param>
        /// <returns></returns>
        public RdfMsg Insert(bool setIdentity = false)
        {
            RdfMsg msg = Bll.InsertValiDate();
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
                msg = Bll.BeforeInsert();
                if (!msg.Success)
                {
                    if (bllTrans)
                        RdfTransaction.Rollback();
                    return msg;
                }
                if (Cfg.Tab.Identity && setIdentity)
                {
                    object identity = ExecuteScalar(GetInsertSql() + "select @@IDENTITY");
                    msg.Success = identity != null;
                    if (msg.Success)
                    {
                        this[Cfg.Tab.PrimaryKey] = Convert.ToInt32(identity);
                        RdfCache.RemoveCache(Cfg.Tab.TabName);
                    }
                }
                else
                    msg.Success = ExecuteNonQuery(GetInsertSql()) > 0;

                if (!msg.Success)
                {
                    if (bllTrans)
                        RdfTransaction.Rollback();
                    msg.Error = "新增失败!";
                    return msg;
                }
                
                msg = Bll.AfterInsert();
            }
            catch (Exception ex)
            {
                msg.Error = ex.Message;
                if (ex.Message.Length>9 && ex.Message.Substring(0,9)=="不能在具有唯一索引")
                    msg.Error = "编号已存在!";
                msg.Success = false;
                RdfLog.WriteException(ex, "新增时异常", GetType().FullName, "Insert");
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
        /// 获取插入语句
        /// </summary>
        /// <returns></returns>
        public string GetInsertSql()
        {
            string column = "", value = "";
            foreach (Field field in Cfg.FieldList)
            {
                if (!field.Save || field.TabAs!=Cfg.Tab.TabAs)
                    continue;
                if (Cfg.Tab.Identity && field.Column.Equals(Cfg.Tab.PrimaryKey))
                    continue;
                if (column != "")
                    column += ",";
                if (value != "")
                    value += ",";
                column += "[" + field.Column+"]";
                value += GetColumnValue(field);
            }
            return string.Format(@"insert into {0}({1}) values({2})", Cfg.Tab.TabName, column, value);
        }
    }
}
