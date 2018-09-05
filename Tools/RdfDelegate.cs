using System;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    /// <summary>
    /// 委托
    /// </summary>
    public class RdfDelegate
    {
        private static Dictionary<string, Delegate> _vDelegateDic;

        private static Delegate GetDelegateCheck(string name)
        {
            if ((_vDelegateDic ?? (_vDelegateDic = new Dictionary<string, Delegate>())).Keys.Contains(name))
                return _vDelegateDic[name];
            return null;
        }
        /// <summary>
        /// 注册委托
        /// </summary>
        /// <param name="name">唯一key</param>
        /// <param name="delegates">委托数组</param>
        public static void Register(string name, params Delegate[] delegates)
        {
            if ((_vDelegateDic ?? (_vDelegateDic = new Dictionary<string, Delegate>())).Keys.Contains(name))
                _vDelegateDic.Remove(name);

            foreach (Delegate item in delegates)
            {
                Delegate vDelegate = GetDelegateCheck(name);
                if (vDelegate == null)
                    _vDelegateDic.Add(name, item);
                else
                    _vDelegateDic[name] = Delegate.Combine(vDelegate, item);
            }
        }
        /// <summary>
        /// 多播
        /// </summary>
        /// <param name="name">唯一key</param>
        /// <param name="adelegate">委托</param>
        public static void AddDelegate(string name, Delegate adelegate)
        {
            Delegate vDelegate = GetDelegateCheck(name);
            if (vDelegate != null)
                _vDelegateDic[name] = Delegate.Combine(vDelegate, adelegate);
            else
                Register(name, adelegate);
        }
        /// <summary>
        /// 多播
        /// </summary>
        /// <param name="name">唯一key</param>
        /// <param name="delegates">委托组</param>
        public static void AddDelegate(string name, params Delegate[] delegates)
        {
            if (GetDelegateCheck(name) != null)
                foreach (Delegate item in delegates)
                    AddDelegate(name, item);
            else
                Register(name, delegates);
        }
        /// <summary>
        /// 获取委托(如果未注册，获取委拖为null)
        /// </summary>
        public static Delegate GetDelegate(string name)
        {
            return GetDelegateCheck(name);
        }
        /// <summary>
        /// 移除多播
        /// </summary>
        /// <param name="name"></param>
        /// <param name="adelegate"></param>
        /// <returns></returns>
        public static void RemoveDelegate(string name, Delegate adelegate)
        {
            Delegate vDelegate = GetDelegateCheck(name);
            if (vDelegate != null)
                _vDelegateDic[name] = Delegate.Remove(vDelegate, adelegate);
        }
        /// <summary>
        /// 移除委托
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static void Remove(string name)
        {
            Delegate vDelegate = GetDelegateCheck(name);
            if (vDelegate != null)
                _vDelegateDic.Remove(name);
        }
    }
}
