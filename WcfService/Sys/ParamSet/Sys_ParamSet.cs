using System;
using Model;

namespace WcfService.Sys
{
    /// <summary>
    /// 参数设置
    /// </summary>
    public class Sys_ParamSet: BaseModel
    {
        public Sys_ParamSet()
        {
            Cfg = new Sys_ParamSetCfg();
        }
        /// <summary>
        ///key
        /// </summary>
        public string key{get;set;}
        /// <summary>
        ///value
        /// </summary>
        public string value{get;set;}
        /// <summary>
        ///remark
        /// </summary>
        public string remark { get; set; }
        public string uuid { get; set; }
        public int type { get; set; }

    }
}
