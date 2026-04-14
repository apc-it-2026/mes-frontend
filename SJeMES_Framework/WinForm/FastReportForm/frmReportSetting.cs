using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.FastReportForm
{
    public partial class frmReportSetting : Form
    {
        private string reportCode;
        private string moduleNo;
        public bool isTrue = false;
        DBHelper.DataBase DB;
   


       
        public frmReportSetting(string reportCode,string moduleNo)
        {
            this.reportCode = reportCode;
            this.moduleNo = moduleNo;
            InitializeComponent();
            LoadData();
        }
  
        public frmReportSetting(DBHelper.DataBase DB, string reportCode, string moduleNo)
        {
            this.DB = DB;
            this.reportCode = reportCode;
            this.moduleNo = moduleNo;
            InitializeComponent();
            LoadData();
        }
       
     

        /// <summary>
        /// 新增加载控件
        /// </summary>
        private void LoadDataSourceName()
        {
            groupBox2.Controls.Clear();
            string ReportStr = SJeMES_Framework.Common.TXTHelper.ReadToEnd(txt_ReportPath.Text);
            string dictionary = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(ReportStr, "<Dictionary>", "</Dictionary>");
            var lstDataSource = SJeMES_Framework.Common.StringHelper.GetDataFromTag(dictionary, "<TableDataSource", "</TableDataSource>");
            TabControl tab = new TabControl();
            tab.SelectedIndexChanged += Tab_SelectedIndexChanged;
            tab.Dock = DockStyle.Fill;
            foreach (var item in lstDataSource)
            {
                string newItem = string.Empty;
                if (item.IndexOf("Alias") > -1)
                {

                    newItem = item.Trim().Replace("Alias=\"", "-");
                    newItem = newItem.Split('-')[1];
                }

                else
                    newItem = item.Trim().Replace("Name=\"", "");
                TabPage page = new TabPage();
                page.Text = newItem.Substring(0, newItem.IndexOf("\""));

                Panel panel = new Panel();
                panel.Location = new Point(0, 0);
                panel.Size = new Size(783, 155);
                TextBox txt = new TextBox();
                txt.Multiline = true;
                txt.ScrollBars = ScrollBars.Both;
                txt.Dock = DockStyle.Fill;
                panel.Controls.Add(txt);
                page.Controls.Add(panel);


                panel = new Panel();
                panel.Location = new Point(0, 161);
                panel.Size = new Size(783, 282);

                DataGridView dgv = new DataGridView();
                dgv.Dock = DockStyle.Fill;
                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;
                dgv.RowHeadersVisible = false;
                panel.Controls.Add(dgv);
                page.Controls.Add(panel);

                tab.Controls.Add(page);
            }
            #region 查询控件
            TabPage tPage = new TabPage();
            tPage.Text = "查询控件";

            Panel panelCtr = new Panel();
            panelCtr.Location = new Point(0, 0);
            panelCtr.Size = new Size(783, 38);

            Button btn = new Button();
            btn.Location = new Point(18, 7);
            btn.Size = new Size(75, 23);
            btn.Text = "添加文本框";
            btn.Click += Btn_Click;
            panelCtr.Controls.Add(btn);


            btn = new Button();
            btn.Location = new Point(109, 7);
            btn.Size = new Size(75, 23);
            btn.Text = "清空控件";
            btn.Click += BtnClear_Click;
            panelCtr.Controls.Add(btn);
            tPage.Controls.Add(panelCtr);



            panelCtr = new Panel();
            panelCtr.Name = "QueryBox";
            panelCtr.Padding = new Padding(10, 10, 10, 10);
            panelCtr.Location = new Point(0, 45);
            panelCtr.Size = new Size(783, 352);
            tPage.Controls.Add(panelCtr);
            tab.Controls.Add(tPage);
            #endregion


            groupBox2.Controls.Add(tab);
        }

        /// <summary>
        /// 修改加载控件数据
        /// </summary>
        private void LoadData()
        {
            groupBox2.Controls.Clear();
            string sql = "select * from [SJEMSSYS].[dbo].[SYSREPORT01M](nolock) where 1=1";
            DataTable dt = new DataTable();
            if (DB != null)
            {


                if (!string.IsNullOrEmpty(reportCode))
                    sql = sql + " and ReportCode='" + reportCode + "'";

                dt = DB.GetDataTable(sql);

            }
            else
            {
                if (!string.IsNullOrEmpty(reportCode))
                    sql = sql + " and ReportCode='" + reportCode + "'";
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", "select * from [SJEMSSYS].[dbo].[SYSREPORT01M](nolock) where ReportCode='" + reportCode + "'");
                string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);
                string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
                dt = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
            }
           
        
            if (dt.Rows.Count > 0)
            {
                txt_ReportCode.ReadOnly = true;

                txt_ReportPath.Text = dt.Rows[0]["ReportPath"].ToString();
                txt_ReportName.Text = dt.Rows[0]["ReportName"].ToString();
                txt_ReportCode.Text = dt.Rows[0]["ReportCode"].ToString();
                string DataSources = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(dt.Rows[0]["ReportXML"].ToString(), "<DataSources>", "</DataSources>");
                txt_Primarykey.Text = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(DataSources, "<DataKey>", "</DataKey>");
                comboBox1.Text = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(DataSources, "<IsShow>", "</IsShow>");

                var lstDataSource = SJeMES_Framework.Common.StringHelper.GetDataFromTag(DataSources, "<DataSource>", "</DataSource>");
                TabControl tab = new TabControl();
                tab.SelectedIndexChanged += Tab_SelectedIndexChanged;
                tab.Dock = DockStyle.Fill;
                foreach (var item in lstDataSource)
                {

                    TabPage page = new TabPage();
                    page.Text = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<TableName>", "</TableName>");

                    Panel panel = new Panel();
                    panel.Location = new Point(0, 0);
                    panel.Size = new Size(783, 155);
                    TextBox txt = new TextBox();
                    txt.Multiline = true;
                    txt.ScrollBars = ScrollBars.Both;
                    txt.Text = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<SearchSQL>", "</SearchSQL>");
                    txt.Dock = DockStyle.Fill;
                    panel.Controls.Add(txt);
                    page.Controls.Add(panel);


                    panel = new Panel();
                    panel.Location = new Point(0, 161);
                    panel.Size = new Size(783, 282);

                    DataGridView dgv = new DataGridView();
                    dgv.Dock = DockStyle.Fill;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.RowHeadersVisible = false;
                    panel.Controls.Add(dgv);
                    page.Controls.Add(panel);

                    tab.Controls.Add(page);
                }
                #region 查询控件
                TabPage tPage = new TabPage();
                tPage.Text = "查询控件";

                Panel panelCtr = new Panel();
                panelCtr.Location = new Point(0, 0);
                panelCtr.Size = new Size(783, 38);

                Button btn = new Button();
                btn.Location = new Point(18, 7);
                btn.Size = new Size(75, 23);
                btn.Text = "添加文本框";
                btn.Click += Btn_Click;
                panelCtr.Controls.Add(btn);


                btn = new Button();
                btn.Location = new Point(109, 7);
                btn.Size = new Size(75, 23);
                btn.Text = "清空控件";
                btn.Click += BtnClear_Click;
                panelCtr.Controls.Add(btn);
                tPage.Controls.Add(panelCtr);


                string ReportQueryBox = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(dt.Rows[0]["ReportXML"].ToString(), "<ReportQueryBox>", "</ReportQueryBox>");
                var lstQueryBox = SJeMES_Framework.Common.StringHelper.GetDataFromTag(ReportQueryBox, "<QueryBox>", "</QueryBox>");
                panelCtr = new Panel();
                panelCtr.Name = "QueryBox";
                panelCtr.Padding = new Padding(10, 10, 10, 10);
                panelCtr.Location = new Point(0, 45);
                panelCtr.Size = new Size(783, 352);

                int i = 0;
                int n = 0;
                foreach (var item in lstQueryBox)
                {
                    i++;
                    n++;
                    if (n > 6)
                        n = 1;
                    ctrX = 120 * (n - 1) + 10;
                    if (i > ctrCount)
                    {

                        n = 1;
                        ctrCount += 6;
                        ctrY += 40;
                        ctrX = 10;
                    }
                    if (SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<DataType>", "</DataType>").ToLower() == "string")
                    {
                        TextBox txt = new TextBox();
                        txt.Location = new Point(ctrX, ctrY);
                        txt.Size = new Size(100, 21);
                        txt.ReadOnly = true;
                        txt.MouseDoubleClick += Txt_MouseDoubleClick;
                        txt.Text = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<Title>", "</Title>");
                        txt.Name = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<Title>", "</Title>");
                        txt.Tag = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<DataSelectSQL>", "</DataSelectSQL>");
                        panelCtr.Controls.Add(txt);
                    }
                    else if(SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<DataType>", "</DataType>").ToLower() == "date")
                    {
                        DateTimePicker dtp = new DateTimePicker();
                        dtp.Location = new Point(ctrX, ctrY);
                        dtp.Size = new Size(100, 21);
                        dtp.MouseUp += Dtp_MouseUp;
                        dtp.Format = DateTimePickerFormat.Custom;
                        dtp.CustomFormat = "yyyy-MM-dd";
                        dtp.Name = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<Title>", "</Title>");
                        panelCtr.Controls.Add(dtp);
                    }else
                    {
                        ComboBox cbo = new ComboBox();

                        cbo.Location = new Point(ctrX, ctrY);
                        cbo.Size = new Size(100, 21);
                 
                        cbo.MouseClick += cbo_MouseClick;
                        cbo.Text = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<Title>", "</Title>");
                        cbo.Name = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<Title>", "</Title>");
                        cbo.Tag = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<DataSelectSQL>", "</DataSelectSQL>");
                        panelCtr.Controls.Add(cbo);
                    }


                }

                tPage.Controls.Add(panelCtr);
                tab.Controls.Add(tPage);
                #endregion
                groupBox2.Controls.Add(tab);
            }





        }
        private void Tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = sender as TabControl;
            if (tab.SelectedTab.Text == "查询控件")
                btn_TestSQL.Enabled = false;
            else
                btn_TestSQL.Enabled = true;
        }

        int ctrX = 10;
        int ctrY = 10;
        int ctrCount = 6;
        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Click(object sender, EventArgs e)
        {
            FrmAddControl frm = new FrmAddControl(groupBox2, DB);
            frm.ShowDialog();
            string title = frm.title;
            string ctrType = frm.ctrType;
            string selSQL = frm.selSQL;
            int i = 1;
            int n = 1;
            foreach (Control ctr in groupBox2.Controls)
            {
                if (ctr is TabControl)
                {
                    TabControl tab = ctr as TabControl;
                    foreach (Control ctrPage in tab.Controls)
                    {
                        if (ctrPage is TabPage)
                        {
                            if (ctrPage.Text == "查询控件")
                            {
                                foreach (Control panCtr in ctrPage.Controls)
                                {
                                    if (panCtr is Panel)
                                    {
                                        if (panCtr.Name == "QueryBox")
                                        {
                                            Panel pan = panCtr as Panel;
                                            foreach (Control ctrText in pan.Controls)
                                            {
                                                if (ctrText is TextBox || ctrText is DateTimePicker)
                                                {
                                                    i++;
                                                    n++;
                                                    if (n > 6)
                                                        n = 1;
                                                    ctrX = 120 * (n - 1) + 10;//调整控件的位置
                                                    if (i > ctrCount)
                                                    {

                                                        n = 1;
                                                        ctrCount += 6;
                                                        ctrY += 40;
                                                        ctrX = 10;
                                                    }

                                                }
                                            }
                                            if (ctrType == "文本框")
                                            {
                                                TextBox txt = new TextBox();
                                                txt.Location = new Point(ctrX, ctrY);
                                                txt.Size = new Size(100, 21);
                                                txt.MouseDoubleClick += Txt_MouseDoubleClick;
                                                txt.ReadOnly = true;
                                                txt.Tag = selSQL;
                                                txt.Name = title;
                                                txt.Text = title;
                                                pan.Controls.Add(txt);
                                            }
                                            else if (ctrType == "日期框")
                                            {
                                                DateTimePicker dtp = new DateTimePicker();
                                                dtp.Location = new Point(ctrX, ctrY);
                                                dtp.MouseUp += Dtp_MouseUp;
                                                dtp.Size = new Size(100, 21);
                                                dtp.Format = DateTimePickerFormat.Custom;
                                                dtp.CustomFormat = "yyyy-MM-dd";
                                                dtp.Name = title;
                                                pan.Controls.Add(dtp);
                                            }
                                            else if (ctrType == "下拉框")
                                            {
                                                ComboBox cbo = new ComboBox();
                                                cbo.Location = new Point(ctrX, ctrY);
                                                cbo.DropDownStyle = ComboBoxStyle.DropDownList;
                                                cbo.MouseClick += cbo_MouseClick;
                                                cbo.Size = new Size(100, 21);
                                                cbo.Tag = selSQL;
                                                cbo.Name = title;
                                                cbo.Text = title;
                                                pan.Controls.Add(cbo);
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

        private void Dtp_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {

                DateTimePicker dtp = sender as DateTimePicker;
                var parent = dtp.Parent;
                FrmAddControl frm = new FrmAddControl(dtp,groupBox2,DB);
                frm.ShowDialog();
                if (frm.ctrType == "文本框")
                {
                    TextBox textBox = new TextBox();
                    textBox.Location = dtp.Location;
                    textBox.Size = dtp.Size;
                    textBox.Text = frm.title;
                    textBox.Name = frm.title;
                    textBox.Tag = frm.selSQL;
                    textBox.MouseDoubleClick += Txt_MouseDoubleClick;
                    parent.Controls.Remove(dtp);
                    parent.Controls.Add(textBox);
                }
                else if (frm.ctrType == "日期框")
                {
                    DateTimePicker newdtp = new DateTimePicker();
                    newdtp.Location = dtp.Location;
                    newdtp.Size = dtp.Size;
                    newdtp.Name = frm.title;
                    newdtp.Tag = frm.selSQL;
                    newdtp.Format = DateTimePickerFormat.Custom;
                    newdtp.CustomFormat = "yyyy-MM-dd";
                    newdtp.MouseUp += Dtp_MouseUp;
                    parent.Controls.Remove(dtp);
                    parent.Controls.Add(dtp);
                }
                else if (frm.ctrType == "下拉框")
                {
                    ComboBox cbo = new ComboBox();
                    cbo.Location = dtp.Location;
                    cbo.Size = dtp.Size;
                    cbo.Text = frm.title;
                    cbo.Name = frm.title;
                    cbo.Tag = frm.selSQL;
                    cbo.MouseClick += cbo_MouseClick;
                    parent.Controls.Remove(dtp);
                    parent.Controls.Add(cbo);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void Txt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {

                TextBox txt = sender as TextBox;
                var parent = txt.Parent;
                FrmAddControl frm = new FrmAddControl(txt, groupBox2, DB);
                frm.ShowDialog();

                if (frm.ctrType == "文本框")
                {
                    TextBox textBox = new TextBox();
                    textBox.Location = txt.Location;
                    textBox.Size = txt.Size;
                    textBox.Text = frm.title;
                    textBox.Name = frm.title;
                    textBox.Tag = frm.selSQL;
                    textBox.MouseDoubleClick += Txt_MouseDoubleClick;
                    parent.Controls.Remove(txt);
                    parent.Controls.Add(textBox);
                }
                else if (frm.ctrType == "日期框")
                {
                    DateTimePicker dtp = new DateTimePicker();
                    dtp.Location = txt.Location;
                    dtp.Size = txt.Size;
                    dtp.Name = frm.title;
                    dtp.Tag = frm.selSQL;
                    dtp.Format = DateTimePickerFormat.Custom;
                    dtp.CustomFormat = "yyyy-MM-dd";
                    dtp.MouseUp += Dtp_MouseUp;
                    parent.Controls.Remove(txt);
                    parent.Controls.Add(dtp);
                }else if (frm.ctrType == "下拉框")
                {
                    ComboBox cbo = new ComboBox();
                    cbo.Location = txt.Location;
                    cbo.Size = txt.Size;
                    cbo.Text = frm.title;
                    cbo.Name = frm.title;
                    cbo.Tag = frm.selSQL;
                    cbo.MouseClick += cbo_MouseClick;
                    parent.Controls.Remove(txt);
                    parent.Controls.Add(cbo);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void cbo_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                ComboBox cbo= sender as ComboBox;
                var parent = cbo.Parent;
                FrmAddControl frm = new FrmAddControl(cbo, groupBox2, DB);
                frm.ShowDialog();
                if (frm.ctrType == "文本框")
                {
                    TextBox textBox = new TextBox();
                    textBox.Location = cbo.Location;
                    textBox.Size = cbo.Size;
                    textBox.Text = frm.title;
                    textBox.Name = frm.title;
                    textBox.Tag = frm.selSQL;
                    textBox.MouseDoubleClick += Txt_MouseDoubleClick;
                    parent.Controls.Remove(cbo);
                    parent.Controls.Add(textBox);
                }
                else if (frm.ctrType == "日期框")
                {
                    DateTimePicker newdtp = new DateTimePicker();
                    newdtp.Location = cbo.Location;
                    newdtp.Size = cbo.Size;
                    newdtp.Name = frm.title;
                    newdtp.Tag = frm.selSQL;
                    newdtp.Format = DateTimePickerFormat.Custom;
                    newdtp.CustomFormat = "yyyy-MM-dd";
                    newdtp.MouseUp += Dtp_MouseUp;
                    parent.Controls.Remove(cbo);
                    parent.Controls.Add(newdtp);
                }
                else if (frm.ctrType == "下拉框")
                {
                    ComboBox newcbo = new ComboBox();
                    newcbo.Location = cbo.Location;
                    newcbo.Size = cbo.Size;
                    newcbo.Text = frm.title;
                    newcbo.Name = frm.title;
                    newcbo.Tag = frm.selSQL;
                    newcbo.MouseClick += cbo_MouseClick;
                    parent.Controls.Remove(cbo);
                    parent.Controls.Add(newcbo);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }
        }

    

        /// <summary>
        /// 清空控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            ctrX = 10;
            ctrY = 10;
            ctrCount = 6;
            foreach (Control ctr in groupBox2.Controls)
            {
                if (ctr is TabControl)
                {
                    TabControl tab = ctr as TabControl;
                    foreach (Control ctrPage in tab.Controls)
                    {
                        if (ctrPage is TabPage)
                        {
                            if (ctrPage.Text == "查询控件")
                            {
                                foreach (Control panCtr in ctrPage.Controls)
                                {
                                    if (panCtr is Panel)
                                    {
                                        if (panCtr.Name == "QueryBox")
                                        {
                                            Panel pan = panCtr as Panel;
                                            pan.Controls.Clear();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 选择报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SelectReport_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择FastReport文件";
            fileDialog.Filter = "FastReport文件(*.frx)|*.frx";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txt_ReportPath.Text = fileDialog.FileName;
                LoadDataSourceName();
                ctrX = 10;
                ctrY = 10;
                ctrCount = 6;
            }


        }
        /// <summary>
        /// 测试SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_TestSQL_Click(object sender, EventArgs e)
        {
            try
            {
                var tabData = new DataTable();
                foreach (Control ctr in groupBox2.Controls)
                {
                    if (ctr is TabControl)
                    {
                        TabControl tab = ctr as TabControl;

                        foreach (Control c in tab.Controls)
                        {
                            if (c is TabPage)
                            {
                                if (c.TabIndex == tab.SelectedIndex)
                                {
                                    TabPage tPage = c as TabPage;
                                    foreach (Control cc in tPage.Controls)
                                    {
                                        if (cc is Panel)
                                        {
                                            Panel p = cc as Panel;

                                            foreach (Control t in p.Controls)
                                            {

                                                if (t is TextBox)
                                                {
                                                    if (DB != null)
                                                    {
                                                        tabData = DB.GetDataTable(t.Text);
                                                    }
                                                    else
                                                    {
                                                        Dictionary<string, string> pp = new Dictionary<string, string>();
                                                        pp.Add("sql", t.Text);
                                                        string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.Org, Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", pp);
                                                        string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");

                                                        tabData = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                                                    }
                                                  
                                                }
                                                else if (t is DataGridView)
                                                {
                                                    DataGridView dgv = t as DataGridView;
                                                    dgv.DataSource = tabData;
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
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }

        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_ReportPath.Text))
                {
                    MessageBox.Show("请选择报表文件！");
                    return;
                }
                if (string.IsNullOrEmpty(txt_ReportCode.Text))
                {
                    MessageBox.Show("请输入报表代号！");
                    txt_ReportCode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txt_ReportName.Text))
                {
                    MessageBox.Show("请输入报表名称！");
                    txt_ReportName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(comboBox1.Text))
                {
                    MessageBox.Show("请选择开始显示数据！");
                    return;
                }
                string ReportQueryBox = string.Empty;
                string fastReportXML = "<FastReport>";
                fastReportXML += "<Title>" + txt_ReportName.Text + "</Title>";
                fastReportXML += " <DataSources>";
                fastReportXML += "<DataKey>" + txt_Primarykey.Text + "</DataKey>";
                fastReportXML += "<IsShow>" + comboBox1.Text + "</IsShow>";
                foreach (Control ctr in groupBox2.Controls)
                {
                    if (ctr is TabControl)
                    {
                        TabControl tc = ctr as TabControl;
                        foreach (Control tp in tc.Controls)//循环TabControl
                        {
                            if (tp is TabPage)
                            {
                                TabPage tabPage = tp as TabPage;
                                if (tabPage.Text == "查询控件")
                                {

                                    foreach (Control p in tabPage.Controls)
                                    {
                                        if (p.Name == "QueryBox")
                                        {
                                            Panel pan = p as Panel;
                                            foreach (Control txt in pan.Controls)
                                            {
                                                if (txt is TextBox)
                                                {
                                                    ReportQueryBox += "<QueryBox>";
                                                    ReportQueryBox += "<Title>" + txt.Name + "</Title>";
                                                    ReportQueryBox += "<DataType>String</DataType>";
                                                    ReportQueryBox += "<DataSelectSQL>" + txt.Tag + "</DataSelectSQL>";
                                                    ReportQueryBox += "</QueryBox>";
                                                }
                                                else if (txt is DateTimePicker)
                                                {
                                                    ReportQueryBox += "<QueryBox>";
                                                    ReportQueryBox += "<Title>" + txt.Name + "</Title>";
                                                    ReportQueryBox += "<DataType>Date</DataType>";
                                                    ReportQueryBox += "</QueryBox>";
                                                }else if(txt is ComboBox)
                                                {
                                                    ReportQueryBox += "<QueryBox>";
                                                    ReportQueryBox += "<Title>" + txt.Name + "</Title>";
                                                    ReportQueryBox += "<DataType>ComboBox</DataType>";
                                                    ReportQueryBox += "<DataSelectSQL>" + txt.Tag + "</DataSelectSQL>";
                                                    ReportQueryBox += "</QueryBox>";
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {

                                    foreach (Control p in tabPage.Controls)//循环TabPage
                                    {
                                        if (p is Panel)
                                        {
                                            Panel panel = p as Panel;
                                            foreach (Control t in panel.Controls)//循环Panel
                                            {
                                                if (t is TextBox)
                                                {
                                                    if (string.IsNullOrEmpty(t.Text.Trim()))
                                                    {
                                                        MessageBox.Show("请输入SQL语句！");
                                                        return;
                                                    }
                                                    fastReportXML += "<DataSource>";
                                                    fastReportXML += "<TableName>" + tabPage.Text + "</TableName>";
                                                    fastReportXML += "<SearchSQL>" + t.Text + "</SearchSQL>";
                                                    fastReportXML += "</DataSource>";
                                                }
                                            }
                                        }
                                    }

                                }

                            }

                        }
                    }
                }
                fastReportXML += "</DataSources>";
                fastReportXML += "<ReportQueryBox>" + ReportQueryBox + "</ReportQueryBox>";
                fastReportXML += "</FastReport>";

                #region 
                string sql = "select top 1 * from [SJEMSSYS].[dbo].[SYSREPORT01M](nolock) where ReportCode='" + txt_ReportCode.Text.Trim() + "'";
                if (string.IsNullOrEmpty(reportCode))
                {
                    DataTable tabData = new DataTable();
                    if (DB != null)
                    {
                        tabData = DB.GetDataTable(sql);

                    }else
                    {
                        Dictionary<string, string> pp = new Dictionary<string, string>();
                        pp.Add("sql", sql);
                        string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", pp);
                        string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
                        tabData = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                    }
                   
                    if (tabData.Rows.Count > 0)
                    {
                        MessageBox.Show(txt_ReportCode.Text.Trim() + "已存在,保存失败！");
                        return;
                    }

                    string reportStr = SJeMES_Framework.Common.TXTHelper.ReadToEnd(txt_ReportPath.Text);
                    sql = "insert into [SJEMSSYS].[dbo].[SYSREPORT01M](ReportCode,ReportName,ReportPath,ReportXML,ReportString,ModuleNo)values" +
                       "('" + txt_ReportCode.Text.Trim() + "','" + txt_ReportName.Text.Trim() + "','" + txt_ReportPath.Text + "','" + fastReportXML.Replace("'", "''") + "','"+ reportStr.Replace("'","''")+"','"+moduleNo+"')";
                    bool b = false;
                    if (DB != null)
                    {
                       int n= DB.ExecuteNonQueryOffline(sql);
                        if (n > 0)
                            b = true;
                    }else
                    {
                        Dictionary<string, string> pp = new Dictionary<string, string>();
                        pp.Add("sql", sql);
                        string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "ExecuteNonQuery", pp);
                        b = Convert.ToBoolean(SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>"));
                    }
                    if (b)
                    {
                        isTrue = true;
                        MessageBox.Show("保存成功！");
                        this.Close();
                    }

                    else
                        MessageBox.Show("保存失败！");
                }
                else
                {
                    string reportStr = SJeMES_Framework.Common.TXTHelper.ReadToEnd(txt_ReportPath.Text);
                    sql = "Update [SJEMSSYS].[dbo].[SYSREPORT01M] set ReportCode='" + txt_ReportCode.Text.Trim() + "' , ReportName='" + txt_ReportName.Text.Trim() +
                        "',ReportPath='" + txt_ReportPath.Text + "',ReportXML='" + fastReportXML.Replace("'", "''") + "',ReportString='"+ reportStr.Replace("'","''")+"' where ReportCode='" + reportCode + "'";
                    bool b = false;
                    if (DB != null)
                    {
                        int n = DB.ExecuteNonQueryOffline(sql);
                        if (n > 0)
                            b = true;
                    }
                    else
                    {
                        Dictionary<string, string> pp = new Dictionary<string, string>();
                        pp.Add("sql", sql);
                        string XML = SJeMES_Framework.Common.WebServiceHelper.RunService( Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "ExecuteNonQuery", pp);
                        b = Convert.ToBoolean(SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>"));
                    }
                   
                    if (b)
                    {
                        isTrue = true;
                        MessageBox.Show("修改成功！");
                        this.Close();
                    }
                    else
                        MessageBox.Show("修改失败！");
                }

                #endregion


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        /// 预览报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pre_Click(object sender, EventArgs e)
        {
            
            string sql = "select ReportPath,ReportXML,ReportString from [SJEMSSYS].[dbo].[SYSREPORT01M](nolock) where ReportCode='" + txt_ReportCode.Text + "'";
            string xml = string.Empty;
            string reportPath = string.Empty;
            string connectionString = string.Empty;
            string reportString = string.Empty;
            var tab = new DataTable();
            if (DB != null)
            {
                try
                {
                    connectionString = DB.ConnectionText;
                    tab = DB.GetDataTable(sql);
                    if (tab != null && tab.Rows.Count > 0)
                        reportString = tab.Rows[0]["ReportString"].ToString();
                }
                catch (Exception)
                {
                }
               
            }
            else
            {
                try
                {
                    Dictionary<string, string> pp = new Dictionary<string, string>();
                    pp.Add("sql", sql);
                    string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", pp);
                    string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
                    tab = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                    connectionString = @"Data Source=" + Program.Org.DBServer + @";AttachDbFilename=;Initial Catalog=" + Program.Org.DBName + @";
                    Integrated Security=False;Persist Security Info=False;User ID=" + Program.Org.DBUser + ";Password=" + Program.Org.DBPassword;
                    if (tab != null && tab.Rows.Count > 0)
                        reportString = tab.Rows[0]["ReportString"].ToString();
                }
                catch (Exception)
                {
                }
             
            }
           
           
            if (string.IsNullOrEmpty(xml))
            {
                string ReportQueryBox = string.Empty;
                string fastReportXML = "<FastReport>";
                fastReportXML += "<Title>" + txt_ReportName.Text + "</Title>";
                fastReportXML += " <DataSources>";
                fastReportXML += "<DataKey>" + txt_Primarykey.Text + "</DataKey>";
                fastReportXML += "<IsShow>" + comboBox1.Text + "</IsShow>";
                foreach (Control ctr in groupBox2.Controls)
                {
                    if (ctr is TabControl)
                    {
                        TabControl tc = ctr as TabControl;
                        foreach (Control tp in tc.Controls)//循环TabControl
                        {
                            if (tp is TabPage)
                            {
                                TabPage tabPage = tp as TabPage;
                                if (tabPage.Text == "查询控件")
                                {

                                    foreach (Control p in tabPage.Controls)
                                    {
                                        if (p.Name == "QueryBox")
                                        {
                                            Panel pan = p as Panel;
                                            foreach (Control txt in pan.Controls)
                                            {
                                                if (txt is TextBox)
                                                {
                                                    ReportQueryBox += "<QueryBox>";
                                                    ReportQueryBox += "<Title>" + txt.Name + "</Title>";
                                                    ReportQueryBox += "<DataType>String</DataType>";
                                                    ReportQueryBox += "<DataSelectSQL>" + txt.Tag + "</DataSelectSQL>";
                                                    ReportQueryBox += "</QueryBox>";
                                                }
                                                else if (txt is DateTimePicker)
                                                {
                                                    ReportQueryBox += "<QueryBox>";
                                                    ReportQueryBox += "<Title>" + txt.Name + "</Title>";
                                                    ReportQueryBox += "<DataType>Date</DataType>";
                                                    ReportQueryBox += "</QueryBox>";
                                                }else if(txt is ComboBox)
                                                {
                                                    ReportQueryBox += "<QueryBox>";
                                                    ReportQueryBox += "<Title>" + txt.Name + "</Title>";
                                                    ReportQueryBox += "<DataType>ComboBox</DataType>";
                                                    ReportQueryBox += "<DataSelectSQL>" + txt.Tag + "</DataSelectSQL>";
                                                    ReportQueryBox += "</QueryBox>";
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {

                                    foreach (Control p in tabPage.Controls)//循环TabPage
                                    {
                                        if (p is Panel)
                                        {
                                            Panel panel = p as Panel;
                                            foreach (Control t in panel.Controls)//循环Panel
                                            {
                                                if (t is TextBox)
                                                {
                                                    if (string.IsNullOrEmpty(t.Text.Trim()))
                                                    {
                                                        MessageBox.Show("请输入SQL语句！");
                                                        return;
                                                    }
                                                    fastReportXML += "<DataSource>";
                                                    fastReportXML += "<TableName>" + tabPage.Text + "</TableName>";
                                                    fastReportXML += "<SearchSQL>" + t.Text + "</SearchSQL>";
                                                    fastReportXML += "</DataSource>";
                                                }
                                            }
                                        }
                                    }

                                }

                            }

                        }
                    }
                }
                fastReportXML += "</DataSources>";
                fastReportXML += "<ReportQueryBox>" + ReportQueryBox + "</ReportQueryBox>";
                fastReportXML += "</FastReport>";
                reportPath = txt_ReportPath.Text;
                xml = fastReportXML;
            }
            SJeMES_Framework.WinForm.FastReportForm.frmFastReport frm = new SJeMES_Framework.WinForm.FastReportForm.frmFastReport(reportPath, xml, connectionString, reportString);
            frm.Show();
        }

    }
}
