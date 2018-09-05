using Model;

namespace WcfService.Sys
{
    public class Sys_DataCfg : BaseCfg
    {

        protected override void SetCfg()
        {
            AddField("用户Id", "UserId", "UserId", ColumnType.Int, "t1", true);
            AddField("创建时间", "CreateTime", "CreateTime", ColumnType.DateTime, "t1", true);
            AddField("心率", "XL", "XL", ColumnType.Decimal, "t1", true);
            AddField("速度", "SD", "SD", ColumnType.Decimal, "t1", true);
            AddField("卡路里", "KAL", "KAL", ColumnType.Decimal, "t1", true);
            AddField("里程", "KM", "KM", ColumnType.Decimal, "t1", true);
            AddField("总里程", "TotalKM", "TotalKM", ColumnType.Decimal, "t1", true);
            AddField("转速", "ZS", "ZS", ColumnType.Decimal, "t1", true);
            AddField("时间", "Time", "Time", ColumnType.String, "t1", true,true);
            Tab = new TableInfo(@"t1.UserId,t1.CreateTime,t1.XL,t1.SD,t1.KAL,t1.KM,t1.TotalKM,t1.ZS,t1.Time", "Sys_Data");
        }

    }
}
