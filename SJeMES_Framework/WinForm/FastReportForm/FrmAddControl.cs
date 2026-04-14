using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SJeMES_Framework.DBHelper;
using System.Threading;

namespace SJeMES_Framework.WinForm.FastReportForm
{
    public partial class FrmAddControl : Form
    {
        public string title;
        public string ctrType;
        public string selSQL;
        private Dictionary<string, string> dic;

        private TextBox txt;


        private DateTimePicker dtp;

        private GroupBox gb;
        private ComboBox cbo;
        private DataBase DB;

        public FrmAddControl()
        {
            InitializeComponent();
            textBox2.Enabled = false;
        }

        public FrmAddControl(TextBox txt, GroupBox gb, DBHelper.DataBase DB)
        {
            this.txt = txt;
            this.gb = gb;
            this.DB = DB;

            InitializeComponent();
            button2.Enabled = false;
            cbo_Title.Items.Clear();
            cbo_Title.Items.Add(txt.Name);

            cbo_Title.Text = txt.Name;
            textBox2.Text = txt.Tag.ToString();
            comboBox1.Text = "文本框";

        }
        public FrmAddControl(DateTimePicker dtp, GroupBox gb, DBHelper.DataBase DB)
        {
            this.dtp = dtp;
            this.gb = gb;
            this.DB = DB;

            InitializeComponent();
            button2.Enabled = false;
            cbo_Title.Items.Clear();
            cbo_Title.Items.Add(dtp.Name);
            cbo_Title.Text = dtp.Name;
            comboBox1.Text = "日期框";

        }
        public FrmAddControl(ComboBox cbo, GroupBox gb, DBHelper.DataBase DB)
        {
            this.cbo = cbo;
            this.gb = gb;
            this.DB = DB;

            InitializeComponent();
            button2.Enabled = false;
            cbo_Title.Items.Clear();
            cbo_Title.Items.Add(cbo.Name);
            cbo_Title.Text = cbo.Name;
            comboBox1.Text = "下拉框";
            textBox2.Text = cbo.Tag.ToString();

        }

        public FrmAddControl(GroupBox gb, DBHelper.DataBase DB)
        {
            this.gb = gb;
            this.DB = DB;
           
            InitializeComponent();
       
        }

        private void FrmAddControl_Load(object sender, EventArgs e)
        {
            LoadDataSource();
        }
        /// <summary>
        /// 加载数据源列表
        /// </summary>
        private void LoadDataSource()
        {
            cbo_DataSource.Items.Clear();
            dic = new Dictionary<string, string>();
            foreach (Control ctr in gb.Controls)
            {
                if (ctr is TabControl)
                {
                    TabControl tab = ctr as TabControl;
                    foreach (Control ctrPage in tab.Controls)
                    {
                        if (ctrPage is TabPage)
                        {
                            if (ctrPage.Text != "查询控件")
                            {
                                cbo_DataSource.Items.Add(ctrPage.Text);
                                TabPage tPage = ctrPage as TabPage;
                                foreach (Control cc in tPage.Controls)
                                {
                                    if (cc is Panel)
                                    {
                                        Panel p = cc as Panel;
                                        foreach (Control t in p.Controls)
                                        {

                                            if (t is TextBox)
                                            {
                                                dic.Add(tPage.Text, t.Text);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbo_Title.Text))
            {
                MessageBox.Show("请选择标题");
                return;
            }
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("请选择控件类型");
                return;
            }

            title = cbo_Title.Text;
            ctrType = comboBox1.Text;
            selSQL = textBox2.Text;
            this.Close();
            this.Dispose();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "文本框")
            {

                textBox2.Enabled = true;
            }
            else if(comboBox1.Text == "下拉框")
            {
                label3.Text = "下拉项";
                textBox2.Enabled = true;
            }
            else
            {
                textBox2.Text = "";
                textBox2.Enabled = false;
            }
        }

        private void cbo_DataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dic.ContainsKey(cbo_DataSource.Text))
            {
                Thread thread = new Thread(new ThreadStart(SetTitle));
                thread.IsBackground = true;
                thread.Start();
            }
           
            
        }

        private void SetTitle()
        {
            cbo_Title.Text = string.Empty; ;
            cbo_Title.Items.Clear();
            DataTable tab = new DataTable();
            if (DB != null)
            {
                tab = DB.GetDataTable(dic[cbo_DataSource.Text]);
            }
            else
            {
                Dictionary<string, string> pp = new Dictionary<string, string>();
                pp.Add("sql","SELECT TOP 1 * fROM("+ dic[cbo_DataSource.Text]+")tab");
                string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.Org, Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", pp);
                string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                tab = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
            }
            for (int i = 0; i < tab.Columns.Count; i++)
            {
                cbo_Title.Items.Add(tab.Columns[i].ColumnName);
            }
        }
    }
}
