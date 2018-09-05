using Common.Helper;
using System;
using System.Collections.Generic;
using Common;
using static DbOpertion.Models.Extend.AllModel;

namespace DbOpertion.DBoperation
{
    /// <summary>
    /// viewpage倒是可以统一。mutiview的mainkey字段，如果是id的，可以统一。不是id，可以传。
    /// 其实condition可以放到一个ConditionOper里
    /// </summary>
    public partial class CommonOper : SingleTon<CommonOper>
    {
        public List<T> GetVPList<T>(GetListCommonReq req, string condition) where T : class, new()
        {
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
            };
            var i = 0;
            if (int.TryParse(search, out i))
            {
                dict.Add("@search2", i.ToString());
            }
            else
                dict.Add("@search2", "0");

            return SqlHelper.Instance.GetViewPaging<T>(condition, index, (int)req.size, orderStr, dict);
        }

        public int GetVPCount<T>(GetListCommonReq req, string condition) where T : class, new()
        {
            var search = req.search ?? "";
            var dict = new Dictionary<string, string>
            {
                { "@search", $"%{search}%" },
            };
            var i = 0;
            if (int.TryParse(search, out i))
            {
                dict.Add("@search2", i.ToString());
            }
            else
                dict.Add("@search2", "0");
            var list = SqlHelper.Instance.GetDistinctCount<T>(condition, dict);
            return list.Count;
        }

        public List<T> GetMVList<T>(GetListCommonReq req, string condition) where T : class, new()
        {
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
            };
            var i = 0;
            if (int.TryParse(search, out i))
            {
                dict.Add("@search2", i.ToString());
            }
            else
                dict.Add("@search2", "0");

            order = order == "id" ? "" : $",{order}";

            //var condition = AuthViewCondition(req, search);
            return SqlHelper.Instance.GetMutiView<T>("id", $"id{order}", condition, index, (int)req.size, orderStr, dict);
        }

        public List<T> GetMVList<T>(GetListCommonReq req, string condition, string mainKey) where T : class, new()
        {
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
            };
            var i = 0;
            if (int.TryParse(search, out i))
            {
                dict.Add("@search2", i.ToString());
            }
            else
                dict.Add("@search2", "0");

            order = order == mainKey ? "" : $",{order}";

            //var condition = AuthViewCondition(req, search);
            return SqlHelper.Instance.GetMutiView<T>(mainKey, mainKey + order, condition, index, (int)req.size, orderStr, dict);
        }

        public int GetMVCount<T>(GetListCommonReq req, string condition) where T : class, new()
        {
            var search = req.search ?? "";
            var dict = new Dictionary<string, string>
            {
                { "@search", $"%{search}%" },
            };
            var i = 0;
            if (int.TryParse(search, out i))
            {
                dict.Add("@search2", i.ToString());
            }
            else
                dict.Add("@search2", "0");

            var list = SqlHelper.Instance.GetMutiViewCount<T>("id", condition, dict);
            return list.Count;
        }

        public int GetMVCount<T>(GetListCommonReq req, string condition, string mainKey) where T : class, new()
        {
            var search = req.search ?? "";
            var dict = new Dictionary<string, string>
            {
                { "@search", $"%{search}%" },
            };
            var i = 0;
            if (int.TryParse(search, out i))
            {
                dict.Add("@search2", i.ToString());
            }
            else
                dict.Add("@search2", "0");

            var list = SqlHelper.Instance.GetMutiViewCount<T>(mainKey, condition, dict);
            return list.Count;
        }
    }
}
