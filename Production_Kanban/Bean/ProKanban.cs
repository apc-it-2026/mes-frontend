using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production_Kanban.Bean
{
    public class ProKanban
    {

        public string Title { get; set; }
        public decimal Target { get; set; }//今日目标产量
        public decimal SumQty { get; set; }//今日累计产量
        public decimal HourTargetQty { get; set; }//每小时目标
        public decimal HourQty { get; set; }//当前时产量
        public double BA { get; set; }
        public decimal Ration { get; set; }//达成率
        public decimal PresentQty { get; set; }//当前目标产量
    }

}
