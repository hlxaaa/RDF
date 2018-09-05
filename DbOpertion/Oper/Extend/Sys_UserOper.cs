using System.Text;
using Common.Helper;
using System;
using System.Collections.Generic;
using Common;
using System.Linq;
using DbOpertion.Models;
using System.Data.SqlClient;
using Common.Extend;
using static DbOpertion.Models.Extend.AllModel;
using DbOpertion.Models.Extend;

namespace DbOpertion.DBoperation
{
    public partial class Sys_UserOper : SingleTon<Sys_UserOper>
    {

        #region API

        public bool IsPhoneExist(string phone, int userId)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("@phone", phone);
            var str = $"select * from sys_user where phone=@phone and id!={userId}";
            var list = SqlHelper.Instance.ExecuteGetDt<Sys_User>(str, dict);
            if (list.Count == 0)
                return false;
            return true;
        }

        public bool IsUIdExist(string UId, int userId)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("@phone", UId);
            var str = $"select * from sys_user where UId=@phone and id!={userId}";
            var list = SqlHelper.Instance.ExecuteGetDt<Sys_User>(str, dict);
            if (list.Count == 0)
                return false;
            return true;
        }

        public Sys_User GetByPhone(string phone)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("@phone", phone);
            var list = SqlHelper.Instance.GetByCondition<Sys_User>(" phone=@phone", dict);
            if (list.Count == 0)
                return null;
            return list.First();
        }

        public Sys_User IsUIdExist(CheckUIdReq req)
        {
            switch (req.type)
            {
                case 0:
                    return IsQQUId(req.UId);
                case 1:
                    return IsWXUId(req.UId);
                case 2:
                    return IsFBUId(req.UId);
                case 3:
                    return IsTTUId(req.UId);
                default:
                    return null;
            }
        }

        public Sys_User IsQQUId(string uid)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("@uid", uid);
            var list = SqlHelper.Instance.GetByCondition<Sys_User>(" qquid=@uid", dict);
            if (list.Count == 0)
                return null;
            return list.First();
        }

        public Sys_User IsWXUId(string uid)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("@uid", uid);
            var list = SqlHelper.Instance.GetByCondition<Sys_User>(" wxuid=@uid", dict);
            if (list.Count == 0)
                return null;
            return list.First();
        }

        public Sys_User IsFBUId(string uid)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("@uid", uid);
            var list = SqlHelper.Instance.GetByCondition<Sys_User>(" fbuid=@uid", dict);
            if (list.Count == 0)
                return null;
            return list.First();
        }

        public Sys_User IsTTUId(string uid)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("@uid", uid);
            var list = SqlHelper.Instance.GetByCondition<Sys_User>(" ttuid=@uid", dict);
            if (list.Count == 0)
                return null;
            return list.First();
        }
        #endregion

        public Sys_User GetServerAccountByUId(string uid)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("@uid", uid);
            var str = "select * from sys_user where type=0 and uid=@uid";
            var list = SqlHelper.Instance.ExecuteGetDt<Sys_User>(str, dict);
            if (list.Count == 0)
                return null;
            return list.First();
        }

        public int GetAllUserCount()
        {
            var str = "select count(*) from sys_user where type in (1,2)";
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }

        /// <summary>
        /// type=2de 
        /// </summary>
        /// <returns></returns>
        public List<Sys_User> GetUser()
        {
            return SqlHelper.Instance.GetByCondition<Sys_User>(" type=2 ", new Dictionary<string, string>());

            //return SqlHelper.Instance.ExecuteGetDt<Sys_User>(str, new Dictionary<string, string>());
        }

        //userDataView 已经限制type=2了

        public List<Sys_User> GetAllCoach()
        {
            return SqlHelper.Instance.GetByCondition<Sys_User>(" type=1 ");
        }
       
    }
}
