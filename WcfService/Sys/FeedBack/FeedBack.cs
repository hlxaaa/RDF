using Tools;
using System;
using Model;
using System.Collections.Generic;
namespace WcfService.Sys
{
    /// <summary>
    /// 意见反馈
    /// </summary>
    public class FeedBack : BaseOpertion
    {
        public FeedBack() : base(8) { }

        /// <summary>
        /// 提交意见
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg AddFeedBack(dynamic param)
        {
            if (!param.Exists("Title"))
                return new RdfMsg(false, "缺少参数Title!");
            if (!param.Exists("Content"))
                return new RdfMsg(false, "缺少参数Content!");

            //int isEnglish = 0;
            //if (param.Exists("isEnglish"))
            //{
            //    var temp = Convert.ToInt32(param.isEnglish);
            //    isEnglish = temp == 1 ? 1 : 0;
            //}

            Sys_FeedBack entity = new Sys_FeedBack() { EditorId = UserInfo.Id, EditTime = DateTime.Now };
            entity.Title = param.Title;
            entity.Content = param.Content;
            //entity.isEnglish = isEnglish;
            if (string.IsNullOrWhiteSpace(entity.Title))
                return new RdfMsg(false, "标题不能为空!");
            if (string.IsNullOrWhiteSpace(entity.Content))
                return new RdfMsg(false, "内容不能为空!");
            return entity.Insert(true);
        }
        /// <summary>
        /// 获取意见反馈（app只能获取自己的反馈）
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg LoadFeedBack(dynamic param)
        {
            if (!param.Exists("pageSize"))
                return new RdfMsg(false, "参数pageSize不存在!");
            if (!param.Exists("pageIndex"))
                return new RdfMsg(false, "参数pageIndex不存在!");
            int size = Convert.ToInt32(param.pageSize);
            int index = Convert.ToInt32(param.pageIndex);
            var query = new RdfSqlQuery<Sys_FeedBack>().JoinTable<Sys_User>((t1, t2) => t1.EditorId == t2.Id);
            if (param.Exists("app"))
            {
                query = query.Where(t1 => t1.EditorId == UserInfo.Id);
            }
            else
            {
                if (UserInfo.UId != "admin")
                    return new RdfMsg(false, "您不是管理员不能查看意见反馈!");
            }
            if (param.Exists("search"))
            {
                string search = param.search;
                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(t1 => t1.Title.Contains(search) || t1.Content.Contains(search));
            }
            int sum = (int)query.Count(t1 => new { cnt = t1.Id }).ToObject();
            int pageCount = 1;
            if (sum % size == 0)
                pageCount = sum / size;
            else
                pageCount = (sum / size) + 1;
            List<Sys_FeedBack> list = query.Select("t1.*,t2.UserName", true).OrderByDesc(t1 => t1.Id).Take(size).PageIndex(index).ToList();

            return new RdfMsg(true, RdfSerializer.ObjToJson(list), pageCount);
        }
    }
}
