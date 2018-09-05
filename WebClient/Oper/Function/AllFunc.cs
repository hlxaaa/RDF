using Common;
using Common.DBToClass;
using Common.Helper;
using Common.Result;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using static DbOpertion.Models.Extend.AllModel;

namespace ServerEnd.Oper.Function
{
    public class AllFunc : SingleTon<AllFunc>
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

        //展示到网页上的，ip端口
        string imgHost = ConfigurationManager.AppSettings.Get("imgHost");

        string apiHost = ConfigurationManager.AppSettings.Get("apiHost");
        //操作文件时,D://……
        //string imgUrl = ConfigurationManager.AppSettings["imgUrl"];

        public ResultJson DeleteVersion(IdReq req)
        {
            AndroidVersionOper.Instance.Delete1((int)req.Id);
            return new ResultJson("删除成功");
        }

        public ResultJson<AndroidVersion> GetVersionById(IdReq req)
        {
            var r = new ResultJson<AndroidVersion>();
            var v = AndroidVersionOper.Instance.GetById((int)req.Id);
            r.ListData.Add(v);
            return r;
        }

        public ResultJson UpdateVersion(UpdateVersionReq req)
        {
            var v = AndroidVersionOper.Instance.GetById((int)req.id);
            v.versionCode = req.versionCode;
            v.versionName = req.versionName;
            v.updateLog = req.updateLog;
            AndroidVersionOper.Instance.Update(v);
            return new ResultJson("修改成功");
        }

        public ResultJson SetPwd(SetPwdReq req)
        {
            var user = Sys_UserOper.Instance.GetById((int)req.userId);
            if (MD5Helper.Instance.Md5_32(req.spwd) != user.UserPwd)
                throw new Exception("原密码不正确");
            if (req.newpwd != req.newpwd2)
                throw new Exception("新密码与确认密码不相同");
            user.UserPwd = MD5Helper.Instance.Md5_32(req.newpwd);
            Sys_UserOper.Instance.Update(user);
            return new ResultJson("修改成功，请重新登录");
        }

        public ResultJson<GetGymDataAllRes> GetGymDataAll(GetListCommonReq req)
        {
            var r = new ResultJson<GetGymDataAllRes>();
            var gid = (int)req.gymId;
            var ds = DeviceOper.Instance.GetByGymId(gid);
            var dIds = ds.Select(p => p.id).ToList();
            var list = CacheFunc.Instance.GetGymData();
            if (list.Count == 0)
                return r;
            list = list.Where(p => dIds.Contains(p.deviceId)).ToList();
            if (list.Count == 0)
                return r;
            var uids = list.Select(p => p.userId).ToList();
            foreach (var item in list)
            {
                r.ListData.Add(new GetGymDataAllRes(item));
            }

            switch (req.orderField)
            {
                case "KM":
                    if ((bool)req.isDesc)
                        r.ListData = r.ListData.OrderByDescending(p => p.KM).ToList();
                    else
                        r.ListData = r.ListData.OrderBy(p => p.KM).ToList();
                    break;
                case "TotalTime":
                    if ((bool)req.isDesc)
                        r.ListData = r.ListData.OrderByDescending(p => p.TotalTime).ToList();
                    else
                        r.ListData = r.ListData.OrderBy(p => p.TotalTime).ToList();
                    break;
                case "DRAG":
                    if ((bool)req.isDesc)
                        r.ListData = r.ListData.OrderByDescending(p => p.DRAG).ToList();
                    else
                        r.ListData = r.ListData.OrderBy(p => p.DRAG).ToList();
                    break;
                case "SD":
                    if ((bool)req.isDesc)
                        r.ListData = r.ListData.OrderByDescending(p => p.SD).ToList();
                    else
                        r.ListData = r.ListData.OrderBy(p => p.SD).ToList();
                    break;
                case "WATT":
                    if ((bool)req.isDesc)
                        r.ListData = r.ListData.OrderByDescending(p => p.WATT).ToList();
                    else
                        r.ListData = r.ListData.OrderBy(p => p.WATT).ToList();
                    break;
                case "CAL":
                    if ((bool)req.isDesc)
                        r.ListData = r.ListData.OrderByDescending(p => p.CAL).ToList();
                    else
                        r.ListData = r.ListData.OrderBy(p => p.CAL).ToList();
                    break;
                case "XL":
                    if ((bool)req.isDesc)
                        r.ListData = r.ListData.OrderByDescending(p => p.XL).ToList();
                    else
                        r.ListData = r.ListData.OrderBy(p => p.XL).ToList();
                    break;
                default:
                    break;
            }

            for (int i = 0; i < r.ListData.Count; i++)
            {
                r.ListData[i].order = i + 1;
            }

            while (r.ListData.Count() < dIds.Count())
            {
                r.ListData.Add(new GetGymDataAllRes());
            }
            return r;
        }

        public ResultJson<GetGymDataAllRes> GetGymDataPage(GetListCommonReq req)
        {
            var size = (int)req.size;
            var index = (int)req.pageIndex;

            var r = new ResultJson<GetGymDataAllRes>();
            var gid = (int)req.gymId;
            var ds = DeviceOper.Instance.GetByGymId(gid);
            var dIds = ds.Select(p => p.id).ToList();
            var list = CacheFunc.Instance.GetGymData();
            if (list.Count == 0)
                return r;
            list = list.Where(p => dIds.Contains(p.deviceId)).ToList();
            if (list.Count == 0)
                return r;
            var uids = list.Select(p => p.userId).ToList();
            foreach (var item in list)
            {
                r.ListData.Add(new GetGymDataAllRes(item));
            }

            switch (req.orderField)
            {
                case "KM":
                    if ((bool)req.isDesc)
                        r.ListData = r.ListData.OrderByDescending(p => p.KM).ToList();
                    else
                        r.ListData = r.ListData.OrderBy(p => p.KM).ToList();
                    break;
                case "TotalTime":
                    if ((bool)req.isDesc)
                        r.ListData = r.ListData.OrderByDescending(p => p.TotalTime).ToList();
                    else
                        r.ListData = r.ListData.OrderBy(p => p.TotalTime).ToList();
                    break;
                case "DRAG":
                    if ((bool)req.isDesc)
                        r.ListData = r.ListData.OrderByDescending(p => p.DRAG).ToList();
                    else
                        r.ListData = r.ListData.OrderBy(p => p.DRAG).ToList();
                    break;
                case "SD":
                    if ((bool)req.isDesc)
                        r.ListData = r.ListData.OrderByDescending(p => p.SD).ToList();
                    else
                        r.ListData = r.ListData.OrderBy(p => p.SD).ToList();
                    break;
                case "WATT":
                    if ((bool)req.isDesc)
                        r.ListData = r.ListData.OrderByDescending(p => p.WATT).ToList();
                    else
                        r.ListData = r.ListData.OrderBy(p => p.WATT).ToList();
                    break;
                case "CAL":
                    if ((bool)req.isDesc)
                        r.ListData = r.ListData.OrderByDescending(p => p.CAL).ToList();
                    else
                        r.ListData = r.ListData.OrderBy(p => p.CAL).ToList();
                    break;
                case "XL":
                    if ((bool)req.isDesc)
                        r.ListData = r.ListData.OrderByDescending(p => p.XL).ToList();
                    else
                        r.ListData = r.ListData.OrderBy(p => p.XL).ToList();
                    break;
                default:
                    break;
            }

            for (int i = 0; i < r.ListData.Count; i++)
            {
                r.ListData[i].order = i + 1;
            }

            r.ListData = r.ListData.Skip((index - 1) * size).Take(size).ToList();

            //while (r.ListData.Count() < dIds.Count())
            //{
            //    r.ListData.Add(new GetGymDataAllRes());
            //}
            return r;
        }

        /// <summary>
        /// 设置直播的点播
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson SetLiveVideo(SetLiveVideoReq req)
        {
            var m = new Sys_VideoInfo();
            m.Id = (int)req.liveId;
            m.VideoId = req.videoId;
            Sys_VideoInfoOper.Instance.Update(m);
            return new ResultJson("设置成功");
        }

        public ResultJson<ServerUserItem> GetServerUserById(IdReq req)
        {
            var r = new ResultJson<ServerUserItem>();
            var userId = (int)req.Id;
            var list = Sys_UserAuthOper.Instance.GetAuthByUserId(userId);
            var res = new List<ServerUserItem>();
            if (list.Count == 0)
            {
                r.ListData = res;
            }
            res.Add(new ServerUserItem(list));
            r.ListData = res;
            return r;
        }

