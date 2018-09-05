using Newtonsoft.Json;
using ServerEnd.Oper.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DbOpertion.Models.Extend.AllModel;

namespace ServerEnd.Controllers.Page
{
    public class PassController : PageBaseController
    {
        #region menuId=1
        public ActionResult Coach()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(1))
                return View("error");
            return View();
        }

        public ActionResult Live()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(1))
                return View("error");
            return View();
        }

        public ActionResult Video()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(1))
                return View("error");
            return View();
        }

        public ActionResult CoachInfo()
        {
            var user2 = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user2.menuIds.Contains(1))
                return View("error");
            var id = Request["id"];
            var r = 0;
            var flag = int.TryParse(id, out r);
            if (!flag)
                return View("Error");

            var user = AllFunc.Instance.GetCoachItem(Convert.ToInt32(id));

            //var user = Sys_UserOper.Instance.GetCoachById(Convert.ToInt32(id));
            if (user == null)
                return View("Error");
            ViewBag.user = user;
            return View();
        }
        #endregion

        #region menuId=7
        public ActionResult TakeCash()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(7))
                return View("error");
            return View();
        }

        public ActionResult MoneyDetail()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(7))
                return View("error");
            return View();
        }
        #endregion

    }
}