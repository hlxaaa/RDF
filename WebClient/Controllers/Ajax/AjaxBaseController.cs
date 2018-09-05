using Common.Filter.Mvc;
using Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerEnd.Controllers.Ajax
{
    [MvcException]
    [MvcValidate]
    public class AjaxBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!this.checkLogin())// 判断是否登录
            {
                ResultJson r = new ResultJson();
                r.HttpCode = 300;
                //r.Message = "";
                //r. = "{}";
                JsonResult jr = new JsonResult();
                jr.ContentType = "application/json";
                jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                jr.Data = r;
                filterContext.Result = jr;
            }
            //base.OnActionExecuting(filterContext);
        }

        public bool checkLogin()
        {
            var a = Session["user"];
            if (a == null)
            {
                return false;
            }
            return true;
        }
    }
}