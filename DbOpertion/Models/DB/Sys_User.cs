using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class Sys_User
    {
        public Sys_User() { }
        //public Sys_User(Req req){
        //if(req.Id != null)
        //Id = Convert.ToInt32(req.Id);
        //if(req.Type != null)
        //Type = Convert.ToInt32(req.Type);
        //if(req.UserPwd != null)
        //UserPwd = req.UserPwd;
        //if(req.UserName != null)
        //UserName = req.UserName;
        //if(req.Phone != null)
        //Phone = req.Phone;
        //if(req.UId != null)
        //UId = req.UId;
        //if(req.UserExplain != null)
        //UserExplain = req.UserExplain;
        //if(req.Url != null)
        //Url = req.Url;
        //if(req.Birthday != null)
        //Birthday = Convert.ToDateTime(req.Birthday);
        //if(req.Sex != null)
        //Sex = Convert.ToBoolean(req.Sex);
        //if(req.Height != null)
        //Height = Convert.ToDecimal(req.Height);
        //if(req.Weight != null)
        //Weight = Convert.ToDecimal(req.Weight);
        //if(req.IdealWeight != null)
        //IdealWeight = Convert.ToDecimal(req.IdealWeight);
        //if(req.RegisterTime != null)
        //RegisterTime = Convert.ToDateTime(req.RegisterTime);
        //if(req.LoginTime != null)
        //LoginTime = Convert.ToDateTime(req.LoginTime);
        //if(req.Enabled != null)
        //Enabled = Convert.ToBoolean(req.Enabled);
        //if(req.UsePlace != null)
        //UsePlace = req.UsePlace;
        //if(req.Address != null)
        //Address = req.Address;
        //if(req.frequency != null)
        //frequency = req.frequency;
        //if(req.KeyName != null)
        //KeyName = req.KeyName;
        //if(req.account != null)
        //account = req.account;
        //if(req.coachImg != null)
        //coachImg = req.coachImg;
        //if(req.isPass != null)
        //isPass = req.isPass;
        //if(req.livePrice != null)
        //livePrice = Convert.ToDecimal(req.livePrice);
        //}
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }
        ///// <summary>
        ///// 类型(0平台账号1教练账号2用户账号
        ///// </summary>
        //public Int32 Type { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public String UserPwd { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public String UserName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public String Phone { get; set; }
        /// <summary>
        /// UID
        /// </summary>
        public String UId { get; set; }
        /// <summary>
        /// 个性签名
        /// </summary>
        public String UserExplain { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public String Url { get; set; }
        ///// <summary>
        ///// 生日
        ///// </summary>
        //public DateTime Birthday { get; set; }
        ///// <summary>
        ///// 性别
        ///// </summary>
        //public Boolean Sex { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public Decimal? Height { get; set; }
        /// <summary>
        /// 体重
        /// </summary>
        public Decimal? Weight { get; set; }
        /// <summary>
        /// 目标体重
        /// </summary>
        public Decimal? IdealWeight { get; set; }
        ///// <summary>
        ///// 注册时间
        ///// </summary>
        //public DateTime RegisterTime { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LoginTime { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        public Boolean? Enabled { get; set; }
        /// <summary>
        /// 使用地址
        /// </summary>
        public String UsePlace { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public String Address { get; set; }
        /// <summary>
        /// 使用频率，不知道具体怎么弄，先放个字段
        /// </summary>
        public String frequency { get; set; }
        /// <summary>
        /// 设备ID，蓝牙名
        /// </summary>
        public String KeyName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public String account { get; set; }
        /// <summary>
        /// 教练证图片
        /// </summary>
        public String coachImg { get; set; }
        /// <summary>
        /// 教练审核结果0申请中，1通过，字符串：未通过原因
        /// </summary>
        public String isPass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String qqUId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String wxUId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String fbUId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String ttUId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String idCardFront { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String idCardBack { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public Decimal? balance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? isEnglish { get; set; }
    }
}
