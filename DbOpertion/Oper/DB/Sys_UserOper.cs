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
    public partial class Sys_UserOper : SingleTon<Sys_UserOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_user"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Sys_User sys_user)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_user.Id != null)
                dict.Add("@Id", sys_user.Id.ToString());
            if (sys_user.Type != null)
                dict.Add("@Type", sys_user.Type.ToString());
            if (sys_user.UserPwd != null)
                dict.Add("@UserPwd", sys_user.UserPwd.ToString());
            if (sys_user.UserName != null)
                dict.Add("@UserName", sys_user.UserName.ToString());
            if (sys_user.Phone != null)
                dict.Add("@Phone", sys_user.Phone.ToString());
            if (sys_user.UId != null)
                dict.Add("@UId", sys_user.UId.ToString());
            if (sys_user.UserExplain != null)
                dict.Add("@UserExplain", sys_user.UserExplain.ToString());
            if (sys_user.Url != null)
                dict.Add("@Url", sys_user.Url.ToString());
            if (sys_user.Birthday != null)
                dict.Add("@Birthday", sys_user.Birthday.ToString());
            if (sys_user.Sex != null)
                dict.Add("@Sex", sys_user.Sex.ToString());
            if (sys_user.Height != null)
                dict.Add("@Height", sys_user.Height.ToString());
            if (sys_user.Weight != null)
                dict.Add("@Weight", sys_user.Weight.ToString());
            if (sys_user.IdealWeight != null)
                dict.Add("@IdealWeight", sys_user.IdealWeight.ToString());
            if (sys_user.RegisterTime != null)
                dict.Add("@RegisterTime", sys_user.RegisterTime.ToString());
            if (sys_user.LoginTime != null)
                dict.Add("@LoginTime", sys_user.LoginTime.ToString());
            if (sys_user.Enabled != null)
                dict.Add("@Enabled", sys_user.Enabled.ToString());
            if (sys_user.UsePlace != null)
                dict.Add("@UsePlace", sys_user.UsePlace.ToString());
            if (sys_user.Address != null)
                dict.Add("@Address", sys_user.Address.ToString());
            if (sys_user.frequency != null)
                dict.Add("@frequency", sys_user.frequency.ToString());
            if (sys_user.KeyName != null)
                dict.Add("@KeyName", sys_user.KeyName.ToString());
            if (sys_user.account != null)
                dict.Add("@account", sys_user.account.ToString());
            if (sys_user.coachImg != null)
                dict.Add("@coachImg", sys_user.coachImg.ToString());
            if (sys_user.isPass != null)
                dict.Add("@isPass", sys_user.isPass.ToString());
            if (sys_user.qqUId != null)
                dict.Add("@qqUId", sys_user.qqUId.ToString());
            if (sys_user.wxUId != null)
                dict.Add("@wxUId", sys_user.wxUId.ToString());
            if (sys_user.fbUId != null)
                dict.Add("@fbUId", sys_user.fbUId.ToString());
            if (sys_user.ttUId != null)
                dict.Add("@ttUId", sys_user.ttUId.ToString());
            if (sys_user.idCardFront != null)
                dict.Add("@idCardFront", sys_user.idCardFront.ToString());
            if (sys_user.idCardBack != null)
                dict.Add("@idCardBack", sys_user.idCardBack.ToString());
            if (sys_user.balance != null)
                dict.Add("@balance", sys_user.balance.ToString());
            if (sys_user.isEnglish != null)
                dict.Add("@isEnglish", sys_user.isEnglish.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sys_user"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Sys_User sys_user)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (sys_user.Type != null)
            {
                part1.Append("Type,");
                part2.Append("@Type,");
            }
            if (sys_user.UserPwd != null)
            {
                part1.Append("UserPwd,");
                part2.Append("@UserPwd,");
            }
            if (sys_user.UserName != null)
            {
                part1.Append("UserName,");
                part2.Append("@UserName,");
            }
            if (sys_user.Phone != null)
            {
                part1.Append("Phone,");
                part2.Append("@Phone,");
            }
            if (sys_user.UId != null)
            {
                part1.Append("UId,");
                part2.Append("@UId,");
            }
            if (sys_user.UserExplain != null)
            {
                part1.Append("UserExplain,");
                part2.Append("@UserExplain,");
            }
            if (sys_user.Url != null)
            {
                part1.Append("Url,");
                part2.Append("@Url,");
            }
            if (sys_user.Birthday != null)
            {
                part1.Append("Birthday,");
                part2.Append("@Birthday,");
            }
            if (sys_user.Sex != null)
            {
                part1.Append("Sex,");
                part2.Append("@Sex,");
            }
            if (sys_user.Height != null)
            {
                part1.Append("Height,");
                part2.Append("@Height,");
            }
            if (sys_user.Weight != null)
            {
                part1.Append("Weight,");
                part2.Append("@Weight,");
            }
            if (sys_user.IdealWeight != null)
            {
                part1.Append("IdealWeight,");
                part2.Append("@IdealWeight,");
            }
            if (sys_user.RegisterTime != null)
            {
                part1.Append("RegisterTime,");
                part2.Append("@RegisterTime,");
            }
            if (sys_user.LoginTime != null)
            {
                part1.Append("LoginTime,");
                part2.Append("@LoginTime,");
            }
            if (sys_user.Enabled != null)
            {
                part1.Append("Enabled,");
                part2.Append("@Enabled,");
            }
            if (sys_user.UsePlace != null)
            {
                part1.Append("UsePlace,");
                part2.Append("@UsePlace,");
            }
            if (sys_user.Address != null)
            {
                part1.Append("Address,");
                part2.Append("@Address,");
            }
            if (sys_user.frequency != null)
            {
                part1.Append("frequency,");
                part2.Append("@frequency,");
            }
            if (sys_user.KeyName != null)
            {
                part1.Append("KeyName,");
                part2.Append("@KeyName,");
            }
            if (sys_user.account != null)
            {
                part1.Append("account,");
                part2.Append("@account,");
            }
            if (sys_user.coachImg != null)
            {
                part1.Append("coachImg,");
                part2.Append("@coachImg,");
            }
            if (sys_user.isPass != null)
            {
                part1.Append("isPass,");
                part2.Append("@isPass,");
            }
            if (sys_user.qqUId != null)
            {
                part1.Append("qqUId,");
                part2.Append("@qqUId,");
            }
            if (sys_user.wxUId != null)
            {
                part1.Append("wxUId,");
                part2.Append("@wxUId,");
            }
            if (sys_user.fbUId != null)
            {
                part1.Append("fbUId,");
                part2.Append("@fbUId,");
            }
            if (sys_user.ttUId != null)
            {
                part1.Append("ttUId,");
                part2.Append("@ttUId,");
            }
            if (sys_user.idCardFront != null)
            {
                part1.Append("idCardFront,");
                part2.Append("@idCardFront,");
            }
            if (sys_user.idCardBack != null)
            {
                part1.Append("idCardBack,");
                part2.Append("@idCardBack,");
            }
            if (sys_user.balance != null)
            {
                part1.Append("balance,");
                part2.Append("@balance,");
            }
            if (sys_user.isEnglish != null)
            {
                part1.Append("isEnglish,");
                part2.Append("@isEnglish,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into sys_user(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_user"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Sys_User sys_user)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_user set ");
            if (sys_user.Type != null)
                part1.Append("Type = @Type,");
            if (sys_user.UserPwd != null)
                part1.Append("UserPwd = @UserPwd,");
            if (sys_user.UserName != null)
                part1.Append("UserName = @UserName,");
            if (sys_user.Phone != null)
                part1.Append("Phone = @Phone,");
            if (sys_user.UId != null)
                part1.Append("UId = @UId,");
            if (sys_user.UserExplain != null)
                part1.Append("UserExplain = @UserExplain,");
            if (sys_user.Url != null)
                part1.Append("Url = @Url,");
            if (sys_user.Birthday != null)
                part1.Append("Birthday = @Birthday,");
            if (sys_user.Sex != null)
                part1.Append("Sex = @Sex,");
            if (sys_user.Height != null)
                part1.Append("Height = @Height,");
            if (sys_user.Weight != null)
                part1.Append("Weight = @Weight,");
            if (sys_user.IdealWeight != null)
                part1.Append("IdealWeight = @IdealWeight,");
            if (sys_user.RegisterTime != null)
                part1.Append("RegisterTime = @RegisterTime,");
            if (sys_user.LoginTime != null)
                part1.Append("LoginTime = @LoginTime,");
            if (sys_user.Enabled != null)
                part1.Append("Enabled = @Enabled,");
            if (sys_user.UsePlace != null)
                part1.Append("UsePlace = @UsePlace,");
            if (sys_user.Address != null)
                part1.Append("Address = @Address,");
            if (sys_user.frequency != null)
                part1.Append("frequency = @frequency,");
            if (sys_user.KeyName != null)
                part1.Append("KeyName = @KeyName,");
            if (sys_user.account != null)
                part1.Append("account = @account,");
            if (sys_user.coachImg != null)
                part1.Append("coachImg = @coachImg,");
            if (sys_user.isPass != null)
                part1.Append("isPass = @isPass,");
            if (sys_user.qqUId != null)
                part1.Append("qqUId = @qqUId,");
            if (sys_user.wxUId != null)
                part1.Append("wxUId = @wxUId,");
            if (sys_user.fbUId != null)
                part1.Append("fbUId = @fbUId,");
            if (sys_user.ttUId != null)
                part1.Append("ttUId = @ttUId,");
            if (sys_user.idCardFront != null)
                part1.Append("idCardFront = @idCardFront,");
            if (sys_user.idCardBack != null)
                part1.Append("idCardBack = @idCardBack,");
            if (sys_user.balance != null)
                part1.Append("balance = @balance,");
            if (sys_user.isEnglish != null)
                part1.Append("isEnglish = @isEnglish,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where Id= @Id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Sys_User"></param>
        /// <returns></returns>
        public int Add(Sys_User model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_User"></param>
        /// <returns></returns>
        public int Update(Sys_User model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Sys_User"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Sys_User where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_user"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Sys_User sys_user,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_user.Id != null)
                dict.Add("@Id" + i, sys_user.Id.ToString());
            if (sys_user.Type != null)
                dict.Add("@Type" + i, sys_user.Type.ToString());
            if (sys_user.UserPwd != null)
                dict.Add("@UserPwd" + i, sys_user.UserPwd.ToString());
            if (sys_user.UserName != null)
                dict.Add("@UserName" + i, sys_user.UserName.ToString());
            if (sys_user.Phone != null)
                dict.Add("@Phone" + i, sys_user.Phone.ToString());
            if (sys_user.UId != null)
                dict.Add("@UId" + i, sys_user.UId.ToString());
            if (sys_user.UserExplain != null)
                dict.Add("@UserExplain" + i, sys_user.UserExplain.ToString());
            if (sys_user.Url != null)
                dict.Add("@Url" + i, sys_user.Url.ToString());
            if (sys_user.Birthday != null)
                dict.Add("@Birthday" + i, sys_user.Birthday.ToString());
            if (sys_user.Sex != null)
                dict.Add("@Sex" + i, sys_user.Sex.ToString());
            if (sys_user.Height != null)
                dict.Add("@Height" + i, sys_user.Height.ToString());
            if (sys_user.Weight != null)
                dict.Add("@Weight" + i, sys_user.Weight.ToString());
            if (sys_user.IdealWeight != null)
                dict.Add("@IdealWeight" + i, sys_user.IdealWeight.ToString());
            if (sys_user.RegisterTime != null)
                dict.Add("@RegisterTime" + i, sys_user.RegisterTime.ToString());
            if (sys_user.LoginTime != null)
                dict.Add("@LoginTime" + i, sys_user.LoginTime.ToString());
            if (sys_user.Enabled != null)
                dict.Add("@Enabled" + i, sys_user.Enabled.ToString());
            if (sys_user.UsePlace != null)
                dict.Add("@UsePlace" + i, sys_user.UsePlace.ToString());
            if (sys_user.Address != null)
                dict.Add("@Address" + i, sys_user.Address.ToString());
            if (sys_user.frequency != null)
                dict.Add("@frequency" + i, sys_user.frequency.ToString());
            if (sys_user.KeyName != null)
                dict.Add("@KeyName" + i, sys_user.KeyName.ToString());
            if (sys_user.account != null)
                dict.Add("@account" + i, sys_user.account.ToString());
            if (sys_user.coachImg != null)
                dict.Add("@coachImg" + i, sys_user.coachImg.ToString());
            if (sys_user.isPass != null)
                dict.Add("@isPass" + i, sys_user.isPass.ToString());
            if (sys_user.qqUId != null)
                dict.Add("@qqUId" + i, sys_user.qqUId.ToString());
            if (sys_user.wxUId != null)
                dict.Add("@wxUId" + i, sys_user.wxUId.ToString());
            if (sys_user.fbUId != null)
                dict.Add("@fbUId" + i, sys_user.fbUId.ToString());
            if (sys_user.ttUId != null)
                dict.Add("@ttUId" + i, sys_user.ttUId.ToString());
            if (sys_user.idCardFront != null)
                dict.Add("@idCardFront" + i, sys_user.idCardFront.ToString());
            if (sys_user.idCardBack != null)
                dict.Add("@idCardBack" + i, sys_user.idCardBack.ToString());
            if (sys_user.balance != null)
                dict.Add("@balance" + i, sys_user.balance.ToString());
            if (sys_user.isEnglish != null)
                dict.Add("@isEnglish" + i, sys_user.isEnglish.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_user"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Sys_User sys_user, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_user set ");
            if (sys_user.Type != null)
                part1.Append($"Type = @Type{i},");
            if (sys_user.UserPwd != null)
                part1.Append($"UserPwd = @UserPwd{i},");
            if (sys_user.UserName != null)
                part1.Append($"UserName = @UserName{i},");
            if (sys_user.Phone != null)
                part1.Append($"Phone = @Phone{i},");
            if (sys_user.UId != null)
                part1.Append($"UId = @UId{i},");
            if (sys_user.UserExplain != null)
                part1.Append($"UserExplain = @UserExplain{i},");
            if (sys_user.Url != null)
                part1.Append($"Url = @Url{i},");
            if (sys_user.Birthday != null)
                part1.Append($"Birthday = @Birthday{i},");
            if (sys_user.Sex != null)
                part1.Append($"Sex = @Sex{i},");
            if (sys_user.Height != null)
                part1.Append($"Height = @Height{i},");
            if (sys_user.Weight != null)
                part1.Append($"Weight = @Weight{i},");
            if (sys_user.IdealWeight != null)
                part1.Append($"IdealWeight = @IdealWeight{i},");
            if (sys_user.RegisterTime != null)
                part1.Append($"RegisterTime = @RegisterTime{i},");
            if (sys_user.LoginTime != null)
                part1.Append($"LoginTime = @LoginTime{i},");
            if (sys_user.Enabled != null)
                part1.Append($"Enabled = @Enabled{i},");
            if (sys_user.UsePlace != null)
                part1.Append($"UsePlace = @UsePlace{i},");
            if (sys_user.Address != null)
                part1.Append($"Address = @Address{i},");
            if (sys_user.frequency != null)
                part1.Append($"frequency = @frequency{i},");
            if (sys_user.KeyName != null)
                part1.Append($"KeyName = @KeyName{i},");
            if (sys_user.account != null)
                part1.Append($"account = @account{i},");
            if (sys_user.coachImg != null)
                part1.Append($"coachImg = @coachImg{i},");
            if (sys_user.isPass != null)
                part1.Append($"isPass = @isPass{i},");
            if (sys_user.qqUId != null)
                part1.Append($"qqUId = @qqUId{i},");
            if (sys_user.wxUId != null)
                part1.Append($"wxUId = @wxUId{i},");
            if (sys_user.fbUId != null)
                part1.Append($"fbUId = @fbUId{i},");
            if (sys_user.ttUId != null)
                part1.Append($"ttUId = @ttUId{i},");
            if (sys_user.idCardFront != null)
                part1.Append($"idCardFront = @idCardFront{i},");
            if (sys_user.idCardBack != null)
                part1.Append($"idCardBack = @idCardBack{i},");
            if (sys_user.balance != null)
                part1.Append($"balance = @balance{i},");
            if (sys_user.isEnglish != null)
                part1.Append($"isEnglish = @isEnglish{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where Id= @Id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_User"></param>
        /// <returns></returns>
        public void UpdateList(List<Sys_User> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="Sys_User"></param>
        /// <returns></returns>
        public Sys_User GetById(int id)
        {
            return SqlHelper.Instance.GetById<Sys_User>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Sys_User"></param>
        /// <returns></returns>
        public List<Sys_User> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Sys_User>();
        }
    }
}
