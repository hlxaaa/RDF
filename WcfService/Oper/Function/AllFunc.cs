using Common;
using Common.Result;
using Common.Extend;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
//using HHTDCDMR.Models.Extend.Req;
//using HHTDCDMR.Models.Extend.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
//using AliyunHelper.SendMail;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Web;
using DbOpertion.Models.Extend;
using AliyunHelper.SendMail;
using Tools;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Core;
using Aliyun.Acs.vod.Model.V20170321;
using Aliyun.Acs.Core.Exceptions;
using System.Text.RegularExpressions;

//using HHTDCDMR4._5.Oper.Function;

namespace WcfService.Oper.Function
{
    public class AllFunc : SingleTon<AllFunc>
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

        /// <summary>
        /// 第三方支付回调的地址，现在是自己服务器，之后要变成客户那边服务器的
        /// </summary>
        string apiHost = ConfigurationManager.AppSettings["apiHost"];

        /// <summary>
        /// app传图片存到服务端。比如D://……
        /// </summary>
        string baseUrl = ConfigurationManager.AppSettings["HeadUrl"];

        /// <summary>
        /// ip+端口
        /// </summary>
        string imgHost = ConfigurationManager.AppSettings["imgHost"];

        /// <summary>
        /// 阿里云点播操作
        /// </summary>
        public static string live = ConfigurationManager.AppSettings["AliParam"];
        static string[] array = live.Split(',');
        static IClientProfile clientProfile = DefaultProfile.GetProfile("cn-shanghai", array[0], array[1]);
        DefaultAcsClient client = new DefaultAcsClient(clientProfile);


        public ResultJson UpLoadGymData(UpLoadGymDataReq req)
        {
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            CacheHelper.Instance.SetGymData(req, userId);
            return new ResultJson("ok");
        }

        /// <summary>
        /// app连接设备调这个接口。如果不存在那个设备名字，要不要直接加入数据库呢？
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<ContactDeviceRes> ContactDevice(ContactDeviceReq req)
        {
            var r = new ResultJson<ContactDeviceRes>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            
            var d = DeviceOper.Instance.GetByName(req.deviceName);
            if (d == null)
            {
                LogHelper.WriteLog(typeof(AllFunc), $"用户{userId}连接设备，设备名{req.deviceName}不存在");
                r.ListData.Add(new ContactDeviceRes { deviceId = 0 });
            }
            else
            {
                //d.userId = userId;
                //DeviceOper.Instance.Update(d);//-txy 也许不需要更新，全靠缓存。做过去看

                var user = Sys_UserOper.Instance.GetById(userId);
                CacheHelper.Instance.SetGymDataUserUrl(d.id, user.Url, user.UserName);
                r.ListData.Add(new ContactDeviceRes { deviceId = d.id });
                //var msg = req.isEnglish == 1 ? "Contact success" : "连接成功";
            }
            return r;
        }

        /// <summary>
        /// 如果给了点播列表，那就直接点播。没有的话，判断有没有直播url,有就直播，没有判断flag。如果flag=1，说明应该是点播，但是点播列表里一个都没有。如果flag=0，那就让他付钱
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<CheckLiveRes> CheckLive(IdReq req)
        {
            Sys_VideoInfoOper.Instance.UpdateLivePlayStatus();
            var r = new ResultJson<CheckLiveRes>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            var liveId = (int)req.Id;
            var live = Sys_VideoInfoOper.Instance.GetById(liveId);

            var msg = req.isEnglish == 1 ? "Live not exists" : "不存在这个直播";
            if (live == null)
                throw new Exception(msg);

            var res = new CheckLiveRes(live);
            if (live.PlayStatus == 3)
            {
                res.flag = 1;
                var videoId = live.VideoId;
                if (videoId == null || videoId == 0)
                {

                }
                else
                {
                    var pv = Sys_PlayVideoOper.Instance.GetById((int)videoId);
                    pv.Title = req.isEnglish == 1 ? pv.TitleE : pv.Title;
                    res.videoInfo = new VideoInfo { Title = pv.Title, Url = pv.Url };
                }
                r.ListData.Add(res);
                return r;
            }
            //if (live.BeginTime > DateTime.Now)
            //    throw new Exception("直播还未开始,开始时间是:" + Convert.ToDateTime(live.BeginTime).ToString("yyyy-MM-dd HH:mm"));
            //判断有没有付钱之类的
            var list = Sys_VideoInfoOper.Instance.GetPayViewByVideoId(live.Id);
            if (list.Count == 0)
                throw new Exception(msg);
            List<string> liveUrl = new List<string>();
            if (!string.IsNullOrWhiteSpace(live.CloudId))
            {
                Sys.VCloud v = new Sys.VCloud();
                Sys.V_Create_Msg v_msg = v.GetAddress(live.CloudId);
                if (v_msg.code != 200)
                    throw new Exception(v_msg.msg);
                liveUrl.Add(v_msg.ret.rtmpPullUrl);
                liveUrl.Add(v_msg.ret.httpPullUrl);
                liveUrl.Add(v_msg.ret.hlsPullUrl);
                //return new RdfMsg(false, v_msg.msg);
            }
            res.liveUrl = liveUrl;
            if (list.First().price == 0 || list.First().UserId == userId || list.Where(p => p.payUserId == userId && p.status == 1).ToList().Count > 0)
            {
                res.flag = 1;
                r.ListData.Add(res);
                return r;
            }
            res.flag = 0;
            r.ListData.Add(res);
            return r;
        }

        public ResultJson<apkRes> GetVersion()
        {
            var r = new ResultJson<apkRes>();
            r.ListData = new List<apkRes>();
            var v = AndroidVersionOper.Instance.GetLastVersion();
            if (v == null)
                return r;
            v.apkFileUrl = StringHelper.Instance.GetApiUrl(v.apkFileUrl);
            r.ListData.Add(new apkRes(v));
            return r;
        }

