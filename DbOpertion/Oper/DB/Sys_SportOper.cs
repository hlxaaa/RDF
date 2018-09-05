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
    public partial class Sys_SportOper : SingleTon<Sys_SportOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_sport"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Sys_Sport sys_sport)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_sport.Id != null)
                dict.Add("@Id", sys_sport.Id.ToString());
            if (sys_sport.Title != null)
                dict.Add("@Title", sys_sport.Title.ToString());
            if (sys_sport.TitleUrl != null)
                dict.Add("@TitleUrl", sys_sport.TitleUrl.ToString());
            if (sys_sport.TypeId != null)
                dict.Add("@TypeId", sys_sport.TypeId.ToString());
            if (sys_sport.CreateTime != null)
                dict.Add("@CreateTime", sys_sport.CreateTime.ToString());
            if (sys_sport.Content != null)
                dict.Add("@Content", sys_sport.Content.ToString());
            if (sys_sport.Remark != null)
                dict.Add("@Remark", sys_sport.Remark.ToString());
            if (sys_sport.Enabled != null)
                dict.Add("@Enabled", sys_sport.Enabled.ToString());
            if (sys_sport.DataType != null)
                dict.Add("@DataType", sys_sport.DataType.ToString());
            if (sys_sport.ContentE != null)
                dict.Add("@ContentE", sys_sport.ContentE.ToString());
            if (sys_sport.RemarkE != null)
                dict.Add("@RemarkE", sys_sport.RemarkE.ToString());
            if (sys_sport.TitleE != null)
                dict.Add("@TitleE", sys_sport.TitleE.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sys_sport"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Sys_Sport sys_sport)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (sys_sport.Title != null)
            {
                part1.Append("Title,");
                part2.Append("@Title,");
            }
            if (sys_sport.TitleUrl != null)
            {
                part1.Append("TitleUrl,");
                part2.Append("@TitleUrl,");
            }
            if (sys_sport.TypeId != null)
            {
                part1.Append("TypeId,");
                part2.Append("@TypeId,");
            }
            if (sys_sport.CreateTime != null)
            {
                part1.Append("CreateTime,");
                part2.Append("@CreateTime,");
            }
            if (sys_sport.Content != null)
            {
                part1.Append("Content,");
                part2.Append("@Content,");
            }
            if (sys_sport.Remark != null)
            {
                part1.Append("Remark,");
                part2.Append("@Remark,");
            }
            if (sys_sport.Enabled != null)
            {
                part1.Append("Enabled,");
                part2.Append("@Enabled,");
            }
            if (sys_sport.DataType != null)
            {
                part1.Append("DataType,");
                part2.Append("@DataType,");
            }
            if (sys_sport.ContentE != null)
            {
                part1.Append("ContentE,");
                part2.Append("@ContentE,");
            }
            if (sys_sport.RemarkE != null)
            {
                part1.Append("RemarkE,");
                part2.Append("@RemarkE,");
            }
            if (sys_sport.TitleE != null)
            {
                part1.Append("TitleE,");
                part2.Append("@TitleE,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into sys_sport(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_sport"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Sys_Sport sys_sport)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_sport set ");
            if (sys_sport.Title != null)
                part1.Append("Title = @Title,");
            if (sys_sport.TitleUrl != null)
                part1.Append("TitleUrl = @TitleUrl,");
            if (sys_sport.TypeId != null)
                part1.Append("TypeId = @TypeId,");
            if (sys_sport.CreateTime != null)
                part1.Append("CreateTime = @CreateTime,");
            if (sys_sport.Content != null)
                part1.Append("Content = @Content,");
            if (sys_sport.Remark != null)
                part1.Append("Remark = @Remark,");
            if (sys_sport.Enabled != null)
                part1.Append("Enabled = @Enabled,");
            if (sys_sport.DataType != null)
                part1.Append("DataType = @DataType,");
            if (sys_sport.ContentE != null)
                part1.Append("ContentE = @ContentE,");
            if (sys_sport.RemarkE != null)
                part1.Append("RemarkE = @RemarkE,");
            if (sys_sport.TitleE != null)
                part1.Append("TitleE = @TitleE,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where Id= @Id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Sys_Sport"></param>
        /// <returns></returns>
        public int Add(Sys_Sport model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_Sport"></param>
        /// <returns></returns>
        public int Update(Sys_Sport model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Sys_Sport"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Sys_Sport where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_sport"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Sys_Sport sys_sport,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_sport.Id != null)
                dict.Add("@Id" + i, sys_sport.Id.ToString());
            if (sys_sport.Title != null)
                dict.Add("@Title" + i, sys_sport.Title.ToString());
            if (sys_sport.TitleUrl != null)
                dict.Add("@TitleUrl" + i, sys_sport.TitleUrl.ToString());
            if (sys_sport.TypeId != null)
                dict.Add("@TypeId" + i, sys_sport.TypeId.ToString());
            if (sys_sport.CreateTime != null)
                dict.Add("@CreateTime" + i, sys_sport.CreateTime.ToString());
            if (sys_sport.Content != null)
                dict.Add("@Content" + i, sys_sport.Content.ToString());
            if (sys_sport.Remark != null)
                dict.Add("@Remark" + i, sys_sport.Remark.ToString());
            if (sys_sport.Enabled != null)
                dict.Add("@Enabled" + i, sys_sport.Enabled.ToString());
            if (sys_sport.DataType != null)
                dict.Add("@DataType" + i, sys_sport.DataType.ToString());
            if (sys_sport.ContentE != null)
                dict.Add("@ContentE" + i, sys_sport.ContentE.ToString());
            if (sys_sport.RemarkE != null)
                dict.Add("@RemarkE" + i, sys_sport.RemarkE.ToString());
            if (sys_sport.TitleE != null)
                dict.Add("@TitleE" + i, sys_sport.TitleE.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_sport"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Sys_Sport sys_sport, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_sport set ");
            if (sys_sport.Title != null)
                part1.Append($"Title = @Title{i},");
            if (sys_sport.TitleUrl != null)
                part1.Append($"TitleUrl = @TitleUrl{i},");
            if (sys_sport.TypeId != null)
                part1.Append($"TypeId = @TypeId{i},");
            if (sys_sport.CreateTime != null)
                part1.Append($"CreateTime = @CreateTime{i},");
            if (sys_sport.Content != null)
                part1.Append($"Content = @Content{i},");
            if (sys_sport.Remark != null)
                part1.Append($"Remark = @Remark{i},");
            if (sys_sport.Enabled != null)
                part1.Append($"Enabled = @Enabled{i},");
            if (sys_sport.DataType != null)
                part1.Append($"DataType = @DataType{i},");
            if (sys_sport.ContentE != null)
                part1.Append($"ContentE = @ContentE{i},");
            if (sys_sport.RemarkE != null)
                part1.Append($"RemarkE = @RemarkE{i},");
            if (sys_sport.TitleE != null)
                part1.Append($"TitleE = @TitleE{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where Id= @Id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_Sport"></param>
        /// <returns></returns>
        public void UpdateList(List<Sys_Sport> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="Sys_Sport"></param>
        /// <returns></returns>
        public Sys_Sport GetById(int id)
        {
            return SqlHelper.Instance.GetById<Sys_Sport>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Sys_Sport"></param>
        /// <returns></returns>
        public List<Sys_Sport> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Sys_Sport>();
        }
    }
}
