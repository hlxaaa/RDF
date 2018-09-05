using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Sys_Menu
    {
        public Sys_Menu(){}
        //public Sys_Menu(Req req){
            //if(req.Id != null)
            //Id = Convert.ToInt32(req.Id);
            //if(req.MenuCode != null)
            //MenuCode = req.MenuCode;
            //if(req.MenuName != null)
            //MenuName = req.MenuName;
            //if(req.MenuUrl != null)
            //MenuUrl = req.MenuUrl;
            //if(req.Sort != null)
            //Sort = Convert.ToInt32(req.Sort);
        //}
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }
        /// <summary>
        /// 菜单编号
        /// </summary>
        public String MenuCode { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public String MenuName { get; set; }
        /// <summary>
        /// 菜品地址
        /// </summary>
        public String MenuUrl { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public Int32 Sort { get; set; }

}
}
