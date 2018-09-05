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
    public class SportAjaxController : AjaxBaseController
    {
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
                return AllFunc.Instance.UpImg(file);
            }
        }

        public string GetSportById(IdReq req)
        {
            var r = AllFunc.Instance.GetSportById(req);
            return JsonConvert.SerializeObject(r);
        }

        public string AddSport(AddSportReq req)
        {
            var r = AllFunc.Instance.AddSport(req);
            return JsonConvert.SerializeObject(r);
        }

        /// <summary>
        /// 只添加中文版，英文版通过更新来
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string AddSport2(AddSportReq req)
        {
            var r = AllFunc.Instance.AddSport2(req);
            return JsonConvert.SerializeObject(r);
        }


        public string UpdateSport(UpdateSportReq req)
        {
            var r = AllFunc.Instance.UpdateSport(req);
            return JsonConvert.SerializeObject(r);
        }

        /// <summary>
        /// 更新中文版
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string UpdateSport1(UpdateSportReq req)
        {
            var r = AllFunc.Instance.UpdateSport1(req);
            return JsonConvert.SerializeObject(r);
        }

        /// <summary>
        /// 更新英文版
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string UpdateSportE(UpdateSportReq req)
        {
            var r = AllFunc.Instance.UpdateSportE(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetSportList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetSportList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string ToggleSport(ToggleVideoReq req)
        {
            var r = AllFunc.Instance.ToggleSport(req);
            return JsonConvert.SerializeObject(r);
        }


        #region SportType
        public string GetSportTypeList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetSportTypeList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetSportTypeById(IdReq req)
        {
            var r = AllFunc.Instance.GetSportTypeById(req);
            return JsonConvert.SerializeObject(r);
        }

        public string AddSportType(AddSportTypeReq req)
        {
            var r = AllFunc.Instance.AddSportType(req);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateSportType(UpdateSportTypeReq req)
        {
            var r = AllFunc.Instance.UpdateSportType(req);
            return JsonConvert.SerializeObject(r);
        }

        public string ToggleSportType(ToggleVideoReq req)
        {
            var r = AllFunc.Instance.ToggleSportType(req);
            return JsonConvert.SerializeObject(r);
        }
        #endregion


        #region SportMsg
        public string GetSportMsgList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetSportMsgList(req);
            return JsonConvert.SerializeObject(r);
        }
        #endregion
    }
}