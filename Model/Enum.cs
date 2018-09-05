namespace Model
{
    /// <summary>
    /// 字段类型
    /// </summary>
    public enum ColumnType
    {
        /// <summary>
        /// 字符型
        /// </summary>
        String,
        /// <summary>
        /// 整形
        /// </summary>
        Int,
        /// <summary>
        /// 数字型
        /// </summary>
        Decimal,
        /// <summary>
        /// 时间类型
        /// </summary>
        DateTime,
        /// <summary>
        /// 唯一身份标识
        /// </summary>
        Guid,
        /// <summary>
        /// Bit
        /// </summary>
        Bit,
        /// <summary>
        /// 日期
        /// </summary>
        Date,
        /// <summary>
        /// 长整形
        /// </summary>
        Long
    }
    /// <summary>
    /// 关系
    /// </summary>
    public enum Relation
    {
        /// <summary>
        /// 并且
        /// </summary>
        And,
        /// <summary>
        /// 或者
        /// </summary>
        Or
    }
    /// <summary>
    /// 条件比较
    /// </summary>
    public enum Compare
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equals,
        /// <summary>
        /// 不等于
        /// </summary>
        NotEquals,
        /// <summary>
        /// 大于
        /// </summary>
        Greater,
        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterEquals,
        /// <summary>
        /// 小于
        /// </summary>
        Less,
        /// <summary>
        /// 小于等于
        /// </summary>
        LessEquals,
        /// <summary>
        /// 类似于
        /// </summary>
        Like,
        /// <summary>
        /// 不类似于
        /// </summary>
        NotLike,
        /// <summary>
        /// 左包含
        /// </summary>
        LeftLike,
        /// <summary>
        /// 右包含
        /// </summary>
        RightLike,
        /// <summary>
        /// 等于多个值
        /// </summary>
        In,
        /// <summary>
        /// 不等于多个值
        /// </summary>
        NotIn,
        /// <summary>
        /// 今天
        /// </summary>
        ToDay,
        /// <summary>
        /// 昨天
        /// </summary>
        YesterDay,
        /// <summary>
        /// 明天
        /// </summary>
        Tomorrow,
        /// <summary>
        /// 本月
        /// </summary>
        CurrentMonth,
        /// <summary>
        /// 上月
        /// </summary>
        LastMonth,
        /// <summary>
        /// 下月
        /// </summary>
        NextMonth,
        /// <summary>
        /// 本年
        /// </summary>
        CurrentYear,
        /// <summary>
        /// 去年
        /// </summary>
        LastYear,
        /// <summary>
        /// 明年
        /// </summary>
        NextYear,
        /// <summary>
        /// sql
        /// </summary>
        Sql,
        /// <summary>
        /// 加
        /// </summary>
        Add,
        /// <summary>
        /// 减
        /// </summary>
        Subtract,
        /// <summary>
        /// 乘
        /// </summary>
        Multiply,
        /// <summary>
        /// 除
        /// </summary>
        Divide,
        /// <summary>
        /// %取模
        /// </summary>
        Modulo,
        /// <summary>
        /// 是
        /// </summary>
        Is,
        /// <summary>
        /// 不是
        /// </summary>
        IsNot
    }
    /// <summary>
    /// 聚合函数
    /// </summary>
    public enum AggregateType
    {
        /// <summary>
        /// 数量
        /// </summary>
        Count,
        /// <summary>
        /// 最大值
        /// </summary>
        Max,
        /// <summary>
        /// 最小值
        /// </summary>
        Min,
        /// <summary>
        /// 求和
        /// </summary>
        Sum,
        /// <summary>
        /// 求平均
        /// </summary>
        Avg
    }
    /// <summary>
    /// sql连接
    /// </summary>
    public enum JoinType
    {
        Inner,
        Left,
        Right
    }
    /// <summary>
    /// 查询解析类型
    /// </summary>
    public enum QueryableType
    {
        /// <summary>
        /// 查询字段
        /// </summary>
        Select,
        /// <summary>
        /// 条件
        /// </summary>
        Where,
        /// <summary>
        /// 关联
        /// </summary>
        Join,
        /// <summary>
        /// 排序
        /// </summary>
        OrderBy,
        /// <summary>
        /// 分组
        /// </summary>
        GroupBy,
        /// <summary>
        /// 数量
        /// </summary>
        Count,
        /// <summary>
        /// 最小值
        /// </summary>
        Min,
        /// <summary>
        /// 最大值
        /// </summary>
        Max,
        /// <summary>
        /// 求和
        /// </summary>
        Sum,
        /// <summary>
        /// 求平均
        /// </summary>
        Avg
    }
}
