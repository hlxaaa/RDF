using System;
using System.Runtime.Serialization;
using Tools;
namespace Model
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class PagingParam
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        [RdfNonSerialized]
        public int Index { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        [RdfNonSerialized]
        public int PageSize { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        [RdfNonSerialized]
        public int SumIndex { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        [RdfNonSerialized]
        public string Sort { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int SumCount { get; set; }
        /// <summary>
        /// 查询数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 查询数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 汇总数据
        /// </summary>
        public object Collect { get; set; }
        /// <summary>
        /// 获取空json
        /// </summary>
        /// <returns></returns>
        public string GetEmptyJson()
        {
            Data = new String[] {};
            Collect = new String[] { };
            return RdfSerializer.ObjToJson(this);
        }
    }
}
