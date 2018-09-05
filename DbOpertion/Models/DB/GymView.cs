using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class GymView
    {
        public GymView(){}
        //public GymView(Req req){
            //if(req.id != null)
            //id = Convert.ToInt32(req.id);
            //if(req.name != null)
            //name = req.name;
            //if(req.editTime != null)
            //editTime = Convert.ToDateTime(req.editTime);
            //if(req.nameE != null)
            //nameE = req.nameE;
            //if(req.gymUserId != null)
            //gymUserId = Convert.ToInt32(req.gymUserId);
            //if(req.UId != null)
            //UId = req.UId;
        //}
        /// <summary>
        /// 
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? editTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String nameE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? gymUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String UId { get; set; }

}
}
