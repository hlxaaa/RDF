using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Sys_VideoInfo
    {
        public Sys_VideoInfo(){}
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
            //if(req.isPass != null)
            //isPass = req.isPass;
            //if(req.price != null)
            //price = Convert.ToDecimal(req.price);
            //if(req.isEnglish != null)
            //isEnglish = Convert.ToInt32(req.isEnglish);
        //}
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }
        /// <summary>
        /// 教练
        /// </summary>
        //public Int32 UserId { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public String Url { get; set; }
        /// <summary>
        /// 直播标题
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 直播开始时间
        /// </summary>
        //public DateTime BeginTime { get; set; }
        ///// <summary>
        ///// 直播持续时间
        ///// </summary>
        //public Int32 PlayLongTime { get; set; }
        /// <summary>
        /// 直播状态 0未开始 1直播中 2已暂停 3已结束 4已失效
        /// </summary>
        //public Int32 PlayStatus { get; set; }
        ///// <summary>
        ///// 数据状态 0未审核 1审核 2驳回 3禁用
        ///// </summary>
        //public Int32 DataStatus { get; set; }
        /// <summary>
        /// 直播Id
        /// </summary>
        public String CloudId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? VideoId { get; set; }
        /// <summary>
        /// 是否过审
        /// </summary>
        public String isPass { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? isEnglish { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String TitleE { get; set; }
    }
}
