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
    public partial class Sys_AudioTypeOper : SingleTon<Sys_AudioTypeOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_audiotype"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Sys_AudioType sys_audiotype)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_audiotype.Id != null)
                dict.Add("@Id", sys_audiotype.Id.ToString());
            if (sys_audiotype.Title != null)
                dict.Add("@Title", sys_audiotype.Title.ToString());
            if (sys_audiotype.TitleUrl != null)
                dict.Add("@TitleUrl", sys_audiotype.TitleUrl.ToString());
            if (sys_audiotype.Remark != null)
                dict.Add("@Remark", sys_audiotype.Remark.ToString());
            if (sys_audiotype.Enabled != null)
                dict.Add("@Enabled", sys_audiotype.Enabled.ToString());
            if (sys_audiotype.isEnglish != null)
                dict.Add("@isEnglish", sys_audiotype.isEnglish.ToString());
            if (sys_audiotype.TitleE != null)
                dict.Add("@TitleE", sys_audiotype.TitleE.ToString());
            if (sys_audiotype.RemarkE != null)
                dict.Add("@RemarkE", sys_audiotype.RemarkE.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sys_audiotype"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Sys_AudioType sys_audiotype)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (sys_audiotype.Title != null)
            {
                part1.Append("Title,");
                part2.Append("@Title,");
            }
            if (sys_audiotype.TitleUrl != null)
            {
                part1.Append("TitleUrl,");
                part2.Append("@TitleUrl,");
            }
            if (sys_audiotype.Remark != null)
            {
                part1.Append("Remark,");
                part2.Append("@Remark,");
            }
            if (sys_audiotype.Enabled != null)
            {
                part1.Append("Enabled,");
                part2.Append("@Enabled,");
            }
            if (sys_audiotype.isEnglish != null)
            {
                part1.Append("isEnglish,");
                part2.Append("@isEnglish,");
            }
            if (sys_audiotype.TitleE != null)
            {
                part1.Append("TitleE,");
                part2.Append("@TitleE,");
            }
            if (sys_audiotype.RemarkE != null)
            {
                part1.Append("RemarkE,");
                part2.Append("@RemarkE,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into sys_audiotype(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_audiotype"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Sys_AudioType sys_audiotype)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_audiotype set ");
            if (sys_audiotype.Title != null)
                part1.Append("Title = @Title,");
            if (sys_audiotype.TitleUrl != null)
                part1.Append("TitleUrl = @TitleUrl,");
            if (sys_audiotype.Remark != null)
                part1.Append("Remark = @Remark,");
            if (sys_audiotype.Enabled != null)
                part1.Append("Enabled = @Enabled,");
            if (sys_audiotype.isEnglish != null)
                part1.Append("isEnglish = @isEnglish,");
            if (sys_audiotype.TitleE != null)
                part1.Append("TitleE = @TitleE,");
            if (sys_audiotype.RemarkE != null)
                part1.Append("RemarkE = @RemarkE,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where Id= @Id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Sys_AudioType"></param>
        /// <returns></returns>
        public int Add(Sys_AudioType model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_AudioType"></param>
        /// <returns></returns>
        public int Update(Sys_AudioType model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Sys_AudioType"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Sys_AudioType where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_audiotype"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Sys_AudioType sys_audiotype,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_audiotype.Id != null)
                dict.Add("@Id" + i, sys_audiotype.Id.ToString());
            if (sys_audiotype.Title != null)
                dict.Add("@Title" + i, sys_audiotype.Title.ToString());
            if (sys_audiotype.TitleUrl != null)
                dict.Add("@TitleUrl" + i, sys_audiotype.TitleUrl.ToString());
            if (sys_audiotype.Remark != null)
                dict.Add("@Remark" + i, sys_audiotype.Remark.ToString());
            if (sys_audiotype.Enabled != null)
                dict.Add("@Enabled" + i, sys_audiotype.Enabled.ToString());
            if (sys_audiotype.isEnglish != null)
                dict.Add("@isEnglish" + i, sys_audiotype.isEnglish.ToString());
            if (sys_audiotype.TitleE != null)
                dict.Add("@TitleE" + i, sys_audiotype.TitleE.ToString());
            if (sys_audiotype.RemarkE != null)
                dict.Add("@RemarkE" + i, sys_audiotype.RemarkE.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_audiotype"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Sys_AudioType sys_audiotype, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_audiotype set ");
            if (sys_audiotype.Title != null)
                part1.Append($"Title = @Title{i},");
            if (sys_audiotype.TitleUrl != null)
                part1.Append($"TitleUrl = @TitleUrl{i},");
            if (sys_audiotype.Remark != null)
                part1.Append($"Remark = @Remark{i},");
            if (sys_audiotype.Enabled != null)
                part1.Append($"Enabled = @Enabled{i},");
            if (sys_audiotype.isEnglish != null)
                part1.Append($"isEnglish = @isEnglish{i},");
            if (sys_audiotype.TitleE != null)
                part1.Append($"TitleE = @TitleE{i},");
            if (sys_audiotype.RemarkE != null)
                part1.Append($"RemarkE = @RemarkE{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where Id= @Id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_AudioType"></param>
        /// <returns></returns>
        public void UpdateList(List<Sys_AudioType> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="Sys_AudioType"></param>
        /// <returns></returns>
        public Sys_AudioType GetById(int id)
        {
            return SqlHelper.Instance.GetById<Sys_AudioType>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Sys_AudioType"></param>
        /// <returns></returns>
        public List<Sys_AudioType> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Sys_AudioType>();
        }
    }
}
