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
    public class GymController : PageBaseController
    {
        #region 健身房数据
        public ActionResult Server1()
        {
            var auth = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (auth.isEnglish != 1)
                return View("error");
            ViewBag.userAuth = auth;
            return View();
        }

        public ActionResult Data()
        {
            var auth = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (auth.isEnglish != 1)
                return View("error");
            return View();
        }

        public ActionResult Data1() {
            var auth = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (auth.isEnglish != 1)
                return View("error");
            return View();
        }

        #endregion

        // GET: Gym
        #region menuId=8
        public ActionResult GymList()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(8))
                return View("error");
            return View();
        }

        public ActionResult DeviceList()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(8))
                return View("error");
            var gymId = Request["gymid"] ?? "0";
            ViewBag.gymId = gymId;
            ViewBag.gymList = GymOper.Instance.GetAllList();
            return View();
        }

        public ActionResult DeviceData()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(8))
                return View("error");
            ViewBag.gymList = GymOper.Instance.GetAllList();
            return View();
        }
        #endregion
    }
}