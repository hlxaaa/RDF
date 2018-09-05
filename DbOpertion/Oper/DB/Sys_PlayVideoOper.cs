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
    public partial class Sys_PlayVideoOper : SingleTon<Sys_PlayVideoOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_playvideo"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Sys_PlayVideo sys_playvideo)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_playvideo.Id != null)
                dict.Add("@Id", sys_playvideo.Id.ToString());
            if (sys_playvideo.Title != null)
                dict.Add("@Title", sys_playvideo.Title.ToString());
            if (sys_playvideo.TitleUrl != null)
                dict.Add("@TitleUrl", sys_playvideo.TitleUrl.ToString());
            if (sys_playvideo.Url != null)
                dict.Add("@Url", sys_playvideo.Url.ToString());
            if (sys_playvideo.LongTime != null)
                dict.Add("@LongTime", sys_playvideo.LongTime.ToString());
            if (sys_playvideo.EditTime != null)
                dict.Add("@EditTime", sys_playvideo.EditTime.ToString());
            if (sys_playvideo.PlayCount != null)
                dict.Add("@PlayCount", sys_playvideo.PlayCount.ToString());
            if (sys_playvideo.Enabled != null)
                dict.Add("@Enabled", sys_playvideo.Enabled.ToString());
            if (sys_playvideo.VieldId != null)
                dict.Add("@VieldId", sys_playvideo.VieldId.ToString());
            if (sys_playvideo.userId != null)
                dict.Add("@userId", sys_playvideo.userId.ToString());
            if (sys_playvideo.price != null)
                dict.Add("@price", sys_playvideo.price.ToString());
            if (sys_playvideo.isPass != null)
                dict.Add("@isPass", sys_playvideo.isPass.ToString());
            if (sys_playvideo.isEnglish != null)
                dict.Add("@isEnglish", sys_playvideo.isEnglish.ToString());
            if (sys_playvideo.TitleE != null)
                dict.Add("@TitleE", sys_playvideo.TitleE.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sys_playvideo"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Sys_PlayVideo sys_playvideo)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (sys_playvideo.Title != null)
            {
                part1.Append("Title,");
                part2.Append("@Title,");
            }
            if (sys_playvideo.TitleUrl != null)
            {
                part1.Append("TitleUrl,");
                part2.Append("@TitleUrl,");
            }
            if (sys_playvideo.Url != null)
            {
                part1.Append("Url,");
                part2.Append("@Url,");
            }
            if (sys_playvideo.LongTime != null)
            {
                part1.Append("LongTime,");
                part2.Append("@LongTime,");
            }
            if (sys_playvideo.EditTime != null)
            {
                part1.Append("EditTime,");
                part2.Append("@EditTime,");
            }
            if (sys_playvideo.PlayCount != null)
            {
                part1.Append("PlayCount,");
                part2.Append("@PlayCount,");
            }
            if (sys_playvideo.Enabled != null)
            {
                part1.Append("Enabled,");
                part2.Append("@Enabled,");
            }
            if (sys_playvideo.VieldId != null)
            {
                part1.Append("VieldId,");
                part2.Append("@VieldId,");
            }
            if (sys_playvideo.userId != null)
            {
                part1.Append("userId,");
                part2.Append("@userId,");
            }
            if (sys_playvideo.price != null)
            {
                part1.Append("price,");
                part2.Append("@price,");
            }
            if (sys_playvideo.isPass != null)
            {
                part1.Append("isPass,");
                part2.Append("@isPass,");
            }
            if (sys_playvideo.isEnglish != null)
            {
                part1.Append("isEnglish,");
                part2.Append("@isEnglish,");
            }
            if (sys_playvideo.TitleE != null)
            {
                part1.Append("TitleE,");
                part2.Append("@TitleE,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into sys_playvideo(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_playvideo"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Sys_PlayVideo sys_playvideo)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_playvideo set ");
            if (sys_playvideo.Title != null)
                part1.Append("Title = @Title,");
            if (sys_playvideo.TitleUrl != null)
                part1.Append("TitleUrl = @TitleUrl,");
            if (sys_playvideo.Url != null)
                part1.Append("Url = @Url,");
            if (sys_playvideo.LongTime != null)
                part1.Append("LongTime = @LongTime,");
            if (sys_playvideo.EditTime != null)
                part1.Append("EditTime = @EditTime,");
            if (sys_playvideo.PlayCount != null)
                part1.Append("PlayCount = @PlayCount,");
            if (sys_playvideo.Enabled != null)
                part1.Append("Enabled = @Enabled,");
            if (sys_playvideo.VieldId != null)
                part1.Append("VieldId = @VieldId,");
            if (sys_playvideo.userId != null)
                part1.Append("userId = @userId,");
            if (sys_playvideo.price != null)
                part1.Append("price = @price,");
            if (sys_playvideo.isPass != null)
                part1.Append("isPass = @isPass,");
            if (sys_playvideo.isEnglish != null)
                part1.Append("isEnglish = @isEnglish,");
            if (sys_playvideo.TitleE != null)
                part1.Append("TitleE = @TitleE,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where Id= @Id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Sys_PlayVideo"></param>
        /// <returns></returns>
        public int Add(Sys_PlayVideo model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_PlayVideo"></param>
        /// <returns></returns>
        public int Update(Sys_PlayVideo model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Sys_PlayVideo"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Sys_PlayVideo where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_playvideo"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Sys_PlayVideo sys_playvideo,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_playvideo.Id != null)
                dict.Add("@Id" + i, sys_playvideo.Id.ToString());
            if (sys_playvideo.Title != null)
                dict.Add("@Title" + i, sys_playvideo.Title.ToString());
            if (sys_playvideo.TitleUrl != null)
                dict.Add("@TitleUrl" + i, sys_playvideo.TitleUrl.ToString());
            if (sys_playvideo.Url != null)
                dict.Add("@Url" + i, sys_playvideo.Url.ToString());
            if (sys_playvideo.LongTime != null)
                dict.Add("@LongTime" + i, sys_playvideo.LongTime.ToString());
            if (sys_playvideo.EditTime != null)
                dict.Add("@EditTime" + i, sys_playvideo.EditTime.ToString());
            if (sys_playvideo.PlayCount != null)
                dict.Add("@PlayCount" + i, sys_playvideo.PlayCount.ToString());
            if (sys_playvideo.Enabled != null)
                dict.Add("@Enabled" + i, sys_playvideo.Enabled.ToString());
            if (sys_playvideo.VieldId != null)
                dict.Add("@VieldId" + i, sys_playvideo.VieldId.ToString());
            if (sys_playvideo.userId != null)
                dict.Add("@userId" + i, sys_playvideo.userId.ToString());
            if (sys_playvideo.price != null)
                dict.Add("@price" + i, sys_playvideo.price.ToString());
            if (sys_playvideo.isPass != null)
                dict.Add("@isPass" + i, sys_playvideo.isPass.ToString());
            if (sys_playvideo.isEnglish != null)
                dict.Add("@isEnglish" + i, sys_playvideo.isEnglish.ToString());
            if (sys_playvideo.TitleE != null)
                dict.Add("@TitleE" + i, sys_playvideo.TitleE.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_playvideo"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Sys_PlayVideo sys_playvideo, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_playvideo set ");
            if (sys_playvideo.Title != null)
                part1.Append($"Title = @Title{i},");
            if (sys_playvideo.TitleUrl != null)
                part1.Append($"TitleUrl = @TitleUrl{i},");
            if (sys_playvideo.Url != null)
                part1.Append($"Url = @Url{i},");
            if (sys_playvideo.LongTime != null)
                part1.Append($"LongTime = @LongTime{i},");
            if (sys_playvideo.EditTime != null)
                part1.Append($"EditTime = @EditTime{i},");
            if (sys_playvideo.PlayCount != null)
                part1.Append($"PlayCount = @PlayCount{i},");
            if (sys_playvideo.Enabled != null)
                part1.Append($"Enabled = @Enabled{i},");
            if (sys_playvideo.VieldId != null)
                part1.Append($"VieldId = @VieldId{i},");
            if (sys_playvideo.userId != null)
                part1.Append($"userId = @userId{i},");
            if (sys_playvideo.price != null)
                part1.Append($"price = @price{i},");
            if (sys_playvideo.isPass != null)
                part1.Append($"isPass = @isPass{i},");
            if (sys_playvideo.isEnglish != null)
                part1.Append($"isEnglish = @isEnglish{i},");
            if (sys_playvideo.TitleE != null)
                part1.Append($"TitleE = @TitleE{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where Id= @Id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_PlayVideo"></param>
        /// <returns></returns>
        public void UpdateList(List<Sys_PlayVideo> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="Sys_PlayVideo"></param>
        /// <returns></returns>
        public Sys_PlayVideo GetById(int id)
        {
            return SqlHelper.Instance.GetById<Sys_PlayVideo>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Sys_PlayVideo"></param>
        /// <returns></returns>
        public List<Sys_PlayVideo> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Sys_PlayVideo>();
        }
    }
}
