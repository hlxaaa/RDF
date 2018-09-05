using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class VideoPayView
    {
        public VideoPayView(){}
        //public VideoPayView(Req req){
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
            //VieldId = req.VieldId;
            //if(req.userId != null)
            //userId = Convert.ToInt32(req.userId);
            //if(req.price != null)
            //price = Convert.ToDecimal(req.price);
            //if(req.isPass != null)
            //isPass = req.isPass;
            //if(req.payUserId != null)
            //payUserId = Convert.ToInt32(req.payUserId);
            //if(req.status != null)
            //status = Convert.ToInt32(req.status);
        //}
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String TitleUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String LongTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32 PlayCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Boolean Enabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String VieldId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String isPass { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? payUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? status { get; set; }

}
}
