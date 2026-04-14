using SJeMES_Framework.WinForm.FormDesign.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.FormDesign.Forms
{
    public partial class FormBodyEditAndAdd : Form
    {
        
        public string Type;
        public TableClass Table;
        public string WebServiceUrl;
        public Dictionary<string, Control.ControlClass> Childrens;
        public FormPanel.FormPanelClass FPC;
        public Forms.FormClass FC;

        public Class.OrgClass Org;
        public string CloseType = string.Empty;

        public FormBodyEditAndAdd(string Type,Forms.FormClass FC, FormPanel.FormPanelClass FPC, TableClass table,string WebServiceUrl,Class.OrgClass Org)
        {
            InitializeComponent();
            this.Org = Org;
            this.FC = FC;
            this.Type = Type;
            this.Table = table;
            this.WebServiceUrl = WebServiceUrl;
            this.FPC = FPC;
            Childrens = new Dictionary<string, Control.ControlClass>();
            int i = 0;

            if (this.FPC.PanelType == "HeadPanel")
            {
                foreach (string key in FPC.Childrens.Keys)
                {
                   
                    Control.TextBoxClass TBC = (Control.TextBoxClass)FPC.Childrens[key];
                    if (TBC.DataKey != "org" &&
                                        TBC.DataKey != "auditby" &&
                                        TBC.DataKey != "auditdatetime" &&
                                        TBC.DataKey != "dosureby" &&
                                        TBC.DataKey != "dosuredatetime" &&
                                        TBC.DataKey != "createby" &&
                                        TBC.DataKey != "createdate" &&
                                        TBC.DataKey != "createtime" &&
                                        TBC.DataKey != "modifyby" &&
                                        TBC.DataKey != "modifydate" &&
                                        TBC.DataKey != "modifytime")
                    {
                        i++;
                        Childrens.Add(key, new TextBoxClass(Org,TBC,this.WebServiceUrl,true));
                    }

                }
            }
            else
            {
                Control.DataViewClass DVC = (Control.DataViewClass)FPC.Childrens["Children1"];
                foreach (string key in DVC.Columns.Keys)
                {
                    Control.DataViewColumnClass DVCC = (Control.DataViewColumnClass)DVC.Columns[key];
                    if (DVCC.DataKey != "org" &&
                                        DVCC.DataKey != "auditby" &&
                                        DVCC.DataKey != "auditdatetime" &&
                                        DVCC.DataKey != "dosureby" &&
                                        DVCC.DataKey != "dosuredatetime" &&
                                        DVCC.DataKey != "createby" &&
                                        DVCC.DataKey != "createdate" &&
                                        DVCC.DataKey != "createtime" &&
                                        DVCC.DataKey != "modifyby" &&
                                        DVCC.DataKey != "modifydate" &&
                                        DVCC.DataKey != "modifytime")
                    {
                        i++;
                        Childrens.Add("Children" + i, new Control.TextBoxClass(Org, DVCC, WebServiceUrl,true));
                    }
                }
            }

            if (Type == "Edit")
            {
                this.Text = this.FPC.Title + "【修改】";
            }
            else if (Type == "Add")
            {
                this.Text = this.FPC.Title + "【添加】";
            }
            this.Width = 800;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimizeBox = false;
            this.MaximizeBox = true;
            

            Panel panelTop = new Panel();
            panelTop.Name = "FormBodyEditAndAdd_panelTop";
            panelTop.BorderStyle = BorderStyle.FixedSingle;
            panelTop.Height = 35;
            panelTop.Dock = DockStyle.Top;

            Button btnSave = new Button();
            btnSave.Name = "FormBodyEditAndAdd_btnSave";
            btnSave.Text = "保存";
            btnSave.Width = 80;
            btnSave.Height = 20;
            btnSave.Font = new System.Drawing.Font("微软雅黑", 9);
            btnSave.ForeColor = System.Drawing.Color.Black;
            btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            btnSave.Dock = DockStyle.Left;
            btnSave.Click += B_Click;

            Button btnCancel = new Button();
            btnCancel.Name = "FormBodyEditAndAdd_btnCancel";
            btnCancel.Text = "取消";
            btnCancel.Width = 80;
            btnCancel.Height = 20;
            btnCancel.Font = new System.Drawing.Font("微软雅黑", 9);
            btnCancel.ForeColor = System.Drawing.Color.Black;
            btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            btnCancel.Dock = DockStyle.Left;
            btnCancel.Click += B_Click;

            panelTop.Controls.Add(btnCancel);
            panelTop.Controls.Add(btnSave);

            Panel panelMain = new Panel();
            panelMain.Name = "FormBodyEditAndAdd_panelMain";
            panelMain.Dock = DockStyle.Fill;
            panelMain.AutoScroll = true;
            panelMain.Padding = new Padding(5, 20, 5, 20);

            int iLast = this.Childrens.Count % 4;
            i = this.Childrens.Count;

            int num = i;

            while (i > 0)
            {
                Panel pTmp = new Panel();
                pTmp.Dock = DockStyle.Top;
                pTmp.Height = 22;

                if (iLast > 0)
                {
                    for (; iLast > 0; iLast--)
                    {
                        Control.ControlClass cc = this.Childrens["Children" + i];
                        switch (cc.ControlType)
                        {
                            case "TextBox":
                                pTmp.Controls.Add(((Control.TextBoxClass)cc).GetControls(this.FPC.ControlName, this.FC));
                                break;
                            
                        }

                        i--;

                    }

                    pTmp.TabIndex = num;
                    panelMain.Controls.Add(pTmp);
                }
                else if (iLast == 0)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        Control.ControlClass cc = this.Childrens["Children" + i];
                        switch (cc.ControlType)
                        {
                            case "TextBox":
                                pTmp.Controls.Add(((Control.TextBoxClass)cc).GetControls(this.FPC.ControlName, this.FC));
                                break;
                        }

                        i--;

                    }

                    pTmp.TabIndex = num;
                    panelMain.Controls.Add(pTmp);
                }
                num--;
            }

            this.Controls.Add(panelMain);
            this.Controls.Add(panelTop);

            SetStatus(Type);
            if (Type == "Edit")
            {
                
                SetData(this.Table.DataRow);
            }
            else if(Type =="Add")
            {
                SetData(this.Table.GetNewRow(Org, this.WebServiceUrl));
                if (FPC.PanelType == "BodyPanel")
                {
                    SetDefaultValueData(((FormPanel.BodyPanelClass)FPC).HeadData);
                }
                else
                {
                    SetDefaultValueData(Table.GetNewRow(Org, this.WebServiceUrl));
                }
                
            }

        }

        private void B_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            switch (b.Name.Replace("FormBodyEditAndAdd_",""))
            {
                case "btnCancel":
                    
                    this.Close();
                    break;
                case "btnSave":
                    if (Type == "Add")
                    {
                        Table.GetNewRow(Org, WebServiceUrl);

                        if (Table.ShowDataRow.Table.Columns.Contains("org"))
                        {
                            Table.ShowDataRow["org"] = Org.Org;
                        }
                        if (Table.ShowDataRow.Table.Columns.Contains("createby"))
                        {
                            Table.ShowDataRow["createby"] = Org.User.UserCode;
                        }
                        if (Table.ShowDataRow.Table.Columns.Contains("createdate"))
                        {
                            Table.ShowDataRow["createdate"] = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        if (Table.ShowDataRow.Table.Columns.Contains("createtime"))
                        {
                            Table.ShowDataRow["createtime"] = DateTime.Now.ToString("HH:mm:ss");
                        }



                        foreach (string key in this.Childrens.Keys)
                        {
                            FormDesign.Control.ControlClass cc = this.Childrens[key];

                            if (cc.ControlType == "TextBox")
                            {
                                TextBoxClass tbc = (TextBoxClass)cc;

                                if (!tbc.IsNull && string.IsNullOrEmpty(tbc.Value))
                                {
                                    MessageBox.Show(tbc.Title + "不能为空");
                                    return;
                                }

                                Table.ShowDataRow[tbc.DataKey] = tbc.Value;

                            }
                        }

                        foreach (string key in this.Childrens.Keys)
                        {
                            FormDesign.Control.ControlClass cc = this.Childrens[key];

                            if (cc.ControlType == "TextBox")
                            {
                                TextBoxClass tbc = (TextBoxClass)cc;
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
                                    int tmp = Convert.ToInt32(Common.WebServiceHelper.GetString(Org, WebServiceUrl, sql, new Dictionary<string, string>()));

                                    sorting = (tmp + 1).ToString("0000");

                                    Table.ShowDataRow[tbc.DataKey] = sorting;
                                }
                            }

                            if (cc.ControlType == "TextBox")
                            {
                                TextBoxClass tbc = (TextBoxClass)cc;
                                if (tbc.DefaultValue.ToLower() == "auto_docno")
                                {
                                    string doc_no = string.Empty;


                                    string sql = @"
SELECT ISNULL(MAX([" + this.Table.Keys[0] + @"]),0)
FROM [" + this.Table.TableName + @"]
WHERE [" + this.Table.Keys[0] + @"] LIKE '" + DateTime.Now.ToString("yyyyMMdd") + @"%'
";
                                    string tmp = Common.WebServiceHelper.GetString(Org, WebServiceUrl, sql, new Dictionary<string, string>());

                                    doc_no = DateTime.Now.ToString("yyyyMMdd") + (Convert.ToUInt32(tmp.Replace(DateTime.Now.ToString("yyyyMMdd"), "")) + 1).ToString("000");

                                    Table.ShowDataRow[tbc.DataKey] = doc_no;
                                }
                            }
                        }

                        if (Table.AddRow(Org, WebServiceUrl))
                        {
                            ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(FC.DataSource.Tables["Table1"].ShowDataRow);
                        }

                        else
                        {
                            ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).LoadLastRow();
                        }


                        CloseType = "AddOrEdit";

                    }
                    else if (Type == "Edit")
                    {

                        if (Table.ShowDataRow.Table.Columns.Contains("org"))
                        {
                            Table.ShowDataRow["org"] = Org.Org;
                        }
                        if (Table.ShowDataRow.Table.Columns.Contains("modifyby"))
                        {
                            Table.ShowDataRow["modifyby"] = Org.User.UserCode;
                        }
                        if (Table.ShowDataRow.Table.Columns.Contains("modifydate"))
                        {
                            Table.ShowDataRow["modifydate"] = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        if (Table.ShowDataRow.Table.Columns.Contains("modifytime"))
                        {
                            Table.ShowDataRow["modifytime"] = DateTime.Now.ToString("HH:mm:ss");
                        }

                        foreach (string key in this.Childrens.Keys)
                        {
                            FormDesign.Control.ControlClass cc = this.Childrens[key];

                            if (cc.ControlType == "TextBox")
                            {
                                TextBoxClass tbc = (TextBoxClass)cc;


                                if (!tbc.IsNull && string.IsNullOrEmpty(tbc.Value))
                                {
                                    MessageBox.Show(tbc.Title + "不能为空");
                                    return;
                                }

                                Table.ShowDataRow[tbc.DataKey] = tbc.Value;
                            }
                        }

                        if (Table.EditRow(Org, WebServiceUrl))
                        {
                            ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).SetData(FC.DataSource.Tables["Table1"].ShowDataRow);
                        }
                        else
                        {
                            ((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).LoadLastRow();
                        }
                        CloseType = "AddOrEdit";
                    }
                    this.Close();
                    break;

            }
        }

        public void SetStatus(string Status)
        {
            foreach (string key in Childrens.Keys)
            {
                Childrens[key].SetStatus(Status);
            }
        }

        public void SetData(System.Data.DataRow dr)
        {
            
            foreach (string key in Childrens.Keys)
            {
                FormDesign.Control.ControlClass cc = Childrens[key];
                switch (cc.ControlType)
                {
                    case "TextBox":
                        ((FormDesign.Control.TextBoxClass)cc).SetData(dr);
                        break;
                }

            }

        }

        public void SetDefaultValueData(System.Data.DataRow dr)
        {

            foreach (string key in Childrens.Keys)
            {
                FormDesign.Control.ControlClass cc = Childrens[key];
                switch (cc.ControlType)
                {
                    case "TextBox":
                        ((FormDesign.Control.TextBoxClass)cc).SetDefaultValueData(dr);
                        break;
                }

            }

        }

        private void FormBodyEditAndAdd_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(this.CloseType !="AddOrEdit" && this.Type =="Add")
            if (this.FPC.PanelType == "HeadPanel")
            {
                ((SJeMES_Framework.WinForm.FormDesign.FormPanel.HeadPanelClass)this.FC.FormPanels["Panel1"]).LoadLastRow();
            }
            else
            {
                ((SJeMES_Framework.WinForm.FormDesign.FormPanel.HeadPanelClass)this.FC.FormPanels["Panel1"]).SetData(this.FC.DataSource.Tables["Table1"].ShowDataRow);
            }
        }
    }
}
