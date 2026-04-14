using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_EPM_Equipment_Allocation
{
  public class MoveOrder
    {

        private string workOder;
        public string OrgName { get; set; }
        public string DeviceName { get; set; }

        public string DeviceNo { get; set; }
        public string Snid { get; set; }
        public string DeviceType { get; set; }

        public string AddressCode { get; set; }
        public string CurPosition { get; set; }

        public string NewPosition { get; set; }
        public string MoveDate { get; set; }
        public bool IsCheck { get; set; }

        public string getWorkOrder(int i) {
            if (workOder==null || workOder.Length==0) {
                string yy = DateTime.Now.ToString("yyyy").Substring(2, 2);
                workOder = "TP" + yy + DateTime.Now.ToString("MMddHHmmss") + i.ToString("000");
            }
            return workOder;
        }
    }
}
