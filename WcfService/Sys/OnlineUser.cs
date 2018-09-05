using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WcfService.Sys
{
    /// <summary>
    /// 在线用户
    /// </summary>
    public class OnlineUser
    {
        /// <summary>
        /// 直播间 用户列表
        /// </summary>
        public static Dictionary<int, List<OnlineUserInfo>> _dic;
        private static readonly ReaderWriterLockSlim LiveLock = new ReaderWriterLockSlim();
        /// <summary>
        /// 根据直播间获取在线用户信息
        /// </summary>
        /// <param name="id">直播间</param>
        /// <returns></returns>
        public static List<OnlineUserInfo> GetUserList(int id)
        {
            LiveLock.EnterReadLock();
            try
            {
                if (_dic == null)
                {
                    _dic = new Dictionary<int, List<OnlineUserInfo>>();
                    return new List<OnlineUserInfo>();
                }
                if (!_dic.Keys.Contains(id))
                    return new List<OnlineUserInfo>();
                return _dic[id];
            }
            finally
            {
                LiveLock.ExitReadLock();
            }
        }
        /// <summary>
        /// 异步写入在线用户信息
        /// </summary>
        /// <param name="id">直播间</param>
        /// <param name="user">用户</param>
        public static void AddOnlineUserAsync(int id, OnlineUserInfo user)
        {
            Task.Run(() => AddOnlineUser(id, user));
        }
        /// <summary>
        /// 写入在线用户信息
        /// </summary>
        /// <param name="id">直播间</param>
        /// <param name="user">用户</param>
        public static void AddOnlineUser(int id, OnlineUserInfo user)
        {
            LiveLock.EnterWriteLock();
            try
            {
                user.SetTime = DateTime.Now;
                if (_dic == null)
                    _dic = new Dictionary<int, List<OnlineUserInfo>>();

                if (!_dic.Keys.Contains(id))
                {
                    user.PlayTime = DateTime.Now;
                    _dic.Add(id, new List<OnlineUserInfo>() { user });
                    return;
                }
                List<OnlineUserInfo> userList = _dic[id];
                if (userList == null || userList.Count == 0)
                {
                    user.PlayTime = DateTime.Now;
                    _dic[id] = new List<OnlineUserInfo> { user };
                    return;
                }
                OnlineUserInfo info = userList.FirstOrDefault(item => item.Id == user.Id);
                if (info == null)
                {
                    user.PlayTime = DateTime.Now;
                    _dic[id].Add(user);
                    return;
                }
                info.SetTime = user.SetTime;
                info.Kll = user.Kll;
                info.Km = user.Km;
                info.Sd = user.Sd;
                info.Xl = user.Xl;
                info.Kcal = user.Kcal;
                info.Name = user.Name;
                info.Url = user.Url;
            }
            finally
            {
                LiveLock.ExitWriteLock();
            }
        }
        /// <summary>
        /// 异步移除直播间
        /// </summary>
        /// <param name="id">直播间</param>
        public static void RemoveLiveAsync(int id)
        {
            Task.Run(() => RemoveLive(id));
        }
        /// <summary>
        /// 移除直播间
        /// </summary>
        /// <param name="id">直播间</param>
        public static void RemoveLive(int id)
        {
            LiveLock.EnterWriteLock();
            try
            {
                if (_dic == null)
                    return;
                _dic.Remove(id);
            }
            finally
            {
                LiveLock.ExitWriteLock();
            }
        }
        /// <summary>
        /// 异步移除在线用户
        /// </summary>
        /// <param name="id">直播间</param>
        /// <param name="userId">用户</param>
        public static void RemoveOnlineUserAsync(int id, int userId)
        {
            Task.Run(() => RemoveOnlineUser(id, userId));
        }
        /// <summary>
        /// 移除在线用户
        /// </summary>
        /// <param name="id">直播间</param>
        /// <param name="userId">用户</param>
        public static void RemoveOnlineUser(int id, int userId)
        {
            LiveLock.EnterWriteLock();
            try
            {
                if (_dic == null)
                    return;
                if (!_dic.Keys.Contains(id))
                    return;
                _dic[id].RemoveAll(item => item.Id == userId);
            }
            finally
            {
                LiveLock.ExitWriteLock();
            }
        }
        /// <summary>
        /// 异步移除超时的用户
        /// </summary>
        /// <param name="id">直播间</param>
        public static void RemoveTimeOutAsyn(int id)
        {
            Task.Run(() => RemoveTimeOut(id));
        }
        /// <summary>
        /// 移除超时的用户
        /// </summary>
        /// <param name="id">直播间</param>
        public static void RemoveTimeOut(int id)
        {
            LiveLock.EnterWriteLock();
            try
            {
                if (_dic == null)
                    return;
                if (!_dic.Keys.Contains(id))
                    return;
                _dic[id].RemoveAll(item => item.SetTime.AddMinutes(5) < DateTime.Now);
            }
            finally
            {
                LiveLock.ExitWriteLock();
            }
        }
    }
    /// <summary>
    /// 在线用户信息
    /// </summary>
    public class OnlineUserInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 心率
        /// </summary>
        public int Xl { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public int Sd { get; set; }
        /// <summary>
        /// 热量
        /// </summary>
        public int Kcal { get; set; }
        /// <summary>
        /// 卡路里
        /// </summary>
        public int Kll { get; set; }
        /// <summary>
        /// 里程
        /// </summary>
        public int Km { get; set; }
        /// <summary>
        /// 播放时间
        /// </summary>
        public DateTime PlayTime { get; set; }
        /// <summary>
        /// 设置数据时间
        /// </summary>
        public DateTime SetTime { get; set; }
    }
}