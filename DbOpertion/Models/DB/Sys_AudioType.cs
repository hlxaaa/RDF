using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Sys_AudioType
    {
        public Sys_AudioType() { }
        //public Sys_AudioType(Req req){
        //if(req.Id != null)
        //Id = Convert.ToInt32(req.Id);
        //if(req.Title != null)
        //Title = req.Title;
        //if(req.TitleUrl != null)
        //TitleUrl = req.TitleUrl;
        //if(req.Remark != null)
        //Remark = req.Remark;
        //if(req.Enabled != null)
        //Enabled = Convert.ToBoolean(req.Enabled);
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
        /// 封面图
        /// </summary>
        public String TitleUrl { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public String Remark { get; set; }
        public string RemarkE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? isEnglish { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        //public Boolean Enabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String TitleE { get; set; }
    }
}
