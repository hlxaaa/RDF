using Model;

namespace WcfService.Sys
{
    public class Sys_AboutCfg : BaseCfg
    {

        protected override void SetCfg()
        {
            AddField("主键", "Id", "Id", ColumnType.Int, "t1", true);
            AddField("正文", "Content", "Content", ColumnType.String, "t1", true,true);
            AddField("修改人", "EditorId", "EditorId", ColumnType.Int, "t1", true);
            AddField("修改时间", "EditTime", "EditTime", ColumnType.DateTime, "t1", true);
            Tab = new TableInfo(@"t1.Id,t1.Content,t1.EditorId,t1.EditTime","Sys_About",true);
        }

    }
}
