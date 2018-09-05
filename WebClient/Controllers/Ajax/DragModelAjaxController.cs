using Common.Filter.Mvc;
using Common.Result;
using Newtonsoft.Json;
using ServerEnd.Oper.Function;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DbOpertion.Models.Extend.AllModel;

namespace ServerEnd.Controllers.Ajax
{
    public class DragModelAjaxController : AjaxBaseController
    {
        public string GetModelList(GetListCommonReq req)
        {
            var r = DragModelFunc.Instance.GetModelList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string AddModel(AddModelReq req)
        {
            var r = DragModelFunc.Instance.AddModel(req);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateModel(UpdateModelReq req)
        {
            var r = DragModelFunc.Instance.UpdateModel(req);
            return JsonConvert.SerializeObject(r);
        }

        public string DeleteModel(IdReq req)
        {
            var r = DragModelFunc.Instance.DeleteModel(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetModelById(IdReq req)
        {
            var r = DragModelFunc.Instance.GetModelById(req);
            return JsonConvert.SerializeObject(r);
        }
    }
}