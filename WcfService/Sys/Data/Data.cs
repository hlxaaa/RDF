using Tools;
using System;
using Model;
using System.Collections.Generic;

namespace WcfService.Sys
{
    /// <summary>
    /// 数据
    /// </summary>
    public class Data : BaseOpertion
    {
        public Data() : base(6) { }
        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg UpLoadData(dynamic param)
        {
            Sys_Data data = new Sys_Data();
            data.DynamicObj(param);
            data.UserId = UserInfo.Id;
            data.CreateTime = DateTime.Now;
            return data.Insert();
        }
        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetUserData(dynamic param)
        {
            if (!param.Exists("userId"))
                return new RdfMsg(false, "缺少参数userId!");
            if (!param.Exists("date"))
                return new RdfMsg(false, "缺少参数date!");
            int userId = param.userId;
            DateTime time = Convert.ToDateTime(param.date);
            DateTime begin = Convert.ToDateTime(time.ToString("yyyy-MM-dd 00:00:00"));
            DateTime end = Convert.ToDateTime(time.ToString("yyyy-MM-dd 23:59:59"));
            List<Sys_Data> list = new RdfSqlQuery<Sys_Data>().Where(t1 => t1.UserId == userId && t1.CreateTime >= begin && t1.CreateTime <= end).OrderBy(t1 => t1.CreateTime).ToList();
            List<Sys_Data> newlist = new List<Sys_Data>();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    newlist.Add(list[i]);
                    break;
                }
                while (i < list.Count - 1 && list[i].CreateTime.Hour == list[i + 1].CreateTime.Hour)
                {
                    list[i + 1].KAL += list[i].KAL;
                    i++;
                }
                newlist.Add(list[i]);
            }
            return new RdfMsg(true, RdfSerializer.ObjToJson(newlist));
        }

    }
}
