using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class SportMsg
    {
        public SportMsg(){}
        //public SportMsg(Req req){
            //if(req.id != null)
            //id = Convert.ToInt32(req.id);
            //if(req.userId != null)
            //userId = Convert.ToInt32(req.userId);
            //if(req.createTime != null)
            //createTime = Convert.ToDateTime(req.createTime);
            //if(req.content != null)
            //content = req.content;
            //if(req.sportId != null)
            //sportId = Convert.ToInt32(req.sportId);
        //}
        /// <summary>
        /// 
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? sportId { get; set; }

}
}
