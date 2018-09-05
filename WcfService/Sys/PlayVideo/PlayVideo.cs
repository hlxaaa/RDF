using Tools;
using System;
using Model;
using System.Collections.Generic;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.vod.Model.V20170321;

namespace WcfService.Sys
{
    /// <summary>
    /// 点播视频
    /// </summary>
    public class PlayVideo : BaseOpertion
    {
        public PlayVideo() : base(2) { }
        /// <summary>
        /// 播放次数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg SetPalyCount(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            Sys_PlayVideo play = new Sys_PlayVideo() { Id = param.Id };
            if (!play.GetEntity())
                return new RdfMsg(false, "视频信息不存在!");
            play.PlayCount++;
            return play.Edit();
        }
        public RdfMsg GetAddress(dynamic param)
        {
            if (!param.Exists("FileName"))
                return new RdfMsg(false, "参数FileName不存在!");
            string live = System.Configuration.ConfigurationManager.AppSettings["AliParam"];
            if (string.IsNullOrWhiteSpace(live))
                throw new Exception("未配置阿里云参数!");
            string[] array = live.Split(',');
            if (array.Length != 4)
                throw new Exception("阿里云参数配置错误!");

            IClientProfile clientProfile = DefaultProfile.GetProfile("cn-shanghai", array[0], array[1]);
            DefaultAcsClient client = new DefaultAcsClient(clientProfile);
            CreateUploadVideoRequest request = new CreateUploadVideoRequest();
            string name = param.FileName;
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
            if (param.Exists("Type"))
            {
                request.FileName = request.Title + ".mp4";
            }
            //request.Description = "视频描述";
            //request.CoverURL = "http://cover.sample.com/sample.jpg";
            //request.Tags = "标签1,标签2";
            //request.CateId = 0;
            CreateUploadVideoResponse response = client.GetAcsResponse(request);
            return new RdfMsg(true, response.UploadAddress, response.UploadAuth);
        }

        /// <summary>
        /// 根据Id获取点播视频信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetPlayVideoById(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            Sys_PlayVideo play = new Sys_PlayVideo() { Id = param.Id };
            if (!play.GetEntity())
                return new RdfMsg(false, "视频信息不存在!");
            return new RdfMsg(true, play.ToJson());
        }
        /// <summary>
        /// 添加点播视频信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg AddPlayVideo(dynamic param)
        {
            Sys_PlayVideo play = new Sys_PlayVideo() { Url = "", TitleUrl = "", EditTime = DateTime.Now, Enabled = true };
            play.Cfg.FieldList.ForEach(field =>
            {
                if (field.ColumnAs != "TitleUrl" && param.Exists(field.ColumnAs))
                {
                    object value = param[field.ColumnAs];
                    play.SetValue(value, field);
                }
            });
            RdfMsg msg = play.Insert(true);
            if (msg.Success)
            {
                play.TitleUrl = "UpLoadFile\\PlayVideoImage\\" + play.Id.ToString() + ".jpg";
                play.Edit();
                msg.Result = play.ToJson();
            }
            return msg;
        }
        /// <summary>
        /// 编辑点播视频信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg EditPlayVideo(dynamic param)
        {
            Sys_PlayVideo play = new Sys_PlayVideo();
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            play.Id = Convert.ToInt32(param.Id);
            if (!play.GetEntity())
                return new RdfMsg(false, "获取视频信息失败!");
            play.Cfg.FieldList.ForEach(field =>
            {
                if (field.ColumnAs != "Id" && param.Exists(field.ColumnAs))
                {
                    object value = param[field.ColumnAs];
                    play.SetValue(value, field);
                }
            });
            play.EditTime = DateTime.Now;
            RdfMsg msg = play.Edit();
            if (msg.Success)
                msg.Result = play.ToJson();
            return msg;
        }
        public RdfMsg LoadPlayVideo(dynamic param)
        {
            if (!param.Exists("pageSize"))
                return new RdfMsg(false, "参数pageSize不存在!");
            if (!param.Exists("pageIndex"))
                return new RdfMsg(false, "参数pageIndex不存在!");
            int size = Convert.ToInt32(param.pageSize);
            int index = Convert.ToInt32(param.pageIndex);
            int isEnglish = 0;
            if (param.Exists("isEnglish"))
            {
                var temp = Convert.ToInt32(param.isEnglish);
                isEnglish = temp == 1 ? 1 : 0;
            }
            var query = new RdfSqlQuery<Sys_PlayVideo>();
            if (param.Exists("app"))
            {
                query.Where(t1 => t1.Enabled == true);
            }
            if (param.Exists("search"))
            {
                string search = param.search;
                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(t1 => t1.Title.Contains(search) || t1.LongTime.ToString().Contains(search) || t1.PlayCount.ToString().Contains(search));
            }
            //query = query.Where(p => p.isEnglish == isEnglish);
            //query = query.Where(p=>p.Title== "20分钟高强度间歇训练12");
            int sum = (int)query.Count(t1 => new { cnt = t1.Id }).ToObject();
            int pageCount = 1;
            if (sum % size == 0)
                pageCount = sum / size;
            else
                pageCount = (sum / size) + 1;
            List<Sys_PlayVideo> list = null;
            if (param.Exists("column") && param.Exists("orderByType"))
            {
                string clm = param.column;
                if (param.orderByType.ToString().ToLower() == "asc")
                {
                    list = query.Select("*", true).OrderBy(t1 => t1[clm]).Take(size).PageIndex(index).ToList();
                }
                else
                {
                    list = query.Select("*", true).OrderByDesc(t1 => t1[clm]).Take(size).PageIndex(index).ToList();
                }
            }
            else
            {
                list = query.Select("*", true).OrderBy(t1 => t1.Id).Take(size).PageIndex(index).ToList();
            }
            if (isEnglish == 1)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Title = list[i].TitleE;
                }
            }

            return new RdfMsg(true, RdfSerializer.ObjToJson(list), pageCount);
        }
    }
}
