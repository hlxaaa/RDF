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
    public class RdfSqlQuery<T> : RdfSqlSugar<T> where T : BaseModel, new()
    {
        /// <summary>
        /// 排序
        /// </summary>
        private string sql_orderby = "";
        /// <summary>
        /// 查询字段
        /// </summary>
        private string sql_column = "";
        /// <summary>
        /// 合并
        /// </summary>
        private string sql_unionall = "";
        /// <summary>
        /// 分组
        /// </summary>
        private string sql_groupby = "";
        /// <summary>
        /// 要获取的条数
        /// </summary>
        private int sql_top = 0;
        /// <summary>
        /// 页数
        /// </summary>
        private int sql_index = 0;


        #region 关联表 (t1,t2)=>t1.Id==t2.Id
        protected override void CheckJoin()
        {
            if (sql_orderby != "")
                throw new Exception("请先关联表在排序!");
        }
        /// <summary>
        /// 关联表，主表和一个从表
        /// </summary>
        /// <typeparam name="T2">从表1</typeparam>
        /// <param name="express"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> JoinTable<T2>(Expression<Func<T, T2, object>> express, JoinType type = JoinType.Left)
        {
            string[] array = new string[2] { typeof(T).Name, typeof(T2).Name };
            return (RdfSqlQuery<T>)GetJoin(express, type, array);
        }
        /// <summary>
        /// 关联表，主表和两个从表
        /// </summary>
        /// <typeparam name="T2">从表1</typeparam>
        /// <typeparam name="T3">从表2</typeparam>
        /// <param name="express"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> JoinTable<T2, T3>(Expression<Func<T, T2, T3, object>> express, JoinType type = JoinType.Left)
        {
            string[] array = new string[3] { typeof(T).Name, typeof(T2).Name, typeof(T3).Name };
            return (RdfSqlQuery<T>)GetJoin(express, type, array);
        }
        #endregion

        #region 获取条件 item=>item.Id==1
        /// <summary>
        /// 主表条件
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Where(Expression<Func<T, bool>> express)
        {
            string[] array = new string[1] { typeof(T).Name };
            return (RdfSqlQuery<T>)GetWhere(express, array);
        }
        /// <summary>
        /// 主表和从表
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Where<T2>(Expression<Func<T, T2, bool>> express)
        {
            string[] array = new string[2] { typeof(T).Name, typeof(T2).Name };
            return (RdfSqlQuery<T>)GetWhere(express, array);
        }
        /// <summary>
        /// 主表和2个从表
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Where<T2, T3>(Expression<Func<T, T2, T3, bool>> express)
        {
            string[] array = new string[3] { typeof(T).Name, typeof(T2).Name, typeof(T3).Name };
            return (RdfSqlQuery<T>)GetWhere(express, array);
        }
        /// <summary>
        /// 主表和3个从表
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Where<T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> express)
        {
            string[] array = new string[4] { typeof(T).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name };
            return (RdfSqlQuery<T>)GetWhere(express, array);
        }
        /// <summary>
        /// 主表条件
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Where(ConditionArea area)
        {
            string[] array = new string[1] { typeof(T).Name };
            return (RdfSqlQuery<T>)GetWhere(area, array);
        }
        /// <summary>
        /// 获取条件
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Where(string where)
        {
            return (RdfSqlQuery<T>)GetWhere(where,false);
        }
        #endregion

        #region 排序 支持 "Id desc,Name asc" 或 item=>item.Id
        /// <summary>
        /// 主表升序排序
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> OrderBy(string orderby)
        {
            if (!string.IsNullOrWhiteSpace(sql_orderby))
                sql_orderby += ",";
            sql_orderby += orderby;
            return this;
        }
        /// <summary>
        /// 主表升序排序 如果要分页排序时lambda表达式对象用 t1=>t1.排序字段
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> OrderBy(Expression<Func<T, object>> express)
        {
            string[] array = new string[1] { typeof(T).Name };
            return GetOrderBy(express, array, "Asc");
        }
        /// <summary>
        /// 从表升序排序
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> OrderBy<T2>(Expression<Func<T2, object>> express)
        {
            string[] array = new string[1] { typeof(T2).Name };
            return GetOrderBy(express, array, "Asc");
        }
        /// <summary>
        /// 主表降序排序
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> OrderByDesc(Expression<Func<T, object>> express)
        {
            string[] array = new string[1] { typeof(T).Name };
            return GetOrderBy(express, array, "Desc");
        }
        /// <summary>
        /// 从表降序排序
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> OrderByDesc<T2>(Expression<Func<T2, object>> express)
        {
            string[] array = new string[1] { typeof(T2).Name };
            return GetOrderBy(express, array, "Desc");
        }
        /// <summary>
        /// 获取排序
        /// </summary>
        /// <param name="express"></param>
        /// <param name="tabNameArray"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private RdfSqlQuery<T> GetOrderBy(Expression express, string[] tabNameArray, string type)
        {
            AddTabAs(express, tabNameArray);
            object result = new ExpressionToSql(tabAs_Dic, QueryableType.OrderBy).Create(express);
            if (result != null)
            {
                string orderby = result.ToString();
                if (!string.IsNullOrWhiteSpace(orderby.ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(sql_orderby))
                        sql_orderby += ",";
                    sql_orderby += orderby + " " + type;
                }
            }
            return this;
        }
        #endregion

        #region 合并和递归
        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> UnionAll(RdfSqlQuery<T> query)
        {
            sql_unionall += "\r\nunion all\r\n" + query.ToSql();
            return this;
        }

        /// <summary>
        /// 递归 案例
        /// var query = new RdfSqlQuery<Sys_Menu>()
        /// .Where(item => item.Id.ToString() == "2F24939A-7072-4D13-BA99-2535E1BAD990")
        /// .UnionAll(new RdfSqlQuery<Sys_Menu>().JoinTable<Sys_Menu_Tab>((t1, t2) => t1.FuniParentId == t2.Id, JoinType.Inner).Select("t1.*"))
        /// .ToWith<Sys_Menu_Tab>()
        /// .ToList();
        /// </summary>
        private string sql_with = "";
        public RdfSqlQuery<T2> ToWith<T2>() where T2 : BaseModel, new()
        {
            var query = new RdfSqlQuery<T2>();
            query.sql_with = string.Format("with {0} as(\r\n{1}\r\n)\r\n", typeof(T2).Name, ToSql());
            return query;
        }
        #endregion

        #region 分页
        /// <summary>
        /// 获取的条数
        /// </summary>
        /// <param name="number">条数</param>
        /// <returns></returns>
        public RdfSqlQuery<T> Take(int number)
        {
            if (number <= 0)
                number = 1;
            sql_top = number;
            return this;
        }
        /// <summary>
        /// 设置页数
        /// </summary>
        /// <param name="index">页数</param>
        /// <returns></returns>
        public RdfSqlQuery<T> PageIndex(int index)
        {
            if (index <= 0)
                index = 1;
            if (sql_orderby == "")
                throw new Exception("设置页数前请先排序");
            if (sql_top <= 0)
                throw new Exception("设置页数前请先设置获取条数");
            sql_index = index;
            return this;
        }
        #endregion

        #region 查询字段 支持 "Id,Name" or item=>new {Id=item.Id,Name=item.Name} or item=>new {item.Id,item.Name}
        private void SelectColumn(string column, bool replace = false)
        {
            if (replace)
            {
                sql_column = column;
                return;
            }
            if (!string.IsNullOrWhiteSpace(column))
            {
                if (sql_column != "")
                    sql_column += ",";
                sql_column += column;
            }
        }
        /// <summary>
        /// 纯字段
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Select(string column, bool replace = false)
        {
            SelectColumn(column, replace);
            return this;
        }
        //原sql,用于组成新的对象
        private string sql_old = "";
        /// <summary>
        /// 纯字段返回新的对象
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="column"></param>
        /// <returns></returns>
        public RdfSqlQuery<TResult> Select<TResult>(string column) where TResult : BaseModel, new()
        {
            SelectColumn(column);
            return new RdfSqlQuery<TResult> { sql_old = ToSql() };
        }
        /// <summary>
        /// 主表的字段 单个item=>item.Name 多个 item=>{Id=item.Id,Name=item.Name} or item=>{item.Id,item.Name}
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Select(Expression<Func<T, object>> express)
        {
            string[] array = new string[1] { typeof(T).Name };
            return GetSelect(express, array);
        }
        /// <summary>
        /// 主表+1个从表 单个item=>item.Name 多个 (t1,t2)=>{Id=t1.Id,Name=t2.Name} or (t1,t2)=>{t1.Id,t2.Name}
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Select<T2>(Expression<Func<T, T2, object>> express)
        {
            string[] array = new string[2] { typeof(T).Name, typeof(T2).Name };
            return GetSelect(express, array);
        }
        /// <summary>
        /// 主表+2个从表 单个item=>item.Name 多个 (t1,t2,t3)=>{Id=t1.Id,Name=t2.Name,Code=t3.Code} or (t1,t2,t3)=>{t1.Id,t2.Name,t3.Code}
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Select<T2, T3>(Expression<Func<T, T2, T3, object>> express)
        {
            string[] array = new string[3] { typeof(T).Name, typeof(T2).Name, typeof(T3).Name };
            return GetSelect(express, array);
        }
        /// <summary>
        /// 返回新对象 主表的字段 单个item=>item.Name 多个 item=>{Id=item.Id,Name=item.Name} or item=>{item.Id,item.Name}
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<TResult> Select<TResult>(Expression<Func<T, object>> express) where TResult : BaseModel, new()
        {
            string[] array = new string[1] { typeof(T).Name };
            return GetSelect<TResult>(express, array);
        }
        /// <summary>
        /// 返回新对象 主表+1个从表 单个item=>item.Name 多个 (t1,t2)=>{Id=t1.Id,Name=t2.Name} or (t1,t2)=>{t1.Id,t2.Name}
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<TResult> Select<T2, TResult>(Expression<Func<T, T2, object>> express) where TResult : BaseModel, new()
        {
            string[] array = new string[2] { typeof(T).Name, typeof(T2).Name };
            return GetSelect<TResult>(express, array);
        }
        /// <summary>
        /// 返回新对象 主表+2个从表 单个item=>item.Name 多个 (t1,t2,t3)=>{Id=t1.Id,Name=t2.Name,Code=t3.Code} or (t1,t2,t3)=>{t1.Id,t2.Name,t3.Code}
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<TResult> Select<T2, T3, TResult>(Expression<Func<T, T2, T3, object>> express) where TResult : BaseModel, new()
        {
            string[] array = new string[3] { typeof(T).Name, typeof(T2).Name, typeof(T3).Name };
            return GetSelect<TResult>(express, array);
        }
        /// <summary>
        /// 获取查询 返回新对象
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="express"></param>
        /// <param name="tabNameArray"></param>
        /// <returns></returns>
        private RdfSqlQuery<TResult> GetSelect<TResult>(Expression express, string[] tabNameArray) where TResult : BaseModel, new()
        {
            AddTabAs(express, tabNameArray);
            object result = new ExpressionToSql(tabAs_Dic, QueryableType.Select).Create(express);
            return Select<TResult>(result.ToString());
        }
        /// <summary>
        /// 获取查询
        /// </summary>
        /// <param name="express"></param>
        /// <param name="tabNameArray"></param>
        /// <returns></returns>
        private RdfSqlQuery<T> GetSelect(Expression express, string[] tabNameArray)
        {
            AddTabAs(express, tabNameArray);
            object result = new ExpressionToSql(tabAs_Dic, QueryableType.Select).Create(express);
            return Select(result.ToString());
        }
        #endregion

        #region 分组
        /// <summary>
        /// 分组 主表的字段 单个item=>item.Name 多个 item=>{Id=item.Id,Name=item.Name} or item=>{item.Id,item.Name}
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> GroupBy(Expression<Func<T, object>> express)
        {
            string[] array = new string[1] { typeof(T).Name };
            return GetGroupBy(express, array);
        }
        /// <summary>
        /// 分组  主表+1个从表 单个item=>item.Name 多个 (t1,t2)=>{Id=t1.Id,Name=t2.Name} or (t1,t2)=>{t1.Id,t2.Name}
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> GroupBy<T2>(Expression<Func<T, T2, object>> express)
        {
            string[] array = new string[2] { typeof(T).Name, typeof(T2).Name };
            return GetGroupBy(express, array);
        }
        /// <summary>
        /// 分组  主表+2个从表 单个item=>item.Name 多个 (t1,t2,t3)=>{Id=t1.Id,Name=t2.Name,Code=t3.Code} or (t1,t2,t3)=>{t1.Id,t2.Name,t3.Code}
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> GroupBy<T2, T3>(Expression<Func<T, T2, T3, object>> express)
        {
            string[] array = new string[3] { typeof(T).Name, typeof(T2).Name, typeof(T3).Name };
            return GetGroupBy(express, array);
        }
        /// <summary>
        /// 分组 获取
        /// </summary>
        /// <param name="express"></param>
        /// <param name="tabNameArray"></param>
        /// <returns></returns>
        private RdfSqlQuery<T> GetGroupBy(Expression express, string[] tabNameArray)
        {
            AddTabAs(express, tabNameArray);
            object result = new ExpressionToSql(tabAs_Dic, QueryableType.GroupBy).Create(express);
            if (result != null)
            {
                if (sql_groupby != "")
                    sql_groupby += ",";
                sql_groupby += result;
            }
            return this;
        }
        #endregion

        #region 聚合函数
        /// <summary>
        /// 获取数量 .Count(item => new { item.Id })
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Count(Expression<Func<T, object>> express)
        {
            return Func(express, QueryableType.Count);
        }
        /// <summary>
        /// 求最小值
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Min(Expression<Func<T, object>> express)
        {
            return Func(express, QueryableType.Min);
        }
        /// <summary>
        /// 求最大值
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Max(Expression<Func<T, object>> express)
        {
            return Func(express, QueryableType.Max);
        }
        /// <summary>
        /// 求和
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Sum(Expression<Func<T, object>> express)
        {
            return Func(express, QueryableType.Sum);
        }
        /// <summary>
        /// 求平均
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public RdfSqlQuery<T> Avg(Expression<Func<T, object>> express)
        {
            return Func(express, QueryableType.Avg);
        }
        /// <summary>
        /// 聚合函数
        /// </summary>
        /// <param name="express"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private RdfSqlQuery<T> Func(Expression<Func<T, object>> express, QueryableType type)
        {
            string[] array = new string[1] { typeof(T).Name };
            AddTabAs(express, array);
            object result = new ExpressionToSql(tabAs_Dic, type).Create(express);
            if (result != null)
            {
                if (sql_column != "")
                    sql_column += ",";
                sql_column += result;
            }
            return this;
        }
        #endregion

        #region 转化
        /// <summary>
        /// 执行sql查询后转json串
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            List<T> list = ToList();
            return RdfSerializer.ObjToJson(list);
        }
        /// <summary>
        /// 执行sql查询后获取首行首列
        /// </summary>
        /// <returns></returns>
        public object ToObject()
        {
            T entity = new T();
            string sql = ToSql();
            return entity.ExecuteScalar(sql);
        }
        /// <summary>
        /// 执行sql查询后转List集合
        /// </summary>
        /// <returns></returns>
        public List<T> ToList()
        {
            T entity = new T();
            return entity.DataTableToList<T>(ToDataTable());
        }
        /// <summary>
        /// 执行sql查询后转List集合
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public List<T2> ToList<T2>() where T2 : BaseModel, new()
        {
            T2 entity = new T2();
            return entity.DataTableToList<T2>(ToDataTable<T2>());
        }
        /// <summary>
        /// 执行sql查询后转实体对象
        /// </summary>
        /// <returns></returns>
        public T ToEntity()
        {
            DataTable dt = ToDataTable();
            if (dt.Rows.Count > 0)
                return new T().DataRowToEntity<T>(dt.Rows[0], dt.Columns);
            return null;
        }
        /// <summary>
        /// 执行sql查询后转实体对象
        /// </summary>
        /// <returns></returns>
        public T2 ToEntity<T2>() where T2 : BaseModel, new()
        {
            DataTable dt = ToDataTable();
            if (dt.Rows.Count > 0)
                return new T2().DataRowToEntity<T2>(dt.Rows[0], dt.Columns);
            return null;
        }
        /// <summary>
        /// 执行sql查询后获取datatable
        /// </summary>
        /// <returns></returns>
        public DataTable ToDataTable()
        {
            T entity = new T();
            string sql = ToSql();
            return entity.ExecuteDataTable(sql);
        }
        /// <summary>
        /// 执行sql查询后获取datatable
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public DataTable ToDataTable<T2>() where T2 : BaseModel, new()
        {
            T2 entity = new T2();
            string sql = ToSql();
            return entity.ExecuteDataTable(sql);
        }
        /// <summary>
        /// 获取生成的sql语句
        /// </summary>
        /// <returns></returns>
        public override string ToSql()
        {
            string groupby = "";
            if (sql_groupby != "")
                groupby = " group by " + sql_groupby;

            string column = "*";
            if (sql_column != "")
                column = sql_column;

            string where = "";
            if (!string.IsNullOrWhiteSpace(sql_where))
                where = " where " + sql_where;

            string tabAs = "";
            if (tabAs_Dic.Keys.Count > 0)
                tabAs = " " + tabAs_Dic.Keys.First();

            string name = typeof(T).Name;
            if (sql_old != "")
                name = "(\r\n" + sql_old + "\r\n)" + (tabAs == "" ? "tab" : "");

            string orderby = "";
            if (!string.IsNullOrWhiteSpace(sql_orderby))
                orderby = " order by " + sql_orderby;

            if (sql_top <= 0 && sql_index <= 0)
            {
                return sql_with + string.Format("select {0} from {1}{2}{3}{4}{5}{6}{7}", column, name, tabAs, sql_join, where, orderby, sql_unionall, groupby);
            }
            else if (sql_top > 0 && sql_index <= 0)
            {
                if (sql_groupby != "")
                    throw new Exception("暂不支持!");
                if (sql_unionall != "")
                    throw new Exception("暂不支持!");
                return sql_with + string.Format("select top({0}) {1} from {2}{3}{4}{5}{6}", sql_top, column, name, tabAs, sql_join, where, orderby);
            }
            else
            {
                if (sql_groupby != "")
                    throw new Exception("暂不支持!");
                if (sql_unionall != "")
                    throw new Exception("暂不支持!");
                //2012
                //return string.Format("select {0} from {1} {2} {3} {4} {5} offset {6} row fetch next {7} rows only", column, name, tabAs, sql_join, where, orderby, (sql_index - 1) * sql_top, sql_top);
                //2008 
                return sql_with + string.Format("select * from(\r\nselect row_number() over ({5}) AS rownumber,{0} from {1} {2}{3} {4}\r\n)rowTab where rownumber between {6} and {7}", column, name, tabAs, sql_join, where, orderby, 1 + (sql_index - 1) * sql_top, sql_index * sql_top);
            }
        }
        #endregion
    }
}
