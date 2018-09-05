using Model;

namespace WcfService.Sys
{
    public class Sys_UserCfg : BaseCfg
    {

        protected override void SetCfg()
        {
            AddField("主键", "Id", "Id", ColumnType.Int, "t1", true);
            AddField("类型", "Type", "Type", ColumnType.Int, "t1", true);
            AddField("密码", "UserPwd", "UserPwd", ColumnType.String, "t1", true,true);
            AddField("姓名", "UserName", "UserName", ColumnType.String, "t1", true,true);
            AddField("手机号", "Phone", "Phone", ColumnType.String, "t1", true,true);
            AddField("UID", "UId", "UId", ColumnType.String, "t1", true,true);
            AddField("个性签名", "UserExplain", "UserExplain", ColumnType.String, "t1", true,true);
            AddField("头像", "Url", "Url", ColumnType.String, "t1", true,true);
            AddField("生日", "Birthday", "Birthday", ColumnType.Date, "t1", true);
            AddField("性别", "Sex", "Sex", ColumnType.Bit, "t1", true);
            AddField("身高", "Height", "Height", ColumnType.Decimal, "t1", true);
            AddField("体重", "Weight", "Weight", ColumnType.Decimal, "t1", true);
            AddField("目标体重", "IdealWeight", "IdealWeight", ColumnType.Decimal, "t1", true);
            AddField("注册时间", "RegisterTime", "RegisterTime", ColumnType.DateTime, "t1", true);
            AddField("最后登录时间", "LoginTime", "LoginTime", ColumnType.DateTime, "t1", true);
            AddField("启用", "Enabled", "Enabled", ColumnType.Bit, "t1", true);
            Tab = new TableInfo(@"t1.*","Sys_User",true);
        }

    }
}
