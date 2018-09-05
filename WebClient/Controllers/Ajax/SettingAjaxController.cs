using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.vod.Model.V20170321;
using Common.Filter.Mvc;
using Common.Helper;
using Common.Result;
using Newtonsoft.Json;
using ServerEnd.Controllers.Ajax;
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

namespace WebClient.Controllers.Ajax
{
    public class SettingAjaxController : AjaxBaseController
    {
        public string DeleteVersion(IdReq req)
        {
            var r = AllFunc.Instance.DeleteVersion(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetVersionById(IdReq req)
        {
            var r = AllFunc.Instance.GetVersionById(req);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateVersion(UpdateVersionReq req)
        {
            var r = AllFunc.Instance.UpdateVersion(req);
            return JsonConvert.SerializeObject(r);
        }


        public string SetPwd(SetPwdReq req)
        {
            var auth = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            req.userId = auth.Id;
            var r = AllFunc.Instance.SetPwd(req);
            Session.Abandon();
            return JsonConvert.SerializeObject(r);
        }

        public string AddNewVersion(AddNewVersionReq req)
        {
            var r = AllFunc.Instance.AddNewVersion(req);
            return JsonConvert.SerializeObject(r);
        }

        public string Reply(ReplyReq req)
        {
            var r = AllFunc.Instance.Reply(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetVersionList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetVersionList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetFeedBackList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetFeedBackList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetAgreement()
        {
            var r = AllFunc.Instance.GetAgreement(0);
            return JsonConvert.SerializeObject(r);
        }
        public string GetAgreementE()
        {
            var r = AllFunc.Instance.GetAgreement(1);
            return JsonConvert.SerializeObject(r);
        }

        public string GetAbout()
        {
            var r = AllFunc.Instance.GetAbout(0);
            return JsonConvert.SerializeObject(r);
        }

        public string GetAboutE()
        {
            var r = AllFunc.Instance.GetAbout(1);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateAgreement(UpdateAgreementReq req)
        {
            var r = AllFunc.Instance.UpdateAgreement(req, 0);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateAgreementE(UpdateAgreementReq req)
        {
            var r = AllFunc.Instance.UpdateAgreement(req, 1);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateAbout(UpdateAgreementReq req)
        {
            var r = AllFunc.Instance.UpdateAbout(req, 0);
            return JsonConvert.SerializeObject(r);
        }
        public string UpdateAboutE(UpdateAgreementReq req)
        {
            var r = AllFunc.Instance.UpdateAbout(req, 1);
            return JsonConvert.SerializeObject(r);
        }

        /// <summary>
        /// 富文本专用
        /// </summary>
        /// <returns></returns>
        public string UpImg()
        {
            var files = Request.Files;
            if (files.Count <= 0)
            {
                return "";
            }
            HttpPostedFileBase file = files[0];
            if (file == null)
            {
                return "error|file is null";
            }
            else
            {
                return AllFunc.Instance.UpImgAgreement(file);
            }
        }

        public string GetParamSet()
        {
            var r = AllFunc.Instance.GetParamSet();
            return JsonConvert.SerializeObject(r);
        }

        public string GetParamSetItem(TestReq req)
        {
            var r = AllFunc.Instance.GetParamSetItem(req);
            return JsonConvert.SerializeObject(r);
        }

        public string AddParamSet(AddParamSetReq req)
        {
            var r = AllFunc.Instance.AddParamSet(req);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateParamSet(UpdateParamSet req)
        {
            var r = AllFunc.Instance.UpdateParamSet(req);
            return JsonConvert.SerializeObject(r);
        }
    }
}