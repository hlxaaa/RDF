using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Device
    {
        public Device(){}
        //public Device(Req req){
            //if(req.id != null)
            //id = Convert.ToInt32(req.id);
            //if(req.name != null)
            //name = req.name;
            //if(req.editTime != null)
            //editTime = Convert.ToDateTime(req.editTime);
            //if(req.gymId != null)
            //gymId = Convert.ToInt32(req.gymId);
            //if(req.userId != null)
            //userId = Convert.ToInt32(req.userId);
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
        public Int32? gymId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? userId { get; set; }

}
}
