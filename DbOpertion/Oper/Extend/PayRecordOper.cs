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
using DbOpertion.Oper.Sql;

namespace DbOpertion.DBoperation
{
    public partial class PayRecordOper : SingleTon<PayRecordOper>
    {
        public List<PayView> GetMoneyDetailListTest()
        {
            var req = new GetListCommonReq();
            req.orderField = "id";
            req.isDesc = true;
            req.pageIndex = 1;
            req.size = 5;
            var search = req.search ?? "";
            var order = req.orderField;
            var desc = Convert.ToBoolean(req.isDesc);
            var index = Convert.ToInt32(req.pageIndex);

            var orderStr = $"order by {order} ";
            if (desc)
                orderStr += " desc ";
            else
                orderStr += " asc ";
            var dict = new Dictionary<string, string>
            {
                { "@search", $"%{search}%" },
                 { "@search2", search },
            };

            var condition = MoneyDetailCondition(req);

            return SqlHelper.Instance.GetViewPagingOwn<PayView>(@"select * from  PayView ", condition, index, (int)req.size, orderStr, SqlView.Instance.PayView(), dict);
        }
        
        #region api
        public List<PayView> GetViewByCoachId(int id, int size, int index, DateTime date)
        {
            var condition = $" status=1 and coachId = {id} and createTime>'{date}' and createTime<'{date.AddMonths(1)}'";
            return SqlHelper.Instance.GetViewPaging<PayView>(condition, index, size, " order by id desc", new Dictionary<string, string>());
        }
        #endregion

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
        
    }
}
