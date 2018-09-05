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
    public partial class GymOper : SingleTon<GymOper>
    {
        public Gym GetByUserId(int userid)
        {
            var list = SqlHelper.Instance.GetByCondition<Gym>($" gymUserId={userid} ");
            if (list.Count == 0)
                return null;
            return list.First();
        }

        public GymView GetViewById(int id)
        {
            return SqlHelper.Instance.GetById<GymView>(id);
        }

        public int GetAllGymCount()
        {
            var str = "select count(*) from gym";
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }

    }
}
