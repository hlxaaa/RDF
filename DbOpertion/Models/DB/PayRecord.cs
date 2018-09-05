using System;

namespace DbOpertion.Models
{
    [Serializable]
    public partial class PayRecord
    {
        public PayRecord(){}
        //public PayRecord(Req req){
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
            //if(req.videoId != null)
            //videoId = Convert.ToInt32(req.videoId);
            //if(req.liveId != null)
            //liveId = Convert.ToInt32(req.liveId);
            //if(req.payType != null)
            //payType = Convert.ToInt32(req.payType);
            //if(req.account != null)
            //account = req.account;
            //if(req.accountName != null)
            //accountName = req.accountName;
        //}
        /// <summary>
        /// 
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        /// 第三方支付订单号
        /// </summary>
        public String outTradeNo { get; set; }
        /// <summary>
        /// 得到钱的那一方（平台id0）
        /// </summary>
        public Int32? coachId { get; set; }
        /// <summary>
        /// 花钱的用户的id
        /// </summary>
        public Int32? userId { get; set; }
        /// <summary>
        /// 0课程收入，1直播收入，99申请提现
        /// </summary>
        public Int32? type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal? money { get; set; }
        /// <summary>
        /// 0未完成1完成
        /// </summary>
        public Int32? status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? videoId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int32? liveId { get; set; }
        /// <summary>
        /// 0 支付宝 1微信
        /// </summary>
        public Int32? payType { get; set; }
        /// <summary>
        /// 提现的账号
        /// </summary>
        public String account { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String accountName { get; set; }

}
}
