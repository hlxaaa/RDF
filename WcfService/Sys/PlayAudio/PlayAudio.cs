using Tools;
using System;
using System.Collections.Generic;
using Model;
using System.Linq;
using System.Text;

namespace WcfService.Sys
{
    /// <summary>
    /// 点播音频
    /// </summary>
    public class PlayAudio : BaseOpertion
    {
        public PlayAudio() : base(2) { }
        /// <summary>
        /// 播放次数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg SetPalyCount(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            Sys_PlayAudio play = new Sys_PlayAudio() { Id = param.Id };
            if (!play.GetEntity())
                return new RdfMsg(false, "音频信息不存在!");
            play.PlayCount++;
            return play.Edit();
        }

        /// <summary>
        /// 根据Id获取点播音频信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetPlayAudioById(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            Sys_PlayAudio play = new Sys_PlayAudio() { Id = param.Id };
            if (!play.GetEntity())
                return new RdfMsg(false, "音频信息不存在!");
            return new RdfMsg(true, play.ToJson());
        }
        /// <summary>
        /// 添加点播音频信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg AddPlayAudio(dynamic param)
        {
            Sys_PlayAudio play = new Sys_PlayAudio() {Url="",EditTime=DateTime.Now,Enabled=true };
            play.Cfg.FieldList.ForEach(field =>
            {
                if (param.Exists(field.ColumnAs))
                {
                    object value = param[field.ColumnAs];
                    play.SetValue(value, field);
                }
            });
            RdfMsg msg= play.Insert(true);
            if (msg.Success)
                msg.Result = play.ToJson();
            return msg;
        }
        /// <summary>
        /// 编辑点播音频信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg EditPlayAudio(dynamic param)
        {
            Sys_PlayAudio play = new Sys_PlayAudio();
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            play.Id = Convert.ToInt32(param.Id);
            if (!play.GetEntity())
                return new RdfMsg(false, "获取音频信息失败!");
            play.Cfg.FieldList.ForEach(field =>
            {
                if (field.ColumnAs != "Id" && param.Exists(field.ColumnAs))
                {
                    object value = param[field.ColumnAs];
                    play.SetValue(value, field);
                }
            });
            play.EditTime = DateTime.Now;
            RdfMsg msg = play.Edit();
            if (msg.Success)
                msg.Result = play.ToJson();
            return msg;
        }
        public RdfMsg LoadPlayAudio(dynamic param)
        {
            if (!param.Exists("pageSize"))
                return new RdfMsg(false, "参数pageSize不存在!");
            if (!param.Exists("pageIndex"))
                return new RdfMsg(false, "参数pageIndex不存在!");
            int size = Convert.ToInt32(param.pageSize);
            int index = Convert.ToInt32(param.pageIndex);
            var query = new RdfSqlQuery<Sys_PlayAudio>().JoinTable<Sys_AudioType>((t1,t2)=>t1.TypeId==t2.Id);
            if (param.Exists("search"))
            {
                string search = param.search;
                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where<Sys_AudioType>((t1, t2) => t1.Title.Contains(search) || t1.LongTime.ToString().Contains(search) || t1.PlayCount.ToString().Contains(search) || t2.Title.Contains(search));
            }
            int sum = (int)query.Count(t1 => new { cnt = t1.Id }).ToObject();
            int pageCount = 1;
            if (sum % size == 0)
                pageCount = sum / size;
            else
                pageCount = (sum / size) + 1;
            List<Sys_PlayAudio> list = query.Select("t1.*,isnull(t2.Title,'') TypeName", true).OrderBy(t1 => t1.Id).Take(size).PageIndex(index).ToList();
            return new RdfMsg(true, RdfSerializer.ObjToJson(list), pageCount);
        }
    }
}
