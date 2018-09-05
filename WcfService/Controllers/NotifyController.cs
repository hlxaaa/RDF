using Aop.Api.Util;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Xml;

namespace WcfService.Controllers
{
    public class NotifyController : ApiController
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

        #region 支付宝支付异步通知
        //string sellerId = "";
        string sellerId = ConfigurationManager.AppSettings["sellerId"];
        //string appId = "";
        string appId = ConfigurationManager.AppSettings["aliAppId"];
        string alipayPublicKey = ConfigurationManager.AppSettings["alipay_public_key"];

        /// <summary>
        /// 支付宝支付订单异步通知
        /// </summary>
        /// <returns></returns>
        public string alipayNotify()
        {
            string success = "success";
            string failure = "failure";
            try
            {
                var dict2 = GetRequestPost();
                var out_trade_no = dict2["out_trade_no"];
                var orderList = SqlHelper.Instance.GetByCondition<PayRecord>($" outTradeNo='{out_trade_no}' ");
                if (orderList.Count < 0)
                    return failure;
                var order = orderList.First();
                if (order.status == 1)
                    return success;
                var outTradeNo = CheckAliPay(dict2, order.money.ToString());

                LogHelper.WriteLog(typeof(NotifyController), "outTradeNo=====" + outTradeNo);

                if (outTradeNo == null)
                    return failure;

                order.payType = 0;
                order.status = 1;

                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction($"alipay{order.id}");
                try
                {
                    PayRecordOper.Instance.Update(order, conn, tran);
                    var user = Sys_UserOper.Instance.GetById((int)order.coachId);
                    user.balance += order.money;
                    Sys_UserOper.Instance.Update(user, conn, tran);

                    tran.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    conn.Close();
                    throw new Exception($"orderId={order.id}支付失败，原因：" + ex.Message);
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(typeof(NotifyController), e.Message);
                return failure;
            }
            return success;
        }

        /// <summary>
        /// 检查阿里支付。正确返回订单号，错误返回null
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="Total_amount"></param>
        /// <returns></returns>
        public string CheckAliPay(IDictionary<string, string> dict, string Total_amount)
        {
            bool flag = AlipaySignature.RSACheckV1(dict, alipayPublicKey, "utf-8", "RSA2", false);

            //txyInfo info = new txyInfo();
            //info.content = JsonConvert.SerializeObject(dict + "==flag===" + flag);
            //txyInfoOper.Instance.Add(info);

            LogHelper.WriteLog(typeof(NotifyController), JsonConvert.SerializeObject(dict) + $"==flag=={flag}");

            if (flag)
            {
                var total_amount = Convert.ToDecimal(dict["total_amount"]);
                var out_trade_no = dict["out_trade_no"];
                string status = dict["trade_status"];
                string seller_id = dict["seller_id"];
                string app_id = dict["app_id"];

                if (total_amount == Convert.ToDecimal(Total_amount) && seller_id == sellerId && app_id == appId)
                {
                    switch (status)
                    {
                        case "TRADE_SUCCESS":
                            return out_trade_no;
                    }
                }
            }
            return null;
        }

        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组 
        /// request回来的信息组成的数组
        public IDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = HttpContext.Current.Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], HttpContext.Current.Request.Form[requestItem[i]]);
            }

            return sArray;
        }

        #endregion

        #region 微信支付异步通知

        /// <summary>
        /// 微信商户密钥
        /// </summary>
        string mchKey = ConfigurationManager.AppSettings["wxMchKey"].ToString();

        /// <summary>
        /// 微信支付订单异步通知
        /// </summary>
        /// <returns></returns>
        public string wxpayNotify()
        {
            var success = "<xml><return_code><![CDATA[SUCCESS]]></return_code>\n<return_msg><![CDATA[OK]]></return_msg></xml>";

            var error = "<xml><return_code><![CDATA[FAIL]]></return_code>\n<return_msg><![CDATA[error]]></return_msg></xml>";
            string xmlStr = getPostStr();

            var outTradeNo = WXCheck(xmlStr);
            LogHelper.WriteLog(typeof(NotifyController), "outTradeNo=====" + outTradeNo);

            if (outTradeNo == null)
                return error;
            var orderList = SqlHelper.Instance.GetByCondition<PayRecord>($" outTradeNo='{outTradeNo}' ");
            if (orderList.Count < 1)
                return error;
            var order = orderList.First();
            if (order.status == 1)
                return success;

            order.payType = 1;
            order.status = 1;

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction($"wxpay{order.id}");
            try
            {
                PayRecordOper.Instance.Update(order, conn, tran);
                var user = Sys_UserOper.Instance.GetById((int)order.coachId);
                user.balance += order.money;
                Sys_UserOper.Instance.Update(user, conn, tran);

                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                //throw new Exception(ex.Message);
                LogHelper.WriteLog(typeof(NotifyController), $"orderId={order.id}支付失败，原因：" + ex.Message);
                return error;
            }
            return success;
        }

        /// <summary>
        /// 获取 Post 提交的参数
        /// </summary>
        /// <returns></returns>
        public string getPostStr()
        {
            Int32 intLen = Convert.ToInt32(HttpContext.Current.Request.InputStream.Length);
            byte[] b = new byte[intLen];
            HttpContext.Current.Request.InputStream.Read(b, 0, intLen);
            return System.Text.Encoding.UTF8.GetString(b);
        }

        /// <summary>
        /// 成功返回订单号out_trade_no 错误返回 null
        /// </summary>
        /// <param name="xmlStr"></param>
        /// <returns></returns>
        public string WXCheck(string xmlStr)
        {
            try
            {
                var signFromWX = "";
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmlStr);
                var dict = new Dictionary<string, string>();
                foreach (XmlNode item in xml.FirstChild)
                {
                    if (item.Name != "sign")
                        dict.Add(item.Name, item.InnerText);
                    else
                        signFromWX = item.InnerText;
                }

                string sign = StringHelper.Instance.GetWXSign(dict, mchKey);
                if (dict["result_code"] == "SUCCESS")
                {
                    //交易成功
                    string out_trade_no = dict["out_trade_no"];//商户订单号
                    if (sign != signFromWX)
                        return null;
                    return out_trade_no;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(typeof(NotifyController), "WXCheck_Error:" + e.Message);
            }
            return null;
        }

        #endregion
    }
}
