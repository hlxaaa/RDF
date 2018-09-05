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
    public partial class Sys_SportTypeOper : SingleTon<Sys_SportTypeOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_sporttype"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Sys_SportType sys_sporttype)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_sporttype.Id != null)
                dict.Add("@Id", sys_sporttype.Id.ToString());
            if (sys_sporttype.Name != null)
                dict.Add("@Name", sys_sporttype.Name.ToString());
            if (sys_sporttype.Remark != null)
                dict.Add("@Remark", sys_sporttype.Remark.ToString());
            if (sys_sporttype.Enabled != null)
                dict.Add("@Enabled", sys_sporttype.Enabled.ToString());
            if (sys_sporttype.isEnglish != null)
                dict.Add("@isEnglish", sys_sporttype.isEnglish.ToString());
            if (sys_sporttype.NameE != null)
                dict.Add("@NameE", sys_sporttype.NameE.ToString());
            if (sys_sporttype.RemarkE != null)
                dict.Add("@RemarkE", sys_sporttype.RemarkE.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sys_sporttype"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Sys_SportType sys_sporttype)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (sys_sporttype.Name != null)
            {
                part1.Append("Name,");
                part2.Append("@Name,");
            }
            if (sys_sporttype.Remark != null)
            {
                part1.Append("Remark,");
                part2.Append("@Remark,");
            }
            if (sys_sporttype.Enabled != null)
            {
                part1.Append("Enabled,");
                part2.Append("@Enabled,");
            }
            if (sys_sporttype.isEnglish != null)
            {
                part1.Append("isEnglish,");
                part2.Append("@isEnglish,");
            }
            if (sys_sporttype.NameE != null)
            {
                part1.Append("NameE,");
                part2.Append("@NameE,");
            }
            if (sys_sporttype.RemarkE != null)
            {
                part1.Append("RemarkE,");
                part2.Append("@RemarkE,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into sys_sporttype(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_sporttype"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Sys_SportType sys_sporttype)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_sporttype set ");
            if (sys_sporttype.Name != null)
                part1.Append("Name = @Name,");
            if (sys_sporttype.Remark != null)
                part1.Append("Remark = @Remark,");
            if (sys_sporttype.Enabled != null)
                part1.Append("Enabled = @Enabled,");
            if (sys_sporttype.isEnglish != null)
                part1.Append("isEnglish = @isEnglish,");
            if (sys_sporttype.NameE != null)
                part1.Append("NameE = @NameE,");
            if (sys_sporttype.RemarkE != null)
                part1.Append("RemarkE = @RemarkE,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where Id= @Id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Sys_SportType"></param>
        /// <returns></returns>
        public int Add(Sys_SportType model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_SportType"></param>
        /// <returns></returns>
        public int Update(Sys_SportType model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Sys_SportType"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Sys_SportType where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_sporttype"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Sys_SportType sys_sporttype,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_sporttype.Id != null)
                dict.Add("@Id" + i, sys_sporttype.Id.ToString());
            if (sys_sporttype.Name != null)
                dict.Add("@Name" + i, sys_sporttype.Name.ToString());
            if (sys_sporttype.Remark != null)
                dict.Add("@Remark" + i, sys_sporttype.Remark.ToString());
            if (sys_sporttype.Enabled != null)
                dict.Add("@Enabled" + i, sys_sporttype.Enabled.ToString());
            if (sys_sporttype.isEnglish != null)
                dict.Add("@isEnglish" + i, sys_sporttype.isEnglish.ToString());
            if (sys_sporttype.NameE != null)
                dict.Add("@NameE" + i, sys_sporttype.NameE.ToString());
            if (sys_sporttype.RemarkE != null)
                dict.Add("@RemarkE" + i, sys_sporttype.RemarkE.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_sporttype"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Sys_SportType sys_sporttype, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_sporttype set ");
            if (sys_sporttype.Name != null)
                part1.Append($"Name = @Name{i},");
            if (sys_sporttype.Remark != null)
                part1.Append($"Remark = @Remark{i},");
            if (sys_sporttype.Enabled != null)
                part1.Append($"Enabled = @Enabled{i},");
            if (sys_sporttype.isEnglish != null)
                part1.Append($"isEnglish = @isEnglish{i},");
            if (sys_sporttype.NameE != null)
                part1.Append($"NameE = @NameE{i},");
            if (sys_sporttype.RemarkE != null)
                part1.Append($"RemarkE = @RemarkE{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where Id= @Id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_SportType"></param>
        /// <returns></returns>
        public void UpdateList(List<Sys_SportType> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="Sys_SportType"></param>
        /// <returns></returns>
        public Sys_SportType GetById(int id)
        {
            return SqlHelper.Instance.GetById<Sys_SportType>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Sys_SportType"></param>
        /// <returns></returns>
        public List<Sys_SportType> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Sys_SportType>();
        }
    }
}
