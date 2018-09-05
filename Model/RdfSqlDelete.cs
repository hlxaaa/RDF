using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tools;
using Model;
using System.Text.RegularExpressions;
using System.Data;

namespace Model
{
    public class RdfSqlDelete<T> : RdfSqlSugar<T>
    {
        #region 关联表 (t1,t2)=>t1.Id==t2.Id
        /// <summary>
        /// 关联表，主表和一个从表
        /// </summary>
        /// <typeparam name="T2">从表1</typeparam>
        /// <param name="express"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public RdfSqlDelete<T> JoinTable<T2>(Expression<Func<T, T2, object>> express, JoinType type = JoinType.Inner)
        {
            string[] array = new string[2] { typeof(T).Name, typeof(T2).Name };
            return (RdfSqlDelete<T>)GetJoin(express, type, array);
        }
        /// <summary>
        /// 关联表，主表和两个从表
        /// </summary>
        /// <typeparam name="T2">从表1</typeparam>
        /// <typeparam name="T3">从表2</typeparam>
        /// <param name="express"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public RdfSqlDelete<T> JoinTable<T2, T3>(Expression<Func<T, T2, T3, object>> express, JoinType type = JoinType.Inner)
        {
            string[] array = new string[3] { typeof(T).Name, typeof(T2).Name, typeof(T3).Name };
            return (RdfSqlDelete<T>)GetJoin(express, type, array);
        }
        #endregion

        #region 获取条件 item=>item.Id==1
        /// <summary>
        /// 主表条件
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlDelete<T> Where(Expression<Func<T, bool>> express)
        {
            string[] array = new string[1] { typeof(T).Name };
            return (RdfSqlDelete<T>)GetWhere(express, array);
        }
        /// <summary>
        /// 主表和从表
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlDelete<T> Where<T2>(Expression<Func<T, T2, bool>> express)
        {
            string[] array = new string[2] { typeof(T).Name, typeof(T2).Name };
            return (RdfSqlDelete<T>)GetWhere(express, array);
        }
        /// <summary>
        /// 主表和2个从表
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlDelete<T> Where<T2, T3>(Expression<Func<T, T2, T3, bool>> express)
        {
            string[] array = new string[3] { typeof(T).Name, typeof(T2).Name, typeof(T3).Name };
            return (RdfSqlDelete<T>)GetWhere(express, array);
        }
        /// <summary>
        /// 主表和3个从表
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlDelete<T> Where<T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> express)
        {
            string[] array = new string[4] { typeof(T).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name };
            return (RdfSqlDelete<T>)GetWhere(express, array);
        }

        #endregion

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="removeCache"></param>
        /// <returns></returns>
        public int Delete(bool removeCache = true)
        {
            string sql = ToSql();
            return RdfExecuteSql.ExecuteNonQuery(sql, removeCache);
        }
        /// <summary>
        /// 获取生成的sql语句
        /// </summary>
        /// <returns></returns>
        public override string ToSql()
        {
            string where = "";
            if (!string.IsNullOrWhiteSpace(sql_where))
                where = " where " + sql_where;
            string tabAs = "";
            if (tabAs_Dic.Keys.Count > 0)
                tabAs = " [" + tabAs_Dic.Keys.First() + "]";
            string name = typeof(T).Name;
            return string.Format("delete{0} from {1}{0}{2}{3}", tabAs, name, sql_join, where);
        }
    }
}
