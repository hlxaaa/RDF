using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Tools;

namespace Model
{
    public partial class BaseModel
    {
        #region 获取实体对象
        /// <summary>
        /// 根据主键获取实体对象
        /// </summary>
        /// <returns></returns>
        public bool GetEntity(bool singleTab = true)
        {
            return GetEntity(GetTable(new Condition(Cfg.Tab.PrimaryKey, this[Cfg.Tab.PrimaryKey]), null, singleTab, 1));
        }

        /// <summary>
        /// 根据条件对象获取实体对象
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="singleTab">单表查询</param>
        /// <returns></returns>
        public bool GetEntity(Condition condition, bool singleTab = true)
        {
            return GetEntity(GetTable(condition, null, singleTab, 1));
        }

        /// <summary>
        /// 根据条件对象获取实体对象
        /// </summary>
        /// <param name="listCon">条件列表</param>
        /// <returns></returns>
        public bool GetEntity(List<Condition> listCon, bool singleTab = true)
        {
            return GetEntity(GetTable(listCon, null, singleTab, 1));
        }

        /// <summary>
        /// 根据条件组获取实体对象
        /// </summary>
        /// <param name="area">条件组</param>
        /// <returns></returns>
        public bool GetEntity(ConditionArea area, bool singleTab = true)
        {
            return GetEntity(GetTable(area, null, singleTab, 1));
        }

        /// <summary>
        /// 根据条件组获取实体对象
        /// </summary>
        /// <param name="func">lambda 表达式</param>
        /// <returns></returns>
        public bool GetEntity<T>(Expression<Func<T, bool>> func, bool singleTab = true)
        {
            return GetEntity(GetTable(func, null, singleTab, 1));
        }

        /// <summary>
        /// 根据DataTable获取实体
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns></returns>
        private bool GetEntity(DataTable table)
        {

            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                SetNotReadField();
                DataRowToEntityByCfg(row, this, Cfg);
                return true;
            }
            return false;
        }
        #endregion

        #region 获取集合
        /// <summary>
        /// 获取所有对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="singleTab">单表查询</param>
        /// <param name="top">前n条</param>
        /// <returns></returns>
        public List<T> GetList<T>(Recursive recursive = null, bool singleTab = false, int top = 0) where T : BaseModel, new()
        {
            return DataTableToListByCfg<T>(GetTable(recursive, singleTab, top));
        }

        /// <summary>
        /// 根据条件获取集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">条件</param>
        /// <param name="singleTab">单表查询</param>
        /// <param name="top">前n条</param>
        /// <returns></returns>
        public List<T> GetList<T>(Condition condition, Recursive recursive = null, bool singleTab = false, int top = 0) where T : BaseModel, new()
        {
            return DataTableToListByCfg<T>(GetTable(condition, recursive, singleTab, top));
        }

        /// <summary>
        /// 根据条件对象获取集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listCon">条件列表</param>
        /// <param name="singleTab">单表查询</param>
        /// <param name="top">前n条</param>
        /// <returns></returns>
        public List<T> GetList<T>(List<Condition> listCon, Recursive recursive = null, bool singleTab = false, int top = 0) where T : BaseModel, new()
        {
            return DataTableToListByCfg<T>(GetTable(listCon, recursive, singleTab, top));
        }

        /// <summary>
        /// 根据条件组获取集合
        /// </summary>
        /// <param name="area">条件组</param>
        /// <param name="singleTab">单表查询</param>
        /// <param name="top">前n条</param>
        /// <returns></returns>
        public List<T> GetList<T>(ConditionArea area, Recursive recursive = null, bool singleTab = false, int top = 0) where T : BaseModel, new()
        {
            return DataTableToListByCfg<T>(GetTable(area, recursive, singleTab, top));
        }

        /// <summary>
        /// 根据lambda表达式获取集合
        /// </summary>
        /// <param name="func">lambda表达式</param>
        /// <param name="singleTab">单表查询</param>
        /// <param name="top">前n条</param>
        /// <returns></returns>
        public List<T> GetList<T>(Expression<Func<T, bool>> func, Recursive recursive = null, bool singleTab = false, int top = 0) where T : BaseModel, new()
        {
            return DataTableToListByCfg<T>(GetTable(func, recursive, singleTab, top));
        }

        #endregion

        #region 获取DataTable
        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="singleTab">单表查询</param>
        /// <param name="top">前n条</param>
        /// <returns></returns>
        public DataTable GetTable(Recursive recursive, bool singleTab, int top = 0)
        {
            return GetTable("", recursive, singleTab, top);
        }

