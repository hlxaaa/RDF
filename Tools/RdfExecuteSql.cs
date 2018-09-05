using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Tools
{
    /// <summary>
    /// 执行sql
    /// </summary>
    public class RdfExecuteSql
    {
        private static string[] array = { "drop", "delete", "insert", "update", "exec", "truncate", "declare", "create", "return", "exists", "alter" };
        /// <summary>
        /// 检测查询条件值中是否包含sql注入
        /// </summary>
        /// <param name="values"></param>
        public static void CheckValue(params string[] values)
        {
            foreach (string value in values)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    string val = value.ToLower();
                    if (val.IndexOf("'") != -1 && array.Any(item => val.Contains(item)))
                        throw new Exception("检测到不合法字符:" + value);
                }
            }
        }
        /// <summary>
        /// 获取执行命令
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static SqlCommand GetCommand(SqlConnection conn, string sql)
        {
            //if (!Use)
            //    CheckValid();
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = sql;
            return comm;
        }
        /// <summary>  
        /// 执行查询并将结果返回至DataTable中  
        /// </summary>  
        /// <param name="strSql">查询语句</param>
        /// <returns>返回一张查询结果表</returns>  
        public static DataTable ExecuteDataTable(string strSql, SqlParameter[] param = null)
        {
            SqlConnection conn = RdfTransaction.IsStart ? RdfTransaction.TranConnection : RdfPool.GetConnection;
            DataSet ds = new DataSet();
            SqlCommand cmd = GetCommand(conn, strSql);
            if (param != null)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(param);
            }
            if (RdfTransaction.IsStart)
                cmd.Transaction = RdfTransaction.Transaction;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (!RdfTransaction.IsStart)
                conn.Close();
            return dt;
        }
        /// <summary>  
        /// 执行对数据的增删改操作  
        /// </summary>  
        /// <param name="strSql">执行sql</param>  
        /// <param name="removeCach">清除缓存</param>  
        public static int ExecuteNonQuery(string strSql, bool removeCache = true, SqlParameter[] param = null)
        {
            SqlConnection conn = RdfTransaction.IsStart ? RdfTransaction.TranConnection : RdfPool.GetConnection;
            SqlCommand cmd = GetCommand(conn, strSql);
            if (param != null)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(param);
            }
            if (RdfTransaction.IsStart)
                cmd.Transaction = RdfTransaction.Transaction;
            int rows = cmd.ExecuteNonQuery();
            if (!RdfTransaction.IsStart)
                conn.Close();
            if (rows > 0 && removeCache)
            {
                List<string> list = RdfCache.GetCachTable(strSql);
                if (RdfTransaction.IsStart)
                    list.ForEach(RdfTransaction.AddCache);//加入事务缓存对象
                else
                    list.ForEach(RdfCache.RemoveCache);//从缓存清除
            }
            return rows;
        }
        /// <summary>  
        /// 执行查询并返回结果集中第一行第一列的值  
        /// </summary>  
        /// <param name="strSql">执行sql</param>
        /// <returns></returns>  
        public static object ExecuteScalar(string strSql, SqlParameter[] param = null)
        {
            SqlConnection conn = RdfTransaction.IsStart ? RdfTransaction.TranConnection : RdfPool.GetConnection;
            SqlCommand cmd = GetCommand(conn, strSql);
            if (param != null)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(param);
            }
            if (RdfTransaction.IsStart)
                cmd.Transaction = RdfTransaction.Transaction;
            object obj = cmd.ExecuteScalar();
            if (!RdfTransaction.IsStart)
                conn.Close();
            return obj;
        }
        private static bool Use = false;
        /// <summary>
        /// 验证框架还能否继续使用
        /// </summary>
        private static void CheckValid()
        {
            object result = RdfRegedit.GetRegeditItem("valid", "SOFTWARE\\RdfNet");
            if (result == null)
            {
                if (DateTime.Now > new DateTime(2017, 11, 1))
                {
                    RdfRegedit.WriteRegeditItem("valid", "0", "SOFTWARE\\RdfNet");
                    throw new Exception("严重错误请联系管理员!");
                }
            }
            else if (result.ToString() != "true")
            {
                throw new Exception("严重错误请联系管理员!");
            }
            Use = true;
        }
    }
    //其他事务可以读取表，但不能更新删除 
    //begin tran
    //select * from sys_menu(holdlock)
    //waitfor delay '00:00:05'
    //commit tran
    //其他事务不能读取表,更新和删除(排它锁)
    //begin tran
    //select * from sys_menu(tablockx)
    //waitfor delay '00:00:05'
    //commit tran
    //行级锁,其他事务不能读取表,更新和删除(排它锁)
    //begin tran
    //update sys_menu set FvchrRemark = '' where Id = '2F24939A-7072-4D13-BA99-2535E1BAD990'
    //waitfor delay '00:00:05'
    //commit tran
}
