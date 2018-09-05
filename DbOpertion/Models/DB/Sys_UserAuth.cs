using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Sys_UserAuth
    {
        public Sys_UserAuth(){}
        //public Sys_UserAuth(Req req){
            //if(req.UserId != null)
            //UserId = Convert.ToInt32(req.UserId);
            //if(req.MenuId != null)
            //MenuId = Convert.ToInt32(req.MenuId);
        //}
        /// <summary>
        /// 用户Id
        /// </summary>
        public Int32 UserId { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        public Int32 MenuId { get; set; }

}
}
