using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class AndroidVersion
    {
        public AndroidVersion(){}
        //public AndroidVersion(Req req){
            //if(req.id != null)
            //id = Convert.ToInt32(req.id);
            //if(req.versionCode != null)
            //versionCode = req.versionCode;
            //if(req.versionName != null)
            //versionName = req.versionName;
            //if(req.apkFileUrl != null)
            //apkFileUrl = req.apkFileUrl;
            //if(req.updateLog != null)
            //updateLog = req.updateLog;
            //if(req.targetSize != null)
            //targetSize = req.targetSize;
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
        public String versionCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String versionName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String apkFileUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String updateLog { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String targetSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? editTime { get; set; }

}
}