        public ResultJson<GetVideoListRes> LoadPlayVideo(LoadPlayVideoReq req)
        {
            var r = new ResultJson<GetVideoListRes>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var index = (int)req.pageIndex;
            var isEnglish = req.isEnglish == 1 ? 1 : 0;
            var list = Sys_PlayVideoOper.Instance.GetListForApi(index);
            r.ListData = new List<GetVideoListRes>();
            //var isEnglish = req.isEnglish == 1 ? 1 : 0;
            foreach (var item in list)
            {
                r.ListData.Add(new GetVideoListRes(item, isEnglish));
            }
            return r;
        }

        /// <summary>
        /// 判断直播能不能看
        /// </summary>
        public ResultJson<LiveFlag> CheckLiveTest(IdReq req)
        {
            var r = new ResultJson<LiveFlag>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            var liveId = (int)req.Id;
            var list = Sys_VideoInfoOper.Instance.GetPayViewByVideoId(liveId);
            var msg = req.isEnglish == 1 ? "Live not exists" : "不存在这个直播";
            if (list.Count == 0)
                throw new Exception(msg);
            if (list.First().price == 0 || list.First().UserId == userId)
                r.ListData.Add(new LiveFlag { flag = 1, id = list.First().Id, price = list.First().price });
            else
            {
                var temp = list.Where(p => p.payUserId == userId && p.status == 1).ToList();
                if (temp.Count == 0)
                    r.ListData.Add(new LiveFlag { flag = 0, id = list.First().Id, price = list.First().price });
                else
                    r.ListData.Add(new LiveFlag { flag = 1, id = list.First().Id, price = list.First().price });
            }
            return r;

        }

        /// <summary>
        /// 判断视频能不能看
        /// </summary>
        public ResultJson<VideoFlag> CheckVideoTest(IdReq req)
        {
            var r = new ResultJson<VideoFlag>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            var videoId = (int)req.Id;
            var list = Sys_PlayVideoOper.Instance.GetPayViewByVideoId(videoId);
            var msg = req.isEnglish == 1 ? "Video not exists" : "不存在这个视频";
            if (list.Count == 0)
                throw new Exception(msg);

            if (list.First().price == 0 || list.First().userId == userId)
                r.ListData.Add(new VideoFlag { flag = 1, videoId = list.First().Id, price = list.First().price });
            else
            {
                var temp = list.Where(p => p.payUserId == userId && p.status == 1).ToList();
                if (temp.Count == 0)
                    r.ListData.Add(new VideoFlag { flag = 0, videoId = list.First().Id, price = list.First().price });
                else
                    r.ListData.Add(new VideoFlag { flag = 1, videoId = list.First().Id, price = list.First().price });
            }
            return r;
        }

        /// <summary>
        /// 支付视频
        /// </summary>
        public ResultJson PayVideoTest(PayVideoTestReq req)
        {
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            var vid = (int)req.videoId;
            var pv = Sys_PlayVideoOper.Instance.GetById(vid);
            var msg = req.isEnglish == 1 ? "Price error" : "价格不对";
            if (pv.price != req.price)
                throw new Exception(msg);

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"PayVideoTest{userId}");
            try
            {
                var pr = new PayRecord();
                pr.coachId = pv.userId;
                pr.userId = userId;
                pr.type = 0;
                pr.money = req.price;
                pr.status = 1;
                pr.createTime = DateTime.Now;
                pr.videoId = vid;
                PayRecordOper.Instance.Add(pr, conn, tran);

                var coach = Sys_UserOper.Instance.GetById((int)pv.userId);
                coach.balance += req.price;
                Sys_UserOper.Instance.Update(coach, conn, tran);

                tran.Commit();
                conn.Close();
                return new ResultJson("支付成功");
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
        /// 第三方支付的哦
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<payRes> PayVideo(PayVideoReq req)
        {
            var r = new ResultJson<payRes>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            var vid = (int)req.videoId;
            var Spbill_create_ip = req.spbill_create_ip;
            var pv = Sys_PlayVideoOper.Instance.GetById(vid);
            var msg = req.isEnglish == 1 ? "Price error" : "价格不对";
            if (pv.price != req.price)
                throw new Exception(msg);

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"PayVideo_userId:{userId}");
            try
            {
                var pr = new PayRecord();
                pr.coachId = pv.userId;
                pr.userId = userId;
                pr.type = 0;
                pr.status = 0;
                pr.money = req.price;
                pr.createTime = DateTime.Now;
                pr.videoId = vid;
                var id = PayRecordOper.Instance.Add(pr, conn, tran);
                var orderNo = GetOrderNo(id);
                pr.id = id;
                pr.outTradeNo = orderNo;
                if (req.payType == 0)
                {
                    pr.payType = 0;
                    PayRecordOper.Instance.Update(pr, conn, tran);
                    AlipayHelper a = new AlipayHelper();
                    var b = a.CreateAlipayOrder(req.price.ToString(), orderNo, apiHost + "api/notify/alipayNotify");
                    r.ListData.Add(new payRes { payType = 0, payStr = b });
                    //return r;
                }
                else
                {
                    pr.payType = 1;
                    PayRecordOper.Instance.Update(pr, conn, tran);
                    WXPayHelper w = new WXPayHelper();
                    var b = w.CreateWXOrder((decimal)req.price, orderNo, apiHost + "api/notify/wxpayNotify", Spbill_create_ip);
                    r.ListData.Add(new payRes { payType = 1, payStr = b });
                    //return r;
                }
                tran.Commit();
                conn.Close();
                return r;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                throw new Exception(ex.Message);
            }
        }

        public ResultJson<IosPurchaseRes> IosPurchase(IosPurchaseReq req)
        {
            var r = new ResultJson<IosPurchaseRes>();
            IosPurchaseHelper p = new IosPurchaseHelper();
            var flag = p.ValidateApplePay();
            if (!flag)
            {
                r.ListData.Add(new IosPurchaseRes { flag = 0 });
                return r;
            }
            r.ListData.Add(new IosPurchaseRes { flag = 1 });
            //成功，要做的事
            return r;
        }

        public string GetOrderNo(int orderId)
        {
            return DateTime.Now.ToString("yyMMdd") + StringHelper.Instance.GetIntStringWithZero(orderId.ToString(), 5) + RandHelper.Instance.Number(3);
        }

