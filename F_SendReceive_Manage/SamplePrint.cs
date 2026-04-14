using F_Sample_SendReceive_Manage;
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

namespace F_SendReceive_Manage
{
    public partial class SamplePrint : MaterialForm
    {
        public SamplePrint(DataTable dt,string path)
        {
            InitializeComponent();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            FastReportHelper.LoadFastReport(panel1, path, dic, dt, "Table");
        }
        public SamplePrint(DataTable dt, string path,Dictionary<string,string> dic)
        {
            InitializeComponent();
           // Dictionary<string, string> dic = new Dictionary<string, string>();
            FastReportHelper.LoadFastReport(panel1, path, dic, dt, "Table");
        }
    }
}
