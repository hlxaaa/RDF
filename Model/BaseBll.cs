using System.Collections.Generic;
using Tools;

namespace Model
{
    public class BaseBll
    {
        /// <summary>
        /// 实体业务
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="trans">是否开启事务</param>
        public BaseBll(BaseModel entity,bool trans=false)
        {
            Entity = entity;
            Trans = trans;
        }
        /// <summary>
        /// 业务是否开启事务
        /// </summary>
        public bool Trans;

        protected BaseModel Entity;

        /// <summary>
        /// 修改前验证
        /// </summary>
        /// <returns></returns>
        public virtual RdfMsg EditValiDate()
        {
            return new RdfMsg(true);
        }
        /// <summary>
        /// 修改前业务操作
        /// </summary>
        /// <returns></returns>
        public virtual RdfMsg BeforeEdit()
        {
            return new RdfMsg(true);
        }
        /// <summary>
        /// 修改后业务操作
        /// </summary>
        /// <returns></returns>
        public virtual RdfMsg AfterEdit()
        {
            return new RdfMsg(true);
        }
        /// <summary>
        /// 新增前验证
        /// </summary>
        /// <returns></returns>
        public virtual RdfMsg InsertValiDate()
        {
            return new RdfMsg(true);
        }
        /// <summary>
        /// 新增前业务操作
        /// </summary>
        /// <returns></returns>
        public virtual RdfMsg BeforeInsert()
        {
            return new RdfMsg(true);
        }
        /// <summary>
        /// 新增后业务操作
        /// </summary>
        /// <returns></returns>
        public virtual RdfMsg AfterInsert()
        {
            return new RdfMsg(true);
        }
        /// <summary>
        /// 删除前验证
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public virtual RdfMsg DeleteValiDate(Condition condition)
        {
            return new RdfMsg(true);
        }

        /// <summary>
        /// 删除前业务操作
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public virtual RdfMsg BeforeDelete(Condition condition)
        {
            return new RdfMsg(true);
        }

        /// <summary>
        /// 删除后业务操作
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public virtual RdfMsg AfterDelete(Condition condition)
        {
            return new RdfMsg(true);
        }
        /// <summary>
        /// 审核前验证
        /// </summary>
        /// <param name="list">list</param>
        /// <param name="isUnAudit">是否是反审核</param>
        /// <returns></returns>
        public virtual RdfMsg AuditValiDate<T>(List<T> list,bool isUnAudit=false)where T:BaseModel
        {
            return new RdfMsg(true);
        }

        /// <summary>
        /// 审核前业务操作
        /// </summary>
        /// <param name="list">ids</param>
        /// <param name="isUnAudit">是否是反审核</param>
        /// <returns></returns>
        public virtual RdfMsg BeforeAudit<T>(List<T> list, bool isUnAudit = false) where T : BaseModel
        {
            return new RdfMsg(true);
        }

        /// <summary>
        /// 审核后业务操作
        /// </summary>
        /// <param name="list">ids</param>
        /// <param name="isUnAudit">是否是反审核</param>
        /// <returns></returns>
        public virtual RdfMsg AfterAudit<T>(List<T> list, bool isUnAudit = false) where T : BaseModel
        {
            return new RdfMsg(true);
        }
    }
}
