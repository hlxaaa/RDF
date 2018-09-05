using DbOpertion.DBoperation;
using Newtonsoft.Json;
using ServerEnd.Controllers.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DbOpertion.Models.Extend.AllModel;

namespace WebClient.Controllers.Page
{
    public class CommonController : PageBaseController
    {
        #region menuId=4
        public ActionResult Suggestion()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(4))
                return View("error");
            return View();
        }

        public ActionResult AndroidVersion()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(4))
                return View("error");
            return View();
        }

        public ActionResult About()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(4))
                return View("error");
            return View();
        }

        public ActionResult AboutE()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(4))
                return View("error");
            return View();
        }

        public ActionResult ParamSet()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(4))
                return View("error");
            return View();
        }

        public ActionResult Agreement()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(4))
                return View("error");
            return View();
        }

        public ActionResult AgreementE()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(4))
                return View("error");
            return View();
        } 
        #endregion

        #region menuId=2
        public ActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(2))
                return View("error");
            var typeList = Sys_SportTypeOper.Instance.GetEnabledList();
            ViewBag.typeList = typeList;
            return View();
        }

        public ActionResult Type()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(2))
                return View("error");
            return View();
        }
        public ActionResult Msg()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(2))
                return View("error");
            return View();
        }
        #endregion

        /// <summary>
        /// 假的
        /// </summary>
        /// <returns></returns>
        public ActionResult MsgInfo()
        {
            return View();
        }
    }
}