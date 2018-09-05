using System;

namespace DbOpertion.Models
{

    public partial class Sys_User
    {
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
        //}
    
        /// <summary>
        /// 类型(0平台账号1教练账号2用户账号
        /// </summary>
        public Int32? Type { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Boolean? Sex { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }

        ///// <summary>
        ///// 启用
        ///// </summary>
        //public Boolean? Enabled { get; set; }


}
}
