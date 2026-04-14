using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.FormDesign
{
    public partial class FormDesignTMP : Form
    {
        Forms.FormClass FC;
        public string WhereSql;

        bool isBodySelect = false;
        Label l_AppCode = new Label();
        public TabControl tabBody = new TabControl();
        public string bodySQL;

        public void SetWhereSql(string sql)
        {
            this.WhereSql = sql;
            FC.DataSource.Tables["Table1"].SqlWhere = sql;
        }


        public FormDesignTMP(Forms.FormClass FC)
        {
            InitializeComponent();

            this.FC = FC;

            try
            {
                this.Text = FC.FormTitle;
                this.Width = FC.FormWidth;
                this.Height = FC.FormHight;
                this.WindowState = FormWindowState.Normal;

                switch (FC.FormType)
                {
                    case "单表头":
                        InitOnlyHeadForm(FC);
                        break;
                    case "表头表身":
                        InitHeadBodyForm(FC);
                        break;
                }
            }
            catch (Exception EX) { MessageBox.Show(EX.Message); }

        }


        public FormDesignTMP(Forms.FormClass FC, string WhereSql)
        {
            InitializeComponent();
            this.WhereSql = WhereSql;

            this.FC = FC;

            try
            {
                this.Text = FC.FormTitle;
                this.Width = FC.FormWidth;
                this.Height = FC.FormHight;
                this.WindowState = FormWindowState.Normal;

                switch (FC.FormType)
                {
                    case "单表头":
                        InitOnlyHeadForm(FC);
                        break;
                    case "表头表身":
                        InitHeadBodyForm(FC);
                        break;
                }

                FC.DataSource.Tables["Table1"].SqlWhere = WhereSql;
               
            }
            catch (Exception EX) { MessageBox.Show(EX.Message); }

        }

        private void InitOnlyHeadForm(Forms.FormClass fC)
        {
            try
            {
                FormPanel.HeadPanelClass hpc = (FormPanel.HeadPanelClass)fC.FormPanels["Panel1"];

                l_AppCode.Dock = DockStyle.Bottom;
                l_AppCode.Text = fC.FormCode;

                this.Controls.Add(hpc.GetControls(this.Name, fC));
                this.Controls.Add(l_AppCode);


            }
            catch (Exception EX) { MessageBox.Show(EX.Message); }
        }


        private void InitHeadBodyForm(Forms.FormClass fC)
        {
            try
            {
                FormPanel.HeadPanelClass hpc = (FormPanel.HeadPanelClass)fC.FormPanels["Panel1"];
                Panel PanelHead = hpc.GetControls(this.Name, fC);

                ((Panel)hpc.Control).Dock = DockStyle.Top;
                ((Panel)hpc.Control).Height = hpc.Hight + 140;
                l_AppCode.Dock = DockStyle.Bottom;
                l_AppCode.Text = fC.FormCode;

               
                tabBody.Name = this.Name + "_TabBody";
                tabBody.Dock = DockStyle.Fill;
                tabBody.SelectedIndexChanged += TabBody_SelectedIndexChanged;

                for (int i = 2; i <= fC.PanelsCount; i++)
                {
                    FormPanel.BodyPanelClass bpc = (FormPanel.BodyPanelClass)fC.FormPanels["Panel" + i];
                    TabPage tp = new TabPage();
                    tp.Name = this.Name + "_TabBody_TabPage" + i;
                    tp.Text = bpc.Title;
                    tp.Controls.Add(bpc.GetControls(tp.Name, fC));
                    

                    tabBody.TabPages.Add(tp);
                }

                this.Controls.Add(tabBody);
                this.Controls.Add(PanelHead);
                this.Controls.Add(l_AppCode);


               

            }
            catch (Exception EX) { MessageBox.Show(EX.Message); }
        }

        private void TabBody_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FC.DataSource.Tables["Table1"].DataRow != null)
            {
                if (!isBodySelect)
                    ((FormPanel.BodyPanelClass)this.FC.FormPanels["Panel" + (tabBody.SelectedIndex + 2)]).SetHeadData(this.FC.DataSource.Tables["Table1"].DataRow);
                else
                {
                    isBodySelect = false;
                    ((FormPanel.BodyPanelClass)this.FC.FormPanels["Panel" + (tabBody.SelectedIndex + 2)]).SetHeadData(this.FC.DataSource.Tables["Table1"].DataRow, bodySQL);
                }
            }
        }

        private void FormDesignTMP_Load(object sender, EventArgs e)
        {
            FormPanel.HeadPanelClass hpc = (FormPanel.HeadPanelClass)FC.FormPanels["Panel1"];

            if(!string.IsNullOrEmpty(Program.LoadLastRow) && Program.LoadLastRow.ToLower()=="true")
                hpc.LoadLastRow();

        }

        private void FormDesignTMP_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void SetLabText(string msg)
        {
            l_AppCode.Text = FC.FormCode + ":" + msg;
        }
        /// <summary>
        /// 设置bodySQL
        /// </summary>
        /// <param name="bodyStr"></param>
        public void SetBodySQL(string bodyStr)
        {
            this.bodySQL = bodyStr;
            isBodySelect = true;
        }
    }
}
