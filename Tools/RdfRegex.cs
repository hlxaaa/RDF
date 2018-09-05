using System.Text.RegularExpressions;

namespace Tools
{
    /// <summary>
    /// 正则验证
    /// </summary>
    public class RdfRegex
    {
        //*  匹配前面的子表达式零次或多次。等价于{0,}。 
        //+  匹配前面的子表达式一次或多次。等价于{1,}。 
        //?  匹配前面的子表达式零次或一次。等价于{0,1}。 
        //\d 匹配一个数字字符。等价于 [0-9]。
        //\D 匹配一个非数字字符。等价于 [^0-9]。 
        /// <summary>
        /// 验证手机号码 规则必须1开头后面接3,4,5,8任意一个在家9个数字
        /// (3|4|5|8)可用[3,4,5,8]
        /// \d可用[0-9]
        /// {9}匹配9次
        /// $结束,否则后面的会不匹配 例如 13873311234ABC也会通过
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool MobilePhone(object input)
        {
            if (input == null)
                return false;
            return Regex.IsMatch(input.ToString(), @"^1(3|4|5|7|8)\d{9}$");
        }
        /// <summary>
        /// 电子邮箱 规则任意字符开头接@任意字符.com
        /// \w可用[A-Za-z0-9_] 或 [A-Z|a-z|0-9|_] 或 [A-Z,a-z,0-9,_]
        /// {1,}至少出现一次
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Email(object input)
        {
            if (input == null)
                return false;
            return Regex.IsMatch(input.ToString(), @"^\w{1,}@\w{1,}.com$");
        }
        /// <summary>
        /// 正整数 规则必须1-9开头后面0-9零次或多次,如果是0开头只能是0
        /// (0或者另个一个验证)
        /// [1-9]{1,1}验证第一个数字是否在1-9
        /// [0-9]*零次或多次 等价于{0,}
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool PositiveInt(object input)
        {
            if (input == null)
                return false;
            return Regex.IsMatch(input.ToString(), @"^(0|[1-9]{1}\d*)$");
        }
        /// <summary>
        /// 整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Int(object input)
        {
            if (input == null)
                return false;
            return Regex.IsMatch(input.ToString(), @"^[+-]?(0|[1-9]{1}\d*)$");
        }
        /// <summary>
        /// 数字 包括小数负数正数 规则正负开头0次或一次接着数字一次或以上接着（如果点开头则后面必须有数字一次或以上否则数字0次或以上）
        /// [+-]?正负号出现0次或1次
        /// \d+数字必须出现
        /// .\d+点后面必须出现数字
        /// \d*零次或以上
        /// </summary>
        /// <returns></returns>
        public static bool Number(object input)
        {
            if (input == null)
                return false;
            return Regex.IsMatch(input.ToString(), @"^[+-]?\d+(.\d+|\d*)$");
        }
        /// <summary>
        /// 日期时间 年月日时分秒毫秒 年月日必须
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool DateTime(object input)
        {
            if (input == null)
                return false;
            return Regex.IsMatch(input.ToString(), @"^\d{1,4}[-,/](0?[1-9]|1[0-2])[-,/](0?[1-9]|1\d|2\d|3[0,1])(\s{1,1}(0?[0-9]|1\d|2[0-3])|$)(:(0?[0-9]|[1-5]\d)|$)(:(0?[0-9]|[1-5]\d)|$)(.(\d{1,3})|$)$");
        }
        /// <summary>
        /// 日期 年月日
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Date(object input)
        {
            if (input == null)
                return false;
            return Regex.IsMatch(input.ToString(), @"^\d{1,4}[-,/](0?[1-9]|1[0-2])[-,/](0?[1-9]|1\d|2\d|3[0,1])$");
        }
        /// <summary>
        /// 时间 时分秒毫秒 时分秒必须
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Time(object input)
        {
            if (input == null)
                return false;
            return Regex.IsMatch(input.ToString(), @"^(0?[0-9]|1\d|2[0-3]):(0?[0-9]|[1-5]\d):(0?[0-9]|[1-5]\d)(.\d{1,3}|$)$");
        }
        /// <summary>
        /// 验证guid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Guid(object input)
        {
            if (input == null)
                return false;
            return Regex.IsMatch(input.ToString(), @"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$");
        }
        /// <summary>
        /// 验证bool
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Bool(object input)
        {
            if (input == null)
                return false;
            string value = input.ToString();
            if (PositiveInt(value))
                return value == "1";
            return value.ToLower() == "true";
        }
        /// <summary>
        /// 用户名(必须,只能)包含数字和字母
        /// </summary>
        /// <returns></returns>
        public static bool UserName(object input)
        {
            if (input == null)
                return false;
            return Regex.IsMatch(input.ToString(), @"^([a-zA-Z]+\d+|\d+[a-zA-Z]+)$");
        }
    }
}
