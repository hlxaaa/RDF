using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Sys_PlayVideo
    {
        public Sys_PlayVideo(){}
        //public Sys_PlayVideo(Req req){
            //if(req.Id != null)
            //Id = Convert.ToInt32(req.Id);
            //if(req.Title != null)
            //Title = req.Title;
            //if(req.TitleUrl != null)
            //TitleUrl = req.TitleUrl;
            //if(req.Url != null)
            //Url = req.Url;
            //if(req.LongTime != null)
            //LongTime = req.LongTime;
            //if(req.EditTime != null)
            //EditTime = Convert.ToDateTime(req.EditTime);
            //if(req.PlayCount != null)
            //PlayCount = Convert.ToInt32(req.PlayCount);
            //if(req.Enabled != null)
            //Enabled = Convert.ToBoolean(req.Enabled);
            //if(req.VieldId != null)
            //VieldId = Convert.ToInt32(req.VieldId);
            //if(req.userId != null)
            //userId = Convert.ToInt32(req.userId);
            //if(req.price != null)
            //price = Convert.ToDecimal(req.price);
        //}
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public String TitleUrl { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public String Url { get; set; }
        /// <summary>
        /// 时长
        /// </summary>
        public String LongTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        //public DateTime EditTime { get; set; }
        ///// <summary>
        ///// 播放次数
        ///// </summary>
        //public Int32 PlayCount { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        //public Boolean Enabled { get; set; }
        /// <summary>
        /// 上传到云后，返回的id
        /// </summary>
        public String VieldId { get; set; }
        /// <summary>
        /// 教练id（0就是平台的）
        /// </summary>
        public Int32? userId { get; set; }
        /// <summary>
        /// 视频价格，0就是不收费
        /// </summary>
        public Decimal? price { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public String isPass { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? isEnglish { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public String TitleE { get; set; }
    }
}
