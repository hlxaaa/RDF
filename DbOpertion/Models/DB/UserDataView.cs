using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class UserDataView
    {
        public UserDataView(){}
        //public UserDataView(Req req){
            //if(req.Id != null)
            //Id = Convert.ToInt32(req.Id);
            //if(req.UserName != null)
            //UserName = req.UserName;
            //if(req.Phone != null)
            //Phone = req.Phone;
            //if(req.Sex != null)
            //Sex = Convert.ToBoolean(req.Sex);
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
            //if(req.balance != null)
            //balance = Convert.ToDecimal(req.balance);
            //if(req.TotalKM != null)
            //TotalKM = Convert.ToDecimal(req.TotalKM);
            //if(req.TotalTime != null)
            //TotalTime = Convert.ToInt32(req.TotalTime);
            //if(req.TotalKal != null)
            //TotalKal = Convert.ToDecimal(req.TotalKal);
        //}
        /// <summary>
        /// 
        /// </summary>
        public Int32 Id { get; set; }
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
        public Boolean Sex { get; set; }
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
        public Decimal? balance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? TotalKM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? TotalTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? TotalKal { get; set; }

}
}
