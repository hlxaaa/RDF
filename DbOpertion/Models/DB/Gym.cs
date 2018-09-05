using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Gym
    {
        public Gym(){}
        //public Gym(Req req){
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

}
}
