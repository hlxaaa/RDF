using System;
using Model;

namespace WcfService.Sys
{
    /// <summary>
    /// 意见反馈
    /// </summary>
    public class Sys_FeedBack : BaseModel
    {
        public Sys_FeedBack()
        {
            Cfg = new Sys_FeedBackCfg();
        }
        /// <summary>
        ///主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///正文
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        ///提交人
        /// </summary>
        public int EditorId { get; set; }
        /// <summary>
        ///提交时间
        /// </summary>
        public DateTime EditTime { get; set; }
        public string UserName { get; set; }
        public int? isEnglish { get; set; }
        public string reply { get; set; }
    }
}
