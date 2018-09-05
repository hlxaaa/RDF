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
    public class DataController : ApiBaseController
    {
        [HttpPost]
        public ResultJson UpLoadData(UpLoadDataReq req)
        {
            return AllFunc.Instance.UpLoadData(req);
        }
        [HttpPost]
        public ResultJson AddMsg(AddMsgReq req)
        {
            return AllFunc.Instance.AddMsg(req);
        }
        [HttpPost]
        public ResultJson<GetSportMsgRes> GetSportMsg(GetSportMsgReq req)
        {
            return AllFunc.Instance.GetSportMsg(req);
        }

        public ResultJson UpLoadGymData(UpLoadGymDataReq req)
        {
            return AllFunc.Instance.UpLoadGymData(req);
        }



    }
}
