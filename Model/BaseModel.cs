using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Tools;

namespace Model
{
    public partial class BaseModel
    {
        /// <summary>
        /// 实体配置
        /// </summary>
        private BaseCfg _cfg;
        [RdfNonSerialized]
        public BaseCfg Cfg
        {
            get
            {
                if (_cfg == null)
                {
                    throw new Exception("实体没有配置模型!");
                }
                return _cfg;
            }
            set { _cfg = value; }
        }
        /// <summary>
        /// 获取模块名称
        /// </summary>
        [RdfNonSerialized]
        public string ModelName
        {
            get
            {
                if (_cfg != null)
                {
                    return Cfg.Tab.TabName;
                }
                else
                {
                    return GetType().Name;
                }
            }
        }
        /// <summary>
        /// 实体业务
        /// </summary>
        private BaseBll _bill;
        [RdfNonSerialized]
        public BaseBll Bll
        {
            get { return _bill ?? (_bill = new BaseBll(this)); }
            set { _bill = value; }
        }

        /// <summary>
        /// 是否缓存
        /// </summary>
        [RdfNonSerialized]
        public bool Cache = true;
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="name">属性名</param>
        /// <returns></returns>
        public object this[string name]
        {
            get
            {
                PropertyInfo info = GetType().GetProperty(name);
                if (info != null)
                    return info.GetValue(this);
                return null;
            }
            set
            {
                PropertyInfo info = GetType().GetProperty(name);
                if (info != null)
                {
                    if (value == DBNull.Value)
                    {
                        info.SetValue(this, null);
                    }
                    else
                    {
                        info.SetValue(this, value);
                    }
                }
            }
        }
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index">下标</param>
        /// <returns></returns>
        public object this[int index]
        {
            get
            {
                PropertyInfo[] info = GetType().GetProperties();
                if (index >= info.Length)
                    index = info.Length - 1;
                return info[index].GetValue(this);
            }
            set
            {
                PropertyInfo[] info = GetType().GetProperties();
                if (index >= info.Length)
                    index = info.Length - 1;
                info[index].SetValue(this, value);
            }
        }

        /// <summary>
        /// 根据配置的字段获取sql语句的值
        /// </summary>
        /// <param name="field">字段信息</param>
        /// <returns></returns>
        private string GetColumnValue(Field field)
        {
            var obj = this[field.ColumnAs];
            if (field == null || obj == null)
                return "null";
            string value;
            switch (field.Type)
            {
                case ColumnType.Int:
                case ColumnType.Decimal:
                    value = obj.ToString();
                    break;
                default:
                    value = "'" + obj.ToString().Replace("'", "''") + "'";
                    break;
            }
            RdfExecuteSql.CheckValue(value);
            return value;
        }
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="field"></param>
        public void SetValue(object value, Field field)
        {
            if (value == null && Nullable.GetUnderlyingType(this.GetType().GetProperty(field.ColumnAs).PropertyType) != null)
            {
                this[field.ColumnAs] = null;
                return;
            }
            switch (field.Type)
            {
                case ColumnType.String:
                    this[field.ColumnAs] = value;
                    break;
                case ColumnType.Int:
                    this[field.ColumnAs] = RdfRegex.Int(value) ? Convert.ToInt32(value) : 0;
                    break;
                case ColumnType.Bit:
                    this[field.ColumnAs] = RdfRegex.Bool(value);
                    break;
                case ColumnType.Decimal:
                    this[field.ColumnAs] = RdfRegex.Number(value) ? Convert.ToDecimal(value) : 0;
                    break;
                case ColumnType.Date:
                case ColumnType.DateTime:
                    this[field.ColumnAs] = RdfRegex.DateTime(value) ? Convert.ToDateTime(value) : new DateTime(1990, 1, 1);
                    break;
                case ColumnType.Guid:
                    this[field.ColumnAs] = RdfRegex.Guid(value) ? new Guid(value.ToString()) : Guid.Empty;
                    break;
                default:
                    this[field.ColumnAs] = value;
                    break;
            }
        }

        /// <summary>
        /// 获取数据表并加入缓存
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql)
        {
            DataTable table;
            if (Cache)
            {
                object obj = RdfCache.GetCache(ModelName, sql);
                if (obj != null)
                {
                    table = (DataTable)obj;
                    return table;
                }
            }
            try
            {
                table = RdfExecuteSql.ExecuteDataTable(sql);
            }
            catch (Exception ex)
            {
                RdfLog.WriteException(ex, GetType().Name + "获取数据表并加入缓存异常:" + sql);
                throw;
            }
            if (Cache)
                RdfCache.CacheAsync(ModelName, sql, table);
            return table;
        }

        /// <summary>
        /// 获取首行首列并加入缓存
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            object val;
            if (Cache)
            {
                object obj = RdfCache.GetCache(ModelName, sql);
                if (obj != null)
                {
                    val = obj;
                    return val;
                }
            }
            try
            {
                val = RdfExecuteSql.ExecuteScalar(sql);
            }
            catch (Exception ex)
            {
                RdfLog.WriteException(ex, GetType().Name + "获取首行首列并加入缓存异常:" + sql);
                throw;
            }
            if (Cache)
                RdfCache.CacheAsync(ModelName, sql, val);
            return val;
        }
        /// <summary>
        /// 返回受影响的行数并删除缓存
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            int row = 0;
            try
            {
                row = RdfExecuteSql.ExecuteNonQuery(sql, false);
            }
            catch (Exception ex)
            {
                RdfLog.WriteException(ex, GetType().Name + "返回受影响的行数并删除缓存异常:" + sql);
                throw;
            }
            if (row > 0 && Cache)
            {
                if (RdfTransaction.IsStart)
                    RdfTransaction.AddCache(ModelName);
                else
                    RdfCache.RemoveCache(ModelName);//主动清除
            }
            return row;
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Clone<T>()
        {
            return RdfSerializer.Deserialize<T>(ToJson());
        }

        /// <summary>
        /// 对象转json
        /// </summary>
        /// <returns></returns>
        public virtual string ToJson()
        {
            return RdfSerializer.ObjToJson(this);
        }

        /// <summary>
        /// 动态对象转实体对象
        /// </summary>
        public void DynamicObj(dynamic obj)
        {
            Cfg.FieldList.ForEach(field =>
            {
                if (obj.Exists(field.ColumnAs))
                {
                    object value = obj[field.ColumnAs];
                    SetValue(value, field);
                }
            });
        }

        /// <summary>
        /// 键值对转实体对象
        /// </summary>
        public void DicObj(Dictionary<string, object> obj)
        {
            Cfg.FieldList.ForEach(field =>
            {
                if (obj.Keys.Contains(field.ColumnAs))
                {
                    object value = obj[field.ColumnAs];
                    SetValue(value, field);
                }
            });
        }
    }
}
