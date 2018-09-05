using System;
using Model;

namespace WcfService.Sys
{
    /// <summary>
    /// 点播音频
    /// </summary>
    public class Sys_PlayAudio : BaseModel
    {
        public Sys_PlayAudio()
        {
            Cfg = new Sys_PlayAudioCfg();
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
        ///专辑
        /// </summary>
        public int TypeId { get; set; }

        public string TitleE { get; set; }

        /// <summary>
        ///Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        ///时长
        /// </summary>
        public String LongTime { get; set; }
        /// <summary>
        ///更新时间
        /// </summary>
        public DateTime EditTime { get; set; }
        /// <summary>
        ///播放次数
        /// </summary>
        public int PlayCount { get; set; }
        /// <summary>
        ///启用
        /// </summary>
        public bool Enabled { get; set; }
        public string TypeName { get; set; }

    }
}
