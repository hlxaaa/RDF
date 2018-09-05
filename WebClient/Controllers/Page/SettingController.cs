using DbOpertion.DBoperation;
using DbOpertion.Models;
using ServerEnd.Oper.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebClient.Controllers.Page
{
    public class SettingController : Controller//-txy 权限弄好后，要改判断session的方式
    {
        public ActionResult AgreementInfo()
        {

            var isEnglish = Request["isEnglish"] ?? "0";
            var ss = new RegistrationAgreement();
            if (isEnglish == "1")
                ss = RegistrationAgreementOper.Instance.GetById(2);
            else
                ss = RegistrationAgreementOper.Instance.GetById(1);

            //var ss = RegistrationAgreementOper.Instance.GetById(1);
            ViewBag.content = ss.content;
            return View();
        }
        public ActionResult AboutInfo()
        {
            var isEnglish = Request["isEnglish"] ?? "0";
            var ss = new Sys_About();
            if (isEnglish == "1")
                ss = Sys_AboutOper.Instance.GetById(7);
            else
                ss = Sys_AboutOper.Instance.GetById(6);
            ViewBag.content = ss.Content;
            return View();
            //return View();
        }


        #region 在common控制器里
        // GET: Setting
        //public ActionResult ParamSet()
        //{
        //    return View();
        //}

        //public ActionResult Agreement()
        //{
        //    return View();
        //}

        //public ActionResult AgreementE()
        //{
        //    return View();
        //}



        //public ActionResult Suggestion()
        //{
        //    return View();
        //}

        //public ActionResult AndroidVersion()
        //{
        //    return View();
        //}

        //public ActionResult About()
        //{
        //    return View();
        //}

        //public ActionResult AboutE()
        //{
        //    return View();
        //}
        #endregion


    }
}