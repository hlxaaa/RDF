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
    public partial class Sys_VideoInfoOper : SingleTon<Sys_VideoInfoOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_videoinfo"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Sys_VideoInfo sys_videoinfo)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_videoinfo.Id != null)
                dict.Add("@Id", sys_videoinfo.Id.ToString());
            if (sys_videoinfo.UserId != null)
                dict.Add("@UserId", sys_videoinfo.UserId.ToString());
            if (sys_videoinfo.Url != null)
                dict.Add("@Url", sys_videoinfo.Url.ToString());
            if (sys_videoinfo.Title != null)
                dict.Add("@Title", sys_videoinfo.Title.ToString());
            if (sys_videoinfo.BeginTime != null)
                dict.Add("@BeginTime", sys_videoinfo.BeginTime.ToString());
            if (sys_videoinfo.PlayLongTime != null)
                dict.Add("@PlayLongTime", sys_videoinfo.PlayLongTime.ToString());
            if (sys_videoinfo.PlayStatus != null)
                dict.Add("@PlayStatus", sys_videoinfo.PlayStatus.ToString());
            if (sys_videoinfo.DataStatus != null)
                dict.Add("@DataStatus", sys_videoinfo.DataStatus.ToString());
            if (sys_videoinfo.CloudId != null)
                dict.Add("@CloudId", sys_videoinfo.CloudId.ToString());
            if (sys_videoinfo.VideoId != null)
                dict.Add("@VideoId", sys_videoinfo.VideoId.ToString());
            if (sys_videoinfo.isPass != null)
                dict.Add("@isPass", sys_videoinfo.isPass.ToString());
            if (sys_videoinfo.price != null)
                dict.Add("@price", sys_videoinfo.price.ToString());
            if (sys_videoinfo.isEnglish != null)
                dict.Add("@isEnglish", sys_videoinfo.isEnglish.ToString());
            if (sys_videoinfo.TitleE != null)
                dict.Add("@TitleE", sys_videoinfo.TitleE.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sys_videoinfo"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Sys_VideoInfo sys_videoinfo)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (sys_videoinfo.UserId != null)
            {
                part1.Append("UserId,");
                part2.Append("@UserId,");
            }
            if (sys_videoinfo.Url != null)
            {
                part1.Append("Url,");
                part2.Append("@Url,");
            }
            if (sys_videoinfo.Title != null)
            {
                part1.Append("Title,");
                part2.Append("@Title,");
            }
            if (sys_videoinfo.BeginTime != null)
            {
                part1.Append("BeginTime,");
                part2.Append("@BeginTime,");
            }
            if (sys_videoinfo.PlayLongTime != null)
            {
                part1.Append("PlayLongTime,");
                part2.Append("@PlayLongTime,");
            }
            if (sys_videoinfo.PlayStatus != null)
            {
                part1.Append("PlayStatus,");
                part2.Append("@PlayStatus,");
            }
            if (sys_videoinfo.DataStatus != null)
            {
                part1.Append("DataStatus,");
                part2.Append("@DataStatus,");
            }
            if (sys_videoinfo.CloudId != null)
            {
                part1.Append("CloudId,");
                part2.Append("@CloudId,");
            }
            if (sys_videoinfo.VideoId != null)
            {
                part1.Append("VideoId,");
                part2.Append("@VideoId,");
            }
            if (sys_videoinfo.isPass != null)
            {
                part1.Append("isPass,");
                part2.Append("@isPass,");
            }
            if (sys_videoinfo.price != null)
            {
                part1.Append("price,");
                part2.Append("@price,");
            }
            if (sys_videoinfo.isEnglish != null)
            {
                part1.Append("isEnglish,");
                part2.Append("@isEnglish,");
            }
            if (sys_videoinfo.TitleE != null)
            {
                part1.Append("TitleE,");
                part2.Append("@TitleE,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into sys_videoinfo(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_videoinfo"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Sys_VideoInfo sys_videoinfo)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_videoinfo set ");
            if (sys_videoinfo.UserId != null)
                part1.Append("UserId = @UserId,");
            if (sys_videoinfo.Url != null)
                part1.Append("Url = @Url,");
            if (sys_videoinfo.Title != null)
                part1.Append("Title = @Title,");
            if (sys_videoinfo.BeginTime != null)
                part1.Append("BeginTime = @BeginTime,");
            if (sys_videoinfo.PlayLongTime != null)
                part1.Append("PlayLongTime = @PlayLongTime,");
            if (sys_videoinfo.PlayStatus != null)
                part1.Append("PlayStatus = @PlayStatus,");
            if (sys_videoinfo.DataStatus != null)
                part1.Append("DataStatus = @DataStatus,");
            if (sys_videoinfo.CloudId != null)
                part1.Append("CloudId = @CloudId,");
            if (sys_videoinfo.VideoId != null)
                part1.Append("VideoId = @VideoId,");
            if (sys_videoinfo.isPass != null)
                part1.Append("isPass = @isPass,");
            if (sys_videoinfo.price != null)
                part1.Append("price = @price,");
            if (sys_videoinfo.isEnglish != null)
                part1.Append("isEnglish = @isEnglish,");
            if (sys_videoinfo.TitleE != null)
                part1.Append("TitleE = @TitleE,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where Id= @Id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Sys_VideoInfo"></param>
        /// <returns></returns>
        public int Add(Sys_VideoInfo model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_VideoInfo"></param>
        /// <returns></returns>
        public int Update(Sys_VideoInfo model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Sys_VideoInfo"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Sys_VideoInfo where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_videoinfo"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Sys_VideoInfo sys_videoinfo,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_videoinfo.Id != null)
                dict.Add("@Id" + i, sys_videoinfo.Id.ToString());
            if (sys_videoinfo.UserId != null)
                dict.Add("@UserId" + i, sys_videoinfo.UserId.ToString());
            if (sys_videoinfo.Url != null)
                dict.Add("@Url" + i, sys_videoinfo.Url.ToString());
            if (sys_videoinfo.Title != null)
                dict.Add("@Title" + i, sys_videoinfo.Title.ToString());
            if (sys_videoinfo.BeginTime != null)
                dict.Add("@BeginTime" + i, sys_videoinfo.BeginTime.ToString());
            if (sys_videoinfo.PlayLongTime != null)
                dict.Add("@PlayLongTime" + i, sys_videoinfo.PlayLongTime.ToString());
            if (sys_videoinfo.PlayStatus != null)
                dict.Add("@PlayStatus" + i, sys_videoinfo.PlayStatus.ToString());
            if (sys_videoinfo.DataStatus != null)
                dict.Add("@DataStatus" + i, sys_videoinfo.DataStatus.ToString());
            if (sys_videoinfo.CloudId != null)
                dict.Add("@CloudId" + i, sys_videoinfo.CloudId.ToString());
            if (sys_videoinfo.VideoId != null)
                dict.Add("@VideoId" + i, sys_videoinfo.VideoId.ToString());
            if (sys_videoinfo.isPass != null)
                dict.Add("@isPass" + i, sys_videoinfo.isPass.ToString());
            if (sys_videoinfo.price != null)
                dict.Add("@price" + i, sys_videoinfo.price.ToString());
            if (sys_videoinfo.isEnglish != null)
                dict.Add("@isEnglish" + i, sys_videoinfo.isEnglish.ToString());
            if (sys_videoinfo.TitleE != null)
                dict.Add("@TitleE" + i, sys_videoinfo.TitleE.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_videoinfo"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Sys_VideoInfo sys_videoinfo, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_videoinfo set ");
            if (sys_videoinfo.UserId != null)
                part1.Append($"UserId = @UserId{i},");
            if (sys_videoinfo.Url != null)
                part1.Append($"Url = @Url{i},");
            if (sys_videoinfo.Title != null)
                part1.Append($"Title = @Title{i},");
            if (sys_videoinfo.BeginTime != null)
                part1.Append($"BeginTime = @BeginTime{i},");
            if (sys_videoinfo.PlayLongTime != null)
                part1.Append($"PlayLongTime = @PlayLongTime{i},");
            if (sys_videoinfo.PlayStatus != null)
                part1.Append($"PlayStatus = @PlayStatus{i},");
            if (sys_videoinfo.DataStatus != null)
                part1.Append($"DataStatus = @DataStatus{i},");
            if (sys_videoinfo.CloudId != null)
                part1.Append($"CloudId = @CloudId{i},");
            if (sys_videoinfo.VideoId != null)
                part1.Append($"VideoId = @VideoId{i},");
            if (sys_videoinfo.isPass != null)
                part1.Append($"isPass = @isPass{i},");
            if (sys_videoinfo.price != null)
                part1.Append($"price = @price{i},");
            if (sys_videoinfo.isEnglish != null)
                part1.Append($"isEnglish = @isEnglish{i},");
            if (sys_videoinfo.TitleE != null)
                part1.Append($"TitleE = @TitleE{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where Id= @Id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_VideoInfo"></param>
        /// <returns></returns>
        public void UpdateList(List<Sys_VideoInfo> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="Sys_VideoInfo"></param>
        /// <returns></returns>
        public Sys_VideoInfo GetById(int id)
        {
            return SqlHelper.Instance.GetById<Sys_VideoInfo>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Sys_VideoInfo"></param>
        /// <returns></returns>
        public List<Sys_VideoInfo> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Sys_VideoInfo>();
        }
    }
}
