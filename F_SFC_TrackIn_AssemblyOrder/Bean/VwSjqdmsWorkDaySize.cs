using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionInput.Bean
{
    public class VwSjqdmsWorkDaySize
    {
        public int ORG_ID { get; set; }
        public string SE_ID { get; set; }
        public int SE_SEQ { get; set; }
        public int  STATUS { get; set; }
        public string WORK_DAY { get; set; }
        public string D_DEPT { get; set; }
        public decimal WORK_QTY { get; set; }
        public string SIZE_NO { get; set; }
        public int SIZE_SEQ { get; set; }
        public decimal FINISH_QTY { get; set; }
        public decimal SUPPLEMENT_QTY { get; set; }
        public string INOUT_PZ { get; set; }
        public decimal SE_QTY { get; set; }
        public string ART_NO { get; set; }
        public string PO { get; set; }
        public string SE_DAY { get; set; }
        public string PRODUCTION_ORDER { get; set; }
        public string MAIN_PROD_ORDER { get; set; }
        public string MATERIAL_NO { get; set; }
        public string BATCH_NO { get; set; }
    }
}
