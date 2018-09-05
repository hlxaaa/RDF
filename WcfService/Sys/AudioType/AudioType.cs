using Tools;
using System;
using Model;
using System.Collections.Generic;

namespace WcfService.Sys
{
    /// <summary>
    /// 音频专辑
    /// </summary>
    public class AudioType : BaseOpertion
    {
        public AudioType() : base(2) { }

        public RdfMsg GetAll(dynamic param)
        {
            return new RdfMsg(true, RdfSerializer.ObjToJson(new RdfSqlQuery<Sys_AudioType>().Where(t1 => t1.Enabled == true).ToList()));
        }
        /// <summary>
        /// 根据Id获取音频专辑信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetAudioTypeById(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            Sys_AudioType play = new Sys_AudioType() { Id = param.Id };
            if (!play.GetEntity())
                return new RdfMsg(false, "专辑信息不存在!");
            return new RdfMsg(true, play.ToJson());
        }
        /// <summary>
        /// 添加音频专辑息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg AddAudioType(dynamic param)
        {
            Sys_AudioType play = new Sys_AudioType() { Remark = "", TitleUrl = "", Enabled = true };
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
                play.TitleUrl = "UpLoadFile\\AudioTypeImage\\" + play.Id.ToString() + ".jpg";
                play.Edit();
                msg.Result = play.ToJson();
            }
            return msg;
        }
        /// <summary>
        /// 编辑音频专辑信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg EditAudioType(dynamic param)
        {
            Sys_AudioType play = new Sys_AudioType();
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            play.Id = Convert.ToInt32(param.Id);
            if (!play.GetEntity())
                return new RdfMsg(false, "获取专辑信息失败!");
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
        public RdfMsg LoadAudioType(dynamic param)
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
            var query = new RdfSqlQuery<Sys_AudioType>();
            if (param.Exists("search"))
            {
                string search = param.search;
                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(t1 => (t1.Title.Contains(search) || t1.Remark.Contains(search)));
            }
            if (param.Exists("app"))
            {
                query.Where(t1 => t1.Enabled == true);
            }
            //query = query.Where(t1 => t1.TitleE == "EnglishName");
            int sum = (int)query.Count(t1 => new { cnt = t1.Id }).ToObject();
            int pageCount = 1;
            if (sum % size == 0)
                pageCount = sum / size;
            else
                pageCount = (sum / size) + 1;
            List<Sys_AudioType> list = query.Select("*", true).OrderBy(t1 => t1.Id).Take(size).PageIndex(index).ToList();
            if (list.Count > 0 && param.Exists("app"))
            {
                List<int> ids = list.ConvertAll(item => item.Id);
                List<Sys_PlayAudio> a_list = new RdfSqlQuery<Sys_PlayAudio>().Where(t1 => ids.Contains(t1.TypeId)).ToList();
                list.ForEach(en =>
                {
                    en.Audios = a_list.FindAll(a => a.TypeId == en.Id);
                });
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
