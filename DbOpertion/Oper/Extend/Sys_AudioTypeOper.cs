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
    public partial class Sys_AudioTypeOper : SingleTon<Sys_AudioTypeOper>
    {
        public List<Sys_AudioType> GetEnabledList()
        {
            return SqlHelper.Instance.GetByCondition<Sys_AudioType>(" enabled=1 ");
        }
    }
}
