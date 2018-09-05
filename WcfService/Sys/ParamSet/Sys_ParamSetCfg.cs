using Model;

namespace WcfService.Sys
{
    public class Sys_ParamSetCfg : BaseCfg
    {

        protected override void SetCfg()
        {
            AddField("key", "key", "key", ColumnType.String, "t1", true,true);
            AddField("value", "value", "value", ColumnType.String, "t1", true,true);
            AddField("remark", "remark", "remark", ColumnType.String, "t1", true, true);
            AddField("uuid", "uuid", "uuid", ColumnType.String, "t1", true, true);
            AddField("type", "type", "type", ColumnType.Int, "t1", true, true);
            Tab = new TableInfo(@"t1.key,t1.value,t1.remark,t1.uuid,t1.type","Sys_ParamSet");
        }

    }
}
