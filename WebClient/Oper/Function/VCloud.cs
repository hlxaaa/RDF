using System;
//using Tools;
using System.Net;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace ServerEnd.Oper.Function
{
    public class VCloud
    {
        private static string AppKey = "";
        private static string Secret = "";

        /// <summary>
        /// 创建频道
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public V_Create_Msg Create(string name)
        {
            name += "-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                string json = Post("https://vcloud.163.com/app/channel/create", "{\"name\":\"" + name + "\", \"type\":0}");
                //return RdfSerializer.Deserialize<V_Create_Msg>(json);
                return JsonConvert.DeserializeObject<V_Create_Msg>(json);
            }
            catch (Exception ex)
            {
                return new V_Create_Msg() { msg = "创建频道异常:" + ex.Message };
            }
        }
        /// <summary>
        /// 删除频道
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public V_Delete_Msg Delete(string cid)
        {
            try
            {
                string json = Post("https://vcloud.163.com/app/channel/delete", "{\"cid\":\"" + cid + "\"}");
                //return RdfSerializer.Deserialize<V_Delete_Msg>(json);
                return JsonConvert.DeserializeObject<V_Delete_Msg>(json);
            }
            catch (Exception ex)
            {
                return new V_Delete_Msg() { msg = "删除频道异常:" + ex.Message };
            }
        }
        /// <summary>
        /// 获取频道
        /// </summary>
        /// <param name="cid"></param>
        public V_Entity_Msg GetEntity(string cid)
        {
            try
            {
                string json = Post("https://vcloud.163.com/app/channelstats", "{\"cid\":\"" + cid + "\"}");
                //return RdfSerializer.Deserialize<V_Entity_Msg>(json);
                return JsonConvert.DeserializeObject<V_Entity_Msg>(json);

            }
            catch (Exception ex)
            {
                return new V_Entity_Msg() { msg = "获取频道异常:" + ex.Message };
            }
        }
        /// <summary>
        /// 获取推流和播放地址
        /// </summary>
        /// <param name="cid"></param>
        public V_Create_Msg GetAddress(string cid)
        {
            try
            {
                string json = Post("https://vcloud.163.com/app/address", "{\"cid\":\"" + cid + "\"}");
                //return RdfSerializer.Deserialize<V_Create_Msg>(json);
                return JsonConvert.DeserializeObject<V_Create_Msg>(json);
            }
            catch (Exception ex)
            {
                return new V_Create_Msg() { msg = "获取地址异常:" + ex.Message };
            }
        }
        /// <summary>
        /// 禁用频道
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public V_Msg Pause(string cid)
        {
            try
            {
                string json = Post("https://vcloud.163.com/app/channel/pause", "{\"cid\":\"" + cid + "\"}");
                //return RdfSerializer.Deserialize<V_Msg>(json);
                return JsonConvert.DeserializeObject<V_Msg>(json);
            }
            catch (Exception ex)
            {
                return new V_Msg() { msg = "禁用频道异常:" + ex.Message };
            }
        }
        /// <summary>
        /// 恢复频道
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public V_Msg Resume(string cid)
        {
            try
            {
                string json = Post("https://vcloud.163.com/app/channel/resume", "{\"cid\":\"" + cid + "\"}");
                //return RdfSerializer.Deserialize<V_Msg>(json);
                return JsonConvert.DeserializeObject<V_Msg>(json);
            }
            catch (Exception ex)
            {
                return new V_Msg() { msg = "恢复频道异常:" + ex.Message };
            }
        }
        /// <summary>
        /// 设置直播状态回调地址
        /// </summary>
        /// <param name="address"></param>
        public void SetCallBack(string address)
        {
            try
            {
                string json = Post("https://vcloud.163.com/app/chstatus/setcallback", "{\"chStatusClk\":\"" + address + "\"}");

            }
            catch (Exception ex)
            {

            }
        }

        public string SHA1(string content)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_in = Encoding.UTF8.GetBytes(content);
            byte[] bytes_out = sha1.ComputeHash(bytes_in);
            sha1.Dispose();
            return BitConverter.ToString(bytes_out);
        }

        private string Post(string url, string json)
        {
            if (string.IsNullOrWhiteSpace(AppKey))
            {
                string live = System.Configuration.ConfigurationManager.AppSettings["LiveParam"];
                if (string.IsNullOrWhiteSpace(live))
                    throw new Exception("未配置直播参数!");
                string[] array = live.Split(',');
                if (array.Length != 2)
                    throw new Exception("直播参数配置错误!");
                AppKey = array[0];
                Secret = array[1];
            }
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接
            string result = "";//返回结果
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = 10000;
                TimeSpan timespan = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);

                string nonce = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string curTime = Convert.ToInt64(timespan.TotalSeconds).ToString();
                string checkSum = SHA1(Secret + nonce + curTime).Replace("-", "").ToLower();

                request.Headers.Add("AppKey", AppKey);
                request.Headers.Add("Nonce", nonce);
                request.Headers.Add("CurTime", curTime);
                request.Headers.Add("CheckSum", checkSum);

                //设置POST的数据类型和长度
                request.ContentType = "application/json;charset=utf-8";
                byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(json);
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
    }
    /// <summary>
    /// 消息类
    /// </summary>
    public class V_Msg
    {
        /// <summary>
        /// 状态编
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 请求id
        /// </summary>
        public string requestId { get; set; }
    }
    /// <summary>
    /// 结果类
    /// </summary>
    public class V_Ret
    {
        /// <summary>
        /// 频道名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 创建频道的时间戳
        /// </summary>
        public long ctime { get; set; }
        /// <summary>
        /// 频道ID
        /// </summary>
        public string cid { get; set; }
    }
    /// <summary>
    /// 删除直播消息类
    /// </summary>
    public class V_Delete_Msg : V_Msg
    {
        /// <summary>
        /// 请求结果
        /// </summary>
        public object ret { get; set; }
    }

    /// <summary>
    /// 创建直播消息类
    /// </summary>
    public class V_Create_Msg : V_Msg
    {
        /// <summary>
        /// 请求结果
        /// </summary>
        public V_Create_Ret ret { get; set; }
    }
    /// <summary>
    /// 创建直播结果
    /// </summary>
    public class V_Create_Ret : V_Ret
    {
        /// <summary>
        /// http拉流地址
        /// </summary>
        public string httpPullUrl { get; set; }
        /// <summary>
        /// hls拉流地址
        /// </summary>
        public string hlsPullUrl { get; set; }
        /// <summary>
        /// rtmp拉流地址
        /// </summary>
        public string rtmpPullUrl { get; set; }
        /// <summary>
        /// 推流地址
        /// </summary>
        public string pushUrl { get; set; }

    }
    /// <summary>
    /// 获取频道消息
    /// </summary>
    public class V_Entity_Msg : V_Msg
    {
        /// <summary>
        /// 请求结果
        /// </summary>
        public V_Entity_Ret ret { get; set; }
    }
    /// <summary>
    /// 获取频道信息
    /// </summary>
    public class V_Entity_Ret : V_Ret
    {
        /// <summary>
        /// 1-开启录制； 0-关闭录制
        /// </summary>
        public int needRecord { get; set; }
        /// <summary>
        /// 用户ID，是用户在网易云视频与通信业务的标识，用于与其他用户的业务进行区分。通常，用户不需关注和使用。
        /// </summary>
        public long uid { get; set; }
        /// <summary>
        /// 录制切片时长(分钟)，默认120分钟
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 频道状态（0：空闲； 1：直播； 2：禁用； 3：直播录制）
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 录制后文件名
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 1-flv； 0-mp4
        /// </summary>
        public int format { get; set; }
        /// <summary>
        /// 频道类型 ( 0 : rtmp, 1 : hls, 2 : http)
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 网易云内部维护用字段，用户不需关注。后续版本将删除，请勿调用
        /// </summary>
        public string recordStatus { get; set; }
    }
}