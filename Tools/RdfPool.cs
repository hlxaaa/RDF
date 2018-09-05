using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace Tools
{
    /// <summary>
    /// 连接池
    /// </summary>
    public class RdfPool
    {
        /// <summary>
        /// 连接池大小，默认100,一般1000人20连接池够用。
        /// </summary>
        private static int _poolSize = 100;

        /// <summary>
        /// 连接超时时间，默认15秒。
        /// </summary>
        private const int ConnTimeOut = 15;

        /// <summary>
        /// 当连接池无连接资源时，连接等待时间，毫秒
        /// </summary>
        private const int PoolPauseTime = 200;

        /// <summary>
        /// 连接池
        /// </summary>
        private static SqlConnection[] _connPool;

        private static string _configKey;
        /// <summary>
        /// 配置文件sql连接字符串key
        /// </summary>
        public static string ConfigKey
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_configKey))
                    _configKey = "ConnectionStrings";
                return _configKey;
            }
            set
            {
                lock (Obj)
                {
                    _connPool = null;
                }
                _connectionStr = null;
                _configKey = value;
            }
        }

        private static string _connectionStr;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionStr
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_connectionStr))
                {
                    _connectionStr = ConfigurationManager.AppSettings[ConfigKey];
                    if (string.IsNullOrWhiteSpace(_connectionStr))
                        throw new Exception("请先配置数据库连接字符串");
                    if (_connectionStr.Substring(_connectionStr.Length - 1, 1) != ";")
                        _connectionStr += ";";
                    string str = _connectionStr.ToLower().Replace(" ", "");
                    if (!str.Contains("connecttimeout"))
                        _connectionStr += "Connect Timeout=" + ConnTimeOut + ";";
                    if (!str.Contains("maxpoolsize"))
                        _connectionStr += "Max Pool Size=" + _poolSize + ";";
                    else
                    {
                        int begin = str.IndexOf("maxpoolsize", StringComparison.Ordinal) + "maxpoolsize".Length + 1;
                        int end = str.IndexOf(";", begin, StringComparison.Ordinal);
                        _poolSize = Convert.ToInt32(str.Substring(begin, end - begin));
                    }
                }
                return _connectionStr;
            }
            set
            {
                lock (Obj)
                {
                    _connPool = null;
                }
                _connectionStr = value;
            }
        }
        private static readonly object Obj = new object();
        /// <summary>
        /// 获取连接对象
        /// </summary>
        public static SqlConnection GetConnection
        {
            get
            {
                SqlConnection conn = null;
                lock (Obj)
                {
                    if (_connPool == null)
                        _connPool = new SqlConnection[_poolSize];
                    for (int i = 0; i < _connPool.Length; i++)
                    {
                        conn = _connPool[i];
                        if (conn == null)
                        {
                            conn = new SqlConnection(ConnectionStr);
                            conn.Open();
                            _connPool[i] = conn;
                            break;
                        }
                        if (conn.State.Equals(ConnectionState.Closed))
                        {
                            conn.Open();
                            break;
                        }
                        if (conn.State.Equals(ConnectionState.Broken))
                        {
                            conn.Close();
                            conn.Open();
                            break;
                        }
                        if (i == _connPool.Length - 1)
                        {
                            Thread.Sleep(PoolPauseTime);
                            i = 0;
                        }
                    }
                }
                return conn;
            }
        }
    }
}
