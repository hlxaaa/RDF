using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Tools;

namespace Model
{
    /// <summary>
    /// 条件对象
    /// </summary>
    public class Condition
    {
        public Condition() { }
        public Condition(string column, object value)
        {
            Rela = Relation.And;
            Column = column;
            Compa = Compare.Equals;
            Value = value;
        }
        public Condition(string column, Compare compare, object value)
        {
            Rela = Relation.And;
            Column = column;
            Compa = compare;
            Value = value;
        }
        public Condition(Relation relation, string column, object value)
        {
            Rela = relation;
            Column = column;
            Compa = Compare.Equals;
            Value = value;
        }
        public Condition(Relation relation, string column, Compare compare, object value)
        {
            Rela = relation;
            Column = column;
            Compa = compare;
            Value = value;
        }
        public Condition(Expression exp, object value)
        {
            Rela = Relation.And;
            Exp = exp;
            Compa = Compare.Equals;
            Value = value;
        }
        public Condition(Expression exp, Compare compare, object value)
        {
            Rela = Relation.And;
            Exp = exp;
            Compa = compare;
            Value = value;
        }
        public Condition(Relation relation, Expression exp, object value)
        {
            Rela = relation;
            Exp = exp;
            Compa = Compare.Equals;
            Value = value;
        }
        public Condition(Relation relation, Expression exp, Compare compare, object value)
        {
            Rela = relation;
            Exp = exp;
            Compa = compare;
            Value = value;
        }
        /// <summary>
        /// 删除值在集合中出现的(只要不存在集合中的值)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="column"></param>
        public void DeleteExists<T>(List<T> list, string column) where T : BaseModel, new()
        {
            List<string> strList = Value.ToString().Split(',', '，').ToList();
            List<string> newList = strList.FindAll(str => !str.Equals("") && !list.Exists(model => model[column].ToString().Equals(str)));
            Value = string.Join(",", newList.ConvertAll(item => item));
        }

        /// <summary>
        /// 关系
        /// </summary>
        public Relation Rela;
        /// <summary>
        /// 字段
        /// </summary>
        public string Column { get; set; }
        /// <summary>
        /// 比较
        /// </summary>
        public Compare Compa { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public Object Value { get; set; }
        public Expression Exp { get; set; }
        public bool CheckValue = true;
    }

    /// <summary>
    /// 条件组对象
    /// </summary>
    public class ConditionArea
    {
        private List<Condition> _conditionList;
        /// <summary>
        /// 条件列表
        /// </summary>
        public List<Condition> ConditionList
        {
            get { return _conditionList ?? (_conditionList = new List<Condition>()); }
            set { _conditionList = value; }
        }

        private List<ConditionArea> _childList;
        /// <summary>
        /// 条件子组无限递归
        /// </summary>
        public List<ConditionArea> ChildList
        {
            get { return _childList ?? (_childList = new List<ConditionArea>()); }
            set { _childList = value; }
        }

        /// <summary>
        /// 动态对象转条件组对象
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public void DynamicToObj(dynamic param)
        {
            ConditionArea area = RdfSerializer.Deserialize<ConditionArea>(RdfSerializer.Serialize(param));
            ConditionList = area.ConditionList;
            ChildList = area.ChildList;
        }
    }

