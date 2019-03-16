using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace 历法
{
    public static partial class 历法Utilities
    {
        /***************************
         * 算法来自 https://www.jianshu.com/p/de33e6d9d880
         * 另外一些有用的资料
         * https://blog.csdn.net/orbit/article/details/7910220
         * https://www.twblogs.net/a/5b8fee1f2b71776722161ee7
         * https://blog.csdn.net/Metal1/article/details/39103631
        * ***********************************/
        private static readonly string[] _solarTerm =
        {
            "小寒", "大寒", "立春", "雨水", "惊蛰", "春分",
            "清明", "谷雨", "立夏", "小满", "芒种", "夏至",
            "小暑", "大暑", "立秋", "处暑", "白露", "秋分",
            "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
        };

        private static readonly int[] _solarTermInfo = {
            0, 21208, 42467, 63836, 85337, 107014, 128867, 150921, 173149, 195551, 218072, 240693, 263343, 285989,
            308563, 331033, 353350, 375494, 397447, 419210, 440795, 462224, 483532, 504758
        };

        private static int[] _solar = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };

        private static DateTime _baseDateAndTime = new DateTime(1900, 1, 6, 2, 5, 0); //#1/6/1900 2:05:00 AM#
        
        /// <summary>
        /// 当前节气 (exactly match 当天)，没有则返回空
        /// </summary>
        public static 二十四节气? SolarTerm(DateTime dateTime)
        {
            var i = SolarTermFunc(dateTime, (x, y) => x == y, out var dt);
            //return i == -1 ? "" : _solarTerm[i];
            return i;
        }

        /// <summary>
        /// 当前节气 (上一个节气到下一个节气之间)
        /// </summary>
        public static 二十四节气? SolarTermCurrent(DateTime dateTime)
        {
            var i = SolarTermFunc(dateTime, (x, y) => x <= y, out var dt);
            //return i == -1 ? "" : _solarTerm[i];
            return i;
        }

        /// <summary>
        /// 上一个节气
        /// </summary>
        public static 二十四节气? SolarTermPrev(DateTime dateTime)
        {
            var i = SolarTermFunc(dateTime, (x, y) => x < y, out var dt);
            //return i == -1 ? "" : _solarTerm[i];
            return i;
        }

        /// <summary>
        /// 下一个节气
        /// </summary>
        public static 二十四节气? SolarTermNext(DateTime dateTime)
        {
            var i = SolarTermFunc(dateTime, (x, y) => x > y, out var dt);
            //return i == -1 ? "" : $"{_solarTerm[i]}";
            return i;
        }

        public static KeyValuePair<二十四节气, DateTime>[] Get一年节气(int year)
        {
            if (year < 1900)
            {
                throw new ArgumentException("year must greater than 1900");
            }

            int[] solar = new int[24];
            Array.Copy(_solar, solar, 24);

            KeyValuePair<二十四节气, DateTime>[] results = new KeyValuePair<二十四节气, DateTime>[24];

            for (int i = 0; i < results.Length; i++)
            {
                var item = solar[i];

                var num = 525948.76 * (year - 1900) + _solarTermInfo[item - 1];
                var newDate = _baseDateAndTime.AddMinutes(num); //按分钟计算

                results[i] = new KeyValuePair<二十四节气, DateTime>((二十四节气)item, newDate);
            }

            return results;
        }

        /// <summary>
        /// 节气计算（当前年），返回指定条件的节气序及日期（公历）
        /// </summary>
        /// <param name="func"></param>
        /// <param name="dateTime"></param>
        /// <returns>-1时即没找到</returns>
        private static 二十四节气? SolarTermFunc(DateTime _dateTime, Expression<Func<int, int, bool>> func, out DateTime dateTime)
        {
            //var baseDateAndTime = new DateTime(1900, 1, 6, 2, 5, 0); //#1/6/1900 2:05:00 AM#
            var year = _dateTime.Year;
            //int[] solar = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };

            int[] solar = new int[24];
            Array.Copy(_solar, solar, 24);

            var expressionType = func.Body.NodeType;
            if (expressionType != ExpressionType.LessThan && expressionType != ExpressionType.LessThanOrEqual &&
                expressionType != ExpressionType.GreaterThan && expressionType != ExpressionType.GreaterThanOrEqual &&
                expressionType != ExpressionType.Equal)
            {
                throw new NotSupportedException("不受支持的操作符");
            }

            if (expressionType == ExpressionType.LessThan || expressionType == ExpressionType.LessThanOrEqual)
            {
                solar = solar.OrderByDescending(x => x).ToArray();
            }

            foreach (var item in solar)
            {
                var num = 525948.76 * (year - 1900) + _solarTermInfo[item - 1];
                var newDate = _baseDateAndTime.AddMinutes(num); //按分钟计算
                if (func.Compile()(newDate.DayOfYear, _dateTime.DayOfYear))
                {
                    dateTime = newDate;
                    return (二十四节气)item;
                }
            }

            dateTime = _calendar.MinSupportedDateTime;
            return null;
        }
    }
}
