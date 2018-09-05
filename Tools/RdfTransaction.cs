using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Tools
{
    /// <summary>
    /// 事务
    /// </summary>
    public static class RdfTransaction
    {
        /// <summary>
        /// 事务连接对象
        /// </summary>
        [ThreadStatic]
        private static SqlConnection _tranConnection;
        internal static SqlConnection TranConnection
        {
            get
            {
                if (_tranConnection == null)
                { 
                    _tranConnection = RdfPool.GetConnection;
                }
                return _tranConnection;
            }
        }
        /// <summary>
        /// 事务缓存
        /// </summary>
        [ThreadStatic]
        private static List<string> _cache;
        private static List<string> Cache
        {
            get { return _cache ?? (_cache = new List<string>()); }
        }
        /// <summary>
        /// 事务
        /// </summary>
        [ThreadStatic]
        internal static SqlTransaction Transaction;
        /// <summary>
        /// 事务是否已开启
        /// </summary>
        [ThreadStatic]
        public static bool IsStart;
        /// <summary>
        /// 添加事务缓存模块
        /// </summary>
        /// <param name="model"></param>
        public static void AddCache(string model)
        {
            if (!Cache.Exists(item => item.Equals(model)))
                Cache.Add(model);
        }
        /// <summary>
        /// 添加事务缓存模块
        /// </summary>
        /// <param name="models"></param>
        public static void AddCache(string[] models)
        {
            if (models == null)
                return;
            models.ToList().ForEach(AddCache);
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        public static void Begin()
        {
            try
            {
                if (!IsStart)
                {
                   Transaction = TranConnection.BeginTransaction();
                   IsStart = true;
                }
            }
            catch (Exception ex)
            {
                RdfLog.WriteException(ex, "开始事务异常");
                throw;
            }
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public static void Commit()
        {
            try
            {
                if (IsStart)
                {
                    Transaction.Commit();
                    TranConnection.Close();
                    Transaction = null;
                    _tranConnection = null;
                    IsStart = false;
                    if (Cache != null)
                    {
                        Cache.ForEach(RdfCache.RemoveCache);
                        Cache.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                RdfLog.WriteException(ex, "提交事务异常");
                throw;
            }
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public static void Rollback()
        {
            try
            {
                if (IsStart)
                {
                    Transaction.Rollback();
                    TranConnection.Close();
                    Transaction = null;
                    _tranConnection = null;
                    IsStart = false;
                    if (Cache != null)
                        Cache.Clear();
                }
            }
            catch (Exception ex)
            {
                RdfLog.WriteException(ex, "回滚事务异常");
                throw;
            }
        }
    }
}
