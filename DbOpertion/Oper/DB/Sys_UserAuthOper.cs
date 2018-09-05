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
    public partial class Sys_UserAuthOper : SingleTon<Sys_UserAuthOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_userauth"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Sys_UserAuth sys_userauth)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_userauth.UserId != null)
                dict.Add("@UserId", sys_userauth.UserId.ToString());
            if (sys_userauth.MenuId != null)
                dict.Add("@MenuId", sys_userauth.MenuId.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sys_userauth"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Sys_UserAuth sys_userauth)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (sys_userauth.UserId != null)
            {
                part1.Append("UserId,");
                part2.Append("@UserId,");
            }
            if (sys_userauth.MenuId != null)
            {
                part1.Append("MenuId,");
                part2.Append("@MenuId,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into sys_userauth(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_userauth"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Sys_UserAuth sys_userauth)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_userauth set ");
            if (sys_userauth.UserId != null)
                part1.Append("UserId = @UserId,");
            if (sys_userauth.MenuId != null)
                part1.Append("MenuId = @MenuId,");

            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Sys_UserAuth"></param>
        /// <returns></returns>
        public int Add(Sys_UserAuth model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_UserAuth"></param>
        /// <returns></returns>
        public int Update(Sys_UserAuth model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Sys_UserAuth"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Sys_UserAuth where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_userauth"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Sys_UserAuth sys_userauth,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_userauth.UserId != null)
                dict.Add("@UserId" + i, sys_userauth.UserId.ToString());
            if (sys_userauth.MenuId != null)
                dict.Add("@MenuId" + i, sys_userauth.MenuId.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_userauth"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Sys_UserAuth sys_userauth, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_userauth set ");
            if (sys_userauth.UserId != null)
                part1.Append($"UserId = @UserId{i},");
            if (sys_userauth.MenuId != null)
                part1.Append($"MenuId = @MenuId{i},");

            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_UserAuth"></param>
        /// <returns></returns>
        public void UpdateList(List<Sys_UserAuth> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="Sys_UserAuth"></param>
        /// <returns></returns>
        public Sys_UserAuth GetById(int id)
        {
            return SqlHelper.Instance.GetById<Sys_UserAuth>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Sys_UserAuth"></param>
        /// <returns></returns>
        public List<Sys_UserAuth> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Sys_UserAuth>();
        }
    }
}
