using Tools;
using System;
using Model;
using System.Collections.Generic;
using Common.Helper;

namespace WcfService.Sys
{
    /// <summary>
    /// 直播信息
    /// </summary>
    public class VideoInfo : BaseOpertion
    {
        public VideoInfo() : base(3) { }

        public RdfMsg LoadVideoInfo(dynamic param)
        {
            if (!param.Exists("pageSize"))
                return new RdfMsg(false, "参数pageSize不存在!");
            if (!param.Exists("pageIndex"))
                return new RdfMsg(false, "参数pageIndex不存在!");

            int size = Convert.ToInt32(param.pageSize);
            int isEnglish = 0;
            if (param.Exists("isEnglish"))
            {
                var temp = Convert.ToInt32(param.isEnglish);
                isEnglish = temp == 1 ? 1 : 0;
            }
            if (size <= 0)
                size = 1;
            int index = Convert.ToInt32(param.pageIndex);
            var query = new RdfSqlQuery<Sys_VideoInfo>().JoinTable<Sys_User>((t1, t2) => t1.UserId == t2.Id).JoinTable<Sys_PlayVideo>((t1, t3) => t1.VideoId == t3.Id).Where<Sys_User>((t1, t2) => t2.Type == 1);
            if (param.Exists("userId") && RdfRegex.Int(param.userId))
            {
                int userId = Convert.ToInt32(param.userId);
                query = query.Where(t1 => t1.UserId == userId);
            }
            if (param.Exists("app"))
            {
                //query = query.Where("t1.DataStatus=1 and t1.PlayStatus<3 and GETDATE()<DATEADD(MI,t1.PlayLongTime,t1.BeginTime)");
                query = query.Where("t1.DataStatus=1");
            }
            else
            {
                EditPlayStatus();
            }
            if (param.Exists("search"))
            {
                string search = param.search;
                List<string> sl = new List<string> { "未开始", "直播中", "已暂停", "已结束", "已失效" };
                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (search == "男")
                    {
                        query = query.Where<Sys_User>((t1, t2) => t2.Sex == true);
                    }
                    else if (search == "女")
                    {
                        query = query.Where<Sys_User>((t1, t2) => t2.Sex == false);
                    }
                    else if (sl.Contains(search))
                    {
                        query = query.Where(t1 => t1.PlayStatus == sl.IndexOf(search));
                    }
                    else if (search == "已驳回")
                    {
                        query = query.Where(t1 => t1.DataStatus == 2);
                    }
                    else if (search == "已审核")
                    {
                        query = query.Where(t1 => t1.DataStatus == 1);
                    }
                    else if (search == "禁用")
                    {
                        query = query.Where(t1 => t1.DataStatus == 3);
                    }
                    else if (search == "未审核")
                    {
                        query = query.Where(t1 => t1.DataStatus == 0);
                    }
                    else
                    {
                        query = query.Where<Sys_User, Sys_PlayVideo>((t1, t2, t3) => t1.Id.ToString().Contains(search) || t1.Title.Contains(search) || t2.UserName.Contains(search) || t2.Phone.Contains(search) || t2.UserExplain.Contains(search) || t3.Title.Contains(search));
                    }
                }
            }
            int sum = (int)query.Count(t1 => new { cnt = t1.Id }).ToObject();
            int pageCount = 1;
            if (sum % size == 0)
                pageCount = sum / size;
            else
                pageCount = (sum / size) + 1;
            List<Sys_VideoInfo> list = query.Select("t1.*,t2.UserName,t2.Url UserUrl,isnull(t3.Title,'') VideoName", true).OrderByDesc(t1 => t1.BeginTime).Take(size).PageIndex(index).ToList();
            List<Sys_VideoInfo> newlist = new List<Sys_VideoInfo>();
            list.ForEach(item =>
            {
                if (item.PlayStatus == 1)
                    newlist.Add(item);
            });
            list.ForEach(item =>
            {
                if (item.PlayStatus == 0)
                    newlist.Add(item);
            });
            list.ForEach(item =>
            {
                if (item.PlayStatus == 2)
                    newlist.Add(item);
            });
            list.ForEach(item =>
            {
                if (item.PlayStatus == 3)
                    newlist.Add(item);
            });
            list.ForEach(item =>
            {
                if (item.PlayStatus == 4)
                    newlist.Add(item);
            });
            if (isEnglish == 1)
            {
                for (int i = 0; i < newlist.Count; i++)
                {
                    newlist[i].Title = newlist[i].TitleE;
                }
            }
            return new RdfMsg(true, RdfSerializer.ObjToJson(newlist), pageCount);
        }

