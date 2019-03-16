using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 历法;

namespace 子平八字
{
    public class 八字命盘
    {
        public 干支 年柱 { get; set; }
        public 干支 月柱 { get; set; }
        public 干支 日柱 { get; set; }
        public 干支 时柱 { get; set; }

        public 命造性别? 性别 { get; set; }
        public DateTime 公历日期 { get; set; }

        public override string ToString()
        {
            if (this != null)
            {
                return $"年柱：{年柱.Name}； 月柱：{月柱.Name}； 日柱：{日柱.Name}； 时柱：{时柱.Name}； ";
            }

            return null;
        }
    }

    public enum 命造性别
    {
        坤造, //女
        乾造 //男
    }
}
