using Common;
using Common.Result;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static DbOpertion.Models.Extend.AllModel;

namespace ServerEnd.Oper.Function
{
    public class DragModelFunc : SingleTon<DragModelFunc>
    {
        public ResultWeb<GetModelListRes> GetModelList(GetListCommonReq req)
        {
            var size = (int)req.size;
            var pages = 0;
            var res = new List<GetModelListRes>();
            var r = new ResultWeb<GetModelListRes>
            {
                HttpCode = 200
            };
            var condition = ConditionOper.Instance.DragModelCondition(req);
            //var condition = DragModelOper.Instance.Condition(req);
            var list2 = CommonOper.Instance.GetVPList<DragModel>(req, condition);
            if (list2.Count == 0)
                r.ListData = res;
            else
            {
                foreach (var item in list2)
                {
                    var temp = new GetModelListRes(item);
                    r.ListData.Add(temp);
                }
                var count = CommonOper.Instance.GetVPCount<DragModel>(req, condition);
                pages = count / size;
                //总页数
                pages = pages * size == count ? pages : pages + 1;
                r.pages = pages;

                r.index = Convert.ToInt32(req.pageIndex);
            }
            return r;
        }

        public ResultJson AddModel(AddModelReq req)
        {
            foreach (var item in req.ms)
            {
                var r = 0;
                var flag = int.TryParse(item.ToString(), out r);
                if (!flag)
                    throw new Exception("请输入整数");
                if (r < 0)
                    throw new Exception("请输入正整数");
            }
            foreach (var item in req.ns)
            {
                var r = 0;
                var flag = int.TryParse(item.ToString(), out r);
                if (!flag)
                    throw new Exception("请输入整数");
                if (r < 0 || r > 32)
                    throw new Exception("阻力为0-32的整数");
            }
            var list = new List<MN>();
            for (int i = 0; i < req.ms.Length; i++)
            {
                list.Add(new MN(req.ms[i], req.ns[i]));
            }
            list = list.OrderBy(p => p.m).ToList();

            var dm = new DragModel(req, JsonConvert.SerializeObject(list));
            dm.modelNameE = req.modelNameE;
            dm.isEnglish = req.isEnglish;
            DragModelOper.Instance.Add(dm);
            return new ResultJson("添加成功");
        }

        public ResultJson<GetModelByIdRes> GetModelById(IdReq req)
        {
            var id = (int)req.Id;
            var dm = DragModelOper.Instance.GetById(id);
            var r = new ResultJson<GetModelByIdRes>();
            r.ListData.Add(new GetModelByIdRes(dm));
            return r;
        }

        public ResultJson UpdateModel(UpdateModelReq req)
        {
            foreach (var item in req.ms)
            {
                var r = 0;
                var flag = int.TryParse(item.ToString(), out r);
                if (!flag)
                    throw new Exception("请输入整数");
                if (r < 0)
                    throw new Exception("请输入正整数");
            }
            foreach (var item in req.ns)
            {
                var r = 0;
                var flag = int.TryParse(item.ToString(), out r);
                if (!flag)
                    throw new Exception("请输入整数");
                if (r < 0 || r > 32)
                    throw new Exception("阻力为0-32的整数");
            }
            var list = new List<MN>();
            for (int i = 0; i < req.ms.Length; i++)
            {
                list.Add(new MN(req.ms[i], req.ns[i]));
            }
            list = list.OrderBy(p => p.m).ToList();

            var dm = new DragModel(req, JsonConvert.SerializeObject(list));
            dm.modelNameE = req.modelNameE;
            DragModelOper.Instance.Update(dm);
            return new ResultJson("更新成功");
        }

        public ResultJson DeleteModel(IdReq req)
        {
            var dm = new DragModel();
            dm.id = (int)req.Id;
            dm.isDeleted = true;
            DragModelOper.Instance.Update(dm);
            return new ResultJson("删除成功");
        }
    }
}