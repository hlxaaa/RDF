using System;
using System.Runtime.Serialization;

namespace Tools
{
    /// <summary>
    /// 消息类
    /// </summary>
    [Serializable]
    [DataContract]
    public class RdfMsg
    {
        public RdfMsg(bool success)
        {
            Success = success;
            Error = "";
            Result = "";
            Value = "";
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="success">状态</param>
        /// <param name="str">true Result,false Error</param>
        public RdfMsg(bool success, object str)
        {
            Success = success;
            Error = success ? "" : str;
            Result = success ? str : "";
            Value = "";
        }
        public RdfMsg(bool success, object result, object value, string error = "")
        {
            Success = success;
            Error = error;
            Result = result;
            Value = value;
        }
        /// <summary>
        /// 成功与否
        /// </summary>
        [DataMember]
        public bool Success { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        [DataMember]
        public object Error { get; set; }
        /// <summary>
        /// 消息正文
        /// </summary>
        [DataMember]
        public object Result { get; set; }
        /// <summary>
        /// 消息正文2
        /// </summary>
        [DataMember]
        public object Value { get; set; }
        /// <summary>
        /// 转json
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return RdfSerializer.ObjToJson(this);
        }
    }
}
