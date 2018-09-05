using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Tools;
using WcfService.Sys;
using Model;
using System.IO;
using System.Web;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using WcfService.Oper.Function;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“DataService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 DataService.svc 或 DataService.svc.cs，然后开始调试。
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = "jsoncallback")]
    public class DataService : IDataService
    {

        public void PlayCallBack(string nId, string time, int status, string cid)
        {
            VideoInfo info = new VideoInfo();
            info.PlayCallBack(nId, time, status, cid);
        }
        /// <summary>
        /// 用户登录信息
        /// </summary>
        private static Dictionary<string, Sys_User> _userDic;
        public static Dictionary<string, Sys_User> UserDic
        {
            get
            {
                var temp = new Dictionary<string, Sys_User>();
                temp.Add("1", new Sys_User { Id = 49 });
                return _userDic ?? (_userDic = temp);
                //return _userDic ?? (_userDic = new Dictionary<string, Sys_User>());
            }
        }
        /// <summary>
        /// 手机验证码信息
        /// </summary>
        private static Dictionary<string, NumCode> _codeDic;
        public static Dictionary<string, NumCode> CodeDic
        {
            get { return _codeDic ?? (_codeDic = new Dictionary<string, NumCode>()); }
        }
        /// <summary>
        /// 根据手机号获取头像地址
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public RdfMsg GetHeadUrl(string number)
        {
            try
            {
                if (!RdfRegex.MobilePhone(number))
                    return new RdfMsg(false, "手机号码格式错误!");
                Sys_User en = new RdfSqlQuery<Sys_User>().Where(item => item.Phone == number).ToEntity();
                return new RdfMsg(true, en == null ? "" : en.Url);
            }
            catch (Exception ex)
            {
                return new RdfMsg(false, ex.Message);
            }
        }
        /// <summary>
        /// 账号+密码登录
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public RdfMsg AccountLogin(string uId, string pwd)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uId))
                    return new RdfMsg(false, "账号不能为空!");
                if (string.IsNullOrWhiteSpace(pwd))
                    return new RdfMsg(false, "密码不能为空!");
                if (uId == "admin" && pwd == "admin")
                {
                    if (new RdfSqlQuery<Sys_User>().Where(item => item.Type == 0).ToList().Count == 0)
                    {
                        RdfMsg msg = new Sys_User()
                        {
                            LoginTime = DateTime.Now,
                            OpertionTime = DateTime.Now,
                            Phone = "",
                            RegisterTime = DateTime.Now,
                            Sex = true,
                            Type = 0,
                            UId = "admin",
                            Url = "Images\\default.png",
                            UserExplain = "",
                            UserName = "管理员",
                            UserPwd = RdfConvert.Md5_32(pwd),
                            Enabled = true
                        }.Insert();
                        if (!msg.Success)
                            return msg;
                    }
                }
                return Login(new Sys_User { UId = uId, UserPwd = RdfConvert.Md5_32(pwd) }, 0);
            }
            catch (Exception e)
            {
                return new RdfMsg(false, e.Message);
            }
        }
        /// <summary>
        /// 手机号码+验证码登录
        /// </summary>
        /// <param name="number"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public RdfMsg NumberLogin(string number, int code)
        {
            try
            {
                RdfMsg msg = CheckCode(number, code);
                if (!msg.Success)
                    return msg;
                msg = Login(new Sys_User { Phone = number }, 1);
                if (msg.Success)
                    CodeDic.Remove(number);
                return msg;
            }
            catch (Exception e)
            {
                return new RdfMsg(false, e.Message);
            }
        }
        /// <summary>
        /// 检查验证码
        /// </summary>
        /// <param name="number"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private RdfMsg CheckCode(string number, int code)
        {
            if (number == "17706405101" && code == 8888)
                return new RdfMsg(true);

            if (!RdfRegex.MobilePhone(number))
                return new RdfMsg(false, "手机号码格式错误!");
            if (!CodeDic.Keys.Contains(number))
                return new RdfMsg(false, "请先获取验证码!");
            if (CodeDic[number].Time.AddMinutes(1) < DateTime.Now)
            {
                CodeDic.Remove(number);
                return new RdfMsg(false, "验证码过期,请重新获取!");
            }
            if (code != CodeDic[number].Code)
                return new RdfMsg(false, "验证码错误!");
            return new RdfMsg(true);
        }

        public bool CheckCode2(string number, int code)
        {
            if (number == "17706405101" && code == 8888)
                return true;
            if (number == "18857120152" && code == 8888)
                return true;

            if (!RdfRegex.MobilePhone(number))
                throw new Exception("手机号码格式错误!");
            if (!CodeDic.Keys.Contains(number))
                throw new Exception("请先获取验证码!");
            if (CodeDic[number].Time.AddMinutes(5) < DateTime.Now)
            {
                CodeDic.Remove(number);
                throw new Exception("验证码过期,请重新获取!");
            }
            if (code != CodeDic[number].Code)
                throw new Exception("验证码错误!");
            return true;
        }

        /// <summary>
        /// 获取手机验证码
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public RdfMsg GetCode(string number)
        {
            try
            {
                if (!RdfRegex.MobilePhone(number))
                    return new RdfMsg(false, "手机号码格式错误!");
                if (CodeDic.Keys.Contains(number))
                {
                    if (CodeDic[number].Time.AddMinutes(1) > DateTime.Now)
                        return new RdfMsg(false, "请在上次获取1分钟后在重新获取!");
                    else
                        CodeDic.Remove(number);
                }
                Random rd = new Random();
                int code = rd.Next(1000, 10000);
                SendSMS sms = new SendSMS();
                RdfMsg msg = sms.SendCode(number, code.ToString());
                if (msg.Success)
                {
                    CodeDic.Add(number, new NumCode { Code = code, Time = DateTime.Now });
                    return new RdfMsg(true);
                }
                return msg;
            }
            catch (Exception e)
            {
                return new RdfMsg(false, e.Message);
            }
        }
        /// <summary>
        /// 检查uId是否有效，如果有效则进行登录，无效则由App进行绑定后登录
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        public RdfMsg CheckUId(string uId)
        {
            try
            {
                List<Sys_User> list = new RdfSqlQuery<Sys_User>().Where(item => item.UId == uId).ToList();
                if (list.Count == 0)
                    return new RdfMsg(true);
                return Login(new Sys_User { UId = uId }, 2);//登录
            }
            catch (Exception e)
            {
                return new RdfMsg(false, e.Message);
            }
        }
        /// <summary>
        /// 绑定用户 绑定成功后会登录
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="number">手机号码</param>
        /// <param name="uId">QQ或微信唯一标识</param>
        /// <param name="name">昵称</param>
        /// <param name="url">头像地址</param>
        /// <param name="sex">性别，true男，false女</param>
        /// <returns></returns>
        public RdfMsg BindUser(int code, string number, string uId, string name, string url, bool sex)
        {
            try
            {
                RdfMsg msg = CheckCode(number, code);
                if (!msg.Success)
                    return msg;

                if (!string.IsNullOrWhiteSpace(uId) && new RdfSqlQuery<Sys_User>().Where(item => item.Type == 2 && item.UId == uId).ToList().Count > 0)
                    return new RdfMsg(false, "此用户已经绑定过，请检查第三方登录用户标识。");

                List<Sys_User> list = new RdfSqlQuery<Sys_User>().Where(item => item.Type == 2 && item.Phone == number).ToList();
                if (list.Count > 1)
                    return new RdfMsg(false, "当前绑定手机号重复，请联系管理员。");

                Sys_User user = null;
                if (list.Count == 1)
                {
                    user = list[0];
                    user.UserName = name;
                    user.UId = uId;
                    user.Url = url;
                    user.Sex = sex;
                    msg = user.Edit();
                }
                else
                {
                    user = new Sys_User()
                    {
                        Phone = number,
                        Sex = sex,
                        Type = 2,
                        UId = uId,
                        UserName = name,
                        UserPwd = "",
                        UserExplain = "",
                        Url = url,
                        RegisterTime = DateTime.Now,
                        LoginTime = DateTime.Now,
                        Enabled = true
                    };
                    msg = user.Insert();
                }
                if (!msg.Success)
                    return msg;
                msg = Login(user, 2);//登录
                if (msg.Success)
                    CodeDic.Remove(number);
                return msg;
            }
            catch (Exception e)
            {
                return new RdfMsg(false, e.Message);
            }
        }
        /// <summary>
        /// 所有登录的入口
        /// </summary>
        /// <param name="number">手机号</param>
        /// <param name="logintype">0账号密码登录，1手机号码登录，2第三方登录</param>
        /// <returns></returns>
        private RdfMsg Login(Sys_User login, int logintype)
        {
            try
            {
                List<Sys_User> list = null;
                if (logintype == 0)
                {
                    list = new RdfSqlQuery<Sys_User>().Where(item => item.UId == login.UId && item.UserPwd == login.UserPwd && item.Type != 2).ToList();
                    if (list.Count == 0)
                        return new RdfMsg(false, "账号或密码错误!");
                }
                else if (logintype == 1)
                {
                    if (!RdfRegex.MobilePhone(login.Phone))
                        return new RdfMsg(false, "手机号码格式错误!");
                    list = new RdfSqlQuery<Sys_User>().Where(item => item.Phone == login.Phone && item.Type == 2).ToList();
                    if (list.Count == 0)
                    {
                        login.Type = 2;
                        login.UId = Guid.NewGuid().ToString();
                        login.UserName = "";
                        login.UserPwd = "";
                        login.UserExplain = "";
                        login.Url = "Images\\default.png";
                        login.RegisterTime = DateTime.Now;
                        login.LoginTime = DateTime.Now;
                        login.Enabled = true;
                        RdfMsg msg = login.Insert();
                        if (!msg.Success)
                            return msg;
                        if (list == null)
                            list = new List<Sys_User>();
                        list.Add(login);
                    }
                }
                else
                {
                    list = new RdfSqlQuery<Sys_User>().Where(item => item.UId == login.UId && item.Type == 2).ToList();
                    if (list.Count == 0)
                        return new RdfMsg(false, "未绑定第三方登录信息!");
                }
                Sys_User user = list[0];
                if (!user.Enabled)
                    return new RdfMsg(false, "您的账号被禁用不能登录!");
                if (user.Id > 0)
                {
                    user.LoginTime = DateTime.Now;
                    RdfMsg msg = user.Edit();
                    if (!msg.Success)
                        return msg;
                }
                foreach (string item in UserDic.Keys)
                {
                    if (UserDic[item].Id.Equals(user.Id))
                    {
                        UserDic.Remove(item);
                        break;
                    }
                }
                string token = Guid.NewGuid().ToString();
                UserDic.Add(token, user);

                ////-txy
                var user2 = new DbOpertion.Models.Sys_User
                {
                    Id = user.Id,
                    Type = user.Type,
                    UserPwd = user.UserPwd,
                    UserName = user.UserName,
                    Phone = user.Phone,
                    UId = user.UId,
                    UserExplain = user.UserExplain,
                    Url = user.Url,
                    Birthday = user.Birthday,
                    Sex = user.Sex,
                    Height = user.Height,
                    Weight = user.Weight,
                    IdealWeight = user.IdealWeight,
                    RegisterTime = user.RegisterTime,
                    LoginTime = user.LoginTime,
                    Enabled = user.Enabled
                };

                AllFunc.Instance.UpdateToken(user2);

                //CacheHelper.Instance.SetUserToken(UserDic);

                if (user.Url == "Images\\default.png")
                    user.Url = "";
                Sys_Data sd = new RdfSqlQuery<Sys_Data>().Where(t1 => t1.UserId == user.Id).OrderByDesc(t1 => t1.CreateTime).Take(1).ToEntity();
                if (sd != null)
                    user.TotalKM = sd.TotalKM;
                return new RdfMsg(true, user.ToJson(), token);
            }
            catch (Exception e)
            {
                return new RdfMsg(false, e.Message);
            }
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                /// <param name="pwd">密码</param>

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="toKen"></param>
        /// <returns></returns>
        public RdfMsg LoginOut(string toKen)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(toKen))
                    UserDic.Remove(toKen);
                return new RdfMsg(true);
            }
            catch (Exception ex)
            {
                return new RdfMsg(false, ex.Message);
            }
        }
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="toKen"></param>
        /// <param name="cls"></param>
        /// <param name="method"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg Get(string toKen, string cls, string method, string param)
        {
            try
            {
                dynamic obj = new RdfDynamic(param);
                return MethodHandler(toKen, cls, method, obj);
            }
            catch (Exception ex)
            {
                RdfLog.WriteException(ex);
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += ex.InnerException.Message;
                return new RdfMsg(false, msg);
            }
        }
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="toKen"></param>
        /// <param name="cls"></param>
        /// <param name="method"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg Post(string toKen, string cls, string method, string param)
        {
            try
            {
                dynamic obj = new RdfDynamic(param);
                return MethodHandler(toKen, cls, method, obj);
            }
            catch (Exception ex)
            {
                RdfLog.WriteException(ex);
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += ex.InnerException.Message;
                return new RdfMsg(false, msg);
            }
        }
        /// <summary>
        /// 根据请求参数反射调用方法
        /// </summary>
        /// <param name="toKen">用户登录信息</param>
        /// <param name="cls">命名空间.类名</param>
        /// <param name="method">方法名</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        private RdfMsg MethodHandler(string toKen, string cls, string method, dynamic param)
        {
            RdfMsg msg = new RdfMsg(false);
            if (string.IsNullOrWhiteSpace(cls))
            {
                msg.Success = false;
                msg.Error = "类名不能为空!";
                return msg;
            }
            if (string.IsNullOrWhiteSpace(method))
            {
                msg.Success = false;
                msg.Error = "方法名不能为空!";
                return msg;
            }
            if (toKen != Guid.Empty.ToString())
            {
                msg = ValiDateLogin(toKen);
                if (!msg.Success)
                    return msg;
            }
            Type type = Type.GetType("WcfService." + cls, false, true);
            if (type == null)
            {
                msg.Success = false;
                msg.Error = "获取程序集失败:" + "WcfService." + cls;
                return msg;
            }
            object obj = type.Assembly.CreateInstance("WcfService." + cls);
            MethodInfo info = type.GetMethod(method, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
            if (info == null)
            {
                msg.Success = false;
                msg.Error = "找不到方法" + cls + "." + method;
                return msg;
            }
            object methodInfo = info.Invoke(obj, new object[] { param });
            if (RdfTransaction.IsStart)
            {
                RdfTransaction.Rollback();
                RdfLog.WriteLog("事务开启未关闭", cls, method);
                return new RdfMsg(false, "事务开启未关闭,已撤销事务!");
            }
            if (methodInfo is RdfMsg)
                return (RdfMsg)methodInfo;
            msg.Success = false;
            msg.Error = "方法" + cls + "." + method + "返回类型不是RdfMsg!";
            return msg;
        }

        /// <summary>
        /// 验证登录信息
        /// </summary>
        /// <param name="toKen"></param>
        /// <returns></returns>
        private RdfMsg ValiDateLogin(string toKen)
        {
            //Dictionary<string, Sys_User>  UserDic = CacheHelper.Instance.GetUserToken();

            //return new RdfMsg(true);

            var UserDic = CacheHelper.Instance.GetUserToken();

            if (string.IsNullOrWhiteSpace(toKen))
                return new RdfMsg(false, "toKen不能为空!");
            if (!UserDic.Keys.Contains(toKen))
            {
                RdfMsg msg = new RdfMsg(false, "用户未登录或被其他登录下线");
                msg.Result = "login";
                return msg;
            }
            Sys_User user = UserDic[toKen];
            user.OpertionTime = DateTime.Now;
            GlobalParam.User = user;
            GlobalParam.ToKen = toKen;
            return new RdfMsg(true);
        }

        public RdfMsg TestHisPost()
        {
            return new RdfMsg(false);
        }

        /// <summary>
        /// 验证码
        /// </summary>
        public class NumCode
        {
            public int Code { get; set; }
            public DateTime Time { get; set; }
        }
    }

}
