using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model
{
    public abstract class RdfSqlSugar<T>
    {
        /// <summary>
        /// sql关联
        /// </summary>
        protected string sql_join = "";
        /// <summary>
        /// sql条件
        /// </summary>
        protected string sql_where = "";
        /// <summary>
        /// 表别名
        /// </summary>
        protected Dictionary<string, string> tabAs_Dic = new Dictionary<string, string>();
        /// <summary>
        /// 添加别名
        /// </summary>
        /// <param name="express"></param>
        /// <param name="tabNameArray"></param>
        protected string[] AddTabAs(Expression express, string[] tabNameArray)
        {
            if (tabNameArray.Length <= 1)
                return new string[] { };
            string str = Regex.Match(express.ToString(), @"\((.+?)\).+").Groups[1].Value.Replace(" ", "");
            if (string.IsNullOrWhiteSpace(str))
                return new string[] { };

            string[] tabAsArray = str.Split(',');
            for (int i = 0; i < tabAsArray.Length; i++)
            {
                if (!tabAs_Dic.Keys.Contains(tabAsArray[i]))
                {
                    tabAs_Dic.Add(tabAsArray[i], tabNameArray[i]);
                }
            }
            return tabAsArray;
        }
        /// <summary>
        /// 获取关联sql
        /// </summary>
        /// <param name="express">表达式</param>
        /// <param name="dic">表别名</param>
        /// <param name="tabName">关联表名</param>
        /// <param name="type">关联类型</param>
        protected RdfSqlSugar<T> GetJoin(Expression express, JoinType type, string[] tabNameArray)
        {
            if (sql_where != "")
                throw new Exception("请先关联表在添加条件!");
            CheckJoin();
            string[] tabAsArray = AddTabAs(express, tabNameArray);
            object result = new ExpressionToSql(tabAs_Dic, QueryableType.Join).Create(express);
            if (result != null)
            {
                string on = result.ToString();
                if (!string.IsNullOrWhiteSpace(on))
                {
                    if (on[0] == '(' && on[on.Length - 1] == ')')
                        on = on.Substring(1, on.Length - 2);
                    sql_join += string.Format("\r\n{0} join {1} {2} on {3}", type.ToString(), tabNameArray[tabNameArray.Length - 1], tabAsArray[tabAsArray.Length - 1], on);
                }
            }
            return this;
        }
        protected virtual void CheckJoin() { }
        /// <summary>
        /// 获取条件
        /// </summary>
        /// <param name="express">表达式</param>
        /// <param name="dic">别名</param>
        protected RdfSqlSugar<T> GetWhere(Expression express, string[] tabNameArray)
        {
            AddTabAs(express, tabNameArray);
            object result = new ExpressionToSql(tabAs_Dic, QueryableType.Where).Create(express);
            if (result != null)
                return GetWhere(result.ToString());
            return this;
        }
        /// <summary>
        /// 获取条件
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        protected RdfSqlSugar<T> GetWhere(string where, bool sub = true)
        {
            if (!string.IsNullOrWhiteSpace(where))
            {
                //RdfExecuteSql.CheckValue(where);
                if (sub && where[0] == '(' && where[where.Length - 1] == ')')
                    where = where.Substring(1, where.Length - 2);
                if (!string.IsNullOrWhiteSpace(sql_where))
                    sql_where += " and ";
                sql_where += where;
            }
            return this;
        }
        public abstract string ToSql();

        /// <summary>
        /// 获取条件
        /// </summary>
        /// <param name="area">条件组对象</param>
        /// <returns></returns>
        public RdfSqlSugar<T> GetWhere(ConditionArea area, string[] tabNameArray)
        {
            return GetWhere(ConditionAreaToLambda(area), tabNameArray);
        }
        /// <summary>
        /// 递归条件组转Lambda表达式
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        private Expression<Func<T, bool>> ConditionAreaToLambda(ConditionArea area)
        {
            Expression<Func<T, bool>> express = null;
            area.ConditionList.ForEach(item =>
            {
                Expression<Func<T, bool>> newexp = ConditionToLambda<T>(item);
                if (express == null)
                {
                    express = newexp;
                }
                else
                {
                    ExpressionType type = item.Rela == Relation.Or ? ExpressionType.Or : ExpressionType.And;
                    express = Binary<T>(type, express.Body, newexp.Body);
                }
            });
            area.ChildList.ForEach(item =>
            {
                Expression<Func<T, bool>> newexp = ConditionAreaToLambda(item);
                if (express == null)
                {
                    express = newexp;
                }
                else
                {
                    ExpressionType type = item.ConditionList.Count == 0 ? ExpressionType.And : (item.ConditionList[0].Rela == Relation.Or ? ExpressionType.Or : ExpressionType.And);
                    express = Binary<T>(type, express.Body, newexp.Body);
                }
            });
            return express;
        }
        /// <summary>
        /// 追加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private Expression<Func<T, bool>> Binary<T>(ExpressionType type, Expression left, Expression right)
        {
            BinaryExpression binary = Expression.MakeBinary(type, left, right);
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t1");
            return Expression.Lambda<Func<T, bool>>(binary, parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private Expression<Func<T, bool>> True<T>()
        {
            return t1 => true;
        }

        /// <summary>
        /// 创建lambda表达式：p=>false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private Expression<Func<T, bool>> False<T>()
        {
            return t1 => false;
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="sort"></param>
        /// <returns></returns>
        private Expression<Func<T, TKey>> GetOrderExpression<T, TKey>(string name)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t1");
            return Expression.Lambda<Func<T, TKey>>(Expression.Property(parameter, name), parameter);
        }
        /// <summary>
        /// 条件对象转Lambda
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <returns></returns>
        private Expression<Func<T, bool>> ConditionToLambda<T>(Condition condition)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t1");
            MemberExpression member = Expression.PropertyOrField(parameter, condition.Column);
            ConstantExpression constant = Expression.Constant(condition.Value);
            Expression express = null;
            switch (condition.Compa)
            {
                case Compare.Equals:
                    express = Expression.Equal(member, constant);
                    break;
                case Compare.NotEquals:
                    express = Expression.NotEqual(member, constant);
                    break;
                case Compare.Greater:
                    express = Expression.GreaterThan(member, constant);
                    break;
                case Compare.GreaterEquals:
                    express = Expression.GreaterThanOrEqual(member, constant);
                    break;
                case Compare.Less:
                    express = Expression.LessThan(member, constant);
                    break;
                case Compare.LessEquals:
                    express = Expression.LessThanOrEqual(member, constant);
                    break;
                case Compare.Like:
                case Compare.NotLike:
                    MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    express = Expression.Call(member, method, constant);
                    if (condition.Compa == Compare.NotLike)
                        express = Expression.Not(express);
                    break;
                case Compare.In:
                    break;
                case Compare.NotIn:
                    break;
                default:
                    break;
            }
            return Expression.Lambda<Func<T, bool>>(express, parameter);
        }
    }
}
