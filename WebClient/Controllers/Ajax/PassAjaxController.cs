using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.vod.Model.V20170321;
using Common.Filter.Mvc;
using Common.Helper;
using Common.Result;
using Newtonsoft.Json;
using ServerEnd.Oper.Function;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DbOpertion.Models.Extend.AllModel;

namespace ServerEnd.Controllers.Ajax
{
    public class PassAjaxController : AjaxBaseController
    {
        public string GetMoneyDetailList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetMoneyDetailList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string PassTakeCash(IdReq req)
        {
            var r = AllFunc.Instance.PassTakeCash(req);
            return JsonConvert.SerializeObject(r);
        }

        public string TakeCashList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.TakeCashList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetVideoListByCoachId(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetVideoListByCoachId(req);
            return JsonConvert.SerializeObject(r);
        }

        /// <summary>
        /// 获取待审核、未过审的教练
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string GetCoachWaitingList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetCoachWaitingList2(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetLiveWaitingList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetLiveWaitingList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetVideoWaitingList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetVideoWaitingList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string PassCoach(IdReq req)
        {
            var r = AllFunc.Instance.PassCoach(req);
            return JsonConvert.SerializeObject(r);
        }

        public string PassLive(IdReq req)
        {
            var r = AllFunc.Instance.PassLive(req);
            return JsonConvert.SerializeObject(r);
        }

        public string PassVideo(IdReq req)
        {
            var r = AllFunc.Instance.PassVideo(req);
            return JsonConvert.SerializeObject(r);
        }

        public string NotPassCoach(NotPassReq req)
        {
            var r = AllFunc.Instance.NotPassCoach(req);
            return JsonConvert.SerializeObject(r);
        }

        public string NotPassLive(NotPassReq req)
        {
            var r = AllFunc.Instance.NotPassLive(req);
            return JsonConvert.SerializeObject(r);
        }

        public string NotPassVideo(NotPassReq req)
        {
            var r = AllFunc.Instance.NotPassVideo(req);
            return JsonConvert.SerializeObject(r);
        }
    }
}