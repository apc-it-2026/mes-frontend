using SJeMES_Framework.WinForm.FormXML.Control;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.FormXML.FormPanel
{
    public class HeadPanelClass:FormPanelClass 
    {
        public System.Data.DataRow Data;
        public Class.OrgClass Org;
        public Label lbTitle;
        Button btnF = new Button();


        public HeadPanelClass( string XML,string WebServiceUrl, Class.OrgClass Org)
        {
            try
            {
                this.Org = Org;
                this.WebServiceUrl = WebServiceUrl;
                Title = Common.StringHelper.GetDataFromFirstTag(XML, "<PanelTitle>", "</PanelTitle>");
                PanelType = Common.StringHelper.GetDataFromFirstTag(XML, "<PanelType>", "</PanelType");
                Hight = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<Hight>", "</Hight>"));

                string xmlToolsBar = Common.StringHelper.GetDataFromFirstTag(XML, "<ToolsBar>", "</ToolsBar>");
                //ToolsBar = new Control.ToolsBarClass(xmlToolsBar, WebServiceUrl, Org);

                ChildrensCount = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<ChildrensCount>", "</ChildrensCount>"));

                string xmlChildrens = Common.StringHelper.GetDataFromFirstTag(XML, "<Childrens>", "</Childrens>");

                Childrens = new Dictionary<string, Control.ControlClass>();
                for(int i= ChildrensCount; i>=1;i--)
                {
                    string xmlChildren = Common.StringHelper.GetDataFromFirstTag(xmlChildrens, "<Children" + i + ">", "</Children" + i + ">");

                    string ControlType = Common.StringHelper.GetDataFromFirstTag(xmlChildren, "<ControlType>", "</ControlType>");

                    switch(ControlType)
                    {
                        case "TextBox":
                            Childrens.Add("Children" + i, new Control.TextBoxClass(Org, xmlChildren, WebServiceUrl,false));
                            break;
                        //case "Button":
                        //    Childrens.Add("Children" + i, new Control.ButtonClass(xmlChildren));
                        //    break;
                    }
                   
                }
                this.Status = "Normal";
                
            }
            catch { }
        }

        public Panel GetControls(string ParentName,Forms.FormClass FC)
        {
            this.ParentName = ParentName;
            this.FC = FC;
            Panel panelRet = new Panel();
            this.Control = panelRet;
            try
            {
                this.ControlName = panelRet.Name = ParentName + "_HeadPanel";
                panelRet.Dock = DockStyle.Fill;
                panelRet.Margin = new Padding(0);
                panelRet.BorderStyle = BorderStyle.FixedSingle;


                Panel panelSysData = new Panel();
                panelSysData.Name = panelRet.Name + "_panelSysData";
                panelSysData.Dock = DockStyle.Top;
                panelSysData.Padding = new Padding(5, 20, 5, 20);
                panelSysData.Height = 120;

                int sysKeyNum = 0;

                foreach (string key in this.Childrens.Keys)
                {
                    TextBoxClass cc = (Control.TextBoxClass)this.Childrens[key];
                    if (((Control.TextBoxClass)cc).DataKey == "org" ||
                        ((Control.TextBoxClass)cc).DataKey == "auditby" ||
                                       ((Control.TextBoxClass)cc).DataKey == "auditdatetime" ||
                                       ((Control.TextBoxClass)cc).DataKey == "dosureby" ||
                                       ((Control.TextBoxClass)cc).DataKey == "dosuredatetime" ||
                                       ((Control.TextBoxClass)cc).DataKey == "createby" ||
                                       ((Control.TextBoxClass)cc).DataKey == "createdate" ||
                                       ((Control.TextBoxClass)cc).DataKey == "createtime" ||
                                       ((Control.TextBoxClass)cc).DataKey == "modifyby" ||
                                       ((Control.TextBoxClass)cc).DataKey == "modifydate" ||
                                       ((Control.TextBoxClass)cc).DataKey == "modifytime")
                    {
                        sysKeyNum++;
                    }
                }

                int iLast = sysKeyNum % 5;
                int i = this.ChildrensCount;

                while (i > this.ChildrensCount - sysKeyNum)
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
                                    if (((Control.TextBoxClass)cc).DataKey == "org" ||
                                        ((Control.TextBoxClass)cc).DataKey == "auditby" ||
                                        ((Control.TextBoxClass)cc).DataKey == "auditdatetime" ||
                                        ((Control.TextBoxClass)cc).DataKey == "dosureby" ||
                                        ((Control.TextBoxClass)cc).DataKey == "dosuredatetime" ||
                                        ((Control.TextBoxClass)cc).DataKey == "createby" ||
                                        ((Control.TextBoxClass)cc).DataKey == "createdate" ||
                                        ((Control.TextBoxClass)cc).DataKey == "createtime" ||
                                        ((Control.TextBoxClass)cc).DataKey == "modifyby" ||
                                        ((Control.TextBoxClass)cc).DataKey == "modifydate" ||
                                        ((Control.TextBoxClass)cc).DataKey == "modifytime")
                                        pTmp.Controls.Add(((Control.TextBoxClass)cc).GetControls(panelSysData.Name, this.FC));
                                    break;

                            }

                            i--;

                        }

                        panelSysData.Controls.Add(pTmp);
                    }
                    else if (iLast == 0)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            Control.ControlClass cc = this.Childrens["Children" + i];
                            switch (cc.ControlType)
                            {
                                case "TextBox":
                                    if (((Control.TextBoxClass)cc).DataKey == "org" ||
                                        ((Control.TextBoxClass)cc).DataKey == "auditby" ||
                                        ((Control.TextBoxClass)cc).DataKey == "auditdatetime" ||
                                        ((Control.TextBoxClass)cc).DataKey == "dosureby" ||
                                        ((Control.TextBoxClass)cc).DataKey == "dosuredatetime" ||
                                        ((Control.TextBoxClass)cc).DataKey == "createby" ||
                                        ((Control.TextBoxClass)cc).DataKey == "createdate" ||
                                        ((Control.TextBoxClass)cc).DataKey == "createtime" ||
                                        ((Control.TextBoxClass)cc).DataKey == "modifyby" ||
                                        ((Control.TextBoxClass)cc).DataKey == "modifydate" ||
                                        ((Control.TextBoxClass)cc).DataKey == "modifytime")
                                        pTmp.Controls.Add(((Control.TextBoxClass)cc).GetControls(panelSysData.Name, this.FC));
                                    break;

                            }

                            i--;

                        }

                        panelSysData.Controls.Add(pTmp);
                    }
                }



                Panel panelChildrens = new Panel();
                panelChildrens.Name = panelRet.Name + "_panelChildrens";
                panelChildrens.Dock = DockStyle.Top;
                panelChildrens.Padding = new Padding(5, 20, 5, 20);
                panelChildrens.Height = this.Hight;

                iLast = (this.ChildrensCount - sysKeyNum) % 5;
                i = (this.ChildrensCount - sysKeyNum);

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
                                    if (((Control.TextBoxClass)cc).DataKey != "org" &&
                                        ((Control.TextBoxClass)cc).DataKey != "auditby" &&
                                        ((Control.TextBoxClass)cc).DataKey != "auditdatetime" &&
                                        ((Control.TextBoxClass)cc).DataKey != "dosureby" &&
                                        ((Control.TextBoxClass)cc).DataKey != "dosuredatetime" &&
                                        ((Control.TextBoxClass)cc).DataKey != "createby" &&
                                        ((Control.TextBoxClass)cc).DataKey != "createdate" &&
                                        ((Control.TextBoxClass)cc).DataKey != "createtime" &&
                                        ((Control.TextBoxClass)cc).DataKey != "modifyby" &&
                                        ((Control.TextBoxClass)cc).DataKey != "modifydate" &&
                                        ((Control.TextBoxClass)cc).DataKey != "modifytime")
                                        pTmp.Controls.Add(((Control.TextBoxClass)cc).GetControls(panelChildrens.Name, this.FC));
                                    break;
                                //case "Button":
                                //    pTmp.Controls.Add(((Control.ButtonClass)cc).GetControls(panelChildrens.Name, this.FC));
                                //    break;
                            }

                            i--;

                        }

                        panelChildrens.Controls.Add(pTmp);
                    }
                    else if (iLast == 0)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            Control.ControlClass cc = this.Childrens["Children" + i];
                            switch (cc.ControlType)
                            {
                                case "TextBox":
                                    if (((Control.TextBoxClass)cc).DataKey != "org" &&
                                        ((Control.TextBoxClass)cc).DataKey != "auditby" &&
                                        ((Control.TextBoxClass)cc).DataKey != "auditdatetime" &&
                                        ((Control.TextBoxClass)cc).DataKey != "dosureby" &&
                                        ((Control.TextBoxClass)cc).DataKey != "dosuredatetime" &&
                                        ((Control.TextBoxClass)cc).DataKey != "createby" &&
                                        ((Control.TextBoxClass)cc).DataKey != "createdate" &&
                                        ((Control.TextBoxClass)cc).DataKey != "createtime" &&
                                        ((Control.TextBoxClass)cc).DataKey != "modifyby" &&
                                        ((Control.TextBoxClass)cc).DataKey != "modifydate" &&
                                        ((Control.TextBoxClass)cc).DataKey != "modifytime")
                                        pTmp.Controls.Add(((Control.TextBoxClass)cc).GetControls(panelChildrens.Name, this.FC));
                                    break;
                                //case "Button":
                                //    pTmp.Controls.Add(((Control.ButtonClass)cc).GetControls(panelChildrens.Name, this.FC));
                                //    break;
                            }

                            i--;

                        }

                        panelChildrens.Controls.Add(pTmp);
                    }
                }




                panelRet.Controls.Add(panelSysData);

                panelRet.Controls.Add(panelChildrens);

                //panelRet.Controls.Add(this.ToolsBar.GetControls(panelRet.Name, this.FC));

                //Panel pTop = new Panel();
                //pTop.Height = 26;
                //pTop.Dock = DockStyle.Top;

                //lbTitle = new Label();
                //lbTitle.Name = panelRet.Name + "_lbTitle";
                //lbTitle.Text = this.Title;
                //lbTitle.Dock = DockStyle.Top;
                //lbTitle.ForeColor = Color.White;
                //lbTitle.Font = new Font("微软雅黑", 12, FontStyle.Bold);
                //lbTitle.TextAlign = ContentAlignment.MiddleLeft;
                //lbTitle.AutoSize = false;
                //lbTitle.BackColor = Color.RoyalBlue;
                ////lbTitle.Click += Lbtmp_Click;
                //pTop.Controls.Add(lbTitle);

                //Panel panTitle = new Panel();
                //panTitle.Dock = DockStyle.Fill;
                //panTitle.BackColor = Color.RoyalBlue;



                //PictureBox pic = new PictureBox();
                //pic.Name = this.Title;
                //pic.Dock = DockStyle.Left;
                //pic.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + @"\img\help.png";
                //pic.SizeMode = PictureBoxSizeMode.Zoom;
                //pic.Click += Pic_Click;
                //pic.Width = 40;
                //pic.Height = 26;
                //panTitle.Controls.Add(pic);

                //lbTitle = new Label();
                //lbTitle.Name = panelRet.Name + "_lbTitle";
                //lbTitle.Text = this.Title;
                //lbTitle.Dock = DockStyle.Left;
                //lbTitle.ForeColor = Color.White;
                //lbTitle.Font = new Font("微软雅黑", 12, FontStyle.Bold);
                //lbTitle.TextAlign = ContentAlignment.MiddleLeft;
                //lbTitle.AutoSize = true;

                //panTitle.Controls.Add(lbTitle);



                //pTop.Controls.Add(panTitle);


                //btnF.Name = panelRet.Name + "_btnF";
                //btnF.Width = 80;
                //btnF.Height = 24;
                //btnF.Text = "收藏";
                //btnF.Font = new Font("微软雅黑", 9);
                
                //btnF.Left = 930;
                //btnF.Top = 0;
                //btnF.Dock = DockStyle.Right;
                //btnF.Click+= Lbtmp_Click;
                //pTop.Controls.Add(btnF);

                

                //panelRet.Controls.Add(pTop);

