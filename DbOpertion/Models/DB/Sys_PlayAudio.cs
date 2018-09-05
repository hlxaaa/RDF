using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Sys_PlayAudio
    {
        public Sys_PlayAudio(){}
        //public Sys_PlayAudio(Req req){
            //if(req.Id != null)
            //Id = Convert.ToInt32(req.Id);
            //if(req.Title != null)
            //Title = req.Title;
            //if(req.TypeId != null)
            //TypeId = Convert.ToInt32(req.TypeId);
            //if(req.Url != null)
            //Url = req.Url;
            //if(req.LongTime != null)
            //LongTime = req.LongTime;
            //if(req.EditTime != null)
            //EditTime = Convert.ToDateTime(req.EditTime);
            //if(req.PlayCount != null)
            //PlayCount = Convert.ToInt32(req.PlayCount);
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
        /// 专辑
        /// </summary>
        //public Int32 TypeId { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public String Url { get; set; }
        /// <summary>
        /// 时长
        /// </summary>
        public String LongTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        //public DateTime EditTime { get; set; }
        /// <summary>
        /// 播放次数
        /// </summary>
        //public Int32 PlayCount { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        //public Boolean Enabled { get; set; }

}
}
