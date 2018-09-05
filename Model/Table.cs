using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 表
    /// </summary>
    public class BaseTable
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TabName;
        /// <summary>
        /// 别名
        /// </summary>
        public string TabAs;
        /// <summary>
        /// 字段
        /// </summary>
        public string Columns;
    }
    /// <summary>
    /// 模型表详细信息
    /// </summary>
    public class TableInfo : BaseTable
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="columns">字段</param>
        /// <param name="tabName">表名</param>
        /// <param name="identity">自增长</param>
        /// <param name="tabAs">表别名</param>
        /// <param name="key">主键</param>
        public TableInfo(string columns, string tabName, bool identity = false,string tabAs="t1", string key = "Id")
        {
            TabName = tabName;
            TabAs = tabAs;
            Columns = columns;
            PrimaryKey = key;
            Identity = identity;
        }
        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey;
        /// <summary>
        /// 自增长
        /// </summary>
        public bool Identity;
        /// <summary>
        /// 之前执行的sql
        /// </summary>
        public string BeforeSql;
        /// <summary>
        /// 之后执行的sql
        /// </summary>
        public string AfterSql;
    }
    /// <summary>
    /// 关联表信息
    /// </summary>
    public class JoinTable : BaseTable
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tabName">表名</param>
        /// <param name="tabAs">表别名</param>
        /// <param name="columns">字段</param>
        /// <param name="joinWhere">关联条件</param>
        /// <param name="type">连接类型</param>
        public JoinTable(string tabName, string tabAs, string columns, string joinWhere, JoinType type = JoinType.Left)
        {
            TabName = tabName;
            TabAs = tabAs;
            Columns = columns;
            JoinWhere = joinWhere;
            Type = type;
        }
        /// <summary>
        /// 连接类型
        /// </summary>
        public JoinType Type;
        /// <summary>
        /// 连接条件
        /// </summary>
        public string JoinWhere;
    }
    /// <summary>
    /// 字段信息
    /// </summary>
    public class Field
    {
        /// <summary>
        /// 这个需要的
        /// </summary>
        public Field() { }
        /// <summary>
        /// 添加字段信息
        /// </summary>
        /// <param name="text">名称</param>
        /// <param name="column">列名</param>
        /// <param name="columnAs">别名</param>
        /// <param name="type">字段类型</param>
        /// <param name="tName">表别名</param>
        /// <param name="save">是否保存</param>
        /// <param name="select">是否参与快速检索</param>
        /// <param name="read">是否读取</param>
        public Field(string text, string column, string columnAs, ColumnType type, string tName,bool save, bool select = false, bool read = true)
        {
            Text = text;
            Column = column;
            ColumnAs = columnAs;
            TabAs = tName;
            Type = type;
            Save = save;
            Select = select;
            Read = read;
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Text;
        /// <summary>
        /// 列名
        /// </summary>
        public string Column;
        /// <summary>
        /// 别名
        /// </summary>
        public string ColumnAs;
        /// <summary>
        /// 是否保存
        /// </summary>
        public bool Save;
        /// <summary>
        /// 是否参与快速检索
        /// </summary>
        public bool Select;
        /// <summary>
        /// 是否读取
        /// </summary>
        public bool Read;
        /// <summary>
        /// 字段类型
        /// </summary>
        public ColumnType Type;
        /// <summary>
        /// 表别名
        /// </summary>
        public string TabAs;
        /// <summary>
        /// 原值
        /// </summary>
        public object OldVal;
    }
}
