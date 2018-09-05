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
    public partial class SportMsgOper : SingleTon<SportMsgOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sportmsg"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(SportMsg sportmsg)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sportmsg.id != null)
                dict.Add("@id", sportmsg.id.ToString());
            if (sportmsg.userId != null)
                dict.Add("@userId", sportmsg.userId.ToString());
            if (sportmsg.createTime != null)
                dict.Add("@createTime", sportmsg.createTime.ToString());
            if (sportmsg.content != null)
                dict.Add("@content", sportmsg.content.ToString());
            if (sportmsg.sportId != null)
                dict.Add("@sportId", sportmsg.sportId.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sportmsg"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(SportMsg sportmsg)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (sportmsg.userId != null)
            {
                part1.Append("userId,");
                part2.Append("@userId,");
            }
            if (sportmsg.createTime != null)
            {
                part1.Append("createTime,");
                part2.Append("@createTime,");
            }
            if (sportmsg.content != null)
            {
                part1.Append("content,");
                part2.Append("@content,");
            }
            if (sportmsg.sportId != null)
            {
                part1.Append("sportId,");
                part2.Append("@sportId,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into sportmsg(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sportmsg"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(SportMsg sportmsg)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sportmsg set ");
            if (sportmsg.userId != null)
                part1.Append("userId = @userId,");
            if (sportmsg.createTime != null)
                part1.Append("createTime = @createTime,");
            if (sportmsg.content != null)
                part1.Append("content = @content,");
            if (sportmsg.sportId != null)
                part1.Append("sportId = @sportId,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where id= @id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="SportMsg"></param>
        /// <returns></returns>
        public int Add(SportMsg model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="SportMsg"></param>
        /// <returns></returns>
        public int Update(SportMsg model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="SportMsg"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from SportMsg where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sportmsg"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(SportMsg sportmsg,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sportmsg.id != null)
                dict.Add("@id" + i, sportmsg.id.ToString());
            if (sportmsg.userId != null)
                dict.Add("@userId" + i, sportmsg.userId.ToString());
            if (sportmsg.createTime != null)
                dict.Add("@createTime" + i, sportmsg.createTime.ToString());
            if (sportmsg.content != null)
                dict.Add("@content" + i, sportmsg.content.ToString());
            if (sportmsg.sportId != null)
                dict.Add("@sportId" + i, sportmsg.sportId.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sportmsg"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(SportMsg sportmsg, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sportmsg set ");
            if (sportmsg.userId != null)
                part1.Append($"userId = @userId{i},");
            if (sportmsg.createTime != null)
                part1.Append($"createTime = @createTime{i},");
            if (sportmsg.content != null)
                part1.Append($"content = @content{i},");
            if (sportmsg.sportId != null)
                part1.Append($"sportId = @sportId{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where id= @id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="SportMsg"></param>
        /// <returns></returns>
        public void UpdateList(List<SportMsg> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="SportMsg"></param>
        /// <returns></returns>
        public SportMsg GetById(int id)
        {
            return SqlHelper.Instance.GetById<SportMsg>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="SportMsg"></param>
        /// <returns></returns>
        public List<SportMsg> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<SportMsg>();
        }
    }
}
