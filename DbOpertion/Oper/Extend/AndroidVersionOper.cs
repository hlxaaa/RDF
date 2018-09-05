using System.Text;
using Common.Helper;
using System;
using System.Collections.Generic;
using Common;
using System.Linq;
using DbOpertion.Models;
using System.Data.SqlClient;
using static DbOpertion.Models.Extend.AllModel;
using Common.Extend;

namespace DbOpertion.DBoperation
{
    public partial class AndroidVersionOper : SingleTon<AndroidVersionOper>
    {
        public AndroidVersion GetLastVersion()
        {
            var str = $"select top 1 * from AndroidVersion order by id desc";
            var list = SqlHelper.Instance.ExecuteGetDt<AndroidVersion>(str, new Dictionary<string, string>());
            if (list.Count == 0)
                return null;
            return list.First();
        }
    }
}
