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
    public class GymAjaxController : AjaxBaseController
    {
        public string GetGymDataPage(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetGymDataPage(req);
            return JsonConvert.SerializeObject(r);
        }


        /// <summary>
        /// 健身房数据，列表，投影，都这个
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string GetGymDataAll(GetListCommonReq req)
        {
            var auth = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            req.gymId = auth.gymId;

            var r = AllFunc.Instance.GetGymDataAll(req);
            return JsonConvert.SerializeObject(r);
        }


        #region Gym
        public string GetGymList(GetListCommonReq req)
        {
            //var auth = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            //req.gymId = auth.gymId;
            var r = AllFunc.Instance.GetGymList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string AddGym(AddGymReq req)
        {
            var r = AllFunc.Instance.AddGym(req);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateGym(UpdateGymReq req)
        {
            var r = AllFunc.Instance.UpdateGym(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetGymById(IdReq req)
        {
            var r = AllFunc.Instance.GetGymById(req);
            return JsonConvert.SerializeObject(r);
        }
        #endregion

        public string GetDeviceList(GetListCommonReq req)
        {
            var auth = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            req.gymId = auth.gymId;

            var r = AllFunc.Instance.GetDeviceList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetDeviceListForServer(GetListCommonReq req)
        {
            //var auth = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            //req.gymId = auth.gymId;

            var r = AllFunc.Instance.GetDeviceList(req);
            return JsonConvert.SerializeObject(r);
        }

        /// <summary>
        /// 健身房数据，好像不用了
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string GetDeviceDataList(GetListCommonReq req)
        {
            var auth = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            req.gymId = auth.gymId;
            var r = AllFunc.Instance.GetDeviceDataList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetDeviceDataListForServer(GetListCommonReq req)
        {
            //var auth = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            //req.gymId = auth.gymId;
            var r = AllFunc.Instance.GetDeviceDataList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string AddDevice(AddDeviceReq req)
        {
            var auth = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            req.gymId = auth.gymId;
            var r = AllFunc.Instance.AddDevice(req);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateDevice(UpdateDeviceReq req)
        {
            var auth = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            req.gymId = auth.gymId;
            var r = AllFunc.Instance.UpdateDevice(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetDeviceById(IdReq req)
        {
            //var auth = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            //req.gymId = auth.gymId;
            var r = AllFunc.Instance.GetDeviceById(req);
            return JsonConvert.SerializeObject(r);
        }
    }
}