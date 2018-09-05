using System;
using Model;

namespace WcfService.Sys
{
    /// <summary>
    /// 直播信息
    /// </summary>
    public class Sys_VideoInfo : BaseModel
    {
        public Sys_VideoInfo()
        {
            Cache = false;
            Cfg = new Sys_VideoInfoCfg();
        }
        /// <summary>
        ///主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///封面图
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        ///直播标题
        /// </summary>
        public string Title { get; set; }
        public string TitleE { get; set; }
        /// <summary>
        ///直播开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        ///直播持续时间 分钟
        /// </summary>
        public int PlayLongTime { get; set; }
        /// <summary>
        ///直播状态 0未开始 1直播中 2已暂停 3已结束 4已失效
        /// </summary>
        public int PlayStatus { get; set; }
        /// <summary>
        ///数据状态 0未审核 1审核 2驳回 3禁用
        /// </summary>
        public int DataStatus { get; set; }
        /// <summary>
        ///教练
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 点播Id
        /// </summary>
        public int VideoId { get; set; }
        public string VideoName { get; set; }
        /// <summary>
        /// 直播Id
        /// </summary>
        public string CloudId { get; set; }

        public string UserName { get; set; }
        public string UserUrl { get; set; }
        public int? isEnglish { get; set; }
    }
}
