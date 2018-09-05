using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class RegistrationAgreement
    {
        public RegistrationAgreement(){}
        //public RegistrationAgreement(Req req){
            //if(req.id != null)
            //id = Convert.ToInt32(req.id);
            //if(req.content != null)
            //content = req.content;
            //if(req.editTime != null)
            //editTime = Convert.ToDateTime(req.editTime);
        //}
        /// <summary>
        /// 
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? editTime { get; set; }

}
}
