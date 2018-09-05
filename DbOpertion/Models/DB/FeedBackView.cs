using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class FeedBackView
    {
        public FeedBackView(){}
        //public FeedBackView(Req req){
            //if(req.Id != null)
            //Id = Convert.ToInt32(req.Id);
            //if(req.Title != null)
            //Title = req.Title;
            //if(req.Content != null)
            //Content = req.Content;
            //if(req.EditorId != null)
            //EditorId = Convert.ToInt32(req.EditorId);
            //if(req.EditTime != null)
            //EditTime = Convert.ToDateTime(req.EditTime);
            //if(req.reply != null)
            //reply = req.reply;
            //if(req.isEnglish != null)
            //isEnglish = Convert.ToInt32(req.isEnglish);
            //if(req.username != null)
            //username = req.username;
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
        public String Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32 EditorId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String reply { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? isEnglish { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String username { get; set; }

}
}
