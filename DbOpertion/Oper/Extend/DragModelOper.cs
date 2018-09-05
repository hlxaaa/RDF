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
    public partial class DragModelOper : SingleTon<DragModelOper>
    {
        public List<DragModel> GetExistList()
        {
            return SqlHelper.Instance.GetByCondition<DragModel>($" isdeleted=0");
        }
    }
}
