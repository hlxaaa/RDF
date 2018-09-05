using System;
using System.Collections.Generic;
using Tools;
using WcfService.Sys;

namespace WcfService
{
    public static class GlobalParam
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        [ThreadStatic]
        public static Sys_User User;
        /// <summary>
        /// 当前用户登录信息
        /// </summary>
        [ThreadStatic]
        public static string ToKen;
    }

}
