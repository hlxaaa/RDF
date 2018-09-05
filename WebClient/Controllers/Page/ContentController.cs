using DbOpertion.DBoperation;
using DbOpertion.Models;
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
    public class ContentController : PageBaseController
    {
        #region menuId=3
        public ActionResult VideoList()
        {
            AllFunc.Instance.ClearTempPic();
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(3))
                return View("error");
            ViewBag.coachs = Sys_UserOper.Instance.GetAllCoach();
            return View();
        }

        public ActionResult VideoListForLive()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(3))
                return View("error");
            var liveId = Request["id"];
            if (liveId == null)
                return View("error");
            ViewBag.liveId = liveId;
            ViewBag.coachs = Sys_UserOper.Instance.GetAllCoach();
            return View();
        }

        public ActionResult AudioList()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(3))
                return View("error");
            var typeList = Sys_AudioTypeOper.Instance.GetEnabledList();
            ViewBag.typeList = typeList;
            return View();
        }

        public ActionResult AudioTypeList()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(3))
                return View("error");
            return View();
        }

        public ActionResult LiveList()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(3))
                return View("error");
            return View();
        }

        public ActionResult CoachList()
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(3))
                return View("error");
            return View();
        }

        public ActionResult CoachInfo()
        {
            var user2 = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user2.menuIds.Contains(3))
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
        //-txy 需要审核的   1、申请成为教练 2、教练创建直播审核 3、教练上传视频审核
    }
}