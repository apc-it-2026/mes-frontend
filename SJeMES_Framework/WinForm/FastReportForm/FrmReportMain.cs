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
    public partial class FrmReportMain : Form
    {
        int pageNo = 1;
        int pageCount = 0;
        string reportCode = string.Empty;
        private string DocNo;//单据编号
        private string ModuleNo;//模块名称
        private string HeadDataKey;
        DBHelper.DataBase DB;
        public FrmReportMain()
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("sql", "use SJEMSSYS select COUNT(*) from  sysobjects where  id = object_id('SYSREPORT01M') and type = 'U'");
            string XML = SJeMES_Framework.Common.WebServiceHelper.RunService( Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);
            string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
            var tab = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
            if (Convert.ToInt32(tab.Rows[0][0]) == 0)
            {
               string sql = "use SJEMSSYS CREATE TABLE SYSREPORT01M(id int identity(1,1) NOT NULL,ReportCode  varchar(200) not null,ReportName varchar(500) not null,ReportPath nvarchar(500) not null,ReportXML nvarchar(max),ReportString nvarchar(max),ModuleNo nvarchar(200))";
                p = new Dictionary<string, string>();
                p.Add("sql", sql);
                XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "ExecuteNonQuery", p);
                // SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>");
            }
           

            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocNo">单据编号</param>
        /// <param name="ModuleNo">模块编号</param>
        /// <param name="HeadDataKey"></param>
        public FrmReportMain(string DocNo,string ModuleNo,string HeadDataKey)
        {
            this.DocNo = DocNo;
            this.ModuleNo = ModuleNo;
            this.HeadDataKey = HeadDataKey;
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("sql", "use SJEMSSYS select COUNT(*) from  sysobjects where  id = object_id('SYSREPORT01M') and type = 'U'");
            string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);
            string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
            var tab = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
            if (Convert.ToInt32(tab.Rows[0][0]) == 0)
            {
                string sql = "use SJEMSSYS CREATE TABLE SYSREPORT01M(id int identity(1,1) NOT NULL,ReportCode  varchar(200) not null,ReportName varchar(500) not null,ReportPath nvarchar(500) not null,ReportXML nvarchar(max),ReportString nvarchar(max),ModuleNo nvarchar(200))";
                p = new Dictionary<string, string>();
                p.Add("sql", sql);
                XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "ExecuteNonQuery", p);
                // SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>");
            }


            InitializeComponent();
        }
        public FrmReportMain(DBHelper.DataBase DB)
        {
            this.DB = DB;
            string sql = "use SJEMSSYS select COUNT(*) from  sysobjects where  id = object_id('SYSREPORT01M') and type = 'U'";
            if (DB.GetInt32(sql) == 0)
            {
                sql = "use SJEMSSYS CREATE TABLE SYSREPORT01M(id int identity(1,1) NOT NULL,ReportCode  varchar(200) not null,ReportName varchar(500) not null,ReportPath nvarchar(500) not null,ReportXML nvarchar(max),ReportString nvarchar(max),ModuleNo nvarchar(200))";
                DB.Open();
                DB.ExecuteNonQuery(sql);
                DB.Close();
            }
            InitializeComponent();
        }
        private void FrmReportMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                string reportWhere = string.Empty;
                if (!string.IsNullOrEmpty(ModuleNo))
                    reportWhere = " where 1=1 and ModuleNo='"+ ModuleNo+"'";
                DataTable tab = new DataTable();
                if (DB != null)
                {
                    pageCount = (int)Math.Ceiling(Convert.ToDouble(DB.GetInt32("select COUNT(*)  from [SJEMSSYS].[dbo].[SYSREPORT01M](NOLOCK) "+ reportWhere))/30.0);
                    string sql = @"select top 30 * from (
select ROW_NUMBER() over(order by ReportCode) as '序号', ReportCode as '报表代码', ReportName as '报表名称'
--,ReportPath as '报表路径', ReportXML as '报表XML' 
from[SJEMSSYS].[dbo].[SYSREPORT01M](NOLOCK) "+ reportWhere+@")tab
where tab.序号 > " + (pageNo - 1) * 30;
                    tab = DB.GetDataTable(sql);

                }
                else
                {
                    Dictionary<string, string> p = new Dictionary<string, string>();
                    p.Add("sql", "select COUNT(*)  from [SJEMSSYS].[dbo].[SYSREPORT01M](NOLOCK) "+ reportWhere);
                    string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);
                    string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
                    tab = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                    pageCount = (int)Math.Ceiling( Convert.ToDouble(tab.Rows[0][0])/30.0);
                    string sql = @"select top 30 * from (