    /// <summary>
    /// 条件处理中心
    /// </summary>
    public class ConditionHandle
    {
        /// <summary>
        /// 根据条件列表获取where条件
        /// </summary>
        /// <param name="cfg">模型配置</param>
        /// <param name="condition">条件</param>
        /// <param name="useAs">true条件字段使用别名并且不用表名例如userName,false使用表名和列名组合例如t1.fvchrUserName</param>
        /// <returns></returns>
        public static string GetSqlWhere(BaseCfg cfg, Condition condition, bool useAs = false)
        {
            if (condition == null)
                return "";
            string sql = GetConditionSql(cfg, condition, useAs);
            if (!string.IsNullOrWhiteSpace(sql))
                return "where" + sql;
            return "";
        }
        /// <summary>
        /// 根据条件列表获取where条件
        /// </summary>
        /// <param name="cfg">模型配置</param>
        /// <param name="list">条件列表</param>
        /// <param name="useAs">true条件字段使用别名并且不用表名例如userName,false使用表名和列名组合例如t1.fvchrUserName</param>
        /// <returns></returns>
        public static string GetSqlWhere(BaseCfg cfg, List<Condition> list, bool useAs = false)
        {
            if (list == null)
                return "";
            string sql = GetListSql(cfg, list, useAs);
            if (!string.IsNullOrWhiteSpace(sql))
                return "where" + sql;
            return "";
        }
        /// <summary>
        /// 根据条件组获取where条件
        /// </summary>
        /// <param name="cfg">模型配置</param>
        /// <param name="area">条件组</param>
        /// <param name="useAs">true条件字段使用别名并且不用表名例如userName,false使用表名和列名组合例如t1.fvchrUserName</param>
        /// <returns></returns>
        public static string GetSqlWhere(BaseCfg cfg, ConditionArea area, bool useAs = false)
        {
            if (area == null)
                return "";
            string sql = GetAreaSql(cfg, area, useAs);
            if (!string.IsNullOrWhiteSpace(sql))
                return "where" + sql;
            return "";
        }
        /// <summary>
        /// 根据条件组获取where条件
        /// </summary>
        /// <param name="cfg">模型配置</param>
        /// <param name="func">lambda 表达式</param>
        /// <param name="useAs">true条件字段使用别名并且不用表名例如userName,false使用表名和列名组合例如t1.fvchrUserName</param>
        /// <returns></returns>
        public static string GetSqlWhere<T>(BaseCfg cfg, Expression<Func<T, bool>> func, bool useAs = false)
        {
            if (func == null)
                return "";
            string sql = "";
            try
            {
                sql = GetLambdaSql(cfg, func.Body, useAs);
            }
            catch (Exception ex)
            {
                RdfLog.WriteException(ex, "根据Lambda获取Sql异常:" + func.ToString());
                throw ex;
            }
            if (!string.IsNullOrWhiteSpace(sql))
                return "where" + sql;
            return "";
        }
        /// <summary>
        /// 根据条件列表获取条件
        /// </summary>
        /// <param name="cfg">模型配置</param>
        /// <param name="condition">条件</param>
        /// <param name="useAs">使用别名</param>
        /// <returns></returns>
        public static string GetConditionSql(BaseCfg cfg, Condition condition, bool useAs = false)
        {
            if (condition.CheckValue && condition.Value != null)
                RdfExecuteSql.CheckValue(condition.Value.ToString());
            if (condition.Compa == Compare.Sql)
                return " " + condition.Value.ToString();
            #region lambda表达式解析
            if (condition.Exp != null && condition.Column == null)
            {
                if (condition.Exp is BinaryExpression)
                {
                    Expression bExp = ((BinaryExpression)condition.Exp).Left;
                    if (bExp is MemberExpression)
                    {
                        string col = GetColumnByExp(bExp);
                        Field mfield = cfg.FieldList.FirstOrDefault(vEn => vEn.ColumnAs.Equals(col));
                        if (mfield == null)
                            throw new Exception("模型配置中找不到字段" + col);
                        return GetLambdaSql(cfg, condition.Exp, useAs) + GetCompareVal(mfield.Type, condition.Compa, condition.Value);
                    }
                    throw new Exception("表达式树未开放");
                }
                else
                {
                    condition.Column = GetColumnByExp(condition.Exp);
                }
            }
            #endregion

            Field field = cfg.FieldList.FirstOrDefault(vEn => vEn.ColumnAs.Equals(condition.Column));
            if (field == null)
                throw new Exception("模型配置中找不到字段" + condition.Column);
            if (condition.Value != null)
                condition.Value = condition.Value.ToString().Replace("'", "''");
            string column = " " + field.TabAs + "." + field.Column + " ";
            if (useAs)
                column = " " + field.ColumnAs + " ";
            switch (condition.Compa)
            {
                case Compare.ToDay:
                    DateTime today = DateTime.Now;
                    return column + GetCompareVal(field.Type, Compare.GreaterEquals, today.ToString("yyyy-MM-dd 00:00:00")) +
                        " and" + column + GetCompareVal(field.Type, Compare.LessEquals, today.ToString("yyyy-MM-dd 23:59:59"));
                case Compare.YesterDay:
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    return column + GetCompareVal(field.Type, Compare.GreaterEquals, yesterday.ToString("yyyy-MM-dd 00:00:00")) +
                        " and" + column + GetCompareVal(field.Type, Compare.LessEquals, yesterday.ToString("yyyy-MM-dd 23:59:59"));
                case Compare.Tomorrow:
                    DateTime tomorrowday = DateTime.Now.AddDays(1);
                    return column + GetCompareVal(field.Type, Compare.GreaterEquals, tomorrowday.ToString("yyyy-MM-dd 00:00:00")) +
                        " and" + column + GetCompareVal(field.Type, Compare.LessEquals, tomorrowday.ToString("yyyy-MM-dd 23:59:59"));
                case Compare.CurrentMonth:
                    DateTime currentmonth = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00")).AddMonths(1);
                    return column + GetCompareVal(field.Type, Compare.GreaterEquals, DateTime.Now.ToString("yyyy-MM-01 00:00:00")) +
                        " and" + column + GetCompareVal(field.Type, Compare.LessEquals, currentmonth.AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
                case Compare.LastMonth:
                    DateTime lastmonth = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00"));
                    return column + GetCompareVal(field.Type, Compare.GreaterEquals, lastmonth.AddMonths(-1)) +
                        " and" + column + GetCompareVal(field.Type, Compare.LessEquals, lastmonth.AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
                case Compare.NextMonth:
                    DateTime nextmonth = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01 00:00:00"));
                    return column + GetCompareVal(field.Type, Compare.GreaterEquals, nextmonth) +
                        " and" + column + GetCompareVal(field.Type, Compare.LessEquals, nextmonth.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
                case Compare.CurrentYear:
                    DateTime currentyear = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01 00:00:00")).AddYears(1);
                    return column + GetCompareVal(field.Type, Compare.GreaterEquals, DateTime.Now.ToString("yyyy-01-01 00:00:00")) +
                        " and" + column + GetCompareVal(field.Type, Compare.LessEquals, currentyear.AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
                case Compare.LastYear:
                    DateTime lastyear = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01 00:00:00"));
                    return column + GetCompareVal(field.Type, Compare.GreaterEquals, lastyear.AddYears(-1)) +
                        " and" + column + GetCompareVal(field.Type, Compare.LessEquals, lastyear.AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
                case Compare.NextYear:
                    DateTime nextyear = Convert.ToDateTime(DateTime.Now.AddYears(1).ToString("yyyy-01-01 00:00:00"));
                    return column + GetCompareVal(field.Type, Compare.GreaterEquals, nextyear) +
                        " and" + column + GetCompareVal(field.Type, Compare.LessEquals, nextyear.AddYears(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
                default:
                    return column + GetCompareVal(field.Type, condition.Compa, condition.Value);
            }
        }
        /// <summary>
        /// 获取列名
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private static string GetColumnByExp(Expression exp)
        {
            if (exp is ConstantExpression)
            {
                return ((ConstantExpression)exp).Value.ToString();
            }
            else if (exp is ConditionalExpression)
            {
                if (GetValueByExp(((ConditionalExpression)exp).Test).ToString().ToLower() == "true")
                    return GetColumnByExp(((ConditionalExpression)exp).IfTrue);
                return GetColumnByExp(((ConditionalExpression)exp).IfFalse);
            }
            else if (exp is MemberExpression)
            {
                return ((MemberExpression)exp).Member.Name;
            }
            else if (exp is MethodCallExpression)
            {
                Expression mExp = ((MethodCallExpression)exp).Object;
                if (mExp is MemberExpression)
                {
                    return GetColumnByExp(mExp);
                }
                else if (mExp is MethodCallExpression)
                {
                    if (((MethodCallExpression)mExp).Method.Name.ToLower() == "get_item")
                    {
                        return GetColumnByExp(((MethodCallExpression)mExp).Arguments[0]);
                    }
                    else
                        throw new Exception("表达式树未开放");
                }
                else if (mExp is ParameterExpression)
                {
                    if (((MethodCallExpression)exp).Method.Name.ToLower() == "get_item")
                    {
                        return GetColumnByExp(((MethodCallExpression)exp).Arguments[0]);
                    }
                    else
                        throw new Exception("表达式树未开放");
                }
                else
                    throw new Exception("表达式树未开放");
            }
            throw new Exception("表达式树未开放");
        }

        /// <summary>
        /// 根据条件列表获取条件
        /// </summary>
        /// <param name="cfg">模型配置</param>
        /// <param name="list">条件列表</param>
        /// <returns></returns>
        private static string GetListSql(BaseCfg cfg, List<Condition> list, bool useAs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Condition item in list)
            {
                if (item == null)
                    continue;
                if (!string.IsNullOrWhiteSpace(sb.ToString()))
                    sb.Append(" " + Enum.GetName(typeof(Relation), item.Rela));
                sb.Append(GetConditionSql(cfg, item, useAs));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 递归拼接条件组
        /// </summary>
        /// <param name="cfg">实体配置</param>
        /// <param name="area">条件组</param>
        /// <param name="useAs"></param>
        /// <returns></returns>
        private static string GetAreaSql(BaseCfg cfg, ConditionArea area, bool useAs)
        {
            if (area == null)
                return "";
            StringBuilder sb = new StringBuilder();
            if (area.ConditionList != null)
                sb.Append(GetListSql(cfg, area.ConditionList, useAs));

            if (area.ChildList != null)
            {
                area.ChildList.ForEach(children =>
                {
                    string str = GetAreaSql(cfg, children, useAs);
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        string relation = "";
                        if (!string.IsNullOrWhiteSpace(sb.ToString()))
                            relation = Enum.GetName(typeof(Relation), children.ConditionList[0].Rela);
                        sb.Append(" " + relation + "(" + str + ")");
                    }
                });
            }
            return sb.ToString();
        }

        /// <summary>
        /// 根据Lambda表达式获取条件
        /// </summary>
        /// <param name="cfg">实体配置</param>
        /// <param name="exp">表达式树</param>
        /// <param name="useAs"></param>
        /// <returns></returns>
        private static string GetLambdaSql(BaseCfg cfg, Expression exp, bool useAs)
        {
            switch (exp.NodeType)
            {
                case ExpressionType.Add:
                    return GetConditionSql(cfg, new Condition(((BinaryExpression)exp).Left, Compare.Add, GetValueByExp(((BinaryExpression)exp).Right)), useAs);
                case ExpressionType.Subtract:
                    return GetConditionSql(cfg, new Condition(((BinaryExpression)exp).Left, Compare.Subtract, GetValueByExp(((BinaryExpression)exp).Right)), useAs);
                case ExpressionType.Multiply:
                    return GetConditionSql(cfg, new Condition(((BinaryExpression)exp).Left, Compare.Multiply, GetValueByExp(((BinaryExpression)exp).Right)), useAs);
                case ExpressionType.Divide:
                    return GetConditionSql(cfg, new Condition(((BinaryExpression)exp).Left, Compare.Divide, GetValueByExp(((BinaryExpression)exp).Right)), useAs);
                case ExpressionType.Modulo:
                    return GetConditionSql(cfg, new Condition(((BinaryExpression)exp).Left, Compare.Modulo, GetValueByExp(((BinaryExpression)exp).Right)), useAs);
                case ExpressionType.AndAlso:
                    string andalsoStr = exp.ToString();
                    if (andalsoStr.Substring(0, 2) == "((" && andalsoStr.Substring(andalsoStr.Length - 2, 2) == "))")
                        return "(" + GetLambdaSql(cfg, ((BinaryExpression)exp).Left, useAs) + " and" + GetLambdaSql(cfg, ((BinaryExpression)exp).Right, useAs) + ")";
                    return GetLambdaSql(cfg, ((BinaryExpression)exp).Left, useAs) + " and" + GetLambdaSql(cfg, ((BinaryExpression)exp).Right, useAs);
                case ExpressionType.Call:
                    return GetLambdaSql(cfg, (MethodCallExpression)exp, exp.NodeType, useAs);
                case ExpressionType.Equal:
                    return GetConditionSql(cfg, new Condition(((BinaryExpression)exp).Left, GetValueByExp(((BinaryExpression)exp).Right)), useAs);
                case ExpressionType.NotEqual:
                    return GetConditionSql(cfg, new Condition(((BinaryExpression)exp).Left, Compare.NotEquals, GetValueByExp(((BinaryExpression)exp).Right)), useAs);
                case ExpressionType.GreaterThan:
                    return GetConditionSql(cfg, new Condition(((BinaryExpression)exp).Left, Compare.Greater, GetValueByExp(((BinaryExpression)exp).Right)), useAs);
                case ExpressionType.GreaterThanOrEqual:
                    return GetConditionSql(cfg, new Condition(((BinaryExpression)exp).Left, Compare.GreaterEquals, GetValueByExp(((BinaryExpression)exp).Right)), useAs);
                case ExpressionType.LessThan:
                    return GetConditionSql(cfg, new Condition(((BinaryExpression)exp).Left, Compare.Less, GetValueByExp(((BinaryExpression)exp).Right)), useAs);
                case ExpressionType.LessThanOrEqual:
                    return GetConditionSql(cfg, new Condition(((BinaryExpression)exp).Left, Compare.LessEquals, GetValueByExp(((BinaryExpression)exp).Right)), useAs);
                case ExpressionType.MemberAccess:
                    return GetConditionSql(cfg, new Condition(exp, true), useAs);
                case ExpressionType.Not:
                    var unary = (UnaryExpression)exp;
                    return GetLambdaSql(cfg, unary.Operand, exp.NodeType, useAs);
                case ExpressionType.OrElse:
                    string orelseStr = exp.ToString();
                    if (orelseStr.Substring(0, 2) == "((" && orelseStr.Substring(orelseStr.Length - 2, 2) == "))")
                        return "(" + GetLambdaSql(cfg, ((BinaryExpression)exp).Left, useAs) + " or" + GetLambdaSql(cfg, ((BinaryExpression)exp).Right, useAs) + ")";
                    return GetLambdaSql(cfg, ((BinaryExpression)exp).Left, useAs) + " or" + GetLambdaSql(cfg, ((BinaryExpression)exp).Right, useAs);
                default:
                    throw new Exception("表达式树未开放");
            }
        }
        private static object GetValueByExp(Expression exp)
        {
            if (exp is ConstantExpression)
            {
                return ((ConstantExpression)exp).Value;
            }
            var obj = Expression.Convert(exp, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(obj);
            var func = lambda.Compile();
            return func();
        }
        private static string GetLambdaSql(BaseCfg cfg, Expression exp, ExpressionType type, bool useAs)
        {
            Compare comp = Compare.Equals;
            if (exp is MethodCallExpression)
            {
                var methodExp = (MethodCallExpression)exp;
                var isColumn = IsColumn(methodExp.Object);
                switch (methodExp.Method.Name.ToLower())
                {
                    case "equals":
                        if (type == ExpressionType.Not)
                            comp = Compare.NotEquals;
                        break;
                    case "contains":
                        if (isColumn)
                        {
                            if (type == ExpressionType.Call)
                                comp = Compare.Like;
                            else if (type == ExpressionType.Not)
                                comp = Compare.NotLike;
                        }
                        else
                        {
                            if (type == ExpressionType.Call)
                                comp = Compare.In;
                            else if (type == ExpressionType.Not)
                                comp = Compare.NotIn;
                        }
                        break;
                    default:
                        throw new Exception("表达式树未开放");
                }
                if (isColumn)
                    return GetConditionSql(cfg, new Condition(methodExp.Object, comp, GetValueByExp(methodExp.Arguments[0])), useAs);
                else
                    return GetConditionSql(cfg, new Condition(methodExp.Arguments[0], comp, GetValueByExp(methodExp.Object)), useAs);

            }
            if (exp is MemberExpression)
            {
                if (type == ExpressionType.Not)
                    comp = Compare.NotEquals;
                return GetConditionSql(cfg, new Condition(exp, comp, true), useAs);
            }
            throw new Exception("表达式树未开放");
        }
        /// <summary>
        /// 判断是否字段，否则值
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private static bool IsColumn(Expression exp)
        {
            if (exp is ConstantExpression)
            {
                return false;
            }
            if (exp == null)
            {
                return false;
            }
            else if (exp is MethodCallExpression)
            {
                return IsColumn(((MethodCallExpression)exp).Object);
            }
            else if (exp is MemberExpression)
            {
                Expression mExp = ((MemberExpression)exp).Expression;
                if (mExp == null)
                {
                    return false;
                }
                else if (mExp.NodeType == ExpressionType.Constant)
                {
                    return false;
                }
                else
                {
                    return IsColumn(mExp);
                }
            }
            return true;
        }
        /// <summary>
        /// 获取条件的比较值
        /// </summary>
        /// <param name="type">字段类型</param>
        /// <param name="compare">比较</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string GetCompareVal(ColumnType type, Compare compare, object value)
        {
            if (value == null)
                value = "null";
            switch (compare)
            {
                case Compare.Less:
                    return "<" + GetValue(type, value);
                case Compare.LessEquals:
                    return "<=" + GetValue(type, value);
                case Compare.Greater:
                    return ">" + GetValue(type, value);
                case Compare.GreaterEquals:
                    return ">=" + GetValue(type, value);
                case Compare.In:
                case Compare.NotIn:
                    string newVal = value.ToString();
                    if (newVal.Length > 0)
                    {
                        while (newVal[0] == ',')
                        {
                            newVal = newVal.Substring(1, newVal.Length - 1);
                        }
                        while (newVal[newVal.Length - 1] == ',')
                        {
                            newVal = newVal.Substring(0, newVal.Length - 1);
                        }
                        if (type == ColumnType.Int || type == ColumnType.Decimal)
                        {
                            value = newVal.Replace("\"", "").Replace("'", "").Replace('，', ',');
                        }
                        else
                        {
                            newVal = newVal.Replace("，\"", ",\"");
                            if (newVal[0] != '"')
                                newVal = "\'" + newVal.Replace(",", "\',\'") + "\'";
                            value = newVal;
                        }
                    }
                    return (compare == Compare.NotIn ? "not " : "") + "in(" + value + ")";
                case Compare.LeftLike:
                    return "like '" + value + "%'";
                case Compare.Like:
                    return "like '%" + value + "%'";
                case Compare.NotLike:
                    return "not like '%" + value + "%'";
                case Compare.RightLike:
                    return "like '%" + value + "'";
                case Compare.NotEquals:
                    return "!=" + GetValue(type, value);
                case Compare.Add:
                    return "+" + GetValue(type, value);
                case Compare.Subtract:
                    return "-" + GetValue(type, value);
                case Compare.Multiply:
                    return "*" + GetValue(type, value);
                case Compare.Divide:
                    return "/" + GetValue(type, value);
                case Compare.Modulo:
                    return "%" + GetValue(type, value);
                case Compare.Is:
                    return "is null";
                case Compare.IsNot:
                    return "is not null";
                default:
                    return "=" + GetValue(type, value);
            }
        }
        private static string GetValue(ColumnType type, object value)
        {
            string val = "";
            switch (type)
            {
                case ColumnType.Int:
                case ColumnType.Long:
                    val = RdfRegex.Int(value) ? value.ToString() : "0";
                    break;
                case ColumnType.Decimal:
                    val = RdfRegex.Number(value) ? value.ToString() : "0";
                    break;
                case ColumnType.DateTime:
                    val = RdfRegex.DateTime(value) ? "cast('" + value.ToString() + "' as datetime)" : "cast('1900-01-01' as datetime)";
                    break;
                case ColumnType.Guid:
                    val = RdfRegex.Guid(value) ? "'" + value.ToString() + "'" : "'" + Guid.Empty.ToString() + "'";
                    break;
                default:
                    val = "'" + value + "'";
                    break;
            }
            return val;
        }
    }
}
