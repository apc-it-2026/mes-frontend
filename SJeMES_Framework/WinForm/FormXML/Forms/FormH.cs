using SJeMES_Framework.WinForm.FormXML.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.FormXML.Forms
{
    public partial class FormH : Form
    {

        Forms.FormClass FC;
        public string WhereSql;

        public string Status;
        public TableClass Table;
        public FormPanel.HeadPanelClass hpc;
        public Dictionary<string, FunButtonClass> PrintButtons;
        public Dictionary<string, FunButtonClass> DoWorkButtons;
        public int PrintButtonsCount;
        public int DoWorkButtonsCount;
        public string DesignXML;



        public void SetWhereSql(string sql)
        {
            this.WhereSql = sql;
            Table.SqlWhere = sql;
        }


        public FormH(string XML, string WebServiceUrl,SJeMES_Framework.Class.OrgClass Org)
        {
            InitializeComponent();

            

            this.DesignXML = XML;
           

            Program.Org = Org;
            Program.WebServiceUrl = WebServiceUrl;

            this.FC = new SJeMES_Framework.WinForm.FormXML.Forms.FormClass(XML, WebServiceUrl,Org);


            try
            {
                this.Text = FC.FormTitle;
                this.Width = FC.FormWidth;
                this.Height = FC.FormHight;

                this.Table = FC.DataSource.Tables["Table1"];


                InitOnlyHeadForm(FC);




            }
            catch (Exception EX) { MessageBox.Show(FC.FormTitle+" "+EX.Message); }

        }


        public FormH(string XML, string WhereSql, string WebServiceUrl, SJeMES_Framework.Class.OrgClass Org)
        {
            InitializeComponent();
            this.WhereSql = WhereSql;

            this.DesignXML = XML;
         

      

            Program.Org = Org;
            Program.WebServiceUrl = WebServiceUrl;

            this.FC = new SJeMES_Framework.WinForm.FormXML.Forms.FormClass(XML, WebServiceUrl, Org);


            try
            {
                this.Text = FC.FormTitle;
                this.Width = FC.FormWidth;
                this.Height = FC.FormHight;

                this.Table = FC.DataSource.Tables["Table1"];

                InitOnlyHeadForm(FC);

                 

                Table.SqlWhere = WhereSql;




            }
            catch (Exception EX) { MessageBox.Show(FC.FormTitle+" "+EX.Message); }

        }

        private void InitOnlyHeadForm(Forms.FormClass fC)
        {
            try
            {
                FormPanel.HeadPanelClass hpc = (FormPanel.HeadPanelClass)fC.FormPanels["Panel1"];


                panel_HeadMain.Controls.Add(hpc.GetControls(this.Name, fC));

                if (this.DesignXML.IndexOf("<DoWorkButtonsCount>") > -1)
                {
                    DoWorkButtonsCount = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(DesignXML, "<DoWorkButtonsCount>", "</DoWorkButtonsCount"));
                }
                if (this.DesignXML.IndexOf("<PrintButtonsCount>") > -1)
                {
                    PrintButtonsCount = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(DesignXML, "<PrintButtonsCount>", "</PrintButtonsCount"));
                }


                if (DesignXML.IndexOf("<PrintButtons>") > -1)
                {
                    string xmlPrintButtons = Common.StringHelper.GetDataFromFirstTag(DesignXML, "<PrintButtons>", "</PrintButtons>");

                    PrintButtons = new Dictionary<string,FunButtonClass>();
                    for (int i = PrintButtonsCount; i >= 1; i--)
                    {
                        string xmlPrintButton = Common.StringHelper.GetDataFromFirstTag(xmlPrintButtons, "<PrintButton" + i + ">", "</PrintButton" + i + ">");

                        PrintButtons.Add("PrintButton" + i, new FunButtonClass(xmlPrintButton));
                    }
                }
                if (DesignXML.IndexOf("<DoWorkButtons>") > -1)
                {
                    string xmlDoWorkButtons = Common.StringHelper.GetDataFromFirstTag(DesignXML, "<DoWorkButtons>", "</DoWorkButtons>");

                    DoWorkButtons = new Dictionary<string,FunButtonClass>();
                    for (int i = DoWorkButtonsCount; i >= 1; i--)
                    {
                        string xmlDoWorkButton = Common.StringHelper.GetDataFromFirstTag(xmlDoWorkButtons, "<DoWorkButton" + i + ">", "</DoWorkButton" + i + ">");

                        DoWorkButtons.Add("DoWorkButton" + i, new FunButtonClass(xmlDoWorkButton));
                    }
                }

                if(PrintButtons !=null)
                foreach(string key in PrintButtons.Keys)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem();
                    tsmi.Name = key;
                    tsmi.Text = PrintButtons[key].Text;

                    tsmi.Click += Tsmi_Click;

                    btn_Print.DropDownItems.Add(tsmi);
                }

                if (DoWorkButtons != null)
                    foreach (string key in DoWorkButtons.Keys)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem();
                    tsmi.Name = key;
                    tsmi.Text = DoWorkButtons[key].Text;

                    tsmi.Click += Tsmi_Click;

                    btn_More.DropDownItems.Add(tsmi);
                }


                SetLabText(string.Empty);
                l_AppName.Text = FC.FormTitle;

            }
            catch (Exception EX) { MessageBox.Show(EX.Message); }
        }

        private void Tsmi_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem mi = (ToolStripMenuItem)sender;

                FunButtonClass fbc = null;

               
                if (DoWorkButtons != null && DoWorkButtons.ContainsKey(mi.Name.Split('_')[2]))
                {
                    fbc = DoWorkButtons[mi.Name.Split('_')[2]];
                }
                if (PrintButtons != null && PrintButtons.ContainsKey(mi.Name.Split('_')[2]))
                {
                    fbc = PrintButtons[mi.Name.Split('_')[2]];
                }
                Dictionary<string, object> P = new Dictionary<string, object>();
                Dictionary<string, string> P2 = new Dictionary<string, string>();

                if (fbc != null)
                    switch (fbc.Action)
                    {
                        case "AccessWeb":
                            System.Diagnostics.Process.Start(fbc.Url);
                            break;
                        case "RunApp":
                            foreach (string s in fbc.Parameters.Keys)
                            {
                                if (fbc.Parameters[s].StartsWith("HeadData."))
                                {
                                    try
                                    {
                                        P.Add(s, FC.DataSource.Tables["Table1"].ShowDataRow[fbc.Parameters[s].Replace("HeadData.", "")].ToString());
                                    }
                                    catch (Exception ex)
                                    {

                                        MessageBox.Show("没有数据，不能进行该操作！");
                                        return;
                                    }

                                }
                                else
                                {
                                    P.Add(s, fbc.Parameters[s]);
                                }
                            }

                            P.Add("Org", Program.Org);
                            P.Add("WebServiceUrl", Program.WebServiceUrl);


                            SJeMES_Framework.Common.OtherPrograms.RunApp(fbc.DllName, fbc.ClassName, fbc.Method, P);
                            return;
                        case "RunFastReport":
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            foreach (string s in fbc.Parameters.Keys)
                            {
                                if (fbc.Parameters[s].StartsWith("HeadData."))
                                {
                                    try
                                    {
                                        dic.Add(s, FC.DataSource.Tables["Table1"].ShowDataRow[fbc.Parameters[s].Replace("HeadData.", "")].ToString() + "*" + fbc.Parameters[s].Replace("HeadData.", ""));
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("没有数据，不能进行该操作！");
                                        return;
                                    }

                                }
                                else
                                {
                                    dic.Add(s, fbc.Parameters[s]);
                                }


                            }
                            FastReportForm.frmFastReport frmFR = new FastReportForm.frmFastReport(Program.Org, Program.WebServiceUrl, dic);
                            frmFR.Show();
                            return;
                        case "PrintFastReport":
                            string docNo = string.Empty;
                            string moduleNo = string.Empty;
                            string headDataKey = string.Empty;
                            foreach (string s in fbc.Parameters.Keys)
                            {
                                if (fbc.Parameters[s].StartsWith("HeadData."))
                                {
                                    try
                                    {
                                        docNo = FC.DataSource.Tables["Table1"].ShowDataRow[fbc.Parameters[s].Replace("HeadData.", "")].ToString();
                                        moduleNo = FC.DataSource.Tables["Table1"].TableName;
                                        headDataKey = fbc.Parameters[s].Replace("HeadData.", "");
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("没有数据，不能进行该操作！");
                                        return;
                                    }

                                }
                            }
                            FastReportForm.FrmReportMain frMain = new FastReportForm.FrmReportMain(docNo, moduleNo, headDataKey);
                            frMain.ShowDialog();
                            return;
                        case "RunService":
                            if (FC.DataSource.Tables["Table1"].ShowDataRow == null)
                                return;
                            foreach (string s in fbc.Parameters.Keys)
                            {
                                if (fbc.Parameters[s].StartsWith("HeadData."))
                                {
                                    P2.Add(s, FC.DataSource.Tables["Table1"].ShowDataRow[fbc.Parameters[s].Replace("HeadData.", "")].ToString());
                                }
                                else
                                {
                                    P2.Add(s, fbc.Parameters[s]);
                                }
                            }

                            P2.Add("UserCode", Program.Org.User.UserCode);


                            string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.Org, Program.WebServiceUrl, fbc.DllName, fbc.ClassName, fbc.Method, P2);
                            if (Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                            {

                                MessageBox.Show("操作成功");
                               
                                hpc.LoadDataWhereSql(" where id =" + FC.DataSource.Tables["Table1"].ShowDataRow["id"].ToString());

                            }

                            else
                            {
                                MessageBox.Show(Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                            }
                            return;
                        case "PrintBarCode":
                            string tmp = string.Empty;
                            string tmp2 = string.Empty;
                            foreach (string s in fbc.Parameters.Keys)
                            {
                                if (fbc.Parameters[s].StartsWith("HeadData."))
                                {
                                    tmp = FC.DataSource.Tables["Table1"].ShowDataRow[fbc.Parameters[s].Replace("HeadData.", "")].ToString();
                                    tmp2 = "@" + fbc.Parameters[s].Replace("HeadData.", "");
                                }

                            }

                            string sql = fbc.Parameters["SQL"];

                            sql = sql.Replace(tmp2, tmp);
                            //判断数据库是否有存储过程
                            if (Common.WebServiceHelper.GetDataTable(Program.Org, Program.WebServiceUrl,
                               "select * from dbo.sysobjects t where t.name='sp_PowerWarehouse'", new Dictionary<string, string>()).Rows.Count > 0)
                            {
                                sql = "exec sp_PowerWarehouse '" + sql.Replace("'", "''") + "','" + Program.Org.User.UserCode + "','material_no',''";
                            }
                            System.Data.DataTable dt = SJeMES_Framework.Common.WebServiceHelper.GetDataTable(Program.Org, Program.WebServiceUrl, sql, new Dictionary<string, string>());

                            //List<string> Data = new List<string>();
                            //foreach (System.Data.DataRow dr in dt.Rows)
                            //{
                            //    Data.Add(dr[0].ToString());
                            //}

                            SJeMES_Framework.Printer.frmBarCodePrinter frm = new SJeMES_Framework.Printer.frmBarCodePrinter(Program.Org, Program.WebServiceUrl, dt, fbc.Text);
                            frm.ShowDialog();
                            return;

                    }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void SetStatus(string Status)
        {

            this.Status = Status;


          
           hpc.SetStatus(Status);
            

            switch (Status)
            {
                case "Normal":
                    btn_Add.Enabled = true;
                    btn_Edit.Enabled = true;
                    btn_Del.Enabled = true;
                    btn_Save.Enabled = false;
                    btn_Cancel.Enabled = false;

                    btn_DoSure.Enabled = true;
                    btn_Audit.Enabled = true;
                    btn_Print.Enabled = true;

                    btn_First.Enabled = true;
                    btn_Back.Enabled = true;
                    btn_Next.Enabled = true;
                    btn_Last.Enabled = true;

                    btn_More.Enabled = true;
                    btn_Help.Enabled = true;
                    btn_Select.Enabled = true;
                    break;

                case "Add":
                    btn_Add.Enabled = false;
                    btn_Edit.Enabled = false;
                    btn_Del.Enabled = false;
                    btn_Save.Enabled = true;
                    btn_Cancel.Enabled = true;

                    btn_DoSure.Enabled = false;
                    btn_Audit.Enabled = false;
                    btn_Print.Enabled = false;

                    btn_First.Enabled = false;
                    btn_Back.Enabled = false;
                    btn_Next.Enabled = false;
                    btn_Last.Enabled = false;

                    btn_More.Enabled = false;
                    btn_Help.Enabled = true;
                    btn_Select.Enabled = false;

                    break;

                case "Edit":
                    btn_Add.Enabled = false;
                    btn_Edit.Enabled = false;
                    btn_Del.Enabled = false;
                    btn_Save.Enabled = true;
                    btn_Cancel.Enabled = true;

                    btn_DoSure.Enabled = false;
                    btn_Audit.Enabled = false;
                    btn_Print.Enabled = false;

                    btn_First.Enabled = false;
                    btn_Back.Enabled = false;
                    btn_Next.Enabled = false;
                    btn_Last.Enabled = false;

                    btn_More.Enabled = false;
                    btn_Help.Enabled = true;
                    btn_Select.Enabled = false;

                    break;
            }

        }



        private void FormH_Load(object sender, EventArgs e)
        {
            hpc = (FormPanel.HeadPanelClass)FC.FormPanels["Panel1"];

            this.Table = FC.DataSource.Tables["Table1"];


            SetStatus("Normal");

          

        }

        private void FormH_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void SetLabText(string msg)
        {
            l_AppCode.Text = FC.FormCode + ":" + msg;
        }

        private void txt_SelectText_KeyPress(object sender, KeyPressEventArgs e)
        {
            ToolStripTextBox t = sender as ToolStripTextBox;
            if (e.KeyChar == 13)
            {
                if (!string.IsNullOrEmpty(t.Text.Trim()))
                {



                    CommonForm.frmSearchData frm = new CommonForm.frmSearchData(FC.Org, Program.WebServiceUrl, Table.SearchSQL,
                        Table.OrderKeys, Table.OrderTypes, true, false);
                    frm.SetWhereKey(t.Text.Trim());
                    frm.SelectData();
                    frm.ShowDialog();

                    DateTime d1 = DateTime.Now;

                    string sTmp = string.Empty;

                    List<string> sRows = Common.StringHelper.GetDataFromTag(frm.ReturnDataXML, "<Row>", "</Row>");

                    if (sRows.Count > 0)
                    {
                        string Wheresql = string.Empty;

                        for (int i = 0; i < Table.Keys.Count; i++)
                        {
                            sTmp = Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<" + Table.RetrunKeys[i] + ">", "</" + Table.RetrunKeys[i] + ">");

                            if (string.IsNullOrEmpty(Wheresql))
                            {
                                if (!string.IsNullOrEmpty(sTmp))
                                    Wheresql += " WHERE [" + Table.Keys[i] + "] ='" + sTmp + @"' ";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(sTmp))
                                    Wheresql += " AND [" + Table.Keys[i] + "] ='" + sTmp + @"' ";
                            }
                        }



                        System.Data.DataRow dr = Table.GetDataTableRow(FC.Org, Program.WebServiceUrl, Wheresql);

                        //判断是否为表身查询
                        if (string.IsNullOrEmpty(frm.bodyStr))
                            ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(dr);
                        else
                            ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(dr, frm.bodyStr, frm.pageName);


                        DateTime d2 = DateTime.Now;

                        this.SetLabText("加载数据用时" + d2.Subtract(d1).TotalSeconds.ToString() + @"秒");



                    }

                }
            }
        }




        private void B_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> P = new Dictionary<string, string>();
            string XML = string.Empty;

            #region 上一条
            if (((ToolStripButton)sender).Name == "btn_Back" && Table.DataRow != null)
            {
                DateTime d1 = DateTime.Now;

                string Where = "WHERE " + Table.Keys[0] + "<'" + Table.DataRow[Table.Keys[0]].ToString() + @"' ";
                Where += " ORDER BY " + Table.Keys[0] + " DESC";
                System.Data.DataRow dr = Table.GetDataTableRow(Program.Org, Program.WebServiceUrl, Where);
                hpc.SetData(dr);

                DateTime d2 = DateTime.Now;

                this.SetLabText("加载数据用时" + d2.Subtract(d1).TotalSeconds.ToString() + @"秒");

            }
            #endregion

            #region 第一体
            else if (((ToolStripButton)sender).Name == "btn_First")
            {
                DateTime d1 = DateTime.Now;

                string Where = " ";
                Where += " ORDER BY " + Table.Keys[0] + " ASC";
                System.Data.DataRow dr = Table.GetDataTableRow(Program.Org, Program.WebServiceUrl, Where);
                hpc.SetData(dr);

                DateTime d2 = DateTime.Now;

                this.SetLabText("加载数据用时" + d2.Subtract(d1).TotalSeconds.ToString() + @"秒");

            }
            #endregion

            #region 下一条
            else if (((ToolStripButton)sender).Name == "btn_Next" && Table.DataRow != null)
            {
                DateTime d1 = DateTime.Now;

                string Where = "WHERE " + Table.Keys[0] + ">'" + Table.DataRow[Table.Keys[0]].ToString() + @"' ";
                Where += " ORDER BY " + Table.Keys[0] + " ASC";
                System.Data.DataRow dr = Table.GetDataTableRow(Program.Org, Program.WebServiceUrl, Where);
                hpc.SetData(dr);

                DateTime d2 = DateTime.Now;

                this.SetLabText("加载数据用时" + d2.Subtract(d1).TotalSeconds.ToString() + @"秒");

            }
            #endregion

            #region 最后一条
            else if (((ToolStripButton)sender).Name == "btn_Last")
            {
                DateTime d1 = DateTime.Now;


                hpc.LoadLastRow();

                DateTime d2 = DateTime.Now;

                this.SetLabText("加载数据用时" + d2.Subtract(d1).TotalSeconds.ToString() + @"秒");

            }
            #endregion


            #region 查询
            else if (((ToolStripButton)sender).Name == "btn_Select")
            {


                string sql = string.Empty;

                if (!string.IsNullOrEmpty(txt_SelectText.Text.Trim()))
                {



                    CommonForm.frmSearchData frm = new CommonForm.frmSearchData(FC.Org, Program.WebServiceUrl, Table.SearchSQL,
                        Table.OrderKeys, Table.OrderTypes, true, false);
                    frm.SetWhereKey(txt_SelectText.Text.Trim());
                    frm.SelectData();
                    frm.ShowDialog();

                    DateTime d1 = DateTime.Now;

                    string sTmp = string.Empty;

                    List<string> sRows = Common.StringHelper.GetDataFromTag(frm.ReturnDataXML, "<Row>", "</Row>");

                    if (sRows.Count > 0)
                    {
                        string Wheresql = string.Empty;

                        for (int i = 0; i < Table.Keys.Count; i++)
                        {
                            sTmp = Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<" + Table.RetrunKeys[i] + ">", "</" + Table.RetrunKeys[i] + ">");

                            if (string.IsNullOrEmpty(Wheresql))
                            {
                                if (!string.IsNullOrEmpty(sTmp))
                                    Wheresql += " WHERE [" + Table.Keys[i] + "] ='" + sTmp + @"' ";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(sTmp))
                                    Wheresql += " AND [" + Table.Keys[i] + "] ='" + sTmp + @"' ";
                            }
                        }



                        System.Data.DataRow dr = Table.GetDataTableRow(FC.Org, Program.WebServiceUrl, Wheresql);

                        //判断是否为表身查询
                        if (string.IsNullOrEmpty(frm.bodyStr))
                            hpc.SetData(dr);
                        else
                            hpc.SetData(dr, frm.bodyStr, frm.pageName);


                        DateTime d2 = DateTime.Now;

                        this.SetLabText("加载数据用时" + d2.Subtract(d1).TotalSeconds.ToString() + @"秒");



                    }

                }
                else
                {
                    if (Table.SearchSQL.ToLower().Contains("where"))
                    {
                        sql = Table.SearchSQL + Table.SqlWhere;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Table.SqlWhere))
                        {
                            sql = Table.SearchSQL + " where 1=1 " + Table.SqlWhere;
                        }
                        else
                        {
                            sql = Table.SearchSQL;
                        }
                    }


                    CommonForm.frmSearchData frm = new CommonForm.frmSearchData(FC.Org, Program.WebServiceUrl, sql,
                        Table.OrderKeys, Table.OrderTypes, true, false);

                    frm.SetFC(FC);
                    frm.ShowDialog();

                    string sTmp = string.Empty;

                    List<string> sRows = Common.StringHelper.GetDataFromTag(frm.ReturnDataXML, "<Row>", "</Row>");

                    DateTime d1 = DateTime.Now;


                    if (sRows.Count > 0)
                    {
                        string Wheresql = string.Empty;

                        for (int i = 0; i < Table.Keys.Count; i++)
                        {
                            sTmp = Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<" + Table.RetrunKeys[i] + ">", "</" + Table.RetrunKeys[i] + ">");

                            if (string.IsNullOrEmpty(Wheresql))
                            {
                                if (!string.IsNullOrEmpty(sTmp))
                                    Wheresql += " WHERE [" + Table.Keys[i] + "] ='" + sTmp + @"' ";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(sTmp))
                                    Wheresql += " AND [" + Table.Keys[i] + "] ='" + sTmp + @"' ";
                            }
                        }



                        System.Data.DataRow dr = Table.GetDataTableRow(FC.Org, Program.WebServiceUrl, Wheresql);

                        //含表身查询
                        if (string.IsNullOrEmpty(frm.bodyStr))
                            ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(dr);
                        else
                            ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(dr, frm.bodyStr, frm.pageName);

                        DateTime d2 = DateTime.Now;

                        this.SetLabText("加载数据用时" + d2.Subtract(d1).TotalSeconds.ToString() + @"秒");
                    }
                }

            }
            #endregion

            #region 新增
            else if (((ToolStripButton)sender).Name == "btn_Add")
            {


                SetStatus("Add");


            }
            #endregion


            #region 编辑
            else if (((ToolStripButton)sender).Name == "btn_Edit")
            {


                if (Table.ShowDataRow != null &&
                    Table.ShowDataRow.Table.Columns.Contains("dosureby") &&
                    (!string.IsNullOrEmpty(Table.ShowDataRow["dosureby"].ToString())
                    || !string.IsNullOrEmpty(Table.ShowDataRow["auditby"].ToString())))
                {
                    MessageBox.Show("数据已确认或已审核，不能修改");
                    return;
                }

                if (Table.DataRow != null)
                    SetStatus("Edit");


            }
            #endregion


            #region 取消
            else if (((ToolStripButton)sender).Name == "btn_Cancel")
            {
                if (Table.DataRow != null)
                {
                    ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(Table.DataRow);
                }
                else
                {
                    hpc.LoadLastRow();
                }
                SetStatus("Normal");
            }
            #endregion

            #region 帮助
            else if (((ToolStripButton)sender).Name == "btn_Help")
            {
                System.Diagnostics.Process.Start(Program.WebServiceUrl.Replace("SJ-WebService.asmx", "") + @"/BIN/HLP/" + this.FC.FormCode + @".pdf");

                return;
            }
            #endregion

            #region 保存
            else if (((ToolStripButton)sender).Name == "btn_Save")
            {

                if (MessageBox.Show("是否保存数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    if (Status == "Add")
                    {
                        Table.GetNewRow(FC.Org, Program.WebServiceUrl);

                    

                        foreach (string key in hpc.Childrens.Keys)
                        {
                            Control.ControlClass cc = hpc.Childrens[key];

                            if (cc.ControlType == "TextBox")
                            {
                                Control.TextBoxClass tbc = (Control.TextBoxClass)cc;

                                if (!tbc.IsNull && string.IsNullOrEmpty(tbc.Value))
                                {
                                    MessageBox.Show(tbc.Title + "不能为空");
                                    return;
                                }

                                Table.ShowDataRow[tbc.DataKey] = tbc.Value;

                            }
                        }

                        if (Table.ShowDataRow.Table.Columns.Contains("org"))
                        {
                            Table.ShowDataRow["org"] = FC.Org.Org;
                        }
                        if (Table.ShowDataRow.Table.Columns.Contains("createby"))
                        {
                            Table.ShowDataRow["createby"] = FC.Org.User.UserCode;
                        }
                        if (Table.ShowDataRow.Table.Columns.Contains("createdate"))
                        {
                            Table.ShowDataRow["createdate"] = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        if (Table.ShowDataRow.Table.Columns.Contains("createtime"))
                        {
                            Table.ShowDataRow["createtime"] = DateTime.Now.ToString("HH:mm:ss");
                        }

                        foreach (string key in hpc.Childrens.Keys)
                        {
                            Control.ControlClass cc = hpc.Childrens[key];

                            if (cc.ControlType == "TextBox")
                            {
                                Control.TextBoxClass tbc = (Control.TextBoxClass)cc;
                                if (tbc.DefaultValue.ToLower() == "sorting")
                                {
                                    string sorting = string.Empty;
                                    string where = string.Empty;
                                    foreach (string s in Table.Keys)
                                    {
                                        if (s != tbc.DataKey)
                                        {
                                            where += " AND [" + s + "] ='" + Table.ShowDataRow[s].ToString() + @"' ";
                                        }
                                    }

                                    string sql = @"
SELECT ISNULL(MAX([" + tbc.DataKey + @"]),0)
FROM [" + this.Table.TableName + @"]
WHERE 1=1 " + where + @" 
";
                                    int tmp = Convert.ToInt32(Common.WebServiceHelper.GetString(FC.Org, Program.WebServiceUrl, sql, new Dictionary<string, string>()));

                                    sorting = (tmp + 1).ToString("0000");

                                    Table.ShowDataRow[tbc.DataKey] = sorting;
                                }
                            }

                            if (cc.ControlType == "TextBox")
                            {
                                Control.TextBoxClass tbc = (Control.TextBoxClass)cc;
                                if (tbc.DefaultValue.ToLower() == "auto_docno")
                                {
                                    string doc_no = string.Empty;


                                    string sql = @"
SELECT ISNULL(MAX([" + this.Table.Keys[0] + @"]),0)
FROM [" + this.Table.TableName + @"]
WHERE [" + this.Table.Keys[0] + @"] LIKE '" + DateTime.Now.ToString("yyyyMMdd") + @"%'
";
                                    string tmp = Common.WebServiceHelper.GetString(FC.Org, Program.WebServiceUrl, sql, new Dictionary<string, string>());

                                    doc_no = DateTime.Now.ToString("yyyyMMdd") + (Convert.ToUInt32(tmp.Replace(DateTime.Now.ToString("yyyyMMdd"), "")) + 1).ToString("000");

                                    Table.ShowDataRow[tbc.DataKey] = doc_no;
                                }
                            }
                        }

                        if (Table.AddRow(FC.Org, Program.WebServiceUrl))
                        {
                            ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(Table.ShowDataRow);
                        }

                        else
                        {
                            ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).LoadLastRow();
                        }

                    }
                    else if (Status == "Edit")
                    {

                        

                        foreach (string key in hpc.Childrens.Keys)
                        {
                            Control.ControlClass cc = hpc.Childrens[key];

                            if (cc.ControlType == "TextBox")
                            {
                                Control.TextBoxClass tbc = (Control.TextBoxClass)cc;


                                if (!tbc.IsNull && string.IsNullOrEmpty(tbc.Value))
                                {
                                    MessageBox.Show(tbc.Title + "不能为空");
                                    return;
                                }

                                Table.ShowDataRow[tbc.DataKey] = tbc.Value;
                            }
                        }

                        if (Table.ShowDataRow.Table.Columns.Contains("org"))
                        {
                            Table.ShowDataRow["org"] = FC.Org.Org;
                        }
                        if (Table.ShowDataRow.Table.Columns.Contains("modifyby"))
                        {
                            Table.ShowDataRow["modifyby"] = FC.Org.User.UserCode;
                        }
                        if (Table.ShowDataRow.Table.Columns.Contains("modifydate"))
                        {
                            Table.ShowDataRow["modifydate"] = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        if (Table.ShowDataRow.Table.Columns.Contains("modifytime"))
                        {
                            Table.ShowDataRow["modifytime"] = DateTime.Now.ToString("HH:mm:ss");
                        }

                        if (Table.EditRow(FC.Org, Program.WebServiceUrl))
                        {
                            hpc.SetData(Table.ShowDataRow);
                        }
                        else
                        {
                            hpc.LoadLastRow();
                        }

                    }
                    SetStatus("Normal");
                }


            }
            #endregion

            #region 删除
            else if (((ToolStripButton)sender).Name == "btn_Del")
            {


                if (Table.ShowDataRow != null &&
                    Table.ShowDataRow.Table.Columns.Contains("dosureby") &&
                    (!string.IsNullOrEmpty(Table.ShowDataRow["dosureby"].ToString())
                    || !string.IsNullOrEmpty(Table.ShowDataRow["auditby"].ToString())))
                {
                    MessageBox.Show("数据已确认或已审核，不能删除");
                    return;
                }





                if (Table.ShowDataRow != null
                    && !string.IsNullOrEmpty(Table.ShowDataRow["id"].ToString())
                        && MessageBox.Show("是否删除本条数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    if (FC.PanelsCount > 1)
                        for (int i = 2; i <= FC.PanelsCount; i++)
                        {
                            if (!string.IsNullOrEmpty(FC.DataSource.Tables["Table" + i].TableName))
                            {
                                SJeMES_Framework.WinForm.FormXML.FormPanel.BodyPanelClass BPC =
                                   FC.FormPanels["Panel" + i] as SJeMES_Framework.WinForm.FormXML.FormPanel.BodyPanelClass;
                                SJeMES_Framework.WinForm.FormXML.Control.DataViewClass DVC =
                                    ((SJeMES_Framework.WinForm.FormXML.Control.DataViewClass)BPC.Childrens["Children1"]);

                                string where = "WHERE 1=1";

                                foreach (string s in DVC.HeadKeys)
                                {
                                    string HeadDataKey = s.Split(':')[0];
                                    string BodyDataKey = s.Split(':')[1];

                                    where += " AND [" + BodyDataKey + @"]='" + Table.ShowDataRow[HeadDataKey].ToString() + @"' ";
                                }

                                FC.DataSource.Tables["Table" + i].DelRow(FC.Org, Program.WebServiceUrl, where);

                            }


                        }


                    //删表头
                    Table.DelRow(FC.Org, Program.WebServiceUrl);
                    Table.GetDataFisrtRow(FC.Org, Program.WebServiceUrl);
                    hpc.SetData(Table.ShowDataRow);

                    SetStatus("Normal");
                }



            }
            #endregion

            #region 确认/反确认数据
            else if (((ToolStripButton)sender).Name == "btn_DoSure")
            {
                if (Table.ShowDataRow != null &&
                   Table.ShowDataRow.Table.Columns.Contains("dosureby") &&
                   !string.IsNullOrEmpty(Table.ShowDataRow["auditby"].ToString()))
                {
                    MessageBox.Show("该数据已经进行审核，不能再进行操作！");
                    return;
                }

                if (Table.ShowDataRow != null &&
               Table.ShowDataRow.Table.Columns.Contains("dosureby") &&
               !string.IsNullOrEmpty(Table.ShowDataRow["dosureby"].ToString()))
                {
                    if (MessageBox.Show("是否需要反确认数据？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        P = new Dictionary<string, string>();
                        P.Add("TableName", FC.DataSource.Tables["Table1"].TableName);
                        P.Add("id", FC.DataSource.Tables["Table1"].ShowDataRow["id"].ToString());
                        P.Add("UserCode", Program.Org.User.UserCode);


                        XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.Org, Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.SYS", "NoDoSure", P);
                        if (Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                        {

                            MessageBox.Show("操作成功");

                            hpc.LoadDataWhereSql(" where id =" + FC.DataSource.Tables["Table1"].ShowDataRow["id"].ToString());

                        }
                        else
                        {
                            MessageBox.Show(Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                        }
                    }


                }

                else if (Table.ShowDataRow != null &&
               Table.ShowDataRow.Table.Columns.Contains("dosureby") &&
               string.IsNullOrEmpty(Table.ShowDataRow["dosureby"].ToString()))
                {

                    if (MessageBox.Show("是否需要确认数据？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        P = new Dictionary<string, string>();
                        P.Add("TableName", FC.DataSource.Tables["Table1"].TableName);
                        P.Add("id", FC.DataSource.Tables["Table1"].ShowDataRow["id"].ToString());
                        P.Add("UserCode", Program.Org.User.UserCode);


                        XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.Org, Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.SYS", "DoSure", P);
                        if (Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                        {

                            MessageBox.Show("操作成功");

                            hpc.LoadDataWhereSql(" where id =" + FC.DataSource.Tables["Table1"].ShowDataRow["id"].ToString());

                        }
                        else
                        {
                            MessageBox.Show(Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                        }

                    }
                }
                #endregion



            else if (((ToolStripButton)sender).Name == "btn_Audit")
            {
            }


            }

        }
    }

}
