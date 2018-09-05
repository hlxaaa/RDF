using Model;

namespace WcfService.Sys
{
    public class Sys_UserAuthCfg : BaseCfg
    {

        protected override void SetCfg()
        {
            AddField("用户Id", "UserId", "UserId", ColumnType.Int, "t1", true);
            AddField("菜单Id", "MenuId", "MenuId", ColumnType.Int, "t1", true);
            Tab = new TableInfo(@"t1.*","Sys_UserAuth");
        }

    }
}
