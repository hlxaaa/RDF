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
    public partial class GymOper : SingleTon<GymOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="gym"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Gym gym)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (gym.id != null)
                dict.Add("@id", gym.id.ToString());
            if (gym.name != null)
                dict.Add("@name", gym.name.ToString());
            if (gym.editTime != null)
                dict.Add("@editTime", gym.editTime.ToString());
            if (gym.nameE != null)
                dict.Add("@nameE", gym.nameE.ToString());
            if (gym.gymUserId != null)
                dict.Add("@gymUserId", gym.gymUserId.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="gym"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Gym gym)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (gym.name != null)
            {
                part1.Append("name,");
                part2.Append("@name,");
            }
            if (gym.editTime != null)
            {
                part1.Append("editTime,");
                part2.Append("@editTime,");
            }
            if (gym.nameE != null)
            {
                part1.Append("nameE,");
                part2.Append("@nameE,");
            }
            if (gym.gymUserId != null)
            {
                part1.Append("gymUserId,");
                part2.Append("@gymUserId,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into gym(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gym"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Gym gym)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update gym set ");
            if (gym.name != null)
                part1.Append("name = @name,");
            if (gym.editTime != null)
                part1.Append("editTime = @editTime,");
            if (gym.nameE != null)
                part1.Append("nameE = @nameE,");
            if (gym.gymUserId != null)
                part1.Append("gymUserId = @gymUserId,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where id= @id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Gym"></param>
        /// <returns></returns>
        public int Add(Gym model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Gym"></param>
        /// <returns></returns>
        public int Update(Gym model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Gym"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Gym where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="gym"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Gym gym,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (gym.id != null)
                dict.Add("@id" + i, gym.id.ToString());
            if (gym.name != null)
                dict.Add("@name" + i, gym.name.ToString());
            if (gym.editTime != null)
                dict.Add("@editTime" + i, gym.editTime.ToString());
            if (gym.nameE != null)
                dict.Add("@nameE" + i, gym.nameE.ToString());
            if (gym.gymUserId != null)
                dict.Add("@gymUserId" + i, gym.gymUserId.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gym"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Gym gym, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update gym set ");
            if (gym.name != null)
                part1.Append($"name = @name{i},");
            if (gym.editTime != null)
                part1.Append($"editTime = @editTime{i},");
            if (gym.nameE != null)
                part1.Append($"nameE = @nameE{i},");
            if (gym.gymUserId != null)
                part1.Append($"gymUserId = @gymUserId{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where id= @id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Gym"></param>
        /// <returns></returns>
        public void UpdateList(List<Gym> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="Gym"></param>
        /// <returns></returns>
        public Gym GetById(int id)
        {
            return SqlHelper.Instance.GetById<Gym>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Gym"></param>
        /// <returns></returns>
        public List<Gym> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Gym>();
        }
    }
}
