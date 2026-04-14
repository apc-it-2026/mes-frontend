using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_BarcodePrinting_WakeOrder
{
    public partial class WakeOrder_QrCodePrint : MaterialForm
    {

        public WakeOrder_QrCodePrint(DataTable dt, string path)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            FastReportHelper.LoadFastReport(panel1, path, dic, dt, "Table");
        }
           
    }
}
