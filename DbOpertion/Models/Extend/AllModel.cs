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
    public class AllModel
    {

        public class ActiveUser
        {
            public int userId { get; set; }
            public DateTime activeTime { get; set; }
        }

        public class AddSportTypeReq : English
        {
            [Required]
            public string Name { get; set; }

            public string Remark { get; set; }
            public string NameE { get; set; }
            public string RemarkE { get; set; }

        }

        public class GetUserListRes
        {
            public GetUserListRes(UserDataView user)
            {
                Enabled = user.Enabled;
                Id = user.Id;

                UserName = user.UserName;

                if (user.Sex)
                    Sex = "男";
                else
                    Sex = "女";
                Phone = user.Phone;
                if (Phone.Substring(0, 4) == "temp")
                    Phone = "";
                if (user.LoginTime != null)
                    LoginTime = Convert.ToDateTime(user.LoginTime).ToString("yyyy-MM-dd HH:mm:ss");
                else
                    LoginTime = "";
                UsePlace = user.UsePlace ?? "";
                frequency = user.frequency ?? "";
                KeyName = user.KeyName ?? "";
                allkm = user.TotalKM ?? 0;
                if (user.TotalTime != null)
                    allTime = StringHelper.Instance.GetTimeStr((int)user.TotalTime);//-txy
                else
                    allTime = "";
                allkal = user.TotalKal ?? 0;
                Address = user.Address ?? "";
            }

            public int Id { get; set; }
            public string UserName { get; set; }
            public string Sex { get; set; }
            public string Phone { get; set; }
            public string LoginTime { get; set; }
            public string UsePlace { get; set; }
            public string frequency { get; set; }
            public string KeyName { get; set; }
            public decimal? allkm { get; set; }
            public string allTime { get; set; }
            public decimal? allkal { get; set; }
            public string Address { get; set; }
            public bool? Enabled { get; set; }
        }

        public class UpdateParamSet
        {
            public String oldKey { get; set; }
            [Required(ErrorMessage = "请输入键")]
            public String key { get; set; }
            [Required(ErrorMessage = "请输入值")]
            public String value { get; set; }
            [Required(ErrorMessage = "请输入备注")]
            public String remark { get; set; }
            [Required(ErrorMessage = "请输入UUID")]
            public String uuid { get; set; }
            public int? type { get; set; }
        }

        public class UpdateLiveReq
        {
            public string Url { get; set; }
            public int? Id { get; set; }
            public string Title { get; set; }
            public string TitleE { get; set; }
        }

        public class AddParamSetReq
        {
            [Required(ErrorMessage = "请输入键")]
            public String key { get; set; }
            [Required(ErrorMessage = "请输入值")]
            public String value { get; set; }
            [Required(ErrorMessage = "请输入备注")]
            public String remark { get; set; }
            [Required(ErrorMessage = "请输入UUID")]
            public String uuid { get; set; }
            public int? type { get; set; }
        }

        public class StrRes
        {
            public string str { get; set; }
        }

        public class GetParamSetRes
        {

            public GetParamSetRes(Sys_ParamSet ps)
            {
                key = ps.key;
                value = ps.value;
                remark = ps.remark;
                uuid = ps.uuid;
                switch (ps.type)
                {
                    case 0:
                        type = "一体式电子表";
                        break;
                    case 1:
                        type = "分离式设备";
                        break;
                    case 2:
                        type = "控制一体电子表";
                        break;
                }
                //type =
            }
            public String key { get; set; }

            public String value { get; set; }

            public String remark { get; set; }

            public String uuid { get; set; }

            public string type { get; set; }
        }

        public class TestReq
        {
            public int n { get; set; }
            public string str { get; set; }
        }

        public class GetModelListRes
        {
            public GetModelListRes(DragModel model)
            {
                id = model.id;
                //m = model.m.ToString();
                switch (model.modelType)
                {
                    case 0:
                        modelType = "里程模式";
                        //m += "米";
                        break;
                    case 1:
                        modelType = "卡路里模式";
                        //m += "卡路里";
                        break;
                    case 2:
                        //m += "秒";
                        modelType = "时间模式";
                        break;
                }
                //n = model.n;
                modelName = model.modelName;
                modelNameE = model.modelNameE;
                //content = model.content;

            }
            public int id { get; set; }
            public string modelType { get; set; }
            //public string m { get; set; }
            //public string n { get; set; }
            public string modelName { get; set; }
            public string modelNameE { get; set; }
            //public string content { get; set; }
            //public string createTime { get; set; }
        }

        public class GetVideoListRes
        {
            public GetVideoListRes(videoView view)
            {
                Id = view.Id;
                Title = view.Title;
                TitleE = view.TitleE;
                //if (view.TitleUrl.Substring(0, 4) == "http")
                //    TitleUrl = view.TitleUrl;
                //else
                //    TitleUrl = "../" + view.TitleUrl;
                TitleUrl = StringHelper.Instance.GetWebUrl(view.TitleUrl);
                Url = view.Url;
                LongTime = view.LongTime;
                EditTime = view.EditTime.ToString("yyyy-MM-dd HH:mm:ss");
                PlayCount = view.PlayCount;
                Enabled = view.Enabled;
                VieldId = view.VieldId;
                price = view.price;
                userId = view.userId;
                if (userId == 0)
                    UserName = "平台";
                else
                    UserName = view.UserName;
                isPass = view.isPass;
            }

            public Int32 Id { get; set; }
            public String Title { get; set; }
            public string TitleE { get; set; }
            public String TitleUrl { get; set; }
            public String Url { get; set; }
            public String LongTime { get; set; }
            public string EditTime { get; set; }
            public Int32 PlayCount { get; set; }
            public Boolean Enabled { get; set; }
            public string VieldId { get; set; }
            public Int32? userId { get; set; }
            public Decimal? price { get; set; }
            public String UserName { get; set; }
            public string isPass { get; set; }
        }

        public class ServerUserItem
        {
            public ServerUserItem() { }
            public ServerUserItem(List<authView> vs, int gId)
            {
                var v = vs.First();
                Id = v.Id;
                //Url = v.Url;
                Url = StringHelper.Instance.GetWebUrl(v.Url);
                UserName = v.UserName;
                UId = v.UId;
                UserPwd = v.UserPwd;
                Phone = v.Phone;
                if (v.MenuId != null)
                    menuIds = vs.Select(p => (int)p.MenuId).ToArray();
                else
                    menuIds = new int[] { };
                isEnglish = v.isEnglish;
                gymId = gId;
            }
            public ServerUserItem(List<authView> vs)
            {
                var v = vs.First();
                Id = v.Id;
                //Url = v.Url;
                Url = StringHelper.Instance.GetWebUrl(v.Url);
                UserName = v.UserName;
                UId = v.UId;
                UserPwd = v.UserPwd;
                Phone = v.Phone;
                menuIds = vs.Select(p => (int)p.MenuId).ToArray();
                isEnglish = v.isEnglish;
            }
            public int? Id { get; set; }
            public string Url { get; set; }
            public string UserName { get; set; }
            public string UId { get; set; }
            public string UserPwd { get; set; }
            public string Phone { get; set; }
            /// <summary>
            /// 此处用作判断是不是健身房账号。健身房=1
            /// </summary>
            public int? isEnglish { get; set; }
            public int[] menuIds { get; set; }
            public int gymId { get; set; }
        }

        public class SetLiveVideoReq
        {
            public int? videoId { get; set; }
            public int? liveId { get; set; }
        }

        public class GetAddressReq
        {
            public string fileName { get; set; }
            public string type { get; set; }

        }

        public class AddVideoReq
        {
            public string videoId { get; set; }
            public string TitleUrl { get; set; }
            public string Title { get; set; }
            public string TitleE { get; set; }
            public string LongTime { get; set; }
            public string Url { get; set; }
            [DecimalValidate2]
            public decimal? price { get; set; }
        }

        public class AddAudioReq
        {
            public string Title { get; set; }
            public string LongTime { get; set; }
            public string Url { get; set; }
            public int? typeId { get; set; }
        }

        public class UpdateAudioReq
        {
            public int? Id { get; set; }
            [Required]
            public string Title { get; set; }
            [Required]
            public string LongTime { get; set; }
            [Required]
            public int? typeId { get; set; }
        }

        public class UpdateVideoReq
        {
            public int? Id { get; set; }
            public string TitleUrl { get; set; }
            [Required]
            public string Title { get; set; }
            public string TitleE { get; set; }
            [Required]
            public string LongTime { get; set; }
            public decimal? price { get; set; }
        }

        public class UpdateModelReq
        {
            [Required]
            public int? id { get; set; }
            [Required(ErrorMessage = "请输入模式名称")]
            public string modelName { get; set; }
            [Required(ErrorMessage = "请输入英文名称")]
            public string modelNameE { get; set; }
            [Required(ErrorMessage = "请选择模式类型")]
            public int? modelType { get; set; }
            //[IdNotZeroValidate(ErrorMessage = "模式变量错误")]
            //public int? m { get; set; }
            //[IdNotZeroValidate(ErrorMessage = "阻力变量错误")]
            //public int? n { get; set; }
            public string[] ms { get; set; }
            public string[] ns { get; set; }
        }

        public class AddModelReq
        {
            [Required(ErrorMessage = "请输入模式名称")]
            public string modelName { get; set; }
            [Required(ErrorMessage = "请输入英文名称")]
            public string modelNameE { get; set; }
            [Required(ErrorMessage = "请选择模式类型")]
            public int? modelType { get; set; }
            //[IdNotZeroValidate(ErrorMessage = "模式变量错误")]
            public int? m { get; set; }
            //[IdNotZeroValidate(ErrorMessage = "阻力变量错误")]
            public int? n { get; set; }
            public int? isEnglish { get; set; }

            public string[] ms { get; set; }
            public string[] ns { get; set; }
        }

        public class MN
        {
            public MN(string M, string N)
            {
                m = Convert.ToInt32(M);
                n = Convert.ToInt32(N);
            }
            public int m { get; set; }
            public int n { get; set; }
        }

        public class GetModelListReq
        {
            public string search { get; set; }
            [Required]
            public int? pageIndex { get; set; }
            [Required]
            public string orderField { get; set; }
            [Required]
            public bool? isDesc { get; set; }
            [Required]
            public int? size { get; set; }
            public int? modelType { get; set; }
            public int? isEnglish { get; set; }
        }

        public class UpdateUserReq
        {
            public int? Id { get; set; }
            public string Url { get; set; }
            [PwdValidate2(ErrorMessage = "密码应为6~16位英文字母、数字")]
            public string UserPwd { get; set; }
            [Required(ErrorMessage = "请输入名称")]
            public string UserName { get; set; }
            [DecimalValidate(ErrorMessage = "请输入身高")]
            public decimal? Height { get; set; }
            [Required(ErrorMessage = "请选择性别")]
            public bool? Sex { get; set; }
            [DecimalValidate(ErrorMessage = "请输入体重")]
            public decimal? Weight { get; set; }
            [Required(ErrorMessage = "请输入手机号")]
            public string Phone { get; set; }
            [Required(ErrorMessage = "请选择生日")]
            public DateTime? Birthday { get; set; }
            //[DecimalValidate(ErrorMessage = "请输入直播单价")]
            //public decimal? livePrice { get; set; }
        }

        public class UpdateCoachReq
        {
            public int? Id { get; set; }
            public string Url { get; set; }
            //[Required(ErrorMessage = "请输入账号")]
            //public string account { get; set; }
            //[Required(ErrorMessage = "请输入密码")]
            public string UserPwd { get; set; }
            [Required(ErrorMessage = "请输入名称")]
            public string UserName { get; set; }
            [DecimalValidate(ErrorMessage = "请输入身高")]
            public decimal? Height { get; set; }
            [Required(ErrorMessage = "请选择性别")]
            public bool? Sex { get; set; }
            [DecimalValidate(ErrorMessage = "请输入体重")]
            public decimal? Weight { get; set; }
            [Required(ErrorMessage = "请输入手机号")]
            public string Phone { get; set; }
            [Required(ErrorMessage = "请选择生日")]
            public DateTime? Birthday { get; set; }
            [DecimalValidate(ErrorMessage = "请输入直播单价")]
            public decimal? livePrice { get; set; }
        }

        public class AddCoachReq : English
        {
            public string Url { get; set; }
            //[Required(ErrorMessage = "请输入账号")]
            //public string account { get; set; }
            [PwdValidate(ErrorMessage = "密码应为6~16位英文字母、数字")]
            public string UserPwd { get; set; }
            [Required(ErrorMessage = "请输入名称")]
            public string UserName { get; set; }
            [DecimalValidate(ErrorMessage = "请输入身高")]
            public decimal? Height { get; set; }
            [Required(ErrorMessage = "请选择性别")]
            public bool? Sex { get; set; }
            [DecimalValidate(ErrorMessage = "请输入体重")]
            public decimal? Weight { get; set; }
            [Required(ErrorMessage = "请输入手机号")]
            public string Phone { get; set; }
            [Required(ErrorMessage = "请选择生日")]
            public DateTime? Birthday { get; set; }
            [DecimalValidate(ErrorMessage = "请输入直播单价")]
            public decimal? livePrice { get; set; }

        }

        public class ToggleCoachReq
        {
            [Required]
            public int? Id { get; set; }
            [Required]
            public string isPass { get; set; }
        }

        public class ToggleVideoReq
        {
            [Required]
            public int? Id { get; set; }
            [Required]
            public bool? Enabled { get; set; }
        }

        public class GetCoachListRes
        {
            public GetCoachListRes(Sys_User user)
            {
                Id = user.Id;
                UserName = user.UserName;
                //Phone = user.Phone;
                Phone = user.Phone;
                if (Phone.Substring(0, 4) == "temp")
                    Phone = "";
                UserExplain = user.UserExplain;
                Url = user.Url;
                Sex = (bool)user.Sex ? "男" : "女";
                LoginTime = user.LoginTime == null ? "" : Convert.ToDateTime(user.LoginTime).ToString("yyyy-MM-dd HH:mm:ss");
                Enabled = user.Enabled;
                account = user.account;
                isPass = user.isPass;
            }
            public Int32 Id { get; set; }
            public String UserName { get; set; }
            public String Phone { get; set; }
            public String UserExplain { get; set; }
            public String Url { get; set; }
            public string Sex { get; set; }
            public string LoginTime { get; set; }
            public Boolean? Enabled { get; set; }
            public String isPass { get; set; }
            public String account { get; set; }
        }

        public class GetLiveListRes
        {
            public GetLiveListRes(LiveView live)
            {
                TitleE = live.TitleE;
                Id = live.Id;
                UserId = live.UserId;
                //Url = "../" + live.Url;
                Url = StringHelper.Instance.GetWebUrl(live.Url);
                Title = live.Title;
                BeginTime = live.BeginTime.ToString("yyyy-MM-dd HH:mm:ss");
                PlayLongTime = live.PlayLongTime;
                switch (live.PlayStatus)
                {
                    case 0:
                        PlayStatus = "未开始";
                        break;
                    case 1:
                        PlayStatus = "直播中";
                        break;
                    case 2:
                        PlayStatus = "已暂停";
                        break;
                    case 3:
                        PlayStatus = "已结束";
                        break;
                    case 4:
                        PlayStatus = "已失效";
                        break;
                }

                //PlayStatus = live.PlayStatus;
                DataStatus = live.DataStatus;
                CloudId = live.CloudId;
                VideoId = live.VideoId;
                isPass = live.isPass;
                //price = live.price;
                UserName = live.UserName;
                price = live.price;
                videoTitle = live.videoTitle;
            }
            public decimal? price { get; set; }
            public Int32 Id { get; set; }
            public Int32 UserId { get; set; }
            public String Url { get; set; }
            public String Title { get; set; }
            public string TitleE { get; set; }
            public string BeginTime { get; set; }
            public Int32 PlayLongTime { get; set; }
            public string PlayStatus { get; set; }
            public Int32 DataStatus { get; set; }
            public String CloudId { get; set; }
            public Int32? VideoId { get; set; }
            public string isPass { get; set; }
            //public Decimal? price { get; set; }
            public String UserName { get; set; }
            public string videoTitle { get; set; }
        }

        public class GetAudioListRes
        {
            public GetAudioListRes(AudioView pa)
            {
                Id = pa.Id;
                Title = pa.Title;
                TypeId = pa.TypeId;
                Url = pa.Url;
                LongTime = pa.LongTime;
                EditTime = pa.EditTime.ToString("yyyy-MM-dd HH:mm:ss");
                PlayCount = pa.PlayCount;
                Enabled = pa.Enabled;
                typeName = pa.typeName;
            }
            public string typeName { get; set; }
            public Int32 Id { get; set; }
            public String Title { get; set; }
            public Int32 TypeId { get; set; }
            public String Url { get; set; }
            public String LongTime { get; set; }
            public string EditTime { get; set; }
            public Int32? PlayCount { get; set; }
            public Boolean? Enabled { get; set; }
        }

        public class GetSportTypeListRes
        {
            public GetSportTypeListRes(Sys_SportType type)
            {
                Id = type.Id;
                Name = type.Name;
                Remark = type.Remark;
                Enabled = type.Enabled;
                NameE = type.NameE;
                RemarkE = type.RemarkE;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string NameE { get; set; }
            public string Remark { get; set; }
            public string RemarkE { get; set; }
            public bool? Enabled { get; set; }
        }

        public class GetAudioTypeListRes
        {
            public GetAudioTypeListRes(Sys_AudioType type)
            {
                Id = type.Id;
                Title = type.Title;
                TitleUrl = StringHelper.Instance.GetWebUrl(type.TitleUrl);
                Remark = type.Remark;
                Enabled = type.Enabled;
                TitleE = type.TitleE;
                RemarkE = type.RemarkE;
            }
            public Int32 Id { get; set; }
            public String Title { get; set; }
            public string TitleE { get; set; }
            public String TitleUrl { get; set; }
            public String Remark { get; set; }
            public string RemarkE { get; set; }
            public Boolean? Enabled { get; set; }
        }

        public class GetAudioTypeByIdRes
        {
            public GetAudioTypeByIdRes(Sys_AudioType at)
            {
                id = at.Id;
                Title = at.Title;
                TitleUrl = StringHelper.Instance.GetWebUrl(at.TitleUrl);
                Remark = at.Remark;
                TitleE = at.TitleE;
                RemarkE = at.RemarkE;
            }
            public int id { get; set; }
            public string Title { get; set; }
            public string TitleE { get; set; }
            public string TitleUrl { get; set; }
            public string Remark { get; set; }
            public string RemarkE { get; set; }
        }

        public class UpdateAudioTypeReq
        {
            public int? Id { get; set; }
            [Required(ErrorMessage = "请输入标题")]
            public string Title { get; set; }
            public string TitleE { get; set; }
            public string TitleUrl { get; set; }
            public string Remark { get; set; }
            public string RemarkE { get; set; }
        }

        public class AddAudioTypeReq : English
        {
            [Required(ErrorMessage = "请输入标题")]
            public string Title { get; set; }
            public string TitleE { get; set; }
            public string TitleUrl { get; set; }
            public string Remark { get; set; }
            public string RemarkE { get; set; }
        }

        public class GetVideoListByCoachIdReq
        {
            public int? id { get; set; }
            public string search { get; set; }
            [Required]
            public int? pageIndex { get; set; }
            [Required]
            public string orderField { get; set; }
            [Required]
            public bool? isDesc { get; set; }
            [Required]
            public int? size { get; set; }
        }

        public class GetVideoForLiveReq
        {
            public string search { get; set; }
            [Required]
            public int? pageIndex { get; set; }
            [Required]
            public string orderField { get; set; }
            [Required]
            public bool? isDesc { get; set; }
            //[Required]
            //public int? size { get; set; }
            public int? userId { get; set; }
        }

        public class GetVideoListReq
        {
            public string search { get; set; }
            [Required]
            public int? pageIndex { get; set; }
            [Required]
            public string orderField { get; set; }
            [Required]
            public bool? isDesc { get; set; }
            [Required]
            public int? size { get; set; }
            public int? userId { get; set; }
        }

        public class GetSportListRes
        {
            public GetSportListRes(SportView view)
            {
                Id = view.Id;
                Title = view.Title;
                TitleUrl = StringHelper.Instance.GetWebUrl(view.TitleUrl);
                TypeId = view.TypeId;
                CreateTime = view.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                Content = view.Content;
                Remark = view.Remark;
                Enabled = view.Enabled;
                DataType = view.DataType;
                typeName = view.typeName;
                TitleE = view.TitleE;
                RemarkE = view.RemarkE;
            }
            public Int32 Id { get; set; }
            public String Title { get; set; }
            public string TitleE { get; set; }
            public String TitleUrl { get; set; }
            public Int32 TypeId { get; set; }
            public string CreateTime { get; set; }
            public String Content { get; set; }
            public String Remark { get; set; }
            public string RemarkE { get; set; }
            public Boolean Enabled { get; set; }
            public Int32 DataType { get; set; }
            public String typeName { get; set; }
        }

        public class GetSportListReq
        {
            public string search { get; set; }
            [Required]
            public int? pageIndex { get; set; }
            [Required]
            public string orderField { get; set; }
            [Required]
            public bool? isDesc { get; set; }
            [Required]
            public int? size { get; set; }
            public int? TypeId { get; set; }
        }

        public class GetAudioListReq
        {
            public string search { get; set; }
            [Required]
            public int? pageIndex { get; set; }
            [Required]
            public string orderField { get; set; }
            [Required]
            public bool? isDesc { get; set; }
            [Required]
            public int? size { get; set; }
            public int? TypeId { get; set; }
        }

        public class GetSportMsgListRes
        {
            public GetSportMsgListRes(SportMsgView view)
            {
                //id = view.id;
                //sportId = view.sportId;
                createTime = Convert.ToDateTime(view.createTime).ToString("yyyy-MM-dd HH:mm:ss");
                UserName = view.UserName;
                //Url = view.Url;
                //TitleUrl = view.TitleUrl;
                Title = view.Title;
                //Content = view.Content;
                msg = view.msg;
            }
            //public Int32? id { get; set; }
            //public Int32? sportId { get; set; }
            public string createTime { get; set; }
            public String UserName { get; set; }
            //public String Url { get; set; }
            //public String TitleUrl { get; set; }
            public String Title { get; set; }
            //public String Content { get; set; }
            public String msg { get; set; }
        }

        public class GetSportMsgListReq
        {
            public string search { get; set; }
            [Required]
            public int? pageIndex { get; set; }
            [Required]
            public string orderField { get; set; }
            [Required]
            public bool? isDesc { get; set; }
            [Required]
            public int? size { get; set; }
            public int? isEnglish { get; set; }
        }

        public class GetFeedBackListRes
        {
            public GetFeedBackListRes(FeedBackView view)
            {
                Id = view.Id;
                Title = view.Title;
                Content = view.Content;
                EditTime = Convert.ToDateTime(view.EditTime).ToString("yyyy-MM-dd HH:mm:ss");
                reply = view.reply;
                username = view.username;
                //EditorId = view.EditorId;
            }
            public Int32 Id { get; set; }
            public String Title { get; set; }
            public String Content { get; set; }
            //public Int32 EditorId { get; set; }
            public string EditTime { get; set; }
            public String reply { get; set; }
            public String username { get; set; }
        }

        public class ReplyReq
        {
            public int? Id { get; set; }
            public string reply { get; set; }
        }

        public class TakeCashListRes
        {
            public TakeCashListRes(PayView view)
            {
                id = view.id;
                //outTradeNo = view.outTradeNo;
                coachId = view.coachId;
                //userId = view.userId;
                //type = view.type;
                money = view.money;
                status = view.status;
                if (view.status == 1)
                    statusMsg = "已提现";
                else
                    statusMsg = "待审核";
                createTime = Convert.ToDateTime(view.createTime).ToString("yyyy-MM-dd HH:mm:ss");
                account = view.account;
                payType = view.payType;
                payTypeName = view.payTypeName;
                //typeName = view.typeName;
                coachName = view.coachName;
                accountName = view.accountName;
            }

            public Int32 id { get; set; }
            //public String outTradeNo { get; set; }
            public Int32? coachId { get; set; }
            //public Int32? userId { get; set; }
            //public Int32? type { get; set; }
            public Decimal? money { get; set; }
            public Int32? status { get; set; }
            public string statusMsg { get; set; }
            public string createTime { get; set; }
            public String account { get; set; }
            public string accountName { get; set; }
            public Int32? payType { get; set; }
            public String payTypeName { get; set; }
            //public String typeName { get; set; }
            public String coachName { get; set; }
        }

        public class GetGymListRes
        {
            public GetGymListRes(GymView g)
            {
                id = g.id;
                name = g.name;
                nameE = g.nameE;
                UId = g.UId;
            }

            public GetGymListRes(Gym g)
            {
                id = g.id;
                name = g.name;
                nameE = g.nameE;
            }
            public int id { get; set; }
            public string name { get; set; }
            public string nameE { get; set; }
            public string UId { get; set; }
        }

        public class GetMoneyDetailListRes
        {
            public GetMoneyDetailListRes(PayView view)
            {
                id = view.id;
                outTradeNo = view.outTradeNo;
                coachId = view.coachId;
                userId = view.userId;
                switch (view.type)
                {
                    case 0:
                        type = "课程付费";
                        break;
                    case 1:
                        type = "直播付费";
                        break;
                    case 99:
                        type = "申请提现";
                        break;
                }
                //type = view.type;
                money = view.money;
                status = view.status;
                createTime = Convert.ToDateTime(view.createTime).ToString("yyyy-MM-dd HH:mm:ss");
                account = view.account;
                if (view.payType == 0)
                    payType = "支付宝";
                else
                    payType = "微信";
                payTypeName = view.payTypeName;
                typeName = view.typeName;
                coachName = view.coachName;
                payUserName = view.payUserName;
            }
            public Int32 id { get; set; }
            public String outTradeNo { get; set; }
            public Int32? coachId { get; set; }
            public Int32? userId { get; set; }
            public string type { get; set; }
            public Decimal? money { get; set; }
            public Int32? status { get; set; }
            public string createTime { get; set; }
            public String account { get; set; }
            public string payType { get; set; }
            public String payTypeName { get; set; }
            public String typeName { get; set; }
            public String coachName { get; set; }
            public string payUserName { get; set; }
        }

        public class GetUserDataRes
        {
            public GetUserDataRes()
            {
                x = "";
                km = 0;
                cal = 0;
                tkm = 0;
            }

            public GetUserDataRes(Sys_Data data)
            {
                x = StringHelper.Instance.GetIntStringWithZero(Convert.ToDateTime(data.CreateTime).Hour.ToString(), 2);
                km = data.KM;
                cal = data.KAL;
                tkm = data.TotalKM;
            }

            public string x { get; set; }
            public decimal? km { get; set; }
            public decimal? cal { get; set; }
            public decimal? tkm { get; set; }
        }

        public class GetUserDataReq
        {
            public int? userId { get; set; }
            public DateTime? date { get; set; }
            /// <summary>
            /// 0:km  1:cal  2:总km
            /// </summary>
            public int? type { get; set; }
        }

        public class GetData3Res
        {
            public int? gymCount { get; set; }
            public int? userOnLineCount { get; set; }
            public int? userCount { get; set; }
        }

        public class UpdateVersionReq
        {
            public int? id { get; set; }
            public string versionCode { get; set; }
            public string versionName { get; set; }
            public string updateLog { get; set; }
        }

        public class SetPwdReq
        {
            [Required(ErrorMessage = "请输入原密码")]
            public string spwd { get; set; }
            [Required(ErrorMessage = "请输入新密码")]
            public string newpwd { get; set; }
            [Required(ErrorMessage = "请输入确认密码")]
            public string newpwd2 { get; set; }
            public int? userId { get; set; }
        }

        public class AddNewVersionReq
        {
            public string versionName { get; set; }
            public string versionCode { get; set; }
            public string updateLog { get; set; }
            public HttpPostedFileBase file { get; set; }
        }

        public class GetVersionListRes
        {
            public GetVersionListRes(AndroidVersion v)
            {
                id = v.id;
                versionCode = v.versionCode;
                versionName = v.versionName;
                apkFileUrl = v.apkFileUrl;
                updateLog = v.updateLog;
                targetSize = v.targetSize;
                editTime = Convert.ToDateTime(v.editTime).ToString("yyyy-MM-dd HH:mm:ss");
            }
            public Int32 id { get; set; }
            public String versionCode { get; set; }
            public String versionName { get; set; }
            public String apkFileUrl { get; set; }
            public String updateLog { get; set; }
            public String targetSize { get; set; }
            public string editTime { get; set; }
        }

        public class UpdateGymReq
        {
            public int? id { get; set; }
            [Required(ErrorMessage = "请输入名称")]
            public string name { get; set; }
            public string nameE { get; set; }
            [Required(ErrorMessage = "请输入账号")]
            public string UserName { get; set; }
            public string UserPwd { get; set; }
        }

        public class AddGymReq
        {
            [Required(ErrorMessage = "请输入名称")]
            public string name { get; set; }
            //[Required(ErrorMessage = "请输入英文名称")]
            public string nameE { get; set; }
            [Required(ErrorMessage = "请输入账号")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "请输入密码")]
            public string UserPwd { get; set; }
        }

        public class UpdateDeviceReq
        {
            public int? id { get; set; }
            public string name { get; set; }
            public int? gymId { get; set; }
        }

        public class AddDeviceReq
        {
            public string name { get; set; }
            public int? gymId { get; set; }
        }

        public class DeviceDataRes
        {
            public DeviceDataRes(DeviceGym d)
            {
                id = d.id;
                deviceName = d.deviceName;
                gymId = d.gymId;
                UserId = d.UserId;
                CreateTime = Convert.ToDateTime(d.CreateTime).ToString("yyyy-MM-dd HH:mm:ss");
                XL = d.XL;
                SD = d.SD;
                KAL = d.KAL;
                KM = d.KM;
                TotalKM = d.TotalKM;
                ZS = d.ZS;
                Time = d.Time;
                TotalTime = d.TotalTime;
                TotalKAL = d.TotalKAL;
                WATT = d.WATT;
                UserName = d.UserName;
                Url = StringHelper.Instance.GetWebUrl(d.Url);
                ThisTime = "12:34";
            }
            public int orderNo { get; set; }
            public Int32 id { get; set; }
            public String deviceName { get; set; }
            public Int32? gymId { get; set; }
            public Int32? UserId { get; set; }
            public string CreateTime { get; set; }
            public Decimal? XL { get; set; }
            public Decimal? SD { get; set; }
            public Decimal? KAL { get; set; }
            public Decimal? KM { get; set; }
            public Decimal? TotalKM { get; set; }
            public Decimal? ZS { get; set; }
            public String Time { get; set; }
            public Int32? TotalTime { get; set; }
            public Decimal? TotalKAL { get; set; }
            public Decimal? WATT { get; set; }
            public String UserName { get; set; }
            public String Url { get; set; }
            /// <summary>
            /// 本次健身的时间。00：00
            /// </summary>
            public string ThisTime { get; set; }
        }

        public class LoginReq
        {
            public string UId { get; set; }
            public string pwd { get; set; }
        }

        public class ServerUserReq
        {
            public ServerUserReq(List<authView> vs)
            {
                var v = vs.First();
                Id = v.Id;
                Type = v.Type;
                UserName = v.UserName;
                Phone = v.Phone;
                UId = v.UId;
                Url = v.Url;
                Enabled = v.Enabled;
                MenuId = v.MenuId;

                var mNames = vs.Select(p => p.menuName).ToArray();
                menuNames = StringHelper.Instance.ArrJoin(mNames);
                //menuName = v.menuName;
            }
            public Int32 Id { get; set; }
            public Int32 Type { get; set; }
            //public String UserPwd { get; set; }
            public String UserName { get; set; }
            public String Phone { get; set; }
            public String UId { get; set; }
            //public String UserExplain { get; set; }
            public String Url { get; set; }
            //public DateTime Birthday { get; set; }
            //public Boolean Sex { get; set; }
            //public Decimal? Height { get; set; }
            //public Decimal? Weight { get; set; }
            //public Decimal? IdealWeight { get; set; }
            //public DateTime RegisterTime { get; set; }
            //public DateTime? LoginTime { get; set; }
            public Boolean? Enabled { get; set; }
            //public String UsePlace { get; set; }
            //public String Address { get; set; }
            //public String frequency { get; set; }
            //public String KeyName { get; set; }
            //public String account { get; set; }
            //public String coachImg { get; set; }
            //public String isPass { get; set; }
            //public String qqUId { get; set; }
            //public String wxUId { get; set; }
            //public String fbUId { get; set; }
            //public String ttUId { get; set; }
            //public String idCardFront { get; set; }
            //public String idCardBack { get; set; }
            //public Decimal? balance { get; set; }
            //public Int32? isEnglish { get; set; }
            public Int32? MenuId { get; set; }
            public String menuNames { get; set; }
        }

        public class UpdateServerUserReq
        {
            public int? Id { get; set; }
            public string Url { get; set; }
            [Required(ErrorMessage = "请输入账号")]
            public string UId { get; set; }
            //[Required(ErrorMessage = "请输入密码")]
            public string UserPwd { get; set; }
            [Required(ErrorMessage = "请输入姓名")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "请输入手机号")]
            public string Phone { get; set; }
            public int[] menuIds { get; set; }
        }

        public class AddServerUserReq
        {
            public string Url { get; set; }
            [Required(ErrorMessage = "请输入账号")]
            public string UId { get; set; }
            [Required(ErrorMessage = "请输入密码")]
            public string UserPwd { get; set; }
            [Required(ErrorMessage = "请输入姓名")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "请输入手机号")]
            public string Phone { get; set; }
            public int[] menuIds { get; set; }
        }

        public class GetGymDataAllRes
        {
            public GetGymDataAllRes() { }

            public GetGymDataAllRes(GymData req)
            {
                deviceId = req.deviceId;
                KM = req.KM;
                DRAG = req.DRAG;
                SD = req.SD;
                WATT = req.WATT;
                CAL = req.CAL;
                XL = req.XL;
                TotalTime = req.TotalTime;
                if (req.createTime != null)
                    createTime = req.createTime.ToString("yyyy-MM-dd HH:mm:ss");
                //userId = uId;
                Url = StringHelper.Instance.GetWebUrl(req.Url);
                UserName = req.UserName;
                //Url = StringHelper.Instance.GetWebUrl(url);
            }
            //public int userId { get; set; }
            public int order { get; set; }
            public string Url { get; set; }
            public string UserName { get; set; }
            public int deviceId { get; set; }
            public decimal KM { get; set; }
            public int DRAG { get; set; }
            public decimal SD { get; set; }
            public int WATT { get; set; }
            public decimal CAL { get; set; }
            public int XL { get; set; }
            public string TotalTime { get; set; }
            public string createTime { get; set; }
        }

        public class GetListCommonReq
        {
            [Required]
            public int? pageIndex { get; set; }
            [Required]
            public string orderField { get; set; }
            [Required]
            public bool? isDesc { get; set; }
            [Required]
            public int? size { get; set; }
            public string search { get; set; }

            //public int? isEnglish { set; get; }
            public int? gymId { get; set; }
            public DateTime? date { get; set; }
            public int? modelType { get; set; }
            public int? userId { get; set; }
            public int? TypeId { get; set; }
            public int? id { get; set; }
            //public int? Id { get; set; }
        }

        public class GetLiveListByIdReq
        {
            public int? Id { get; set; }
            public string search { get; set; }
            [Required]
            public int? pageIndex { get; set; }
            [Required]
            public string orderField { get; set; }
            [Required]
            public bool? isDesc { get; set; }
            [Required]
            public int? size { get; set; }
        }

        public class GetUserByIdRes
        {
            public GetUserByIdRes(Sys_User user)
            {
                Id = user.Id;
                UserName = user.UserName;
                Phone = user.Phone;
                if (user.Birthday != null)
                    Birthday = Convert.ToDateTime(user.Birthday).ToString("yyyy-MM-dd");
                else
                    Birthday = "";
                Sex = (bool)user.Sex;
                Height = user.Height;
                Weight = user.Weight;
                if (user.RegisterTime != null)
                    RegisterTime = Convert.ToDateTime(user.RegisterTime).ToString("yyyy-MM-dd HH:mm:ss");
                else
                    RegisterTime = "";
                if (user.LoginTime != null)
                    LoginTime = Convert.ToDateTime(user.LoginTime).ToString("yyyy-MM-dd HH:mm:ss");
                else
                    LoginTime = "";
                Url = StringHelper.Instance.GetWebUrl(user.Url);
                if (user.Weight != null && user.Height != null)
                    BMI = StringHelper.Instance.GetBMI((int)user.Weight, (int)user.Height);
                else
                    BMI = "";
                fhr = StringHelper.Instance.GetFHR(Convert.ToDateTime(user.Birthday));
                account = user.account;
            }
            public string BMI { get; set; }
            /// <summary>
            /// 燃脂心率
            /// </summary>
            public string fhr { get; set; }
            public Int32 Id { get; set; }
            //public Int32 Type { get; set; }
            //public String UserPwd { get; set; }
            public String UserName { get; set; }
            public String Phone { get; set; }
            //public String UId { get; set; }
            //public String UserExplain { get; set; }
            public String Url { get; set; }
            public string Birthday { get; set; }
            public Boolean Sex { get; set; }
            public Decimal? Height { get; set; }
            public Decimal? Weight { get; set; }
            //public Decimal? IdealWeight { get; set; }
            public string RegisterTime { get; set; }
            public string LoginTime { get; set; }
            public string account { get; set; }
            //public Boolean? Enabled { get; set; }
            //public String UsePlace { get; set; }
            //public String Address { get; set; }
            //public String frequency { get; set; }
            //public String KeyName { get; set; }
            //public Decimal? allkm { get; set; }
            //public String allTime { get; set; }
            //public Decimal? allkal { get; set; }
        }

        public class GetCoachItemRes
        {
            public GetCoachItemRes() { }
            public GetCoachItemRes(Sys_User coach)
            {
                Id = coach.Id;
                UserName = coach.UserName;
                Phone = coach.Phone;
                if (coach.Birthday != null)
                    Birthday = Convert.ToDateTime(coach.Birthday).ToString("yyyy-MM-dd");
                else
                    Birthday = "";
                Sex = (bool)coach.Sex;
                Height = coach.Height;
                Weight = coach.Weight;
                RegisterTime = Convert.ToDateTime(coach.RegisterTime);
                LoginTime = coach.LoginTime == null ? "" : Convert.ToDateTime(coach.LoginTime).ToString("yyyy-MM-dd");
                Url = StringHelper.Instance.GetWebUrl(coach.Url);
                if (coach.Weight != null && coach.Height != null)
                    BMI = StringHelper.Instance.GetBMI((int)coach.Weight, (int)coach.Height);
                else
                    BMI = "";
                fhr = StringHelper.Instance.GetFHR(Convert.ToDateTime(coach.Birthday));
                account = coach.account;
                //livePrice = (decimal)coach.livePrice;
                coachImg = "../" + coach.coachImg;
                idCardFront = "../" + coach.idCardFront;
                idCardBack = "../" + coach.idCardBack;
                isPass = coach.isPass == "0" ? "待审核" : coach.isPass;
                isEnglish = coach.isEnglish;
            }
            public string BMI { get; set; }
            /// <summary>
            /// 燃脂心率
            /// </summary>
            public string fhr { get; set; }
            public Int32 Id { get; set; }
            //public Int32 Type { get; set; }
            //public String UserPwd { get; set; }
            public String UserName { get; set; }
            public String Phone { get; set; }
            //public String UId { get; set; }
            //public String UserExplain { get; set; }
            public String Url { get; set; }
            public string Birthday { get; set; }
            public Boolean Sex { get; set; }
            public Decimal? Height { get; set; }
            public Decimal? Weight { get; set; }
            //public Decimal? IdealWeight { get; set; }
            public DateTime RegisterTime { get; set; }
            public string LoginTime { get; set; }
            public string account { get; set; }
            public decimal livePrice { get; set; }
            //public Boolean? Enabled { get; set; }
            //public String UsePlace { get; set; }
            //public String Address { get; set; }
            //public String frequency { get; set; }
            //public String KeyName { get; set; }
            //public Decimal? allkm { get; set; }
            //public String allTime { get; set; }
            //public Decimal? allkal { get; set; }
            public String coachImg { get; set; }
            public String idCardFront { get; set; }
            public String idCardBack { get; set; }
            public string isPass { get; set; }
            public int? isEnglish { get; set; }
        }

        public class AddLiveReq : English
        {
            [Required]
            public int? userId { get; set; }
            public string Url { get; set; }
            [Required(ErrorMessage = "请输入标题")]
            public string Title { get; set; }
            [DateTimeValidate(ErrorMessage = "开始时间错误")]
            public DateTime? BeginTime { get; set; }
            [IntStringValidate(ErrorMessage = "时长错误")]
            public int? PlayLongTime { get; set; }
            [DecimalValidate(ErrorMessage = "价格错误")]
            public decimal? price { get; set; }
        }

        public class UpdateAgreementReq
        {
            public string Content { get; set; }
            public string[] imgNames { get; set; }
        }



        public class UpdateSportReq
        {
            public int? id { get; set; }
            public string TitleUrl { get; set; }
            public string Title { get; set; }
            public string TitleE { get; set; }
            public string Remark { get; set; }
            public string RemarkE { get; set; }
            public int? TypeId { get; set; }
            public string Content { get; set; }
            public string ContentE { get; set; }
            public string[] imgNames { get; set; }
            public string[] imgNamesE { get; set; }
        }

        /// <summary>
        /// js验证了
        /// </summary>
        public class AddSportReq
        {
            public string TitleUrl { get; set; }
            public string Title { get; set; }
            public string TitleE { get; set; }
            public string Remark { get; set; }
            public string RemarkE { get; set; }
            public int? TypeId { get; set; }
            public string Content { get; set; }
            public string ContentE { get; set; }
            public string[] imgNames { get; set; }
            public string[] imgNamesE { get; set; }
        }

        public class IdReq
        {
            [Required]
            public int? Id { get; set; }
        }

        public class GetSportByIdRes
        {
            public GetSportByIdRes(Sys_Sport ss)
            {
                Id = ss.Id;
                TitleUrl = StringHelper.Instance.GetWebUrl(ss.TitleUrl);
                Title = ss.Title;
                Remark = ss.Remark;
                TypeId = ss.TypeId;
                Content = ss.Content;
                TitleE = ss.TitleE;
                RemarkE = ss.RemarkE;
                ContentE = ss.ContentE;
            }
            public int Id { get; set; }
            public string TitleUrl { get; set; }
            public string Title { get; set; }
            public string TitleE { get; set; }
            public string Remark { get; set; }
            public string RemarkE { get; set; }
            public int? TypeId { get; set; }
            public string Content { get; set; }
            public string ContentE { get; set; }
        }

        public class NotPassReq
        {
            [Required]
            public int? Id { get; set; }
            [Required(ErrorMessage = "请输入驳回理由")]
            public string isPass { get; set; }
        }

        public class GetAudioByIdRes
        {
            public GetAudioByIdRes(AudioView pa)
            {
                id = pa.Id;
                title = pa.Title;
                typeId = pa.TypeId;
                longTime = pa.LongTime;
            }
            public int id { get; set; }
            public string title { get; set; }
            public int typeId { get; set; }
            public string longTime { get; set; }
        }

        public class GetVideoByIdRes
        {
            public GetVideoByIdRes(Sys_PlayVideo pv)
            {
                Id = pv.Id;
                TitleUrl = StringHelper.Instance.GetWebUrl(pv.TitleUrl);
                Title = pv.Title;
                LongTime = pv.LongTime;
                price = pv.price;
                TitleE = pv.TitleE;
            }
            public int? Id { get; set; }
            public string TitleUrl { get; set; }
            public string Title { get; set; }
            public string TitleE { get; set; }
            public string LongTime { get; set; }
            public decimal? price { get; set; }
        }

        public class GetModelByIdRes
        {
            public GetModelByIdRes(DragModel dm)
            {
                id = dm.id;
                modelName = dm.modelName;
                modelNameE = dm.modelNameE;
                modelType = dm.modelType;
                //m = dm.m;
                //n = dm.n;
                listMN = JsonConvert.DeserializeObject<List<MN>>(dm.content);
            }

            public int id { get; set; }
            public string modelName { get; set; }
            public string modelNameE { get; set; }
            public int modelType { get; set; }
            public List<MN> listMN { get; set; }
            //public int m { get; set; }
            //public int n { get; set; }
        }

        public class SaveBase64Req
        {
            public string img { get; set; }
        }

        public class UpdateSportTypeReq
        {
            [Required]
            public int? Id { get; set; }
            [Required]
            public string Name { get; set; }

            public string Remark { get; set; }
            public string NameE { get; set; }
            public string RemarkE { get; set; }
        }
    }
}