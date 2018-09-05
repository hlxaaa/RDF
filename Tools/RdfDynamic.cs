using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Tools
{
    /// <summary>
    /// 动态对象
    /// </summary>
    public class RdfDynamic : DynamicObject
    {
        public RdfDynamic() { }
        /// <summary>
        /// json对象转动态对象
        /// </summary>
        /// <param name="json"></param>
        public RdfDynamic(string json)
        {
            Dic = RdfSerializer.Deserialize<Dictionary<string, object>>(json);
        }
        /// <summary>
        /// 键值对集合转动态对象
        /// </summary>
        /// <param name="dictionary"></param>
        public RdfDynamic(Dictionary<string, object> dictionary)
        {
            Dic = dictionary;
        }
        private Dictionary<string, object> _dic;
        private Dictionary<string, object> Dic
        {
            get { return _dic ?? (_dic = new Dictionary<string, object>()); }
            set { _dic = value; }
        }
        /// <summary>
        /// 动态对象索引器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object this[string name]
        {
            get
            {
                return Dic[name];
            }
            set
            {
                Dic[name] = value;
            }
        }
        /// <summary>
        /// 动态对象索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public KeyValuePair<string, object> this[int index]
        {
            get
            {
                return Dic.ElementAt(index);
            }
        }
        /// <summary>
        /// 动态对象属性个数
        /// </summary>
        public int Count
        {
            get
            {
                return Dic.Count;
            }
        }
        /// <summary>
        /// 设置动态对象属性值
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Dic[binder.Name] = value;
            return true;
        }
        /// <summary>
        /// 获取动态对象属性值
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return Dic.TryGetValue(binder.Name, out result);
        }
        /// <summary>
        /// 确定动态对象是否存在某个属性名
        /// </summary>
        /// <param name="name">属性名</param>
        /// <returns></returns>
        public bool Exists(string name)
        {
            return Dic.Keys.Contains(name);
        }
        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Add(string key, object value)
        {
            if (Dic.Keys.Contains(key))
                return false;
            Dic.Add(key, value);
            return true;
        }
        /// <summary>
        /// 移除项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void Remove(string key)
        {
            if (Dic.Keys.Contains(key))
                Dic.Remove(key);
        }
    }
}
