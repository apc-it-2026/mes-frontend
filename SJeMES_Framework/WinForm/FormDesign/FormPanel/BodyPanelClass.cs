using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.FormDesign.FormPanel
{
    public class BodyPanelClass : FormPanelClass 
    {
        public System.Data.DataRow HeadData;
        public System.Data.DataTable Data;
        public int PageRowsCount;
        public Class.OrgClass Org;
        public bool IsAddnEdit = true;

        public BodyPanelClass(string XML,string WebServiceUrl,Class.OrgClass Org)
        {
            try
            {
                this.Org = Org;
                this.WebServiceUrl = WebServiceUrl;
                Title = Common.StringHelper.GetDataFromFirstTag(XML, "<PanelTitle>", "</PanelTitle>");
                PanelType = Common.StringHelper.GetDataFromFirstTag(XML, "<PanelType>", "</PanelType");
                Hight = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<Hight>", "</Hight>"));

                string xmlToolsBar = Common.StringHelper.GetDataFromFirstTag(XML, "<ToolsBar>", "</ToolsBar>");
                ToolsBar = new Control.ToolsBarClass(xmlToolsBar, WebServiceUrl, Org);

               

                ChildrensCount = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<ChildrensCount>", "</ChildrensCount>"));
                PageRowsCount = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<PageRowsCount>", "</PageRowsCount>"));

                try
                {
                    IsAddnEdit = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<IsAddnEdit>", "</IsAddnEdit>"));
                }
                catch { }

                

                string xmlChildrens = Common.StringHelper.GetDataFromFirstTag(XML, "<Childrens>", "</Childrens>");

                Childrens = new Dictionary<string, Control.ControlClass>();
                for(int i= ChildrensCount; i>=1;i--)
                {
                    string xmlChildren = Common.StringHelper.GetDataFromFirstTag(xmlChildrens, "<Children" + i + ">", "</Children" + i + ">");

                    string ControlType = Common.StringHelper.GetDataFromFirstTag(xmlChildren, "<ControlType>", "</ControlType>");

                    switch(ControlType)
                    {
                        case "DataView":
                            Childrens.Add("Children" + i, new Control.DataViewClass(xmlChildren, WebServiceUrl,Org));
                            break;
                       
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
                this.ControlName = panelRet.Name = ParentName + "_BodyPanel_"+this.Title;
                panelRet.Dock = DockStyle.Fill;
                panelRet.Margin = new Padding(0);
                panelRet.BorderStyle = BorderStyle.FixedSingle;

                Panel panelChildrens = new Panel();
                panelChildrens.Name = panelRet.Name + "_panelChildrens";
                panelChildrens.Dock = DockStyle.Fill;
                panelChildrens.Padding = new Padding(5,20,5,20);


                for (int i = 1; i <= this.ChildrensCount; i++)
                {
                    FormDesign.Control.ControlClass cc = this.Childrens["Children" + i];
                    switch (cc.ControlType)
                    {
                       
                        case "DataView":
                            int PageRowsCount = Convert.ToInt32(SJeMES_Framework.Common.WebServiceHelper.GetString(Program.Org, Program.WebServiceUrl,
                                @"SELECT parameters_value FROM SYS002M where parameters_code='PageRows'", new Dictionary<string, string>()));
                            ((Control.DataViewClass)cc).PageRowsCount = PageRowsCount;
                            ToolsBar.SetPageRowsCount(PageRowsCount);
                            panelChildrens.Controls.Add(((Control.DataViewClass)cc).GetControls(panelChildrens.Name, this.FC));

                            break;
                    }

                }
                panelRet.Controls.Add(panelChildrens);

                panelRet.Controls.Add(this.ToolsBar.GetControls(panelRet.Name,this.FC));

            }
            catch { }

           
            SetStatus(Status);

            return panelRet;
        }

        public void ClearData()
        {
            ((FormDesign.Control.DataViewClass)this.Childrens["Children1"]).ClearData();
        }

        public void NextPage()
        {
            ((FormDesign.Control.DataViewClass)this.Childrens["Children1"]).NextPage();
        }

        public void BackPage()
        {
            ((FormDesign.Control.DataViewClass)this.Childrens["Children1"]).BackPage();
        }

        public void FirstPage()
        {
            ((FormDesign.Control.DataViewClass)this.Childrens["Children1"]).FirstPage();
        }

        public void LastPage()
        {
            ((FormDesign.Control.DataViewClass)this.Childrens["Children1"]).LastPage();
        }

        public void ChangePage(int PageNum)
        {
            ((FormDesign.Control.DataViewClass)this.Childrens["Children1"]).ChangePage(PageNum);
        }

        public void UpdatePageInfo(int RowCount,int PageNow)
        {
            this.ToolsBar.UpdatePageInfo(RowCount, PageNow);
        }

        public void SetHeadData(System.Data.DataRow dr)
        {
            this.HeadData = dr;
            this.FC.DataSource.Tables["Table" + this.ControlName.Split('_')[2].Replace("TabPage", "")].ShowDataRow = null;
            ((FormDesign.Control.DataViewClass)this.Childrens["Children1"]).SetBodySQL(string.Empty);//清空表身条件
            ((FormDesign.Control.DataViewClass)this.Childrens["Children1"]).SetHeadData(dr);
            ((FormDesign.Control.DataViewClass)this.Childrens["Children1"]).FirstPage();
         
        }
        /// <summary>
        /// 含有表身查询
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="bodySQL"></param>
        public void SetHeadData(System.Data.DataRow dr, string bodySQL)
        {
            this.HeadData = dr;
            this.FC.DataSource.Tables["Table" + this.ControlName.Split('_')[2].Replace("TabPage", "")].ShowDataRow = null;
            ((FormDesign.Control.DataViewClass)this.Childrens["Children1"]).SetHeadData(dr);
            ((FormDesign.Control.DataViewClass)this.Childrens["Children1"]).SetBodySQL(bodySQL);
            ((FormDesign.Control.DataViewClass)this.Childrens["Children1"]).FirstPage();
        }


        public void SetStatus(string Status)
        {
            this.Status = Status;
            this.Status = Status;

            Forms.FormBodyEditAndAdd frm;
            switch (Status)
            {
                case "Add":
                
                    frm = new Forms.FormBodyEditAndAdd("Add",this.FC, this, this.FC.DataSource.Tables["Table"+this.ControlName.Split('_')[2].Replace("TabPage","")], WebServiceUrl, Org);
                    frm.ShowDialog();
                    SetHeadData(this.HeadData);
                    SetStatus("Normal");
                    break;

                case "Edit":

                   

                    if (this.FC.DataSource.Tables["Table" + this.ControlName.Split('_')[2].Replace("TabPage", "")].ShowDataRow != null 
                        && !string.IsNullOrEmpty(this.FC.DataSource.Tables["Table" + this.ControlName.Split('_')[2].Replace("TabPage", "")].ShowDataRow["id"].ToString()))
                    {
                        frm = new Forms.FormBodyEditAndAdd("Edit", this.FC, this, this.FC.DataSource.Tables["Table" + this.ControlName.Split('_')[2].Replace("TabPage", "")], WebServiceUrl, Org);
                        frm.ShowDialog();
                        SetHeadData(this.HeadData);
                        SetStatus("Normal");
                    }
                    else
                    {
                        MessageBox.Show("请先选择数据");
                    }
                    break;
                case "Normal":
                    ToolsBar.SetStatus(Status);
                    foreach (string key in Childrens.Keys)
                    {
                        Childrens[key].SetStatus(Status);
                    }
                    break;
            }
        }

        
    }
}
