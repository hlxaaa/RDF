using System;
using Model;
using Tools;
using System.Collections.Generic;
using System.Linq;

namespace WcfService.Sys
{
    /// <summary>
    /// 用户
    /// </summary>
    public class Sys_User : BaseModel
    {
        public Sys_User(DbOpertion.Models.Sys_User user)
        {
            Cfg = new Sys_UserCfg();
            Bll = new Sys_UserBll(this);

            Id = user.Id;
            Type = (int)user.Type;
            UserPwd = user.UserPwd;
            UserName = user.UserName;
            Phone = user.Phone;
            UserExplain = user.UserExplain;
            Url = user.Url;
            Birthday = Convert.ToDateTime(user.Birthday);
            Sex = (bool)user.Sex;
            Height = (decimal)user.Height;
            Weight = (decimal)user.Weight;
            if (user.IdealWeight != null)
                IdealWeight = (decimal)user.IdealWeight;
            else
                IdealWeight = 0;
            RegisterTime = Convert.ToDateTime(user.RegisterTime);
            LoginTime = Convert.ToDateTime(user.LoginTime);
            Enabled = (bool)user.Enabled;
            isEnglish = user.isEnglish;
        }

        public Sys_User()
        {
            Cfg = new Sys_UserCfg();
            Bll = new Sys_UserBll(this);
        }
        /// <summary>
        ///主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///类型 0管理员 1教练 2app用户
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        ///密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        ///姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        ///手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        ///UID QQ或微信用户唯一标识 ,后台登录的账号
        /// </summary>
        public string UId { get; set; }
        /// <summary>
        ///个性签名
        /// </summary>
        public string UserExplain { get; set; }
        /// <summary>
        ///头像
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        ///生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        ///性别
        /// </summary>
        public bool Sex { get; set; }
        /// <summary>
        ///身高
        /// </summary>
        public decimal Height { get; set; }
        /// <summary>
        ///体重
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// 目标体重
        /// </summary>
        public decimal IdealWeight { get; set; }
        /// <summary>
        ///注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 最后操作时间
        /// </summary>
        [RdfNonSerialized]
        public DateTime OpertionTime { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        public string Menus { get; set; }
        /// <summary>
        /// 总里程
        /// </summary>
        public int? isEnglish { get; set; }
        public decimal TotalKM { get; set; }
    }
}
