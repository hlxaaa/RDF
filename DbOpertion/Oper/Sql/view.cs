using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.Oper.Sql
{
    public class SqlView : SingleTon<SqlView>
    {
        public string PayView() {
            return $@"SELECT
	pr.id,
	pr.outTradeNo,
	pr.coachId,
	pr.userId,
	pr.type,
	pr.money,
	pr.status,
	pr.createTime,
pr.account,
pr.accountName,
pr.payType,
case pr.payType
WHEN 0 THEN
	'支付宝支付'
WHEN 1 THEN
	'微信支付'
END as payTypeName,
	CASE pr.type
WHEN 0 THEN
	'课程收入'
WHEN 1 THEN
	'直播收入'
WHEN 99 THEN
	'申请提现'
END AS typeName,
case pr.type 
when 99 then '平台'
else
u2.UserName
end as payUserName,
u.UserName coachName,u.isEnglish
FROM
	PayRecord pr
left JOIN Sys_User u on u.id=pr.coachId
left JOIN Sys_User u2 on u2.id=pr.userId";
        }
    }
}
