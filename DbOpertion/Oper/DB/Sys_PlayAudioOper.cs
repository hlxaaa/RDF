using System.Text;
using Common.Helper;
using System;
using System.Collections.Generic;
using Common;
using System.Linq;
using DbOpertion.Models;
using System.Data.SqlClient;

namespace DbOpertion.DBoperation
{
    public partial class Sys_PlayAudioOper : SingleTon<Sys_PlayAudioOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_playaudio"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Sys_PlayAudio sys_playaudio)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_playaudio.Id != null)
                dict.Add("@Id", sys_playaudio.Id.ToString());
            if (sys_playaudio.Title != null)
                dict.Add("@Title", sys_playaudio.Title.ToString());
            if (sys_playaudio.TypeId != null)
                dict.Add("@TypeId", sys_playaudio.TypeId.ToString());
            if (sys_playaudio.Url != null)
                dict.Add("@Url", sys_playaudio.Url.ToString());
            if (sys_playaudio.LongTime != null)
                dict.Add("@LongTime", sys_playaudio.LongTime.ToString());
            if (sys_playaudio.EditTime != null)
                dict.Add("@EditTime", sys_playaudio.EditTime.ToString());
            if (sys_playaudio.PlayCount != null)
                dict.Add("@PlayCount", sys_playaudio.PlayCount.ToString());
            if (sys_playaudio.Enabled != null)
                dict.Add("@Enabled", sys_playaudio.Enabled.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sys_playaudio"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Sys_PlayAudio sys_playaudio)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (sys_playaudio.Title != null)
            {
                part1.Append("Title,");
                part2.Append("@Title,");
            }
            if (sys_playaudio.TypeId != null)
            {
                part1.Append("TypeId,");
                part2.Append("@TypeId,");
            }
            if (sys_playaudio.Url != null)
            {
                part1.Append("Url,");
                part2.Append("@Url,");
            }
            if (sys_playaudio.LongTime != null)
            {
                part1.Append("LongTime,");
                part2.Append("@LongTime,");
            }
            if (sys_playaudio.EditTime != null)
            {
                part1.Append("EditTime,");
                part2.Append("@EditTime,");
            }
            if (sys_playaudio.PlayCount != null)
            {
                part1.Append("PlayCount,");
                part2.Append("@PlayCount,");
            }
            if (sys_playaudio.Enabled != null)
            {
                part1.Append("Enabled,");
                part2.Append("@Enabled,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into sys_playaudio(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_playaudio"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Sys_PlayAudio sys_playaudio)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_playaudio set ");
            if (sys_playaudio.Title != null)
                part1.Append("Title = @Title,");
            if (sys_playaudio.TypeId != null)
                part1.Append("TypeId = @TypeId,");
            if (sys_playaudio.Url != null)
                part1.Append("Url = @Url,");
            if (sys_playaudio.LongTime != null)
                part1.Append("LongTime = @LongTime,");
            if (sys_playaudio.EditTime != null)
                part1.Append("EditTime = @EditTime,");
            if (sys_playaudio.PlayCount != null)
                part1.Append("PlayCount = @PlayCount,");
            if (sys_playaudio.Enabled != null)
                part1.Append("Enabled = @Enabled,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where Id= @Id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Sys_PlayAudio"></param>
        /// <returns></returns>
        public int Add(Sys_PlayAudio model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_PlayAudio"></param>
        /// <returns></returns>
        public int Update(Sys_PlayAudio model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Sys_PlayAudio"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Sys_PlayAudio where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_playaudio"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Sys_PlayAudio sys_playaudio,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_playaudio.Id != null)
                dict.Add("@Id" + i, sys_playaudio.Id.ToString());
            if (sys_playaudio.Title != null)
                dict.Add("@Title" + i, sys_playaudio.Title.ToString());
            if (sys_playaudio.TypeId != null)
                dict.Add("@TypeId" + i, sys_playaudio.TypeId.ToString());
            if (sys_playaudio.Url != null)
                dict.Add("@Url" + i, sys_playaudio.Url.ToString());
            if (sys_playaudio.LongTime != null)
                dict.Add("@LongTime" + i, sys_playaudio.LongTime.ToString());
            if (sys_playaudio.EditTime != null)
                dict.Add("@EditTime" + i, sys_playaudio.EditTime.ToString());
            if (sys_playaudio.PlayCount != null)
                dict.Add("@PlayCount" + i, sys_playaudio.PlayCount.ToString());
            if (sys_playaudio.Enabled != null)
                dict.Add("@Enabled" + i, sys_playaudio.Enabled.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_playaudio"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Sys_PlayAudio sys_playaudio, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_playaudio set ");
            if (sys_playaudio.Title != null)
                part1.Append($"Title = @Title{i},");
            if (sys_playaudio.TypeId != null)
                part1.Append($"TypeId = @TypeId{i},");
            if (sys_playaudio.Url != null)
                part1.Append($"Url = @Url{i},");
            if (sys_playaudio.LongTime != null)
                part1.Append($"LongTime = @LongTime{i},");
            if (sys_playaudio.EditTime != null)
                part1.Append($"EditTime = @EditTime{i},");
            if (sys_playaudio.PlayCount != null)
                part1.Append($"PlayCount = @PlayCount{i},");
            if (sys_playaudio.Enabled != null)
                part1.Append($"Enabled = @Enabled{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where Id= @Id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_PlayAudio"></param>
        /// <returns></returns>
        public void UpdateList(List<Sys_PlayAudio> list, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = "";
            var dict = new Dictionary<string,string>();
            for(int i=0;i<list.Count;i++)
            {
            var tempDict=GetParametersItem(list[i],i);
            foreach(var item in tempDict)
            {
            dict.Add(item.Key,item.Value);
            }
            str+=GetUpdateStrItem(list[i],i);
            }
            SqlHelper.Instance.ExcuteNon(str, dict, connection, transaction);
        }
        /// <summary>
        /// getById
        /// </summary>
        /// <param name="Sys_PlayAudio"></param>
        /// <returns></returns>
        public Sys_PlayAudio GetById(int id)
        {
            return SqlHelper.Instance.GetById<Sys_PlayAudio>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Sys_PlayAudio"></param>
        /// <returns></returns>
        public List<Sys_PlayAudio> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Sys_PlayAudio>();
        }
    }
}
