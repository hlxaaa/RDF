using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Auth
    {
        public Auth(){}
        //public Auth(Req req){
            //if(req.id != null)
            //id = Convert.ToInt32(req.id);
            //if(req.userId != null)
            //userId = Convert.ToInt32(req.userId);
            //if(req.menuId != null)
            //menuId = Convert.ToInt32(req.menuId);
        //}
        /// <summary>
        /// 
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? menuId { get; set; }

}
}
