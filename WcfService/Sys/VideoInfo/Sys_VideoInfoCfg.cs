using Model;

namespace WcfService.Sys
{
    public class Sys_VideoInfoCfg : BaseCfg
    {

        protected override void SetCfg()
        {
            AddField("主键", "Id", "Id", ColumnType.Int, "t1", true);
            AddField("教练", "UserId", "UserId", ColumnType.Int, "t1", true);
            AddField("封面图", "Url", "Url", ColumnType.String, "t1", true,true);
            AddField("直播标题", "Title", "Title", ColumnType.String, "t1", true,true);
            AddField("直播开始时间", "BeginTime", "BeginTime", ColumnType.DateTime, "t1", true);
            AddField("直播持续时间", "PlayLongTime", "PlayLongTime", ColumnType.Int, "t1", true,true);
            AddField("直播状态", "PlayStatus", "PlayStatus", ColumnType.Int, "t1", true);
            AddField("数据状态", "DataStatus", "DataStatus", ColumnType.Int, "t1", true);
            AddField("直播Id", "CloudId", "CloudId", ColumnType.String, "t1", true, true);
            AddField("点播Id", "VideoId", "VideoId", ColumnType.Int, "t1", true);
            Tab = new TableInfo(@"t1.Id,t1.UserId,t1.Url,t1.Title,t1.BeginTime,t1.PlayLongTime,t1.PlayStatus,t1.DataStatus,t1.CloudId,t1.VideoId", "Sys_VideoInfo", true);
        }

    }
}
