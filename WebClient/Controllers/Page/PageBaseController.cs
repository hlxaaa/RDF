using Newtonsoft.Json;
using System.Web.Mvc;
using static DbOpertion.Models.Extend.AllModel;

namespace ServerEnd.Controllers.Page
{
    public class PageBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!checkLogin())// 判断是否登录
            {
                filterContext.Result = View("Login");
            }
            else
            {
                //通用变量
                ViewBag.userAuth = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
                //ViewBag.AlarmCounts = AllFunc.Instance.GetAlarmCount(user);
                //ViewBag.lastAlarmId = AllFunc.Instance.GetLastAlarmId(user);
            }
            base.OnActionExecuting(filterContext);
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