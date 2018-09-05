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
    public partial class UserDataViewOper : SingleTon<UserDataViewOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="userdataview"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(UserDataView userdataview)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (userdataview.Id != null)
                dict.Add("@Id", userdataview.Id.ToString());
            if (userdataview.UserName != null)
                dict.Add("@UserName", userdataview.UserName.ToString());
            if (userdataview.Phone != null)
                dict.Add("@Phone", userdataview.Phone.ToString());
            if (userdataview.Sex != null)
                dict.Add("@Sex", userdataview.Sex.ToString());
            if (userdataview.LoginTime != null)
                dict.Add("@LoginTime", userdataview.LoginTime.ToString());
            if (userdataview.Enabled != null)
                dict.Add("@Enabled", userdataview.Enabled.ToString());
            if (userdataview.UsePlace != null)
                dict.Add("@UsePlace", userdataview.UsePlace.ToString());
            if (userdataview.Address != null)
                dict.Add("@Address", userdataview.Address.ToString());
            if (userdataview.frequency != null)
                dict.Add("@frequency", userdataview.frequency.ToString());
            if (userdataview.KeyName != null)
                dict.Add("@KeyName", userdataview.KeyName.ToString());
            if (userdataview.account != null)
                dict.Add("@account", userdataview.account.ToString());
            if (userdataview.TotalKM != null)
                dict.Add("@TotalKM", userdataview.TotalKM.ToString());
            if (userdataview.TotalTime != null)
                dict.Add("@TotalTime", userdataview.TotalTime.ToString());
            if (userdataview.TotalKal != null)
                dict.Add("@TotalKal", userdataview.TotalKal.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="userdataview"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(UserDataView userdataview)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (userdataview.UserName != null)
            {
                part1.Append("UserName,");
                part2.Append("@UserName,");
            }
            if (userdataview.Phone != null)
            {
                part1.Append("Phone,");
                part2.Append("@Phone,");
            }
            if (userdataview.Sex != null)
            {
                part1.Append("Sex,");
                part2.Append("@Sex,");
            }
            if (userdataview.LoginTime != null)
            {
                part1.Append("LoginTime,");
                part2.Append("@LoginTime,");
            }
            if (userdataview.Enabled != null)
            {
                part1.Append("Enabled,");
                part2.Append("@Enabled,");
            }
            if (userdataview.UsePlace != null)
            {
                part1.Append("UsePlace,");
                part2.Append("@UsePlace,");
            }
            if (userdataview.Address != null)
            {
                part1.Append("Address,");
                part2.Append("@Address,");
            }
            if (userdataview.frequency != null)
            {
                part1.Append("frequency,");
                part2.Append("@frequency,");
            }
            if (userdataview.KeyName != null)
            {
                part1.Append("KeyName,");
                part2.Append("@KeyName,");
            }
            if (userdataview.account != null)
            {
                part1.Append("account,");
                part2.Append("@account,");
            }
            if (userdataview.TotalKM != null)
            {
                part1.Append("TotalKM,");
                part2.Append("@TotalKM,");
            }
            if (userdataview.TotalTime != null)
            {
                part1.Append("TotalTime,");
                part2.Append("@TotalTime,");
            }
            if (userdataview.TotalKal != null)
            {
                part1.Append("TotalKal,");
                part2.Append("@TotalKal,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into userdataview(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="userdataview"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(UserDataView userdataview)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update userdataview set ");
            if (userdataview.UserName != null)
                part1.Append("UserName = @UserName,");
            if (userdataview.Phone != null)
                part1.Append("Phone = @Phone,");
            if (userdataview.Sex != null)
                part1.Append("Sex = @Sex,");
            if (userdataview.LoginTime != null)
                part1.Append("LoginTime = @LoginTime,");
            if (userdataview.Enabled != null)
                part1.Append("Enabled = @Enabled,");
            if (userdataview.UsePlace != null)
                part1.Append("UsePlace = @UsePlace,");
            if (userdataview.Address != null)
                part1.Append("Address = @Address,");
            if (userdataview.frequency != null)
                part1.Append("frequency = @frequency,");
            if (userdataview.KeyName != null)
                part1.Append("KeyName = @KeyName,");
            if (userdataview.account != null)
                part1.Append("account = @account,");
            if (userdataview.TotalKM != null)
                part1.Append("TotalKM = @TotalKM,");
            if (userdataview.TotalTime != null)
                part1.Append("TotalTime = @TotalTime,");
            if (userdataview.TotalKal != null)
                part1.Append("TotalKal = @TotalKal,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where Id= @Id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="UserDataView"></param>
        /// <returns></returns>
        public int Add(UserDataView model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="UserDataView"></param>
        /// <returns></returns>
        public int Update(UserDataView model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="UserDataView"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from UserDataView where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="userdataview"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(UserDataView userdataview,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (userdataview.Id != null)
                dict.Add("@Id" + i, userdataview.Id.ToString());
            if (userdataview.UserName != null)
                dict.Add("@UserName" + i, userdataview.UserName.ToString());
            if (userdataview.Phone != null)
                dict.Add("@Phone" + i, userdataview.Phone.ToString());
            if (userdataview.Sex != null)
                dict.Add("@Sex" + i, userdataview.Sex.ToString());
            if (userdataview.LoginTime != null)
                dict.Add("@LoginTime" + i, userdataview.LoginTime.ToString());
            if (userdataview.Enabled != null)
                dict.Add("@Enabled" + i, userdataview.Enabled.ToString());
            if (userdataview.UsePlace != null)
                dict.Add("@UsePlace" + i, userdataview.UsePlace.ToString());
            if (userdataview.Address != null)
                dict.Add("@Address" + i, userdataview.Address.ToString());
            if (userdataview.frequency != null)
                dict.Add("@frequency" + i, userdataview.frequency.ToString());
            if (userdataview.KeyName != null)
                dict.Add("@KeyName" + i, userdataview.KeyName.ToString());
            if (userdataview.account != null)
                dict.Add("@account" + i, userdataview.account.ToString());
            if (userdataview.TotalKM != null)
                dict.Add("@TotalKM" + i, userdataview.TotalKM.ToString());
            if (userdataview.TotalTime != null)
                dict.Add("@TotalTime" + i, userdataview.TotalTime.ToString());
            if (userdataview.TotalKal != null)
                dict.Add("@TotalKal" + i, userdataview.TotalKal.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="userdataview"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(UserDataView userdataview, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update userdataview set ");
            if (userdataview.UserName != null)
                part1.Append($"UserName = @UserName{i},");
            if (userdataview.Phone != null)
                part1.Append($"Phone = @Phone{i},");
            if (userdataview.Sex != null)
                part1.Append($"Sex = @Sex{i},");
            if (userdataview.LoginTime != null)
                part1.Append($"LoginTime = @LoginTime{i},");
            if (userdataview.Enabled != null)
                part1.Append($"Enabled = @Enabled{i},");
            if (userdataview.UsePlace != null)
                part1.Append($"UsePlace = @UsePlace{i},");
            if (userdataview.Address != null)
                part1.Append($"Address = @Address{i},");
            if (userdataview.frequency != null)
                part1.Append($"frequency = @frequency{i},");
            if (userdataview.KeyName != null)
                part1.Append($"KeyName = @KeyName{i},");
            if (userdataview.account != null)
                part1.Append($"account = @account{i},");
            if (userdataview.TotalKM != null)
                part1.Append($"TotalKM = @TotalKM{i},");
            if (userdataview.TotalTime != null)
                part1.Append($"TotalTime = @TotalTime{i},");
            if (userdataview.TotalKal != null)
                part1.Append($"TotalKal = @TotalKal{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where Id= @Id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="UserDataView"></param>
        /// <returns></returns>
        public void UpdateList(List<UserDataView> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="UserDataView"></param>
        /// <returns></returns>
        public UserDataView GetById(int id)
        {
            return SqlHelper.Instance.GetById<UserDataView>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="UserDataView"></param>
        /// <returns></returns>
        public List<UserDataView> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<UserDataView>();
        }
    }
}
