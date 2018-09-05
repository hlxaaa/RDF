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
    public partial class Sys_ParamSetOper : SingleTon<Sys_ParamSetOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_paramset"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Sys_ParamSet sys_paramset)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_paramset.key != null)
                dict.Add("@key", sys_paramset.key.ToString());
            if (sys_paramset.value != null)
                dict.Add("@value", sys_paramset.value.ToString());
            if (sys_paramset.remark != null)
                dict.Add("@remark", sys_paramset.remark.ToString());
            if (sys_paramset.uuid != null)
                dict.Add("@uuid", sys_paramset.uuid.ToString());
            if (sys_paramset.type != null)
                dict.Add("@type", sys_paramset.type.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sys_paramset"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Sys_ParamSet sys_paramset)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (sys_paramset.value != null)
            {
                part1.Append("value,");
                part2.Append("@value,");
            }
            if (sys_paramset.remark != null)
            {
                part1.Append("remark,");
                part2.Append("@remark,");
            }
            if (sys_paramset.uuid != null)
            {
                part1.Append("uuid,");
                part2.Append("@uuid,");
            }
            if (sys_paramset.type != null)
            {
                part1.Append("type,");
                part2.Append("@type,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into sys_paramset(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_paramset"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Sys_ParamSet sys_paramset)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_paramset set ");
            if (sys_paramset.value != null)
                part1.Append("value = @value,");
            if (sys_paramset.remark != null)
                part1.Append("remark = @remark,");
            if (sys_paramset.uuid != null)
                part1.Append("uuid = @uuid,");
            if (sys_paramset.type != null)
                part1.Append("type = @type,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where key= @key  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Sys_ParamSet"></param>
        /// <returns></returns>
        public int Add(Sys_ParamSet model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_ParamSet"></param>
        /// <returns></returns>
        public int Update(Sys_ParamSet model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Sys_ParamSet"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Sys_ParamSet where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_paramset"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Sys_ParamSet sys_paramset,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_paramset.key != null)
                dict.Add("@key" + i, sys_paramset.key.ToString());
            if (sys_paramset.value != null)
                dict.Add("@value" + i, sys_paramset.value.ToString());
            if (sys_paramset.remark != null)
                dict.Add("@remark" + i, sys_paramset.remark.ToString());
            if (sys_paramset.uuid != null)
                dict.Add("@uuid" + i, sys_paramset.uuid.ToString());
            if (sys_paramset.type != null)
                dict.Add("@type" + i, sys_paramset.type.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_paramset"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Sys_ParamSet sys_paramset, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_paramset set ");
            if (sys_paramset.value != null)
                part1.Append($"value = @value{i},");
            if (sys_paramset.remark != null)
                part1.Append($"remark = @remark{i},");
            if (sys_paramset.uuid != null)
                part1.Append($"uuid = @uuid{i},");
            if (sys_paramset.type != null)
                part1.Append($"type = @type{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where key= @key{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_ParamSet"></param>
        /// <returns></returns>
        public void UpdateList(List<Sys_ParamSet> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="Sys_ParamSet"></param>
        /// <returns></returns>
        public Sys_ParamSet GetById(int id)
        {
            return SqlHelper.Instance.GetById<Sys_ParamSet>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Sys_ParamSet"></param>
        /// <returns></returns>
        public List<Sys_ParamSet> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Sys_ParamSet>();
        }
    }
}
