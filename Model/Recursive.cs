using System;
using System.Collections.Generic;

namespace Model
{
    public class Recursive
    {
        public Recursive()
        {
        }

        /// <summary>
        /// 递归函数初始化
        /// </summary>
        /// <param name="condition">上级条件</param>
        /// <param name="id">上级Id(最上层的Id)</param>
        /// <param name="pId">子集对应上级的Id</param>
        /// <param name="combination">组合递归</param>
        /// <param name="distinct">排除重复数据</param>
        public Recursive(Condition condition, string id, string pId, bool combination = false, bool distinct = false)
        {
            PArea = new ConditionArea();
            PArea.ConditionList.Add(condition);
            PIdField = id;
            CpIdField = pId;
            Combination = combination;
            Distinct = distinct;
        }
        /// <summary>
        /// 递归函数初始化
        /// </summary>
        /// <param name="list">上级条件列表</param>
        /// <param name="id">上级Id(最上层的Id)</param>
        /// <param name="pId">子集对应上级的Id</param>
        /// <param name="combination">组合递归</param>
        /// <param name="distinct">排除重复数据</param>
        public Recursive(List<Condition> list, string id, string pId, bool combination = false, bool distinct = false)
        {
            PArea = new ConditionArea { ConditionList = list };
            PIdField = id;
            CpIdField = pId;
            Combination = combination;
            Distinct = distinct;
        }
        /// <summary>
        /// 递归函数初始化
        /// </summary>
        /// <param name="area">上级条件组</param>
        /// <param name="id">上级Id(最上层的Id)</param>
        /// <param name="pId">子集对应上级的Id</param>
        /// <param name="combination">组合递归</param>
        /// <param name="distinct">排除重复数据</param>
        public Recursive(ConditionArea area, string id, string pId, bool combination = false, bool distinct = false)
        {
            PArea = area;
            PIdField = id;
            CpIdField = pId;
            Combination = combination;
            Distinct = distinct;
        }
        public ConditionArea PArea { get; set; }
        /// <summary>
        /// 上级Id(最上层的Id)
        /// </summary>
        public string PIdField { get; set; }
        /// <summary>
        /// 子集对应上级的Id
        /// </summary>
        public string CpIdField { get; set; }
        /// <summary>
        /// 组合递归
        /// 默认单表自身递归，表本身就是递归表
        /// 组合递归则表本身不是递归表，但是有递归表的引用，借助于引用也可进行递归
        /// 组合递归支持单表递归，单表递归不支持组合递归
        /// </summary>
        public bool Combination { get; set; }
        /// <summary>
        /// 排除重复数据，如果上级条件查询的结果集不止一条，可能会有重复
        /// </summary>
        public bool Distinct { get; set; }
        public BaseCfg Cfg { get; set; }
        /// <summary>
        /// 取得递归查询sql
        /// </summary>
        /// <param name="recursive"></param>
        public string RecursiveSearch()
        {
            if (Cfg == null)
                return "";
            Field id = null, pId = null;
            foreach (Field item in Cfg.FieldList)
            {
                if (Combination)
                {
                    if (item.ColumnAs.Equals(PIdField))
                        id = item;
                    if (item.ColumnAs.Equals(CpIdField))
                        pId = item;
                }
                else
                {
                    if (item.Column.Equals(PIdField))
                        id = item;
                    if (item.Column.Equals(CpIdField))
                        pId = item;
                }
                if (id != null && pId != null)
                    break;
            }
            if (id == null)
                throw new Exception("递归查询上级Id字段不存在！");
            if (pId == null)
                throw new Exception("递归查询子集对应上级的Id字段不存在！");
            return string.Format(@"with Tab_{0} as(select * from {4} {1} union all select t1.* from {4} t1 inner join Tab_{0} t2 on t1.{2} = t2.{3})
            ", Cfg.Tab.TabName, ConditionHandle.GetSqlWhere(Cfg, PArea, true), CpIdField, PIdField, Combination ? "#temp" : Cfg.Tab.TabName);
        }
    }
}
