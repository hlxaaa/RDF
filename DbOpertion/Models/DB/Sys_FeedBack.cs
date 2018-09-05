using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Sys_FeedBack
    {
        public Sys_FeedBack(){}
        //public Sys_FeedBack(Req req){
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
        /// 正文
        /// </summary>
        public String Content { get; set; }
        ///// <summary>
        ///// 提交人
        ///// </summary>
        //public Int32 EditorId { get; set; }
        ///// <summary>
        ///// 提交时间
        ///// </summary>
        //public DateTime EditTime { get; set; }
        /// <summary>
        /// 回复
        /// </summary>
        public String reply { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? isEnglish { get; set; }
    }
}
