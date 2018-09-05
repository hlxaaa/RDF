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
    public partial class DeviceOper : SingleTon<DeviceOper>
    {

        public string GetGymNameByDeviceId(int id)
        {
            var str = $@"SELECT
	name
FROM
	Gym
WHERE
	id IN (
		SELECT
			gymId
		FROM
			Device
		WHERE
			id = {id}
	)";
            return SqlHelper.Instance.ExecuteScalar(str);
        }

        public List<Device> GetByGymId(int gid)
        {
            if (gid == 0)
                return SqlHelper.Instance.GetByCondition<Device>($" 1=1 ");
            return SqlHelper.Instance.GetByCondition<Device>($" gymId={gid} ");
        }

        public bool IsNameExist(string name, int id)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("@name", name);

            var list = SqlHelper.Instance.GetByCondition<Device>($" name=@name and id!={id} ", dict);
            if (list.Count == 0)
                return false;
            return true;
        }

        public Device GetByName(string name)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("@name", name);
            var list = SqlHelper.Instance.GetByCondition<Device>(" name=@name", dict);
            if (list.Count == 0)
                return null;
            return list.First();
        }

    }
}
