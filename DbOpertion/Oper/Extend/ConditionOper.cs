using Common;
using Common.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DbOpertion.Models.Extend.AllModel;

namespace DbOpertion.DBoperation
{
    public class ConditionOper : SingleTon<ConditionOper>
    {
        /// <summary>
        /// 安卓版本查询条件
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string AndroidVersionCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" 1=1 ";
            if (!search.IsNullOrEmpty())
                condition += " and (versionCode like @search or versionName like @search or updateLog like @search)";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 音频查询条件
        /// </summary>
        public string PlayAudioCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" 1=1 ";
            if (req.TypeId != 0)
                condition += $" and typeid={req.TypeId}";
            if (!search.IsNullOrEmpty())
                condition += " and (title like @search or typename like @search )";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 设备数据查询条件
        /// </summary>
        public string DeviceDataCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" 1=1  ";

            if (req.gymId != 0)
                condition += $" and gymid={req.gymId} ";

            if (!search.IsNullOrEmpty())
                condition += " and ( deviceName like @search)";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 设备查询条件
        /// </summary>
        public string DeviceCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" 1=1 ";

            if (req.gymId != 0)
                condition += $" and gymid={req.gymId} ";

            if (!search.IsNullOrEmpty())
                condition += " and ( name like @search)";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 模式查询条件
        /// </summary>
        public string DragModelCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" isDeleted=0 ";

            if (req.modelType != 3)
                condition += $" and modelType={req.modelType} ";

            if (!search.IsNullOrEmpty())
                condition += " and (modelName like @search )";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 意见反馈查询条件
        /// </summary>
        public string FeedBackCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" 1=1 ";
            if (!search.IsNullOrEmpty())
                condition += " and (title like @search or content like @search or reply like @search or username like @search)";
            condition += "";
            return condition;
        }
        /// <summary>
        /// 健身房查询条件
        /// </summary>
        public string GymCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" 1=1 ";
            if (!search.IsNullOrEmpty())
                condition += " and ( name like @search or nameE like @search)";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 过审的直播查询条件
        /// </summary>
        public string PassLiveCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" ispass='1' ";
            if (!search.IsNullOrEmpty())
                condition += " and (title like @search or username like @search )";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 没过审的直播查询条件
        /// </summary>
        public string LiveWaitingCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" ispass!='1' ";
            if (!search.IsNullOrEmpty())
                condition += " and (title like @search or username like @search )";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 某个教练的直播列表查询条件
        /// </summary>
        public string LiveByIdCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" userId={req.id} ";

            if (!search.IsNullOrEmpty())
                condition += " and (title like @search or username like @search )";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 资金流水查询条件
        /// </summary>
        public string MoneyDetailCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" status=1 ";
            if (req.date != null)
                condition += $" and createTime>'{req.date}' and createTime<'{Convert.ToDateTime(req.date).AddDays(1)}'";

            if (!search.IsNullOrEmpty())
                condition += " and ( coachname like @search or payUserName like @search )";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 提现查询列表
        /// </summary>
        public string TakeCashCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" type=99 ";
            if (!search.IsNullOrEmpty())
                condition += " and ( coachname like @search)";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 干活留言查询条件
        /// </summary>
        public string SportMsgCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" 1=1 ";

            if (!search.IsNullOrEmpty())
                condition += " and (title like @search or username like @search or msg like @search )";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 干货列表查询条件
        /// </summary>
        public string SportViewCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" 1=1  ";
            if (req.TypeId != 0)
                condition += $" and typeid={req.TypeId} ";
            if (!search.IsNullOrEmpty())
                condition += " and (title like @search or typename like @search or remark like @search)";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 专辑查询条件
        /// </summary>
        public string AudioTypeCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" 1=1 ";
            if (!search.IsNullOrEmpty())
                condition += " and (title like @search  )";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 运动干货分类查询条件
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string SportTypeCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" 1=1 ";

            //if (req.isEnglish != 2)
            //    condition += $" and isEnglish={req.isEnglish} ";

            if (!search.IsNullOrEmpty())
                condition += " and (name like @search or remark like @search )";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 已过审的教练列表查询条件
        /// </summary>
        public string PassCoachCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" type=1 and isPass='1' ";

            if (!search.IsNullOrEmpty())
                condition += " and (username like @search or phone like @search  or useplace like @search or keyname like @search or id=@search2)";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 待审核或未过审教练查询条件
        /// </summary>
        public string CoachWaitingCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" type=2 and isPass!='1' and isPass!='' ";

            //if (req.isEnglish != 2)
            //    condition += $" and isEnglish={req.isEnglish} ";

            if (!search.IsNullOrEmpty())
                condition += " and (username like @search or phone like @search  or useplace like @search or keyname like @search or id=@search2)";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 用户列表查询条件
        /// </summary>
        public string UserCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" 1=1 ";

            if (!search.IsNullOrEmpty())
                condition += " and (username like @search or phone like @search  or useplace like @search or keyname like @search or id=@search2)";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 过审的视频列表查询条件
        /// </summary>
        public string PassVideoCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" ispass='1' ";
            if (req.userId != -1)
                condition += $" and userId={req.userId}";

            //if (req.modelType != 3)
            //    condition += $" and modelType={req.modelType} ";

            if (!search.IsNullOrEmpty())
                condition += " and (title like @search or username like @search )";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 某个教练视频列表查询条件
        /// </summary>
        public string VideoByCoachIdCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" userId={req.id} ";

            if (!search.IsNullOrEmpty())
                condition += " and (title like @search or username like @search )";
            condition += "";
            return condition;
        }

        /// <summary>
        /// 没过审的视频列表查询条件
        /// </summary>
        public string VideoWaitingCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" ispass!='1' ";

            if (!search.IsNullOrEmpty())
                condition += " and (title like @search or username like @search )";
            condition += "";
            return condition;
        }


        /// <summary>
        /// 用户权限1对多视图查询条件
        /// </summary>
        public string AuthViewCondition(GetListCommonReq req)
        {
            var search = req.search ?? "";
            var condition = $" type=0 and isEnglish!=1  ";

            if (!search.IsNullOrEmpty())
                condition += " and (username like @search or phone like @search  or useplace like @search or keyname like @search or id=@search2)";
            condition += "";
            return condition;
        }
    }
}
