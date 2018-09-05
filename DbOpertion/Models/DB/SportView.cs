using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class SportView
    {
        public SportView(){}
        //public SportView(Req req){
            //if(req.Id != null)
            //Id = Convert.ToInt32(req.Id);
            //if(req.Title != null)
            //Title = req.Title;
            //if(req.TitleUrl != null)
            //TitleUrl = req.TitleUrl;
            //if(req.TypeId != null)
            //TypeId = Convert.ToInt32(req.TypeId);
            //if(req.CreateTime != null)
            //CreateTime = Convert.ToDateTime(req.CreateTime);
            //if(req.Content != null)
            //Content = req.Content;
            //if(req.Remark != null)
            //Remark = req.Remark;
            //if(req.Enabled != null)
            //Enabled = Convert.ToBoolean(req.Enabled);
            //if(req.DataType != null)
            //DataType = Convert.ToInt32(req.DataType);
            //if(req.ContentE != null)
            //ContentE = req.ContentE;
            //if(req.RemarkE != null)
            //RemarkE = req.RemarkE;
            //if(req.TitleE != null)
            //TitleE = req.TitleE;
            //if(req.typeName != null)
            //typeName = req.typeName;
        //}
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String TitleUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32 TypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Boolean Enabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32 DataType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String ContentE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String RemarkE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String TitleE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String typeName { get; set; }

}
}
