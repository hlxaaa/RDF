using System;

namespace Tools
{
    /// <summary>
    /// 数字小数位处理
    /// </summary>
    public class RdfMath
    {
        /// <summary>
        /// 根据小数位四舍五入
        /// </summary>
        /// <param name="number">四舍五入的数字</param>
        /// <param name="length">小数位</param>
        /// <returns>四舍五入后数字</returns>
        public static decimal Round(decimal number, int length)
        {
            int rate = GetRate(length);
            return Math.Round(number * rate, MidpointRounding.AwayFromZero) / rate;
        }
        /// <summary>
        /// 根据小数位进位
        /// </summary>
        /// <param name="number">进位的数字</param>
        /// <param name="length">小数位</param>
        /// <returns>进位后数字</returns>
        public static decimal Ceiling(decimal number, int length)
        {
            int rate = GetRate(length);
            return Math.Ceiling(number * rate) / rate;
        }
        /// <summary>
        /// 根据小数位舍位
        /// </summary>
        /// <param name="number">舍位的数字</param>
        /// <param name="length">小数位</param>
        /// <returns>舍位后数字</returns>
        public static decimal Floor(decimal number, int length)
        {
            int rate = GetRate(length);
            return Math.Floor(number * rate) / rate;
        }
        /// <summary>
        /// 根据长度取得比率
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static int GetRate(int length)
        {
            int rate = 1;
            if (length > 0)
            {
                for (int i = 1; i <= length / 1; i++)
                    rate = rate * 10;
                return rate;
            }
            if (length < 0)
            {
                for (int i = 1; i <= length / -1; i++)
                    rate = rate / 10;
            }
            return rate;
        }
        /// <summary>
        /// 取得小数点后面的0
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Tofixed(object number)
        {
            if (number == null)
                return "";
            string str = number.ToString();
            if (str.Contains("."))
            {
                while (str[str.Length-1]=='0')
                {
                    str = str.Substring(0, str.Length - 1);
                }
                if (str[str.Length - 1] == '.')
                {
                    str = str.Substring(0, str.Length - 1);
                }
            }
            return str;
        }
    }
}
