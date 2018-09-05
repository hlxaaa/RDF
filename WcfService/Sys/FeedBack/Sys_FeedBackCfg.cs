using Model;

namespace WcfService.Sys
{
    public class Sys_FeedBackCfg : BaseCfg
    {

        protected override void SetCfg()
        {
            AddField("主键", "Id", "Id", ColumnType.Int, "t1", true);
            AddField("标题", "Title", "Title", ColumnType.String, "t1", true,true);
            AddField("正文", "Content", "Content", ColumnType.String, "t1", true,true);
            AddField("提交人", "EditorId", "EditorId", ColumnType.Int, "t1", true);
            AddField("提交时间", "EditTime", "EditTime", ColumnType.DateTime, "t1", true);
            AddField("是不是英文版", "isEnglish", "isEnglish", ColumnType.Int, "t1", true);
            Tab = new TableInfo(@"t1.Id,t1.Title,t1.Content,t1.EditorId,t1.EditTime","Sys_FeedBack",true);
        }

    }
}
