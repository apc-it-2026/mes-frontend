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
    public partial class frmFastReport : Form
    {
        private string fileName;
        private string fastReportXml;
        private string connectionString;
        private string sqlWhere;
        private Dictionary<string, string> dicParameter;
        DBHelper.DataBase DB;

        Class.OrgClass Org;
        private string WebServiceUrl;
        private string reportString;
        private Dictionary<string, string> dic;


        FastReport.Report report = new FastReport.Report();
        FastReport.Preview.PreviewControl previewControl = new FastReport.Preview.PreviewControl();//创建报表控件


        public frmFastReport(string reportCode, Class.OrgClass Org, string WebServiceUrl)
        {
            this.WebServiceUrl = WebServiceUrl;
            this.Org = Org;
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("sql", "SELECT ReportPath,ReportXML,ReportString FROM[SJEMSSYS].[dbo].[SYSREPORT01M] where ReportCode = '" + reportCode + "'");
            string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);
            string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
            var tab = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
            if (tab.Rows.Count > 0)
            {

                this.fastReportXml = tab.Rows[0]["ReportXML"].ToString();
                this.fileName = tab.Rows[0]["ReportPath"].ToString();
                this.reportString = tab.Rows[0]["ReportString"].ToString();
                this.connectionString = @"Data Source=" + Org.DBServer + @";AttachDbFilename=;Initial Catalog=" + Org.DBName + @";
                    Integrated Security=False;Persist Security Info=False;User ID=" + Org.DBUser + ";Password=" + Org.DBPassword;

            }
            InitializeComponent();
        }

        public frmFastReport(string reportCode, Class.OrgClass Org, string WebServiceUrl,string sqlWhere)
        {
            this.WebServiceUrl = WebServiceUrl;
            this.Org = Org;
            this.sqlWhere = sqlWhere;
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("sql", "SELECT ReportPath,ReportXML,ReportString FROM[SJEMSSYS].[dbo].[SYSREPORT01M] where ReportCode = '" + reportCode + "'");
            string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);
            string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
            var tab = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
            if (tab.Rows.Count > 0)
            {

                this.fastReportXml = tab.Rows[0]["ReportXML"].ToString();
                this.fileName = tab.Rows[0]["ReportPath"].ToString();
                this.reportString = tab.Rows[0]["ReportString"].ToString();
                this.connectionString = @"Data Source=" + Org.DBServer + @";AttachDbFilename=;Initial Catalog=" + Org.DBName + @";
                    Integrated Security=False;Persist Security Info=False;User ID=" + Org.DBUser + ";Password=" + Org.DBPassword;

            }
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">报表文件路径</param>
        /// <param name="fastReportXml">报表条件XML</param>
        /// <param name="connectionString">数据库链接字符串</param>
        public frmFastReport(string fileName, string fastReportXml, string connectionString)
        {
            this.fileName = fileName;
            this.fastReportXml = fastReportXml;
            this.connectionString = connectionString;
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">报表文件路径</param>
        /// <param name="fastReportXml">报表条件XML</param>
        /// <param name="connectionString">数据库链接字符串</param>
        /// <param name="reportString">报表字符串</param>
        public frmFastReport(string fileName, string fastReportXml, string connectionString,string reportString)
        {
            this.fileName = fileName;
            this.fastReportXml = fastReportXml;
            this.connectionString = connectionString;
            this.reportString = reportString;
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">报表文件路径</param>
        /// <param name="fastReportXml">报表条件XML</param>
        /// <param name="connectiongString">数据库链接字符串</param>
        /// <param name="dicParameter">参数键值对</param>
        public frmFastReport(string fileName, string fastReportXml, string connectiongString, Dictionary<string, string> dicParameter)
        {
            this.fileName = fileName;
            this.fastReportXml = fastReportXml;
            this.connectionString = connectiongString;
            this.dicParameter = dicParameter;
            InitializeComponent();
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DB">DB</param>
        /// <param name="fileName">报表文件路径</param>
        /// <param name="fastReportXml">报表条件XML</param>
        public frmFastReport(DBHelper.DataBase DB, string fileName, string fastReportXml)
        {
            this.DB = DB;
            this.fileName = fileName;
            this.fastReportXml = fastReportXml;
            this.connectionString = DB.ConnectionText;
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DB"></param>
        /// <param name="fileName">报表文件路径</param>
        /// <param name="fastReportXml">报表条件XML</param>
        /// <param name="dicParameter">参数键值对</param>
        public frmFastReport(DBHelper.DataBase DB, string fileName, string fastReportXml,  Dictionary<string, string> dicParameter)
        {
            this.DB = DB;
            this.fileName = fileName;
            this.fastReportXml = fastReportXml;
            this.connectionString = DB.ConnectionText;
            this.dicParameter = dicParameter;
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Org">账套</param>
        /// <param name="WebServiceUrl"></param>
        /// <param name="dic">打印条件</param>
        public frmFastReport(Class.OrgClass Org, string WebServiceUrl, Dictionary<string, string> dic)
        {
            try
            {
                string reportCode = (dic as Dictionary<string, string>)["ReportCode"];
                string docNo = (dic as Dictionary<string, string>)["DocNo"];
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", "SELECT ReportPath,ReportXML,ReportString FROM[SJEMSSYS].[dbo].[SYSREPORT01M] where ReportCode = '" + reportCode + "'");
                this.Org = Org;
                this.WebServiceUrl = WebServiceUrl;
                this.dic = dic;
                string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);
                string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
                var tab = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                if (tab.Rows.Count > 0)
                {

                    this.fastReportXml = tab.Rows[0]["ReportXML"].ToString().Replace("@" + docNo.Split('*')[1], docNo.Split('*')[0]);
                    this.fileName = tab.Rows[0]["ReportPath"].ToString();
                    this.reportString = tab.Rows[0]["ReportString"].ToString();
                    this.connectionString = @"Data Source=" + Org.DBServer + @";AttachDbFilename=;Initial Catalog=" + Org.DBName + @";
                    Integrated Security=False;Persist Security Info=False;User ID=" + Org.DBUser + ";Password=" + Org.DBPassword;

                }
            }
            catch (Exception ex)
            {
            }
           
            InitializeComponent();
        }
        public frmFastReport(Class.OrgClass Org ,Dictionary<string, string> dic, string WebServiceUrl )
        {
            try
            {
                string reportCode = (dic as Dictionary<string, string>)["ReportCode"];
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", "SELECT ReportPath,ReportXML,ReportString FROM[SJEMSSYS].[dbo].[SYSREPORT01M] where ReportCode = '" + reportCode + "'");
                this.Org = Org;
                this.WebServiceUrl = WebServiceUrl;
                this.dic = dic;
                string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(this.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);
                string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
                var tab = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                if (tab.Rows.Count > 0)
                {

                    this.fastReportXml = tab.Rows[0]["ReportXML"].ToString();
                    this.fileName = tab.Rows[0]["ReportPath"].ToString();
                    this.reportString = tab.Rows[0]["ReportString"].ToString();
                    this.connectionString = @"Data Source=" + Org.DBServer + @";AttachDbFilename=;Initial Catalog=" + Org.DBName + @";
                    Integrated Security=False;Persist Security Info=False;User ID=" + Org.DBUser + ";Password=" + Org.DBPassword;

                }
            }
            catch (Exception ex)
            {
            }

            InitializeComponent();
        }


        private void frmFastReport_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.reportString))
                {
                    if (!System.IO.File.Exists(fileName))
                    {
                        fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                        MessageBox.Show("找不到报表文件：" + fileName, "报表提示");
                        return;
                    }
                }
                
                string title = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(fastReportXml, "<Title>", "</Title>");
                this.Text = title;
                Panel panel = new Panel();
                panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
                panel.Name = "panelQuery";
                panel.Location = new Point(4, 3);
                panel.Size = new Size(963, 45);
                Controls.Add(panel);



                #region 查询文本框
                string reportQueryBox = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(fastReportXml, "<ReportQueryBox>", "</ReportQueryBox>");
                List<string> lstQueryBox = SJeMES_Framework.Common.StringHelper.GetDataFromTag(reportQueryBox, "<QueryBox>", "</QueryBox>");

                int lblX = 18;//初始化lableX轴坐标
                int lblY = 15;//初始化lableY轴坐标


                int txtX = 77;//初始化textboxX轴坐标
                int txtY = 12;//初始化textboxY轴坐标

                int i = 1;//控件数
                int controlCount = 4;//一行放4个控件
                foreach (var item in lstQueryBox)
                {
                    if (i > 1)
                    {
                        lblX += 204;
                        txtX += 204;
                    }
                    if (i > controlCount)//控件数大于一行数
                    {
                        controlCount += 4;//控件数加4
                        lblY += 27;//lableY轴坐标加27
                        txtY += 27;//textboxY轴坐标加27

                        lblX = 18;//lableX轴坐标初始化
                        txtX = 77;//textboxX轴坐标初始化

                        int pHeight = panel.Height + 26;//panel高度加26
                        panel.Size = new Size(963, pHeight);
                    }

                    Label lbl = new Label();
                    lbl.Text = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<Title>", "</Title>");
                    lbl.Location = new Point(lblX, lblY);
                    lbl.Size = new Size(53, 12);
                    panel.Controls.Add(lbl);

                    string dataType = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<DataType>", "</DataType>");
                    string dataSelectSQL = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<DataSelectSQL>", "</DataSelectSQL>");
                    switch (dataType.ToLower())
                    {
                        case "string":
                            TextBox txt = new TextBox();
                            txt.Size = new Size(128, 21);
                            txt.Location = new Point(txtX, txtY);
                            txt.Name = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<Title>", "</Title>");
                            if (!string.IsNullOrEmpty(dataSelectSQL))
                            {
                                txt.Tag = dataSelectSQL;
                                BandClick(txt);
                                txt.BackColor = System.Drawing.SystemColors.Info;
                            }

                            panel.Controls.Add(txt);
                            break;
                        case "date":
                            DateTimePicker dtp = new DateTimePicker();
                            dtp.Format = DateTimePickerFormat.Custom;
                            dtp.CustomFormat = "yyyy-MM-dd";
                            dtp.Size = new Size(128, 21);
                            dtp.Location = new Point(txtX, txtY);
                            dtp.Name = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<Title>", "</Title>");
                            panel.Controls.Add(dtp);
                            break;
                        case "combobox":
                            ComboBox cbo = new ComboBox();
                            cbo.Size = new Size(128, 21);
                            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
                            cbo.Location = new Point(txtX, txtY);
                            cbo.Name = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<Title>", "</Title>");
                            if (!string.IsNullOrEmpty(dataSelectSQL))
                            {
                                dataSelectSQL = dataSelectSQL.Replace("，", ",");
                                string[] cbo_Items;
                                if (dataSelectSQL.IndexOf(",") > -1)
                                {
                                    cbo_Items = dataSelectSQL.Split(',');
                                    cbo.Items.AddRange(cbo_Items);
                                }
                                else
                                {
                                    cbo.Items.Add(dataSelectSQL);
                                }
                            }
                            panel.Controls.Add(cbo);
                            break;
                    }
                    i++;

                }
                #endregion
                if (lstQueryBox.Count > 0)
                {
                    #region 查询按钮
                    Button btn = new Button();
                    btn.Text = "查 询";
                    btn.Size = new Size(75, 23);
                    btn.Location = new Point(txtX + 180, txtY);
                    btn.Click += Btn_Click;
                    panel.Controls.Add(btn);
                    #endregion
                }else
                {
                    panel.Height = 0;
                }



                #region 报表
                Panel panelReport = new Panel();
                panelReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
                panelReport.Location = new Point(4, panel.Height + 5);
                panelReport.Size = new Size(963, this.Height - panel.Height - 60);
                Controls.Add(panelReport);

               
               

                previewControl.Dock = System.Windows.Forms.DockStyle.Fill;//填充整个控件
                panelReport.Controls.Add(previewControl);//添加控件
               
                report.Preview = previewControl;//指定在这个控件预览，如果没有这行，会弹出一个窗口预览 

                if (string.IsNullOrEmpty(this.reportString))//报表XML为空的，直接加载当前路径报表
                    report.Load(fileName);
                else
                    report.LoadFromString(this.reportString);
                
                report.Dictionary.Connections[0].ConnectionString = connectionString;//设置连接字符串
                #region 设置数据源
                string dataSources = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(fastReportXml, "<DataSources>", "</DataSources>");

                string dataKey = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(dataSources, "<DataKey>", "</DataKey>");//关联字段

                bool isShow = Convert.ToBoolean(SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(dataSources, "<IsShow>", "</IsShow>"));//是否显示数据

                List<string> lstDataSources = SJeMES_Framework.Common.StringHelper.GetDataFromTag(dataSources, "<DataSource>", "</DataSource>");
     
                    if (isShow)
                    {
                        foreach (var item in lstDataSources)
                        {
                            string tableName = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<TableName>", "</TableName>");
                            string searchSQL = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<SearchSQL>", "</SearchSQL>");
                            FastReport.Data.TableDataSource tds = report.GetDataSource(tableName) as FastReport.Data.TableDataSource;//设置表格数据源名称
                            tds.SelectCommand = "select * from(" + searchSQL+ sqlWhere + ")tab where 1=1";//设置表格数据源SQL
                            tds.Init();//初始化
                        }
                    }
                    else
                    {
                        foreach (var item in lstDataSources)
                        {
                            string tableName = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<TableName>", "</TableName>");
                            string searchSQL = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<SearchSQL>", "</SearchSQL>");
                            FastReport.Data.TableDataSource tds = report.GetDataSource(tableName) as FastReport.Data.TableDataSource;//设置表格数据源名称
                            tds.SelectCommand = "select * from(" + searchSQL+ sqlWhere + ")tab where 1<>1";//设置表格数据源SQL
                            tds.Init();//初始化
                        }
                    }
               

                #endregion


                #region 参数
                if (dicParameter != null)
                {
                    foreach (var item in dicParameter)
                    {
                        report.SetParameterValue(item.Key, item.Value);
                    }
                }

                #endregion


                report.Prepare();
                report.ShowPrepared(true);
                WindowState = FormWindowState.Maximized;
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    MessageBox.Show("找不到报表文件：" + fileName, "报表提示");
                    return;
                }
                Dictionary<string, string> dicWhere = new Dictionary<string, string>();
                foreach (Control ctr in this.Controls)
                {
                    if (ctr is Panel)
                    {
                        if (ctr.Name == "panelQuery")
                        {
                            Panel panel = ctr as Panel;
                            foreach (Control c in panel.Controls)//循环里面的文本框
                            {
                                if (c is TextBox)
                                {
                                    if (!string.IsNullOrEmpty(c.Text))
                                    {
                                        dicWhere.Add(c.Name, " and tab." + c.Name + "='" + c.Text + "'");
                                    }

                                }
                                else if (c is DateTimePicker)
                                {
                                    dicWhere.Add(c.Name, " and tab." + c.Name + "='" + c.Text + "'");
                                }else if(c is ComboBox)
                                {
                                    if (!string.IsNullOrEmpty(c.Text))
                                        dicWhere.Add(c.Name, " and tab." + c.Name + "='" + c.Text + "'");
                                }
                            }
                        }
                        else
                        {
                            //if (string.IsNullOrEmpty(sqlWhere))
                            //{
                            //    MessageBox.Show("请输入查询条件");
                            //    return;
                            //}

                         

                            report.Preview = previewControl;
                         

                            if (string.IsNullOrEmpty(this.reportString))//报表XML为空的，直接加载当前路径报表
                                report.Load(fileName);
                            else
                                report.LoadFromString(this.reportString);


                            report.Dictionary.Connections[0].ConnectionString = connectionString;//设置连接字符串
                            #region 设置数据源
                            string dataSources = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(fastReportXml, "<DataSources>", "</DataSources>");

                            string dataKey = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(dataSources, "<DataKey>", "</DataKey>");//关联字段

                            List<string> lstDataSources = SJeMES_Framework.Common.StringHelper.GetDataFromTag(dataSources, "<DataSource>", "</DataSource>");
                           
                            if (lstDataSources.Count > 1)//当有多个数据源的时候，表头没数据，表身也不要显示数据
                            {
                                int n = 1;
                                DataTable tab = new DataTable();
                                foreach (var item in lstDataSources)
                                {
                                    
                                    string tableName = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<TableName>", "</TableName>");
                                    string searchSQL = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<SearchSQL>", "</SearchSQL>");
                                    string newSQLWhere = string.Empty;
                                    foreach (var dic_kv in dicWhere)
                                    {
                                        if (searchSQL.Contains(dic_kv.Key))
                                        {
                                            newSQLWhere += dic_kv.Value;
                                        }
                                    }

                                   
                                    if (n == 1)
                                    {
                                        if (DB != null)
                                        {
                                            tab= DB.GetDataTable("select top 1*  from(" + searchSQL + ")tab where 1=1 " + newSQLWhere);
                                        }
                                        else
                                        {
                                            Dictionary<string, string> p = new Dictionary<string, string>();
                                            p.Add("sql", "select top 1 *  from(" + searchSQL + ")tab where 1=1 " + newSQLWhere);

                                            string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.Org, Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);
                                            string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
                                             tab = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                                        }
                                    }


                                    if (tab.Rows.Count > 0)//当表头没数据的时候，表身也不加载数据
                                    {
                                        FastReport.Data.TableDataSource tds = report.GetDataSource(tableName) as FastReport.Data.TableDataSource;//设置表格数据源名称
                                        tds.SelectCommand = "select *  from(" + searchSQL + ")tab where 1=1 " + newSQLWhere;//设置表格数据源SQL
                                        tds.Init();//初始化
                                    }else
                                    {
                                        FastReport.Data.TableDataSource tds = report.GetDataSource(tableName) as FastReport.Data.TableDataSource;//设置表格数据源名称
                                        tds.SelectCommand = "select *  from(" + searchSQL + ")tab where 1<>1 " ;//设置表格数据源SQL
                                        tds.Init();//初始化
                                    }
                                    
                                    n++;
                                }

                            }
                            else
                            {
                                foreach (var item in lstDataSources)
                                {
                                    string tableName = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<TableName>", "</TableName>");
                                    string searchSQL = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(item, "<SearchSQL>", "</SearchSQL>");
                                    string newSQLWhere = string.Empty;
                                    foreach (var dic_kv in dicWhere)
                                    {
                                        if (searchSQL.Contains(dic_kv.Key))
                                        {
                                            newSQLWhere += dic_kv.Value;
                                        }
                                    }

                                    FastReport.Data.TableDataSource tds = report.GetDataSource(tableName) as FastReport.Data.TableDataSource;//设置表格数据源名称
                                    tds.SelectCommand = "select *  from(" + searchSQL + ")tab where 1=1 " + newSQLWhere;//设置表格数据源SQL
                                    tds.Init();//初始化
                                }
                            }
                
                            #endregion


                            #region 参数
                            if (dicParameter != null)
                            {
                                foreach (var item in dicParameter)
                                {
                                    report.SetParameterValue(item.Key, item.Value);
                                }
                            }
                            #endregion


                            report.Prepare();
                            report.ShowPrepared(true);
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
       
        private void BandClick(TextBox txt)
        {
     
            txt.Click += Txt_Click;
        }
        private void Txt_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                if (DB != null)
                {
                    WinForm.CommonForm.frmSearchData frm = new CommonForm.frmSearchData(DB, txt.Tag.ToString(), true, false);
                    frm.ShowDialog();
                    if (!string.IsNullOrEmpty(frm.ReturnDataXML))
                    {
                        txt.Text = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<" + txt.Name + ">", "</" + txt.Name + ">");
                    }
                }
                else
                {
                    WinForm.CommonForm.frmSearchData frm = new CommonForm.frmSearchData(Program.Org, Program.WebServiceUrl, txt.Tag.ToString(), true, false);
                    frm.ShowDialog();
                    if (!string.IsNullOrEmpty(frm.ReturnDataXML))
                    {
                        txt.Text = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<" + txt.Name + ">", "</" + txt.Name + ">");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
