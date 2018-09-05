using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    /// <summary>
    /// 利息计算
    /// </summary>
    public class RdfInterest
    {
        /// <summary>
        /// 等额本金法 每月还款金额 = （贷款本金 / 还款月数）+（本金 — 已归还本金累计额）×每月利率
        /// </summary>
        /// <param name="amount">投资金额</param>
        /// <param name="yearRate">年利率,如年化18%，传入18即可</param>
        /// <param name="months">投资期限，单位：月</param>
        /// <returns></returns>
        public static List<InterestLog> DEBJ(double amount, double yearRate, int months)
        {
            var monthRate = (yearRate) / 1200.0;     //年利率转为月利率
            List<InterestLog> list = new List<InterestLog>();
            var sum = 0.0;
            var p = 0.0;
            for (int i = 1; i <= months; i++)
            {
                p = (amount / months) + (amount - sum) * monthRate;
                sum += p;
                list.Add(new InterestLog()
                {
                    Number = i,
                    Interest = 0,
                    Principal = p,
                    Amount = p
                });
            }
            list.ForEach(item =>
            {
                item.TotalInterest = sum - amount;
                item.TotalAmount = sum;
            });
            return list;
        }
        /// <summary>
        /// 等额本息法
        /// </summary>
        /// <param name="amount">投资金额</param>
        /// <param name="yearRate">年利率,如年化18%，传入18即可</param>
        /// <param name="months">投资期限，单位：月</param>
        /// <returns></returns>
        public static List<InterestLog> DEBX(double amount, double yearRate, int months)
        {
            var monthRate = (yearRate) / 1200.0;     //年利率转为月利率
            var datalist = new List<InterestLog>();
            var i = 0;
            var a = 0.0; // 偿还本息
            var b = 0.0; // 偿还利息
            var c = 0.0; // 偿还本金
            //利息收益
            var totalInterest = (amount * months * monthRate * Math.Pow((1 + monthRate), months)) / (Math.Pow((1 + monthRate), months) - 1) - amount;
            var totalAmount = totalInterest + amount;//应还本息
            var d = amount + totalInterest; // 剩余本息
            totalInterest = Math.Round(totalInterest * 100) / 100;// 支付总利息
            totalAmount = Math.Round(totalAmount * 100) / 100;
            a = totalAmount / months;    //每月还款本息
            a = Math.Round(a * 100) / 100;//每月还款本息 
            for (i = 1; i <= months; i++)
            {
                b = (amount * monthRate * (Math.Pow((1 + monthRate), months) - Math.Pow((1 + monthRate), (i - 1)))) / (Math.Pow((1 + monthRate), months) - 1);
                b = Math.Round(b * 100) / 100;
                c = a - b;
                c = Math.Round(c * 100) / 100;
                d = d - a;
                d = Math.Round(d * 100) / 100;
                if (i == months)
                {
                    c = c + d;
                    b = b - d;
                    c = Math.Round(c * 100) / 100;
                    b = Math.Round(b * 100) / 100;
                    d = 0;
                }
                var unit = new InterestLog();
                unit.Number = i;// 期数
                unit.TotalInterest = totalInterest;// 总利息
                unit.TotalAmount = totalAmount;// 总还款
                unit.Amount = a;// 偿还本息  someNumber.ToString("N2");
                unit.Interest = b;// 偿还利息
                unit.Principal = c;// 偿还本金
                unit.OddAmount = d;// 剩余本息
                datalist.Add(unit);
            }
            return datalist;
        }

        /// <summary>
        /// 按月付息到期还本
        /// </summary>
        /// <param name="amount">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="months">投资期限，单位：月</param>
        /// <returns></returns>
        public static List<InterestLog> AYFX(double amount, double yearRate, int months)
        {
            var datalist = new List<InterestLog>();  //new Array(Deadline);     // 
            double rateIncome = amount * yearRate / 100 * (months / 12.0);
            double rateIncomeEve = (rateIncome / months);
            var total = amount + rateIncome;
            for (var i = 1; i < months; i++)
            {
                var unit = new InterestLog();
                unit.Number = i;// 期数
                unit.TotalInterest = rateIncome;//Math.Round((Amount + TotalRate) * 100) / 100;// 总利息
                unit.TotalAmount = total;//TotalRate;// 总还款
                unit.Amount = rateIncomeEve;// 偿还本息  someNumber.ToString("N2");
                unit.Interest = rateIncomeEve;// 偿还利息
                unit.Principal = 0;// 偿还本金
                unit.OddAmount = amount * 1 + rateIncome * 1 - rateIncomeEve * i;// 剩余本息
                datalist.Add(unit);
            }
            datalist.Add(new InterestLog()
            {
                Number = months,
                TotalInterest = rateIncome,
                TotalAmount = total,
                Amount = amount + rateIncomeEve,
                Interest = rateIncomeEve,
                Principal = amount,
                OddAmount = 0
            });
            return datalist;
        }


        /// <summary>
        /// 一次性还本付息
        /// </summary>
        /// <param name="amount">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="months">投资期限，单位：月</param>
        /// <returns></returns>
        public static InterestLog YCXBX(double amount, double yearRate, int months)
        {
            InterestLog unit = new InterestLog();
            var rate = yearRate;
            var rateIncome = amount * rate / 100 * (months / 12.0);
            var totalAmount = amount + rateIncome;
            unit.Number = 1;// 期数
            unit.TotalInterest = rateIncome;//Math.Round((Amount + TotalRate) * 100) / 100;// 总利息
            unit.TotalAmount = totalAmount;//TotalRate;// 总还款
            unit.Amount = totalAmount;// 偿还本息  someNumber.ToString("N2");
            unit.Interest = rateIncome;// 偿还利息
            unit.Principal = 0;// 偿还本金
            unit.OddAmount = 0;// 剩余本息
            return unit;
        }
    }
    public class InterestLog
    {
        //当前期数
        public int Number { get; set; }
        //总利息
        public double TotalInterest { get; set; }
        //总还款
        public double TotalAmount { get; set; }
        //本期应还本息
        public double Amount { get; set; }
        //本期应还利息
        public double Interest { get; set; }
        //本期应还本金
        public double Principal { get; set; }
        //本期剩余本息
        public double OddAmount { get; set; }
    }
}
