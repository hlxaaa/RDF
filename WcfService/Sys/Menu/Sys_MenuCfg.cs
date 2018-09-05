using Model;

namespace WcfService.Sys
{
    public class Sys_MenuCfg : BaseCfg
    {

        protected override void SetCfg()
        {
            AddField("主键", "Id", "Id", ColumnType.Int, "t1", true);
            AddField("菜单编号", "MenuCode", "MenuCode", ColumnType.String, "t1", true,true);
            AddField("菜单名称", "MenuName", "MenuName", ColumnType.String, "t1", true,true);
            AddField("菜品地址", "MenuUrl", "MenuUrl", ColumnType.String, "t1", true,true);
            AddField("序号", "Sort", "Sort", ColumnType.Int, "t1", true);
            Tab = new TableInfo(@"t1.*", "Sys_Menu", true);
        }

    }
}