        public RdfMsg AddVideoInfo(dynamic param)
        {
            Sys_VideoInfo info = new Sys_VideoInfo()
            {
                PlayStatus = 0,
                DataStatus = 0,
                UserId = UserInfo.Id,
                CloudId = "",
                VideoId = 0
            };
            if (param.Exists("userId"))
                info.UserId = Convert.ToInt32(param.userId.ToString());

            if (!param.Exists("Title"))
                return new RdfMsg(false, "参数Title不存在!");
            if (!param.Exists("BeginTime"))
                return new RdfMsg(false, "参数BeginTime不存在!");
            if (!param.Exists("PlayLongTime"))
                return new RdfMsg(false, "参数PlayLongTime不存在!");
            if (!RdfRegex.Int(param.PlayLongTime))
                return new RdfMsg(false, "直播持续分钟不正确!");
            if (param.Exists("BeginTime") && !RdfRegex.DateTime(param.BeginTime))
                return new RdfMsg(false, "直播开始时间格式错误!");
            info.Url = "";
            info.Title = param.Title;
            info.BeginTime = Convert.ToDateTime(param.BeginTime);
            if (info.BeginTime <= DateTime.Now)
                return new RdfMsg(false, "直播开始时间应大于当前时间!");
            info.PlayLongTime = Convert.ToInt32(param.PlayLongTime.ToString());
            if (info.PlayLongTime <= 0)
                return new RdfMsg(false, "直播持续分钟应大于0!");
            RdfMsg msg = info.Insert(true);
            if (!msg.Success)
                return msg;
            info.Url = "UpLoadFile\\VideoTitleImage\\" + info.Id.ToString() + ".jpg";
            msg = info.Edit();
            msg.Result = info.Id;
            return msg;
        }
        /// <summary>
        /// 审核或驳回
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg AuditVideoInfo(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            if (!param.Exists("Type"))
                return new RdfMsg(false, "参数Type不存在!");
            int type = Convert.ToInt32(param.Type.ToString());
            Sys_VideoInfo info = new Sys_VideoInfo() { Id = Convert.ToInt32(param.Id.ToString()) };
            if (!info.GetEntity())
                return new RdfMsg(false, "获取直播信息失败!");
            if (info.PlayStatus != 0)
                return new RdfMsg(false, "未开始才能操作!");
            if (info.DataStatus == 1)
                return new RdfMsg(false, "已审核不能操作!");
            if (info.DataStatus == 2)
                return new RdfMsg(false, "已驳回不能操作!");
            info.DataStatus = type;
            VCloud v = new VCloud();
            V_Create_Msg v_msg = null;
            if (string.IsNullOrWhiteSpace(info.CloudId))
            {
                v_msg = v.Create(info.Id.ToString());
                if (v_msg.code != 200)
                    return new RdfMsg(false, v_msg.msg);
                info.CloudId = v_msg.ret.cid;
                RdfMsg msg = info.Edit();
                if (!msg.Success)
                {
                    v.Delete(v_msg.ret.cid);
                    return msg;
                }
            }
            return info.Edit();
        }
        /// <summary>
        /// 获取推流地址
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetPlugFlowUrl(dynamic param)
        {
            RdfMsg msg = GetVideoInfo(param);
            if (!msg.Success)
                return msg;
            Sys_VideoInfo info = RdfSerializer.JsonToObj<Sys_VideoInfo>(msg.Result.ToString());
            if (info.BeginTime > DateTime.Now)
                return new RdfMsg(false, "直播还未开始,开始时间是:" + info.BeginTime.AddMinutes(15).ToString("yyyy-MM-dd HH:mm"));
            DateTime end = info.BeginTime.AddMinutes(info.PlayLongTime);
            if (end < DateTime.Now)
            {
                if (info.PlayStatus < 3)
                {
                    info.PlayStatus = 3;
                    info.Edit();
                }
                return new RdfMsg(false, "直播已结束,结束时间是:" + end.ToString("yyyy-MM-dd HH:mm"));
            }
            VCloud v = new VCloud();
            V_Create_Msg v_msg = null;
            if (string.IsNullOrWhiteSpace(info.CloudId))
            {
                v_msg = v.Create(info.Id.ToString());
                if (v_msg.code != 200)
                    return new RdfMsg(false, v_msg.msg);
                info.CloudId = v_msg.ret.cid;
                msg = info.Edit();
                if (!msg.Success)
                {
                    v.Delete(v_msg.ret.cid);
                    return msg;
                }
            }
            else
            {
                v_msg = v.GetAddress(info.CloudId);
                if (v_msg.code != 200)
                    return new RdfMsg(false, v_msg.msg);
            }
            return new RdfMsg(true, v_msg.ret.pushUrl);
        }
        /// <summary>
        /// 获取直播信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetVideoInfo(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            Sys_VideoInfo info = new Sys_VideoInfo() { Id = Convert.ToInt32(param.Id.ToString()) };
            if (!info.GetEntity())
                return new RdfMsg(false, "获取直播信息失败!");
            DateTime end = info.BeginTime.AddMinutes(info.PlayLongTime);
            if (end < DateTime.Now && (info.PlayStatus == 0 || info.PlayStatus == 2))
            {
                info.PlayStatus = info.PlayStatus == 0 ? 4 : 3;
                RdfMsg msg = info.Edit();
                if (!msg.Success)
                    return msg;
            }
            if (info.PlayStatus == 3)
                return new RdfMsg(false, "直播已结束不能操作!");
            if (info.PlayStatus == 4)
                return new RdfMsg(false, "直播已失效不能操作!");
            if (info.DataStatus == 0)
                return new RdfMsg(false, "未审核不能操作!");
            if (info.DataStatus == 2)
                return new RdfMsg(false, "已驳回不能操作!");
            if (info.DataStatus == 3)
                return new RdfMsg(false, "已禁用不能操作!");
            return new RdfMsg(true, info.ToJson());
        }
        /// <summary>
        /// 获取播放地址
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetPlayUrl(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            Sys_VideoInfo info = new Sys_VideoInfo() { Id = Convert.ToInt32(param.Id.ToString()) };
            if (!info.GetEntity())
                return new RdfMsg(false, "获取直播信息失败!");
            if (info.PlayStatus == 3 && info.VideoId > 0)
            {
                Sys_PlayVideo p = new Sys_PlayVideo { Id = info.VideoId };
                if (p.GetEntity())
                    return new RdfMsg(true, RdfSerializer.ObjToJson(new List<string> { p.Url }), info.ToJson());
            }

            RdfMsg msg = GetVideoInfo(param);
            if (!msg.Success)
                return msg;

            if (info.BeginTime > DateTime.Now)
                return new RdfMsg(false, "直播还未开始,开始时间是:" + info.BeginTime.AddMinutes(15).ToString("yyyy-MM-dd HH:mm"));
            List<string> list = new List<string>();
            if (!string.IsNullOrWhiteSpace(info.CloudId))
            {
                VCloud v = new VCloud();
                V_Create_Msg v_msg = v.GetAddress(info.CloudId);
                if (v_msg.code != 200)
                    return new RdfMsg(false, v_msg.msg);
                list.Add(v_msg.ret.rtmpPullUrl);
                list.Add(v_msg.ret.httpPullUrl);
                list.Add(v_msg.ret.hlsPullUrl);
                if (info.VideoId > 0)
                {
                    Sys_PlayVideo p = new Sys_PlayVideo { Id = info.VideoId };
                    if (p.GetEntity())
                        list.Add(p.Url);
                }

            }
            return new RdfMsg(true, RdfSerializer.ObjToJson(list), info.ToJson());
        }
        /// <summary>
        /// 直播回调
        /// </summary>
        /// <param name="nId"></param>
        /// <param name="time"></param>
        /// <param name="status">0：空闲； 1：直播； 2：禁用； 3：直播录制</param>
        /// <param name="cid"></param>
        public void PlayCallBack(string nId, string time, int status, string cid)
        {
            try
            {
                VCloud v = new VCloud();
                Sys_VideoInfo info = new RdfSqlQuery<Sys_VideoInfo>().Where(t1 => t1.CloudId == cid).ToEntity();
                if (info == null)
                    return;
                if (info.DataStatus == 1 || info.DataStatus == 3)
                {
                    if (status == 1)
                    {
                        //开始直播
                        info.PlayStatus = 1;
                    }
                    else
                    {
                        //结束直播
                        OnlineUser.RemoveLiveAsync(info.Id);
                        if (info.BeginTime.AddMinutes(info.PlayLongTime) <= DateTime.Now)
                        {
                            info.PlayStatus = 3;
                            V_Delete_Msg msg = v.Delete(info.CloudId);
                            if (msg.code == 200)
                                info.CloudId = "";
                        }
                        else
                        {
                            info.PlayStatus = 2;
                        }
                    }
                    info.Edit();
                }
            }
            catch (Exception ex)
            {
                RdfLog.WriteException(ex, "直播回调异常");
            }
        }
        /// <summary>
        /// 获取直播间在线用户列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetOnlineUserList(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            if (!RdfRegex.Int(param.Id))
                return new RdfMsg(false, "参数Id不正确!");
            int id = Convert.ToInt32(param.Id.ToString());
            Sys_VideoInfo info = new RdfSqlQuery<Sys_VideoInfo>().Where(t1 => t1.Id == id).ToEntity();
            if (info == null)
                return new RdfMsg(false, "获取直播信息失败!");
            List<OnlineUserInfo> list = OnlineUser.GetUserList(id);
            list.RemoveAll(item => item.SetTime.AddMinutes(5) < DateTime.Now);
            OnlineUser.RemoveTimeOutAsyn(id);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Url = StringHelper.Instance.GetApiUrl(list[i].Url);
            }
            return new RdfMsg(true, RdfSerializer.ObjToJson(list), info.PlayStatus);
        }
        /// <summary>
        /// 添加在线用户
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg AddOnlineUser(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            if (!param.Exists("Kll"))
                return new RdfMsg(false, "参数Kll不存在!");
            if (!param.Exists("Kcal"))
                return new RdfMsg(false, "参数Kcal不存在!");
            if (!param.Exists("Km"))
                return new RdfMsg(false, "参数Km不存在!");
            if (!param.Exists("Sd"))
                return new RdfMsg(false, "参数Sd不存在!");
            if (!param.Exists("Xl"))
                return new RdfMsg(false, "参数Xl不存在!");
            int id = Convert.ToInt32(param.Id.ToString());
            Sys_VideoInfo en = new Sys_VideoInfo() { Id = id };
            if (!en.GetEntity())
                return new RdfMsg(false, "参数Id错误!");
            if (en.PlayStatus == 1)
            {
                int kll = Convert.ToInt32(param.Kll.ToString());
                int kcal = Convert.ToInt32(param.Kcal.ToString());
                int km = Convert.ToInt32(param.Km.ToString());
                int sd = Convert.ToInt32(param.Sd.ToString());
                int xl = Convert.ToInt32(param.Xl.ToString());
                var userHere = DbOpertion.DBoperation.Sys_UserOper.Instance.GetById(UserInfo.Id);
                OnlineUserInfo info = new OnlineUserInfo
                {
                    Id = UserInfo.Id,
                    Kll = kll,
                    Kcal = kcal,
                    Km = km,
                    Sd = sd,
                    Xl = xl,
                    //Name = UserInfo.UserName,
                    Name = userHere.UserName,
                    Url = UserInfo.Url
                };
                OnlineUser.AddOnlineUserAsync(id, info);
            }
            return new RdfMsg(true);
        }
        /// <summary>
        /// 修改播放状态和删除频道
        /// </summary>
        public void EditPlayStatus()
        {
            List<Sys_VideoInfo> list = new RdfSqlQuery<Sys_VideoInfo>().Where("(PlayStatus=0 or PlayStatus=2) and GETDATE()>DATEADD(MI,PlayLongTime,BeginTime)").ToList();
            if (list == null || list.Count == 0)
                return;
            VCloud v = new VCloud();
            list.ForEach(item =>
            {
                if (!string.IsNullOrWhiteSpace(item.CloudId))
                {
                    V_Delete_Msg msg = v.Delete(item.CloudId);
                    if (msg.code == 200)
                    {
                        if (item.PlayStatus == 0)
                            item.PlayStatus = 4;
                        else
                            item.PlayStatus = 3;
                        item.CloudId = "";
                        item.Edit();
                    }
                    else
                    {
                        RdfLog.WriteLog("删除频道失败:" + msg.msg);
                    }
                }
            });
        }
        /// <summary>
        /// 退出播放
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg RemoveOnlineUser(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            int id = Convert.ToInt32(param.Id.ToString());
            OnlineUser.RemoveOnlineUserAsync(id, UserInfo.Id);
            return new RdfMsg(true);
        }
        /// <summary>
        /// 禁用频道
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg PauseVideo(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            int id = Convert.ToInt32(param.Id.ToString());
            Sys_VideoInfo en = new Sys_VideoInfo() { Id = id };
            if (!en.GetEntity())
                return new RdfMsg(false, "参数Id错误!");
            if (en.DataStatus == 0)
                return new RdfMsg(false, "未审核不能操作!");
            if (en.DataStatus == 2)
                return new RdfMsg(false, "已驳回不能操作!");
            if (en.DataStatus == 3)
                return new RdfMsg(false, "已禁用不能操作!");
            if (string.IsNullOrWhiteSpace(en.CloudId))
                return new RdfMsg(false, "直播未生成,无需禁用!");
            VCloud v = new VCloud();
            V_Msg msg = v.Pause(en.CloudId);
            if (msg.code != 200)
                return new RdfMsg(false, msg.msg);
            en.DataStatus = 3;
            return en.Edit();
        }

        /// <summary>
        /// 恢复频道
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg ResumeVideo(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            int id = Convert.ToInt32(param.Id.ToString());
            Sys_VideoInfo en = new Sys_VideoInfo() { Id = id };
            if (!en.GetEntity())
                return new RdfMsg(false, "参数Id错误!");
            if (en.DataStatus == 0)
                return new RdfMsg(false, "未审核不能操作!");
            if (en.DataStatus == 2)
                return new RdfMsg(false, "已驳回不能操作!");
            if (en.DataStatus == 1)
                return new RdfMsg(false, "已恢复不能操作!");
            if (string.IsNullOrWhiteSpace(en.CloudId))
                return new RdfMsg(false, "直播未生成,无需恢复!");
            VCloud v = new VCloud();
            V_Msg msg = v.Resume(en.CloudId);
            if (msg.code != 200)
                return new RdfMsg(false, msg.msg);
            en.DataStatus = 1;
            return en.Edit();
        }

        public RdfMsg SetVideo(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            if (!param.Exists("VideoId"))
                return new RdfMsg(false, "参数VideoId不存在!");
            int id = Convert.ToInt32(param.Id.ToString());
            int videoId = Convert.ToInt32(param.VideoId.ToString());
            Sys_VideoInfo en = new Sys_VideoInfo() { Id = id };
            if (!en.GetEntity())
                return new RdfMsg(false, "参数Id错误!");
            en.VideoId = videoId;
            return en.Edit();
        }
    }
}
