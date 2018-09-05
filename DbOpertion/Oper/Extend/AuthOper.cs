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
    public partial class AuthOper : SingleTon<AuthOper>
    {
        public void DeleteByUserId(int id, SqlConnection conn, SqlTransaction tran)
        {
            var str = "delete from Auth where userid=" + id;
            SqlHelper.Instance.ExcuteNon(str, new Dictionary<string, string>(), conn, tran);
        }
    }
}
