using System;
using Model;

namespace WcfService.Sys
{
    /// <summary>
    /// 数据
    /// </summary>
    public class Sys_Data: BaseModel
    {
        public Sys_Data()
        {
            Cfg = new Sys_DataCfg();
        }
        /// <summary>
        ///用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateTime{get;set;}
        /// <summary>
        ///心率
        /// </summary>
        public decimal XL{get;set;}
        /// <summary>
        ///速度
        /// </summary>
        public decimal SD { get; set; }
        /// <summary>
        ///卡路里
        /// </summary>
        public decimal KAL { get; set; }
        /// <summary>
        ///里程
        /// </summary>
        public decimal KM { get; set; }
        /// <summary>
        ///总里程
        /// </summary>
        public decimal TotalKM { get; set; }
        /// <summary>
        ///转速
        /// </summary>
        public decimal ZS { get; set; }
        /// <summary>
        ///时间
        /// </summary>
        public string Time{get;set;}

    }
}
