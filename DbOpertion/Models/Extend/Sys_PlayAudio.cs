using System;

namespace DbOpertion.Models
{

    public partial class Sys_PlayAudio
    {
        /// <summary>
        /// 专辑
        /// </summary>
        public Int32? TypeId { get; set; }
   
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? EditTime { get; set; }
        /// <summary>
        /// 播放次数
        /// </summary>
        public Int32? PlayCount { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        public Boolean? Enabled { get; set; }

}
}
