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
    public partial class Sys_UserAuthOper : SingleTon<Sys_UserAuthOper>
    {
        public void DeleteByUserId(int id, SqlConnection conn, SqlTransaction tran)
        {
            var str = "delete from Sys_UserAuth where userid=" + id;
            SqlHelper.Instance.ExcuteNon(str, new Dictionary<string, string>(), conn, tran);
        }

        #region 实际是AuthOper的
        public List<authView> GetAuthByUserId(int id)
        {
            return SqlHelper.Instance.GetByCondition<authView>($" id={id}");
        }

        public void Add2(Sys_UserAuth model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model) + " select @@identity";
            var dict = GetParameters(model);
            SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction);
        }
        #endregion
    }
}
