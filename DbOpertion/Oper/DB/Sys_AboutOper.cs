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
    public partial class Sys_AboutOper : SingleTon<Sys_AboutOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_about"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Sys_About sys_about)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_about.Id != null)
                dict.Add("@Id", sys_about.Id.ToString());
            if (sys_about.Content != null)
                dict.Add("@Content", sys_about.Content.ToString());
            if (sys_about.EditorId != null)
                dict.Add("@EditorId", sys_about.EditorId.ToString());
            if (sys_about.EditTime != null)
                dict.Add("@EditTime", sys_about.EditTime.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sys_about"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Sys_About sys_about)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (sys_about.Content != null)
            {
                part1.Append("Content,");
                part2.Append("@Content,");
            }
            if (sys_about.EditorId != null)
            {
                part1.Append("EditorId,");
                part2.Append("@EditorId,");
            }
            if (sys_about.EditTime != null)
            {
                part1.Append("EditTime,");
                part2.Append("@EditTime,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into sys_about(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_about"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Sys_About sys_about)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_about set ");
            if (sys_about.Content != null)
                part1.Append("Content = @Content,");
            if (sys_about.EditorId != null)
                part1.Append("EditorId = @EditorId,");
            if (sys_about.EditTime != null)
                part1.Append("EditTime = @EditTime,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where Id= @Id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Sys_About"></param>
        /// <returns></returns>
        public int Add(Sys_About model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_About"></param>
        /// <returns></returns>
        public int Update(Sys_About model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Sys_About"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Sys_About where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_about"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Sys_About sys_about,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_about.Id != null)
                dict.Add("@Id" + i, sys_about.Id.ToString());
            if (sys_about.Content != null)
                dict.Add("@Content" + i, sys_about.Content.ToString());
            if (sys_about.EditorId != null)
                dict.Add("@EditorId" + i, sys_about.EditorId.ToString());
            if (sys_about.EditTime != null)
                dict.Add("@EditTime" + i, sys_about.EditTime.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_about"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Sys_About sys_about, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_about set ");
            if (sys_about.Content != null)
                part1.Append($"Content = @Content{i},");
            if (sys_about.EditorId != null)
                part1.Append($"EditorId = @EditorId{i},");
            if (sys_about.EditTime != null)
                part1.Append($"EditTime = @EditTime{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where Id= @Id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_About"></param>
        /// <returns></returns>
        public void UpdateList(List<Sys_About> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="Sys_About"></param>
        /// <returns></returns>
        public Sys_About GetById(int id)
        {
            return SqlHelper.Instance.GetById<Sys_About>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Sys_About"></param>
        /// <returns></returns>
        public List<Sys_About> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Sys_About>();
        }
    }
}
