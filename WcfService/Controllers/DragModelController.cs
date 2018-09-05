using Common.Attribute;
using Common.Helper;
using Common.Result;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using DbOpertion.Models.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WcfService.Oper.Function;

namespace WcfService.Controllers
{
    [ValidateModel]
    public class DragModelController : ApiBaseController
    {
        [HttpPost]
        public ResultJson<ModelListAllRes> GetDragModelList(GetDragModelListReq req)
        {
            return AllFunc.Instance.GetDragModelList(req);
        }
    }
}
