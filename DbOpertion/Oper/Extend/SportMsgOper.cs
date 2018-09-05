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
    public partial class SportMsgOper : SingleTon<SportMsgOper>
    {
        public List<SportMsgView> GetSportMsgBySportId(int sportId, int index)
        {
            var size = 10;
            var condition = $" sportId = {sportId}";
            return SqlHelper.Instance.GetViewPaging<SportMsgView>(condition, index, size, " order by id desc", new Dictionary<string, string>());
        }
    }
}
