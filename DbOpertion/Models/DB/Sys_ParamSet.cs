using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Sys_ParamSet
    {
        public Sys_ParamSet(){}
        //public Sys_ParamSet(Req req){
            //if(req.key != null)
            //key = req.key;
            //if(req.value != null)
            //value = req.value;
            //if(req.remark != null)
            //remark = req.remark;
            //if(req.uuid != null)
            //uuid = req.uuid;
            //if(req.type != null)
            //type = Convert.ToInt32(req.type);
        //}
        /// <summary>
        /// key
        /// </summary>
        public String key { get; set; }
        /// <summary>
        /// value
        /// </summary>
        public String value { get; set; }
        /// <summary>
        /// remark
        /// </summary>
        public String remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String uuid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? type { get; set; }

}
}