select ROW_NUMBER() over(order by ReportCode) as '序号', ReportCode as '报表代码', ReportName as '报表名称'
--,ReportPath as '报表路径', ReportXML as '报表XML' 
from[SJEMSSYS].[dbo].[SYSREPORT01M](NOLOCK) "+ reportWhere+@")tab
where tab.序号 > " + (pageNo - 1) * 30;
                    p = new Dictionary<string, string>();
                    p.Add("sql", sql);
                    XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);
                    dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
                    tab = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                }
            

                dataGridView1.DataSource = tab;
                dataGridView1.ClearSelection();
                label1.Text = "当前第" + pageNo + "页";
                label2.Text = "共" + pageCount + "页";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void btn_Index_Click(object sender, EventArgs e)
        {
            pageNo = 1;
            LoadData();
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            pageNo--;
            if (pageNo < 1)
            {
                pageNo = 1;
                MessageBox.Show("已经是第一页");
                return;
            }
            LoadData();
        }

        private void btn_Pre_Click(object sender, EventArgs e)
        {
            pageNo++;
            if (pageNo > pageCount)
            {
                pageNo = pageCount;
                MessageBox.Show("已经是最后一页");
                return;
            }
            LoadData();
        }

        private void btn_LastIndex_Click(object sender, EventArgs e)
        {
            pageNo = pageCount;
            LoadData();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {
                frmReportSetting frm = new frmReportSetting(DB,"", ModuleNo);
                frm.ShowDialog();
                if (frm.isTrue)
                {
                    pageNo = 1;
                    LoadData();
                }
            }else
            {
                frmReportSetting frm = new frmReportSetting("", ModuleNo);
                frm.ShowDialog();
                if (frm.isTrue)
                {
                    pageNo = 1;
                    LoadData();
                }
            }
           


        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (DB != null)
            {
                frmReportSetting frm = new frmReportSetting(DB, reportCode, ModuleNo);
                frm.ShowDialog();
                if (frm.isTrue)
                {
                    pageNo = 1;
                    LoadData();
                }
            }else
            {
                frmReportSetting frm = new frmReportSetting(reportCode, ModuleNo);
                frm.ShowDialog();
                if (frm.isTrue)
                {
                    pageNo = 1;
                    LoadData();
                }
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                reportCode = dataGridView1.Rows[e.RowIndex].Cells["报表代码"].Value.ToString();
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(reportCode))
            {
                MessageBox.Show("请选择需要删除的报表");
                return;
            }
            if (MessageBox.Show("确定要删除报表数据？", "删除报表", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string sql = "delete from [SJEMSSYS].[dbo].[SYSREPORT01M] where ReportCode='" + reportCode + "'";
                bool b = false;
                if (DB != null)
                {
                    int n=DB.ExecuteNonQueryOffline(sql);
                    if (n > 0)
                        b = true;
                    
                }else
                {
                    Dictionary<string, string> p = new Dictionary<string, string>();
                    p.Add("sql", sql);
                    string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "ExecuteNonQuery", p);
                    b = Convert.ToBoolean(SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>"));
                }
                
                if (b)
                {
                    MessageBox.Show("删除成功！");
                    pageNo = 1;
                    LoadData();
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }
            }
            else
                return;

        }

        private void btn_DownLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(reportCode))
            {
                MessageBox.Show("请选择需要下载的报表");
                return;
            }
         

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                var tab = new DataTable();
                string sql = "select * from [SJEMSSYS].[dbo].[SYSREPORT01M] where ReportCode='" + reportCode + "'";
                bool b = false;
                if (DB != null)
                {
                    tab = DB.GetDataTable(sql);
                    if (tab.Rows.Count > 0)
                        b = true;

                }
                else
                {
                    Dictionary<string, string> p = new Dictionary<string, string>();
                    p.Add("sql", sql);
                    string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);
                    // b = Convert.ToBoolean(SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>"));
                    string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
                    tab = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                    if (tab.Rows.Count > 0)
                        b = true;
                }
                if (b)
                {

                    if (SJeMES_Framework.Common.TXTHelper.WriteToEnd(fbd.SelectedPath + "\\" + tab.Rows[0]["ReportName"].ToString() + ".frx", tab.Rows[0]["ReportString"].ToString()))
                        MessageBox.Show("下载成功！");
                    else
                        MessageBox.Show("下载失败！");
                }
                else
                {
                    MessageBox.Show("下载失败！");
                }

            }






        }

        private void btn_preView_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(reportCode))
            {
                MessageBox.Show("请选择需要预览的报表");
                return;
            }
            string sql = "select * from [SJEMSSYS].[dbo].[SYSREPORT01M] where ReportCode='" + reportCode + "'";
            var tab = new DataTable();
            string connectionString = string.Empty;
          
            if (DB != null)
            {
                tab = DB.GetDataTable(sql);
                connectionString = DB.ConnectionText;
            }
            else
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", sql);
                string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);
                string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
                tab = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                connectionString = @"Data Source=" + Program.Org.DBServer + @";AttachDbFilename=;Initial Catalog=" + Program.Org.DBName + @";
                    Integrated Security=False;Persist Security Info=False;User ID=" + Program.Org.DBUser + ";Password=" + Program.Org.DBPassword;
            }
            if (tab != null && tab.Rows.Count > 0)
            {
                SJeMES_Framework.WinForm.FastReportForm.frmFastReport frm = new SJeMES_Framework.WinForm.FastReportForm.frmFastReport(tab.Rows[0]["ReportPath"].ToString(), tab.Rows[0]["ReportXML"].ToString().Replace("@"+ HeadDataKey,DocNo), connectionString, tab.Rows[0]["ReportString"].ToString());
                frm.Show();
            }else
            {
                MessageBox.Show("预览失败，请配置好当前报表！");
                return;
            }
            


        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
          
                string sql = "select * from [SJEMSSYS].[dbo].[SYSREPORT01M] where ReportCode='" + dataGridView1.Rows[e.RowIndex].Cells["报表代码"].Value.ToString() + "'";
                var tab = new DataTable();
                string connectionString = string.Empty;

                if (DB != null)
                {
                    tab = DB.GetDataTable(sql);
                    connectionString = DB.ConnectionText;
                }
                else
                {
                    Dictionary<string, string> p = new Dictionary<string, string>();
                    p.Add("sql", sql);
                    string XML = SJeMES_Framework.Common.WebServiceHelper.RunService( Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.DataBase", "GetDataTable", p);
                    string dtXML = SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
                    tab = SJeMES_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                    connectionString = @"Data Source=" + Program.Org.DBServer + @";AttachDbFilename=;Initial Catalog=" + Program.Org.DBName + @";
                    Integrated Security=False;Persist Security Info=False;User ID=" + Program.Org.DBUser + ";Password=" + Program.Org.DBPassword;
                }
                if (tab != null && tab.Rows.Count > 0)
                {
                    SJeMES_Framework.WinForm.FastReportForm.frmFastReport frm = new SJeMES_Framework.WinForm.FastReportForm.frmFastReport(tab.Rows[0]["ReportPath"].ToString(), tab.Rows[0]["ReportXML"].ToString().Replace("@" + HeadDataKey, DocNo), connectionString, tab.Rows[0]["ReportString"].ToString());
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("预览失败，请配置好当前报表！");
                    return;
                }
            }
        }
    }
}
