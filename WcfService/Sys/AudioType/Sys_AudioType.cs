using System;
using Model;
using System.Collections.Generic;

namespace WcfService.Sys
{
    /// <summary>
    /// 音频专辑
    /// </summary>
    public class Sys_AudioType : BaseModel
    {
        public Sys_AudioType()
        {
            Cfg = new Sys_AudioTypeCfg();
        }
        /// <summary>
        ///主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///标题
        /// </summary>
        public string Title { get; set; }
        public string TitleE { get; set; }
        /// <summary>
        ///封面图
        /// </summary>
        public string TitleUrl { get; set; }
        /// <summary>
        ///描述
        /// </summary>
        public string Remark { get; set; }
        public string RemarkE { get; set; }
        /// <summary>
        ///启用
        /// </summary>
        public bool Enabled { get; set; }

        public int? isEnglish { get; set; }

        public List<Sys_PlayAudio> Audios { get; set; }

    }
}
