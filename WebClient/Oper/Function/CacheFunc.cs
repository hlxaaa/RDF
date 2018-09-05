using BeIT.MemCached;
using Common;
using Common.Helper;
using Common.Result;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using DbOpertion.Models.Extend;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DbOpertion.Models.Extend.AllModel;

namespace ServerEnd.Oper.Function
{
    public class CacheFunc : SingleTon<CacheFunc>
    {
        public MemcachedClient cache = MemCacheHelper.GetMyConfigInstance();
        public double TokenInvalidOutTimeDays = 28;
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
                string VerificationCode = RandHelper.Instance.Number(6);
                cache.Set($"VerificationCode_UserPhone={Phone}", VerificationCode);
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

        public void SetToken(string token, int id)
        {
            cache.Set("UserToken_" + id, token, DateTime.Now.AddDays(TokenInvalidOutTimeDays));
        }
        public string GetToken(int id)
        {
            var r = cache.Get("UserToken_" + id);
            if (r == null)
                return "";
            return r.ToString();
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

        public List<GymData> GetGymData()
        {
            var c = (string)cache.Get("GymData");
            if (c == null)
                return new List<GymData>();
            return JsonConvert.DeserializeObject<List<GymData>>(c);
        }

        public void SetClearTempTime()
        {
            cache.Set("ClearTempTime", DateTime.Now, DateTime.Now.AddDays(TokenInvalidOutTimeDays));
        }

        public bool IsNeedClear()
        {
            var c = (DateTime?)cache.Get("ClearTempTime");
            if (c == null || c < DateTime.Now.AddDays(-1))
                return true;
            return false;
        }
    }
}
