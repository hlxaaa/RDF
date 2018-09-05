using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class DragModel
    {
        public DragModel(){}
        //public DragModel(Req req){
            //if(req.id != null)
            //id = Convert.ToInt32(req.id);
            //if(req.modelType != null)
            //modelType = Convert.ToInt32(req.modelType);
            //if(req.modelName != null)
            //modelName = req.modelName;
            //if(req.createTime != null)
            //createTime = Convert.ToDateTime(req.createTime);
            //if(req.isDeleted != null)
            //isDeleted = Convert.ToBoolean(req.isDeleted);
            //if(req.content != null)
            //content = req.content;
            //if(req.isEnglish != null)
            //isEnglish = Convert.ToInt32(req.isEnglish);
            //if(req.modelNameE != null)
            //modelNameE = req.modelNameE;
        //}
        /// <summary>
        /// 
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        /// 0里程模式1卡路里模式2时间模式
        /// </summary>
        public Int32 modelType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String modelName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Boolean? isDeleted { get; set; }
        /// <summary>
        /// [{"m":"100","n":"60"}]
        /// </summary>
        public String content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? isEnglish { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String modelNameE { get; set; }

}
}
