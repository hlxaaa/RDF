using Model;
using System;
using Tools;

namespace WcfService.Sys
{
    /// <summary>
    /// 用户业务逻辑
    /// </summary>
    public class Sys_UserBll : BaseBll
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public Sys_UserBll(BaseModel entity, bool trans = false)
            : base(entity, trans)
        {

        }

        /// <summary>
        /// 新增前验证
        /// </summary>
        public override RdfMsg InsertValiDate()
        {
            return ValiLength();
        }

        /// <summary>
        /// 修改前验证
        /// </summary>
        public override RdfMsg EditValiDate()
        {
            return ValiLength();
        }

        /// <summary>
        /// 验证方法
        /// </summary>
        private RdfMsg ValiLength()
        {
            Sys_User entity = Entity as Sys_User;
            if (entity != null)
            {
                if (entity.UserName != null && entity.UserName.Length > 30)
                    return new RdfMsg(false, "姓名超过有效长度！");
                if (entity.Phone != null && entity.Phone.Length > 50)
                    return new RdfMsg(false, "手机号超过有效长度！");
                if (entity.UId != null && entity.UId.Length > 50)
                    return new RdfMsg(false, "UID超过有效长度！");
                if (entity.UserExplain != null && entity.UserExplain.Length > 200)
                    return new RdfMsg(false, "个性签名超过有效长度！");
                if (entity.Url != null && entity.Url.Length > 200)
                    return new RdfMsg(false, "头像超过有效长度！");
                if (string.IsNullOrWhiteSpace(entity.UId))
                    entity.UId = Guid.NewGuid().ToString();
                if (entity.UId == "admin" && !entity.Enabled)
                    return new RdfMsg(false, "admin账号不能禁用!");
                if (new RdfSqlQuery<Sys_User>().Where(item => item.UId == entity.UId && item.Id != entity.Id).ToEntity() != null)
                    return new RdfMsg(false, entity.UId + ":账号已存在！");
                if (new RdfSqlQuery<Sys_User>().Where(item => item.Phone == entity.Phone && item.Id != entity.Id).ToEntity() != null)
                    return new RdfMsg(false, entity.Phone + ":手机号已存在！");
            }
            return new RdfMsg(true);
        }
    }
}
