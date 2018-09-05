using Common.Attribute;
using Common.Helper;
using DbOpertion.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DbOpertion.Models.Extend
{
    public class ApiModel
    {

    }
    public class TestReq : English
    {
        public int id { get; set; }
        public string str { get; set; }
        public int authId { get; set; }
    }

    public class English
    {
        public int? isEnglish { get; set; }
    }

    public class PwdLoginReq : English
    {
        [Required(ErrorMessage = "请输入手机号")]
        public string phone { get; set; }
        [Required]
        public string pwd { get; set; }

    }

    public class CodeLoginReq : English
    {
        [Required(ErrorMessage = "请输入手机号")]
        public string phone { get; set; }
        [Required(ErrorMessage = "请输入验证码")]
        public int? code { get; set; }

    }

    public class RegisterPwdReq : English
    {
        [Required(ErrorMessage = "请输入手机号")]
        public string phone { get; set; }
        [Required(ErrorMessage = "请输入密码")]
        public string pwd { get; set; }
        [Required(ErrorMessage = "请输入验证码")]
        public int? code { get; set; }
        //public int? isEnglish { get; set; }
    }

    public class RegisterReq : English
    {
        /// <summary>
        /// 0:qq  1:微信 2:Twitter 3:Facebook
        /// </summary>
        [Required]
        public int? type { get; set; }
        [Required]
        public string UId { get; set; }
        [Required(ErrorMessage = "请输入手机号")]
        public string phone { get; set; }
        [Required(ErrorMessage = "请输入验证码")]
        public int? code { get; set; }
    }

    public class SendMailReq : English
    {
        [IntStringValidate]
        public string phone { get; set; }
    }



    public class CheckUIdReq : English
    {
        /// <summary>
        /// 0:qq  1:微信 2:Twitter 3:Facebook
        /// </summary>
        [Required]
        public int? type { get; set; }
        [Required]
        public string UId { get; set; }
        public string Url { get; set; }
        public int? Sex { get; set; }
        public string UserName { get; set; }
        //public int? isEnglish { get; set; }
    }

    public class GetDragModelListReq : UserToken
    {
        public int? isEnglish { get; set; }
    }

    public class LoginOutReq : UserToken
    {
        public int? isEnglish { get; set; }
    }

    public class GetUserInfoReq : UserToken
    {
        public int? isEnglish { get; set; }
    }
    public class UserToken
    {
        public string token { get; set; }
    }

    public class CreateLiveReq : UserToken
    {
        public int? isEnglish { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime? BeginTime { get; set; }
        [IntStringValidate]
        public int? PlayLongTime { get; set; }
        [DecimalValidate]
        public decimal? price { get; set; }
    }

    public class ApplyToCoachReq : UserToken
    {
        /// <summary>
        /// 教练证图片base64
        /// </summary>
        public string coachImg { get; set; }
        public string idCardFront { get; set; }
        public string idCardBack { get; set; }
        /// <summary>
        /// 视频封面图
        /// </summary>
        public string TitleUrl { get; set; }
        /// <summary>
        /// 视频上传后的地址，应该是sv/……
        /// </summary>
        public string Url { get; set; }
        public string Title { get; set; }
        public decimal? price { get; set; }
        public string LongTime { get; set; }
        public int? isEnglish { get; set; }
    }

    public class UpdatePwdReq : English
    {
        [Required(ErrorMessage = "请输入手机号")]
        public string phone { get; set; }
        [Required(ErrorMessage = "请输入验证码")]
        public int? code { get; set; }
        [Required(ErrorMessage = "请输入新密码")]
        public string pwd { get; set; }
    }

    public class AddCourseRes
    {
        public int? isEnglish { get; set; }
        public int Id { get; set; }
        public string TitleUrl { get; set; }
    }

    public class GetLiveListRes
    {
        public GetLiveListRes(Sys_VideoInfo live, int isEnglish)
        {
            Id = live.Id;
            //UserId = (int)live.UserId;
            Url = StringHelper.Instance.GetApiUrl(live.Url);
            Title = isEnglish == 1 ? live.TitleE : live.Title;
            BeginTime = Convert.ToDateTime(live.BeginTime).AddMinutes(15).ToString("yyyy-MM-dd HH:mm");
            PlayLongTime = (int)live.PlayLongTime;
            PlayStatus = (int)live.PlayStatus;
            //DataStatus = live.DataStatus;
            //CloudId = live.CloudId;
            //VideoId = live.VideoId;
            isPass = live.isPass;
            price = live.price;
        }

        public Int32 Id { get; set; }
        //public Int32 UserId { get; set; }
        public String Url { get; set; }
        public String Title { get; set; }
        public string BeginTime { get; set; }
        public Int32 PlayLongTime { get; set; }
        public Int32 PlayStatus { get; set; }
        //public Int32 DataStatus { get; set; }
        //public String CloudId { get; set; }
        //public Int32? VideoId { get; set; }
        public String isPass { get; set; }
        public Decimal? price { get; set; }

    }

    public class GetVideoListRes
    {
        public GetVideoListRes(videoView view, int isEnglish)
        {
            Id = view.Id;
            Title = isEnglish == 1 ? view.TitleE : view.Title;
            TitleUrl = view.TitleUrl;
            Url = StringHelper.Instance.GetApiUrl(view.Url);
            LongTime = view.LongTime;
            EditTime = view.EditTime.ToString("yyyy-MM-dd HH:mm");
            PlayCount = view.PlayCount;
            //Enabled = view.Enabled;
            //VieldId = view.VieldId;
            userId = view.userId;
            price = view.price;
            isPass = view.isPass;
            //TitleE = view.TitleE;
            //UserName = view.UserName;
            //type = view.type;
            //isUserPass = view.isUserPass;
        }
        public Int32 Id { get; set; }
        public String Title { get; set; }
        //public string TitleE { get; set; }
        public String TitleUrl { get; set; }
        public String Url { get; set; }
        public String LongTime { get; set; }
        public string EditTime { get; set; }
        public Int32 PlayCount { get; set; }
        //public Boolean Enabled { get; set; }
        //public String VieldId { get; set; }
        public Int32? userId { get; set; }
        public Decimal? price { get; set; }
        public String isPass { get; set; }
        //public String UserName { get; set; }
        //public Int32? type { get; set; }
        //public String isUserPass { get; set; }
    }

    public class AppUploadAuthRes
    {
        public string RequestId { get; set; }
        public string VideoId { get; set; }
        public string UploadAddress { get; set; }
        public string UploadAuth { get; set; }
    }

    public class MyRankRes
    {
        public int order { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public string TotalKAL { get; set; }
        public string TotalKM { get; set; }
    }

    public class GetRankListRes
    {
        public List<GetRankList> rankList { get; set; }
        public int order { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public string TotalKAL { get; set; }
        public string TotalKM { get; set; }
    }

    public class GetRankList
    {
        public GetRankList() { }

        public int order { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public string TotalKAL { get; set; }
        public string TotalKM { get; set; }
        public int UserId { get; set; }
    }

    public class GetAuthReq
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string FileName { get; set; }
    }

    public class PlugFlowUrlRes
    {
        public string plugFlowUrl { get; set; }
        public int Id { get; set; }
    }


    public class GetInComeListRes
    {
        public GetInComeListRes(PayView view, int? isEnglish)
        {
            id = view.id;
            coachId = view.coachId;
            type = view.type;
            money = view.money;
            createTime = Convert.ToDateTime(view.createTime).ToString("yyyy-MM-dd HH:mm:ss");
            switch (type)
            {
                case 0:
                    typeName = isEnglish == 1 ? "Video revenue" : "视频收入";
                    break;
                case 1:
                    typeName = isEnglish == 1 ? "Live revenue" : "直播收入";
                    break;
                case 99:
                    typeName = isEnglish == 1 ? "Take cash" : "申请提现";
                    //TitleE = "Live revenue";
                    break;
            }

            //typeName = view.typeName;
        }

        public GetInComeListRes(PayView view)
        {
            id = view.id;
            coachId = view.coachId;
            type = view.type;
            money = view.money;
            createTime = Convert.ToDateTime(view.createTime).ToString("yyyy年MM月dd日 HH:mm:ss");
            typeName = view.typeName;
        }
        public Int32 id { get; set; }
        //public String outTradeNo { get; set; }
        public Int32? coachId { get; set; }
        //public Int32? userId { get; set; }
        public int? type { get; set; }
        public Decimal? money { get; set; }
        //public Int32? status { get; set; }
        public string createTime { get; set; }
        public String typeName { get; set; }
        //public string TitleE { get; set; }
    }

    public class GetSportMsgRes
    {
        public GetSportMsgRes(SportMsgView view)
        {
            id = view.id;
            createTime = Convert.ToDateTime(view.createTime).ToString("yyyy年MM月dd日");
            UserName = view.UserName;
            Url = StringHelper.Instance.GetApiUrl(view.Url);
            msg = view.msg;
        }
        public Int32? id { get; set; }
        public String createTime { get; set; }
        public String UserName { get; set; }
        public String Url { get; set; }
        public String msg { get; set; }
    }

    public class GetSportMsgReq : UserToken
    {
        [Required]
        public int? sportId { get; set; }
        [Required]
        public int? pageIndex { get; set; }
        public int? isEnglish { get; set; }
    }

    public class TakeCashRes
    {
        public decimal? balance { get; set; }
    }

    public class TakeCashReq : UserToken
    {
        [DecimalValidate(ErrorMessage = "金额错误")]
        public decimal? money { get; set; }
        [Required]
        public int? payType { get; set; }
        [Required]
        public string account { get; set; }
        [Required]
        public string accountName { get; set; }
        public int? isEnglish { get; set; }
    }

    public class GetInComeRecordReq : UserToken
    {
        public int? isEnglish { get; set; }
        [Required]
        public DateTime? date { get; set; }
        [Required]
        public int? pageIndex { get; set; }
    }

    public class UpdatePhoneReq : UserToken
    {
        [Required]
        public string phone { get; set; }
        [Required]
        public int? code { get; set; }
        public int? isEnglish { get; set; }
    }

    public class AddMsgReq : UserToken
    {
        [Required]
        public string msg { get; set; }
        [Required]
        public int? sportId { get; set; }
        public int? isEnglish { get; set; }
    }

    public class Flag
    {
        public int flag { set; get; }
        //public int? isEnglish { get; set; }
    }

    public class VideoFlag : Flag
    {
        public int videoId { get; set; }
        public decimal? price { get; set; }
        public int? isEnglish { get; set; }

    }

    public class LiveFlag : Flag
    {
        public int? isEnglish { get; set; }
        public int id { get; set; }
        public decimal? price { get; set; }
    }

    public class payRes
    {
        public int payType { get; set; }
        public string payStr { get; set; }
    }

    public class PayLiveReq : UserToken
    {
        public int? id { get; set; }
        public decimal? price { get; set; }
        /// <summary>
        /// 0支付宝 1微信
        /// </summary>
        public int? payType { get; set; }
        public string spbill_create_ip { get; set; }
        public int? isEnglish { get; set; }
    }

    public class IosPurchaseRes
    {
        public int flag { get; set; }
    }

    public class IosPurchaseReq
    {

    }

    public class PayVideoReq : UserToken
    {
        public int? videoId { get; set; }
        public decimal? price { get; set; }
        /// <summary>
        /// 0支付宝 1微信
        /// </summary>
        public int? payType { get; set; }
        public string spbill_create_ip { get; set; }
        public int? isEnglish { get; set; }
    }

    public class PayVideoTestReq : UserToken
    {
        public int? videoId { get; set; }
        public decimal? price { get; set; }
        public int? isEnglish { get; set; }
    }

    public class PayLiveTestReq : UserToken
    {
        public int? id { get; set; }
        public decimal? price { get; set; }
        public int? isEnglish { get; set; }
    }

    public class CheckLiveRes
    {
        public CheckLiveRes()
        {
            liveUrl = new List<string>();
            //videoInfo = new VideoInfo();
        }

        public CheckLiveRes(Sys_VideoInfo v)
        {
            liveUrl = new List<string>();
            //videoInfo = new VideoInfo();
            Id = v.Id;
            UserId = v.UserId;
            Url = StringHelper.Instance.GetApiUrl(v.Url);
            Title = v.Title;
            BeginTime = Convert.ToDateTime(v.BeginTime).ToString("yyyy-MM-dd HH:mm:ss");
            PlayLongTime = v.PlayLongTime;
            PlayStatus = v.PlayStatus;
            DataStatus = v.DataStatus;
            CloudId = v.CloudId;
            VideoId = v.VideoId;
            isPass = v.isPass;
            price = v.price;
        }
        public int flag { get; set; }
        public List<string> liveUrl { get; set; }
        public VideoInfo videoInfo { get; set; }

        public Int32 Id { get; set; }
        public Int32? UserId { get; set; }
        public String Url { get; set; }
        public String Title { get; set; }
        public string BeginTime { get; set; }
        public Int32? PlayLongTime { get; set; }
        public Int32? PlayStatus { get; set; }
        public Int32? DataStatus { get; set; }
        public String CloudId { get; set; }
        public Int32? VideoId { get; set; }
        public String isPass { get; set; }
        public Decimal? price { get; set; }
    }

    public class VideoInfo
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }

    public class UpLoadGymDataReq : UserToken
    {
        public int deviceId { get; set; }
        public decimal KM { get; set; }
        public int DRAG { get; set; }
        public decimal SD { get; set; }
        public int WATT { get; set; }
        public decimal KAL { get; set; }
        public int XL { get; set; }
        public string TotalTime { get; set; }
        public int? isEnglish { get; set; }
    }

    public class GymData
    {
        public GymData() { }

        public GymData(UpLoadGymDataReq req, int uId)
        {
            deviceId = req.deviceId;
            KM = req.KM;
            DRAG = req.DRAG;
            SD = req.SD;
            WATT = req.WATT;
            CAL = req.KAL;
            XL = req.XL;
            TotalTime = req.TotalTime;
            createTime = DateTime.Now;
            userId = uId;
        }
        public string Url { get; set; }
        public string UserName { get; set; }
        public int userId { get; set; }
        public int deviceId { get; set; }
        public decimal KM { get; set; }
        public int DRAG { get; set; }
        public decimal SD { get; set; }
        public int WATT { get; set; }
        public decimal CAL { get; set; }
        public int XL { get; set; }
        public string TotalTime { get; set; }
        public DateTime createTime { get; set; }
    }

    public class ContactDeviceRes
    {
        public int deviceId { get; set; }
    }

    public class ContactDeviceReq : UserToken
    {
        public int? isEnglish { get; set; }
        public string deviceName { get; set; }
    }

    public class IdReq : UserToken
    {
        [IntStringValidate]
        public int? Id { get; set; }
        public int? isEnglish { get; set; }
    }

    public class apkRes
    {
        public apkRes(AndroidVersion v)
        {
            versionCode = v.versionCode;
            versionName = v.versionName;
            apkFileUrl = v.apkFileUrl;
            updateLog = v.updateLog;
            targetSize = v.targetSize;
        }
        public string versionCode { get; set; }
        public string versionName { get; set; }
        public string apkFileUrl { get; set; }
        public string updateLog { get; set; }
        public string targetSize { get; set; }
    }

    public class LoadPlayVideoReq : UserToken
    {
        public int? isEnglish { get; set; }
        public int? pageIndex { get; set; }
    }

    public class GetRankListReq : UserToken
    {
        [DateTimeValidate]
        public DateTime? date { get; set; }
        [IntStringValidate]
        public int? pageIndex { get; set; }
        public int? isEnglish { get; set; }
    }

    public class UpLoadDataReq : UserToken
    {
        [DecimalValidate]
        public decimal? XL { get; set; }
        [DecimalValidate]
        public decimal? SD { get; set; }
        [DecimalValidate]
        public decimal? KAL { get; set; }
        [DecimalValidate]
        public decimal? KM { get; set; }
        [DecimalValidate]
        public decimal? ZS { get; set; }
        [DecimalValidate]
        public decimal? TotalKM { get; set; }
        [DecimalValidate]
        public decimal? WATT { get; set; }
        [MinuteSecond]
        public string Time { get; set; }
        public int? isEnglish { get; set; }
    }

    public class PageIndexReq : UserToken
    {
        [Required]
        public int? pageIndex { get; set; }
        public int? isEnglish { get; set; }
    }

    public class AddCourseReq : UserToken
    {
        public int? isEnglish { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string TitleUrl { get; set; }
        //[Required]
        //public string Url { get; set; }
        //[Required]
        //public string LongTime { get; set; }
        [Required]
        public string videoId { get; set; }
        [Required]
        public decimal? price { get; set; }
    }

    public class UserLoginRes
    {
        public UserLoginRes(Sys_User user, string Token, decimal km)
        {
            TotalKM = km;
            token = Token;
            Id = user.Id;
            Type = (int)user.Type;
            //UserPwd = user.UserPwd;
            UserName = user.UserName;
            if (user.Phone.Substring(0, 4) == "temp")
                Phone = "";
            else
                Phone = user.Phone;
            //UId = user.UId;
            UserExplain = user.UserExplain;
            Url = StringHelper.Instance.GetApiUrl(user.Url);

            if (user.Birthday == null)
                Birthday = "";
            else
                Birthday = Convert.ToDateTime(user.Birthday).ToString("yyyy-MM-dd");
            if ((bool)user.Sex)
                Sex = 1;
            else
                Sex = 0;
            Height = user.Height;
            Weight = user.Weight;
            IdealWeight = user.IdealWeight;
            RegisterTime = Convert.ToDateTime(user.RegisterTime).ToString("yyyy-MM-dd HH:mm:ss");
            LoginTime = user.LoginTime;
            Enabled = user.Enabled;
            UsePlace = user.UsePlace;
            Address = user.Address;
            frequency = user.frequency;
            KeyName = user.KeyName;
            //account = user.account;
            coachImg = user.coachImg;
            isPass = user.isPass ?? "";
            //livePrice = user.livePrice;
            qqUId = user.qqUId;
            wxUId = user.wxUId;
            fbUId = user.fbUId;
            ttUId = user.ttUId;
            balance = user.balance;
        }
        public decimal? balance { get; set; }
        public string token { get; set; }
        public Int32 Id { get; set; }
        public Int32 Type { get; set; }
        //public String UserPwd { get; set; }
        public String UserName { get; set; }
        public String Phone { get; set; }
        //public String UId { get; set; }
        public String UserExplain { get; set; }
        public String Url { get; set; }
        public string Birthday { get; set; }
        public int Sex { get; set; }
        public Decimal? Height { get; set; }
        public Decimal? Weight { get; set; }
        public Decimal? IdealWeight { get; set; }
        public string RegisterTime { get; set; }
        public DateTime? LoginTime { get; set; }
        public Boolean? Enabled { get; set; }
        public String UsePlace { get; set; }
        public String Address { get; set; }
        public String frequency { get; set; }
        public String KeyName { get; set; }
        //public String account { get; set; }
        public String coachImg { get; set; }
        public String isPass { get; set; }
        public Decimal? livePrice { get; set; }
        public String qqUId { get; set; }
        public String wxUId { get; set; }
        public String fbUId { get; set; }
        public String ttUId { get; set; }
        public decimal TotalKM { get; set; }
    }

    public class ModelListAllRes
    {
        public ModelListAllRes()
        {
            meters = new List<ModelListRes>();
            cals = new List<ModelListRes>();
            times = new List<ModelListRes>();
        }
        public List<ModelListRes> meters { get; set; }
        public List<ModelListRes> cals { get; set; }
        public List<ModelListRes> times { get; set; }
    }

    public class ModelListRes
    {
        public ModelListRes(DragModel dm, int isEnglish)
        {
            id = dm.id;
            modelName = isEnglish == 1 ? dm.modelNameE : dm.modelName;
            modelType = dm.modelType;
            switch (modelType)
            {
                case 0:
                    modelTypeName = "里程模式";
                    break;
                case 1:
                    modelTypeName = "卡路里模式";
                    break;
                case 2:
                    modelTypeName = "时间模式";
                    break;
            }

            listMN = JsonConvert.DeserializeObject<List<MN>>(dm.content);
        }

        public int id { get; set; }
        public string modelName { get; set; }
        //public int m { get; set; }
        //public int n { get; set; }
        public int modelType { get; set; }
        public string modelTypeName { get; set; }
        public List<MN> listMN { get; set; }

    }
    public class MN
    {
        public int m { get; set; }
        public int n { get; set; }
    }

}