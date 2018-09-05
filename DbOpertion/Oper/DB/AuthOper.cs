using System.Text;
using Common.Helper;
using System;
using System.Collections.Generic;
using Common;
using System.Linq;
using DbOpertion.Models;
using System.Data.SqlClient;

namespace DbOpertion.DBoperation
{
    public partial class AuthOper : SingleTon<AuthOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Auth auth)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (auth.id != null)
                dict.Add("@id", auth.id.ToString());
            if (auth.userId != null)
                dict.Add("@userId", auth.userId.ToString());
            if (auth.menuId != null)
                dict.Add("@menuId", auth.menuId.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="auth"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Auth auth)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (auth.userId != null)
            {
                part1.Append("userId,");
                part2.Append("@userId,");
            }
            if (auth.menuId != null)
            {
                part1.Append("menuId,");
                part2.Append("@menuId,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into auth(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="auth"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Auth auth)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update auth set ");
            if (auth.userId != null)
                part1.Append("userId = @userId,");
            if (auth.menuId != null)
                part1.Append("menuId = @menuId,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where id= @id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Auth"></param>
        /// <returns></returns>
        public int Add(Auth model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Auth"></param>
        /// <returns></returns>
        public int Update(Auth model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Auth"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Auth where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Auth auth,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (auth.id != null)
                dict.Add("@id" + i, auth.id.ToString());
            if (auth.userId != null)
                dict.Add("@userId" + i, auth.userId.ToString());
            if (auth.menuId != null)
                dict.Add("@menuId" + i, auth.menuId.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="auth"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Auth auth, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update auth set ");
            if (auth.userId != null)
                part1.Append($"userId = @userId{i},");
            if (auth.menuId != null)
                part1.Append($"menuId = @menuId{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where id= @id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Auth"></param>
        /// <returns></returns>
        public void UpdateList(List<Auth> list, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = "";
            var dict = new Dictionary<string,string>();
            for(int i=0;i<list.Count;i++)
            {
            var tempDict=GetParametersItem(list[i],i);
            foreach(var item in tempDict)
            {
            dict.Add(item.Key,item.Value);
            }
            str+=GetUpdateStrItem(list[i],i);
            }
            SqlHelper.Instance.ExcuteNon(str, dict, connection, transaction);
        }
        /// <summary>
        /// getById
        /// </summary>
        /// <param name="Auth"></param>
        /// <returns></returns>
        public Auth GetById(int id)
        {
            return SqlHelper.Instance.GetById<Auth>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Auth"></param>
        /// <returns></returns>
        public List<Auth> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Auth>();
        }
    }
}
