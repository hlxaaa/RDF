using System;

namespace Tools
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class RdfNonSerialized : Attribute
    {
        /// <summary>
        /// 序列化属性
        /// </summary>
        /// <param name="serialized">默认false,不序列化</param>
        /// <param name="filed">字段名称，空代表默认属性或字段名，可更改</param>
        /// <param name="format">格式化</param>
        public RdfNonSerialized(bool serialized = false, string filed = null, string format = null)
        {
            Serialized = serialized;
            Filed = filed;
            Format = format;
        }
        public bool Serialized { get; set; }
        public string Filed { get; set; }
        public string Format { get; set; }
    }
}
