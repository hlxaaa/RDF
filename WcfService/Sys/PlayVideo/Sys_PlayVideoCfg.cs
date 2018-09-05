using Model;

namespace WcfService.Sys
{
    public class Sys_PlayVideoCfg : BaseCfg
    {

        protected override void SetCfg()
        {
            AddField("主键", "Id", "Id", ColumnType.Int, "t1", true);
            AddField("标题", "Title", "Title", ColumnType.String, "t1", true,true);
            AddField("封面图", "TitleUrl", "TitleUrl", ColumnType.String, "t1", true,true);
            AddField("Url", "Url", "Url", ColumnType.String, "t1", true,true);
            AddField("时长", "LongTime", "LongTime", ColumnType.String, "t1", true);
            AddField("更新时间", "EditTime", "EditTime", ColumnType.DateTime, "t1", true);
            AddField("播放次数", "PlayCount", "PlayCount", ColumnType.Int, "t1", true);
            AddField("启用", "Enabled", "Enabled", ColumnType.Bit, "t1", true);
            Tab = new TableInfo(@"t1.Id,t1.Title,t1.TitleUrl,t1.Url,t1.LongTime,t1.EditTime,t1.PlayCount,t1.Enabled","Sys_PlayVideo",true);
        }

    }
}
