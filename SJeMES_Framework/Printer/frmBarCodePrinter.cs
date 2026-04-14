using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SJeMES_Framework.Printer
{
    public partial class frmBarCodePrinter : Form
    {
        SJeMES_Framework.DBHelper.DataBase DB = new SJeMES_Framework.DBHelper.DataBase();
        private string ColName;
        private string sqlSelectData;
        private List<string> BarCodes;
        private string WebServiceUrl;
        public Class.OrgClass Org;
        private DataTable dataTable;
        private string printType;


        int pageCount = 1;
        int pageNo = 1;

        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name);

        public frmBarCodePrinter(Class.OrgClass Org, string WebServiceUrl, string sqlSelectData)
        {
            InitializeComponent();
            this.sqlSelectData = sqlSelectData;
            this.WebServiceUrl = WebServiceUrl;
            this.Org = Org;
            DB = new SJeMES_Framework.DBHelper.DataBase("SqlServer", Org.DBServer, Org.DBName, Org.DBUser, Org.DBPassword, string.Empty);
            comboBox1.SelectedIndex = 0;
        }
        public frmBarCodePrinter(Class.OrgClass Org, string WebServiceUrl, DataTable dt,string printType)
        {
            InitializeComponent();
            this.dataTable = dt;
            this.WebServiceUrl = WebServiceUrl;
            this.printType = printType;
            this.Org = Org;
            txt_BarCodeS.Enabled = false;
            txt_BarCodeE.Enabled = false;
            comboBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            label7.Visible = false;
            dataGridView1.DataSource = dataTable;
            DB = new SJeMES_Framework.DBHelper.DataBase("SqlServer", Org.DBServer, Org.DBName, Org.DBUser, Org.DBPassword, string.Empty);
        }
        public frmBarCodePrinter(Class.OrgClass Org, string WebServiceUrl, List<string> BarCodes)
        {
            InitializeComponent();
            this.BarCodes = BarCodes;
            this.WebServiceUrl = WebServiceUrl;
            this.Org = Org;
            txt_BarCodeS.Enabled = false;
            txt_BarCodeE.Enabled = false;

            DataTable tab = new DataTable();
            DataColumn dc = null;
            dc = tab.Columns.Add("条码", Type.GetType("System.String"));
            foreach (string s in BarCodes)
            {
                DataRow newRow;
                newRow = tab.NewRow();
                newRow["条码"] = s;
                tab.Rows.Add(newRow);
              //  listBox1.Items.Add(s);
            }
            dataGridView1.DataSource = tab;
            DB = new SJeMES_Framework.DBHelper.DataBase("SqlServer", Org.DBServer, Org.DBName, Org.DBUser, Org.DBPassword, string.Empty);
        }
        public frmBarCodePrinter(Class.OrgClass Org, string WebServiceUrl, DataTable dt)
        {
            InitializeComponent();
            this.dataTable = dt;
            this.WebServiceUrl = WebServiceUrl;
            this.Org = Org;
            txt_BarCodeS.Enabled = false;
            txt_BarCodeE.Enabled = false;

         
            dataGridView1.DataSource = dataTable;
            DB = new SJeMES_Framework.DBHelper.DataBase("SqlServer", Org.DBServer, Org.DBName, Org.DBUser, Org.DBPassword, string.Empty);
        }

        private void frmBarCodePrinter_Load(object sender, EventArgs e)
        {
            try
            {
             
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
                {
                    cbo_Print.Items.Add(fPrinterName);
                }
                if (cbo_Print.Items.Count > 0)
                {
                    PrintDocument fPrintDocument = new PrintDocument();
                    cbo_Print.Text = fPrintDocument.PrinterSettings.PrinterName;
                }

                List<Common.IOHelper.File> Files = new List<Common.IOHelper.File>();
                Files = Common.IOHelper.GetAllFile(Application.StartupPath + @"\Printer\BarCodeModel", Files, 1);
                string codeType = string.Empty;
                if (string.IsNullOrEmpty(sqlSelectData))
                {
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(SelALL));
                    thread.IsBackground = true;
                    thread.Start();
                    if (printType.Contains("物料条码"))
                        codeType = "物料条码";
                    else if (printType.Contains("单品条码"))
                        codeType = "产品条码";
                    else if (printType.Contains("批号条码"))
                        codeType = "批号条码";
                    else if (printType.Contains("包装条码"))
                        codeType = "包装条码";
                    else if (printType.Contains("人员信息"))
                        codeType = "人员信息";
                    else if (printType.Contains("库位条码"))
                        codeType = "库位条码";
                    else if (printType.Contains("托盘条码"))
                        codeType = "托盘条码";
                    else if (printType.Contains("固定资产条码"))
                        codeType = "固定资产条码";

                    foreach (Common.IOHelper.File f in Files)
                    {
                        if (f.Name.ToUpper().EndsWith(".BTW"))
                        {
                            if (f.Name.Replace(".BTW", "").Replace(".btw", "").Contains(codeType))
                            { 
                             Button btnModel = new Button();
                            btnModel.Name = f.Name.Replace(".BTW", "").Replace(".btw", "");
                            btnModel.Image = System.Drawing.Icon.ExtractAssociatedIcon(f.Path + @"\" + f.Name).ToBitmap();
                            btnModel.ImageAlign = ContentAlignment.TopCenter;
                            btnModel.Padding = new Padding(10);
                            btnModel.Text = f.Name;
                            btnModel.Font = new Font("宋体", 10, FontStyle.Bold);
                            btnModel.Height = 90;
                            btnModel.Dock = DockStyle.Top;
                            btnModel.TextAlign = ContentAlignment.BottomCenter;
                            btnModel.Click += BtnModel_Click;
                            list_Model.Controls.Add(btnModel);
                        }

                        }
                    }
                }else
                {
                  
                        if (sqlSelectData.Contains("BASE011M"))
                            codeType = "库位";
                        else if (sqlSelectData.Contains("BASE007M"))
                            codeType = "物料";
                        else if (sqlSelectData.Contains("CODE001M"))
                            codeType = "产品";
                        else if (sqlSelectData.Contains("CODE002M"))
                            codeType = "批号";
                        else if (sqlSelectData.Contains("CODE003M"))
                            codeType = "包装";
                        else if (sqlSelectData.Contains("HR001M"))
                            codeType = "人员信息";
                    else if (sqlSelectData.Contains("MES010AA"))
                        codeType = "托盘";

                    foreach (Common.IOHelper.File f in Files)
                    {
                        if (f.Name.ToUpper().EndsWith(".BTW"))
                        {
                            Button btnModel = new Button();
                            //if (codeType== f.Name.Replace(".BTW", "").Replace(".btw", ""))
                            //{

                            //    btnModel.Name = f.Name.Replace(".BTW", "").Replace(".btw", "");
                            //    btnModel.Image = System.Drawing.Icon.ExtractAssociatedIcon(f.Path + @"\" + f.Name).ToBitmap();
                            //    btnModel.ImageAlign = ContentAlignment.TopCenter;
                            //    btnModel.Padding = new Padding(10);
                            //    btnModel.Text = f.Name;
                            //    btnModel.Font = new Font("宋体", 10, FontStyle.Bold);
                            //    btnModel.Height = 90;
                            //    btnModel.Dock = DockStyle.Top;
                            //    btnModel.TextAlign = ContentAlignment.BottomCenter;
                            //    btnModel.Click += BtnModel_Click;
                            //    list_Model.Controls.Add(btnModel);
                            //}
                            if (f.Name.Replace(".BTW", "").Replace(".btw", "").Contains(codeType))
                            {
                                btnModel.Name = f.Name.Replace(".BTW", "").Replace(".btw", "");
                                btnModel.Image = System.Drawing.Icon.ExtractAssociatedIcon(f.Path + @"\" + f.Name).ToBitmap();
                                btnModel.ImageAlign = ContentAlignment.TopCenter;
                                btnModel.Padding = new Padding(10);
                                btnModel.Text = f.Name;
                                btnModel.Font = new Font("宋体", 10, FontStyle.Bold);
                                btnModel.Height = 90;
                                btnModel.Dock = DockStyle.Top;
                                btnModel.TextAlign = ContentAlignment.BottomCenter;
                                btnModel.Click += BtnModel_Click;
                                list_Model.Controls.Add(btnModel);
                            }
                            if (codeType== "物料条码")
                            {
                                string code = "固定资产条码";
                                if (code == f.Name.Replace(".BTW", "").Replace(".btw", ""))
                                {
                                    //Button btnModel = new Button();
                                    btnModel.Name = f.Name.Replace(".BTW", "").Replace(".btw", "");
                                    btnModel.Image = System.Drawing.Icon.ExtractAssociatedIcon(f.Path + @"\" + f.Name).ToBitmap();
                                    btnModel.ImageAlign = ContentAlignment.TopCenter;
                                    btnModel.Padding = new Padding(10);
                                    btnModel.Text = f.Name;
                                    btnModel.Font = new Font("宋体", 10, FontStyle.Bold);
                                    btnModel.Height = 90;
                                    btnModel.Dock = DockStyle.Top;
                                    btnModel.TextAlign = ContentAlignment.BottomCenter;
                                    btnModel.Click += BtnModel_Click;
                                    list_Model.Controls.Add(btnModel);
                                }
                            }
                           

                        }
                    }
                }
          

                Label l = new Label();
                l.Text = "打印";
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Dock = DockStyle.Top;
                l.AutoSize = false;
                l.Height = 20;
                l.BorderStyle = BorderStyle.FixedSingle;
                list_Model.Controls.Add(l);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                Application.Exit();
            }


        }

        private void BtnModel_Click(object sender, EventArgs e)
        {
            try
            {
                

                


                try
                    {
                        Convert.ToInt32(txt_Qty.Text);
                    }
                    catch { MessageBox.Show("打印份数必须输入整数"); return; }


                //if (list_Print.SelectedIndex < 0)
                //{
                //    MessageBox.Show("请选择打印机");
                //    return;
                //}
                if (cbo_Print.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择打印机");
                    return;
                }

                string sql = Common.TXTHelper.ReadToEnd(Application.StartupPath + @"\Printer\BarCodeModel\" + ((Button)sender).Name + ".sql");

                string where = string.Empty;

                string where1 = string.Empty;
                //foreach(string s in listBox1.Items)
                //{
                //    if(string.IsNullOrEmpty(where))
                //    {
                //        where += "WHERE [条码] IN (";

                //        where += "'" + s + @"'";
                //    }
                //    else
                //    {
                //        where += ",'" + s + "'";
                //    }
                //}

                if (dataGridView1.Columns.Contains("BN_NO"))
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                        {
                            if (string.IsNullOrEmpty(where))
                            {
                                where += "WHERE [条码] IN (";
                                where1 += " and [BN_NO] in(";
                                if (dataGridView1.Columns.Contains("行号")|| dataGridView1.Columns.Contains("序号"))
                                {
                                    where += "'" + dataGridView1.Rows[i].Cells[3].Value.ToString() + @"'";
                                    where1+="'"+ dataGridView1.Rows[i].Cells[2].Value.ToString() + @"'";
                                }

                                else
                                {
                                    where += "'" + dataGridView1.Rows[i].Cells[2].Value.ToString() + @"'";
                                    where1 += "'" + dataGridView1.Rows[i].Cells[1].Value.ToString() + @"'";
                                }
                                   
                            }
                            else
                            {
                                if (dataGridView1.Columns.Contains("行号")|| dataGridView1.Columns.Contains("序号"))
                                {
                                    where += ",'" + dataGridView1.Rows[i].Cells[3].Value.ToString() + "'";
                                    where1 += ",'" + dataGridView1.Rows[i].Cells[2].Value.ToString() + @"'";
                                }

                                else
                                {
                                    where += ",'" + dataGridView1.Rows[i].Cells[2].Value.ToString() + @"'";
                                    where1 += ",'" + dataGridView1.Rows[i].Cells[1].Value.ToString() + @"'";
                                }
                                  
                            }
                        }
                    }
                }else
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                        {
                            if (string.IsNullOrEmpty(where))
                            {
                                where += "WHERE [条码] IN (";
                                if (dataGridView1.Columns.Contains("行号") || dataGridView1.Columns.Contains("序号"))
                                    where += "'" + dataGridView1.Rows[i].Cells[2].Value.ToString() + @"'";
                                else
                                    where += "'" + dataGridView1.Rows[i].Cells[1].Value.ToString() + @"'";
                            }
                            else
                            {
                                if (dataGridView1.Columns.Contains("行号") || dataGridView1.Columns.Contains("序号"))
                                    where += ",'" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "'";
                                else
                                    where += ",'" + dataGridView1.Rows[i].Cells[1].Value.ToString() + @"'";
                            }
                        }

                    }
                }
                if (string.IsNullOrEmpty(where))
                {
                    MessageBox.Show("请选中条码！");
                    return;
                }

                where += ")";
                if (!string.IsNullOrEmpty(where1))
                    where1 += ")";



                sql = @"
SELECT
*
FROM
(" + sql + @") TMP
" + where + where1 + @" order by 条码";

     
                DataTable dt = DB.GetDataTable(sql);

                PrintBarCodeHelper.WriteTxt(dt, ((Button)sender).Name, Application.StartupPath + @"\Printer\BarCodeModel", Convert.ToInt32(txt_Qty.Text));

                if (check_OpenModel.Checked)
                {
                    #region 启动答应程序
                    Process ps = new Process();

                    ps.StartInfo.FileName = ((Button)sender).Name + ".bat";

                    ps.StartInfo.RedirectStandardInput = true;
                    ps.StartInfo.RedirectStandardOutput = true;
                    ps.StartInfo.RedirectStandardError = true;
                    ps.StartInfo.CreateNoWindow = true;
                    ps.StartInfo.UseShellExecute = false;
                    ps.Start();
                    ps.WaitForExit();


                    #endregion

                    return;
                }

                SetDefaultPrinter(cbo_Print.Text);
                Thread.Sleep(100);
               
                if (!check_OpenModel.Checked)
                {
                    PrintBarCodeHelper.PrintNow(((Button)sender).Name);
                }
         


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txt_BarCodeS_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txt_BarCodeS.Text = GetBarCode();
        }

        private string GetBarCode()
        {
            string ret = string.Empty;

            try
            {
                WinForm.CommonForm.frmSearchData frm = new WinForm.CommonForm.frmSearchData(Org, WebServiceUrl, sqlSelectData, true, true);
                frm.ShowDialog();

                string sTmp = string.Empty;

                string sColumns = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<Columns>", "</Columns>");
                List<string> sRows = SJeMES_Framework.Common.StringHelper.GetDataFromTag(frm.ReturnDataXML, "<Row>", "</Row>");

                if (sRows.Count > 0)
                {
                    ColName = sColumns.Split(';')[1].Replace("@", "");
                    ret = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(sRows[0], "<" + sColumns.Split(';')[1].Replace("@", "") + @">", "</" + sColumns.Split(';')[1].Replace("@", "") + @">");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return ret;
        }

        private void txt_BarCodeE_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txt_BarCodeE.Text = GetBarCode();
        }

        private void GetBarCodes()
        {
          //  listBox1.Items.Clear();

            try
            {


                string sql = string.Empty;
                string newSql = string.Empty;
             
                sql = "select manager_no from HR002A1 WHERE staff_no='"+ Program.Org.User.UserCode+"'";
                var tab=  Common.WebServiceHelper.GetDataTable(Program.Org, Program.WebServiceUrl,sql, new Dictionary<string, string>());
                string manager_no = string.Empty;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    manager_no += "'"+tab.Rows[i]["manager_no"].ToString()+"',";
                }
                string where = string.Empty;
                if (!string.IsNullOrEmpty(manager_no))
                {
                    manager_no = manager_no.Substring(0, manager_no.LastIndexOf(','));

                    sql = "SELECT manager_powercontent1 FROM HR002M where manager_no in("+ manager_no+")";
                    tab = Common.WebServiceHelper.GetDataTable(Program.Org, Program.WebServiceUrl, sql, new Dictionary<string, string>());
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        where+=" and"+ tab.Rows[i]["manager_powercontent1"].ToString();
                    }

                }
                if (!string.IsNullOrEmpty(where))
                    where = where.Replace("material_no","品号");
                if (sqlSelectData.Contains("品号"))
                {
                    newSql = "select * from(" + sqlSelectData + ")M where 1=1 " + where;
                }
                else
                    newSql = sqlSelectData;

                //sqlSelectData = sqlSelectData + " where 1=1 and ";
                sql = @"
SELECT
count(*)
FROM
(" + newSql + @") TMP
WHERE [" + ColName + @"] between '" + txt_BarCodeS.Text.Trim() + @"' AND '" + txt_BarCodeE.Text.Trim() + @"'";


                  
               


                pageCount = (int)Math.Ceiling(DB.GetDouble(sql) / Convert.ToDouble(comboBox1.Text));
                label7.Text = "当前第" + pageNo + "页/共" + pageCount + "页";


                string codeType = string.Empty;
                if (sqlSelectData.Contains("BASE011M"))
                    codeType = "库位编号";
                else if (sqlSelectData.Contains("BASE007M"))
                    codeType = "品号";
                else if (sqlSelectData.Contains("CODE001M"))
                    codeType = "品号";
                else if (sqlSelectData.Contains("CODE002M"))
                    codeType = "品号";
                else if (sqlSelectData.Contains("CODE003M"))
                    codeType = "品号";
                else if (sqlSelectData.Contains("HR001M"))
                    codeType = "工号";
                else if (sqlSelectData.Contains("MES010AA"))
                    codeType = "品号";

                sql = @"
select top " + comboBox1.Text + @" * from(
SELECT
ROW_NUMBER()over(order by "+ codeType+@")as '序号',*
FROM
(" + newSql + @") TMP
WHERE [" + ColName + @"] between '" + txt_BarCodeS.Text.Trim() + @"' AND '" + txt_BarCodeE.Text.Trim() + @"')tab  where tab.序号>=" + (pageNo - 1) * Convert.ToInt32(comboBox1.Text);

                tab = DB.GetDataTable(sql);
                dataGridView1.DataSource = tab;
               
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(SelALL));
                thread.IsBackground = true;
                thread.Start();
                //for (int i = 0; i < dataGridView1.Rows.Count; i++) //循环datagridview每行
                //{
                //    dataGridView1.Rows[i].Cells[0].Value = true;

                //}

                //Dictionary<string, string> p = new Dictionary<string, string>();
                //p.Add("sql", sql);

                //string xml = Common.WebServiceHelper.RunService(Org, WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);

                //if (Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(xml, "<IsSuccess>", "</IsSuccess>")))
                //{
                //    DataTable dt = Common.StringHelper.GetDataTableFromXML(Common.StringHelper.GetDataFromFirstTag(xml, "<RetData>", "</RetData>"));


                //    dataGridView1.DataSource = dt;
                //    for (int i = 0; i < dataGridView1.Rows.Count; i++) //循环datagridview每行
                //    {
                //        dataGridView1.Rows[i].Cells[0].Value = true;

                //    }

                //}
                //else
                //{
                //    MessageBox.Show(Common.StringHelper.GetDataFromFirstTag(xml, "<RetData>", "</RetData>"));
                //}

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txt_BarCodeS_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txt_BarCodeE.Text.Trim())&&
                !string.IsNullOrEmpty(txt_BarCodeS.Text.Trim()))
            {
                GetBarCodes();
            }
        }

        private void txt_BarCodeE_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_BarCodeE.Text.Trim()) &&
                !string.IsNullOrEmpty(txt_BarCodeS.Text.Trim()))
            {
                GetBarCodes();
            }
        }

        private void btn_SelALL_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                SelALL();
            }
        }
        private void SelALL()
        {
            System.Threading.Tasks.Task.Factory.StartNew(() => {
                for (int i = 0; i < dataGridView1.Rows.Count; i++) //循环datagridview每行
                {
                    dataGridView1.Rows[i].Cells[0].Value = true;

                }
            });
         
        }

        private void btn_RevSel_Click(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() => {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    //判断当前行是否被选中
                    if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                        //设置每一行的选择框为未选中
                        dataGridView1.Rows[i].Cells[0].Value = false;
                    else
                        //设置每一行的选择框为选中
                        dataGridView1.Rows[i].Cells[0].Value = true;
                }
            });
           
        }

        private void btn_NotAll_Click(object sender, EventArgs e)
        {
          
            System.Threading.Tasks.Task.Factory.StartNew(() => {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    //判断当前行是否被选中
                    if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                        //设置每一行的选择框为未选中
                        dataGridView1.Rows[i].Cells[0].Value = false;

                }
            });
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //判断当前行是否被选中
                if ((bool)dataGridView1.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
                    //设置每一行的选择框为未选中
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = false;
                else
                    //设置每一行的选择框为选中
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = true;
          
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pageNo = 1;
            GetBarCodes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pageNo--;
            if (pageNo < 1)
                pageNo = 1;
            GetBarCodes();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pageNo++;
            if (pageNo > pageCount)
                pageNo = pageCount;
            GetBarCodes();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pageNo = pageCount;
            GetBarCodes();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageNo = 1;
            pageCount = 1;
           if(!string.IsNullOrEmpty(ColName))
            GetBarCodes();
        }
    }
    
}
