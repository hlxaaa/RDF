using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ExpressionToSql
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dic">key表名，value别名</param>
        /// <param name="type">查询解析类型</param>
        public ExpressionToSql(Dictionary<string, string> tabAs, QueryableType type)
        {
            tabAs_Dic = tabAs ?? (tabAs = new Dictionary<string, string>());
            q_type = type;
        }
        /// <summary>
        /// 别名
        /// </summary>
        private Dictionary<string, string> tabAs_Dic = null;
        /// <summary>
        /// 解析类型
        /// </summary>
        private QueryableType q_type;
        /// <summary>
        /// 递归解析表达式路由计算
        /// </summary>
        /// <returns></returns>
        public object Create(Expression exp)
        {
            object value = null;
            if (exp is LambdaExpression)
            {
                value = LambdaExpression(exp);
            }
            else if (exp is BinaryExpression)
            {
                value = BinaryExpression(exp);
            }
            else if (exp is MemberExpression)
            {
                value = MemberExpression(exp);
            }
            else if (exp is NewExpression)
            {
                value = NewExpression(exp);
            }
            else if (exp is MethodCallExpression)
            {
                value = MethodCallExpression(exp, exp.NodeType);
            }
            else if (exp is UnaryExpression)
            {
                value = UnaryExpression(exp);
            }
            else if (exp is ConstantExpression)
            {
                value = ConstantExpression(exp);
            }
            else if (exp is ParameterExpression)
            {
                value = ParameterExpression(exp);
            }
            else
            {
                throw new Exception("不支持此类型:" + GetTypeName(exp));
            }
            if (value != null)
            {
                string expStr = exp.ToString();
                if (expStr.Length > 2)
                {
                    if (expStr.Substring(0, 2) == "((" && expStr.Substring(expStr.Length - 2, 2) == "))")
                        value = "(" + value + ")";
                }
            }
            return value;
        }
        /// <summary>
        /// 判断是否字段，否则值
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private bool IsColumn(Expression exp)
        {
            if (exp == null || exp is ConstantExpression)
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
        private object ParameterExpression(Expression exp)
        {
            ParameterExpression c_exp = ((ParameterExpression)exp);
            return c_exp.Name;
        }
        /// <summary>
        /// 获取常量值
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private object ConstantExpression(Expression exp)
        {
            ConstantExpression c_exp = ((ConstantExpression)exp);
            object value = c_exp.Value;
            if (value == null)
                return null;

            if (value is String)
            {
                return "'" + value + "'";
            }
            else if (value is Boolean)
            {
                return (bool)value ? 1 : 0;
            }
            else
            {
                return value;
            }
        }
        private object UnaryExpression(Expression exp)
        {
            UnaryExpression c_exp = ((UnaryExpression)exp);
            if (c_exp.Operand is MethodCallExpression)
                return MethodCallExpression(c_exp.Operand, c_exp.NodeType);
            return Create(c_exp.Operand);
        }
        private object MethodCallExpression(Expression exp, ExpressionType type)
        {
            MethodCallExpression c_exp = (MethodCallExpression)exp;
            string method = c_exp.Method.Name.ToLower();
            if (method == "contains" || method == "equals")
            {
                if (type == ExpressionType.Call || type == ExpressionType.Not)
                {
                    bool is_clm = IsColumn(c_exp.Object);
                    object column = Create(is_clm ? c_exp.Object : c_exp.Arguments[0]);
                    object obj = Create(is_clm ? c_exp.Arguments[0] : c_exp.Object);
                    if (method == "contains")
                    {
                        string val = obj.ToString();
                        if (is_clm)
                        {
                            if (val.Length > 2 && val[0] == '\'' && val[val.Length - 1] == '\'')
                                val = val.Substring(1, val.Length - 2);
                            return column + (type == ExpressionType.Not ? " not" : "") + " like '%" + val + "%'";
                        }
                        return column + (type == ExpressionType.Not ? " not" : "") + " in(" + val + ")";
                    }
                    else
                    {
                        return column + (type == ExpressionType.Call ? "=" : "<>") + (obj == null ? "null" : obj.ToString());
                    }
                }
                else
                {
                    throw new Exception("方法MethodCallExpression不支持:" + type.ToString());
                }
            }
            else if (method == "get_item")
            {
                string val = Create(c_exp.Arguments[0]).ToString();
                if (val.Length > 2 && val[0] == '\'' && val[0] == '\'')
                    val = val.Substring(1, val.Length - 2);
                if (tabAs_Dic.Count > 1)
                    return Create(c_exp.Object) + "." + val;
                return val;
            }
            else if (c_exp.Object == null && c_exp.Arguments.Count == 1)
            {
                return Create(c_exp.Arguments[0]);
            }
            else
            {
                return Create(c_exp.Object);
            }
        }
        private object NewExpression(Expression exp)
        {
            NewExpression c_exp = (NewExpression)exp;
            if (c_exp.Arguments.Count == 1)
            {
                if (c_exp.Members.Count == 1 && (q_type == QueryableType.Count || q_type == QueryableType.Min || q_type == QueryableType.Max || q_type == QueryableType.Sum || q_type == QueryableType.Avg))
                {
                    MemberInfo info = c_exp.Members[0];
                    return q_type.ToString() + "(" + Create(c_exp.Arguments[0]) + ") as " + info.Name;
                }
                return Create(c_exp.Arguments[0]);
            }
            else if (c_exp.Arguments.Count > 1 && c_exp.Arguments.Count == c_exp.Members.Count)
            {
                List<string> list = new List<string>();
                for (int i = 0; i < c_exp.Arguments.Count; i++)
                {
                    MemberInfo info = c_exp.Members[i];
                    if (q_type == QueryableType.GroupBy)
                        list.Add(Create(c_exp.Arguments[i]).ToString());
                    else
                        list.Add(Create(c_exp.Arguments[i]).ToString() + " as " + info.Name);
                }
                return string.Join(",", list);
            }
            object value = ExceFunc(exp);
            string type = exp.Type.Name.ToLower();
            if (type == "guid" || type == "string")
            {
                return "'" + value.ToString() + "'";
            }
            return value.ToString();
        }
        private object ExceFunc(Expression exp)
        {
            var obj = Expression.Convert(exp, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(obj);
            object value = lambda.Compile()();
            if (value == null)
                return value;
            string tname = exp.Type.Name.ToLower();
            if (exp.Type.IsGenericType)
            {
                if (exp.Type.GetGenericArguments().Count() == 1)
                {
                    tname = exp.Type.GetGenericArguments()[0].Name.ToLower();
                    if (tname == "int32")
                    {
                        List<int> list = (List<int>)value;
                        return string.Join(",", list.ConvertAll(item => item));
                    }
                    else if (tname == "string")
                    {
                        List<string> list = (List<string>)value;
                        return string.Join(",", list.ConvertAll(item => "'" + item + "'"));
                    }
                }
                throw new Exception("不支持:" + tname);
            }
            else if (tname == "string")
            {
                return "'" + value + "'";
            }
            else if (tname == "datetime")
            {
                return "'" + ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            else
            {
                return value;
            }
        }
        private object MemberExpression(Expression exp)
        {
            MemberExpression c_exp = ((MemberExpression)exp);
            if (c_exp.Expression != null)
            {
                if (c_exp.Expression.NodeType == ExpressionType.Constant)
                    return ExceFunc(exp);

                if (c_exp.Expression.NodeType == ExpressionType.Parameter && tabAs_Dic.Count > 0)
                {
                    if (tabAs_Dic.Count == 1)
                        return tabAs_Dic.First().Key + "." + c_exp.Member.Name;
                    return Create(c_exp.Expression) + "." + c_exp.Member.Name;
                }
                if (c_exp.Expression.NodeType == ExpressionType.MemberAccess)
                    return ExceFunc(exp);
            }
            string tname = c_exp.Type.Name.ToLower();
            string mname = c_exp.Member.Name.ToLower();
            if (tname == "guid" && mname == "empty")
                return "'" + Guid.Empty.ToString() + "'";
            if (tname == "datetime" && mname == "now")
                return "getdate()";

            return c_exp.Member.Name;
        }
        private string BinaryExpression(Expression exp)
        {
            BinaryExpression c_exp = exp as BinaryExpression;
            var left = Create(c_exp.Left);
            var right = Create(c_exp.Right);
            string oper = GetOperator(c_exp.NodeType);
            if (right == null)
            {
                if (oper == "=")
                {
                    right = " is null";
                    oper = "";
                }
                else if (oper == "<>")
                {
                    right = " is not null";
                    oper = "";
                }
                else
                {
                    throw new Exception();
                }
            }
            return left + oper + right;
        }
        private object LambdaExpression(Expression exp)
        {
            LambdaExpression c_exp = exp as LambdaExpression;
            return Create(c_exp.Body);
        }
        /// <summary>
        /// 根据条件生成对应的sql查询操作符
        /// </summary>
        /// <param name="expressiontype"></param>
        /// <returns></returns>
        private string GetOperator(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return " and ";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return " or ";
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return "+";
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return "-";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return "*";
                default:
                    throw new Exception(type.ToString());
            }
        }
        private string GetTypeName(Expression exp)
        {
            if (exp is BinaryExpression) { return "BinaryExpression"; }
            if (exp is BlockExpression) { return "BlockExpression"; }
            if (exp is ConditionalExpression) { return "ConditionalExpression"; }
            if (exp is ConstantExpression) { return "ConstantExpression"; }
            if (exp is DebugInfoExpression) { return "DebugInfoExpression"; }
            if (exp is DefaultExpression) { return "DefaultExpression"; }
            if (exp is DynamicExpression) { return "DynamicExpression"; }
            if (exp is GotoExpression) { return "GotoExpression"; }
            if (exp is IndexExpression) { return "IndexExpression"; }
            if (exp is InvocationExpression) { return "InvocationExpression"; }
            if (exp is LabelExpression) { return "LabelExpression"; }
            if (exp is LambdaExpression) { return "LambdaExpression"; }
            if (exp is ListInitExpression) { return "ListInitExpression"; }
            if (exp is LoopExpression) { return "LoopExpression"; }
            if (exp is MemberExpression) { return "MemberExpression"; }
            if (exp is MemberInitExpression) { return "MemberInitExpression"; }
            if (exp is MethodCallExpression) { return "MethodCallExpression"; }
            if (exp is NewArrayExpression) { return "NewArrayExpression"; }
            if (exp is NewExpression) { return "NewExpression"; }
            if (exp is ParameterExpression) { return "ParameterExpression"; }
            if (exp is RuntimeVariablesExpression) { return "RuntimeVariablesExpression"; }
            if (exp is SwitchExpression) { return "SwitchExpression"; }
            if (exp is TryExpression) { return "TryExpression"; }
            if (exp is TypeBinaryExpression) { return "TypeBinaryExpression"; }
            if (exp is UnaryExpression) { return "UnaryExpression"; }
            return "";
        }
    }
}
