using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 历法
{
    public enum 天干
    {
        甲 = 1,
        乙 = 2,
        丙 = 3,
        丁 = 4,
        戊 = 5,
        己 = 6,
        庚 = 7,
        辛 = 8,
        壬 = 9,
        癸 = 10
    }

    public enum 地支
    {
        子 = 1,
        丑 = 2,
        寅 = 3,
        卯 = 4,
        辰 = 5,
        巳 = 6,
        午 = 7,
        未 = 8,
        申 = 9,
        酉 = 10,
        戌 = 11,
        亥 = 12
    }

    public struct 干支
    {
        public 天干 天干 { get; set; }
        public 地支 地支 { get; set; }

        public string Name
        {
            get
            {
                return $"{天干}{地支}";
            }
        }
    }
}
