using DbOpertion.DBoperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerEnd.Controllers.Page
{
    public class SportController : Controller//-txy 权限弄好后，要改判断session的方式
    {
        public ActionResult SportInfo()
        {
            var id = Request["Id"];
            if (id == null)
                return View();
            var isEnglish = Request["isEnglish"];
            var ss = Sys_SportOper.Instance.GetById(Convert.ToInt32(id));
            if (ss == null)
                return View();
            ViewBag.content = isEnglish == "1" ? ss.ContentE : ss.Content;
            return View();
        }


        #region 在common控制器里

        //public ActionResult Index()
        //{
        //    var typeList = Sys_SportTypeOper.Instance.GetEnabledList();
        //    ViewBag.typeList = typeList;
        //    return View();
        //}
        //public ActionResult Type()
        //{
        //    return View();
        //}
        //public ActionResult Msg()
        //{
        //    return View();
        //}

        #endregion



    }
}