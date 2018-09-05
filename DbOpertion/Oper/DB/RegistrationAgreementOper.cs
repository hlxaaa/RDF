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
    public partial class RegistrationAgreementOper : SingleTon<RegistrationAgreementOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="registrationagreement"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(RegistrationAgreement registrationagreement)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (registrationagreement.id != null)
                dict.Add("@id", registrationagreement.id.ToString());
            if (registrationagreement.content != null)
                dict.Add("@content", registrationagreement.content.ToString());
            if (registrationagreement.editTime != null)
                dict.Add("@editTime", registrationagreement.editTime.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="registrationagreement"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(RegistrationAgreement registrationagreement)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (registrationagreement.content != null)
            {
                part1.Append("content,");
                part2.Append("@content,");
            }
            if (registrationagreement.editTime != null)
            {
                part1.Append("editTime,");
                part2.Append("@editTime,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into registrationagreement(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="registrationagreement"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(RegistrationAgreement registrationagreement)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update registrationagreement set ");
            if (registrationagreement.content != null)
                part1.Append("content = @content,");
            if (registrationagreement.editTime != null)
                part1.Append("editTime = @editTime,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where id= @id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="RegistrationAgreement"></param>
        /// <returns></returns>
        public int Add(RegistrationAgreement model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="RegistrationAgreement"></param>
        /// <returns></returns>
        public int Update(RegistrationAgreement model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="RegistrationAgreement"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from RegistrationAgreement where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="registrationagreement"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(RegistrationAgreement registrationagreement,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (registrationagreement.id != null)
                dict.Add("@id" + i, registrationagreement.id.ToString());
            if (registrationagreement.content != null)
                dict.Add("@content" + i, registrationagreement.content.ToString());
            if (registrationagreement.editTime != null)
                dict.Add("@editTime" + i, registrationagreement.editTime.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="registrationagreement"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(RegistrationAgreement registrationagreement, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update registrationagreement set ");
            if (registrationagreement.content != null)
                part1.Append($"content = @content{i},");
            if (registrationagreement.editTime != null)
                part1.Append($"editTime = @editTime{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where id= @id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="RegistrationAgreement"></param>
        /// <returns></returns>
        public void UpdateList(List<RegistrationAgreement> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="RegistrationAgreement"></param>
        /// <returns></returns>
        public RegistrationAgreement GetById(int id)
        {
            return SqlHelper.Instance.GetById<RegistrationAgreement>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="RegistrationAgreement"></param>
        /// <returns></returns>
        public List<RegistrationAgreement> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<RegistrationAgreement>();
        }
    }
}
