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
    public partial class AndroidVersionOper : SingleTon<AndroidVersionOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="androidversion"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(AndroidVersion androidversion)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (androidversion.id != null)
                dict.Add("@id", androidversion.id.ToString());
            if (androidversion.versionCode != null)
                dict.Add("@versionCode", androidversion.versionCode.ToString());
            if (androidversion.versionName != null)
                dict.Add("@versionName", androidversion.versionName.ToString());
            if (androidversion.apkFileUrl != null)
                dict.Add("@apkFileUrl", androidversion.apkFileUrl.ToString());
            if (androidversion.updateLog != null)
                dict.Add("@updateLog", androidversion.updateLog.ToString());
            if (androidversion.targetSize != null)
                dict.Add("@targetSize", androidversion.targetSize.ToString());
            if (androidversion.editTime != null)
                dict.Add("@editTime", androidversion.editTime.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="androidversion"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(AndroidVersion androidversion)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (androidversion.versionCode != null)
            {
                part1.Append("versionCode,");
                part2.Append("@versionCode,");
            }
            if (androidversion.versionName != null)
            {
                part1.Append("versionName,");
                part2.Append("@versionName,");
            }
            if (androidversion.apkFileUrl != null)
            {
                part1.Append("apkFileUrl,");
                part2.Append("@apkFileUrl,");
            }
            if (androidversion.updateLog != null)
            {
                part1.Append("updateLog,");
                part2.Append("@updateLog,");
            }
            if (androidversion.targetSize != null)
            {
                part1.Append("targetSize,");
                part2.Append("@targetSize,");
            }
            if (androidversion.editTime != null)
            {
                part1.Append("editTime,");
                part2.Append("@editTime,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into androidversion(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="androidversion"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(AndroidVersion androidversion)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update androidversion set ");
            if (androidversion.versionCode != null)
                part1.Append("versionCode = @versionCode,");
            if (androidversion.versionName != null)
                part1.Append("versionName = @versionName,");
            if (androidversion.apkFileUrl != null)
                part1.Append("apkFileUrl = @apkFileUrl,");
            if (androidversion.updateLog != null)
                part1.Append("updateLog = @updateLog,");
            if (androidversion.targetSize != null)
                part1.Append("targetSize = @targetSize,");
            if (androidversion.editTime != null)
                part1.Append("editTime = @editTime,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where id= @id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="AndroidVersion"></param>
        /// <returns></returns>
        public int Add(AndroidVersion model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="AndroidVersion"></param>
        /// <returns></returns>
        public int Update(AndroidVersion model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="AndroidVersion"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from AndroidVersion where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="androidversion"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(AndroidVersion androidversion,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (androidversion.id != null)
                dict.Add("@id" + i, androidversion.id.ToString());
            if (androidversion.versionCode != null)
                dict.Add("@versionCode" + i, androidversion.versionCode.ToString());
            if (androidversion.versionName != null)
                dict.Add("@versionName" + i, androidversion.versionName.ToString());
            if (androidversion.apkFileUrl != null)
                dict.Add("@apkFileUrl" + i, androidversion.apkFileUrl.ToString());
            if (androidversion.updateLog != null)
                dict.Add("@updateLog" + i, androidversion.updateLog.ToString());
            if (androidversion.targetSize != null)
                dict.Add("@targetSize" + i, androidversion.targetSize.ToString());
            if (androidversion.editTime != null)
                dict.Add("@editTime" + i, androidversion.editTime.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="androidversion"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(AndroidVersion androidversion, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update androidversion set ");
            if (androidversion.versionCode != null)
                part1.Append($"versionCode = @versionCode{i},");
            if (androidversion.versionName != null)
                part1.Append($"versionName = @versionName{i},");
            if (androidversion.apkFileUrl != null)
                part1.Append($"apkFileUrl = @apkFileUrl{i},");
            if (androidversion.updateLog != null)
                part1.Append($"updateLog = @updateLog{i},");
            if (androidversion.targetSize != null)
                part1.Append($"targetSize = @targetSize{i},");
            if (androidversion.editTime != null)
                part1.Append($"editTime = @editTime{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where id= @id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="AndroidVersion"></param>
        /// <returns></returns>
        public void UpdateList(List<AndroidVersion> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="AndroidVersion"></param>
        /// <returns></returns>
        public AndroidVersion GetById(int id)
        {
            return SqlHelper.Instance.GetById<AndroidVersion>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="AndroidVersion"></param>
        /// <returns></returns>
        public List<AndroidVersion> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<AndroidVersion>();
        }
    }
}
