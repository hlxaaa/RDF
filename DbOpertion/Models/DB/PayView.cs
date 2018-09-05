using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class PayView
    {
        public PayView(){}
        //public PayView(Req req){
            //if(req.id != null)
            //id = Convert.ToInt32(req.id);
            //if(req.outTradeNo != null)
            //outTradeNo = req.outTradeNo;
            //if(req.coachId != null)
            //coachId = Convert.ToInt32(req.coachId);
            //if(req.userId != null)
            //userId = Convert.ToInt32(req.userId);
            //if(req.type != null)
            //type = Convert.ToInt32(req.type);
            //if(req.money != null)
            //money = Convert.ToDecimal(req.money);
            //if(req.status != null)
            //status = Convert.ToInt32(req.status);
            //if(req.createTime != null)
            //createTime = Convert.ToDateTime(req.createTime);
            //if(req.account != null)
            //account = req.account;
            //if(req.accountName != null)
            //accountName = req.accountName;
            //if(req.payType != null)
            //payType = Convert.ToInt32(req.payType);
            //if(req.payTypeName != null)
            //payTypeName = req.payTypeName;
            //if(req.typeName != null)
            //typeName = req.typeName;
            //if(req.payUserName != null)
            //payUserName = req.payUserName;
            //if(req.coachName != null)
            //coachName = req.coachName;
            //if(req.isEnglish != null)
            //isEnglish = Convert.ToInt32(req.isEnglish);
        //}
        /// <summary>
        /// 
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String outTradeNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? coachId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String account { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String accountName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? payType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String payTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String typeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String payUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String coachName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? isEnglish { get; set; }

}
}
