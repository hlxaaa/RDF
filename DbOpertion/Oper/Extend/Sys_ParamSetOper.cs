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
        public Sys_ParamSet GetByKey(string key)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("@key", key);
            var str = "select * from Sys_ParamSet where [key]=@key";
            var list = SqlHelper.Instance.ExecuteGetDt<Sys_ParamSet>(str, dict);
            if (list.Count == 0)
                return null;
            return list.First();
        }

        #region 主键乱来，所以要特地这样写

        public string GetInsertStr2(Sys_ParamSet sys_paramset)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();

            if (sys_paramset.key != null)
            {
                part1.Append("[key],");
                part2.Append("@key,");
            }
            if (sys_paramset.value != null)
            {
                part1.Append("[value],");
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
            sql.Append("insert into sys_paramset(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length - 1)).Append(")");
            return sql.ToString();
        }

        public void Add2(Sys_ParamSet model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr2(model) + " select @@identity";
            var dict = GetParameters(model);
            SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction);
        }

        public string GetUpdateStr2(Sys_ParamSet sys_paramset, string sourceKey)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_paramset set ");
            if (sys_paramset.key != null)
                part1.Append("[key] = @key,");
            if (sys_paramset.value != null)
                part1.Append("[value] = @value,");
            if (sys_paramset.remark != null)
                part1.Append("remark = @remark,");
            if (sys_paramset.uuid != null)
                part1.Append("uuid = @uuid,");
            if (sys_paramset.type != null)
                part1.Append("type = @type,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where [key]= '{sourceKey}'  ");
            return part1.ToString();
        }

        public int Update2(Sys_ParamSet model, string sourceKey, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr2(model, sourceKey);
            var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }

        public string GetUpdateStrItem2(Sys_ParamSet sys_paramset, int i, string sourceKey)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_paramset set ");
            if (sys_paramset.key != null)
                part1.Append($"[key] = @key{i},");
            if (sys_paramset.value != null)
                part1.Append($"[value] = @value{i},");
            if (sys_paramset.remark != null)
                part1.Append($"remark = @remark{i},");
            if (sys_paramset.uuid != null)
                part1.Append($"uuid = @uuid{i},");
            if (sys_paramset.type != null)
                part1.Append($"type = @type{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where [key]= '{sourceKey}'  ");
            return part1.ToString();
        }

        public void UpdateList2(List<Sys_ParamSet> list, List<string> sourceKeys, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = "";
            var dict = new Dictionary<string, string>();
            for (int i = 0; i < list.Count; i++)
            {
                var tempDict = GetParametersItem(list[i], i);
                foreach (var item in tempDict)
                {
                    dict.Add(item.Key, item.Value);
                }
                str += GetUpdateStrItem2(list[i], i, sourceKeys[i]);
            }
            SqlHelper.Instance.ExcuteNon(str, dict, connection, transaction);
        }
        #endregion
    }
}
