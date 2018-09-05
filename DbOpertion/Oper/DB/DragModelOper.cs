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
    public partial class DragModelOper : SingleTon<DragModelOper>
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="dragmodel"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParameters(DragModel dragmodel)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (dragmodel.id != null)
                dict.Add("@id", dragmodel.id.ToString());
            if (dragmodel.modelType != null)
                dict.Add("@modelType", dragmodel.modelType.ToString());
            if (dragmodel.modelName != null)
                dict.Add("@modelName", dragmodel.modelName.ToString());
            if (dragmodel.createTime != null)
                dict.Add("@createTime", dragmodel.createTime.ToString());
            if (dragmodel.isDeleted != null)
                dict.Add("@isDeleted", dragmodel.isDeleted.ToString());
            if (dragmodel.content != null)
                dict.Add("@content", dragmodel.content.ToString());
            if (dragmodel.isEnglish != null)
                dict.Add("@isEnglish", dragmodel.isEnglish.ToString());
            if (dragmodel.modelNameE != null)
                dict.Add("@modelNameE", dragmodel.modelNameE.ToString());

            return dict;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="dragmodel"></param>
        /// <returns>是否成功</returns>
        public string GetInsertStr(DragModel dragmodel)
        {
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            
            if (dragmodel.modelType != null)
            {
                part1.Append("modelType,");
                part2.Append("@modelType,");
            }
            if (dragmodel.modelName != null)
            {
                part1.Append("modelName,");
                part2.Append("@modelName,");
            }
            if (dragmodel.createTime != null)
            {
                part1.Append("createTime,");
                part2.Append("@createTime,");
            }
            if (dragmodel.isDeleted != null)
            {
                part1.Append("isDeleted,");
                part2.Append("@isDeleted,");
            }
            if (dragmodel.content != null)
            {
                part1.Append("content,");
                part2.Append("@content,");
            }
            if (dragmodel.isEnglish != null)
            {
                part1.Append("isEnglish,");
                part2.Append("@isEnglish,");
            }
            if (dragmodel.modelNameE != null)
            {
                part1.Append("modelNameE,");
                part2.Append("@modelNameE,");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into dragmodel(").Append(part1.ToString().Remove(part1.Length - 1)).Append(") values (").Append(part2.ToString().Remove(part2.Length-1)).Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dragmodel"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStr(DragModel dragmodel)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update dragmodel set ");
            if (dragmodel.modelType != null)
                part1.Append("modelType = @modelType,");
            if (dragmodel.modelName != null)
                part1.Append("modelName = @modelName,");
            if (dragmodel.createTime != null)
                part1.Append("createTime = @createTime,");
            if (dragmodel.isDeleted != null)
                part1.Append("isDeleted = @isDeleted,");
            if (dragmodel.content != null)
                part1.Append("content = @content,");
            if (dragmodel.isEnglish != null)
                part1.Append("isEnglish = @isEnglish,");
            if (dragmodel.modelNameE != null)
                part1.Append("modelNameE = @modelNameE,");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append(" where id= @id  ");
            return part1.ToString();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="DragModel"></param>
        /// <returns></returns>
        public int Add(DragModel model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetInsertStr(model)+" select @@identity";
              var dict = GetParameters(model);
            return Convert.ToInt32(SqlHelper.Instance.ExecuteScalar(str, dict, connection, transaction));
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="DragModel"></param>
        /// <returns></returns>
        public int Update(DragModel model, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var str = GetUpdateStr(model);
              var dict = GetParameters(model);
            return SqlHelper.Instance.ExcuteNonQuery(str, dict, connection, transaction);
        }
        /// <summary>
        /// del
        /// </summary>
        /// <param name="DragModel"></param>
        /// <returns></returns>
        public int Delete1(int id)
        {
            var str = "delete from DragModel where id = " + id;
            var r = SqlHelper.Instance.ExecuteScalar(str);
            return Convert.ToInt32(r);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="dragmodel"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetParametersItem(DragModel dragmodel,int i)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (dragmodel.id != null)
                dict.Add("@id" + i, dragmodel.id.ToString());
            if (dragmodel.modelType != null)
                dict.Add("@modelType" + i, dragmodel.modelType.ToString());
            if (dragmodel.modelName != null)
                dict.Add("@modelName" + i, dragmodel.modelName.ToString());
            if (dragmodel.createTime != null)
                dict.Add("@createTime" + i, dragmodel.createTime.ToString());
            if (dragmodel.isDeleted != null)
                dict.Add("@isDeleted" + i, dragmodel.isDeleted.ToString());
            if (dragmodel.content != null)
                dict.Add("@content" + i, dragmodel.content.ToString());
            if (dragmodel.isEnglish != null)
                dict.Add("@isEnglish" + i, dragmodel.isEnglish.ToString());
            if (dragmodel.modelNameE != null)
                dict.Add("@modelNameE" + i, dragmodel.modelNameE.ToString());

            return dict;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dragmodel"></param>
        /// <returns>是否成功</returns>
        public string GetUpdateStrItem(DragModel dragmodel, int i)
        {
            StringBuilder part1 = new StringBuilder();
            part1.Append("update dragmodel set ");
            if (dragmodel.modelType != null)
                part1.Append($"modelType = @modelType{i},");
            if (dragmodel.modelName != null)
                part1.Append($"modelName = @modelName{i},");
            if (dragmodel.createTime != null)
                part1.Append($"createTime = @createTime{i},");
            if (dragmodel.isDeleted != null)
                part1.Append($"isDeleted = @isDeleted{i},");
            if (dragmodel.content != null)
                part1.Append($"content = @content{i},");
            if (dragmodel.isEnglish != null)
                part1.Append($"isEnglish = @isEnglish{i},");
            if (dragmodel.modelNameE != null)
                part1.Append($"modelNameE = @modelNameE{i},");
            int n = part1.ToString().LastIndexOf(",");
            part1.Remove(n, 1);
            part1.Append($" where id= @id{i}  ");
            return part1.ToString();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="DragModel"></param>
        /// <returns></returns>
        public void UpdateList(List<DragModel> list, SqlConnection connection = null, SqlTransaction transaction = null)
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
        /// <param name="DragModel"></param>
        /// <returns></returns>
        public DragModel GetById(int id)
        {
            return SqlHelper.Instance.GetById<DragModel>(id);
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="DragModel"></param>
        /// <returns></returns>
        public List<DragModel> GetAllList()
        {
            return SqlHelper.Instance.GetListFromDb<DragModel>();
        }
    }
}
