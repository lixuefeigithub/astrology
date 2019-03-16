using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 历法
{
    public static partial class 天干地支Utilities
    {
        public static 天干[] All天干 = Enum.GetValues(typeof(天干)).OfType<天干>().ToArray();
        public static 地支[] All地支 = Enum.GetValues(typeof(地支)).OfType<地支>().ToArray();

        public static 天干 GetNext天干(this 天干 current天干)
        {
            if (current天干 == 天干.癸)
            {
                return 天干.甲;
            }

            return (天干)((int)current天干 + 1);
        }

        public static 地支 GetNext地支(this 地支 current地支)
        {
            if (current地支 == 地支.亥)
            {
                return 地支.子;
            }

            return (地支)((int)current地支 + 1);
        }

        public static 干支 GetNext干支(this 干支 current干支)
        {
            return new 干支
            {
                天干 = current干支.天干.GetNext天干(),
                地支 = current干支.地支.GetNext地支()
            };
        }

        public static 干支[] Get一甲子()
        {
            var 一甲子 = new 干支[60];
            一甲子[0] = new 干支 { 天干 = 天干.甲, 地支 = 地支.子 };

            for (int i = 1; i < 一甲子.Length; i++)
            {
                一甲子[i] = 一甲子[i - 1].GetNext干支();
            }

            return 一甲子;
        }
    }
}
