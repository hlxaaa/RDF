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
    [MvcValidate]
    public class ContentAjaxController : AjaxBaseController
    {

        public string SetLiveVideo(SetLiveVideoReq req)
        {
            var r = AllFunc.Instance.SetLiveVideo(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetAuth(GetAddressReq req)
        {
            string live = System.Configuration.ConfigurationManager.AppSettings["AliParam"];
            string[] array = live.Split(',');
            IClientProfile clientProfile = DefaultProfile.GetProfile("cn-shanghai", array[0], array[1]);
            DefaultAcsClient client = new DefaultAcsClient(clientProfile);
            CreateUploadVideoRequest request = new CreateUploadVideoRequest();
            string name = req.fileName;
            int index = name.LastIndexOf('.');
            if (index > 0)
            {
                request.Title = name.Substring(0, index);
            }
            else
            {
                request.Title = name;
            }
            request.FileName = name;
            if (req.type != null)
            {
                request.FileName = request.Title + ".mp4";
            }
            //request.Description = "视频描述";
            //request.CoverURL = "http://cover.sample.com/sample.jpg";
            //request.Tags = "标签1,标签2";
            //request.CateId = 0;
            CreateUploadVideoResponse response = client.GetAcsResponse(request);
            var r = new ResultJson<CreateUploadVideoResponse>();
            r.ListData.Add(response);
            return JsonConvert.SerializeObject(r);
        }

        #region Video
        public string AddVideo(AddVideoReq req)
        {
            var r = AllFunc.Instance.AddVideo(req);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateVideo(UpdateVideoReq req)
        {
            var r = AllFunc.Instance.UpdateVideo(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetVideoList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetVideoList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string ToggleVideo(ToggleVideoReq req)
        {
            var r = AllFunc.Instance.ToggleVideo(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetVideoById(IdReq req)
        {
            var r = AllFunc.Instance.GetVideoById(req);
            return JsonConvert.SerializeObject(r);
        }
        #endregion


        #region Audio
        public string AddAudio(AddAudioReq req)
        {
            var r = AllFunc.Instance.AddAudio(req);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateAudio(UpdateAudioReq req)
        {
            var r = AllFunc.Instance.UpdateAudio(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetAudioList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetAudioList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string ToggleAudio(ToggleVideoReq req)
        {
            var r = AllFunc.Instance.ToggleAudio(req);
            return JsonConvert.SerializeObject(r);
        }

        public string ToggleAudioType(ToggleVideoReq req)
        {
            var r = AllFunc.Instance.ToggleAudioType(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetAudioById(IdReq req)
        {
            var r = AllFunc.Instance.GetAudioById(req);
            return JsonConvert.SerializeObject(r);
        }
        #endregion


        #region AudioType
        public string GetAudioTypeList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetAudioTypeList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string AddAudioType(AddAudioTypeReq req)
        {
            //var d = Server.MapPath("");
            var r = AllFunc.Instance.AddAudioType(req);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateAudioType(UpdateAudioTypeReq req)
        {
            var r = AllFunc.Instance.UpdateAudioType(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetAudioTypeById(IdReq req)
        {
            var r = AllFunc.Instance.GetAudioTypeById(req);
            return JsonConvert.SerializeObject(r);
        }
        #endregion


        public string GetLiveList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetLiveList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetLiveListById(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetLiveListById(req);
            return JsonConvert.SerializeObject(r);
        }


        /// <summary>
        /// 获取已过审的教练的列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string GetCoachList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetCoachList(req);
            return JsonConvert.SerializeObject(r);
        }



        public string AddCoach(AddCoachReq req)
        {
            var r = AllFunc.Instance.AddCoach(req);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateCoach(UpdateCoachReq req)
        {
            var r = AllFunc.Instance.UpdateCoach(req);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateUser(UpdateUserReq req)
        {
            var r = AllFunc.Instance.UpdateUser(req);
            return JsonConvert.SerializeObject(r);
        }


        public string ToggleCoach(ToggleVideoReq req)
        {
            var r = AllFunc.Instance.ToggleCoach(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetCoachItem(IdReq req)
        {
            var r = AllFunc.Instance.GetCoachItem(req);
            return JsonConvert.SerializeObject(r);
        }

        public string AddLive(AddLiveReq req)
        {
            var r = AllFunc.Instance.AddLive(req);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateLive(UpdateLiveReq req)
        {
            var r = AllFunc.Instance.UpdateLive(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetLiveById(IdReq req)
        {
            var r = AllFunc.Instance.GetLiveById(req);
            return JsonConvert.SerializeObject(r);
        }
    }
}