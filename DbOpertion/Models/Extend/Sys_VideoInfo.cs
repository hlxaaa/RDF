using System;

namespace DbOpertion.Models
{

    public partial class Sys_VideoInfo
    {

        //public Sys_VideoInfo(Req req){
        //if(req.Id != null)
        //Id = Convert.ToInt32(req.Id);
        //if(req.UserId != null)
        //UserId = Convert.ToInt32(req.UserId);
        //if(req.Url != null)
        //Url = req.Url;
        //if(req.Title != null)
        //Title = req.Title;
        //if(req.BeginTime != null)
        //BeginTime = Convert.ToDateTime(req.BeginTime);
        //if(req.PlayLongTime != null)
        //PlayLongTime = Convert.ToInt32(req.PlayLongTime);
        //if(req.PlayStatus != null)
        //PlayStatus = Convert.ToInt32(req.PlayStatus);
        //if(req.DataStatus != null)
        //DataStatus = Convert.ToInt32(req.DataStatus);
        //if(req.CloudId != null)
        //CloudId = req.CloudId;
        //if(req.VideoId != null)
        //VideoId = Convert.ToInt32(req.VideoId);
        //}

        /// <summary>
        /// 教练
        /// </summary>
        public Int32? UserId { get; set; }
        /// <summary>
        /// 直播开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 直播持续时间
        /// </summary>
        public Int32? PlayLongTime { get; set; }
        /// <summary>
        /// 直播状态
        /// </summary>
        public Int32? PlayStatus { get; set; }
        /// <summary>
        /// 数据状态
        /// </summary>
        public Int32? DataStatus { get; set; }
    }
}
