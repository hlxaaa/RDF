using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class DeviceGym
    {
        public DeviceGym(){}
        //public DeviceGym(Req req){
            //if(req.id != null)
            //id = Convert.ToInt32(req.id);
            //if(req.deviceName != null)
            //deviceName = req.deviceName;
            //if(req.gymId != null)
            //gymId = Convert.ToInt32(req.gymId);
            //if(req.UserId != null)
            //UserId = Convert.ToInt32(req.UserId);
            //if(req.CreateTime != null)
            //CreateTime = Convert.ToDateTime(req.CreateTime);
            //if(req.XL != null)
            //XL = Convert.ToDecimal(req.XL);
            //if(req.SD != null)
            //SD = Convert.ToDecimal(req.SD);
            //if(req.KAL != null)
            //KAL = Convert.ToDecimal(req.KAL);
            //if(req.KM != null)
            //KM = Convert.ToDecimal(req.KM);
            //if(req.TotalKM != null)
            //TotalKM = Convert.ToDecimal(req.TotalKM);
            //if(req.ZS != null)
            //ZS = Convert.ToDecimal(req.ZS);
            //if(req.Time != null)
            //Time = req.Time;
            //if(req.TotalTime != null)
            //TotalTime = Convert.ToInt32(req.TotalTime);
            //if(req.TotalKAL != null)
            //TotalKAL = Convert.ToDecimal(req.TotalKAL);
            //if(req.WATT != null)
            //WATT = Convert.ToDecimal(req.WATT);
            //if(req.UserName != null)
            //UserName = req.UserName;
            //if(req.Url != null)
            //Url = req.Url;
        //}
        /// <summary>
        /// 
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String deviceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? gymId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? XL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? SD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? KAL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? KM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? TotalKM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? ZS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? TotalTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? TotalKAL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? WATT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Url { get; set; }

}
}
