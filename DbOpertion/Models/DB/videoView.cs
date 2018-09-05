using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class videoView
    {
        public videoView(){}
        //public videoView(Req req){
            //if(req.Id != null)
            //Id = Convert.ToInt32(req.Id);
            //if(req.Title != null)
            //Title = req.Title;
            //if(req.TitleE != null)
            //TitleE = req.TitleE;
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
            //if(req.isEnglish != null)
            //isEnglish = Convert.ToInt32(req.isEnglish);
            //if(req.UserName != null)
            //UserName = req.UserName;
            //if(req.type != null)
            //type = Convert.ToInt32(req.type);
            //if(req.isUserPass != null)
            //isUserPass = req.isUserPass;
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
        public String TitleE { get; set; }
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
        public Int32? isEnglish { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String isUserPass { get; set; }

}
}
