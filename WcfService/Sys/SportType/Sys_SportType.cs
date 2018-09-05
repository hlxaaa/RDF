using System;
using Model;

namespace WcfService.Sys
{
    /// <summary>
    /// 分类
    /// </summary>
    public class Sys_SportType : BaseModel
    {
        public Sys_SportType()
        {
            Cfg = new Sys_SportTypeCfg();
        }
        /// <summary>
        ///主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///名称
        /// </summary>
        public string Name { get; set; }
        public string NameE { get; set; }
        /// <summary>
        ///描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        ///启用
        /// </summary>
        public bool Enabled { get; set; }
        public int? isEnglish { get; set; }
    }
}