        /// <summary>
        /// 支付直播
        /// </summary>
        public ResultJson PayLiveTest(PayLiveTestReq req)
        {
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            var vid = (int)req.id;
            var pv = Sys_VideoInfoOper.Instance.GetById(vid);
            var msg = req.isEnglish == 1 ? "Price error" : "价格不对";
            if (pv.price != req.price)
                throw new Exception(msg);

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"PayLiveTest{userId}");
            try
            {
                var pr = new PayRecord();
                pr.coachId = pv.UserId;
                pr.userId = userId;
                pr.type = 0;
                pr.money = req.price;
                pr.status = 1;
                pr.createTime = DateTime.Now;
                pr.liveId = vid;
                PayRecordOper.Instance.Add(pr, conn, tran);

                var coach = Sys_UserOper.Instance.GetById((int)pv.UserId);
                coach.balance += req.price;
                Sys_UserOper.Instance.Update(coach, conn, tran);

                tran.Commit();
                conn.Close();
                return new ResultJson("支付成功");
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
        /// 第三方支付的哦
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<payRes> PayLive(PayLiveReq req)
        {
            var r = new ResultJson<payRes>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            var liveId = (int)req.id;
            var Spbill_create_ip = req.spbill_create_ip;
            var pv = Sys_VideoInfoOper.Instance.GetById(liveId);
            var msg = req.isEnglish == 1 ? "Price error" : "价格不对";
            if (pv.price != req.price)
                throw new Exception(msg);

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"PayLive_userId:{userId}");
            try
            {
                var pr = new PayRecord();
                pr.coachId = pv.UserId;
                pr.userId = userId;
                pr.type = 0;
                pr.status = 0;
                pr.money = req.price;
                pr.createTime = DateTime.Now;
                pr.liveId = liveId;
                var id = PayRecordOper.Instance.Add(pr, conn, tran);
                var orderNo = GetOrderNo(id);
                pr.id = id;
                pr.outTradeNo = orderNo;
                if (req.payType == 0)
                {
                    pr.payType = 0;
                    PayRecordOper.Instance.Update(pr, conn, tran);
                    AlipayHelper a = new AlipayHelper();
                    var b = a.CreateAlipayOrder(req.price.ToString(), orderNo, apiHost + "api/notify/alipayNotify");
                    r.ListData.Add(new payRes { payType = 0, payStr = b });
                    //return r;
                }
                else
                {
                    pr.payType = 1;
                    PayRecordOper.Instance.Update(pr, conn, tran);
                    WXPayHelper w = new WXPayHelper();
                    var b = w.CreateWXOrder((decimal)req.price, orderNo, apiHost + "api/notify/wxpayNotify", Spbill_create_ip);
                    r.ListData.Add(new payRes { payType = 1, payStr = b });
                    //return r;
                }
                tran.Commit();
                conn.Close();
                return r;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 提现
        /// </summary>
        public ResultJson<TakeCashRes> TakeCash(TakeCashReq req)
        {
            var r = new ResultJson<TakeCashRes>("已提交，待审核");
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"TakeCash{userId}");
            try
            {
                var user = Sys_UserOper.Instance.GetById(userId);
                var msg = req.isEnglish == 1 ? "Balance not enough" : "余额不足";
                if (user.balance < req.money)
                    throw new Exception(msg);

                var pr = new PayRecord();
                pr.coachId = userId;
                pr.type = 99;
                pr.money = req.money;
                pr.status = 0;
                pr.accountName = req.accountName;
                pr.createTime = DateTime.Now;
                pr.payType = req.payType;
                pr.account = req.account;
                PayRecordOper.Instance.Add(pr, conn, tran);

                user.balance -= req.money;
                Sys_UserOper.Instance.Update(user, conn, tran);
                r.ListData.Add(new TakeCashRes { balance = user.balance });
                tran.Commit();
                conn.Close();
                return r;
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
        /// 获取收入明细
        /// </summary>
        public ResultJson<GetInComeListRes> GetInComeRecord(GetInComeRecordReq req)
        {
            var r = new ResultJson<GetInComeListRes>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            var date = Convert.ToDateTime(req.date);
            var list = PayRecordOper.Instance.GetViewByCoachId(userId, 10, (int)req.pageIndex, date);
            r.ListData = new List<GetInComeListRes>();
            foreach (var item in list)
            {
                r.ListData.Add(new GetInComeListRes(item, req.isEnglish));
            }
            return r;
        }

        /// <summary>
        /// 设置手机号（绑定或者更换）
        /// </summary>
        public ResultJson UpdatePhone(UpdatePhoneReq req)
        {
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            CheckCode(req.phone, (int)req.code, req.isEnglish);
            var user = Sys_UserOper.Instance.GetById(userId);
            //var msg = req.isEnglish == 1 ? "Phone number exists" : "不能与原手机号相同";
            if (user.Phone == req.phone)
            {
                var msg = req.isEnglish == 1 ? "Phone number is same" : "不能与原手机号相同";
                throw new Exception(msg);
            }
            if (Sys_UserOper.Instance.IsPhoneExist(req.phone, user.Id))
            {
                var msg = req.isEnglish == 1 ? "Phone number exists" : "已存在这个手机号";
                throw new Exception(msg);
            }
            user.Phone = req.phone;
            Sys_UserOper.Instance.Update(user);
            var msg2 = req.isEnglish == 1 ? "Success" : "设置手机号成功";
            return new ResultJson(msg2);
        }

        /// <summary>
        /// 运动干货留言
        /// </summary>
        public ResultJson AddMsg(AddMsgReq req)
        {
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            var m = new SportMsg();
            m.userId = userId;
            m.createTime = DateTime.Now;
            m.content = req.msg;
            m.sportId = req.sportId;
            SportMsgOper.Instance.Add(m);
            return new ResultJson("留言成功");
        }

        /// <summary>
        /// 获取运动干货的留言
        /// </summary>
        public ResultJson<GetSportMsgRes> GetSportMsg(GetSportMsgReq req)
        {
            var r = new ResultJson<GetSportMsgRes>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var list = SportMsgOper.Instance.GetSportMsgBySportId((int)req.sportId, (int)req.pageIndex);
            if (list.Count == 0)
            {
                r.ListData = new List<GetSportMsgRes>();
                return r;
            }
            foreach (var item in list)
            {
                r.ListData.Add(new GetSportMsgRes(item));
            }
            return r;
        }

        /// <summary>
        /// 关闭直播
        /// </summary>
        public ResultJson CloseLive(IdReq req)
        {
            var r = new ResultJson<PlugFlowUrlRes>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var id = (int)req.Id;
            var live = Sys_VideoInfoOper.Instance.GetById(id);
            Sys.OnlineUser.RemoveLiveAsync(id);
            Sys.VCloud v = new Sys.VCloud();
            if (Convert.ToDateTime(live.BeginTime).AddMinutes((int)live.PlayLongTime) <= DateTime.Now)
            {
                live.PlayStatus = 3;
                Sys.V_Delete_Msg msg = v.Delete(live.CloudId);
                if (msg.code == 200)
                    live.CloudId = "";
            }
            else
            {
                live.PlayStatus = 2;
            }
            Sys_VideoInfoOper.Instance.Update(live);
            return new ResultJson("成功关闭");
        }

        /// <summary>
        /// 教练获取直播的拉流地址
        /// </summary>
        public ResultJson<PlugFlowUrlRes> GetPlugFlowUrl(IdReq req)
        {
            Sys_VideoInfoOper.Instance.UpdateLivePlayStatus();
            var r = new ResultJson<PlugFlowUrlRes>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var id = (int)req.Id;
            var live = Sys_VideoInfoOper.Instance.GetById(id);

            switch (live.PlayStatus)
            {
                case 3:
                    DateTime end = Convert.ToDateTime(live.BeginTime).AddMinutes((int)live.PlayLongTime);
                    var msg = req.isEnglish == 1 ? "Live is over,endTime is :" + end.ToString("yyyy-MM-dd HH:mm") : "直播已结束,结束时间是:" + end.ToString("yyyy-MM-dd HH:mm");
                    throw new Exception(msg);
                case 4:
                    var msg2 = req.isEnglish == 1 ? "Live is failed" : "直播已失效";
                    throw new Exception(msg2);
                default:
                    break;
            }

            if (live.isPass != "1")
            {
                if (live.isPass == "0")
                {
                    var msg = req.isEnglish == 1 ? "Auditing" : "审核中";
                    throw new Exception(msg);
                }
                else
                {
                    var msg = req.isEnglish == 1 ? "Audit failed" : "未通过审核";
                    throw new Exception(msg);

                }
            }

            if (live.BeginTime > DateTime.Now)
            {
                var msg = req.isEnglish == 1 ? "Live hasn't  start,startTime is :" + Convert.ToDateTime(live.BeginTime).AddMinutes(15).ToString("yyyy-MM-dd HH:mm") : "直播还未开始,开始时间是:" + Convert.ToDateTime(live.BeginTime).AddMinutes(15).ToString("yyyy-MM-dd HH:mm");

                //throw new Exception("直播还未开始,开始时间是:" + Convert.ToDateTime(live.BeginTime).AddMinutes(15).ToString("yyyy-MM-dd HH:mm"));

                throw new Exception(msg);
            }

            Sys.VCloud v = new Sys.VCloud();
            Sys.V_Create_Msg v_msg = null;
            if (string.IsNullOrWhiteSpace(live.CloudId))
            {
                v_msg = v.Create(live.Id.ToString());
                if (v_msg.code != 200)
                    throw new Exception(v_msg.msg);
                //return new RdfMsg(false, v_msg.msg);
                live.CloudId = v_msg.ret.cid;
                live.PlayStatus = 1;
                Sys_VideoInfoOper.Instance.Update(live);
            }
            else
            {
                v_msg = v.GetAddress(live.CloudId);
                if (v_msg.code != 200)
                    throw new Exception(v_msg.msg);
                live.PlayStatus = 1;
                Sys_VideoInfoOper.Instance.Update(live);
                //return new RdfMsg(false, v_msg.msg);
            }
            r.ListData.Add(new PlugFlowUrlRes { plugFlowUrl = v_msg.ret.pushUrl, Id = id });
            return r;
        }

        /// <summary>
        /// 获取教练自己的直播列表
        /// </summary>
        public ResultJson<GetLiveListRes> GetLiveList(PageIndexReq req)
        {
            Sys_VideoInfoOper.Instance.UpdateLivePlayStatus();
            var r = new ResultJson<GetLiveListRes>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            var list = Sys_VideoInfoOper.Instance.GetListByCoachId(userId, (int)req.pageIndex);

            r.ListData = new List<GetLiveListRes>();
            var isEnglish = req.isEnglish == 1 ? 1 : 0;
            foreach (var item in list)
            {
                r.ListData.Add(new GetLiveListRes(item, isEnglish));
            }
            return r;
        }

        /// <summary>
        /// 获取教练自己的视频列表
        /// </summary>
        public ResultJson<GetVideoListRes> GetVideoList(PageIndexReq req)
        {
            var r = new ResultJson<GetVideoListRes>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            var list = Sys_PlayVideoOper.Instance.GetListByCoachId(userId, (int)req.pageIndex);
            r.ListData = new List<GetVideoListRes>();
            var isEnglish = req.isEnglish == 1 ? 1 : 0;
            foreach (var item in list)
            {
                r.ListData.Add(new GetVideoListRes(item, isEnglish));
            }
            return r;
        }

        /// <summary>
        /// 删除视频，云上删除
        /// </summary>
        public void DeleteVideo(string videoId)
        {
            DeleteVideoRequest request = new DeleteVideoRequest();
            request.VideoIds = videoId;
            // 发起请求，并得到 response
            DeleteVideoResponse response = client.GetAcsResponse(request);
        }

        /// <summary>
        /// 删除视频
        /// </summary>
        public ResultJson DeleteVideoById(IdReq req)
        {
            var r = new ResultJson<GetVideoListRes>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var id = (int)req.Id;
            var pv = Sys_PlayVideoOper.Instance.GetById(id);
            Sys_PlayVideoOper.Instance.DeleteVideoById(id);
            DeleteVideo(pv.VieldId);
            var msg = req.isEnglish == null ? "删除成功" : "Delete success";
            return new ResultJson(msg);
        }

        /// <summary>
        /// app获取RequestId
        /// </summary>
        public ResultJson<AppUploadAuthRes> GetAuth()
        {
            CreateUploadVideoRequest request = new CreateUploadVideoRequest();
            request.Title = "test";
            request.FileName = "test.mp4";
            //request.Description = "视频描述";
            //request.CoverURL = "http://cover.sample.com/sample.jpg";
            //request.Tags = "标签1,标签2";
            //request.CateId = 0;
            try
            {
                CreateUploadVideoResponse response = client.GetAcsResponse(request);

                //CreateUploadVideoResponse response = client.GetAcsResponse(request);
                var r = new ResultJson<AppUploadAuthRes>();
                r.ListData.Add(new AppUploadAuthRes
                {
                    RequestId = response.RequestId,
                    VideoId = response.VideoId,
                    UploadAddress = response.UploadAddress,
                    UploadAuth = response.UploadAuth
                });
                return r;
                //Console.WriteLine("RequestId = " + response.RequestId);
                //Console.WriteLine("VideoId = " + response.VideoId);
            }
            catch (ServerException e)
            {
                throw new Exception(e.ErrorCode + e.ErrorMessage);
            }
            catch (ClientException e)
            {

                throw new Exception(e.ErrorCode + e.ErrorMessage);
            }
        }

        /// <summary>
        /// 获取排行榜
        /// </summary>
        public ResultJson<GetRankListRes> GetRankList(GetRankListReq req)
        {
            var r = new ResultJson<GetRankListRes>();
            var res = new GetRankListRes();
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            int size = 10;
            var index = (int)req.pageIndex;
            var isEnglish = req.isEnglish == 1 ? 1 : 0;
            var list = Sys_DataOper.Instance.GetRankList(Convert.ToDateTime(req.date));
            var own = list.Where(p => p.UserId == userId).ToList();
            if (own.Count > 0)
            {
                res.order = own.First().order;
                res.TotalKAL = own.First().TotalKAL;
                res.TotalKM = own.First().TotalKM;
                res.UserName = own.First().UserName;
                res.Url = StringHelper.Instance.GetApiUrl(own.First().Url);
            }
            var temp = list.Skip((index - 1) * size).Take(10).ToList();
            res.rankList = new List<GetRankList>();
            foreach (var item in temp)
            {
                item.Url = StringHelper.Instance.GetApiUrl(item.Url);
                res.rankList.Add(item);
            }

            r.ListData.Add(res);
            return r;
        }

        /// <summary>
        /// 上传运动数据
        /// </summary>
        public ResultJson UpLoadData(UpLoadDataReq req)
        {
            var r = new ResultJson<GetVideoListRes>();
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            var lastData = Sys_DataOper.Instance.GetLastDataByUserId(userId);
            var data = new Sys_Data();
            data.UserId = userId;
            data.CreateTime = DateTime.Now;
            data.XL = req.XL;
            data.SD = req.SD;
            data.KAL = req.KAL;
            data.KM = req.KM;
            data.ZS = req.ZS;
            data.Time = req.Time;
            data.WATT = req.WATT;
            data.TotalKAL = lastData.TotalKAL + data.KAL;
            data.TotalKM = req.TotalKM;
            data.TotalTime = StringHelper.Instance.GetSeconds(req.Time) + lastData.TotalTime;
            Sys_DataOper.Instance.Add2(data);
            return new ResultJson("上传成功");
        }


        /// <summary>
        /// 创建直播
        /// </summary>
        public ResultJson CreateLive(CreateLiveReq req)
        {
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            var url = "";
            SqlTransaction tran = conn.BeginTransaction($"CreateLive_{userId}");
            try
            {
                var live = new Sys_VideoInfo();
                live.UserId = userId;
                //req.Url;
                if (req.isEnglish == 1)
                {
                    live.TitleE = req.Title;
                    live.Title = "";
                }
                else
                    live.Title = req.Title;
                live.BeginTime = Convert.ToDateTime(req.BeginTime).AddMinutes(-15);
                live.PlayLongTime = req.PlayLongTime + 30;
                live.PlayStatus = 0;
                live.DataStatus = 0;
                live.price = req.price;
                live.CloudId = "";
                live.isPass = "0";
                live.isEnglish = req.isEnglish == 1 ? 1 : 0;
                //-txy  直播的初始状态。。弄清楚再说
                live.Url = SaveLiveImg(req.Url, userId);
                url = live.Url;
                Sys_VideoInfoOper.Instance.Add(live, conn, tran);

                tran.Commit();
                conn.Close();
                var msg = req.isEnglish == 1 ? "Upload success , waiting audit" : "创建成功,待审核";
                return new ResultJson(msg);
            }
            catch (Exception ex)
            {
                //DeleteUrl(url);
                tran.Rollback();
                conn.Close();
                var path = baseUrl + url;
                if (File.Exists(path))
                    File.Delete(path);
                //r.HttpCode = 500;
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 申请成为教练
        /// </summary>
        public ResultJson ApplyToCoach(ApplyToCoachReq req)
        {
            var dict = ValidateToken(req.token, req.isEnglish);
            var userId = dict[req.token].Id;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"ApplyToCoach{userId}");
            var user = Sys_UserOper.Instance.GetById(userId);
            if (user.Type == 1)
            {
                var msg = req.isEnglish == 1 ? "Don't need apply" : "不需要申请";
                //if (req.isEnglish == 1)
                //    throw new Exception("Don't need apply");
                throw new Exception(msg);
            }
            if (user.isPass == "0")
            {
                var msg = req.isEnglish == 1 ? "Applying" : "正在审核中";
                //if (req.isEnglish == 1)
                //    throw new Exception("Applying");
                throw new Exception(msg);
            }
            var coachImg = "";
            var idFrontImg = "";
            var idBackImg = "";
            var videoImg = "";
            try
            {
                coachImg = SaveCoachImg(req.coachImg, userId);
                idFrontImg = SaveIdFrontImg(req.idCardFront, userId);
                idBackImg = SaveIdBackImg(req.idCardBack, userId);
                if (req.TitleUrl != null)
                    videoImg = SaveCoachVideoImg(req.TitleUrl, userId);

                user.coachImg = coachImg;
                user.idCardFront = idFrontImg;
                user.idCardBack = idBackImg;
                user.isPass = "0";
                Sys_UserOper.Instance.Update(user, conn, tran);
                if (req.TitleUrl != null)
                {
                    var pv = new Sys_PlayVideo();
                    pv.Title = req.Title;
                    pv.TitleUrl = videoImg;
                    pv.Url = "http://vod.pooboofit.com/" + req.Url;
                    pv.EditTime = DateTime.Now;
                    pv.PlayCount = 0;
                    pv.Enabled = false;
                    pv.userId = userId;
                    pv.price = (decimal)req.price;
                    pv.LongTime = req.LongTime;
                    pv.isPass = "0";
                    Sys_PlayVideoOper.Instance.Add(pv, conn, tran);
                }

                tran.Commit();
                conn.Close();
                if (req.isEnglish == 1)
                    return new ResultJson("Applied , to be audited");
                return new ResultJson("已申请，待审核");
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                DeleteUrl(coachImg);
                DeleteUrl(idFrontImg);
                DeleteUrl(idBackImg);
                DeleteUrl(videoImg);
                throw new Exception(ex.Message);
                //r.HttpCode = 500;
                //r.Message = 
            }



        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson UpdatePwd(UpdatePwdReq req)
        {
            CheckCode(req.phone, (int)req.code, req.isEnglish);
            var user = Sys_UserOper.Instance.GetByPhone(req.phone);
            if (user == null)
            {
                var msg2 = req.isEnglish == 1 ? "User does not exist" : "不存在此用户";

                //if (req.isEnglish == 1)
                //    throw new Exception("User does not exist");
                throw new Exception(msg2);
            }
            user.UserPwd = MD5Helper.Instance.Md5_32(req.pwd);
            Sys_UserOper.Instance.Update(user);
            var msg = req.isEnglish == 1 ? "Success" : "更改密码成功";
            //if (req.isEnglish == 1)
            //    return new ResultJson("Update password success");
            return new ResultJson(msg);
        }

        /// <summary>
        /// 可能不用，直接
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson LoginOut(LoginOutReq req)
        {
            var dict = CacheHelper.Instance.GetUserToken();
            dict.Remove(req.token);
            CacheHelper.Instance.SetUserToken(dict);
            var msg = req.isEnglish == 1 ? "Success" : "退出成功";
            return new ResultJson(msg);
        }

        /// <summary>
        /// 密码注册
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<UserLoginRes> RegisterPwd(RegisterPwdReq req)
        {
            var RegexStr = StringHelper.Instance.regPwd;
            if (!Regex.IsMatch(req.pwd, RegexStr))
            {
                var msg = req.isEnglish == 1 ? "6 to 16 English or numeric figures" : "6到16位英文或数字";
                //if (req.isEnglish == 1)
                //    throw new Exception("6 to 16 English or numeric figures.");
                throw new Exception(msg);
            }

            var user = Sys_UserOper.Instance.GetByPhone(req.phone);
            if (user != null)
            {
                var msg = req.isEnglish == 1 ? "PhoneNumber existed" : "已存在该手机号";
                //if (req.isEnglish == 1)
                //    throw new Exception("PhoneNumber existed");
                throw new Exception(msg);
            }

            var r = new ResultJson<UserLoginRes>();
            DataService ds = new DataService();
            //ds.CheckCode2(req.phone, (int)req.code);
            user = new Sys_User();
            user.Type = 2;
            user.UserPwd = "";
            user.UserName = "BlankUser";
            user.Phone = req.phone;
            user.UserPwd = MD5Helper.Instance.Md5_32(req.pwd);
            user.Birthday = DateTime.Now;
            user.Sex = true;
            user.Height = 0;
            user.Url = "Images\\default.png";
            user.Weight = 0;
            user.IdealWeight = 0;
            user.Enabled = true;
            user.RegisterTime = DateTime.Now;
            user.isEnglish = req.isEnglish == 1 ? 1 : 0;

            user.Id = Sys_UserOper.Instance.Add(user);

            var token = UpdateToken(user);
            var totalKm = (decimal)Sys_DataOper.Instance.GetLastDataByUserId(user.Id).TotalKM;
            r.ListData.Add(new UserLoginRes(user, token, totalKm));
            return r;
        }

        /// <summary>
        /// 验证码登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<UserLoginRes> CodeLogin(CodeLoginReq req)
        {
            var r = new ResultJson<UserLoginRes>();
            //DataService ds = new DataService();
            CheckCode(req.phone, (int)req.code, req.isEnglish);
            var user = Sys_UserOper.Instance.GetByPhone(req.phone);
            if (user == null)
            {
                var msg = req.isEnglish == 1 ? "Phone number not exists" : "手机号不存在";
                throw new Exception(msg);
            }
            //string token = Guid.NewGuid().ToString();
            //DataService.UserDic.Add(token, new Sys.Sys_User(user));
            //CacheHelper.Instance.SetUserToken(DataService.UserDic);
            var token = UpdateToken(user);
            user.LoginTime = DateTime.Now;
            user.isEnglish = req.isEnglish == 1 ? 1 : 0;
            Sys_UserOper.Instance.Update(user);
            var totalKm = (decimal)Sys_DataOper.Instance.GetLastDataByUserId(user.Id).TotalKM;
            r.ListData.Add(new UserLoginRes(user, token, totalKm));
            return r;
        }

        /// <summary>
        /// 密码登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<UserLoginRes> PwdLogin(PwdLoginReq req)
        {
            var r = new ResultJson<UserLoginRes>();
            var user = Sys_UserOper.Instance.GetByPhone(req.phone);
            if (user == null)
            {
                var msg = req.isEnglish == 1 ? "User does not exist" : "不存在该用户";
                //if (req.isEnglish == 1)
                //    throw new Exception("User does not exist");
                throw new Exception(msg);
            }
            if (user.UserPwd != MD5Helper.Instance.Md5_32(req.pwd))
            {
                var msg = req.isEnglish == 1 ? "Password error" : "密码错误";
                //if (req.isEnglish == 1)
                //    throw new Exception("");
                throw new Exception(msg);
            }
            if (!(bool)user.Enabled)
            {
                var msg = req.isEnglish == 1 ? "Your account is disabled" : "您的账号被禁用不能登录";
                //if (req.isEnglish == 1)
                //    throw new Exception("Your account is disabled");
                throw new Exception(msg);
            }
            var token = UpdateToken(user);
            user.LoginTime = DateTime.Now;
            user.isEnglish = req.isEnglish == 1 ? 1 : 0;
            Sys_UserOper.Instance.Update(user);
            var totalKm = (decimal)Sys_DataOper.Instance.GetLastDataByUserId(user.Id).TotalKM;
            r.ListData.Add(new UserLoginRes(user, token, totalKm));
            return r;
        }

        /// <summary>
        /// 第三方登录(第一次则注册
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<UserLoginRes> CheckUId(CheckUIdReq req)
        {
            var r = new ResultJson<UserLoginRes>();
            var user = Sys_UserOper.Instance.IsUIdExist(req);
            //注册
            if (user == null)
            {
                user = new Sys_User();
                switch (req.type)
                {
                    case 0:
                        user.qqUId = req.UId;
                        break;
                    case 1:
                        user.wxUId = req.UId;
                        break;
                    case 2:
                        user.fbUId = req.UId;
                        break;
                    case 3:
                        user.ttUId = req.UId;
                        break;
                }
                user.Type = 2;
                user.UserPwd = "";

                //user.Phone = req.;
                user.Birthday = DateTime.Now;
                if (req.UserName != null)
                    user.UserName = req.UserName;
                else
                    user.UserName = "BlankUser";
                if (req.Url != null)
                    user.Url = req.Url;
                else
                    user.Url = "Images\\default.png";
                if (req.Sex != null)
                    user.Sex = req.Sex == 1 ? true : false;
                else
                    user.Sex = true;
                user.Height = 0;
                user.Weight = 0;

                user.IdealWeight = 0;
                user.Enabled = true;
                user.RegisterTime = DateTime.Now;
                user.LoginTime = DateTime.Now;
                user.isEnglish = req.isEnglish == 1 ? 1 : 0;
                string token = Guid.NewGuid().ToString();
                user.Phone = "temp" + DateTime.Now.ToString("yyyyMMddHHmmss") + token;
                if (user.Phone.Length > 50)
                    user.Phone = user.Phone.Substring(0, 50);
                user.Id = Sys_UserOper.Instance.Add(user);
                token = UpdateToken(user);
                var totalKm = (decimal)Sys_DataOper.Instance.GetLastDataByUserId(user.Id).TotalKM;
                r.ListData.Add(new UserLoginRes(user, token, totalKm));

                return r;
            }
            //登录
            else
            {
                if (!(bool)user.Enabled)
                {
                    var msg = req.isEnglish == 1 ? "Your account is disabled" : "您的账号被禁用不能登录";
                    //if (req.isEnglish == 1)
                    //    throw new Exception("Your account is disabled");
                    throw new Exception(msg);
                }

                var token = UpdateToken(user);
                user.LoginTime = DateTime.Now;
                Sys_UserOper.Instance.Update(user);
                var totalKm = (decimal)Sys_DataOper.Instance.GetLastDataByUserId(user.Id).TotalKM;
                r.ListData.Add(new UserLoginRes(user, token, totalKm));
            }
            return r;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson SendMail(SendMailReq req)
        {
            var r = new ResultJson();
            var phone = req.phone;
            if (true)
            {
                string verification = CacheHelper.Instance.SetUserVerificationCode(phone); //SetUserVerificationCode;
                Enum_SendEmailCode SendEmail;
                SendEmail = Enum_SendEmailCode.UserRegistrationVerificationCode;
                SendSmsResponse Email = AliyunHelper.SendMail.SendMail.Instance.SendEmail(phone, verification, SendEmail);
                //string str = "";
                if (Email.Code.ToUpper() == "OK")
                {
                    r.Message = "短信验证码发送成功";
                    return r;
                }
                //return ControllerHelper.Instance.JsonResult(200, "短信验证码发送成功");
                else
                    throw new Exception("请过段时间重新发送");
            }
        }

        /// <summary>
        /// 验证验证码是否正确
        /// </summary>
        public bool CheckCode(string phone, int code, int? isEnglish)
        {
            //if (code == 188571)//-txy
            //    return true;
            //if (phone == "17706405101" && code == 8888)
            //    return true;
            //if (phone == "18857120152" && code == 8888)
            //    return true;

            if (!RdfRegex.MobilePhone(phone))
            {
                var msg = isEnglish == 1 ? "Phone Number format error" : "手机号码格式错误";
                throw new Exception(msg);
            }

            var codeHere = CacheHelper.Instance.GetUserVerificationCode(phone);
            if (codeHere == null)
            {
                var msg = isEnglish == 1 ? "Please send code" : "请先获取验证码";
                throw new Exception(msg);
            }
            else if (codeHere != code.ToString())
            {
                var msg = isEnglish == 1 ? "Code error" : "验证码错误";
                throw new Exception(msg);
            }

            //if (!CodeDic.Keys.Contains(phone))
            //    throw new Exception("请先获取验证码!");
            //if (CodeDic[phone].Time.AddMinutes(5) < DateTime.Now)
            //{
            //    CodeDic.Remove(phone);
            //    throw new Exception("验证码过期,请重新获取!");
            //}
            //if (code != CodeDic[phone].Code)
            //    throw new Exception("验证码错误!");
            return true;
        }
        
        public Sys_PlayVideo GetPlayInfo(string videoId)
        {
            GetPlayInfoRequest request = new GetPlayInfoRequest();
            request.VideoId = videoId;
            GetPlayInfoResponse response = client.GetAcsResponse(request);
            var r = new Sys_PlayVideo();
            r.Url = response.PlayInfoList[0].PlayURL;
            r.LongTime = StringHelper.Instance.GetMinuteSecondStr(Convert.ToInt32(response.PlayInfoList[0].Duration.ToString().Split('.')[0]));
            return r;
        }

        /// <summary>
        /// 获取阻力模式列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<ModelListAllRes> GetDragModelList(GetDragModelListReq req)
        {
            var r = new ResultJson<ModelListAllRes>();
            ValidateToken(req.token, req.isEnglish);

            var res = new ModelListAllRes();
            var list = new List<DragModel>();
            //if (req.isEnglish == 1)
            //    list = DragModelOper.Instance.GetExistList(1);
            //else
            list = DragModelOper.Instance.GetExistList();

            var isEnglish = req.isEnglish == 1 ? 1 : 0;
            var temp = list.Where(p => p.modelType == 0).ToList();
            foreach (var item in temp)
            {
                res.meters.Add(new ModelListRes(item, isEnglish));
            }
            temp = list.Where(p => p.modelType == 1).ToList();
            foreach (var item in temp)
            {
                res.cals.Add(new ModelListRes(item, isEnglish));
            }
            temp = list.Where(p => p.modelType == 2).ToList();
            foreach (var item in temp)
            {
                res.times.Add(new ModelListRes(item, isEnglish));
            }

            r.ListData.Add(res);
            return r;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<UserLoginRes> GetUserInfo(GetUserInfoReq req)
        {
            var r = new ResultJson<UserLoginRes>();
            var token = req.token;
            var dict = ValidateToken(token, req.isEnglish);
            var id = dict[token].Id;
            var user = Sys_UserOper.Instance.GetById(id);
            var totalKm = (decimal)Sys_DataOper.Instance.GetLastDataByUserId(user.Id).TotalKM;
            r.ListData.Add(new UserLoginRes(user, token, totalKm));
            return r;
        }

        /// <summary>
        /// 教练添加课程
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ResultJson<AddCourseRes> AddCourse(AddCourseReq req)
        {
            var msg = req.isEnglish == null ? "上传成功" : "Upload success";
            var r = new ResultJson<AddCourseRes>(msg);
            var token = req.token;
            var dict = ValidateToken(token, req.isEnglish);
            var userId = dict[token].Id;
            var pv = GetPlayInfo(req.videoId);
            pv.EditTime = DateTime.Now;
            pv.PlayCount = 0;
            pv.Enabled = false;
            if (req.isEnglish == 1)
            {
                pv.TitleE = req.Title;
                pv.Title = "";
            }
            else
                pv.Title = req.Title;
            pv.price = req.price;
            pv.TitleUrl = req.TitleUrl;
            pv.VieldId = req.videoId;
            pv.isPass = "0";
            pv.userId = userId;
            pv.isEnglish = req.isEnglish == 1 ? 1 : 0;
            var id = Sys_PlayVideoOper.Instance.Add(pv);
            if (id > 0)
            {
                r.ListData.Add(new AddCourseRes { Id = id, TitleUrl = StringHelper.Instance.GetApiUrl(pv.TitleUrl) });
                return r;
            }
            var msg2 = req.isEnglish == null ? "上传失败" : "Upload failure";
            throw new Exception(msg2);
        }

        /// <summary>
        /// 登录或者什么。更新token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string UpdateToken(Sys_User user)
        {
            var dict = CacheHelper.Instance.GetUserToken();
            foreach (string item in dict.Keys)
            {
                if (dict[item].Id.Equals(user.Id))
                {
                    dict.Remove(item);
                    break;
                }
            }

            string token = Guid.NewGuid().ToString();
            dict.Add(token, new Sys.Sys_User(user));
            CacheHelper.Instance.SetUserToken(dict);
            return token;
        }

        /// <summary>
        /// token验证，通过返回字典
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Dictionary<string, Sys.Sys_User> ValidateToken(string token, int? isEnglish)
        {
            if (token == null)
            {
                var msg = isEnglish == 1 ? "token empty" : "token为空";
                var ex = new Exception(msg);
                ex.Data.Add("code", 400);
                throw ex;
            }
            var dict = CacheHelper.Instance.GetUserToken();
            if (!dict.Keys.Contains(token))
            {
                var msg = isEnglish == 1 ? "Not logged in" : "用户未登录或被其他登录下线";
                var ex = new Exception(msg);
                ex.Data.Add("code", 400);
                throw ex;
            }

            var userId = dict[token].Id;
            CacheHelper.Instance.SetActiveUser(userId);

            return dict;
        }

        public string SaveCoachVideoImg(string base64, int userId)
        {
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            //string saveFileName = DateTime.Now.ToFileTime().ToString();
            string shortPath = $"UpLoadFile/PlayVideoImage/{userId}-{name}-{RandHelper.Instance.Number(3)}.jpg";
            string path = baseUrl + shortPath;
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

        public string SaveCoachImg(string base64, int userId)
        {
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            //string saveFileName = DateTime.Now.ToFileTime().ToString();
            string shortPath = $"UpLoadFile/CoachImg/{userId}-{name}-{RandHelper.Instance.Number(3)}.jpg";
            string path = baseUrl + shortPath;
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

        public string SaveIdFrontImg(string base64, int userId)
        {
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            //string saveFileName = DateTime.Now.ToFileTime().ToString();
            string shortPath = $"UpLoadFile/IdCardFront/{userId}-{name}-{RandHelper.Instance.Number(3)}.jpg";
            string path = baseUrl + shortPath;
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

        public string SaveIdBackImg(string base64, int userId)
        {
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            byte[] byteArray = Convert.FromBase64String(base64);
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            //string saveFileName = DateTime.Now.ToFileTime().ToString();
            string shortPath = $"UpLoadFile/IdCardBack/{userId}-{name}-{RandHelper.Instance.Number(3)}.jpg";
            string path = baseUrl + shortPath;
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
            string path = baseUrl + shortPath;
            Stream s = new FileStream(path, FileMode.Append);
            s.Write(byteArray, 0, byteArray.Length);
            s.Close();
            return shortPath;
        }

        public void DeleteUrl(string url)
        {
            var path = baseUrl + url;
            if (File.Exists(path))
                File.Delete(path);
        }

        public object GetPlayInfo(TestReq req)
        {

            GetPlayInfoRequest request = new GetPlayInfoRequest();
            request.VideoId = req.str;
            GetPlayInfoResponse response = client.GetAcsResponse(request);
            return response;
        }

        public object GetVideoInfo(TestReq req)
        {
            GetVideoInfoRequest request = new GetVideoInfoRequest();
            request.VideoId = req.str;
            GetVideoInfoResponse response = client.GetAcsResponse(request);
            return response;
        }
    }
}