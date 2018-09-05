using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    /// <summary>
    /// 注册表
    /// </summary>
    public class RdfRegedit
    {
        /// <summary>
        /// 判断注册表项是否存在
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="url">注册表目录</param>
        /// <returns></returns>
        public static bool ExistsRegeditItem(string key = "", string url = "SOFTWARE")
        {
            List<string> subKeys = url.Split('\\').ToList();
            RegistryKey parentKey = Registry.LocalMachine;
            foreach (string item in subKeys)
            {
                parentKey = parentKey.OpenSubKey(item, true);
                if (parentKey == null)
                    return false;
            }
            return parentKey.GetValueNames().ToList().Exists(item => item.Equals(key));
        }
        /// <summary>
        /// 写入注册表项
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="url">注册表目录</param>
        /// <returns></returns>
        public static void WriteRegeditItem(string key = "", string value = "", string url = "SOFTWARE")
        {
            List<string> subKeys = url.Split('\\').ToList();
            RegistryKey parentKey = Registry.LocalMachine;
            foreach (string item in subKeys)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;
                RegistryKey subKey = parentKey.OpenSubKey(item, true);
                if (subKey == null)
                {
                    Registry.LocalMachine.CreateSubKey(url);
                    subKey = parentKey.OpenSubKey(item, true);
                }
                if (subKey == null)
                    throw new Exception("写入注册表异常:" + url);
                parentKey = subKey;
            }
            parentKey.SetValue(key, value);
        }
        /// <summary>
        /// 获取注册表项
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="url">注册表目录</param>
        /// <returns></returns>
        public static object GetRegeditItem(string key = "", string url = "SOFTWARE")
        {
            List<string> subKeys = url.Split('\\').ToList();
            RegistryKey parentKey = Registry.LocalMachine;
            foreach (string item in subKeys)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;
                parentKey = parentKey.OpenSubKey(item, true);
                if (parentKey == null)
                    return null;
            }
            return parentKey.GetValue(key);
        }
        /// <summary>
        /// 删除注册表项
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="url">注册表目录</param>
        /// <returns></returns>
        public static void DeleteRegeditItem(string key = "", string url = "SOFTWARE")
        {
            List<string> subKeys = url.Split('\\').ToList();
            RegistryKey parentKey = Registry.LocalMachine;
            foreach (string item in subKeys)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;
                parentKey = parentKey.OpenSubKey(item, true);
                if (parentKey == null)
                    return;
            }
            parentKey.DeleteValue(key);
        }
        /// <summary>
        /// 删除注册表文件夹
        /// </summary>
        /// <param name="subKey">文件名称</param>
        /// <param name="url">注册表目录</param>
        /// <returns></returns>
        public static void DeleteRegeditSubKey(string subKey = "", string url = "SOFTWARE")
        {
            List<string> subKeys = url.Split('\\').ToList();
            RegistryKey parentKey = Registry.LocalMachine;
            foreach (string item in subKeys)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;
                parentKey = parentKey.OpenSubKey(item, true);
                if (parentKey == null)
                    return;
            }
            parentKey.DeleteSubKeyTree(subKey);
        }
    }
}
