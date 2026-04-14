using System.Collections.Generic;
using System.Data;
using MaterialSkin.Controls;

namespace F_MMS_InOut_BillAddOrder
{
    public partial class InOutBillAddOrder_MMSPrint : MaterialForm
    {

        public InOutBillAddOrder_MMSPrint(DataTable dt, string path)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            FastReportHelper.LoadFastReport(panel1, path, dic, dt, "Table");
        }
           
    }
}
