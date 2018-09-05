using System;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public class RdfCalendar
    {
        /// <summary>
        /// 阳历节日
        /// </summary>
        private static List<Calendar> _ylist;
        public static List<Calendar> yList
        {
            get
            {
                if (_ylist == null)
                    _ylist = new List<Calendar> {
                        new Calendar {Month=1,Day=1,LC="元旦" },
                        new Calendar {Month=2,Day=14,LC="情人节" },
                        new Calendar {Month=3,Day=8,LC="妇女节" },
                        new Calendar {Month=3,Day=12,LC="植树节" },
                        new Calendar {Month=4,Day=1,LC="愚人节" },
                        new Calendar {Month=5,Day=1,LC="劳动节" },
                        new Calendar {Month=5,Day=4,LC="青年节" },
                        new Calendar {Month=6,Day=1,LC="儿童节" },
                        new Calendar {Month=6,Day=5,LC="世界环境日" },
                        new Calendar {Month=8,Day=1,LC="建军节" },
                        new Calendar {Month=9,Day=10,LC="教师节" },
                        new Calendar {Month=10,Day=1,LC="国庆节" },
                        new Calendar {Month=11,Day=11,LC="光棍节" },
                        new Calendar {Month=12,Day=24,LC="平安夜" },
                        new Calendar {Month=12,Day=25,LC="圣诞节" }
                    };
                return _ylist;
            }
        }
        /// <summary>
        /// 农历节日
        /// </summary>
        private static List<Calendar> _nlist;
        public static List<Calendar> nList
        {
            get
            {
                if (_nlist == null)
                    _nlist = new List<Calendar> {
                        new Calendar {NMonth="一",Day=1,LC="春节" },
                        new Calendar {NMonth="一",Day=15,LC="元宵节" },
                        new Calendar {NMonth="五",Day=5,LC="端午节" },
                        new Calendar {NMonth="七",Day=7,LC="七夕节" },
                        new Calendar {NMonth="八",Day=15,LC="中秋节" },
                        new Calendar {NMonth="九",Day=9,LC="重阳节" },
                        new Calendar {NMonth="腊",Day=8,LC="腊八节" },
                        new Calendar {NMonth="腊",Day=23,LC="小年" },
                        new Calendar {NMonth="腊",Day=30,LC="除夕" },
                    };
                return _nlist;
            }
        }
        /// <summary>
        /// 日期转农历
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="formart">TGDZSXMMDD：TG天干DZ地支SX生肖MM月DD日</param>
        /// <param name="useFestival">使用节日</param>
        /// <returns></returns>
        public static string DateToChineseLunisolar(DateTime date, string formart = "TGDZSX年MM月DD", bool useFestival = true)
        {
            System.Globalization.ChineseLunisolarCalendar cal = new System.Globalization.ChineseLunisolarCalendar();
            int year = cal.GetYear(date);
            int month = cal.GetMonth(date);
            int day = cal.GetDayOfMonth(date);
            int leapMonth = cal.GetLeapMonth(year);
            formart = formart.Replace("TG", "甲乙丙丁戊己庚辛壬癸"[(year - 4) % 10].ToString());
            formart = formart.Replace("DZ", "子丑寅卯辰巳午未申酉戌亥"[(year - 4) % 12].ToString());
            formart = formart.Replace("SX", "鼠牛虎兔龙蛇马羊猴鸡狗猪"[(year - 4) % 12].ToString());
            int ts = TombSweeping(date.Year);
            string nMonth = (month == leapMonth ? "闰" : "") + "无正二三四五六七八九十冬腊"[leapMonth > 0 && leapMonth <= month ? month - 1 : month];
            if (useFestival && (formart.Contains("MM") || formart.Contains("DD")))
            {
                if (date.Month == 4 && date.Day == ts)
                    return "清明节";
                Calendar yC = yList.FirstOrDefault(item => item.Month == date.Month && item.Day == date.Day);
                if (yC == null)
                    yC = nList.FirstOrDefault(item => item.NMonth == nMonth && item.Day == day);
                if (yC != null)
                    return yC.LC;
            }
            formart = formart.Replace("MM", nMonth);
            formart = formart.Replace("DD", "初十廿三"[day / 10].ToString() + "十一二三四五六七八九"[day % 10].ToString());
            formart = formart.Replace("十十", "初十");
            return formart;
        }
        /// <summary>
        /// 清明节
        /// </summary>
        /// <param name="year">年</param>
        /// <returns></returns>
        public static int TombSweeping(int year)
        {
            int y = Convert.ToInt32(year.ToString().Substring(2, 2));
            int a = Convert.ToInt32((y * 0.2422 + 4.81).ToString().Substring(0, 1));
            int b = Convert.ToInt32((y / 4).ToString().Substring(0, 1));
            return a - b;
        }
    }
    public class Calendar
    {
        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 阳历月份
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// 农历月份
        /// </summary>
        public string NMonth { get; set; }
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 农历
        /// </summary>
        public string LC { get; set; }
    }
}
