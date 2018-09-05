using System;

namespace DbOpertion.Models
{

    public partial class Sys_Sport
    {

        //public Sys_Sport(Req req){
        //if(req.Id != null)
        //Id = Convert.ToInt32(req.Id);
        //if(req.Title != null)
        //Title = req.Title;
        //if(req.TitleUrl != null)
        //TitleUrl = req.TitleUrl;
        //if(req.TypeId != null)
        //TypeId = Convert.ToInt32(req.TypeId);
        //if(req.CreateTime != null)
        //CreateTime = Convert.ToDateTime(req.CreateTime);
        //if(req.Content != null)
        //Content = req.Content;
        //if(req.Remark != null)
        //Remark = req.Remark;
        //if(req.Enabled != null)
        //Enabled = Convert.ToBoolean(req.Enabled);
        //if(req.DataType != null)
        //DataType = Convert.ToInt32(req.DataType);
        //}

        /// <summary>
        /// 分类
        /// </summary>
        public Int32? TypeId { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public Boolean? Enabled { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public Int32? DataType { get; set; }

    }
}
