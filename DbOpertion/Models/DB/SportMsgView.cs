using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class SportMsgView
    {
        public SportMsgView(){}
        //public SportMsgView(Req req){
            //if(req.id != null)
            //id = Convert.ToInt32(req.id);
            //if(req.sportId != null)
            //sportId = Convert.ToInt32(req.sportId);
            //if(req.createTime != null)
            //createTime = Convert.ToDateTime(req.createTime);
            //if(req.UserName != null)
            //UserName = req.UserName;
            //if(req.Url != null)
            //Url = req.Url;
            //if(req.TitleUrl != null)
            //TitleUrl = req.TitleUrl;
            //if(req.Title != null)
            //Title = req.Title;
            //if(req.Content != null)
            //Content = req.Content;
            //if(req.msg != null)
            //msg = req.msg;
        //}
        /// <summary>
        /// 
        /// </summary>
        public Int32? id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? sportId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String TitleUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String msg { get; set; }

}
}
