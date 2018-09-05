using Common;
using Common.Result;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static DbOpertion.Models.Extend.AllModel;

namespace ServerEnd.Oper.Function
{
    public class UserFunc : SingleTon<UserFunc>
    {
        public ResultWeb<GetUserListRes> GetUserList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetUserListRes>();
            var r = new ResultWeb<GetUserListRes>();
            var condition = ConditionOper.Instance.UserCondition(req);
            //var condition = Sys_UserOper.Instance.Condition(req);
            var list2 = CommonOper.Instance.GetVPList<UserDataView>(req, condition);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                var gymData = CacheFunc.Instance.GetGymData();
                foreach (var item in list2)
                {
                    var temp2 = gymData.Where(p => p.userId == item.Id).ToList();
                    if (temp2.Count > 0)
                    {
                        var did = temp2.First().deviceId;
                        var name = DeviceOper.Instance.GetGymNameByDeviceId(did);
                        item.frequency = name;
                    }
                    var temp = new GetUserListRes(item);
                    r.ListData.Add(temp);
                }

                var count = CommonOper.Instance.GetVPCount<UserDataView>(req, condition);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        public ResultJson<GetUserByIdRes> GetUserById(IdReq req)
        {
            var r = new ResultJson<GetUserByIdRes>();
            var id = (int)req.Id;
            var user = Sys_UserOper.Instance.GetById(id);
            if (user == null)
                throw new Exception("不存在此用户");
            r.ListData.Add(new GetUserByIdRes(user));
            return r;
        }
    }
}