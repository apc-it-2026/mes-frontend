using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.CommonForm
{
    public partial class frmDBSetting : Form
    {
        public frmDBSetting(string Server,string Name,string User,string Pwd)
        {
            InitializeComponent();
            textBox1.Text = Server;
            textBox2.Text = Name;
            textBox3.Text = User;
            textBox4.Text = Pwd;
        }

        private void btn_Count_Click(object sender, EventArgs e)
        {
            try
            {

                try
                {
                    SJeMES_Framework.DBHelper.DataBase db = new SJeMES_Framework.DBHelper.DataBase(
                    "Sqlserver", textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, string.Empty);
                    string tmp = db.TimeHMS;
                }
                catch { MessageBox.Show("数据库连接失败"); return; }

             

                string Config = @"
<Config>
  <DBServer>" + textBox1.Text + @"</DBServer>
  <DBName>" + textBox2.Text + @"</DBName>
  <DBUser>" + textBox3.Text + @"</DBUser>
  <DBPwd>" + textBox4.Text + @"</DBPwd>
</Config>
";
                System.IO.File.Delete(Application.StartupPath + @"\DBConfig.xml");
                SJeMES_Framework.Common.TXTHelper.WriteToEnd(Application.StartupPath + @"\DBConfig.xml", Config);
                MessageBox.Show("设置成功");
                Application.Restart();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
