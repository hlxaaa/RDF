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
    public class UserController : ApiBaseController
    {
        [HttpPost]
        public int GetOnlineUserCount()
        {
            var r = 0;
            var users = Sys.OnlineUser._dic;
            if (users == null)
                return r;
            foreach (var item in users)
            {
                r += item.Value.Count;
            }
            return r;
        }

        [HttpPost]
        public ResultJson<ContactDeviceRes> ContactDevice(ContactDeviceReq req)
        {
            return AllFunc.Instance.ContactDevice(req);
        }

        [HttpPost]
        public ResultJson<apkRes> GetVersion()
        {
            return AllFunc.Instance.GetVersion();
        }

        [HttpPost]
        public ResultJson UpdatePhone(UpdatePhoneReq req)
        {
            return AllFunc.Instance.UpdatePhone(req);
        }

        [HttpPost]
        public ResultJson<GetRankListRes> GetRankList(GetRankListReq req)
        {
            return AllFunc.Instance.GetRankList(req);
        }

        public ResultJson SendMail(SendMailReq req)
        {
            return AllFunc.Instance.SendMail(req);
        }

        public ResultJson TestThrow()
        {
            var ex = new Exception("token失效");
            ex.Data.Add("code", 400);
            throw ex;
        }

        public ResultJson TestThrow2()
        {
            throw new Exception("bad");
        }

        [HttpPost]
        public ResultJson<UserLoginRes> GetUserInfo(GetUserInfoReq req)
        {
            return AllFunc.Instance.GetUserInfo(req);
        }

        public ResultJson LoginOut(LoginOutReq req)
        {
            return AllFunc.Instance.LoginOut(req);
        }

        public ResultJson<UserLoginRes> CheckUId(CheckUIdReq req)
        {
            return AllFunc.Instance.CheckUId(req);
        }

        //public ResultJson<UserLoginRes> Register(RegisterReq req)
        //{
        //    return AllFunc.Instance.Register(req);
        //}

        public ResultJson<UserLoginRes> RegisterPwd(RegisterPwdReq req)
        {
            return AllFunc.Instance.RegisterPwd(req);
        }


        public ResultJson<UserLoginRes> PwdLogin(PwdLoginReq req)
        {
            return AllFunc.Instance.PwdLogin(req);
        }

        public ResultJson<UserLoginRes> CodeLogin(CodeLoginReq req)
        {
            return AllFunc.Instance.CodeLogin(req);
        }

        public ResultJson UpdatePwd(UpdatePwdReq req)
        {
            return AllFunc.Instance.UpdatePwd(req);
        }
    }
}
