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
    public partial class PayRecordOper : SingleTon<PayRecordOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="payrecord"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(PayRecord payrecord)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (payrecord.id != null)
                dict.Add("@id", payrecord.id.ToString());
            if (payrecord.outTradeNo != null)
                dict.Add("@outTradeNo", payrecord.outTradeNo.ToString());
            if (payrecord.coachId != null)
                dict.Add("@coachId", payrecord.coachId.ToString());
            if (payrecord.userId != null)
                dict.Add("@userId", payrecord.userId.ToString());
            if (payrecord.type != null)
                dict.Add("@type", payrecord.type.ToString());
            if (payrecord.money != null)
                dict.Add("@money", payrecord.money.ToString());
            if (payrecord.status != null)
                dict.Add("@status", payrecord.status.ToString());
            if (payrecord.createTime != null)
                dict.Add("@createTime", payrecord.createTime.ToString());
            if (payrecord.videoId != null)
                dict.Add("@videoId", payrecord.videoId.ToString());
            if (payrecord.liveId != null)
                dict.Add("@liveId", payrecord.liveId.ToString());
            if (payrecord.payType != null)
                dict.Add("@payType", payrecord.payType.ToString());
            if (payrecord.account != null)
                dict.Add("@account", payrecord.account.ToString());
            if (payrecord.accountName != null)
                dict.Add("@accountName", payrecord.accountName.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="payrecord"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(PayRecord payrecord)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (payrecord.outTradeNo != null)
            {
                part1.Append("outTradeNo,");
                part2.Append("@outTradeNo,");
            }
            if (payrecord.coachId != null)
            {
                part1.Append("coachId,");
                part2.Append("@coachId,");
            }
            if (payrecord.userId != null)
            {
                part1.Append("userId,");
                part2.Append("@userId,");
            }
            if (payrecord.type != null)
            {
                part1.Append("type,");
                part2.Append("@type,");
            }
            if (payrecord.money != null)
            {
                part1.Append("money,");
                part2.Append("@money,");
            }
            if (payrecord.status != null)
            {
                part1.Append("status,");
                part2.Append("@status,");
            }
            if (payrecord.createTime != null)
            {
                part1.Append("createTime,");
                part2.Append("@createTime,");
            }
            if (payrecord.videoId != null)
            {
                part1.Append("videoId,");
                part2.Append("@videoId,");
            }
            if (payrecord.liveId != null)
            {
                part1.Append("liveId,");
                part2.Append("@liveId,");
            }
            if (payrecord.payType != null)
            {
                part1.Append("payType,");
                part2.Append("@payType,");
            }
            if (payrecord.account != null)
            {
                part1.Append("account,");
                part2.Append("@account,");
            }
            if (payrecord.accountName != null)
            {
                part1.Append("accountName,");
                part2.Append("@accountName,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into payrecord(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="payrecord"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(PayRecord payrecord)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update payrecord set ");
            if (payrecord.outTradeNo != null)
                part1.Append("outTradeNo = @outTradeNo,");
            if (payrecord.coachId != null)
                part1.Append("coachId = @coachId,");
            if (payrecord.userId != null)
                part1.Append("userId = @userId,");
            if (payrecord.type != null)
                part1.Append("type = @type,");
            if (payrecord.money != null)
                part1.Append("money = @money,");
            if (payrecord.status != null)
                part1.Append("status = @status,");
            if (payrecord.createTime != null)
                part1.Append("createTime = @createTime,");
            if (payrecord.videoId != null)
                part1.Append("videoId = @videoId,");
            if (payrecord.liveId != null)
                part1.Append("liveId = @liveId,");
            if (payrecord.payType != null)
                part1.Append("payType = @payType,");
            if (payrecord.account != null)
                part1.Append("account = @account,");
            if (payrecord.accountName != null)
                part1.Append("accountName = @accountName,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where id= @id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="PayRecord"></param>
        /// <returns></returns>
        public int Add(PayRecord model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="PayRecord"></param>
        /// <returns></returns>
        public int Update(PayRecord model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="PayRecord"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from PayRecord where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="payrecord"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(PayRecord payrecord,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (payrecord.id != null)
                dict.Add("@id" + i, payrecord.id.ToString());
            if (payrecord.outTradeNo != null)
                dict.Add("@outTradeNo" + i, payrecord.outTradeNo.ToString());
            if (payrecord.coachId != null)
                dict.Add("@coachId" + i, payrecord.coachId.ToString());
            if (payrecord.userId != null)
                dict.Add("@userId" + i, payrecord.userId.ToString());
            if (payrecord.type != null)
                dict.Add("@type" + i, payrecord.type.ToString());
            if (payrecord.money != null)
                dict.Add("@money" + i, payrecord.money.ToString());
            if (payrecord.status != null)
                dict.Add("@status" + i, payrecord.status.ToString());
            if (payrecord.createTime != null)
                dict.Add("@createTime" + i, payrecord.createTime.ToString());
            if (payrecord.videoId != null)
                dict.Add("@videoId" + i, payrecord.videoId.ToString());
            if (payrecord.liveId != null)
                dict.Add("@liveId" + i, payrecord.liveId.ToString());
            if (payrecord.payType != null)
                dict.Add("@payType" + i, payrecord.payType.ToString());
            if (payrecord.account != null)
                dict.Add("@account" + i, payrecord.account.ToString());
            if (payrecord.accountName != null)
                dict.Add("@accountName" + i, payrecord.accountName.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="payrecord"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(PayRecord payrecord, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update payrecord set ");
            if (payrecord.outTradeNo != null)
                part1.Append($"outTradeNo = @outTradeNo{i},");
            if (payrecord.coachId != null)
                part1.Append($"coachId = @coachId{i},");
            if (payrecord.userId != null)
                part1.Append($"userId = @userId{i},");
            if (payrecord.type != null)
                part1.Append($"type = @type{i},");
            if (payrecord.money != null)
                part1.Append($"money = @money{i},");
            if (payrecord.status != null)
                part1.Append($"status = @status{i},");
            if (payrecord.createTime != null)
                part1.Append($"createTime = @createTime{i},");
            if (payrecord.videoId != null)
                part1.Append($"videoId = @videoId{i},");
            if (payrecord.liveId != null)
                part1.Append($"liveId = @liveId{i},");
            if (payrecord.payType != null)
                part1.Append($"payType = @payType{i},");
            if (payrecord.account != null)
                part1.Append($"account = @account{i},");
            if (payrecord.accountName != null)
                part1.Append($"accountName = @accountName{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where id= @id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="PayRecord"></param>
        /// <returns></returns>
        public void UpdateList(List<PayRecord> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="PayRecord"></param>
        /// <returns></returns>
        public PayRecord GetById(int id)
        {
            return SqlHelper.Instance.GetById<PayRecord>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="PayRecord"></param>
        /// <returns></returns>
        public List<PayRecord> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<PayRecord>();
        }
    }
}
