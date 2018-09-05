using System;
using Model;

namespace WcfService.Sys
{
    /// <summary>
    /// 用户菜单权限
    /// </summary>
    public class Sys_UserAuth: BaseModel
    {
        public Sys_UserAuth()
        {
            Cfg = new Sys_UserAuthCfg();
        }
        /// <summary>
        ///用户Id
        /// </summary>
        public int UserId{get;set;}
        /// <summary>
        ///菜单Id
        /// </summary>
        public int MenuId{get;set;}

        public string MenuName { get; set; }

    }
}
