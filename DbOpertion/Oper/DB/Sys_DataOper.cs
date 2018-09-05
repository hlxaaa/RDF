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
    public partial class Sys_DataOper : SingleTon<Sys_DataOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_data"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Sys_Data sys_data)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_data.UserId != null)
                dict.Add("@UserId", sys_data.UserId.ToString());
            if (sys_data.CreateTime != null)
                dict.Add("@CreateTime", sys_data.CreateTime.ToString());
            if (sys_data.XL != null)
                dict.Add("@XL", sys_data.XL.ToString());
            if (sys_data.SD != null)
                dict.Add("@SD", sys_data.SD.ToString());
            if (sys_data.KAL != null)
                dict.Add("@KAL", sys_data.KAL.ToString());
            if (sys_data.KM != null)
                dict.Add("@KM", sys_data.KM.ToString());
            if (sys_data.TotalKM != null)
                dict.Add("@TotalKM", sys_data.TotalKM.ToString());
            if (sys_data.ZS != null)
                dict.Add("@ZS", sys_data.ZS.ToString());
            if (sys_data.Time != null)
                dict.Add("@Time", sys_data.Time.ToString());
            if (sys_data.TotalTime != null)
                dict.Add("@TotalTime", sys_data.TotalTime.ToString());
            if (sys_data.TotalKAL != null)
                dict.Add("@TotalKAL", sys_data.TotalKAL.ToString());
            if (sys_data.WATT != null)
                dict.Add("@WATT", sys_data.WATT.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sys_data"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Sys_Data sys_data)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (sys_data.UserId != null)
            {
                part1.Append("UserId,");
                part2.Append("@UserId,");
            }
            if (sys_data.CreateTime != null)
            {
                part1.Append("CreateTime,");
                part2.Append("@CreateTime,");
            }
            if (sys_data.XL != null)
            {
                part1.Append("XL,");
                part2.Append("@XL,");
            }
            if (sys_data.SD != null)
            {
                part1.Append("SD,");
                part2.Append("@SD,");
            }
            if (sys_data.KAL != null)
            {
                part1.Append("KAL,");
                part2.Append("@KAL,");
            }
            if (sys_data.KM != null)
            {
                part1.Append("KM,");
                part2.Append("@KM,");
            }
            if (sys_data.TotalKM != null)
            {
                part1.Append("TotalKM,");
                part2.Append("@TotalKM,");
            }
            if (sys_data.ZS != null)
            {
                part1.Append("ZS,");
                part2.Append("@ZS,");
            }
            if (sys_data.Time != null)
            {
                part1.Append("Time,");
                part2.Append("@Time,");
            }
            if (sys_data.TotalTime != null)
            {
                part1.Append("TotalTime,");
                part2.Append("@TotalTime,");
            }
            if (sys_data.TotalKAL != null)
            {
                part1.Append("TotalKAL,");
                part2.Append("@TotalKAL,");
            }
            if (sys_data.WATT != null)
            {
                part1.Append("WATT,");
                part2.Append("@WATT,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into sys_data(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_data"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Sys_Data sys_data)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_data set ");
            if (sys_data.UserId != null)
                part1.Append("UserId = @UserId,");
            if (sys_data.CreateTime != null)
                part1.Append("CreateTime = @CreateTime,");
            if (sys_data.XL != null)
                part1.Append("XL = @XL,");
            if (sys_data.SD != null)
                part1.Append("SD = @SD,");
            if (sys_data.KAL != null)
                part1.Append("KAL = @KAL,");
            if (sys_data.KM != null)
                part1.Append("KM = @KM,");
            if (sys_data.TotalKM != null)
                part1.Append("TotalKM = @TotalKM,");
            if (sys_data.ZS != null)
                part1.Append("ZS = @ZS,");
            if (sys_data.Time != null)
                part1.Append("Time = @Time,");
            if (sys_data.TotalTime != null)
                part1.Append("TotalTime = @TotalTime,");
            if (sys_data.TotalKAL != null)
                part1.Append("TotalKAL = @TotalKAL,");
            if (sys_data.WATT != null)
                part1.Append("WATT = @WATT,");

            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Sys_Data"></param>
        /// <returns></returns>
        public int Add(Sys_Data model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_Data"></param>
        /// <returns></returns>
        public int Update(Sys_Data model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Sys_Data"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Sys_Data where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_data"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Sys_Data sys_data,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_data.UserId != null)
                dict.Add("@UserId" + i, sys_data.UserId.ToString());
            if (sys_data.CreateTime != null)
                dict.Add("@CreateTime" + i, sys_data.CreateTime.ToString());
            if (sys_data.XL != null)
                dict.Add("@XL" + i, sys_data.XL.ToString());
            if (sys_data.SD != null)
                dict.Add("@SD" + i, sys_data.SD.ToString());
            if (sys_data.KAL != null)
                dict.Add("@KAL" + i, sys_data.KAL.ToString());
            if (sys_data.KM != null)
                dict.Add("@KM" + i, sys_data.KM.ToString());
            if (sys_data.TotalKM != null)
                dict.Add("@TotalKM" + i, sys_data.TotalKM.ToString());
            if (sys_data.ZS != null)
                dict.Add("@ZS" + i, sys_data.ZS.ToString());
            if (sys_data.Time != null)
                dict.Add("@Time" + i, sys_data.Time.ToString());
            if (sys_data.TotalTime != null)
                dict.Add("@TotalTime" + i, sys_data.TotalTime.ToString());
            if (sys_data.TotalKAL != null)
                dict.Add("@TotalKAL" + i, sys_data.TotalKAL.ToString());
            if (sys_data.WATT != null)
                dict.Add("@WATT" + i, sys_data.WATT.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_data"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Sys_Data sys_data, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_data set ");
            if (sys_data.UserId != null)
                part1.Append($"UserId = @UserId{i},");
            if (sys_data.CreateTime != null)
                part1.Append($"CreateTime = @CreateTime{i},");
            if (sys_data.XL != null)
                part1.Append($"XL = @XL{i},");
            if (sys_data.SD != null)
                part1.Append($"SD = @SD{i},");
            if (sys_data.KAL != null)
                part1.Append($"KAL = @KAL{i},");
            if (sys_data.KM != null)
                part1.Append($"KM = @KM{i},");
            if (sys_data.TotalKM != null)
                part1.Append($"TotalKM = @TotalKM{i},");
            if (sys_data.ZS != null)
                part1.Append($"ZS = @ZS{i},");
            if (sys_data.Time != null)
                part1.Append($"Time = @Time{i},");
            if (sys_data.TotalTime != null)
                part1.Append($"TotalTime = @TotalTime{i},");
            if (sys_data.TotalKAL != null)
                part1.Append($"TotalKAL = @TotalKAL{i},");
            if (sys_data.WATT != null)
                part1.Append($"WATT = @WATT{i},");

            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_Data"></param>
        /// <returns></returns>
        public void UpdateList(List<Sys_Data> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="Sys_Data"></param>
        /// <returns></returns>
        public Sys_Data GetById(int id)
        {
            return SqlHelper.Instance.GetById<Sys_Data>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Sys_Data"></param>
        /// <returns></returns>
        public List<Sys_Data> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Sys_Data>();
        }
    }
}
