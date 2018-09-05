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
    public partial class Sys_PlayVideoOper : SingleTon<Sys_PlayVideoOper>
    {
        #region API
        /// <summary>
        /// 
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public List<VideoPayView> GetPayViewByVideoId(int videoId)
        {
            return SqlHelper.Instance.GetByCondition<VideoPayView>($" id={videoId}  ");
        }

        public List<videoView> GetListByCoachId(int id, int index)
        {
            var size = 10;
            var condition = $" userId = {id}";
            return SqlHelper.Instance.GetViewPaging<videoView>(condition, index, size, " order by id ", new Dictionary<string, string>());
        }

        public List<videoView> GetListForApi(int index)
        {
            var size = 10;
            var condition = $" 1=1 and enabled=1 ";
            return SqlHelper.Instance.GetViewPaging<videoView>(condition, index, size, " order by id desc ", new Dictionary<string, string>());
        }

        #endregion

        public void DeleteVideoById(int id)
        {
            var str = $"delete from sys_playVideo where id={id}";
            SqlHelper.Instance.ExcuteNon(str);
        }
        
        public videoView GetViewById(int id)
        {
            return SqlHelper.Instance.GetById<videoView>(id);
        }
        
    }
}
