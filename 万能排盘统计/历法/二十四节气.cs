using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 历法
{
    /// <summary>
    /// Should NEVER change the orders
    /// </summary>
    public enum 二十四节气
    {
        小寒 = 1, //starts from 阳历年 first 节气 is 小寒 around 1/6
        大寒 = 2,
        立春 = 3, //寅月要从立春起
        雨水 = 4,
        惊蛰 = 5,
        春分 = 6, 
        清明 = 7,
        谷雨 = 8, 
        立夏 = 9,
        小满 = 10,
        芒种 = 11,
        夏至 = 12,
        小暑 = 13,
        大暑 = 14,
        立秋 = 15, 
        处暑 = 16, 
        白露 = 17, 
        秋分 = 18,
        寒露 = 19, 
        霜降 = 20, 
        立冬 = 21, 
        小雪 = 22, 
        大雪 = 23, 
        冬至 = 24
    }
}
