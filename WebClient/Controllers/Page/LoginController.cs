using Common.Filter.Mvc;
using Common.Helper;
using Common.Result;
using DbOpertion.DBoperation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DbOpertion.Models.Extend.AllModel;

namespace WebClient.Controllers.Page
{
    [MvcException]
    [MvcValidate]
    public class LoginController : Controller
    {
        public string Login(LoginReq req)
        {
            var uid = req.UId;
            var pwd = req.pwd;
            var user = Sys_UserOper.Instance.GetServerAccountByUId(uid);
            if (user == null)
                throw new Exception("不存在这个账号");
            if (user.UserPwd != MD5Helper.Instance.Md5_32(pwd))
                throw new Exception("密码错误");
            Session.Timeout = 60;
            var list = Sys_UserAuthOper.Instance.GetAuthByUserId(user.Id);
            var rr = new ServerUserItem();
            var gym = GymOper.Instance.GetByUserId((int)list[0].Id);
            if (gym != null)
                rr = new ServerUserItem(list, gym.id);
            else
                rr = new ServerUserItem(list);
            Session["user"] = JsonConvert.SerializeObject(rr);
            //Session["lv"] = user.level;
            //-txy
            var msg = "";
            if (rr.isEnglish == 1)
                msg = "99";
            else
                msg = rr.menuIds.First().ToString();
            var r = new ResultJson(msg);
            return JsonConvert.SerializeObject(r);
            //return JsonHelperHere.JsonMsg(true, "登录成功");
        }

        public string LoginOut()
        {
            Session.Abandon();
            var r = new ResultJson("退出成功");
            return JsonConvert.SerializeObject(r);
        }

        public string ClearSession()
        {
            Session.RemoveAll();
            return "";
        }
    }
}