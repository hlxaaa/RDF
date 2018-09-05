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
    public partial class Sys_VideoInfoOper : SingleTon<Sys_VideoInfoOper>
    {

        public void RemoveVideoIdAfterDelVideo(int id)
        {
            var str = $"select * from Sys_VideoInfo where videoId={id}";
            var list = SqlHelper.Instance.ExecuteGetDt<Sys_VideoInfo>(str, new Dictionary<string, string>());
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].VideoId = 0;
                }
                UpdateList(list);
            }
        }

        #region API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="liveId"></param>
        /// <returns></returns>
        public List<LivePayView> GetPayViewByVideoId(int liveId)
        {
            return SqlHelper.Instance.GetByCondition<LivePayView>($" id={liveId}  ");
        }

        public List<Sys_VideoInfo> GetListByCoachId(int id, int index)
        {
            var size = 10;
            var condition = $" userId = {id}";
            return SqlHelper.Instance.GetViewPaging<Sys_VideoInfo>(condition, index, size, " order by id ", new Dictionary<string, string>());
        }
        #endregion

        /// <summary>
        /// 检查0,1,2超时的，未过审变失效，过审变已结束
        /// </summary>
        public void UpdateLivePlayStatus()
        {
            var list = SqlHelper.Instance.GetByCondition<Sys_VideoInfo>(" playStatus in (0,1,2) ");
            if (list.Count > 0)
            {
                var upList = new List<Sys_VideoInfo>();
                foreach (var item in list)
                {
                    var overTime = Convert.ToDateTime(item.BeginTime).AddMinutes((int)item.PlayLongTime);
                    if (DateTime.Now > overTime)
                    {
                        if (item.DataStatus == 1)
                            item.PlayStatus = 3;
                        else
                            item.PlayStatus = 4;
                        upList.Add(item);
                    }
                }
                if (upList.Count > 0)
                    UpdateList(upList);
            }
        }
        
        /// <summary>
        /// 主要是时间超时变失效之类的
        /// </summary>
        /// <param name="live"></param>
        public List<Sys_VideoInfo> UpdateLivePlayStatus(List<Sys_VideoInfo> lives)
        {
            var list = new List<Sys_VideoInfo>();
            var r = new List<Sys_VideoInfo>();
            foreach (var item in lives)
            {
                var overTime = Convert.ToDateTime(item.BeginTime).AddMinutes((int)item.PlayLongTime);
                if (DateTime.Now > overTime)
                {
                    if (item.DataStatus == 1)
                        item.PlayStatus = 3;
                    else
                        item.PlayStatus = 4;
                    list.Add(item);
                }
                r.Add(item);
            }
            if (list.Count > 0)
                UpdateList(list);
            return r;
        }

        /// <summary>
        /// 主要是时间超时变失效之类的
        /// </summary>
        /// <param name="live"></param>
        public List<LiveView> UpdateLivePlayStatus(List<LiveView> lives)
        {
            var list = new List<Sys_VideoInfo>();
            var r = new List<LiveView>();
            foreach (var item in lives)
            {
                var overTime = item.BeginTime.AddMinutes(item.PlayLongTime);
                if (DateTime.Now > overTime && (item.PlayStatus == 0 || item.PlayStatus == 1 || item.PlayStatus == 2))
                {
                    //如果超时了，审核过的就结束，没审核的就失效
                    if (item.DataStatus == 1)
                        item.PlayStatus = 3;
                    else
                        item.PlayStatus = 4;
                    list.Add(new Sys_VideoInfo { Id = item.Id, PlayStatus = item.PlayStatus });
                }
                r.Add(item);
            }
            if (list.Count > 0)
                UpdateList(list);
            return r;
        }

      

    }
}
