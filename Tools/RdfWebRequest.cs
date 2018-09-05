using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Tools
{
    /// <summary>
    /// HTTP GET POST请求
    /// </summary>
    public class RdfWebRequest
    {
        /// <summary>
        /// HttpWebRequest Get
        /// </summary>
        /// <param name="urlParam">地址+参数</param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, object> dic)
        {
            string param = AppendParam(dic);
            if (!string.IsNullOrWhiteSpace(param))
                param = "?" + param;
            return Get(url + param);
        }
        /// <summary>
        /// HttpWebRequest Get
        /// </summary>
        /// <param name="urlParam">地址+参数</param>
        /// <returns></returns>
        public static string Get(string urlParam)
        {
            System.GC.Collect();
            string result = "";
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                //if (urlParam.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                //{
                //    ServicePointManager.ServerCertificateValidationCallback =
                //            new RemoteCertificateValidationCallback(CheckValidationResult);
                //}

                //下面设置HttpWebRequest的相关属性
                request = (HttpWebRequest)WebRequest.Create(urlParam);
                request.Method = "GET";
                request.Timeout = 10000;

                //设置代理
                //WebProxy proxy = new WebProxy();
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);
                //request.Proxy = proxy;

                //获取服务器返回
                response = (HttpWebResponse)request.GetResponse();

                //获取HTTP返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (Exception ex)
            {
                RdfLog.WriteException(ex, "HttpWebRequestGet异常:" + urlParam);
                throw ex;
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }
        /// <summary>
        /// HttpWebRequest Post param json
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="jsonParam">参数</param>
        /// <returns></returns>
        public static string PostJson(string url, string jsonParam)
        {
            return Post(url, "application/json", jsonParam);
        }
        /// <summary>
        /// HttpWebRequest Post param dic
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string PostDic(string url, Dictionary<string, object> dic)
        {
            return Post(url, "application/x-www-form-urlencoded", AppendParam(dic));
        }
        /// <summary>
        /// 拼接参数
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private static string AppendParam(Dictionary<string, object> dic)
        {
            if (dic == null)
                return "";
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    sb.Append("&");
                sb.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            return sb.ToString();
        }
        /// <summary>
        /// HttpWebRequest Post param dic
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string Post(string url, string contentType, string param)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接
            string result = "";//返回结果
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                //if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                //{
                //    ServicePointManager.ServerCertificateValidationCallback =
                //            new RemoteCertificateValidationCallback(CheckValidationResult);
                //}

                //下面设置HttpWebRequest的相关属性
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = 10000;

                //设置代理服务器
                //WebProxy proxy = new WebProxy();                          //定义一个网关对象
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);              //网关服务器端口:端口
                //request.Proxy = proxy;

                //设置POST的数据类型和长度
                request.ContentType = contentType;
                byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(param);
                request.ContentLength = data.Length;

                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (Exception ex)
            {
                RdfLog.WriteException(ex, "HttpWebRequestPost URL:" + url + " param:" + param);
                throw ex;
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }
        /// <summary>
        /// HttpWebRequest Get
        /// </summary>
        /// <param name="urlParam">地址+参数</param>
        /// <returns></returns>
        public static byte[] GetByte(string urlParam)
        {
            System.GC.Collect();
            byte[] bytes;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                request = (HttpWebRequest)WebRequest.Create(urlParam);
                request.Method = "GET";
                request.Timeout = 10000;
                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                long length = response.ContentLength;
                bytes = new byte[length];
                stream.Read(bytes, 0, (int)length);
                stream.Close();
            }
            catch (Exception ex)
            {
                RdfLog.WriteException(ex, "HttpWebRequestGet异常:" + urlParam);
                throw ex;
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return bytes;
        }
    }
}
