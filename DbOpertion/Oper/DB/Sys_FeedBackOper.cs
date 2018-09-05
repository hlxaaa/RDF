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
    public partial class Sys_FeedBackOper : SingleTon<Sys_FeedBackOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_feedback"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Sys_FeedBack sys_feedback)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_feedback.Id != null)
                dict.Add("@Id", sys_feedback.Id.ToString());
            if (sys_feedback.Title != null)
                dict.Add("@Title", sys_feedback.Title.ToString());
            if (sys_feedback.Content != null)
                dict.Add("@Content", sys_feedback.Content.ToString());
            if (sys_feedback.EditorId != null)
                dict.Add("@EditorId", sys_feedback.EditorId.ToString());
            if (sys_feedback.EditTime != null)
                dict.Add("@EditTime", sys_feedback.EditTime.ToString());
            if (sys_feedback.reply != null)
                dict.Add("@reply", sys_feedback.reply.ToString());
            if (sys_feedback.isEnglish != null)
                dict.Add("@isEnglish", sys_feedback.isEnglish.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sys_feedback"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Sys_FeedBack sys_feedback)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (sys_feedback.Title != null)
            {
                part1.Append("Title,");
                part2.Append("@Title,");
            }
            if (sys_feedback.Content != null)
            {
                part1.Append("Content,");
                part2.Append("@Content,");
            }
            if (sys_feedback.EditorId != null)
            {
                part1.Append("EditorId,");
                part2.Append("@EditorId,");
            }
            if (sys_feedback.EditTime != null)
            {
                part1.Append("EditTime,");
                part2.Append("@EditTime,");
            }
            if (sys_feedback.reply != null)
            {
                part1.Append("reply,");
                part2.Append("@reply,");
            }
            if (sys_feedback.isEnglish != null)
            {
                part1.Append("isEnglish,");
                part2.Append("@isEnglish,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into sys_feedback(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_feedback"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Sys_FeedBack sys_feedback)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_feedback set ");
            if (sys_feedback.Title != null)
                part1.Append("Title = @Title,");
            if (sys_feedback.Content != null)
                part1.Append("Content = @Content,");
            if (sys_feedback.EditorId != null)
                part1.Append("EditorId = @EditorId,");
            if (sys_feedback.EditTime != null)
                part1.Append("EditTime = @EditTime,");
            if (sys_feedback.reply != null)
                part1.Append("reply = @reply,");
            if (sys_feedback.isEnglish != null)
                part1.Append("isEnglish = @isEnglish,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where Id= @Id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Sys_FeedBack"></param>
        /// <returns></returns>
        public int Add(Sys_FeedBack model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_FeedBack"></param>
        /// <returns></returns>
        public int Update(Sys_FeedBack model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Sys_FeedBack"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Sys_FeedBack where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_feedback"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Sys_FeedBack sys_feedback,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_feedback.Id != null)
                dict.Add("@Id" + i, sys_feedback.Id.ToString());
            if (sys_feedback.Title != null)
                dict.Add("@Title" + i, sys_feedback.Title.ToString());
            if (sys_feedback.Content != null)
                dict.Add("@Content" + i, sys_feedback.Content.ToString());
            if (sys_feedback.EditorId != null)
                dict.Add("@EditorId" + i, sys_feedback.EditorId.ToString());
            if (sys_feedback.EditTime != null)
                dict.Add("@EditTime" + i, sys_feedback.EditTime.ToString());
            if (sys_feedback.reply != null)
                dict.Add("@reply" + i, sys_feedback.reply.ToString());
            if (sys_feedback.isEnglish != null)
                dict.Add("@isEnglish" + i, sys_feedback.isEnglish.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_feedback"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Sys_FeedBack sys_feedback, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_feedback set ");
            if (sys_feedback.Title != null)
                part1.Append($"Title = @Title{i},");
            if (sys_feedback.Content != null)
                part1.Append($"Content = @Content{i},");
            if (sys_feedback.EditorId != null)
                part1.Append($"EditorId = @EditorId{i},");
            if (sys_feedback.EditTime != null)
                part1.Append($"EditTime = @EditTime{i},");
            if (sys_feedback.reply != null)
                part1.Append($"reply = @reply{i},");
            if (sys_feedback.isEnglish != null)
                part1.Append($"isEnglish = @isEnglish{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where Id= @Id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_FeedBack"></param>
        /// <returns></returns>
        public void UpdateList(List<Sys_FeedBack> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="Sys_FeedBack"></param>
        /// <returns></returns>
        public Sys_FeedBack GetById(int id)
        {
            return SqlHelper.Instance.GetById<Sys_FeedBack>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Sys_FeedBack"></param>
        /// <returns></returns>
        public List<Sys_FeedBack> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Sys_FeedBack>();
        }
    }
}
