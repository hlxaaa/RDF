using Common.Filter.Mvc;
using Common.Helper;
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

    public class UserAjaxController : AjaxBaseController
    {
        #region ServerUser
        public string GetServerUserById(IdReq req)
        {
            var r = AllFunc.Instance.GetServerUserById(req);
            return JsonConvert.SerializeObject(r);
        }

        public string AddServerUser(AddServerUserReq req)
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(4))
                throw new Exception("没有操作权限");

            var r = AllFunc.Instance.AddServerUser(req);
            return JsonConvert.SerializeObject(r);
        }

        public string UpdateServerUser(UpdateServerUserReq req)
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(4))
                throw new Exception("没有操作权限");
            var r = AllFunc.Instance.UpdateServerUser(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetSeverUserList(GetListCommonReq req)
        {
            var r = AllFunc.Instance.GetSeverUserList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string ToggleServerUser(ToggleVideoReq req)
        {
            var user = JsonConvert.DeserializeObject<ServerUserItem>(Session["user"].ToString());
            if (!user.menuIds.Contains(4))
                throw new Exception("没有操作权限");
            var r = AllFunc.Instance.ToggleServerUser(req);
            return JsonConvert.SerializeObject(r);
        }

        #endregion

        public string GetUserData(GetUserDataReq req)
        {
            var r = AllFunc.Instance.GetUserData(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetUserList(GetListCommonReq req)
        {
            var r = UserFunc.Instance.GetUserList(req);
            return JsonConvert.SerializeObject(r);
        }

        public string GetUserById(IdReq req)
        {
            var r = UserFunc.Instance.GetUserById(req);
            return JsonConvert.SerializeObject(r);
        }

        public string SaveBase64(SaveBase64Req req)
        {
            var r = new ResultJson();
            saveImg(req.img);
            return JsonConvert.SerializeObject(r);
        }

        private string saveImg(string base64)
        {
            string url = ConfigurationManager.AppSettings.Get("imgUrl");
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            string shortPath = $"UpLoadFile/AppHeadImage/999_Test.jpg";
            var path = url + shortPath;
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

    }
}