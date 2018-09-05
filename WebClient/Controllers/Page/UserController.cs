using DbOpertion.DBoperation;
using Newtonsoft.Json;
using ServerEnd.Oper.Function;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DbOpertion.Models.Extend.AllModel;

namespace ServerEnd.Controllers.Page
{
    public class UserController : PageBaseController
    {
        // GET: User
        public ActionResult Index()
        {
            return View("Login");
        }

        #region menuId=6
        public ActionResult UserList()
        {
            var user2 = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user2.menuIds.Contains(6))
                return View("error");
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(1))
                return View("error");
            string url = ConfigurationManager.AppSettings.Get("imgHost");
            url += "UpLoadFile\\AppHeadImage\\14.jpg?ts=1532744796918";
            ViewBag.url = url;
            return View();
        }

        public ActionResult UserData()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(6))
                return View("error");

            var id = Request["id"];
            //if (id == null)
            //    return View("error");
            ViewBag.id = id ?? "0";
            ViewBag.Data3 = AllFunc.Instance.GetData3();
            ViewBag.users = Sys_UserOper.Instance.GetUser();
            return View();
        } 
        #endregion

        #region menuId=4
        public ActionResult UserAuth()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(4))
                return View("error");
            return View();
        }
        #endregion
    }
}