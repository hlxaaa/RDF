using System;
using Model;

namespace WcfService.Sys
{
    /// <summary>
    /// 运动干活
    /// </summary>
    public class Sys_Sport : BaseModel
    {
        public Sys_Sport()
        {
            Cfg = new Sys_SportCfg();
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
        ///分类
        /// </summary>
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        /// <summary>
        ///发布时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        ///正文
        /// </summary>
        public string Content { get; set; }
        public string ContentE { get; set; }
        /// <summary>
        ///描述
        /// </summary>
        public string Remark { get; set; }
        public string RemarkE { get; set; }
        /// <summary>
        ///启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 0运动干活，1奖品
        /// </summary>
        public int DataType { get; set; }

    }
}
