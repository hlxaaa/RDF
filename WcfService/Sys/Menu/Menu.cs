using Tools;
using System;
using Model;
using System.Linq;
using System.Collections.Generic;

namespace WcfService.Sys
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : BaseOpertion
    {
        public Menu() : base(2) { }

        /// <summary>
        /// 获取当前用户能看到的菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetUserMenu(dynamic param)
        {
            if (UserInfo.Type == 2)
            {
                return new RdfMsg(false, "您没有获取菜单的权限!");
            }
            else if (UserInfo.Type == 1)
            {
                List<Sys_Menu> list = new RdfSqlQuery<Sys_Menu>().Where(item => item.MenuCode == "03").ToList();
                if (list != null && list.Count > 0)
                    list[0].MenuUrl = "CoachInfo.html";
                return new RdfMsg(true, RdfSerializer.ObjToJson(list));
            }
            else if (UserInfo.UId=="admin")
            {
                return GetMenuList(param);   
            }
            else
            {
                var menus = new RdfSqlQuery<Sys_Menu>()
                    .JoinTable<Sys_UserAuth>((t1, t2) => t1.Id == t2.MenuId)
                    .Where<Sys_UserAuth>((t1, t2) => t2.UserId == UserInfo.Id)
                    .Select(t1 => new { t1.Id, t1.MenuCode, t1.MenuName, t1.MenuUrl })
                    .OrderBy(t1 => t1.Sort)
                    .ToList()
                    .Distinct();
                return new RdfMsg(true, RdfSerializer.ObjToJson(menus));
            }
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetMenuList(dynamic param)
        {
            return new RdfMsg(true, new RdfSqlQuery<Sys_Menu>().OrderBy(t1 => t1.Sort).ToJson());
        }
    }
}