//                string sql = @"
//SELECT * FROM SYSUSER05M
//WHERE UserCode='" + Program.Org.User.UserCode + @"' and AppCode ='" + FC.FormCode + @"'
//";
//                if (!string.IsNullOrEmpty(SJeMES_Framework.Common.WebServiceHelper.GetString(Program.WebServiceUrl, sql, new Dictionary<string, string>())))
//                {
//                    btnF.Text = "已收藏";
//                }

            }
            catch { }

            SetStatus(Status);

            return panelRet;
        }

        private void Lbtmp_Click(object sender, EventArgs e)
        {
            if(btnF.Text=="已收藏")
            { 
                if (MessageBox.Show("是否取消收藏本功能", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sql = @"
DELETE FROM SYSUSER05M
WHERE UserCode='" + Program.Org.User.UserCode + @"' and AppCode ='" + FC.FormCode + @"'
";
                    SJeMES_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, new Dictionary<string, string>());

                    btnF.Text = "收藏";
                }
            }
            else
            {
                if (MessageBox.Show("是否收藏本功能", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sql = @"
INSERT INTO SYSUSER05M
(UserCode,AppCode)
values
('" + Program.Org.User.UserCode + @"','" + FC.FormCode + @"')
";
                    SJeMES_Framework.Common.WebServiceHelper.ExecuteNonQuery(Program.WebServiceUrl, sql, new Dictionary<string, string>());

                    btnF.Text = "已收藏";
                }
            }
        }
        private void Pic_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            Dictionary<string, object> OBJ = new Dictionary<string, object>();
            OBJ.Add("FileName", pic.Name);
            OBJ.Add("WebServiceUrl", Program.WebServiceUrl);
            SJeMES_Framework.Common.OtherPrograms.RunApp("SJEMS_PictureHelp", "SJEMS_PictureHelp.Interface", "RunApp", OBJ);

        }

        public void SetDefaultValueData(System.Data.DataRow dr)
        {

            foreach (string key in Childrens.Keys)
            {
                FormXML.Control.ControlClass cc = Childrens[key];
                switch (cc.ControlType)
                {
                    case "TextBox":
                        ((FormXML.Control.TextBoxClass)cc).SetDefaultValueData(dr);
                        break;
                }

            }

        }


        public void SetData(System.Data.DataRow dr)
        {
            this.Data = dr;
            foreach(string key  in Childrens.Keys)
            {
                FormXML.Control.ControlClass cc = Childrens[key];
                switch (cc.ControlType)
                {
                    case "TextBox":
                        ((FormXML.Control.TextBoxClass)cc).SetData(dr);
                        break;
                }

            }

            


           
        }
        /// <summary>
        /// 表身查询
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="bodyStr"></param>
        public void SetData(System.Data.DataRow dr,string bodyStr,string pageName)
        {
            this.Data = dr;
            foreach (string key in Childrens.Keys)
            {
                FormXML.Control.ControlClass cc = Childrens[key];
                switch (cc.ControlType)
                {
                    case "TextBox":
                        ((FormXML.Control.TextBoxClass)cc).SetData(dr);
                        break;
                }

            }

            if (this.FC.FormType == "表头表身")
            {
                int n = 0;

                //foreach (TabPage tab in ((FormDesignTMP)((FormPanel.HeadPanelClass)this.FC.FormPanels["Panel1"]).Control.FindForm()).tabBody.TabPages)
                //{
                //    if (string.IsNullOrEmpty(pageName))
                //    {
                //        string num = (((FormDesignTMP)((FormPanel.HeadPanelClass)this.FC.FormPanels["Panel1"]).Control.FindForm()).tabBody.SelectedIndex + 2).ToString();

                //        ((FormPanel.BodyPanelClass)this.FC.FormPanels["Panel" + num]).SetHeadData(dr, bodyStr);
                //        break;
                //    }
                //    if (tab.Text == pageName)
                //    {
                //        //设置表身条件
                //         ((FormDesignTMP)((FormPanel.HeadPanelClass)this.FC.FormPanels["Panel1"]).Control.FindForm()).SetBodySQL(bodyStr);
                          
                //        if (((FormDesignTMP)((FormPanel.HeadPanelClass)this.FC.FormPanels["Panel1"]).Control.FindForm()).tabBody.SelectedIndex == n)
                //            ((FormPanel.BodyPanelClass)this.FC.FormPanels["Panel" + (((FormDesignTMP)((FormPanel.HeadPanelClass)this.FC.
                //                FormPanels["Panel1"]).Control.FindForm()).tabBody.SelectedIndex + 2)]).SetHeadData(this.FC.DataSource.Tables["Table1"].DataRow, bodyStr);
                //        else
                //            ((FormDesignTMP)((FormPanel.HeadPanelClass)this.FC.FormPanels["Panel1"]).Control.FindForm()).tabBody.SelectedIndex = n;

                //        break;
                //    }
                //    n++;
                //}

               

            }



        }

        public void SetStatus(string Status)
        {
            this.Status = Status;

            //Forms.FormBodyEditAndAdd frm;
            switch (Status)
            {
                case "Add":
                    FormPanel.HeadPanelClass HPC = this;
                    //frm = new Forms.FormBodyEditAndAdd( "Add",this.FC, HPC, this.FC.DataSource.Tables["Table1"], WebServiceUrl, Org);
                    //frm.ShowDialog();
                    //SetData(frm.Table.DataRow);



                    foreach (string key in Childrens.Keys)
                    {
                        Childrens[key].SetStatus(Status);
                    }

                    SetDefaultValueData(this.FC.DataSource.Tables["Table1"].GetNewRow(Org, this.WebServiceUrl));
                    break;

                case "Edit":

                    

                    if (this.FC.DataSource.Tables["Table1"].ShowDataRow != null &&
                        !string.IsNullOrEmpty(this.FC.DataSource.Tables["Table1"].ShowDataRow["id"].ToString()))
                    {
                        //frm = new Forms.FormBodyEditAndAdd("Edit", this.FC, this, this.FC.DataSource.Tables["Table1"], WebServiceUrl, Org);
                        //frm.ShowDialog();
                        //SetData(frm.Table.DataRow);
                        foreach (string key in Childrens.Keys)
                        {
                            Childrens[key].SetStatus(Status);
                        }

                    }
                    else
                    {
                        MessageBox.Show("请选择数据");
                    }
                    break;
                case "Normal":
                    //ToolsBar.SetStatus(Status);
                    foreach (string key in Childrens.Keys)
                    {
                        Childrens[key].SetStatus(Status);
                    }
                    break;
            }

           
        }

        public void LoadLastRow()
        {
            System.Data.DataRow dr = FC.DataSource.Tables["Table1"].GetDataFisrtRow(Org,WebServiceUrl);

            SetData(dr);
        }

        public void LoadDataWhereSql(string whereSql)
        {
            System.Data.DataRow dr = FC.DataSource.Tables["Table1"].GetDataTableRow(Org, WebServiceUrl,whereSql);

            SetData(dr);
        }

    }
}
