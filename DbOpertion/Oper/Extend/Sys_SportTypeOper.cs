using System.Text;
using Common.Helper;
using System;
using System.Collections.Generic;
using Common;
using System.Linq;
using DbOpertion.Models;
using System.Data.SqlClient;
using Common.Extend;
using static DbOpertion.Models.Extend.AllModel;

namespace DbOpertion.DBoperation
{
    public partial class Sys_SportTypeOper : SingleTon<Sys_SportTypeOper>
    {
        public List<Sys_SportType> GetEnabledList()
        {
            return SqlHelper.Instance.GetByCondition<Sys_SportType>(" enabled=1 ");
        }
    }
}
