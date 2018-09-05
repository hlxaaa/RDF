using DbOpertion.Models.Extend;
using System;


namespace DbOpertion.Models
{

    public partial class Sys_PlayVideo
    {
        public Boolean? Enabled { get; set; }
        public DateTime? EditTime { get; set; }
        /// <summary>
        /// ²¥·Å´ÎÊý
        /// </summary>
        public Int32? PlayCount { get; set; }

        public Sys_PlayVideo(AddCourseReq req, int uId)
        {
            price = req.price;
            userId = uId;
            if (req.Title != null)
                Title = req.Title;
            if (req.TitleUrl != null)
                TitleUrl = req.TitleUrl;
            //if (req.Url != null)
            //    Url = req.Url;
            //if (req.LongTime != null)
            //    LongTime = req.LongTime;
            EditTime = DateTime.Now;
            PlayCount = 0;
            Enabled = false;
            isPass = "0";
            if (req.videoId != null)
                VieldId = req.videoId;
        }

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
        //}

    }
}
