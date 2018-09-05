using Model;

namespace WcfService.Sys
{
    public class Sys_AudioTypeCfg : BaseCfg
    {

        protected override void SetCfg()
        {
            AddField("主键", "Id", "Id", ColumnType.Int, "t1", true);
            AddField("标题", "Title", "Title", ColumnType.String, "t1", true,true);
            AddField("封面图", "TitleUrl", "TitleUrl", ColumnType.String, "t1", true,true);
            AddField("描述", "Remark", "Remark", ColumnType.String, "t1", true,true);
            AddField("启用", "Enabled", "Enabled", ColumnType.Bit, "t1", true);
            AddField("英文标题", "TitleE", "TitleE", ColumnType.String, "t1", true);
            Tab = new TableInfo(@"t1.Id,t1.Title,t1.TitleUrl,t1.Remark,t1.Enabled","Sys_AudioType",true);
        }

    }
}
