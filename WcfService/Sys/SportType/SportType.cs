using Tools;
using System;
using Model;
using System.Collections.Generic;

namespace WcfService.Sys
{
    /// <summary>
    /// 分类
    /// </summary>
    public class SportType : BaseOpertion
    {
        public SportType() : base(4) { }


        public RdfMsg GetAll(dynamic param)
        {
            return new RdfMsg(true, RdfSerializer.ObjToJson(new RdfSqlQuery<Sys_SportType>().Where(t1 => t1.Enabled == true).ToList()));
        }
        /// <summary>
        /// 根据Id获取类型信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetSportTypeById(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            Sys_SportType play = new Sys_SportType() { Id = param.Id };
            if (!play.GetEntity())
                return new RdfMsg(false, "类型不存在!");
            return new RdfMsg(true, play.ToJson());
        }
        /// <summary>
        /// 添加类型息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg AddSportType(dynamic param)
        {
            Sys_SportType play = new Sys_SportType() { Remark = "", Name = "", Enabled = true };
            play.Cfg.FieldList.ForEach(field =>
            {
                if (param.Exists(field.ColumnAs))
                {
                    object value = param[field.ColumnAs];
                    play.SetValue(value, field);
                }
            });
            RdfMsg msg = play.Insert(true);
            msg.Result = play.ToJson();
            return msg;
        }
        /// <summary>
        /// 编辑类型信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg EditSportType(dynamic param)
        {
            Sys_SportType play = new Sys_SportType();
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            play.Id = Convert.ToInt32(param.Id);
            if (!play.GetEntity())
                return new RdfMsg(false, "获取类型失败!");
            play.Cfg.FieldList.ForEach(field =>
            {
                if (field.ColumnAs != "Id" && param.Exists(field.ColumnAs))
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
        public RdfMsg LoadSportType(dynamic param)
        {
            if (!param.Exists("pageSize"))
                return new RdfMsg(false, "参数pageSize不存在!");
            if (!param.Exists("pageIndex"))
                return new RdfMsg(false, "参数pageIndex不存在!");
            int size = Convert.ToInt32(param.pageSize);
            int index = Convert.ToInt32(param.pageIndex);
            int isEnglish = 0;
            if (param.Exists("isEnglish"))
            {
                var temp = Convert.ToInt32(param.isEnglish);
                isEnglish = temp == 1 ? 1 : 0;
            }
            //int isEnglish = Convert.ToInt32(param.isEnglish);
            var query = new RdfSqlQuery<Sys_SportType>();
            if (param.Exists("app"))
            {
                query = query.Where(t1 => t1.Enabled == true);
            }
            if (param.Exists("search"))
            {
                string search = param.search;
                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(t1 => t1.Name.Contains(search) || t1.Remark.Contains(search));
            }
            //query = query.Where(p => p.isEnglish == isEnglish);
            int sum = (int)query.Count(t1 => new { cnt = t1.Id }).ToObject();
            int pageCount = 1;
            if (sum % size == 0)
                pageCount = sum / size;
            else
                pageCount = (sum / size) + 1;
            //.Where(p => p.isEnglish == isEnglish)
            List<Sys_SportType> list = query.Select("*", true).OrderBy(t1 => t1.Id).Take(size).PageIndex(index).ToList();
            if (isEnglish == 1)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Name = list[i].NameE;
                }
            }
            return new RdfMsg(true, RdfSerializer.ObjToJson(list), pageCount);
        }
    }
}
