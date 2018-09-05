using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Model;
using Tools;
using WcfService.Sys;
using System.Data;

namespace WcfService
{
    public class BaseOpertion
    {
        private Sys_User _userInfo;
        /// <summary>
        /// 用户
        /// </summary>
        protected Sys_User UserInfo
        {
            get { return _userInfo ?? (_userInfo = new Sys_User()); }
            set { _userInfo = value; }
        }
        private int _menuId;
        /// <summary>
        /// 菜单ID
        /// </summary>
        protected int MenuId
        {
            get
            {
                return _menuId;
            }
            set
            {
                _menuId = value;
            }
        }

        public BaseOpertion(int menuId)
        {
            MenuId = menuId;
            UserInfo = GlobalParam.User;
        }
    }

}
