using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 历法
{
    public static partial class 历法Utilities
    {
        private static ChineseLunisolarCalendar _calendar = new ChineseLunisolarCalendar();

        public static 天干 Get月上正月天干(天干 年干)
        {
            /***********************
            若遇甲或己的年份，正月是丙寅；
            遇上乙或庚之年，正月为戊寅；
            丙或辛之年正月为庚寅，
            丁或壬之年正为为壬寅，戊或癸之年正月为甲寅
            ***********************/
            switch (年干)
            {
                case 天干.甲:
                case 天干.己:
                    return 天干.丙;
                case 天干.乙:
                case 天干.庚:
                    return 天干.戊;
                case 天干.丙:
                case 天干.辛:
                    return 天干.庚;
                case 天干.丁:
                case 天干.壬:
                    return 天干.壬;
                case 天干.戊:
                case 天干.癸:
                    return 天干.甲;
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month">1-12, if leap month then must give original month number</param>
        /// <returns></returns>
        public static 地支 Get月上地支(int month)
        {
            /***********************
            正月是寅月
            ***********************/
            if (month == 1)
            {
                return 地支.寅;
            }

            if (month > 1 && month <= 12)
            {
                var 月上地支序号 = (int)Get月上地支(1) + (month - 1);

                月上地支序号 = 月上地支序号 <= 12 ? 月上地支序号 : 月上地支序号 - 12;
                return (地支)(月上地支序号);
            }

            throw new ArgumentOutOfRangeException($"Month must from 1 -12");
        }

        public static 天干 Get时上子时天干(天干 日干)
        {
            /***********************
            甲己还生甲，乙庚丙作初，
            丙辛从戊起，丁壬庚子居，
            戊癸何方发，壬子是真途。
            即若该日是甲或己的，在子时上配上甲为甲子；
            日是乙或庚的，在子时上配上丙为丙子；
            丙辛日子时配上戊为戊子；
            丁任日为庚子；戊癸日为壬子。
            ***********************/
            switch (日干)
            {
                case 天干.甲:
                case 天干.己:
                    return 天干.甲;
                case 天干.乙:
                case 天干.庚:
                    return 天干.丙;
                case 天干.丙:
                case 天干.辛:
                    return 天干.戊;
                case 天干.丁:
                case 天干.壬:
                    return 天干.庚;
                case 天干.戊:
                case 天干.癸:
                    return 天干.壬;
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// 为了方便只传阳历年号，实际返回干支要掐头从立春（或者正月）开始算
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static 干支 Get年上干支(int year)
        {
            if (year == 1924)
            {
                return new 干支 { 天干 = 天干.甲, 地支 = 地支.子 };
            }

            var 一甲子 = 天干地支Utilities.Get一甲子();

            if (year > 1924)
            {
                var diff = year - 1924;

                var mod = diff % 60;

                return 一甲子[mod];
            }

            if (year < 1924)
            {
                var diff = 1924 - year;

                var mod = diff % 60;

                return 一甲子[60 - mod];
            }

            throw new ArgumentException();
        }

        public static 干支 Get日上干支(DateTime dateTime)
        {
            //1984年11月26日为甲子日
            var dateTimeBase = new DateTime(1984, 11, 26);

            if (dateTime.Date == dateTimeBase)
            {
                return new 干支 { 天干 = 天干.甲, 地支 = 地支.子 };
            }

            var 一甲子 = 天干地支Utilities.Get一甲子();

            if (dateTime.Date > dateTimeBase)
            {
                var diffDays = (dateTime.Date - dateTimeBase).Days;

                var mod = diffDays % 60;

                return 一甲子[mod];
            }

            if (dateTime.Date < dateTimeBase)
            {
                var diffDays = (dateTimeBase - dateTime.Date).Days;

                var mod = diffDays % 60;

                return 一甲子[60 - mod];
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year">为了方便只传阳历年号，实际返回干支要掐头从立春（或者正月）开始算</param>
        /// <param name="month">为了方便只传1-12，具体如何月柱如何区分根据紫微或者八字来算</param>
        /// <returns></returns>
        public static 干支 Get月上干支(int year, int month)
        {
            var 年干 = Get年上干支(year).天干;

            return Get月上干支(年干, month);
        }

        public static 干支 Get月上干支(天干 年干, int month)
        {
            var 正月天干 = Get月上正月天干(年干);

            var 月上天干序号 = (int)正月天干 + month - 1;

            月上天干序号 = 月上天干序号 <= 10 ? 月上天干序号 : 月上天干序号 - 10;
            var 月上天干 = (天干)月上天干序号;

            var 月上地支 = Get月上地支(month);

            return new 干支 { 天干 = 月上天干, 地支 = 月上地支 };
        }

        public static 地支 Get时上地支(DateTime dateTime)
        {
            /*********************
            时辰时间对照表
            早子时（zǐ）	夜半	24:00-01:00	三更
            丑时（chǒu）	鸡鸣	01:00-03:00	四更
            寅时（yín）	平旦	03:00-05:00	五更
            卯时（mǎo）	日出	05:00-07:00	
            辰时（chén）	食时	07:00-09:00	
            巳时（sì）	隅中	09:00-11:00	
            午时（wǔ）	日中	11:00-13:00	
            未时（wèi）	日映	13:00-15;00	
            申时（shēn）	哺时	15:00-17:00	
            酉时（yǒu）	日入	17:00-19:00	
            戌时（xū）	黄昏	19:00-21:00	一更
            亥时（hài）	人定	21:00-23:00	二更
            夜子时（zǐ）	夜半	23:00-24:00	三更

             * *******************/

            //For now 我们只用这个时辰表，各季节和地方日出日落时间暂不考虑
            if (dateTime.TimeOfDay >= TimeSpan.FromHours(0) && dateTime.TimeOfDay < TimeSpan.FromHours(1))
            {
                return 地支.子;
            }

            if (dateTime.TimeOfDay >= TimeSpan.FromHours(1) && dateTime.TimeOfDay < TimeSpan.FromHours(3))
            {
                return 地支.丑;
            }

            if (dateTime.TimeOfDay >= TimeSpan.FromHours(3) && dateTime.TimeOfDay < TimeSpan.FromHours(5))
            {
                return 地支.寅;
            }

            if (dateTime.TimeOfDay >= TimeSpan.FromHours(5) && dateTime.TimeOfDay < TimeSpan.FromHours(7))
            {
                return 地支.卯;
            }

            if (dateTime.TimeOfDay >= TimeSpan.FromHours(7) && dateTime.TimeOfDay < TimeSpan.FromHours(9))
            {
                return 地支.辰;
            }

            if (dateTime.TimeOfDay >= TimeSpan.FromHours(9) && dateTime.TimeOfDay < TimeSpan.FromHours(11))
            {
                return 地支.巳;
            }

            if (dateTime.TimeOfDay >= TimeSpan.FromHours(11) && dateTime.TimeOfDay < TimeSpan.FromHours(13))
            {
                return 地支.午;
            }

            if (dateTime.TimeOfDay >= TimeSpan.FromHours(13) && dateTime.TimeOfDay < TimeSpan.FromHours(15))
            {
                return 地支.未;
            }

            if (dateTime.TimeOfDay >= TimeSpan.FromHours(15) && dateTime.TimeOfDay < TimeSpan.FromHours(17))
            {
                return 地支.申;
            }

            if (dateTime.TimeOfDay >= TimeSpan.FromHours(17) && dateTime.TimeOfDay < TimeSpan.FromHours(19))
            {
                return 地支.酉;
            }

            if (dateTime.TimeOfDay >= TimeSpan.FromHours(19) && dateTime.TimeOfDay < TimeSpan.FromHours(21))
            {
                return 地支.戌;
            }

            if (dateTime.TimeOfDay >= TimeSpan.FromHours(21) && dateTime.TimeOfDay < TimeSpan.FromHours(23))
            {
                return 地支.亥;
            }

            if (dateTime.TimeOfDay >= TimeSpan.FromHours(23))
            {
                return 地支.子;
            }

            throw new ArgumentNullException();
        }

        public static 干支[] Get一天12个时辰(天干 天干)
        {
            var 子时天干 = Get时上子时天干(天干);

            var twelve地支 = Enum.GetValues(typeof(地支)).Cast<地支>().OrderBy(x => x).ToArray();

            var twelve时辰 = new 干支[12];

            for (int i = 1; i <= 12; i++)
            {
                var 时上地支 = twelve地支[i - 1];

                var 时上天干序号 = i <= 10 ? i : i - 10;
                var 时上天干 = (天干)时上天干序号;

                twelve时辰[i-1] = new 干支 { 天干 = 时上天干, 地支 = 时上地支 };
            }

            return twelve时辰;
        }

        public static 干支 Get时上干支(天干 天干, DateTime dateTime)
        {
            var 子时天干 = Get时上子时天干(天干);
            var 时上地支 = Get时上地支(dateTime);

            var num = 时上地支 - 地支.子;

            var 时上天干序号 = (int)子时天干 + num;
            时上天干序号 = 时上天干序号 <= 10 ? 时上天干序号 : 时上天干序号 - 10;

            var 时上天干 = (天干)时上天干序号;

            return new 干支 { 天干 = 时上天干, 地支 = 时上地支 };
        }
    }
}
