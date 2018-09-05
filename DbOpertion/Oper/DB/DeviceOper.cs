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
    public partial class DeviceOper : SingleTon<DeviceOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Device device)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (device.id != null)
                dict.Add("@id", device.id.ToString());
            if (device.name != null)
                dict.Add("@name", device.name.ToString());
            if (device.editTime != null)
                dict.Add("@editTime", device.editTime.ToString());
            if (device.gymId != null)
                dict.Add("@gymId", device.gymId.ToString());
            if (device.userId != null)
                dict.Add("@userId", device.userId.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="device"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Device device)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (device.name != null)
            {
                part1.Append("name,");
                part2.Append("@name,");
            }
            if (device.editTime != null)
            {
                part1.Append("editTime,");
                part2.Append("@editTime,");
            }
            if (device.gymId != null)
            {
                part1.Append("gymId,");
                part2.Append("@gymId,");
            }
            if (device.userId != null)
            {
                part1.Append("userId,");
                part2.Append("@userId,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into device(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="device"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Device device)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update device set ");
            if (device.name != null)
                part1.Append("name = @name,");
            if (device.editTime != null)
                part1.Append("editTime = @editTime,");
            if (device.gymId != null)
                part1.Append("gymId = @gymId,");
            if (device.userId != null)
                part1.Append("userId = @userId,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where id= @id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Device"></param>
        /// <returns></returns>
        public int Add(Device model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Device"></param>
        /// <returns></returns>
        public int Update(Device model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Device"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Device where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Device device,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (device.id != null)
                dict.Add("@id" + i, device.id.ToString());
            if (device.name != null)
                dict.Add("@name" + i, device.name.ToString());
            if (device.editTime != null)
                dict.Add("@editTime" + i, device.editTime.ToString());
            if (device.gymId != null)
                dict.Add("@gymId" + i, device.gymId.ToString());
            if (device.userId != null)
                dict.Add("@userId" + i, device.userId.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="device"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Device device, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update device set ");
            if (device.name != null)
                part1.Append($"name = @name{i},");
            if (device.editTime != null)
                part1.Append($"editTime = @editTime{i},");
            if (device.gymId != null)
                part1.Append($"gymId = @gymId{i},");
            if (device.userId != null)
                part1.Append($"userId = @userId{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where id= @id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Device"></param>
        /// <returns></returns>
        public void UpdateList(List<Device> list, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = "";
            var dict = new Dictionary<string,string>();
            for(int i=0;i<list.Count;i++)
            {
            var tempDict=GetParametersItem(list[i],i);
            foreach(var item in tempDict)
            {
            dict.Add(item.Key,item.Value);
            }
            str+=GetUpdateStrItem(list[i],i);
            }
            SqlHelper.Instance.ExcuteNon(str, dict, connection, transaction);
        }
        /// <summary>
        /// getById
        /// </summary>
        /// <param name="Device"></param>
        /// <returns></returns>
        public Device GetById(int id)
        {
            return SqlHelper.Instance.GetById<Device>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Device"></param>
        /// <returns></returns>
        public List<Device> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Device>();
        }
    }
}
