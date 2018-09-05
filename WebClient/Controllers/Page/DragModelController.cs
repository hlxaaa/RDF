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
    public class DragModelController : PageBaseController
    {
        #region menuId=5
        public ActionResult ModelList()
        {
         
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(5))
                return View("error");
            return View();
        }
        #endregion
    }
}