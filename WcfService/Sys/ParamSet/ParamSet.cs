using Tools;
using System;
using Model;
using System.Text;
namespace WcfService.Sys
{
    /// <summary>
    /// 参数设置
    /// </summary>
    public class ParamSet : BaseOpertion
    {
        public ParamSet() : base(8) { }

        public RdfMsg GetData(dynamic param)
        {
            return new RdfMsg(true, new RdfSqlQuery<Sys_ParamSet>().ToJson());
        }

        public RdfMsg SaveData(dynamic param)
        {
            //Sys_ParamSet back = new RdfSqlQuery<Sys_ParamSet>().Where(item => item.key == "PlayCallBack").ToEntity();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("delete Sys_ParamSet");
            foreach (var item in param.data)
            {
                Sys_ParamSet set = new Sys_ParamSet();
                set.DicObj(item);
                sb.AppendLine(set.GetInsertSql());
                //if (set.key=="PlayCallBack" && back!=null && set.value!=back.value)
                //{
                //    //更新回调地址
                //    VCloud v = new VCloud();
                //    v.SetCallBack(set.value);
                //}
            }
            RdfExecuteSql.ExecuteNonQuery(sb.ToString(), false);
            RdfCache.RemoveCache("Sys_ParamSet");
            return new RdfMsg(true);
        }
    }
}
