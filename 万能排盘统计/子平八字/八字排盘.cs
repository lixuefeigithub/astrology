using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 历法;

namespace 子平八字
{
    public static class 八字排盘
    {
        public static 八字命盘 Get八字命盘(DateTime 公历出生日期)
        {
            var year = 公历出生日期.Year;

            var 一年二十四节气 = 历法Utilities.Get一年节气(year);

            var isInCurrentYear = 公历出生日期 >= 一年二十四节气.FirstOrDefault(x => x.Key == 二十四节气.立春).Value;

            一年二十四节气 = isInCurrentYear ? 一年二十四节气 : 历法Utilities.Get一年节气(year - 1);
            一年二十四节气 = 二十四节气重排(一年二十四节气);

            var 年柱 = isInCurrentYear ? 历法Utilities.Get年上干支(year) : 历法Utilities.Get年上干支(year - 1);

            var 日柱 = 历法Utilities.Get日上干支(公历出生日期);

            KeyValuePair<二十四节气, DateTime> 月柱节气 = default(KeyValuePair<二十四节气, DateTime>);

            for (int i = 0; i < 一年二十四节气.Length; i++)
            {
                if (一年二十四节气[i].Value == 公历出生日期)
                {
                    月柱节气 = 一年二十四节气[i];
                    break;
                }

                if (一年二十四节气[i].Value > 公历出生日期)
                {
                    月柱节气 = 一年二十四节气[i-1];
                    break;
                }
            }

            var 月柱 = 历法Utilities.Get月上干支(年柱.天干, GetMonthBy节气(月柱节气.Key));

            var 时柱 = 历法Utilities.Get时上干支(日柱.天干, 公历出生日期);

            return new 八字命盘
            {
                年柱 = 年柱,
                月柱 = 月柱,
                日柱 = 日柱,
                时柱 = 时柱,
            };
        }

        /// <summary>
        /// 将二十四节气按照阴历顺序重排(以立春为一年开始）
        /// </summary>
        /// <param name="二十四节气阳历顺序"></param>
        /// <returns></returns>
        private static KeyValuePair<二十四节气, DateTime>[] 二十四节气重排(KeyValuePair<二十四节气, DateTime>[] 二十四节气阳历顺序)
        {
            if (二十四节气阳历顺序 == null || 二十四节气阳历顺序.Length != 24)
            {
                throw new ArgumentNullException();
            }

            var results = new KeyValuePair<二十四节气, DateTime>[24];

            for (int i = 0; i < 二十四节气阳历顺序.Length; i ++)
            {
                //立春原来排第三，现要挪第一
                var newIndex = i - 2;

                if (newIndex < 0)
                {
                    newIndex = newIndex + 24;
                }

                results[newIndex] = 二十四节气阳历顺序[i];
            }

            return results;
        }

        private static int GetMonthBy节气(二十四节气 节气)
        {
            if (节气 == 二十四节气.立春)
            {
                return 1;
            }

            var diff = 节气 - 二十四节气.立春;

            return 1 + diff / 2;
        }
    }
}
