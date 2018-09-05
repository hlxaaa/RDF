using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Sys_About
    {
        public Sys_About(){}
        //public Sys_About(Req req){
            //if(req.Id != null)
            //Id = Convert.ToInt32(req.Id);
            //if(req.Content != null)
            //Content = req.Content;
            //if(req.EditorId != null)
            //EditorId = Convert.ToInt32(req.EditorId);
            //if(req.EditTime != null)
            //EditTime = Convert.ToDateTime(req.EditTime);
        //}
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public String Content { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public Int32 EditorId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime EditTime { get; set; }

}
}
