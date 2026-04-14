using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.FormDesign.Control
{
    public class ToolsBarClass: ControlClass
    {

        public bool Enable;
        public int ButtonsCount;
        public int FunButtonsCount;
        public int PrintButtonsCount;
        public int DoWorkButtonsCount;
        public Dictionary<string, ButtonClass> Buttons;
        public Dictionary<string, FunButtonClass> PrintButtons;
        public Dictionary<string, FunButtonClass> DoWorkButtons;
        public Dictionary<string, FunButtonClass> FunButtons;
        public ContextMenu FunMenu;
        public ContextMenu PrintMenu;
        public ContextMenu DoWorkMenu;
        public bool PageControlEnable;
        public PageControlClass PageControl;
        public string WebServiceUrl;
        public Class.OrgClass Org;

        public ToolsBarClass(string XML,string WebServiceUrl,Class.OrgClass Org)
        {
            try
            {
                this.Org = Org;

                this.WebServiceUrl = WebServiceUrl;
                ControlType = Common.StringHelper.GetDataFromFirstTag(XML, "<ControlType>", "</ControlType>");
                Enable = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<Enable>", "</Enable>"));
                ButtonsCount = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<ButtonsCount>", "</ButtonsCount>"));
                if (XML.IndexOf("<FunButtonsCount>") > -1)
                {
                    FunButtonsCount = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<FunButtonsCount>", "</FunButtonsCount"));
                }
                if (XML.IndexOf("<DoWorkButtonsCount>") > -1)
                {
                    DoWorkButtonsCount = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<DoWorkButtonsCount>", "</DoWorkButtonsCount"));
                }
                if (XML.IndexOf("<PrintButtonsCount>") > -1)
                {
                    PrintButtonsCount = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<PrintButtonsCount>", "</PrintButtonsCount"));
                }
                PageControlEnable = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<PageControlEnable>", "</PageControlEnable>"));

                if(PageControlEnable)
                {
                    this.PageControl = new FormDesign.Control.PageControlClass();
                }

                string xmlButtons = Common.StringHelper.GetDataFromFirstTag(XML, "<Buttons>", "</Buttons>");

                Buttons = new Dictionary<string, ButtonClass>();
                for(int i= ButtonsCount; i>=1;i--)
                {
                    string xmlButton = Common.StringHelper.GetDataFromFirstTag(xmlButtons, "<Button" + i + ">", "</Button" + i + ">");

                    Buttons.Add("Button" + i, new ButtonClass(xmlButton));
                }

                if (XML.IndexOf("<FunButtons>") > -1)
                {
                    string xmlFunButtons = Common.StringHelper.GetDataFromFirstTag(XML, "<FunButtons>", "</FunButtons>");

                    FunButtons = new Dictionary<string, FormDesign.Control.FunButtonClass>();
                    for (int i = FunButtonsCount; i >= 1; i--)
                    {
                        string xmlFunButton = Common.StringHelper.GetDataFromFirstTag(xmlFunButtons, "<FunButton" + i + ">", "</FunButton" + i + ">");

                        FunButtons.Add("FunButton" + i, new FunButtonClass(xmlFunButton));
                    }
                }
                if (XML.IndexOf("<PrintButtons>") > -1)
                {
                    string xmlPrintButtons = Common.StringHelper.GetDataFromFirstTag(XML, "<PrintButtons>", "</PrintButtons>");

                    PrintButtons = new Dictionary<string, FormDesign.Control.FunButtonClass>();
                    for (int i = PrintButtonsCount; i >= 1; i--)
                    {
                        string xmlPrintButton = Common.StringHelper.GetDataFromFirstTag(xmlPrintButtons, "<PrintButton" + i + ">", "</PrintButton" + i + ">");

                        PrintButtons.Add("PrintButton" + i, new FunButtonClass(xmlPrintButton));
                    }
                }
                if (XML.IndexOf("<DoWorkButtons>") > -1)
                {
                    string xmlDoWorkButtons = Common.StringHelper.GetDataFromFirstTag(XML, "<DoWorkButtons>", "</DoWorkButtons>");

                    DoWorkButtons = new Dictionary<string, FormDesign.Control.FunButtonClass>();
                    for (int i = DoWorkButtonsCount; i >= 1; i--)
                    {
                        string xmlDoWorkButton = Common.StringHelper.GetDataFromFirstTag(xmlDoWorkButtons, "<DoWorkButton" + i + ">", "</DoWorkButton" + i + ">");

                        DoWorkButtons.Add("DoWorkButton" + i, new FunButtonClass(xmlDoWorkButton));
                    }
                }
            }
            catch { }
        }

        public Panel GetControls(string ParentName,Forms.FormClass FC)
        {
            this.ParentName = ParentName;
            this.FC = FC;
            Panel panelRet = new Panel();
            try
            {
                this.Control = panelRet;
                this.ControlName = panelRet.Name = ParentName + "_ToolsBar";
                panelRet.Dock = DockStyle.Top;
                panelRet.BorderStyle = BorderStyle.FixedSingle;
                panelRet.Height = 35;

                if (ParentName.Contains("HeadPanel"))
                {
                    Panel p = new Panel();
                    p.Width = 250;
                    p.Dock = DockStyle.Left;

                    TextBox txtWhereKey = new TextBox();
                    txtWhereKey.Name = "txtWhereKey";
                    txtWhereKey.Width = 150;
                    txtWhereKey.Top = 5;
                    txtWhereKey.Left = 85;
                    txtWhereKey.Text = "";

                    txtWhereKey.KeyPress += TxtWhereKey_KeyPress;


                    p.Controls.Add(txtWhereKey);

                    Label l = new Label();
                    l.Text = "模糊查询：";
                    l.Left = 20;
                    l.Top = 8;

                      p.Controls.Add(l);

                    panelRet.Controls.Add(p);
                }

                foreach (string key in this.Buttons.Keys)
                {
                    ButtonClass bc = this.Buttons[key];

                    if (bc.Enable)
                    {
                        Button b = bc.GetControls(panelRet.Name, FC);

                        b.Click += B_Click;

                        panelRet.Controls.Add(b);
                    }
                    
                }


                if (ParentName.Contains("HeadPanel"))
                {
                    Button btnNext = new Button();
                    btnNext.Name = "btnNext";
                    btnNext.Text = ">";
                    btnNext.Dock = DockStyle.Left;
                    btnNext.Width = 40;
                    btnNext.Height = 20;
                    btnNext.Font = new System.Drawing.Font("微软雅黑", 9);
                    btnNext.ForeColor = System.Drawing.Color.Black;
                    btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    btnNext.Click += B_Click;
                    panelRet.Controls.Add(btnNext);

                    Button btnBack = new Button();
                    btnBack.Name = "btnBack";
                    btnBack.Text = "<";
                    btnBack.Dock = DockStyle.Left;
                    btnBack.Width = 40;
                    btnBack.Height = 20;
                    btnBack.Font = new System.Drawing.Font("微软雅黑", 9);
                    btnBack.ForeColor = System.Drawing.Color.Black;
                    btnBack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    btnBack.Click += B_Click;
                    panelRet.Controls.Add(btnBack);
                }

            }
            catch { }

            this.FunMenu = new ContextMenu();
            this.FunMenu.Name = this.ParentName + "_FunMenu";
            
            for(int i=1;i<=this.FunButtonsCount;i++)
            {
                MenuItem mi = new MenuItem();
                mi.Name = this.ParentName + "_FunButton" + i;

                mi.Text = FunButtons["FunButton" + i].Text;
                mi.Click += Mi_Click;
                this.FunMenu.MenuItems.Add(mi);
            }

            this.PrintMenu = new ContextMenu();
            this.PrintMenu.Name = this.ParentName + "_PrintMenu";

            for (int i = 1; i <= this.PrintButtonsCount; i++)
            {
                MenuItem mi = new MenuItem();
                mi.Name = this.ParentName + "_PrintButton" + i;

                mi.Text = PrintButtons["PrintButton" + i].Text;
                mi.Click += Mi_Click;
                this.PrintMenu.MenuItems.Add(mi);
            }

            this.DoWorkMenu = new ContextMenu();
            this.DoWorkMenu.Name = this.ParentName + "_DoWorkMenu";

            for (int i = 1; i <= this.DoWorkButtonsCount; i++)
            {
                MenuItem mi = new MenuItem();
                mi.Name = this.ParentName + "_DoWorkButton" + i;

                mi.Text = DoWorkButtons["DoWorkButton" + i].Text;
                mi.Click += Mi_Click;
                this.DoWorkMenu.MenuItems.Add(mi);
            }

            if (PageControlEnable)
            {
                panelRet.Controls.Add(this.PageControl.GetControls(panelRet.Name, this.FC));
            }


            return panelRet;
        }

        private void TxtWhereKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;
            if(e.KeyChar ==13)
            {
                if (!string.IsNullOrEmpty(t.Text.Trim()))
                {
                    if (Program.Org.User.Permissions.ContainsKey(FC.FormCode) && !Program.Org.User.Permissions[FC.FormCode]["Select"])
                    {
                        MessageBox.Show("当前用户没有该权限");
                        return;
                    }


                    CommonForm.frmSearchData frm = new CommonForm.frmSearchData(Org, WebServiceUrl, FC.DataSource.Tables["Table1"].SearchSQL,
                        FC.DataSource.Tables["Table1"].OrderKeys, FC.DataSource.Tables["Table1"].OrderTypes, true, false);
                    frm.SetWhereKey(t.Text.Trim());
                    frm.SelectData();
                    frm.ShowDialog();

                    DateTime d1 = DateTime.Now;

                    string sTmp = string.Empty;

                    List<string> sRows = Common.StringHelper.GetDataFromTag(frm.ReturnDataXML, "<Row>", "</Row>");

                    if (sRows.Count > 0)
                    {
                        string Wheresql = string.Empty;

                        for (int i = 0; i < FC.DataSource.Tables["Table1"].Keys.Count; i++)
                        {
                            sTmp = Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<" + FC.DataSource.Tables["Table1"].RetrunKeys[i] + ">", "</" + FC.DataSource.Tables["Table1"].RetrunKeys[i] + ">");

                            if (string.IsNullOrEmpty(Wheresql))
                            {
                               if(!string.IsNullOrEmpty(sTmp))
                                Wheresql += " WHERE [" + FC.DataSource.Tables["Table1"].Keys[i] + "] ='" + sTmp + @"' ";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(sTmp))
                                    Wheresql += " AND [" + FC.DataSource.Tables["Table1"].Keys[i] + "] ='" + sTmp + @"' ";
                            }
                        }



                        System.Data.DataRow dr = FC.DataSource.Tables["Table1"].GetDataTableRow(Org, WebServiceUrl, Wheresql);

                        //判断是否为表身查询
                        if (string.IsNullOrEmpty(frm.bodyStr))
                           ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(dr);
                        else
                           ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(dr, frm.bodyStr,frm.pageName);


                        DateTime d2 = DateTime.Now;

                        ((FormDesignTMP)((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).Control.FindForm()).SetLabText("加载数据用时" + d2.Subtract(d1).TotalSeconds.ToString() + @"秒");



                    }

                }
            }
        }

        private void Mi_Click(object sender, EventArgs e)
        {
            try
            {
                MenuItem mi = (MenuItem)sender;

                FunButtonClass fbc = null;

                if (FunButtons!=null&&FunButtons.ContainsKey(mi.Name.Split('_')[2]))
                {
                    fbc = FunButtons[mi.Name.Split('_')[2]];
                }
                if (DoWorkButtons!=null&& DoWorkButtons.ContainsKey(mi.Name.Split('_')[2]))
                {
                    fbc = DoWorkButtons[mi.Name.Split('_')[2]];
                }
                if (PrintButtons!=null&&PrintButtons.ContainsKey(mi.Name.Split('_')[2]))
                {
                    fbc = PrintButtons[mi.Name.Split('_')[2]];
                }
                Dictionary<string, object> P = new Dictionary<string, object>();
                Dictionary<string, string> P2 = new Dictionary<string, string>();

                if (fbc.Text == "确认单据")
                {
                    try
                    {
                        if (!Program.Org.User.Permissions[FC.FormCode]["DoSure"])
                        {
                            MessageBox.Show("当前用户没有该权限");
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("当前用户没有该权限");
                        return;
                    }
                   
                }
                else if (fbc.Text == "取消确认单据")
                {
                    try
                    {
                        if (!Program.Org.User.Permissions[FC.FormCode]["DoSure"])
                        {
                            MessageBox.Show("当前用户没有该权限");
                            return;
                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("当前用户没有该权限");
                        return;
                    }
                       
                    
                }

                else  if (fbc.Text == "审核单据")
                {
                    try
                    {
                        if (!Program.Org.User.Permissions[FC.FormCode]["Audit"])
                        {
                            MessageBox.Show("当前用户没有该权限");
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("当前用户没有该权限");
                        return;
                    }
                       
                    
                }

                if (fbc.Text == "确认单据")
                {
                    if (FC.DataSource.Tables["Table1"].ShowDataRow == null)
                        return;
                    P2.Add("TableName", FC.DataSource.Tables["Table1"].TableName);
                    P2.Add("id", FC.DataSource.Tables["Table1"].ShowDataRow["id"].ToString());
                    P2.Add("UserCode", Program.Org.User.UserCode);


                    string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.Org, Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.SYS", "DoSure", P2);
                    if (Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                    {

                        MessageBox.Show("操作成功");
                        SJeMES_Framework.WinForm.FormDesign.FormPanel.HeadPanelClass HPC = this.FC.FormPanels["Panel1"] as SJeMES_Framework.WinForm.FormDesign.FormPanel.HeadPanelClass;
                        HPC.LoadDataWhereSql(" where id =" + FC.DataSource.Tables["Table1"].ShowDataRow["id"].ToString());
                    
                    }
                    else
                    {
                        MessageBox.Show(Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                    }

                    return;
                }
                if (fbc.Text == "取消确认单据")
                {
                    if (FC.DataSource.Tables["Table1"].ShowDataRow == null)
                        return;
                    P2.Add("TableName", FC.DataSource.Tables["Table1"].TableName);
                    P2.Add("id", FC.DataSource.Tables["Table1"].ShowDataRow["id"].ToString());
                    P2.Add("UserCode", Program.Org.User.UserCode);


                    string XML = SJeMES_Framework.Common.WebServiceHelper.RunService(Program.Org, Program.WebServiceUrl, "SJ_MESAPI", "SJ_MESAPI.SYS", "NoDoSure", P2);
                    if (Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<IsSuccess>", "</IsSuccess>")))
                    {

                        MessageBox.Show("操作成功");
                        SJeMES_Framework.WinForm.FormDesign.FormPanel.HeadPanelClass HPC = this.FC.FormPanels["Panel1"] as SJeMES_Framework.WinForm.FormDesign.FormPanel.HeadPanelClass;
                        HPC.LoadDataWhereSql(" where id =" + FC.DataSource.Tables["Table1"].ShowDataRow["id"].ToString());

                    }
                    else
                    {
                        MessageBox.Show(Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>"));
                    }

                    return;
                }
                if (fbc.Text == "关于")
                {
                    System.Diagnostics.Process.Start(Program.WebServiceUrl.Replace("SJ-WebService.asmx","")+@"/BIN/HLP/"+this.FC.FormCode+@".pdf");

                    return;
                }

                else if (fbc != null)
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
                                        docNo= FC.DataSource.Tables["Table1"].ShowDataRow[fbc.Parameters[s].Replace("HeadData.", "")].ToString();
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
                                SJeMES_Framework.WinForm.FormDesign.FormPanel.HeadPanelClass HPC = this.FC.FormPanels["Panel1"] as SJeMES_Framework.WinForm.FormDesign.FormPanel.HeadPanelClass;
                                HPC.LoadDataWhereSql(" where id =" + FC.DataSource.Tables["Table1"].ShowDataRow["id"].ToString());

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
                           if(Common.WebServiceHelper.GetDataTable(Program.Org, Program.WebServiceUrl,
                              "select * from dbo.sysobjects t where t.name='sp_PowerWarehouse'", new Dictionary<string, string>()).Rows.Count>0)
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

        private void B_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Name == "btnBack" && FC.DataSource.Tables["Table1"].DataRow !=null)
            {
                DateTime d1 = DateTime.Now;

                string Where ="WHERE "+   FC.DataSource.Tables["Table1"].Keys[0] +"<'"+ FC.DataSource.Tables["Table1"].DataRow[FC.DataSource.Tables["Table1"].Keys[0]].ToString()+@"' ";
                Where += " ORDER BY " + FC.DataSource.Tables["Table1"].Keys[0] + " DESC";
                System.Data.DataRow dr = FC.DataSource.Tables["Table1"].GetDataTableRow(Program.Org, Program.WebServiceUrl, Where);
                ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(dr);

                DateTime d2 = DateTime.Now;

                ((FormDesignTMP)((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).Control.FindForm()).SetLabText("加载数据用时" + d2.Subtract(d1).TotalSeconds.ToString() + @"秒");

            }

            if (((Button)sender).Name == "btnNext" && FC.DataSource.Tables["Table1"].DataRow != null)
            {
                DateTime d1 = DateTime.Now;

                string Where = "WHERE " + FC.DataSource.Tables["Table1"].Keys[0] + ">'" + FC.DataSource.Tables["Table1"].DataRow[FC.DataSource.Tables["Table1"].Keys[0]].ToString() + @"' ";
                Where += " ORDER BY " + FC.DataSource.Tables["Table1"].Keys[0] + " ASC";
                System.Data.DataRow dr = FC.DataSource.Tables["Table1"].GetDataTableRow(Program.Org, Program.WebServiceUrl, Where);
                ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(dr);

                DateTime d2 = DateTime.Now;

                ((FormDesignTMP)((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).Control.FindForm()).SetLabText("加载数据用时" + d2.Subtract(d1).TotalSeconds.ToString() + @"秒");

            }


            if (((Button)sender).Name == this.ParentName + "_ToolsBar_ButtonSearch")
            {
                try
                {
                    if (Program.Org.User.Permissions.ContainsKey(FC.FormCode) && !Program.Org.User.Permissions[FC.FormCode]["Select"])
                    {
                        MessageBox.Show("当前用户没有该权限");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("当前用户没有该权限");
                    return;
                }
              

                if (this.ParentName.IndexOf("HeadPanel") > -1)
                {
                    string sql = string.Empty;

                    if (FC.DataSource.Tables["Table1"].SearchSQL.ToLower().Contains("where"))
                    {
                        sql = FC.DataSource.Tables["Table1"].SearchSQL + FC.DataSource.Tables["Table1"].SqlWhere;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(FC.DataSource.Tables["Table1"].SqlWhere))
                        {
                           sql = FC.DataSource.Tables["Table1"].SearchSQL + " where 1=1 " + FC.DataSource.Tables["Table1"].SqlWhere;
                        }
                        else
                        {
                            sql = FC.DataSource.Tables["Table1"].SearchSQL;
                        }
                    }

                  
                    CommonForm.frmSearchData frm = new CommonForm.frmSearchData(Org,WebServiceUrl, sql,
                        FC.DataSource.Tables["Table1"].OrderKeys, FC.DataSource.Tables["Table1"].OrderTypes,true, false);

                    frm.SetFC(FC);
                    frm.ShowDialog();

                    string sTmp = string.Empty;

                    List<string> sRows = Common.StringHelper.GetDataFromTag(frm.ReturnDataXML, "<Row>", "</Row>");

                    DateTime d1 = DateTime.Now;


                    if (sRows.Count > 0)
                    {
                        string Wheresql = string.Empty;

                        for (int i = 0; i < FC.DataSource.Tables["Table1"].Keys.Count; i++)
                        {
                            sTmp = Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<" + FC.DataSource.Tables["Table1"].RetrunKeys[i] + ">", "</" + FC.DataSource.Tables["Table1"].RetrunKeys[i] + ">");

                            if (string.IsNullOrEmpty(Wheresql))
                            {
                                if (!string.IsNullOrEmpty(sTmp))
                                    Wheresql += " WHERE [" + FC.DataSource.Tables["Table1"].Keys[i] + "] ='" + sTmp + @"' ";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(sTmp))
                                    Wheresql += " AND [" + FC.DataSource.Tables["Table1"].Keys[i] + "] ='" + sTmp + @"' ";
                            }
                        }



                        System.Data.DataRow dr = FC.DataSource.Tables["Table1"].GetDataTableRow(Org, WebServiceUrl, Wheresql);

                        //含表身查询
                        if (string.IsNullOrEmpty(frm.bodyStr))
                            ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(dr);
                        else
                            ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(dr, frm.bodyStr,frm.pageName);

                        DateTime d2 = DateTime.Now;

                        ((FormDesignTMP)((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).Control.FindForm()).SetLabText("加载数据用时" + d2.Subtract(d1).TotalSeconds.ToString() + @"秒");
                    }
                }
            }
            else if (((Button)sender).Name == this.ParentName + "_ToolsBar_ButtonExit")
            {
                this.Control.FindForm().Close();
            }
            else if (((Button)sender).Name == this.ParentName + "_ToolsBar_ButtonAdd")
            {
                try
                {
                    if (Program.Org.User.Permissions.ContainsKey(FC.FormCode) && !Program.Org.User.Permissions[FC.FormCode]["Add"])
                    {
                        MessageBox.Show("当前用户没有该权限");
                        return;
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("当前用户没有该权限");
                    return;
                }
              

                if (this.ParentName.IndexOf("HeadPanel") > -1)
                {
                    FC.FormPanels["Panel1"].SetStatus("Add");

                    
                }
                else
                {
                    if (FC.DataSource.Tables["Table1"].ShowDataRow != null &&
                    FC.DataSource.Tables["Table1"].ShowDataRow.Table.Columns.Contains("dosureby") &&
                    (!string.IsNullOrEmpty(FC.DataSource.Tables["Table1"].ShowDataRow["dosureby"].ToString())
                    || !string.IsNullOrEmpty(FC.DataSource.Tables["Table1"].ShowDataRow["auditby"].ToString())))
                    {
                        MessageBox.Show("数据已确认或已审核，不能添加");
                        return;
                    }
                    if (FC.DataSource.Tables["Table1"].ShowDataRow != null)
                    {
                        foreach (string key in FC.FormPanels.Keys)
                        {
                            if (this.ControlName.IndexOf(FC.FormPanels[key].ControlName) > -1)
                            {
                                FC.FormPanels[key].SetStatus("Add");
                            }
                        }
                    }
                       
                }
            }
            else if (((Button)sender).Name == this.ParentName + "_ToolsBar_ButtonEdit")
            {
                try
                {
                    if (Program.Org.User.Permissions.ContainsKey(FC.FormCode) && !Program.Org.User.Permissions[FC.FormCode]["Edit"])
                    {
                        MessageBox.Show("当前用户没有该权限");
                        return;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("当前用户没有该权限");
                    return;
                }

              
                if (FC.DataSource.Tables["Table1"].ShowDataRow != null &&
                    FC.DataSource.Tables["Table1"].ShowDataRow.Table.Columns.Contains("dosureby") &&
                    (!string.IsNullOrEmpty(FC.DataSource.Tables["Table1"].ShowDataRow["dosureby"].ToString())
                    || !string.IsNullOrEmpty(FC.DataSource.Tables["Table1"].ShowDataRow["auditby"].ToString())))
                {
                    MessageBox.Show("数据已确认或已审核，不能修改");
                    return;
                }


                if (this.ParentName.IndexOf("HeadPanel") > -1)
                {
                    FC.FormPanels["Panel1"].SetStatus("Edit");
                }
                else
                {
                    if (FC.DataSource.Tables["Table1"].ShowDataRow != null)
                    {
                        foreach (string key in FC.FormPanels.Keys)
                        {
                            if (this.ControlName.IndexOf(FC.FormPanels[key].ControlName) > -1)
                            {
                                FC.FormPanels[key].SetStatus("Edit");
                            }
                        }
                    }

                   
                }
               
              
            }
            else if (((Button)sender).Name == this.ParentName + "_ToolsBar_ButtonCancel")
            {
                if (this.ParentName.IndexOf("HeadPanel") > -1)
                {
                    ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(FC.DataSource.Tables["Table1"].ShowDataRow);
                    FC.FormPanels["Panel1"].SetStatus("Normal");
                }
                else
                {
                    foreach (string key in FC.FormPanels.Keys)
                    {
                        if (this.ControlName.IndexOf(FC.FormPanels[key].ControlName) > -1)
                        {
                            FC.FormPanels[key].SetStatus("Normal");
                        }
                    }
                }
            }

            else if (((Button)sender).Name == this.ParentName + "_ToolsBar_ButtonSave")
            {
                if (this.ParentName.IndexOf("HeadPanel") > -1)
                {
                    if (MessageBox.Show("是否保存数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                    {
                        if (FC.FormPanels["Panel1"].Status == "Add")
                        {
                            FC.DataSource.Tables["Table1"].GetNewRow(Org, WebServiceUrl);


                            foreach (string key in FC.FormPanels["Panel1"].Childrens.Keys)
                            {
                                FormDesign.Control.ControlClass cc = FC.FormPanels["Panel1"].Childrens[key];

                                if (cc.ControlType == "TextBox")
                                {
                                    FC.DataSource.Tables["Table1"].ShowDataRow[((TextBoxClass)cc).DataKey] = ((TextBoxClass)cc).Value;
                                }
                            }

                            FC.DataSource.Tables["Table1"].AddRow(Org,WebServiceUrl);
                            ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(FC.DataSource.Tables["Table1"].ShowDataRow);
                        }
                        else if (FC.FormPanels["Panel1"].Status == "Edit")
                        {
                            foreach (string key in FC.FormPanels["Panel1"].Childrens.Keys)
                            {
                                FormDesign.Control.ControlClass cc = FC.FormPanels["Panel1"].Childrens[key];

                                if (cc.ControlType == "TextBox")
                                {
                                    FC.DataSource.Tables["Table1"].ShowDataRow[((TextBoxClass)cc).DataKey] = ((TextBoxClass)cc).Value;
                                }
                            }

                            FC.DataSource.Tables["Table1"].EditRow(Org,WebServiceUrl);
                            ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(FC.DataSource.Tables["Table1"].ShowDataRow);
                        }
                        FC.FormPanels["Panel1"].SetStatus("Normal");
                    }
                }

            }
            else if (((Button)sender).Name == this.ParentName + "_ToolsBar_ButtonDel")
            {
                try
                {
                    if (Program.Org.User.Permissions.ContainsKey(FC.FormCode) && !Program.Org.User.Permissions[FC.FormCode]["Delete"])
                    {
                        MessageBox.Show("当前用户没有该权限");
                        return;
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("当前用户没有该权限");
                    return;
                }
               

                if (FC.DataSource.Tables["Table1"].ShowDataRow != null &&
                    FC.DataSource.Tables["Table1"].ShowDataRow.Table.Columns.Contains("dosureby") &&
                    (!string.IsNullOrEmpty(FC.DataSource.Tables["Table1"].ShowDataRow["dosureby"].ToString())
                    || !string.IsNullOrEmpty(FC.DataSource.Tables["Table1"].ShowDataRow["auditby"].ToString())))
                {
                    MessageBox.Show("数据已确认或已审核，不能删除");
                    return;
                }


                if (this.ParentName.IndexOf("HeadPanel") > -1)
                {
                    

                    if (FC.DataSource.Tables["Table1"].ShowDataRow !=null 
                        && !string.IsNullOrEmpty(FC.DataSource.Tables["Table1"].ShowDataRow["id"].ToString())
                            &&MessageBox.Show("是否删除本条数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                    {
                        if(FC.PanelsCount>1)
                        for (int i = 2; i <= FC.PanelsCount; i++)
                        {
                            if(!string.IsNullOrEmpty(FC.DataSource.Tables["Table" + i].TableName))
                            { 
                            SJeMES_Framework.WinForm.FormDesign.FormPanel.BodyPanelClass BPC =
                               FC.FormPanels["Panel" + i] as SJeMES_Framework.WinForm.FormDesign.FormPanel.BodyPanelClass;
                                SJeMES_Framework.WinForm.FormDesign.Control.DataViewClass DVC =
                                    ((SJeMES_Framework.WinForm.FormDesign.Control.DataViewClass)BPC.Childrens["Children1"]);

                                string where = "WHERE 1=1";

                                foreach (string s in DVC.HeadKeys)
                                {
                                    string HeadDataKey = s.Split(':')[0];
                                    string BodyDataKey = s.Split(':')[1];

                                    where += " AND [" + BodyDataKey + @"]='" + FC.DataSource.Tables["Table1"].ShowDataRow[HeadDataKey].ToString() + @"' ";
                                }

                                FC.DataSource.Tables["Table" + i].DelRow(Org, WebServiceUrl,where);
                                
                            }


                        }


                        //删表头
                        FC.DataSource.Tables["Table1"].DelRow(Org, WebServiceUrl);
                        FC.DataSource.Tables["Table1"].GetDataFisrtRow(Org, WebServiceUrl);
                        ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(FC.DataSource.Tables["Table1"].ShowDataRow);

                        FC.FormPanels["Panel1"].SetStatus("Normal");
                    }
                   
                }
                else
                {
                    for(int i=1;i<=FC.PanelsCount;i++)
                    {
                        if (this.ControlName.IndexOf(FC.FormPanels["Panel"+i].ControlName) > -1)
                        {
                            if (FC.DataSource.Tables["Table" + i].ShowDataRow !=null 
                                &&!string.IsNullOrEmpty(FC.DataSource.Tables["Table" + i].ShowDataRow["id"].ToString())
                                  &&
                                MessageBox.Show("是否删除本条数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                            {


                                FC.DataSource.Tables["Table"+i].DelRow(Org, WebServiceUrl);
                                FC.DataSource.Tables["Table"+i].GetDataFisrtRow(Org, WebServiceUrl);
                                ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(FC.DataSource.Tables["Table1"].ShowDataRow);

                                FC.FormPanels["Panel1"].SetStatus("Normal");
                                FC.FormPanels["Panel"+i].SetStatus("Normal");
                            }
                           
                        }
                    }
                }

            }
            else if (((Button)sender).Name == this.ParentName + "_ToolsBar_ButtonMore")
            {
                try
                {
                    if (Program.Org.User.Permissions.ContainsKey(FC.FormCode) && !Program.Org.User.Permissions[FC.FormCode]["Fun"])
                    {
                        MessageBox.Show("当前用户没有该权限");
                        return;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("当前用户没有该权限");
                    return;
                }
              

                this.FunMenu.Show(this.Control.Controls.Find(this.ParentName + "_ToolsBar_ButtonMore", false)[0], new System.Drawing.Point(0, this.Control.Controls.Find(this.ParentName + "_ToolsBar_ButtonMore", false)[0].Height));
            }
            else if (((Button)sender).Name == this.ParentName + "_ToolsBar_ButtonPrint")
            {
                try
                {
                    if (Program.Org.User.Permissions.ContainsKey(FC.FormCode) && !Program.Org.User.Permissions[FC.FormCode]["Print"])
                    {
                        MessageBox.Show("当前用户没有该权限");
                        return;
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("当前用户没有该权限");
                    return;
                }
               
                this.PrintMenu.Show(this.Control.Controls.Find(this.ParentName + "_ToolsBar_ButtonPrint", false)[0], new System.Drawing.Point(0, this.Control.Controls.Find(this.ParentName + "_ToolsBar_ButtonPrint", false)[0].Height));
            }
            else if (((Button)sender).Name == this.ParentName + "_ToolsBar_ButtonDoWork")
            {
                try
                {
                    if (Program.Org.User.Permissions.ContainsKey(FC.FormCode) && !Program.Org.User.Permissions[FC.FormCode]["DoWork"])
                    {
                        MessageBox.Show("当前用户没有该权限");
                        return;
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("当前用户没有该权限");
                    return;
                }
            
                this.DoWorkMenu.Show(this.Control.Controls.Find(this.ParentName + "_ToolsBar_ButtonDoWork", false)[0], new System.Drawing.Point(0, this.Control.Controls.Find(this.ParentName + "_ToolsBar_ButtonDoWork", false)[0].Height));
            }
        }

        public void SetPageRowsCount(int PageRowsCount)
        {
            this.PageControl.setPageRowsCount(PageRowsCount);
        }

        public void UpdatePageInfo(int RowCount,int PageNow)
        {
            this.PageControl.UpdatePageInfo(RowCount, PageNow);
        }


        public void SetStatus(string Status)
        {
            this.Status = Status;
            foreach (string key in Buttons.Keys)
            {
                Buttons[key].SetStatus(Status);
            }   
        }
    }
}
