using System;
using Model;

namespace WcfService.Sys
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Sys_Menu: BaseModel
    {
        public Sys_Menu()
        {
            Cfg = new Sys_MenuCfg();
        }
        /// <summary>
        ///主键
        /// </summary>
        public int Id{get;set;}
        /// <summary>
        ///菜单编号
        /// </summary>
        public string MenuCode{get;set;}
        /// <summary>
        ///菜单名称
        /// </summary>
        public string MenuName{get;set;}
        /// <summary>
        ///菜品地址
        /// </summary>
        public string MenuUrl{get;set;}
        /// <summary>
        ///序号
        /// </summary>
        public int Sort{get;set;}

    }
}
