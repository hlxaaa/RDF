using BeIT.MemCached;
using Common;
using Common.Helper;
using DbOpertion.Models.Extend;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Sys;
using static DbOpertion.Models.Extend.AllModel;

namespace WcfService.Oper.Function
{
    public class CacheHelper : SingleTon<CacheHelper>
    {
        public MemcachedClient cache = MemCacheHelper.GetMyConfigInstance();
        public double TokenInvalidOutTimeDays = 28;

        public void ClearCache(string name)
        {
            cache.Delete(name);
        }

        /// <summary>
        /// 设置用户注册验证码
        /// </summary>
        /// <param name="Phone">用户手机</param>
        /// <returns></returns>
        public string SetUserVerificationCode(string Phone)
        {
            var code = cache.Get("Ta_VerificationCode_UserPhone=" + Phone);
            if (code == null)
            {
                string VerificationCode = RandHelper.Instance.Number(4);
                cache.Set($"VerificationCode_UserPhone={Phone}", VerificationCode, new TimeSpan(0, 5, 0));
                //CacheHelper.Set("Ta_VerificationCode_UserPhone=" + Phone, VerificationCode, new TimeSpan(0, 5, 0));//TimeSpan(0, 5, 0)保留5分钟,测试暂时保留一天-txy
                return VerificationCode;
            }
            return (string)code;
        }

        /// <summary>
        /// 获取手机号对应验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>验证码或null</returns>
        public string GetUserVerificationCode(string phone)
        {
            return (string)cache.Get("VerificationCode_UserPhone=" + phone);
        }

        public void SetUserToken(Dictionary<string, Sys_User> dict)
        {
            cache.Set("tokens", JsonConvert.SerializeObject(dict), DateTime.Now.AddDays(TokenInvalidOutTimeDays));
        }

        public Dictionary<string, Sys_User> GetUserToken()
        {
            var r = cache.Get("tokens");
            if (r == null)
                return new Dictionary<string, Sys_User>();
            return JsonConvert.DeserializeObject<Dictionary<string, Sys_User>>(r.ToString());
        }

        public void SetUserToken2(Dictionary<string, int> dict)
        {
            //var dict = new Dictionary<string, int>();
            //dict.Add(token, userId);
            cache.Set("tokens2", JsonConvert.SerializeObject(dict));
        }

        //public void SetUserToken2(string token, int userId)
        //{
        //    var dict = new Dictionary<string, int>();
        //    dict.Add(token, userId);
        //    cache.Set("tokens2", JsonConvert.SerializeObject(dict));
        //}

        public Dictionary<string, int> GetUserToken2()
        {
            var r = cache.Get("tokens2");
            if (r == null)
                return new Dictionary<string, int>();
            return JsonConvert.DeserializeObject<Dictionary<string, int>>(r.ToString());
        }

        public void SetActiveUser(int userId)
        {
            var r = new ActiveUser();
            r.activeTime = DateTime.Now;
            r.userId = userId;
            var c = (string)cache.Get("ActiveUser");
            if (c == null)
            {
                var list = new List<ActiveUser>();
                list.Add(r);
                cache.Set("ActiveUser", JsonConvert.SerializeObject(list), DateTime.Now.AddDays(TokenInvalidOutTimeDays));
            }
            else
            {
                var list = JsonConvert.DeserializeObject<List<ActiveUser>>(c);
                var temp = list.Where(p => p.userId == userId).ToList();
                if (temp.Count == 0)
                {
                    list.Add(r);
                    cache.Set("ActiveUser", JsonConvert.SerializeObject(list), DateTime.Now.AddDays(TokenInvalidOutTimeDays));
                }
                else
                {
                    list.Where(p => p.userId == userId).First().activeTime = DateTime.Now;
                    cache.Set("ActiveUser", JsonConvert.SerializeObject(list), DateTime.Now.AddDays(TokenInvalidOutTimeDays));
                }
            }
        }

        public List<ActiveUser> GetActiveUser()
        {
            var c = (string)cache.Get("ActiveUser");
            if (c == null)
                return new List<ActiveUser>();
            var list = JsonConvert.DeserializeObject<List<ActiveUser>>(c);
            var t = DateTime.Now;
            var dt = new DateTime(t.Year, t.Month, t.Day);
            return list.Where(p => p.activeTime > dt).ToList();
        }

        public string RemoveTimestamp(string url)
        {
            var i = url.LastIndexOf('?');
            if (i < 0)
                return url;
            return url.Substring(0, i);
        }

        public void SetGymDataUserUrl(int did, string url, string name)
        {
            var list = new List<GymData>();
            var c = (string)cache.Get("GymData");
            var item = new GymData
            {
                deviceId = did,
                Url = RemoveTimestamp(url),
                UserName = name
            };
            if (c != null)
            {
                list = JsonConvert.DeserializeObject<List<GymData>>(c);
                var tempList = list.Where(p => p.deviceId == item.deviceId).ToList();
                if (tempList.Count() > 0)
                    list.Remove(tempList.First());
            }
            list.Add(item);
            cache.Set("GymData", JsonConvert.SerializeObject(list), DateTime.Now.AddDays(TokenInvalidOutTimeDays));
        }

        public void SetGymData(UpLoadGymDataReq req, int userId)
        {
            var list = new List<GymData>();
            var c = (string)cache.Get("GymData");
            var item = new GymData(req, userId);
            if (c != null)
            {
                list = JsonConvert.DeserializeObject<List<GymData>>(c);
                var tempList = list.Where(p => p.deviceId == item.deviceId).ToList();
                if (tempList.Count() > 0)
                {
                    var temp = tempList.First();
                    var url = temp.Url;
                    var name = temp.UserName;
                    list.Remove(temp);
                    item.Url = url;
                    item.UserName = name;
                }
            }
            list.Add(item);
            cache.Set("GymData", JsonConvert.SerializeObject(list), DateTime.Now.AddDays(TokenInvalidOutTimeDays));
        }

        public List<GymData> GetGymData()
        {
            var c = (string)cache.Get("GymData");
            if (c == null)
                return new List<GymData>();
            return JsonConvert.DeserializeObject<List<GymData>>(c);
        }
    }
}