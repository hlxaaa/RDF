using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class AudioView
    {
        public AudioView(){}
        //public AudioView(Req req){
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
            //if(req.typeName != null)
            //typeName = req.typeName;
        //}
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32 TypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String LongTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32 PlayCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Boolean Enabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String typeName { get; set; }

}
}
