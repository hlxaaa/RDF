using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class authView
    {
        public authView(){}
        //public authView(Req req){
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
            //if(req.qqUId != null)
            //qqUId = req.qqUId;
            //if(req.wxUId != null)
            //wxUId = req.wxUId;
            //if(req.fbUId != null)
            //fbUId = req.fbUId;
            //if(req.ttUId != null)
            //ttUId = req.ttUId;
            //if(req.idCardFront != null)
            //idCardFront = req.idCardFront;
            //if(req.idCardBack != null)
            //idCardBack = req.idCardBack;
            //if(req.balance != null)
            //balance = Convert.ToDecimal(req.balance);
            //if(req.isEnglish != null)
            //isEnglish = Convert.ToInt32(req.isEnglish);
            //if(req.MenuId != null)
            //MenuId = Convert.ToInt32(req.MenuId);
            //if(req.menuName != null)
            //menuName = req.menuName;
        //}
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32 Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String UserPwd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String UId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String UserExplain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Boolean Sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? Height { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? Weight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? IdealWeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime RegisterTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LoginTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Boolean? Enabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String UsePlace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String frequency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String KeyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String account { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String coachImg { get; set; }
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public Decimal? balance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? isEnglish { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? MenuId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String menuName { get; set; }

}
}
