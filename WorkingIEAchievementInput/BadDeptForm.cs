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

namespace WorkingIEAchievementInput
{
    public partial class BadDeptForm : MaterialForm
    {
        public BadDeptForm(DataTable dt)
        {
            InitializeComponent();
            dataGridView3.DataSource = dt;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
        }

  
        private void button4_Click(object sender, EventArgs e)
        {
            NewExportExcels.ExportExcels.Export("异常部门", dataGridView3);
        }
    }
}
