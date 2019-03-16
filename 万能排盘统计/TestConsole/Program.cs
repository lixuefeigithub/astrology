using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 历法;
using 子平八字;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var calendar = new ChineseLunisolarCalendar();
            Console.WriteLine(string.Join(",", calendar.Eras));
            Console.WriteLine(calendar.GetMonth(new DateTime (1984,11,26)));
            Console.WriteLine(calendar.GetDayOfMonth(new DateTime(1984, 11, 26)));
            Console.WriteLine(calendar.GetDaysInMonth(1984, 11));
            Console.WriteLine(calendar.GetMonthsInYear(1984, 1));
            Console.WriteLine(calendar.GetSexagenaryYear(new DateTime(1984, 11, 26)));
            Console.WriteLine(calendar.GetTerrestrialBranch(1));
            Console.WriteLine(calendar.IsLeapMonth(1984, 11, 1));
            Console.WriteLine(calendar.IsLeapDay(1984, 11, 26, 1));
            Console.WriteLine(string.Join(",", 天干地支Utilities.All天干));
            Console.WriteLine(string.Join(",", 天干地支Utilities.All地支));
            Console.WriteLine(string.Join(",", 天干地支Utilities.Get一甲子().Select(x => x.Name)));
            Console.WriteLine(历法Utilities.SolarTermCurrent(new DateTime(1984, 11, 26)));

            var 一年节气 = 历法Utilities.Get一年节气(1972);
            foreach(var 节气 in 一年节气)
            {
                Console.WriteLine($"节气: {节气.Key}，日期: {节气.Value}");
            }

            var 命盘 = 八字排盘.Get八字命盘(new DateTime(1921, 5, 22, 14, 20, 0));

            Console.WriteLine(命盘.ToString());

            Console.ReadLine();
        }
    }
}
