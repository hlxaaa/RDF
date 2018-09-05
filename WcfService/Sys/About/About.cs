using Tools;
using System;
using Model;

namespace WcfService.Sys
{
    /// <summary>
    /// 关于我们
    /// </summary>
    public class About : BaseOpertion
    {
        public About() : base(8) { }

        /// <summary>
        /// 获取关于我们
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetAbout(dynamic param)
        {
            Sys_About about = new RdfSqlQuery<Sys_About>().ToEntity();
            if (about == null)
                return new RdfMsg(true, "暂无信息!");
            return new RdfMsg(true, about.Content);
        }
        /// <summary>
        /// 编辑关于我们
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg EditAbout(dynamic param)
        {
            if (!param.Exists("Content"))
                return new RdfMsg(false, "缺少参数Content!");
            if (UserInfo.UId != "admin")
                return new RdfMsg(false, "您不是管理员不能编辑!");
            string content = param.Content;
            if (content == null)
                content = "";
            Sys_About about = new RdfSqlQuery<Sys_About>().ToEntity();
            if (about == null)
            {
                about = new Sys_About();
                about.Content = content;
                about.EditorId = UserInfo.Id;
                about.EditTime = DateTime.Now;
                return about.Insert(true);
            }
            else
            {
                about.Content = content;
                about.EditorId = UserInfo.Id;
                about.EditTime = DateTime.Now;
                return about.Edit();
            }
        }
    }
}
