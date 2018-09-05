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
    public partial class Sys_MenuOper : SingleTon<Sys_MenuOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_menu"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(Sys_Menu sys_menu)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_menu.Id != null)
                dict.Add("@Id", sys_menu.Id.ToString());
            if (sys_menu.MenuCode != null)
                dict.Add("@MenuCode", sys_menu.MenuCode.ToString());
            if (sys_menu.MenuName != null)
                dict.Add("@MenuName", sys_menu.MenuName.ToString());
            if (sys_menu.MenuUrl != null)
                dict.Add("@MenuUrl", sys_menu.MenuUrl.ToString());
            if (sys_menu.Sort != null)
                dict.Add("@Sort", sys_menu.Sort.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sys_menu"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(Sys_Menu sys_menu)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (sys_menu.Id != null)
            {
                part1.Append("Id,");
                part2.Append("@Id,");
            }
            if (sys_menu.MenuName != null)
            {
                part1.Append("MenuName,");
                part2.Append("@MenuName,");
            }
            if (sys_menu.MenuUrl != null)
            {
                part1.Append("MenuUrl,");
                part2.Append("@MenuUrl,");
            }
            if (sys_menu.Sort != null)
            {
                part1.Append("Sort,");
                part2.Append("@Sort,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into sys_menu(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_menu"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(Sys_Menu sys_menu)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_menu set ");
            if (sys_menu.Id != null)
                part1.Append("Id = @Id,");
            if (sys_menu.MenuName != null)
                part1.Append("MenuName = @MenuName,");
            if (sys_menu.MenuUrl != null)
                part1.Append("MenuUrl = @MenuUrl,");
            if (sys_menu.Sort != null)
                part1.Append("Sort = @Sort,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where MenuCode= @MenuCode  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="Sys_Menu"></param>
        /// <returns></returns>
        public int Add(Sys_Menu model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_Menu"></param>
        /// <returns></returns>
        public int Update(Sys_Menu model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="Sys_Menu"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from Sys_Menu where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sys_menu"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(Sys_Menu sys_menu,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (sys_menu.Id != null)
                dict.Add("@Id" + i, sys_menu.Id.ToString());
            if (sys_menu.MenuCode != null)
                dict.Add("@MenuCode" + i, sys_menu.MenuCode.ToString());
            if (sys_menu.MenuName != null)
                dict.Add("@MenuName" + i, sys_menu.MenuName.ToString());
            if (sys_menu.MenuUrl != null)
                dict.Add("@MenuUrl" + i, sys_menu.MenuUrl.ToString());
            if (sys_menu.Sort != null)
                dict.Add("@Sort" + i, sys_menu.Sort.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sys_menu"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(Sys_Menu sys_menu, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update sys_menu set ");
            if (sys_menu.Id != null)
                part1.Append($"Id = @Id{i},");
            if (sys_menu.MenuName != null)
                part1.Append($"MenuName = @MenuName{i},");
            if (sys_menu.MenuUrl != null)
                part1.Append($"MenuUrl = @MenuUrl{i},");
            if (sys_menu.Sort != null)
                part1.Append($"Sort = @Sort{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where MenuCode= @MenuCode{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="Sys_Menu"></param>
        /// <returns></returns>
        public void UpdateList(List<Sys_Menu> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="Sys_Menu"></param>
        /// <returns></returns>
        public Sys_Menu GetById(int id)
        {
            return SqlHelper.Instance.GetById<Sys_Menu>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="Sys_Menu"></param>
        /// <returns></returns>
        public List<Sys_Menu> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<Sys_Menu>();
        }
    }
}
