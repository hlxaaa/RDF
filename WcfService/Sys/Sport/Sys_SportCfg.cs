using Model;

namespace WcfService.Sys
{
    public class Sys_SportCfg : BaseCfg
    {

        protected override void SetCfg()
        {
            AddField("主键", "Id", "Id", ColumnType.Int, "t1", true);
            AddField("标题", "Title", "Title", ColumnType.String, "t1", true,true);
            AddField("封面图", "TitleUrl", "TitleUrl", ColumnType.String, "t1", true,true);
            AddField("分类", "TypeId", "TypeId", ColumnType.Int, "t1", true);
            AddField("类型", "DataType", "DataType", ColumnType.Int, "t1", true);
            AddField("发布时间", "CreateTime", "CreateTime", ColumnType.DateTime, "t1", true);
            AddField("正文", "Content", "Content", ColumnType.String, "t1", true,true);
            AddField("描述", "Remark", "Remark", ColumnType.String, "t1", true,true);
            AddField("启用", "Enabled", "Enabled", ColumnType.Bit, "t1", true); 
            Tab = new TableInfo(@"t1.Id,t1.Title,t1.TitleUrl,t1.DataType,t1.TypeId,t1.CreateTime,t1.Content,t1.Remark,t1.Enabled","Sys_Sport",true);
        }

    }
}
