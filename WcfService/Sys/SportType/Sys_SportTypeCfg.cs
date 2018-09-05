using Model;

namespace WcfService.Sys
{
    public class Sys_SportTypeCfg : BaseCfg
    {

        protected override void SetCfg()
        {
            AddField("主键", "Id", "Id", ColumnType.Int, "t1", true);
            AddField("名称", "Name", "Name", ColumnType.String, "t1", true,true);
            AddField("描述", "Remark", "Remark", ColumnType.String, "t1", true,true);
            AddField("启用", "Enabled", "Enabled", ColumnType.Bit, "t1", true);
            Tab = new TableInfo(@"t1.Id,t1.Name,t1.Remark,t1.Enabled","Sys_SportType",true);
        }

    }
}