        public ResultJson UpdateServerUser(UpdateServerUserReq req)
        {
            var phone = req.Phone;
            if (Sys_UserOper.Instance.IsPhoneExist(phone, (int)req.Id))
                throw new Exception("已存在这个手机号");

            if (Sys_UserOper.Instance.IsUIdExist(req.UId, (int)req.Id))
                throw new Exception("已存在这个账号");

            var r = new ResultJson("修改成功");
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"AddServerUser_Server");
            try
            {
                var user = Sys_UserOper.Instance.GetById((int)req.Id);
                user.UId = req.UId;
                if (req.UserPwd != null)
                    user.UserPwd = MD5Helper.Instance.Md5_32(req.UserPwd);
                user.UserName = req.UserName;
                user.Phone = req.Phone;
                if (req.Url.Substring(0, 4) == "data")
                {
                    user.Url = UpdateUserImg(req.Url, user.Url, user.Id);
                }
                Sys_UserOper.Instance.Update(user, conn, tran);
                AuthOper.Instance.DeleteByUserId(user.Id, conn, tran);
                if (req.menuIds != null)
                {
                    foreach (var item in req.menuIds)
                    {
                        var ua = new Auth();
                        ua.userId = user.Id;
                        ua.menuId = item;
                        AuthOper.Instance.Add(ua, conn, tran);
                    }
                }
                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                r.HttpCode = 500;
                r.Message = ex.Message;
            }
            return r;
        }

        public ResultJson AddServerUser(AddServerUserReq req)
        {
            var phone = req.Phone;
            if (Sys_UserOper.Instance.IsPhoneExist(phone, 0))
                throw new Exception("已存在这个手机号");

            if (Sys_UserOper.Instance.IsUIdExist(req.UId, 0))
                throw new Exception("已存在这个账号");


            var r = new ResultJson("添加成功");
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"AddServerUser_Server");
            try
            {
                var user = new Sys_User();
                user.Type = 0;
                user.UId = req.UId;
                user.UserPwd = MD5Helper.Instance.Md5_32(req.UserPwd);
                user.UserName = req.UserName;
                user.Height = 0;
                user.Sex = true;
                user.Weight = 0;
                user.Phone = req.Phone;
                user.Birthday = DateTime.Now;
                user.Enabled = false;
                user.RegisterTime = DateTime.Now;
                var id = Sys_UserOper.Instance.Add(user, conn, tran);
                user.Url = SaveUserHeadImg(req.Url, id);
                user.Id = id;
                Sys_UserOper.Instance.Update(user, conn, tran);
                if (req.menuIds != null)
                {
                    foreach (var item in req.menuIds)
                    {
                        var a = new Auth();
                        a.userId = id;
                        a.menuId = item;
                        AuthOper.Instance.Add(a, conn, tran);
                    }
                }
                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                r.HttpCode = 500;
                r.Message = ex.Message;
            }
            return r;
        }

        public ResultWeb<ServerUserReq> GetSeverUserList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<ServerUserReq>();
            var r = new ResultWeb<ServerUserReq>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.AuthViewCondition(req);
            //var condition = Sys_UserAuthOper.Instance.AuthViewCondition(req);
            var list2 = CommonOper.Instance.GetMVList<authView>(req, condition);
            //var list2 = Sys_UserAuthOper.Instance.GetAuthViewList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                var ids = list2.Select(p => p.Id).Distinct().ToList();
                foreach (var item in ids)
                {
                    var temp = list2.Where(p => p.Id == item).ToList();
                    var rr = new ServerUserReq(temp);
                    r.ListData.Add(rr);
                }
                //r.ListData = list2;
                var count = CommonOper.Instance.GetMVCount<authView>(req, condition);
                //var count = Sys_UserAuthOper.Instance.GetAuthViewCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
            //return new ResultWeb<Sys_User>();
        }

        /// <summary>
        /// 获取用户量、活跃人数（今天用过的就算活跃）、健身房数
        /// </summary>
        /// <returns></returns>
        public GetData3Res GetData3()
        {
            var r = new GetData3Res();
            r.gymCount = GymOper.Instance.GetAllGymCount();
            //var rr = HttpHelper.Instance.HttpPost(apiHost + "api/user/GetOnlineUserCount", "");
            var list = CacheFunc.Instance.GetActiveUser();
            r.userOnLineCount = Convert.ToInt32(list.Count);
            r.userCount = Sys_UserOper.Instance.GetAllUserCount();
            return r;
        }

        /// <summary>
        /// 新增安卓版本
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson AddNewVersion(AddNewVersionReq req)
        {
            var file = req.file;
            var v = new AndroidVersion();
            v.versionName = req.versionName;
            v.versionCode = req.versionCode;
            v.updateLog = req.updateLog;

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"AddNewVersion_server");
            try
            {
                v.targetSize = StringHelper.Instance.Keep2Decimal(file.ContentLength / 1000000m) + "M";
                v.id = AndroidVersionOper.Instance.Add(v, conn, tran);
                v.apkFileUrl = SaveAndroidApk(file, v.id);
                v.editTime = DateTime.Now;
                AndroidVersionOper.Instance.Update(v, conn, tran);

                tran.Commit();
                conn.Close();
                return new ResultJson("上传成功");
            }
            catch (Exception ex)
            {
                //DeleteUrl(url);
                tran.Rollback();
                conn.Close();
                throw new Exception(ex.Message);
            }



        }

        /// <summary>
        /// 获取安卓版本列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultWeb<GetVersionListRes> GetVersionList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetVersionListRes>();
            var r = new ResultWeb<GetVersionListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.AndroidVersionCondition(req);
            //var condition = AndroidVersionOper.Instance.Condition(req);
            var list2 = CommonOper.Instance.GetVPList<AndroidVersion>(req, condition);
            //var list2 = AndroidVersionOper.Instance.GetAudioTypeList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetVersionListRes(item);
                    r.ListData.Add(temp);
                }
                //r.ListData = list2;
                var count = CommonOper.Instance.GetVPCount<AndroidVersion>(req, condition);
                //var count = AndroidVersionOper.Instance.GetAudioTypeCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取用户\设备的数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<GetUserDataRes> GetUserData(GetUserDataReq req)
        {
            var r = new ResultJson<GetUserDataRes>();
            var res = new List<GetUserDataRes>();
            var date = Convert.ToDateTime(req.date);
            if (req.userId == null)
                throw new Exception("未选择用户");
            var userId = (int)req.userId;
            var list = Sys_DataOper.Instance.GetDataForLine(date, userId);
            if (list.Count == 0)
            {
                r.ListData = res;
                return r;
            }

            foreach (var item in list)
            {
                res.Add(new GetUserDataRes(item));
            }
            var temp = new List<GetUserDataRes>();
            var xs = res.Select(p => p.x).Distinct().ToList();
            foreach (var item in xs)
            {
                temp.Add(res.Where(p => p.x == item).OrderByDescending(p => p.km).First());
            }

            r.ListData = temp;
            return r;
        }

        /// <summary>
        /// 获取财务明细列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultWeb<GetMoneyDetailListRes> GetMoneyDetailList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetMoneyDetailListRes>();
            var r = new ResultWeb<GetMoneyDetailListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.MoneyDetailCondition(req);
            var list2 = CommonOper.Instance.GetVPList<PayView>(req, condition);
            //var list2 = PayRecordOper.Instance.GetMoneyDetailList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetMoneyDetailListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<PayView>(req, condition);
                //var count = PayRecordOper.Instance.GetMoneyDetailCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 新增健身房
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson AddGym(AddGymReq req)
        {

            if (Sys_UserOper.Instance.IsUIdExist(req.UserName, 0))
                throw new Exception("已存在这个账号");

            var g = new Gym();
            g.name = req.name;
            g.nameE = req.nameE;

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"AddGym_server");
            try
            {
                g.id = GymOper.Instance.Add(g, conn, tran);
                var user = new Sys_User();
                user.Type = 0;
                user.UId = req.UserName;
                user.UserPwd = MD5Helper.Instance.Md5_32(req.UserPwd);
                user.UserName = "";
                user.Height = 0;
                user.Sex = true;
                user.Weight = 0;
                string token = Guid.NewGuid().ToString();
                user.Phone = "temp" + DateTime.Now.ToString("yyyyMMmmss") + token;
                //user.Phone = req.Phone;
                user.Birthday = DateTime.Now;
                user.Enabled = false;
                user.RegisterTime = DateTime.Now;
                user.isEnglish = 1;
                var id = Sys_UserOper.Instance.Add(user, conn, tran);
                user.Id = id;
                g.gymUserId = id;
                GymOper.Instance.Update(g, conn, tran);

                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                //DeleteUrl(url);
                tran.Rollback();
                conn.Close();
                throw new Exception(ex.Message);
            }



            return new ResultJson("添加成功");
        }

        /// <summary>
        /// 更新健身房
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson UpdateGym(UpdateGymReq req)
        {


            var g = GymOper.Instance.GetById((int)req.id);

            if (Sys_UserOper.Instance.IsUIdExist(req.UserName, (int)g.gymUserId))
                throw new Exception("已存在这个账号");

            g.id = (int)req.id;
            g.name = req.name;
            g.nameE = req.nameE;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"UpdateGym_server");
            try
            {
                GymOper.Instance.Update(g, conn, tran);
                var user = new Sys_User();
                user.Id = (int)g.gymUserId;
                user.UId = req.UserName;
                if (req.UserPwd != null)
                    user.UserPwd = MD5Helper.Instance.Md5_32(req.UserPwd);
                Sys_UserOper.Instance.Update(user, conn, tran);

                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                //DeleteUrl(url);
                tran.Rollback();
                conn.Close();
                throw new Exception(ex.Message);
            }


            return new ResultJson("更新成功");
        }

        /// <summary>
        /// 获取单个健身房
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<GymView> GetGymById(IdReq req)
        {
            var r = new ResultJson<GymView>();
            var id = (int)req.Id;
            var g = GymOper.Instance.GetViewById(id);
            r.ListData.Add(g);
            return r;
        }

        /// <summary>
        /// 新增设备
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson AddDevice(AddDeviceReq req)
        {

            if (DeviceOper.Instance.IsNameExist(req.name, 0))
                throw new Exception("设备名称已存在");

            var d = new Device();
            d.name = req.name;
            d.gymId = req.gymId;
            DeviceOper.Instance.Add(d);
            return new ResultJson("添加成功");
        }

        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson UpdateDevice(UpdateDeviceReq req)
        {
            if (DeviceOper.Instance.IsNameExist(req.name, (int)req.id))
                throw new Exception("设备名称已存在");

            var d = new Device();
            d.id = (int)req.id;
            d.name = req.name;
            d.gymId = req.gymId;
            DeviceOper.Instance.Update(d);
            return new ResultJson("更新成功");
        }

        /// <summary>
        /// 获取单个设备
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<Device> GetDeviceById(IdReq req)
        {
            var r = new ResultJson<Device>();
            var id = (int)req.Id;
            var d = DeviceOper.Instance.GetById(id);
            r.ListData.Add(d);
            return r;
        }

        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultWeb<DeviceView> GetDeviceList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<DeviceView>();
            var r = new ResultWeb<DeviceView>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.DeviceCondition(req);
            //var condition = DeviceOper.Instance.DeviceCondition(req);
            var list2 = CommonOper.Instance.GetVPList<DeviceView>(req, condition);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                r.ListData = list2;
                var count = CommonOper.Instance.GetVPCount<DeviceView>(req, condition);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取用户\设备数据列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultWeb<DeviceDataRes> GetDeviceDataList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<DeviceDataRes>();
            var r = new ResultWeb<DeviceDataRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.DeviceDataCondition(req);
            //var condition = DeviceOper.Instance.DeviceDataCondition(req);
            var list2 = CommonOper.Instance.GetVPList<DeviceGym>(req, condition);
            //var list2 = DeviceOper.Instance.GetDeviceDataList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {

                for (int i = 0; i < list2.Count; i++)
                {
                    var temp = new DeviceDataRes(list2[i]);
                    temp.orderNo = ((int)req.pageIndex - 1) * size + i + 1;
                    r.ListData.Add(temp);
                }

                //foreach (var item in list2)
                //{
                //    var temp = new DeviceDataRes(item);
                //    r.ListData.Add(temp);
                //}
                //r.ListData = list2;
                var count = CommonOper.Instance.GetVPCount<DeviceGym>(req, condition);
                //var count = DeviceOper.Instance.GetDeviceDataCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取健身房列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultWeb<GetGymListRes> GetGymList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetGymListRes>();
            var r = new ResultWeb<GetGymListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.GymCondition(req);
            //var condition = GymOper.Instance.GymCondition(req);
            var list2 = CommonOper.Instance.GetVPList<GymView>(req, condition);
            //var list2 = GymOper.Instance.GetGymList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetGymListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<GymView>(req, condition);
                //var count = GymOper.Instance.GetGymCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 确认提现
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson PassTakeCash(IdReq req)
        {
            var id = (int)req.Id;
            var pr = new PayRecord();
            pr.id = id;
            pr.status = 1;
            PayRecordOper.Instance.Update(pr);
            return new ResultJson("确认提现成功");
        }

        /// <summary>
        /// 申请提现的列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultWeb<TakeCashListRes> TakeCashList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<TakeCashListRes>();
            var r = new ResultWeb<TakeCashListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.TakeCashCondition(req);
            var list2 = CommonOper.Instance.GetVPList<PayView>(req, condition);
            //var list2 = PayRecordOper.Instance.GetTakeCashList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new TakeCashListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<PayView>(req, condition);
                //var count = PayRecordOper.Instance.GetTakeCashCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 回复意见反馈
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson Reply(ReplyReq req)
        {
            var fb = new Sys_FeedBack();
            fb.Id = (int)req.Id;
            fb.reply = req.reply;
            Sys_FeedBackOper.Instance.Update(fb);
            return new ResultJson("回复成功");
        }

        /// <summary>
        /// 获取意见反馈列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultWeb<GetFeedBackListRes> GetFeedBackList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetFeedBackListRes>();
            var r = new ResultWeb<GetFeedBackListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.FeedBackCondition(req);
            //var condition = Sys_FeedBackOper.Instance.Condition(req);
            var list2 = CommonOper.Instance.GetVPList<FeedBackView>(req, condition);
            //var list2 = Sys_FeedBackOper.Instance.GetAudioTypeList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetFeedBackListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<FeedBackView>(req, condition);
                //var count = Sys_FeedBackOper.Instance.GetAudioTypeCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取运动干货留言列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultWeb<GetSportMsgListRes> GetSportMsgList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetSportMsgListRes>();
            var r = new ResultWeb<GetSportMsgListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.SportMsgCondition(req);
            var list2 = CommonOper.Instance.GetVPList<SportMsgView>(req, condition);
            //var list2 = SportMsgOper.Instance.GetAudioTypeList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetSportMsgListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<SportMsgView>(req, condition);
                //var count = SportMsgOper.Instance.GetAudioTypeCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取注册协议的html
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public ResultJson<StrRes> GetAgreement(int isEnglish)
        {
            var ss = new RegistrationAgreement();
            if (isEnglish == 1)
                ss = RegistrationAgreementOper.Instance.GetById(2);
            else
                ss = RegistrationAgreementOper.Instance.GetById(1);
            var r = new ResultJson<StrRes>();
            r.ListData.Add(new StrRes { str = ss.content });
            return r;
        }

        /// <summary>
        /// 获取关于我们的html
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public ResultJson<StrRes> GetAbout(int isEnglish)
        {
            var ss = new Sys_About();
            if (isEnglish == 1)
                ss = Sys_AboutOper.Instance.GetById(7);
            else
                ss = Sys_AboutOper.Instance.GetById(6);
            var r = new ResultJson<StrRes>();
            r.ListData.Add(new StrRes { str = ss.Content });
            return r;
        }

        /// <summary>
        /// 获取单个直播
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<Sys_VideoInfo> GetLiveById(IdReq req)
        {
            var r = new ResultJson<Sys_VideoInfo>();
            var id = (int)req.Id;
            var live = Sys_VideoInfoOper.Instance.GetById(id);
            live.Url = StringHelper.Instance.GetWebUrl(live.Url);
            r.ListData.Add(live);
            return r;
        }

        /// <summary>
        /// 更新直播
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson UpdateLive(UpdateLiveReq req)
        {
            var id = (int)req.Id;
            var live = Sys_VideoInfoOper.Instance.GetById(id);
            //var pv = new Sys_PlayVideo();
            live.Title = req.Title;
            live.TitleE = req.TitleE;
            //pv.LongTime = req.LongTime;
            //pv.time = DateTime.Now;
            //pv.price = req.price ?? 0;
            if (req.Url.Substring(0, 4) == "data")
            {
                live.Url = UpdateLiveImg(req.Url, live.Url, live.Id);
            }
            Sys_VideoInfoOper.Instance.Update(live);
            return new ResultJson("更新成功");
        }

        /// <summary>
        /// 新增参数设置
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson AddParamSet(AddParamSetReq req)
        {
            var ps = new Sys_ParamSet();
            ps.key = req.key;
            ps.value = req.value;
            ps.remark = req.remark;
            ps.uuid = req.uuid;
            ps.type = req.type;
            Sys_ParamSetOper.Instance.Add2(ps);
            return new ResultJson("添加成功");
        }

        /// <summary>
        /// 更新参数设置
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson UpdateParamSet(UpdateParamSet req)
        {
            var ps = new Sys_ParamSet();
            ps.key = req.key;
            ps.value = req.value;
            ps.remark = req.remark;
            ps.uuid = req.uuid;
            ps.type = req.type;
            Sys_ParamSetOper.Instance.Update2(ps, req.oldKey);
            return new ResultJson("更新成功");
        }

        /// <summary>
        /// 获取单个参数设置
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<Sys_ParamSet> GetParamSetItem(TestReq req)
        {
            var r = new ResultJson<Sys_ParamSet>();
            var key = req.str;
            var ps = Sys_ParamSetOper.Instance.GetByKey(key);
            if (ps == null)
                throw new Exception("没有这个键");
            r.ListData.Add(ps);
            return r;
        }

        /// <summary>
        /// 获取参数设置列表
        /// </summary>
        /// <returns></returns>
        public ResultJson<GetParamSetRes> GetParamSet()
        {
            var r = new ResultJson<GetParamSetRes>();
            var list = Sys_ParamSetOper.Instance.GetAllList();
            foreach (var item in list)
            {
                r.ListData.Add(new GetParamSetRes(item));
            }
            return r;
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourceUrl"></param>
        /// <param name="targetUrl"></param>
        public void CopyFile(string sourceUrl, string targetUrl)
        {
            if (!File.Exists(targetUrl))
                File.Copy(sourceUrl, targetUrl);
        }

        /// <summary>
        /// 新增运动干货
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson AddSport(AddSportReq req)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"AddSport_server");
            var url = "";
            try
            {
                var imgNames = req.imgNames;
                foreach (var item in imgNames)
                {
                    var temp = HttpContext.Current.Server.MapPath("../UpLoadFile/Temp/" + item);
                    var target = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                    CopyFile(temp, target);
                }

                var imgNamesE = req.imgNamesE;
                foreach (var item in imgNamesE)
                {
                    var temp = HttpContext.Current.Server.MapPath("../UpLoadFile/Temp/" + item);
                    var target = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                    CopyFile(temp, target);
                }

                var ss = new Sys_Sport();
                ss.Title = req.Title;
                ss.TitleE = req.TitleE;
                url = SaveSportImg(req.TitleUrl);
                ss.TitleUrl = url;
                ss.TypeId = req.TypeId;
                ss.CreateTime = DateTime.Now;
                ss.Content = StringHelper.Instance.GetContentHtml(req.Content);
                ss.ContentE = StringHelper.Instance.GetContentHtml(req.ContentE);
                ss.Remark = req.Remark;
                ss.RemarkE = req.RemarkE;
                ss.Enabled = false;
                ss.DataType = 0;
                Sys_SportOper.Instance.Add(ss, conn, tran);
                tran.Commit();
                conn.Close();
                return new ResultJson("添加成功");
            }
            catch (Exception ex)
            {
                DeleteUrl(url);
                tran.Rollback();
                conn.Close();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 新增运动干货（只加中文版）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson AddSport2(AddSportReq req)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"AddSport2_server");
            var url = "";
            try
            {
                if (req.imgNames != null)
                {
                    var imgNames = req.imgNames;
                    foreach (var item in imgNames)
                    {
                        var temp = HttpContext.Current.Server.MapPath("../UpLoadFile/Temp/" + item);
                        var target = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                        CopyFile(temp, target);
                    }
                }

                var ss = new Sys_Sport();
                ss.Title = req.Title;
                ss.TitleE = req.TitleE;
                url = SaveSportImg(req.TitleUrl);
                ss.TitleUrl = url;
                ss.TypeId = req.TypeId;
                ss.CreateTime = DateTime.Now;
                ss.Content = StringHelper.Instance.GetContentHtml(req.Content ?? "");
                //ss.ContentE = StringHelper.Instance.GetContentHtml(req.ContentE);
                ss.Remark = req.Remark;
                ss.RemarkE = req.RemarkE;
                ss.Enabled = false;
                ss.DataType = 0;
                Sys_SportOper.Instance.Add(ss, conn, tran);
                tran.Commit();
                conn.Close();
                return new ResultJson("添加成功");
            }
            catch (Exception ex)
            {
                DeleteUrl(url);
                tran.Rollback();
                conn.Close();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 更新注册协议
        /// </summary>
        /// <param name="req"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public ResultJson UpdateAgreement(UpdateAgreementReq req, int isEnglish)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"UpdateAgreement_server");
            try
            {
                var imgNames = req.imgNames ?? new string[] { };

                foreach (var item in imgNames)
                {
                    var temp = HttpContext.Current.Server.MapPath("../UpLoadFile/Temp/" + item);
                    var target = HttpContext.Current.Server.MapPath("../UpLoadFile/Agreement/" + item);
                    CopyFile(temp, target);
                }
                var ss = new RegistrationAgreement();
                if (isEnglish == 1)
                    ss = RegistrationAgreementOper.Instance.GetById(2);
                else
                    ss = RegistrationAgreementOper.Instance.GetById(1);
                var oldImgNames = GetOldImgNames(ss.content);
                var oldImgUrls = new List<string>();
                var newImgUrls = new List<string>();
                foreach (var item in oldImgNames)
                {
                    var path = HttpContext.Current.Server.MapPath("../UpLoadFile/Agreement/" + item);
                    oldImgUrls.Add(path);

                }
                foreach (var item in imgNames)
                {
                    var path = HttpContext.Current.Server.MapPath("../UpLoadFile/Agreement/" + item);
                    newImgUrls.Add(path);
                    //newImgUrls.Add(imgUrl + "/UpLoadFile/SportInfoImg/" + item);
                }
                UpdateImg(oldImgUrls.ToArray(), newImgUrls.ToArray());


                //ss.TitleUrl = StringHelper.Instance.UrlSetTimeStamp(ss.TitleUrl);
                ss.content = StringHelper.Instance.GetContentHtml(req.Content);
                RegistrationAgreementOper.Instance.Update(ss, conn, tran);
                //Sys_SportOper.Instance.Update(ss, conn, tran);

                tran.Commit();
                conn.Close();
                return new ResultJson("更新成功");
            }
            catch (Exception ex)
            {
                //DeleteUrl(url);
                tran.Rollback();
                conn.Close();
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 更新关于我们
        /// </summary>
        /// <param name="req"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public ResultJson UpdateAbout(UpdateAgreementReq req, int isEnglish)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"UpdateAbout_server");
            try
            {
                var imgNames = req.imgNames ?? new string[] { };

                foreach (var item in imgNames)
                {
                    var temp = HttpContext.Current.Server.MapPath("../UpLoadFile/Temp/" + item);
                    var target = HttpContext.Current.Server.MapPath("../UpLoadFile/Agreement/" + item);
                    CopyFile(temp, target);
                }
                var ss = new Sys_About();
                if (isEnglish == 1)
                    ss = Sys_AboutOper.Instance.GetById(7);
                else
                    ss = Sys_AboutOper.Instance.GetById(6);
                var oldImgNames = ss.Content == null ? new string[] { } : GetOldImgNames(ss.Content);
                var oldImgUrls = new List<string>();
                var newImgUrls = new List<string>();
                foreach (var item in oldImgNames)
                {
                    var path = HttpContext.Current.Server.MapPath("../UpLoadFile/Agreement/" + item);
                    oldImgUrls.Add(path);
                }
                foreach (var item in imgNames)
                {
                    var path = HttpContext.Current.Server.MapPath("../UpLoadFile/Agreement/" + item);
                    newImgUrls.Add(path);
                    //newImgUrls.Add(imgUrl + "/UpLoadFile/SportInfoImg/" + item);
                }
                UpdateImg(oldImgUrls.ToArray(), newImgUrls.ToArray());
                //ss.TitleUrl = StringHelper.Instance.UrlSetTimeStamp(ss.TitleUrl);
                ss.Content = StringHelper.Instance.GetContentHtml(req.Content);
                ss.EditTime = DateTime.Now;
                ss.EditorId = 1;
                Sys_AboutOper.Instance.Update(ss, conn, tran);
                //Sys_SportOper.Instance.Update(ss, conn, tran);

                tran.Commit();
                conn.Close();
                return new ResultJson("更新成功");
            }
            catch (Exception ex)
            {
                //DeleteUrl(url);
                tran.Rollback();
                conn.Close();
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 更新运动干货
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson UpdateSport(UpdateSportReq req)
        {
            var id = (int)req.id;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"UpdateSport_server");
            try
            {
                var imgNames = req.imgNames;
                foreach (var item in imgNames)
                {
                    var temp = HttpContext.Current.Server.MapPath("../UpLoadFile/Temp/" + item);
                    var target = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                    CopyFile(temp, target);
                }
                var imgNamesE = req.imgNamesE;
                foreach (var item in imgNamesE)
                {
                    var temp = HttpContext.Current.Server.MapPath("../UpLoadFile/Temp/" + item);
                    var target = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                    CopyFile(temp, target);
                }


                var ss = Sys_SportOper.Instance.GetById(id);
                var oldImgNames = GetOldImgNames(ss);
                var oldImgUrls = new List<string>();
                var newImgUrls = new List<string>();
                foreach (var item in oldImgNames)
                {
                    var path = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                    oldImgUrls.Add(path);

                }
                foreach (var item in imgNames)
                {
                    var path = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                    newImgUrls.Add(path);
                    //newImgUrls.Add(imgUrl + "/UpLoadFile/SportInfoImg/" + item);
                }
                UpdateImg(oldImgUrls.ToArray(), newImgUrls.ToArray());

                var oldImgNamesE = GetOldImgNamesE(ss);
                var oldImgUrlsE = new List<string>();
                var newImgUrlsE = new List<string>();
                foreach (var item in oldImgNamesE)
                {
                    var path = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                    oldImgUrlsE.Add(path);

                }
                foreach (var item in imgNamesE)
                {
                    var path = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                    newImgUrlsE.Add(path);
                    //newImgUrls.Add(imgUrl + "/UpLoadFile/SportInfoImg/" + item);
                }
                UpdateImg(oldImgUrlsE.ToArray(), newImgUrlsE.ToArray());

                if (req.TitleUrl.Substring(0, 4) == "data")
                {
                    ss.TitleUrl = UpdateSportImg(req.TitleUrl, ss.TitleUrl);
                }

                ss.Title = req.Title;
                ss.TitleE = req.TitleE;
                ss.Remark = req.Remark;
                ss.RemarkE = req.RemarkE;
                ss.TypeId = req.TypeId;
                //ss.TitleUrl = StringHelper.Instance.UrlSetTimeStamp(ss.TitleUrl);
                ss.Content = StringHelper.Instance.GetContentHtml(req.Content);
                ss.ContentE = StringHelper.Instance.GetContentHtml(req.ContentE);
                Sys_SportOper.Instance.Update(ss, conn, tran);

                tran.Commit();
                conn.Close();
                return new ResultJson("更新成功");
            }
            catch (Exception ex)
            {
                //DeleteUrl(url);
                tran.Rollback();
                conn.Close();
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 更新运动干货中文版
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson UpdateSport1(UpdateSportReq req)
        {
            var id = (int)req.id;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"UpdateSport1_server");
            try
            {
                var imgNames = new string[] { };
                if (req.imgNames != null)
                {
                    imgNames = req.imgNames;
                    foreach (var item in imgNames)
                    {
                        var temp = HttpContext.Current.Server.MapPath("../UpLoadFile/Temp/" + item);
                        var target = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                        CopyFile(temp, target);
                    }
                }

                var ss = Sys_SportOper.Instance.GetById(id);
                var oldImgNames = GetOldImgNames(ss);
                var oldImgUrls = new List<string>();
                var newImgUrls = new List<string>();
                foreach (var item in oldImgNames)
                {
                    var path = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                    oldImgUrls.Add(path);

                }
                foreach (var item in imgNames)
                {
                    var path = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                    newImgUrls.Add(path);
                    //newImgUrls.Add(imgUrl + "/UpLoadFile/SportInfoImg/" + item);
                }
                UpdateImg(oldImgUrls.ToArray(), newImgUrls.ToArray());

                if (req.TitleUrl.Substring(0, 4) == "data")
                {
                    ss.TitleUrl = UpdateSportImg(req.TitleUrl, ss.TitleUrl);
                }

                ss.Title = req.Title;
                ss.TitleE = req.TitleE;
                ss.Remark = req.Remark;
                ss.RemarkE = req.RemarkE;
                ss.TypeId = req.TypeId;
                //ss.TitleUrl = StringHelper.Instance.UrlSetTimeStamp(ss.TitleUrl);
                ss.Content = StringHelper.Instance.GetContentHtml(req.Content ?? "");
                //ss.ContentE = StringHelper.Instance.GetContentHtml(req.ContentE);
                Sys_SportOper.Instance.Update(ss, conn, tran);

                tran.Commit();
                conn.Close();
                return new ResultJson("更新成功");
            }
            catch (Exception ex)
            {
                //DeleteUrl(url);
                tran.Rollback();
                conn.Close();
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 更新运动干货英文版
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson UpdateSportE(UpdateSportReq req)
        {
            var id = (int)req.id;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"UpdateSportE_server");
            try
            {
                var imgNames = new string[] { };
                if (req.imgNames != null)
                {
                    imgNames = req.imgNames;
                    foreach (var item in imgNames)
                    {
                        var temp = HttpContext.Current.Server.MapPath("../UpLoadFile/Temp/" + item);
                        var target = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                        CopyFile(temp, target);
                    }
                }
                //var imgNamesE = req.imgNamesE;
                //foreach (var item in imgNamesE)
                //{
                //    var temp = HttpContext.Current.Server.MapPath("../UpLoadFile/Temp/" + item);
                //    var target = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                //    CopyFile(temp, target);
                //}

                var ss = Sys_SportOper.Instance.GetById(id);
                var oldImgNames = GetOldImgNamesE(ss);
                var oldImgUrls = new List<string>();
                var newImgUrls = new List<string>();
                foreach (var item in oldImgNames)
                {
                    var path = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                    oldImgUrls.Add(path);
                }
                foreach (var item in imgNames)
                {
                    var path = HttpContext.Current.Server.MapPath("../UpLoadFile/SportInfoImg/" + item);
                    newImgUrls.Add(path);
                    //newImgUrls.Add(imgUrl + "/UpLoadFile/SportInfoImg/" + item);
                }
                UpdateImg(oldImgUrls.ToArray(), newImgUrls.ToArray());

                if (req.TitleUrl.Substring(0, 4) == "data")
                {
                    ss.TitleUrl = UpdateSportImg(req.TitleUrl, ss.TitleUrl);
                }

                ss.Title = req.Title;
                ss.TitleE = req.TitleE;
                ss.Remark = req.Remark;
                ss.RemarkE = req.RemarkE;
                ss.TypeId = req.TypeId;
                //ss.TitleUrl = StringHelper.Instance.UrlSetTimeStamp(ss.TitleUrl);
                //ss.Content = StringHelper.Instance.GetContentHtml(req.Content);
                ss.ContentE = StringHelper.Instance.GetContentHtml(req.Content ?? "");
                Sys_SportOper.Instance.Update(ss, conn, tran);

                tran.Commit();
                conn.Close();
                return new ResultJson("更新成功");
            }
            catch (Exception ex)
            {
                //DeleteUrl(url);
                tran.Rollback();
                conn.Close();
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 获取更新前html中所有图片的名字
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public string[] GetOldImgNames(int id)
        {
            var ss = Sys_SportOper.Instance.GetById(id);
            string content = ss.Content.Replace("*gt;", ">").Replace("*lt;", "<").Replace("*amp", "&");
            string[] imgs = GetImgPath(content);
            for (int i = 0; i < imgs.Length; i++)
            {
                imgs[i] = imgs[i].Substring(imgs[i].LastIndexOf('/') + 1);
            }
            return imgs;
        }
        public string[] GetOldImgNames(Sys_Sport ss)
        {
            string content = ss.Content.Replace("*gt;", ">").Replace("*lt;", "<").Replace("*amp", "&");
            string[] imgs = GetImgPath(content);
            for (int i = 0; i < imgs.Length; i++)
            {
                imgs[i] = imgs[i].Substring(imgs[i].LastIndexOf('/') + 1);
            }
            return imgs;
        }
        public string[] GetOldImgNamesE(Sys_Sport ss)
        {
            if (ss.ContentE == null)
                return new string[] { };
            string content = ss.ContentE.Replace("*gt;", ">").Replace("*lt;", "<").Replace("*amp", "&");
            string[] imgs = GetImgPath(content);
            for (int i = 0; i < imgs.Length; i++)
            {
                imgs[i] = imgs[i].Substring(imgs[i].LastIndexOf('/') + 1);
            }
            return imgs;
        }

        public string[] GetOldImgNames(string content)
        {
            //var ss = Sys_SportOper.Instance.GetById(id);
            //string content = ss.Content.Replace("*gt;", ">").Replace("*lt;", "<").Replace("*amp", "&");
            string[] imgs = GetImgPath(content);
            for (int i = 0; i < imgs.Length; i++)
            {
                imgs[i] = imgs[i].Substring(imgs[i].LastIndexOf('/') + 1);
            }
            return imgs;
        }

        public string[] GetImgPath(string content)
        {
            List<string> list = new List<string>();
            string[] temp = content.Split('<');
            for (int i = 0; i < temp.Length; i++)
            {
                string result = Regex.Match(temp[i], "(?<=src=\").*?(?=\")").Value;
                if (result != "" && result != null)
                    list.Add(result);
            }
            return list.ToArray();
        }

        public void UpdateImg(string[] oldImgNames, string[] imgNames)
        {
            for (int i = 0; i < oldImgNames.Length; i++)
            {
                bool isExist = false;
                for (int j = 0; j < imgNames.Length; j++)
                {
                    if (oldImgNames[i] == imgNames[j])
                        isExist = true;
                }
                if (!isExist)
                    DeleteFile(oldImgNames[i]);
            }
        }

        public void DeleteFile(string url)
        {
            if (File.Exists(url))
                File.Delete(url);
        }

        public void DeleteUrl(string url)
        {
            var path = HttpContext.Current.Server.MapPath($"../{url}");
            //var path = imgUrl + url;
            if (File.Exists(path))
                File.Delete(path);
        }

        /// <summary>
        /// 获取单个运动干货
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<GetSportByIdRes> GetSportById(IdReq req)
        {
            var id = (int)req.Id;
            var r = new ResultJson<GetSportByIdRes>();
            var ss = Sys_SportOper.Instance.GetById(id);
            r.ListData.Add(new GetSportByIdRes(ss));
            return r;
        }

        public string UpImg(HttpPostedFileBase file)
        {
            Random r = new Random();
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");

            //string path = imgUrl + "/UpLoadFile/Temp/";  //存储图片的文件夹
            var path = HttpContext.Current.Server.MapPath($"../UpLoadFile/Temp/");
            string fname = file.FileName;
            string fileExtension = fname.Substring(fname.LastIndexOf('.'), fname.Length - fname.LastIndexOf('.'));
            //string currentFileName = "exhibitionCoverImg" + time + file.ContentLength + r.Next(100, 999) + fileExtension;  //文件名中不要带中文，否则会出错
            string currentFileName = $"sportImg-{time}-{file.ContentLength}-{r.Next(100, 999)}{fileExtension}";

            //生成文件路径
            string imagePath = path + currentFileName;
            //保存文件
            file.SaveAs(imagePath);
            return "/UpLoadFile/Temp/" + currentFileName;
            //return "/UpLoadFile/Temp/" + currentFileName;
        }

        public string UpImgAgreement(HttpPostedFileBase file)
        {
            Random r = new Random();
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");

            //string path = imgUrl + "/UpLoadFile/Temp/";  //存储图片的文件夹
            var path = HttpContext.Current.Server.MapPath($"../UpLoadFile/Temp/");
            string fname = file.FileName;
            string fileExtension = fname.Substring(fname.LastIndexOf('.'), fname.Length - fname.LastIndexOf('.'));
            //string currentFileName = "exhibitionCoverImg" + time + file.ContentLength + r.Next(100, 999) + fileExtension;  //文件名中不要带中文，否则会出错
            string currentFileName = $"agreement-{time}-{file.ContentLength}-{r.Next(100, 999)}{fileExtension}";
            //生成文件路径
            string imagePath = path + currentFileName;
            //保存文件
            file.SaveAs(imagePath);
            return "/UpLoadFile/Temp/" + currentFileName;
            //return "/UpLoadFile/Temp/" + currentFileName;
        }

        /// <summary>
        /// 更新运动干货类型
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson UpdateSportType(UpdateSportTypeReq req)
        {
            var st = new Sys_SportType(req);
            Sys_SportTypeOper.Instance.Update(st);
            var r = new ResultJson("更新成功");
            return r;
        }

        /// <summary>
        /// 新增运动干货类型
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson AddSportType(AddSportTypeReq req)
        {
            var st = new Sys_SportType(req);
            st.isEnglish = req.isEnglish;
            Sys_SportTypeOper.Instance.Add(st);
            var r = new ResultJson("添加成功");
            return r;
        }


        /// <summary>
        /// 教练通过审核
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson PassCoach(IdReq req)
        {
            var id = (int)req.Id;
            var user = new Sys_User();
            user.Id = id;
            user.Type = 1;
            user.isPass = "1";
            Sys_UserOper.Instance.Update(user);
            return new ResultJson("通过成功");
        }

        /// <summary>
        /// 直播通过审核
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson PassLive(IdReq req)
        {
            var id = (int)req.Id;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"PassLive{id}");
            VCloud v = new VCloud();
            V_Create_Msg v_msg = null;
            try
            {
                var live = new Sys_VideoInfo();
                live.Id = id;
                live.isPass = "1";
                live.DataStatus = 1;

                v_msg = v.Create(id.ToString());
                if (v_msg.code != 200)
                    //return new RdfMsg(false, v_msg.msg);
                    throw new Exception(v_msg.msg);
                live.CloudId = v_msg.ret.cid;
                Sys_VideoInfoOper.Instance.Update(live, conn, tran);
                tran.Commit();
                conn.Close();
                return new ResultJson("通过成功");
            }
            catch (Exception ex)
            {
                //DeleteUrl(url);
                v.Delete(v_msg.ret.cid);
                tran.Rollback();
                conn.Close();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 视频通过审核
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson PassVideo(IdReq req)
        {
            var id = (int)req.Id;

            var live = new Sys_PlayVideo();
            live.Id = id;
            live.isPass = "1";
            Sys_PlayVideoOper.Instance.Update(live);
            return new ResultJson("通过成功");
        }

        /// <summary>
        /// 教练申请驳回
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson NotPassCoach(NotPassReq req)
        {
            var id = (int)req.Id;
            var user = new Sys_User();
            user.Id = id;
            user.isPass = req.isPass;
            Sys_UserOper.Instance.Update(user);
            return new ResultJson("驳回成功");
        }

        /// <summary>
        /// 直播驳回
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson NotPassLive(NotPassReq req)
        {
            var id = (int)req.Id;
            var live = new Sys_VideoInfo();
            live.Id = id;
            live.isPass = req.isPass;
            Sys_VideoInfoOper.Instance.Update(live);
            return new ResultJson("驳回成功");
        }

        /// <summary>
        /// 视频驳回
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson NotPassVideo(NotPassReq req)
        {
            var id = (int)req.Id;
            var live = new Sys_PlayVideo();
            live.Id = id;
            live.isPass = req.isPass;
            Sys_PlayVideoOper.Instance.Update(live);
            return new ResultJson("驳回成功");
        }

        /// <summary>
        /// 添加直播
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson AddLive(AddLiveReq req)
        {
            var r = new ResultJson("添加成功");
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            var url = "";
            SqlTransaction tran = conn.BeginTransaction($"AddLive_server");
            VCloud v = new VCloud();
            V_Create_Msg v_msg = null;
            try
            {
                var userId = (int)req.userId;
                var live = new Sys_VideoInfo();
                live.UserId = userId;
                //req.Url;
                live.Title = req.Title;
                live.BeginTime = Convert.ToDateTime(req.BeginTime).AddMinutes(-15);
                live.PlayLongTime = req.PlayLongTime + 30;
                live.PlayStatus = 0;
                live.DataStatus = 1;
                live.price = req.price;
                live.CloudId = "";
                live.isPass = "1";
                live.isEnglish = req.isEnglish == 1 ? 1 : 0;
                //-txy  直播的初始状态。。弄清楚再说
                live.Url = SaveLiveImg(req.Url, userId);
                url = live.Url;
                var id = Sys_VideoInfoOper.Instance.Add(live, conn, tran);

                v_msg = v.Create(id.ToString());
                if (v_msg.code != 200)
                    //return new RdfMsg(false, v_msg.msg);
                    throw new Exception(v_msg.msg);
                live.CloudId = v_msg.ret.cid;
                live.Id = id;
                Sys_VideoInfoOper.Instance.Update(live, conn, tran);
                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                var path = HttpContext.Current.Server.MapPath($"../{url}");
                //var path = imgUrl + url;
                if (File.Exists(path))
                    File.Delete(path);
                r.HttpCode = 500;
                r.Message = ex.Message;
            }
            return r;
        }

        /// <summary>
        /// 获取单个音乐专辑
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<GetAudioTypeByIdRes> GetAudioTypeById(IdReq req)
        {
            var id = (int)req.Id;
            var pv = Sys_AudioTypeOper.Instance.GetById(id);
            var r = new ResultJson<GetAudioTypeByIdRes>();
            r.ListData.Add(new GetAudioTypeByIdRes(pv));
            return r;
        }

        /// <summary>
        /// 获取单个运动干货分类
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<Sys_SportType> GetSportTypeById(IdReq req)
        {
            var id = (int)req.Id;
            var pv = Sys_SportTypeOper.Instance.GetById(id);
            var r = new ResultJson<Sys_SportType>();
            r.ListData.Add(pv);
            return r;
        }

        /// <summary>
        /// 更新音乐专辑
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson UpdateAudioType(UpdateAudioTypeReq req)
        {
            var at = Sys_AudioTypeOper.Instance.GetById((int)req.Id);
            at.Title = req.Title;
            at.TitleE = req.TitleE;
            at.Remark = req.Remark;
            at.RemarkE = req.RemarkE;
            if (req.TitleUrl.Substring(0, 4) == "data")
            {
                at.TitleUrl = UpdateAudioTypeImg(req.TitleUrl, at.TitleUrl);
            }
            Sys_AudioTypeOper.Instance.Update(at);
            return new ResultJson("更新成功");
        }

        /// <summary>
        /// 新增音乐专辑
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson AddAudioType(AddAudioTypeReq req)
        {
            var at = new Sys_AudioType();
            at.Title = req.Title;
            at.TitleE = req.TitleE;
            //at.TitleUrl = req.TitleUrl;
            at.Remark = req.Remark;
            at.RemarkE = req.RemarkE;
            at.Enabled = false;
            at.TitleUrl = SaveAudioTypeImg(req.TitleUrl);
            at.isEnglish = req.isEnglish;
            Sys_AudioTypeOper.Instance.Add(at);
            return new ResultJson("添加成功");
        }

        /// <summary>
        /// 新增视频
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson AddVideo(AddVideoReq req)
        {
            var r = new ResultJson("添加成功");
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"AddVideo" + DateTime.Now);
            try
            {
                var pv = new Sys_PlayVideo();
                pv.Title = req.Title;
                pv.TitleE = req.TitleE;
                pv.Url = "http://vod.pooboofit.com/" + req.Url;
                pv.LongTime = req.LongTime;
                pv.EditTime = DateTime.Now;
                pv.PlayCount = 0;
                pv.Enabled = false;
                pv.userId = 0;
                pv.isPass = "1";
                pv.price = req.price ?? 0;
                pv.VieldId = req.videoId;
                pv.TitleUrl = SaveServerVideoImg(req.TitleUrl);
                Sys_PlayVideoOper.Instance.Add(pv, conn, tran);
                tran.Commit();
                conn.Close();
                return r;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                r.HttpCode = 500;
                r.Message = ex.Message;
                return r;
            }
        }

        /// <summary>
        /// 新增音频
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson AddAudio(AddAudioReq req)
        {
            var r = new ResultJson("添加成功");

            var pa = new Sys_PlayAudio();
            pa.Title = req.Title;
            pa.Url = "http://vod.pooboofit.com/" + req.Url;
            pa.LongTime = req.LongTime;
            pa.EditTime = DateTime.Now;
            pa.PlayCount = 0;
            pa.Enabled = false;
            pa.TypeId = req.typeId;
            Sys_PlayAudioOper.Instance.Add(pa);

            return r;

        }

        /// <summary>
        /// 更新视频
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson UpdateVideo(UpdateVideoReq req)
        {
            var id = (int)req.Id;
            var pv = Sys_PlayVideoOper.Instance.GetById(id);
            //var pv = new Sys_PlayVideo();
            pv.Title = req.Title;
            pv.TitleE = req.TitleE;
            pv.LongTime = req.LongTime;
            pv.EditTime = DateTime.Now;
            pv.price = req.price ?? 0;
            if (req.TitleUrl.Substring(0, 4) == "data")
            {
                pv.TitleUrl = UpdateVideoImg(req.TitleUrl, pv.TitleUrl, (int)pv.userId);
            }
            Sys_PlayVideoOper.Instance.Update(pv);
            return new ResultJson("更新成功");
        }

        /// <summary>
        /// 更新音频
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson UpdateAudio(UpdateAudioReq req)
        {
            //var id = (int)req.Id;
            //var pv = Sys_PlayAudioOper.Instance.GetById(id);
            ////var pv = new Sys_PlayVideo();
            var pa = new Sys_PlayAudio();
            pa.Id = (int)req.Id;
            pa.Title = req.Title;
            pa.LongTime = req.LongTime;
            pa.EditTime = DateTime.Now;
            pa.TypeId = req.typeId;

            Sys_PlayAudioOper.Instance.Update(pa);
            return new ResultJson("更新成功");
        }

        /// <summary>
        /// 获取视频列表
        /// </summary>
        public ResultWeb<GetVideoListRes> GetVideoList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetVideoListRes>();
            var r = new ResultWeb<GetVideoListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.PassVideoCondition(req);
            var list2 = CommonOper.Instance.GetVPList<videoView>(req, condition);
            //var list2 = Sys_PlayVideoOper.Instance.GetPassVideoList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetVideoListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<videoView>(req, condition);
                //var count = Sys_PlayVideoOper.Instance.GetPassVideoCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取运动干货列表
        /// </summary>
        public ResultWeb<GetSportListRes> GetSportList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetSportListRes>();
            var r = new ResultWeb<GetSportListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.SportViewCondition(req);
            var list2 = CommonOper.Instance.GetVPList<SportView>(req, condition);
            //var list2 = Sys_SportOper.Instance.GetSportViewList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                //r.ListData = list2;
                foreach (var item in list2)
                {
                    var temp = new GetSportListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<SportView>(req, condition);
                //var count = Sys_SportOper.Instance.GetSportViewCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取某个教练的视频列表
        /// </summary>
        public ResultWeb<GetVideoListRes> GetVideoListByCoachId(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetVideoListRes>();
            var r = new ResultWeb<GetVideoListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.VideoByCoachIdCondition(req);
            var list2 = CommonOper.Instance.GetVPList<videoView>(req, condition);
            //var list2 = Sys_PlayVideoOper.Instance.GetVideoListByCoachId(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetVideoListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<videoView>(req, condition);
                //var count = Sys_PlayVideoOper.Instance.GetVideoByCoachIdCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取待审核的视频列表
        /// </summary>
        public ResultWeb<GetVideoListRes> GetVideoWaitingList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetVideoListRes>();
            var r = new ResultWeb<GetVideoListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.VideoWaitingCondition(req);
            //var condition = Sys_PlayVideoOper.Instance.VideoWaitingCondition(req);
            var list2 = CommonOper.Instance.GetVPList<videoView>(req, condition);
            //var list2 = Sys_PlayVideoOper.Instance.GetVideoWaitingList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetVideoListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<videoView>(req, condition);
                //var count = Sys_PlayVideoOper.Instance.GetVideoWaitingCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取音频列表
        /// </summary>
        public ResultWeb<GetAudioListRes> GetAudioList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetAudioListRes>();
            var r = new ResultWeb<GetAudioListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.PlayAudioCondition(req);
            //var condition = Sys_PlayAudioOper.Instance.Condition(req);
            var list2 = CommonOper.Instance.GetVPList<AudioView>(req, condition);
            //var list2 = Sys_PlayAudioOper.Instance.GetAudioList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetAudioListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<AudioView>(req, condition);
                //var count = Sys_PlayAudioOper.Instance.GetAudioCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取直播列表
        /// </summary>
        public ResultWeb<GetLiveListRes> GetLiveList(GetListCommonReq req)
        {
            Sys_VideoInfoOper.Instance.UpdateLivePlayStatus();
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetLiveListRes>();
            var r = new ResultWeb<GetLiveListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.PassLiveCondition(req);
            //var condition = Sys_VideoInfoOper.Instance.PassLiveCondition(req);
            var list2 = CommonOper.Instance.GetVPList<LiveView>(req, condition);
            //var list2 = Sys_VideoInfoOper.Instance.GetPassLiveList(req);

            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                //list2 = Sys_VideoInfoOper.Instance.UpdateLivePlayStatus(list2);
                foreach (var item in list2)
                {
                    var temp = new GetLiveListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<LiveView>(req, condition);
                //var count = Sys_VideoInfoOper.Instance.GetPassLiveCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取待审核的直播列表
        /// </summary>
        public ResultWeb<GetLiveListRes> GetLiveWaitingList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetLiveListRes>();
            var r = new ResultWeb<GetLiveListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.LiveWaitingCondition(req);
            var list2 = CommonOper.Instance.GetVPList<LiveView>(req, condition);
            //var list2 = Sys_VideoInfoOper.Instance.GetLiveWaitingList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetLiveListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<LiveView>(req, condition);
                //var count = Sys_VideoInfoOper.Instance.GetLiveWaitingCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取某个教练的直播列表
        /// </summary>
        public ResultWeb<GetLiveListRes> GetLiveListById(GetListCommonReq req)
        {
            Sys_VideoInfoOper.Instance.UpdateLivePlayStatus();
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetLiveListRes>();
            var r = new ResultWeb<GetLiveListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.LiveByIdCondition(req);
            var list2 = CommonOper.Instance.GetVPList<LiveView>(req, condition);
            //var list2 = Sys_VideoInfoOper.Instance.GetLiveListByCoachId(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                //list2 = Sys_VideoInfoOper.Instance.UpdateLivePlayStatus(list2);
                foreach (var item in list2)
                {
                    var temp = new GetLiveListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<LiveView>(req, condition);
                //var count = Sys_VideoInfoOper.Instance.GetLiveCountById(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取教练列表
        /// </summary>
        public ResultWeb<GetCoachListRes> GetCoachList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetCoachListRes>();
            var r = new ResultWeb<GetCoachListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.PassCoachCondition(req);
            var list2 = CommonOper.Instance.GetVPList<Sys_User>(req, condition);
            //var list2 = Sys_UserOper.Instance.GetPassCoachList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetCoachListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<Sys_User>(req, condition);
                //var count = Sys_UserOper.Instance.GetPassCoachCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取待审核的教练列表
        /// </summary>
        public ResultWeb<GetCoachListRes> GetCoachWaitingList2(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetCoachListRes>();
            var r = new ResultWeb<GetCoachListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.CoachWaitingCondition(req);
            //var condition = Sys_UserOper.Instance.CoachWaitingCondition2(req);
            var list2 = CommonOper.Instance.GetVPList<Sys_User>(req, condition);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetCoachListRes(item);
                    r.ListData.Add(temp);
                }

                var count = CommonOper.Instance.GetVPCount<Sys_User>(req, condition);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }


        /// <summary>
        /// 获取音频专辑列表
        /// </summary>
        public ResultWeb<GetAudioTypeListRes> GetAudioTypeList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetAudioTypeListRes>();
            var r = new ResultWeb<GetAudioTypeListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.AudioTypeCondition(req);
            var list2 = CommonOper.Instance.GetVPList<Sys_AudioType>(req, condition);
            //var list2 = Sys_AudioTypeOper.Instance.GetAudioTypeList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetAudioTypeListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<Sys_AudioType>(req, condition);
                //var count = Sys_AudioTypeOper.Instance.GetAudioTypeCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 获取运动干货分类列表
        /// </summary>
        public ResultWeb<GetSportTypeListRes> GetSportTypeList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetSportTypeListRes>();
            var r = new ResultWeb<GetSportTypeListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.SportTypeCondition(req);
            var list2 = CommonOper.Instance.GetVPList<Sys_SportType>(req, condition);
            //var list2 = Sys_SportTypeOper.Instance.GetSportTypeList(req);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetSportTypeListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<Sys_SportType>(req, condition);
                //var count = Sys_SportTypeOper.Instance.GetSportTypeCount(req);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        /// <summary>
        /// 启用或禁用视频
        /// </summary>
        public ResultJson ToggleVideo(ToggleVideoReq req)
        {
            var id = (int)req.Id;
            if ((bool)req.Enabled)
            {
                var pvView = Sys_PlayVideoOper.Instance.GetViewById(id);

                if (pvView.userId != 0 && pvView.type != 1)
                    return new ResultJson("");
            }
            var pv = new Sys_PlayVideo();
            pv.Id = id;
            pv.Enabled = (bool)req.Enabled;
            Sys_PlayVideoOper.Instance.Update(pv);
            var str = "禁用成功";
            if ((bool)pv.Enabled)
                str = "启用成功";
            return new ResultJson(str);
        }

        /// <summary>
        /// 启用或禁用音频
        /// </summary>
        public ResultJson ToggleAudio(ToggleVideoReq req)
        {
            var pa = new Sys_PlayAudio();
            pa.Id = (int)req.Id;
            pa.Enabled = req.Enabled;
            Sys_PlayAudioOper.Instance.Update(pa);
            var str = "禁用成功";
            if ((bool)pa.Enabled)
                str = "启用成功";
            return new ResultJson(str);
        }


        public ResultJson ToggleServerUser(ToggleVideoReq req)
        {
            var pa = new Sys_User();
            var id = (int)req.Id;
            if (id == 1)
                return new ResultJson("");
            pa.Id = id;
            pa.Enabled = req.Enabled;
            Sys_UserOper.Instance.Update(pa);
            var str = "禁用成功";
            if ((bool)pa.Enabled)
                str = "启用成功";
            return new ResultJson(str);
        }

        /// <summary>
        /// 启用或禁用音频专辑
        /// </summary>
        public ResultJson ToggleAudioType(ToggleVideoReq req)
        {
            var at = new Sys_AudioType();
            at.Id = (int)req.Id;
            at.Enabled = (bool)req.Enabled;
            Sys_AudioTypeOper.Instance.Update(at);
            var str = "禁用成功";
            if ((bool)at.Enabled)
                str = "启用成功";
            return new ResultJson(str);
        }

        /// <summary>
        /// 启用或禁用干货分类
        /// </summary>
        public ResultJson ToggleSportType(ToggleVideoReq req)
        {
            var st = new Sys_SportType();
            st.Id = (int)req.Id;
            st.Enabled = (bool)req.Enabled;
            Sys_SportTypeOper.Instance.Update(st);
            var str = "禁用成功";
            if ((bool)st.Enabled)
                str = "启用成功";
            return new ResultJson(str);
        }

        /// <summary>
        /// 启用或禁用运动干货
        /// </summary>
        public ResultJson ToggleSport(ToggleVideoReq req)
        {
            var ss = new Sys_Sport();
            ss.Id = (int)req.Id;
            ss.Enabled = (bool)req.Enabled;
            Sys_SportOper.Instance.Update(ss);
            var str = "禁用成功";
            if ((bool)ss.Enabled)
                str = "启用成功";
            return new ResultJson(str);
        }

        /// <summary>
        /// 新增教练
        /// </summary>
        public ResultJson AddCoach(AddCoachReq req)
        {
            var phone = req.Phone;
            if (Sys_UserOper.Instance.IsPhoneExist(phone, 0))
                throw new Exception("已存在这个手机号");

            var r = new ResultJson("添加成功");
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"AddCoach_Server");
            try
            {
                var user = new Sys_User();
                user.Type = 1;
                //user.account = req.account;
                user.UserPwd = MD5Helper.Instance.Md5_32(req.UserPwd);
                user.UserName = req.UserName;
                user.Height = req.Height;
                user.Sex = req.Sex;
                user.Weight = req.Weight;
                user.Phone = req.Phone;
                user.Birthday = req.Birthday;
                user.Enabled = false;
                user.isPass = "1";
                user.RegisterTime = DateTime.Now;
                user.isEnglish = req.isEnglish == 1 ? 1 : 0;
                //user.livePrice = req.livePrice;
                var id = Sys_UserOper.Instance.Add(user, conn, tran);
                user.Url = SaveUserHeadImg(req.Url, id);
                user.Id = id;
                Sys_UserOper.Instance.Update(user, conn, tran);
                tran.Commit();
                conn.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                r.HttpCode = 500;
                r.Message = ex.Message;
            }
            return r;
        }

        /// <summary>
        /// 启用或禁用教练
        /// </summary>
        public ResultJson ToggleCoach(ToggleVideoReq req)
        {
            var user = new Sys_User();
            user.Id = (int)req.Id;
            user.Enabled = req.Enabled;
            Sys_UserOper.Instance.Update(user);
            var str = "禁用成功";
            if ((bool)user.Enabled)
                str = "启用成功";
            return new ResultJson(str);

        }

        /// <summary>
        /// 获取单个教练
        /// </summary>
        public ResultJson<GetCoachItemRes> GetCoachItem(IdReq req)
        {
            var r = new ResultJson<GetCoachItemRes>();
            var id = (int)req.Id;
            var coach = Sys_UserOper.Instance.GetById(id);
            r.ListData.Add(new GetCoachItemRes(coach));
            return r;
        }

        /// <summary>
        /// 获取单个教练
        /// </summary>
        public GetCoachItemRes GetCoachItem(int id)
        {
            var coach = Sys_UserOper.Instance.GetById(id);
            if (coach == null)
                return null;
            return new GetCoachItemRes(coach);
        }

        /// <summary>
        /// 更新教练
        /// </summary>
        public ResultJson UpdateCoach(UpdateCoachReq req)
        {
            var phone = req.Phone;
            if (Sys_UserOper.Instance.IsPhoneExist(phone, (int)req.Id))
                throw new Exception("已存在这个手机号");

            var r = new ResultJson("更新成功");
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"UpdateCoach_server");
            try
            {
                var id = (int)req.Id;
                var user = Sys_UserOper.Instance.GetById(id);
                //var user = new Sys_User();
                //user.Id = (int)req.Id;
                //user.account = req.account;
                if (req.UserPwd != null)
                    user.UserPwd = MD5Helper.Instance.Md5_32(req.UserPwd);
                user.UserName = req.UserName;
                user.Height = req.Height;
                user.Sex = req.Sex;
                user.Weight = req.Weight;
                user.Phone = req.Phone;
                user.Birthday = req.Birthday;
                //user.livePrice = req.livePrice;
                Sys_UserOper.Instance.Update(user, conn, tran);
                if (req.Url.Substring(0, 4) == "data")
                {
                    user.Url = UpdateUserImg(req.Url, RemoveTimestamp(user.Url), user.Id);
                    Sys_UserOper.Instance.Update(user, conn, tran);
                }
                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                r.HttpCode = 500;
                r.Message = ex.Message;
            }
            return r;
        }

        /// <summary>
        /// url后的时间戳去掉
        /// </summary>
        public string RemoveTimestamp(string url)
        {
            var i = url.LastIndexOf('?');
            if (i < 0)
                return url;
            return url.Substring(0, i);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        public ResultJson UpdateUser(UpdateUserReq req)
        {
            var phone = req.Phone;
            if (Sys_UserOper.Instance.IsPhoneExist(phone, (int)req.Id))
                throw new Exception("已存在这个手机号");

            var r = new ResultJson("更新成功");
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"UpdateUser_server");
            try
            {
                var id = (int)req.Id;
                var user = Sys_UserOper.Instance.GetById(id);
                if (req.UserPwd != null)
                    user.UserPwd = MD5Helper.Instance.Md5_32(req.UserPwd);
                user.UserName = req.UserName;
                user.Height = req.Height;
                user.Sex = req.Sex;
                user.Weight = req.Weight;
                user.Phone = req.Phone;
                user.Birthday = req.Birthday;
                //user.livePrice = req.livePrice;
                Sys_UserOper.Instance.Update(user, conn, tran);
                if (req.Url.Substring(0, 4) == "data")
                {
                    user.Url = UpdateUserImg(req.Url, user.Url, user.Id);
                    Sys_UserOper.Instance.Update(user, conn, tran);
                }

                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                r.HttpCode = 500;
                r.Message = ex.Message;
            }
            return r;
        }

        /// <summary>
        /// 获取单个视频
        /// </summary>
        public ResultJson<GetVideoByIdRes> GetVideoById(IdReq req)
        {
            var id = (int)req.Id;
            var pv = Sys_PlayVideoOper.Instance.GetById(id);
            var r = new ResultJson<GetVideoByIdRes>();
            r.ListData.Add(new GetVideoByIdRes(pv));
            return r;
        }

        /// <summary>
        /// 获取单个音频
        /// </summary>
        public ResultJson<GetAudioByIdRes> GetAudioById(IdReq req)
        {
            var id = (int)req.Id;
            var pv = Sys_PlayAudioOper.Instance.GetViewById(id);
            var r = new ResultJson<GetAudioByIdRes>();
            r.ListData.Add(new GetAudioByIdRes(pv));
            return r;
        }

        public string SaveServerVideoImg(string base64)
        {
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            string shortPath = $"UpLoadFile/PlayVideoImage/0-{name}-{RandHelper.Instance.Number(3)}.jpg";
            var path = HttpContext.Current.Server.MapPath($"../{shortPath}");
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

        public string SaveAudioTypeImg(string base64)
        {
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            string shortPath = $"UpLoadFile/AudioTypeImage/{name}-{RandHelper.Instance.Number(3)}.jpg";
            var path = HttpContext.Current.Server.MapPath($"../{shortPath}");
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

        public string SaveSportImg(string base64)
        {
            //string baseUrl = ConfigurationManager.AppSettings["imgUrl"];
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            //string saveFileName = DateTime.Now.ToFileTime().ToString();
            string shortPath = $"UpLoadFile/SportImage/{name}-{RandHelper.Instance.Number(3)}.jpg";
            //string path = baseUrl + shortPath;
            var path = HttpContext.Current.Server.MapPath($"../{shortPath}");
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

        public string SaveUserHeadImg(string base64, int userId)
        {
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            string shortPath = $"UpLoadFile/AppHeadImage/{userId}-{name}-{RandHelper.Instance.Number(3)}.jpg";
            var path = HttpContext.Current.Server.MapPath($"../{shortPath}");
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

        public string SaveLiveImg(string base64, int userId)
        {
            //string baseUrl = ConfigurationManager.AppSettings["imgUrl"];
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            //string saveFileName = DateTime.Now.ToFileTime().ToString();
            string shortPath = $"UpLoadFile/VideoTitleImage/{userId}-{name}-{RandHelper.Instance.Number(3)}.jpg";
            //string path = baseUrl + shortPath;
            var path = HttpContext.Current.Server.MapPath($"../{shortPath}");
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

        public string SaveAndroidApk(HttpPostedFileBase file, int id)
        {
            string shortPath = $"UpLoadFile/AndroidApk/fitnow_{id}.apk";
            //string path = baseUrl + shortPath;
            var path = HttpContext.Current.Server.MapPath($"../{shortPath}");
            file.SaveAs(path);
            return shortPath;
        }

        public string UpdateAudioTypeImg(string base64, string oldUrl)
        {
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            string shortPath = $"UpLoadFile/AudioTypeImage/{name}-{RandHelper.Instance.Number(3)}.jpg";
            var path = HttpContext.Current.Server.MapPath($"../{shortPath}");
            var delPath = HttpContext.Current.Server.MapPath($"../{oldUrl}");
            if (File.Exists(delPath) && oldUrl != "Images\\default.png")
                File.Delete(delPath);
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

        public string UpdateLiveImg(string base64, string oldUrl, int liveId)
        {
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            string shortPath = $"UpLoadFile/VideoTitleImage/{liveId}-{name}-{RandHelper.Instance.Number(3)}.jpg";
            var path = HttpContext.Current.Server.MapPath($"../{shortPath}");
            var delPath = HttpContext.Current.Server.MapPath($"../{oldUrl}");
            if (File.Exists(delPath) && oldUrl != "Images\\default.png")
                File.Delete(delPath);
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

        public string UpdateSportImg(string base64, string oldUrl)
        {
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            string shortPath = $"UpLoadFile/SportImage/{name}-{RandHelper.Instance.Number(3)}.jpg";
            var path = HttpContext.Current.Server.MapPath($"../{shortPath}");
            var delPath = HttpContext.Current.Server.MapPath($"../{oldUrl}");
            if (File.Exists(delPath) && oldUrl != "Images\\default.png")
                File.Delete(delPath);
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

        public string UpdateVideoImg(string base64, string oldUrl, int userId)
        {
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            string shortPath = $"UpLoadFile/PlayVideoImage/{userId}-{name}-{RandHelper.Instance.Number(3)}.jpg";
            var path = HttpContext.Current.Server.MapPath($"../{shortPath}");
            var delPath = HttpContext.Current.Server.MapPath($"../{oldUrl}");
            if (File.Exists(delPath) && oldUrl != "Images\\default.png")
                File.Delete(delPath);
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

        public string UpdateUserImg(string base64, string oldUrl, int userId)
        {
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            string shortPath = $"UpLoadFile/AppHeadImage/{userId}-{name}-{RandHelper.Instance.Number(3)}.jpg";
            var path = HttpContext.Current.Server.MapPath($"../{shortPath}");
            var delPath = HttpContext.Current.Server.MapPath($"../{oldUrl}");
            if (File.Exists(delPath) && oldUrl != "Images\\default.png")
                File.Delete(delPath);
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

        public void UpdateServerImg(string base64, string shortPath)
        {
            //string baseUrl = ConfigurationManager.AppSettings["imgUrl"];
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            //string path = imgUrl + shortPath;
            var path = HttpContext.Current.Server.MapPath($"../{shortPath}");
            if (File.Exists(path) && shortPath != "Images\\default.png")
                File.Delete(path);
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
        }

        public void ClearTempPic()
        {
            if (CacheFunc.Instance.IsNeedClear())
            {
                var path = HttpContext.Current.Server.MapPath("../uploadfile/Temp");
                FileHelper.Instance.ClearTempPic(path);
                CacheFunc.Instance.SetClearTempTime();
            }
        }
    }
}