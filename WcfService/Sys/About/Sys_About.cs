using System;
using Model;

namespace WcfService.Sys
{
    /// <summary>
    /// 关于我们
    /// </summary>
    public class Sys_About: BaseModel
    {
        public Sys_About()
        {
            Cfg = new Sys_AboutCfg();
        }
        /// <summary>
        ///主键
        /// </summary>
        public int Id{get;set;}
        /// <summary>
        ///正文
        /// </summary>
        public string Content{get;set;}
        /// <summary>
        ///修改人
        /// </summary>
        public int EditorId{get;set;}
        /// <summary>
        ///修改时间
        /// </summary>
        public DateTime EditTime{get;set;}

    }
}
