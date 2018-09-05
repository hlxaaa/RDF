using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Sys_SportType
    {
        public Sys_SportType(){}
        //public Sys_SportType(Req req){
            //if(req.Id != null)
            //Id = Convert.ToInt32(req.Id);
            //if(req.Name != null)
            //Name = req.Name;
            //if(req.Remark != null)
            //Remark = req.Remark;
            //if(req.Enabled != null)
            //Enabled = Convert.ToBoolean(req.Enabled);
        //}
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public String Remark { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        public Boolean Enabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? isEnglish { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String NameE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RemarkE { get; set; }
    }
}
