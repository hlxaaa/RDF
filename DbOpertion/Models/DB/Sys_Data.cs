using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Sys_Data
    {
        public Sys_Data(){}
        //public Sys_Data(Req req){
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
        //}
        /// <summary>
        /// 用户Id
        /// </summary>
        public Int32? UserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 心率
        /// </summary>
        public Decimal? XL { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public Decimal? SD { get; set; }
        /// <summary>
        /// 卡路里
        /// </summary>
        public Decimal? KAL { get; set; }
        /// <summary>
        /// 里程
        /// </summary>
        public Decimal? KM { get; set; }
        /// <summary>
        /// 总里程
        /// </summary>
        public Decimal? TotalKM { get; set; }
        /// <summary>
        /// 转速
        /// </summary>
        public Decimal? ZS { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public String Time { get; set; }
        /// <summary>
        /// 运动总时间（单位：秒
        /// </summary>
        public Int32? TotalTime { get; set; }
        /// <summary>
        /// 消耗总卡路里
        /// </summary>
        public Decimal? TotalKAL { get; set; }
        /// <summary>
        /// 瓦特
        /// </summary>
        public Decimal? WATT { get; set; }

}
}
