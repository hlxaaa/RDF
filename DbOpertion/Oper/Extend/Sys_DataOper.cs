using System.Text;
using Common.Helper;
using System;
using System.Collections.Generic;
using Common;
using System.Linq;
using DbOpertion.Models;
using System.Data.SqlClient;
using DbOpertion.Models.Extend;

namespace DbOpertion.DBoperation
{
    public partial class Sys_DataOper : SingleTon<Sys_DataOper>
    {
        public List<Sys_Data> GetDataForLine(DateTime date, int userId)
        {
            return SqlHelper.Instance.GetByCondition<Sys_Data>($" userid={userId} and createTime>'{date}' and createTime<'{date.AddDays(1)}' order by createTime ");
        }

        public List<GetRankList> GetRankList(DateTime dt)
        {
            var str = $@"SELECT
	ROW_NUMBER () OVER (ORDER BY r2.TotalKM DESC) [order],
	r2.*
FROM
	(
		SELECT
			r.UserId,
			SUM (r.kal) AS TotalKAL,
			SUM (r.km) AS TotalKM,
			r.UserName,
			r.Url
		FROM
			(
				SELECT
					d.*, u.UserName,
					u.Url
				FROM
					Sys_Data d
				LEFT JOIN Sys_User u ON u.id = d.UserId
				
			) r
		WHERE
			r.CreateTime > '{dt}'
		AND r.CreateTime < '{dt.AddDays(1)}'
		GROUP BY
			r.UserId,
			r.UserName,
			r.Url
	) r2";

            return SqlHelper.Instance.ExecuteGetDt<GetRankList>(str, new Dictionary<string, string>());
        }

        public void Add2(Sys_Data model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model) + " select @@identity";
            var dict = GetParameters(model);
            SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction);
        }

        public Sys_Data GetLastDataByUserId(int userId)
        {
            var str = $"select top 1 * from sys_data where userId ={userId} order by createTime desc";
            var list = SqlHelper.Instance.ExecuteGetDt<Sys_Data>(str, new Dictionary<string, string>());
            if (list.Count == 0)
                return new Sys_Data
                {
                    TotalKAL = 0,
                    TotalKM = 0,
                    TotalTime = 0
                };
            var r = list.First();
            r.TotalKAL = r.TotalKAL ?? 0;
            r.TotalKM = r.TotalKM ?? 0;
            r.TotalTime = r.TotalTime ?? 0;
            return list.First();
        }
    }
}
