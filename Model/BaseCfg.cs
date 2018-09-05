using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Model
{
    public abstract class BaseCfg
    {
        protected BaseCfg()
        {
            SetCfg();
        }

        private List<Field> _fieldList;
        /// <summary>
        /// 字段列表
        /// </summary>
        public List<Field> FieldList { get { return _fieldList ?? (_fieldList = new List<Field>()); } }
        /// <summary>
        /// 表
        /// </summary>
        public TableInfo Tab;

        private List<JoinTable> _joinTab;
        /// <summary>
        /// 关联表
        /// </summary>
        public List<JoinTable> JoinTab { get { return _joinTab ?? (_joinTab = new List<JoinTable>()); } }
        /// <summary>
        /// 排序
        /// </summary>
        public string Sort = "";

        private List<string> notSelect;
        /// <summary>
        /// 不参与查询的表
        /// </summary>
        public List<string> NotSelect
        {
            get
            {
                if (notSelect == null)
                    notSelect = new List<string>();
                return notSelect;
            }
        }
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
        protected void AddField(string text, string column, string columnAs, ColumnType type, string tName, bool save = true, bool select = false, bool read = true)
        {
            FieldList.Add(new Field(text, column, columnAs, type, tName, save, select, read));
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tabName">表名</param>
        /// <param name="tabAs">表别名</param>
        /// <param name="joinWhere">关联条件</param>
        /// <param name="columns">字段</param>
        /// <param name="type">连接类型</param>
        protected void AddJoin(string tabName, string tabAs, string joinWhere, string columns = "", JoinType type = JoinType.Left)
        {
            JoinTab.Add(new JoinTable(tabName, tabAs, columns, joinWhere, type));
        }
        /// <summary>
        /// 设置属性
        /// </summary>
        protected abstract void SetCfg();
    }
}