        /// <summary>
        /// 根据条件获取DataTable
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="singleTab">单表查询</param>
        /// <param name="top">前n条</param>
        /// <returns></returns>
        public DataTable GetTable(Condition condition, Recursive recursive, bool singleTab, int top = 0)
        {
            return GetTable(ConditionHandle.GetSqlWhere(Cfg, condition), recursive, singleTab, top);
        }

        /// <summary>
        /// 根据条件对象获取DataTable
        /// </summary>
        /// <param name="listCon">条件集合</param>
        /// <param name="singleTab">单表查询</param>
        /// <param name="top">前n条</param>
        /// <returns></returns>
        public DataTable GetTable(List<Condition> listCon, Recursive recursive, bool singleTab, int top = 0)
        {
            return GetTable(ConditionHandle.GetSqlWhere(Cfg, listCon), recursive, singleTab, top);
        }

        /// <summary>
        /// 根据条件组获取DataTable
        /// </summary>
        /// <param name="area">条件组</param>
        /// <param name="singleTab">单表查询</param>
        /// <param name="top">前n条</param>
        /// <returns></returns>
        public DataTable GetTable(ConditionArea area, Recursive recursive, bool singleTab, int top = 0)
        {
            return GetTable(ConditionHandle.GetSqlWhere(Cfg, area), recursive, singleTab, top);
        }
        /// <summary>
        /// 根据条件组获取DataTable
        /// </summary>
        /// <param name="func">lambda 表达式</param>
        /// <param name="singleTab">单表查询</param>
        /// <param name="top">前n条</param>
        /// <returns></returns>
        public DataTable GetTable<T>(Expression<Func<T, bool>> func, Recursive recursive, bool singleTab, int top = 0)
        {
            return GetTable(ConditionHandle.GetSqlWhere<T>(Cfg, func), recursive, singleTab, top);
        }
        /// <summary>
        /// 根据条件获取DataTable
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="singleTab">单表查询</param>
        /// <param name="top">前n条</param>
        /// <returns></returns>
        public DataTable GetTable(string where, Recursive recursive, bool singleTab, int top = 0)
        {
            if (recursive != null)
                return GetTable(where, recursive, singleTab);
            if (singleTab)
                Cfg.NotSelect.AddRange(Cfg.JoinTab.ConvertAll(item => item.TabAs));
            string topStr = top > 0 ? "top(" + top + ")" : "";
            string orderBy = string.IsNullOrWhiteSpace(Cfg.Sort) ? "" : "order by " + Cfg.Sort;
            string columns, joins;
            SetSelectSql(out columns, out joins);
            string sql = string.Format(@"select {0} {1} from {2} {3} {4}", topStr, columns, Cfg.Tab.TabName + " " + Cfg.Tab.TabAs + " " + joins, where, orderBy);
            return ExecuteDataTable(sql);
        }
        /// <summary>
        /// 递归获取DataTable
        /// </summary>
        /// <param name="recursive">递归</param>
        /// <param name="singleTab">单表查询</param>
        /// <returns></returns>
        public DataTable GetTable(string where, Recursive recursive, bool singleTab)
        {
            if (singleTab)
                Cfg.NotSelect.AddRange(Cfg.JoinTab.ConvertAll(item => item.TabAs));
            string withSql = "";
            string tabName = Cfg.Tab.TabName;
            if (recursive != null)
            {
                if (recursive.Cfg == null)
                {
                    recursive.Cfg = Cfg;
                    tabName = "Tab_" + Cfg.Tab.TabName;
                }
                withSql = recursive.RecursiveSearch();
            }
            string columns, joins;
            SetSelectSql(out columns, out joins);
            string orderBy = string.IsNullOrWhiteSpace(Cfg.Sort) ? "" : "order by " + Cfg.Sort;
            string sql = string.Format(@"{0} select {1} from {2} {3} {4} {5}", withSql, columns, tabName + " " + Cfg.Tab.TabAs, joins, where, orderBy);
            return ExecuteDataTable(sql);
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="param">分页参数</param>
        /// <param name="recursive">递归查询</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public void GetPagingData(PagingParam param, Recursive recursive = null, string[] fields = null)
        {
            GetPagingData(param, "", recursive, fields);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="param">分页参数</param>
        /// <param name="condition">条件</param>
        /// <param name="recursive">递归查询</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public void GetPagingData(PagingParam param, Condition condition, Recursive recursive = null, string[] fields = null)
        {
            //取得条件
            string where = ConditionHandle.GetSqlWhere(Cfg, condition);
            GetPagingData(param, where, recursive, fields);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="param">分页参数</param>
        /// <param name="listCon">条件集合</param>
        /// <param name="recursive">递归查询</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public void GetPagingData(PagingParam param, List<Condition> listCon, Recursive recursive = null, string[] fields = null)
        {
            //取得条件
            string where = ConditionHandle.GetSqlWhere(Cfg, listCon);
            GetPagingData(param, where, recursive, fields);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="param">分页参数</param>
        /// <param name="area">条件组</param>
        /// <param name="recursive">递归查询</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public void GetPagingData(PagingParam param, ConditionArea area, Recursive recursive = null, string[] fields = null)
        {
            //取得条件
            string where = ConditionHandle.GetSqlWhere(Cfg, area);
            GetPagingData(param, where, recursive, fields);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="param">分页参数</param>
        /// <param name="where">条件</param>
        /// <param name="recursive">递归查询</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public void GetPagingData(PagingParam param, string where, Recursive recursive = null, string[] fields = null)
        {
            if (param.Index <= 0)
                param.Index = 1;
            if (param.PageSize <= 0)
                param.PageSize = 20;
            param.SumCount = Convert.ToInt32(AggregateFunc(Cfg.FieldList[0].ColumnAs, AggregateType.Count, where, recursive));
            DataTable dt = new DataTable();
            param.Data = dt;
            if (param.SumCount > 0)
            {
                GetSumIndex(param); //总页数
                string exeSql, collectSql, columns, joins, distinct = "", withSql = "", sort = GetSort(param), tName = Cfg.Tab.TabName;//查询sql ,统计sql
                if (recursive != null && recursive.Distinct)
                    distinct = "distinct";
                SetSelectSql(out columns, out joins);
                if (recursive != null)
                {
                    if (recursive.Cfg == null)
                        recursive.Cfg = Cfg;
                    if (recursive.Cfg.Tab.TabName == Cfg.Tab.TabName)
                        tName = "Tab_" + tName;
                    withSql = recursive.RecursiveSearch();
                }
                if (recursive == null || !recursive.Combination)
                {
                    //2012
                    collectSql = string.Format(@"select {0} {1} {2} {3} order by {4} offset {5} row fetch next {6} rows only",
                            distinct, columns + " from " + tName + " " + Cfg.Tab.TabAs, joins, where, sort, (param.Index - 1) * param.PageSize, param.PageSize);
                    //2008
                    //collectSql = string.Format(@"select * from(select {0} row_number() over (order by {4}) AS rownumber,{1} {2} {3})rntab where rownumber between {5} and {6}",
                    //        distinct, columns + " from " + tName + " " + Cfg.Tab.TabAs, joins, where, sort, 1 + (param.Index - 1) * param.PageSize, param.Index * param.PageSize);
                    exeSql = withSql + " " + collectSql;
                }
                else
                {
                    collectSql = "";
                    exeSql = string.Format(@"select {0} {1} {2};{3} select {8}* from Tab_{4} t1 order by {5} offset {6} row fetch next {7} rows only drop table #temp",
                    columns + " into #temp from " + Cfg.Tab.TabName + " " + Cfg.Tab.TabAs, joins, where, withSql, Cfg.Tab.TabName, sort, (param.Index - 1) * param.PageSize, param.PageSize, distinct);
                }
                if (!string.IsNullOrWhiteSpace(Cfg.Tab.BeforeSql))
                {
                    exeSql = Cfg.Tab.BeforeSql + " " + exeSql;
                    collectSql = Cfg.Tab.BeforeSql + " " + collectSql;
                }
                if (!string.IsNullOrWhiteSpace(Cfg.Tab.AfterSql))
                {
                    exeSql = exeSql + " " + Cfg.Tab.AfterSql;
                    collectSql = collectSql + " " + Cfg.Tab.AfterSql;
                }
                //获取数据后筛选字段
                FilterColumn(param, ExecuteDataTable(exeSql), fields);
                //汇总统计
                CollectColumm(param, withSql, collectSql);
            }
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="param">分页参数</param>
        /// <param name="countSql">总数量sql</param>
        /// <param name="dataSql">查询数据sql</param>
        /// <param name="collectSql">统计sql</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public void GetPagingData(PagingParam param, string countSql, string dataSql, string collectSql = null)
        {
            param.SumCount = Convert.ToInt32(ExecuteScalar(countSql));
            DataTable dt = new DataTable();
            param.Data = dt;
            if (param.SumCount > 0)
            {
                GetSumIndex(param);
                dt = ExecuteDataTable(dataSql);
                param.Data = dt;
                param.Count = dt.Rows.Count;
            }
            if (string.IsNullOrWhiteSpace(collectSql))
                param.Collect = new string[] { };
            else
                param.Collect = ExecuteDataTable(collectSql);
        }
        /// <summary>
        /// 取得排序
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetSort(PagingParam param)
        {
            if (!string.IsNullOrWhiteSpace(param.Sort))
                return param.Sort;
            if (!string.IsNullOrWhiteSpace(Cfg.Sort))
                return Cfg.Sort;
            Field field = Cfg.FieldList.FirstOrDefault(item => item.Column == "FdtmCreateTime");
            if (field != null)
                return field.TabAs + "." + field.Column + " desc";
            return Cfg.FieldList[0].TabAs + "." + Cfg.FieldList[0].Column;
        }

        /// <summary>
        /// 取得总页数
        /// </summary>
        /// <param name="param"></param>
        private void GetSumIndex(PagingParam param)
        {
            param.SumIndex = (int)Math.Ceiling(Convert.ToDecimal(param.SumCount) / Convert.ToDecimal(param.PageSize));
            if (param.Index > param.SumIndex)
                param.Index = param.SumIndex;
        }

        /// <summary>
        /// 筛选字段
        /// </summary>
        /// <param name="param"></param>
        /// <param name="dt"></param>
        /// <param name="fields"></param>
        private void FilterColumn(PagingParam param, DataTable dt, string[] fields = null)
        {
            if (fields != null && fields.Length > 0)
            {
                DataTable newDt = dt.Copy();
                List<string> column = (from DataColumn item in newDt.Columns select item.ColumnName).ToList();
                column.ForEach(item =>
                {
                    if (!fields.Contains(item))
                        newDt.Columns.Remove(item);
                });
                param.Data = newDt;
            }
            else
                param.Data = dt;
            param.Count = dt.Rows.Count;
        }
        /// <summary>
        /// 汇总列
        /// </summary>
        /// <param name="param"></param>
        /// <param name="withSql"></param>
        /// <param name="collectSql"></param>
        private void CollectColumm(PagingParam param, string withSql, string collectSql)
        {
            if (param.Collect == null || param.Collect.ToString() == "" || collectSql == "")
            {
                param.Collect = new string[] { };
                return;
            }
            string exeSql = "";
            ArrayList list = (ArrayList)param.Collect;
            List<string> keyList = new List<string>();
            foreach (Dictionary<string, object> item in list)
            {
                foreach (string name in item.Keys)
                {
                    if (!keyList.Exists(key => key.Equals(name)))
                        keyList.Add(name);
                }
            }
            foreach (Dictionary<string, object> item in list)
            {
                StringBuilder sb = new StringBuilder();
                if (exeSql != "")
                    exeSql += " union all ";
                for (int i = 0; i < keyList.Count; i++)
                {
                    if (i != 0)
                        sb.Append(",");
                    if (item.Keys.Contains(keyList[i]))
                    {
                        string val = item[keyList[i]].ToString();
                        if (string.IsNullOrWhiteSpace(val))
                            sb.Append("'' as " + keyList[i]);
                        else
                            sb.Append("convert(nvarchar(50)," + val + ") as " + keyList[i]);
                    }
                    else
                        sb.Append("'' as " + keyList[i]);
                }
                exeSql += string.Format(@" select {0} from({1})tab", sb, collectSql);
            }
            param.Collect = ExecuteDataTable(withSql + exeSql);
        }

        #endregion

        #region DataTable List DataRow Entity的转换
        /// <summary>
        /// DataTable转集合
        /// </summary>
        /// <typeparam name="T">BaseEntity 子类</typeparam>
        /// <param name="table">数据表</param>
        /// <returns></returns>
        public List<T> DataTableToListByCfg<T>(DataTable table) where T : BaseModel, new()
        {
            List<T> list = new List<T>();
            if (table != null)
            {
                SetNotReadField();
                table.AsEnumerable().ToList().ForEach(item =>
                {
                    T vEn = new T();
                    DataRowToEntityByCfg(item, vEn, Cfg);
                    list.Add(vEn);
                });
            }
            return list;
        }
        /// <summary>
        /// DataRow转实体
        /// </summary>
        /// <typeparam name="T">BaseEntity 子类</typeparam>
        /// <param name="row">数据行</param>
        /// <param name="entity">实体对象</param>
        public void DataRowToEntityByCfg<T>(DataRow row, T entity, BaseCfg cfg) where T : BaseModel
        {
            cfg.FieldList.ForEach(field =>
            {
                if (field.Read)
                {
                    var val = row[field.ColumnAs];
                    if (val.Equals(DBNull.Value))
                        val = null;
                    field.OldVal = val;
                    entity[field.ColumnAs] = val;
                }
            });
        }
        /// <summary>
        /// datatable to list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<T> DataTableToList<T>(DataTable dt) where T : BaseModel, new()
        {
            List<T> list = new List<T>();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(DataRowToEntity<T>(item, dt.Columns));
            }
            return list;
        }
        /// <summary>
        /// datarow to entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public T DataRowToEntity<T>(DataRow row, DataColumnCollection colums) where T : BaseModel, new()
        {
            T t = new T();
            try
            {
                PropertyInfo[] pInfo = t.GetType().GetProperties();
                foreach (var item in pInfo)
                {
                    if (item.GetIndexParameters().Length > 0 || !colums.Contains(item.Name))
                        continue;
                    t[item.Name] = row[item.Name];
                }
                return t;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ",方法:DataRowToEntity,类名:" + t.GetType().Name);
            }
        }
        #endregion

        #region 聚合函数
        /// <summary>
        /// 聚合函数
        /// </summary>
        /// <param name="column">字段</param>
        /// <param name="type">函数</param>
        /// <param name="recursive">递归查询</param>
        /// <returns></returns>
        public object AggregateFunc(string column, AggregateType type, Recursive recursive = null)
        {
            return AggregateFunc(column, type, "", recursive);
        }

        /// <summary>
        /// 聚合函数
        /// </summary>
        /// <param name="column">字段</param>
        /// <param name="type">函数</param>
        /// <param name="condition">条件</param>
        /// <param name="recursive">递归查询</param>
        /// <returns></returns>
        public object AggregateFunc(string column, AggregateType type, Condition condition, Recursive recursive = null)
        {
            return AggregateFunc(column, type, ConditionHandle.GetSqlWhere(Cfg, condition), recursive);
        }

        /// <summary>
        /// 聚合函数
        /// </summary>
        /// <param name="column">字段</param>
        /// <param name="type">函数</param>
        /// <param name="listCon">条件列表</param>
        /// <param name="recursive">递归查询</param>
        /// <returns></returns>
        public object AggregateFunc(string column, AggregateType type, List<Condition> listCon, Recursive recursive = null)
        {
            return AggregateFunc(column, type, ConditionHandle.GetSqlWhere(Cfg, listCon), recursive);
        }

        /// <summary>
        /// 聚合函数
        /// </summary>
        /// <param name="column">字段</param>
        /// <param name="type">函数</param>
        /// <param name="area">条件组</param>
        /// <param name="recursive">递归查询</param>
        /// <returns></returns>
        public object AggregateFunc(string column, AggregateType type, ConditionArea area, Recursive recursive = null)
        {
            return AggregateFunc(column, type, ConditionHandle.GetSqlWhere(Cfg, area), recursive);
        }
        /// <summary>
        /// 聚合函数
        /// </summary>
        /// <param name="column">字段</param>
        /// <param name="type">函数</param>
        /// <param name="func">lambda表达式</param>
        /// <param name="recursive">递归查询</param>
        /// <returns></returns>
        public object AggregateFunc<T>(string column, AggregateType type, Expression<Func<T, bool>> func, Recursive recursive = null)
        {
            return AggregateFunc(column, type, ConditionHandle.GetSqlWhere<T>(Cfg, func), recursive);
        }
        /// <summary>
        /// 聚合函数
        /// </summary>
        /// <param name="column">字段</param>
        /// <param name="type">函数</param>
        /// <param name="where">条件</param>
        /// <param name="recursive">递归查询</param>
        /// <returns></returns>
        public object AggregateFunc(string column, AggregateType type, string where, Recursive recursive = null)
        {
            Field field = AggregateCheck(column, type);
            string distinct = "";
            if (recursive != null && recursive.Distinct)
                distinct = "distinct";
            string sql, columns, joins;
            SetSelectSql(out columns, out joins);
            string withSql = "", TabName = Cfg.Tab.TabName;
            if (recursive != null)
            {
                if (recursive.Cfg == null)
                    recursive.Cfg = Cfg;
                if (recursive.Cfg.Tab.TabName == Cfg.Tab.TabName)
                    TabName = "Tab_" + TabName;
                withSql = recursive.RecursiveSearch();
            }
            if (recursive == null || !recursive.Combination)
                sql = string.Format(@"{0} select {7} {1}({2}) from {3} {4} {5} {6}",
                    withSql, Enum.GetName(typeof(AggregateType), type), field.TabAs + "." + field.Column, TabName, Cfg.Tab.TabAs, joins, where, distinct);
            else
                sql = string.Format(@"select {0} {1} {2};{3} select {7} {4}({5}) from Tab_{6} drop table #temp",
                   columns + " into #temp from " + Cfg.Tab.TabName + " " + Cfg.Tab.TabAs, joins, where, withSql,
                    Enum.GetName(typeof(AggregateType), type), field.ColumnAs, Cfg.Tab.TabName, distinct);
            if (!string.IsNullOrWhiteSpace(Cfg.Tab.BeforeSql))
                sql = Cfg.Tab.BeforeSql + " " + sql;
            if (!string.IsNullOrWhiteSpace(Cfg.Tab.AfterSql))
                sql = sql + " " + Cfg.Tab.AfterSql;
            return ExecuteScalar(sql);
        }

        /// <summary>
        /// 检测聚合函数
        /// </summary>
        /// <param name="column"></param>
        /// <param name="type"></param>
        private Field AggregateCheck(string column, AggregateType type)
        {
            Field field = Cfg.FieldList.FirstOrDefault(item => item.ColumnAs.Equals(column));
            if (field == null)
                throw new Exception("模型配置中没有字段" + column);
            if (column.Equals("*") && type.Equals(AggregateType.Count))
                return field;
            if ((field.Type == ColumnType.String ||
                 field.Type == ColumnType.DateTime ||
                 field.Type == ColumnType.Date ||
                 field.Type == ColumnType.Guid) &&
                (type == AggregateType.Sum ||
                 type == AggregateType.Avg))
                throw new Exception("字符或时间不能用于求和或求平均");
            return field;
        }

        #endregion

        /// <summary>
        /// 快速检索条件
        /// </summary>
        /// <param name="area"></param>
        /// <param name="text"></param>
        public void ToFasttipsSql(ConditionArea area, string text)
        {
            ConditionArea childArea = new ConditionArea();
            Cfg.FieldList.ForEach(item =>
            {
                if (item.Select)
                    childArea.ConditionList.Add(new Condition(Relation.Or, item.ColumnAs, Compare.Like, text));
            });
            if (childArea.ConditionList.Count == 1)
            {
                area.ConditionList.Add(childArea.ConditionList[0]);
                return;
            }
            if (childArea.ConditionList.Count > 1)
            {
                childArea.ConditionList[0].Rela = Relation.And;
                area.ChildList.Add(childArea);
            }
        }
        /// <summary>
        /// 设置读取的字段和关联的表
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="joins"></param>
        public void SetSelectSql(out string columns, out string joins)
        {
            columns = Cfg.Tab.Columns;
            joins = "";
            if (Cfg.JoinTab != null)
            {
                foreach (JoinTable jt in Cfg.JoinTab)
                {
                    if (Cfg.NotSelect.Exists(tab => tab == jt.TabName || tab == jt.TabAs))
                        continue;
                    if (!string.IsNullOrWhiteSpace(jt.Columns))
                        columns += "," + jt.Columns;
                    switch (jt.Type)
                    {
                        case JoinType.Left:
                            joins += " left join ";
                            break;
                        case JoinType.Right:
                            joins += " right join ";
                            break;
                        default:
                            joins += " inner join ";
                            break;
                    }
                    joins += jt.TabName + " " + jt.TabAs + " on " + jt.JoinWhere;
                }
            }
        }
        /// <summary>
        /// 设置不读取的字段
        /// </summary>
        private void SetNotReadField()
        {
            if (Cfg.NotSelect.Count > 0)
            {
                foreach (var tab in Cfg.NotSelect)
                {
                    JoinTable join = Cfg.JoinTab.FirstOrDefault(item => item.TabAs == tab || item.TabName == tab);
                    if (join != null)
                        Cfg.FieldList.ForEach(field =>
                        {
                            if (field.TabAs == join.TabAs)
                                field.Read = false;
                        });
                }
            }
        }
    }
}
