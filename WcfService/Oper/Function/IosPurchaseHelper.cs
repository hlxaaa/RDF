using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WcfService.Oper.Function
{
    public class IosPurchaseHelper
    {
        public bool ValidateApplePay()
        {
            var isSandbox = true;
            //客户端post过来的参数
            string appleReceipt = HttpContext.Current.Request.Form["appleReceipt"]; //苹果内购的验证收据
            //string orderId = PayHelper.GetOrderIDByPrefix("AP");  //订单编号-txy到底要不要呢，可能是指后台存数据需要用的吧
            string amount = HttpContext.Current.Request.Form["amount"];             //金额
            string userId = HttpContext.Current.Request.Form["userId"];             //用户UserID

            // 验证参数
            if (appleReceipt.Length < 20)
            {
                return false;
            }

            string strJosn = string.Format("{{\"receipt-data\":\"{0}\"}}", appleReceipt);
            // 请求验证
            string strResult = CreatePostHttpResponse(strJosn, isSandbox);
            JObject obj = JObject.Parse(strResult);//using Newtonsoft.Json.Linq;

            // 判断是否购买成功
            if (obj["status"].ToString() == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string CreatePostHttpResponse(string datas, bool isSandbox = false)
        {
            //正式购买地址 沙盒购买地址
            string url_buy = "https://buy.itunes.apple.com/verifyReceipt";
            string url_sandbox = "https://sandbox.itunes.apple.com/verifyReceipt";
            string url = isSandbox ? url_sandbox : url_buy;

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(datas.ToString());
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();   //获取响应的字符串流
            StreamReader sr = new StreamReader(responseStream); //创建一个stream读取流
            var str = sr.ReadToEnd();
            sr.Close();
            responseStream.Close();
            return str.ToString();
        }

    }
}