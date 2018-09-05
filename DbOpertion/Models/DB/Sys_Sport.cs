using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Sys_Sport
    {
        public Sys_Sport(){}
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
        ///// <summary>
        ///// 分类
        ///// </summary>
        //public Int32 TypeId { get; set; }
        ///// <summary>
        ///// 发布时间
        ///// </summary>
        //public DateTime CreateTime { get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public String Content { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public String Remark { get; set; }
        ///// <summary>
        ///// 启用
        ///// </summary>
        //public Boolean Enabled { get; set; }
        ///// <summary>
        ///// 类型
        ///// </summary>
        //public Int32 DataType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String ContentE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String RemarkE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String TitleE { get; set; }
    }
}
