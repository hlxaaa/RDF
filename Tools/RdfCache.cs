using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tools
{
    /// <summary>
    /// 数据缓存
    /// </summary>
    public class RdfCache
    {
        private static Dictionary<string, List<CacheObj>> _cacheDic;
        private static readonly ReaderWriterLockSlim CacheLock = new ReaderWriterLockSlim();
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="tab">表名</param>
        /// <param name="key">键</param>
        /// <returns>缓存数据</returns>
        public static object GetCache(string tab, string key)
        {
            CacheLock.EnterReadLock();
            try
            {
                if (_cacheDic == null)
                {
                    _cacheDic = new Dictionary<string, List<CacheObj>>();
                    return null;
                }
                if (!_cacheDic.Keys.Contains(tab))
                    return null;

                List<CacheObj> listCache = _cacheDic[tab];
                if (listCache == null || listCache.Count == 0)
                    return null;
                CacheObj cache = listCache.FirstOrDefault(item => item.Key.Equals(key));
                if (cache == null)
                    return null;
                return cache.Data;
            }
            finally
            {
                CacheLock.ExitReadLock();
            }
        }
        /// <summary>
        /// 异步获取缓存
        /// </summary>
        /// <param name="tab">表名</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static async Task<object> GetCacheAsync(string tab, string key)
        {
            return await Task.Run(() => GetCache(tab, key));
        }
        /// <summary>
        /// 获取缓存表
        /// </summary>
        /// <param name="sql">执行sql</param>
        public static List<string> GetCachTable(string sql)
        {
            List<string> list = new List<string>();
            CacheLock.EnterWriteLock();
            try
            {
                if (_cacheDic == null)
                    return list;
                list.AddRange(_cacheDic.Keys.Where(sql.Contains));
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
            return list;
        }
        /// <summary>
        /// 异步缓存数据
        /// </summary>
        /// <param name="tab">表名</param>
        /// <param name="key">键</param>
        /// <param name="data">缓存数据</param>
        public static void CacheAsync(string tab, string key, object data)
        {
            Task.Run(() => Cache(tab, key, data));
        }
        /// <summary>
        /// 缓存数据
        /// </summary>
        /// <param name="tab">表名</param>
        /// <param name="key">键</param>
        /// <param name="data">缓存数据</param>
        public static void Cache(string tab, string key, object data)
        {
            CacheLock.EnterWriteLock();
            try
            {
                if (_cacheDic == null)
                    _cacheDic = new Dictionary<string, List<CacheObj>>();
                if (!_cacheDic.Keys.Contains(tab))
                {
                    _cacheDic.Add(tab, new List<CacheObj> { new CacheObj { Key = key, Time = DateTime.Now, Data = data } });
                    return;
                }
                List<CacheObj> listCache = _cacheDic[tab];
                if (listCache == null || listCache.Count == 0)
                {
                    _cacheDic[tab] = new List<CacheObj> { new CacheObj { Key = key, Time = DateTime.Now, Data = data } };
                    return;
                }
                CacheObj cache = listCache.FirstOrDefault(item => item.Key.Equals(key));
                if (cache == null)
                {
                    listCache.Add(new CacheObj { Key = key, Time = DateTime.Now, Data = data });
                    return;
                }
                cache.Data = data;
                cache.Time = DateTime.Now;
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
        }
        /// <summary>
        /// 异步移除模块缓存
        /// </summary>
        /// <param name="tab">表名</param>
        /// <returns>缓存数据</returns>
        public static void RemoveCacheAsync(string tab)
        {
            Task.Run(() => RemoveCache(tab));
        }
        /// <summary>
        /// 移除模块缓存
        /// </summary>
        /// <param name="tab">表名</param>
        /// <returns>缓存数据</returns>
        public static void RemoveCache(string tab)
        {
            CacheLock.EnterWriteLock();
            try
            {
                if (_cacheDic == null)
                    return;
                _cacheDic.Remove(tab);
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
        }
        /// <summary>
        /// 异步移除模块缓存
        /// </summary>
        /// <param name="array">表名数组</param>
        /// <returns>缓存数据</returns>
        public static void RemoveCacheAsync(string[] array)
        {
            Task.Run(() => RemoveCache(array));
        }
        /// <summary>
        /// 移除模块缓存
        /// </summary>
        /// <param name="array">表名数组</param>
        /// <returns>缓存数据</returns>
        public static void RemoveCache(string[] array)
        {
            CacheLock.EnterWriteLock();
            try
            {
                if (_cacheDic == null)
                    return;
                array.ToList().ForEach(item => _cacheDic.Remove(item));
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
        }
        /// <summary>
        /// 异步移除所有缓存
        /// </summary>
        /// <param name="array">表名数组</param>
        /// <returns>缓存数据</returns>
        public static void RemoveAllAsync()
        {
            Task.Run(() => RemoveAll());
        }
        /// <summary>
        /// 移除所有缓存
        /// </summary>
        /// <param name="array">表名数组</param>
        /// <returns>缓存数据</returns>
        public static void RemoveAll()
        {
            CacheLock.EnterWriteLock();
            try
            {
                if (_cacheDic == null)
                    return;
                _cacheDic.Clear();
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
        }
        /// <summary>
        /// 处理内存溢出的问题
        /// </summary>
        public static void DealOutOfMemory()
        {
            CacheLock.EnterWriteLock();
            try
            {
                if (_cacheDic == null)
                    return;
                List<CacheModel> list = new List<CacheModel>();
                foreach (var item in _cacheDic.Keys)
                {
                    list.Add(new CacheModel { model = item, count = _cacheDic[item].Count });
                }
                list.Sort((a, b) => b.count.CompareTo(a.count));
                if (list.Count==1)
                {
                    _cacheDic.Clear();
                }
                else
                {
                    int clearcnt = Convert.ToInt32(list.Count / 2);
                    for (int i = 0; i < clearcnt; i++)
                    {
                        _cacheDic.Remove(list[i].model);
                    }
                }
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
        }
        /// <summary>
        /// 缓存对象
        /// </summary>
        class CacheObj
        {
            /// <summary>
            /// 键
            /// </summary>
            public string Key { get; set; }
            /// <summary>
            /// 缓存时间
            /// </summary>
            public DateTime Time { get; set; }
            /// <summary>
            /// 值
            /// </summary>
            public object Data { get; set; }
        }
        class CacheModel
        {
            /// <summary>
            /// 缓存模块
            /// </summary>
            public string model { get; set; }
            /// <summary>
            /// 缓存数量
            /// </summary>
            public int count { get; set; }
        }
    }
}
