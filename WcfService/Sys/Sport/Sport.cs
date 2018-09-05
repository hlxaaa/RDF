using Tools;
using System;
using Model;
using System.Collections.Generic;
using Common.Helper;

namespace WcfService.Sys
{
    /// <summary>
    /// 运动干活
    /// </summary>
    public class Sport : BaseOpertion
    {
        public Sport() : base(4) { }
        /// <summary>
        /// 根据Id获取运动干活
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetSportById(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            int id = Convert.ToInt32(param.Id.ToString());
            Sys_Sport play = new Sys_Sport() { Id = id };
            if (!play.GetEntity())
                return new RdfMsg(false, "数据不存在!");
            return new RdfMsg(true, play.ToJson());
        }
        public RdfMsg GetContent(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            int id = Convert.ToInt32(param.Id.ToString());
            if (id == 0)
            {
                About about = new About();
                return about.GetAbout(param);
            }
            else
            {
                Sys_Sport play = new Sys_Sport() { Id = id };
                if (!play.GetEntity())
                    return new RdfMsg(false, "数据不存在!");
                return new RdfMsg(true, play.Content);
            }
        }
        /// <summary>
        /// 添加运动干活
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg AddSport(dynamic param)
        {
            Sys_Sport play = new Sys_Sport() { Remark = "", TitleUrl = "", CreateTime = DateTime.Now, Enabled = true };
            play.Cfg.FieldList.ForEach(field =>
            {
                if (field.ColumnAs != "TitleUrl" && param.Exists(field.ColumnAs))
                {
                    object value = param[field.ColumnAs];
                    play.SetValue(value, field);
                }
            });
            RdfMsg msg = play.Insert(true);
            if (msg.Success)
            {
                play.TitleUrl = "UpLoadFile\\SportImage\\" + play.Id.ToString() + ".jpg";
                play.Edit();
                msg.Result = play.ToJson();
            }
            return msg;
        }
        /// <summary>
        /// 编辑运动干活信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg EditSport(dynamic param)
        {
            Sys_Sport play = new Sys_Sport();
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            play.Id = Convert.ToInt32(param.Id);
            if (!play.GetEntity())
                return new RdfMsg(false, "获取数据失败!");
            play.Cfg.FieldList.ForEach(field =>
            {
                if (field.ColumnAs != "TitleUrl" && field.ColumnAs != "Id" && param.Exists(field.ColumnAs))
                {
                    object value = param[field.ColumnAs];
                    play.SetValue(value, field);
                }
            });
            RdfMsg msg = play.Edit();
            if (msg.Success)
                msg.Result = play.ToJson();
            return msg;
        }
        public RdfMsg LoadSport(dynamic param)
        {
            if (!param.Exists("pageSize"))
                return new RdfMsg(false, "参数pageSize不存在!");
            if (!param.Exists("pageIndex"))
                return new RdfMsg(false, "参数pageIndex不存在!");
            if (!param.Exists("type"))
                return new RdfMsg(false, "参数type不存在!");
            int size = Convert.ToInt32(param.pageSize);
            int index = Convert.ToInt32(param.pageIndex);
            int type = Convert.ToInt32(param.type);

            int isEnglish = 0;
            if (param.Exists("isEnglish"))
            {
                var temp = Convert.ToInt32(param.isEnglish);
                isEnglish = temp == 1 ? 1 : 0;
            }

            var query = new RdfSqlQuery<Sys_Sport>().JoinTable<Sys_SportType>((t1, t2) => t1.TypeId == t2.Id).Where(t1 => t1.DataType == type);
            if (param.Exists("app"))
            {
                query = query.Where(t1 => t1.Enabled == true);
                if (type == 0)
                {
                    int app = Convert.ToInt32(param.app.ToString());
                    query = query.Where(t1 => t1.TypeId == app);
                }
            }
            if (param.Exists("search"))
            {
                string search = param.search;
                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where<Sys_SportType>((t1, t2) => t1.Title.Contains(search) || t1.Remark.Contains(search) || t2.Name.Contains(search));
            }
            int sum = (int)query.Count(t1 => new { cnt = t1.Id }).ToObject();
            int pageCount = 1;
            if (sum % size == 0)
                pageCount = sum / size;
            else
                pageCount = (sum / size) + 1;
            List<Sys_Sport> list = query.Select("t1.Id,t1.Title,t1.TitleUrl,t1.Remark,t1.CreateTime,t1.Enabled,isnull(t2.Name,'')TypeName", true).OrderBy(t1 => t1.Id).Take(size).PageIndex(index).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                list[i].TitleUrl = StringHelper.Instance.GetApiUrl(list[i].TitleUrl);
            }

            if (isEnglish == 1)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Title = list[i].TitleE;
                    list[i].Remark = list[i].RemarkE;
                }
            }
            return new RdfMsg(true, RdfSerializer.ObjToJson(list), pageCount);
        }

    }
}
