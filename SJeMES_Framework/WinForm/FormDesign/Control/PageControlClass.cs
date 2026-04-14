using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.FormDesign.Control
{
    public class PageControlClass : ControlClass
    {
        public bool Enable;
        public string Dock;
        public int PageRowsCount = 20;

        public PageControlClass()
        {
            try
            {
                Enable = true;
                ControlType = "PageControl";
                Dock = "Right";
             
            }
            catch { }
        }

        public Panel GetControls(string ParentName, Forms.FormClass FC)
        {
            this.ParentName = ParentName;
            this.FC = FC;
            Panel retPanel = new Panel();
            this.Control = retPanel;
            this.ControlName = retPanel.Name = this.ParentName + "_Panel" + ControlType;
            retPanel.Dock = DockStyle.Right;
            retPanel.Width = 560;

            Button btnFirst = new Button();
            btnFirst.Name = this.ControlName + "_btnFirstPage";
            btnFirst.Text = "首页";
            btnFirst.Width = 60;
            btnFirst.Click += B_Click;
            btnFirst.Dock = DockStyle.Right;
            btnFirst.Font = new System.Drawing.Font("微软雅黑", 9);
            btnFirst.ForeColor = System.Drawing.Color.Black;
            btnFirst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            Button btnBack = new Button();
            btnBack.Name = this.ControlName + "_btnBackPage";
            btnBack.Text = "上一页";
            btnBack.Width = 60;
            btnBack.Click += B_Click;
            btnBack.Dock = DockStyle.Right;
            btnBack.Font = new System.Drawing.Font("微软雅黑", 9);
            btnBack.ForeColor = System.Drawing.Color.Black;
            btnBack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            Button btnNext = new Button();
            btnNext.Name = this.ControlName + "_btnNextPage";
            btnNext.Text = "下一页";
            btnNext.Width = 60;
            btnNext.Click += B_Click;
            btnNext.Dock = DockStyle.Right;
            btnNext.Font = new System.Drawing.Font("微软雅黑", 9);
            btnNext.ForeColor = System.Drawing.Color.Black;
            btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;


            Button btnLast = new Button();
            btnLast.Name = this.ControlName + "_btnLastPage";
            btnLast.Text = "尾页";
            btnLast.Width = 60;
            btnLast.Click += B_Click;
            btnLast.Dock = DockStyle.Right;
            btnLast.Font = new System.Drawing.Font("微软雅黑", 9);
            btnLast.ForeColor = System.Drawing.Color.Black;
            btnLast.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            Panel panelPageNum = new Panel();
            panelPageNum.Dock = DockStyle.Right;
            panelPageNum.AutoSize = true;
            panelPageNum.Padding = new Padding(0, 5, 0, 0);

            Label l1 = new Label();
            l1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            l1.Text = "第";
            l1.AutoSize = true;
            l1.Padding = new Padding(0, 3, 0, 0);
            l1.Dock = DockStyle.Right;

            TextBox txtPageNum = new TextBox();
            txtPageNum.Name = this.ControlName + "_txtPageNum";
            txtPageNum.Width = 60;
            txtPageNum.Dock = DockStyle.Right;
            txtPageNum.Font = new System.Drawing.Font("微软雅黑", 9);
            txtPageNum.ForeColor = System.Drawing.Color.Black;
            txtPageNum.TextAlign = HorizontalAlignment.Center;
            txtPageNum.KeyPress += TxtPageNum_KeyPress;

            Label l2 = new Label();
            l2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            l2.Text = "页/共";
            l2.Padding = new Padding(0, 3, 0, 0);
            l2.AutoSize = true;
            l2.Dock = DockStyle.Right;

            Label labPagesCount = new Label();
            labPagesCount.Name = this.ControlName + "_labPagesCount";
            labPagesCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            labPagesCount.AutoSize = true;
            labPagesCount.Text = "0";
            labPagesCount.Padding = new Padding(0, 3, 0, 0);
            labPagesCount.Dock = DockStyle.Right;

            Label l3 = new Label();
            l3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            l3.AutoSize = true;
            l3.Text = "页";
            l3.Padding = new Padding(0, 3, 0, 0);
            l3.Dock = DockStyle.Right;

            Label labCount = new Label();
            labCount.Name = this.ControlName + "_labCount";
            labCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            labCount.Text = "共0条数据";
            labCount.Padding = new Padding(0, 3, 0, 0);
            labCount.AutoSize = true;
            labCount.Dock = DockStyle.Right;


            TextBox txtRows = new TextBox();
            txtRows.Name = this.ControlName + "_txtRows";
            txtRows.Text = this.PageRowsCount.ToString();
            txtRows.Width = 60;
            txtRows.Dock = DockStyle.Right;
            txtRows.Font = new System.Drawing.Font("微软雅黑", 9);
            txtRows.ForeColor = System.Drawing.Color.Black;
            txtRows.TextAlign = HorizontalAlignment.Center;
            txtRows.KeyPress += TxtRows_KeyPress; ;

            Label lab0 = new Label();
            lab0.Name = this.ControlName + "_lab0";
            lab0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lab0.Text = "行/页";
            lab0.Padding = new Padding(0, 3, 0, 0);
            lab0.AutoSize = true;
            lab0.Dock = DockStyle.Right;


            
            panelPageNum.Controls.Add(l1);
            panelPageNum.Controls.Add(txtPageNum);
            panelPageNum.Controls.Add(l2);
            panelPageNum.Controls.Add(labPagesCount);
            panelPageNum.Controls.Add(l3);
            panelPageNum.Controls.Add(labCount);
            panelPageNum.Controls.Add(txtRows);
            panelPageNum.Controls.Add(lab0);



            retPanel.Controls.Add(btnFirst);
            retPanel.Controls.Add(btnBack);
            retPanel.Controls.Add(panelPageNum);
            retPanel.Controls.Add(btnNext);
            retPanel.Controls.Add(btnLast);
            
            
            
           
            

            return retPanel;

        }

        private void TxtRows_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13 && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 13)
            {
                TextBox tb = sender as TextBox;

                this.PageRowsCount = Convert.ToInt32(tb.Text);

                foreach (string key in this.FC.FormPanels.Keys)
                {
                    if (this.ControlName.IndexOf(this.FC.FormPanels[key].ControlName) > -1)
                    {
                        (((FormDesign.Control.DataViewClass)((FormPanel.BodyPanelClass)this.FC.FormPanels[key]).Childrens["Children1"])).PageRowsCount = this.PageRowsCount;

                        ((FormPanel.BodyPanelClass)this.FC.FormPanels[key]).ChangePage(1);
                    }
                }

            }
        }

        private void TxtPageNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar!=8 && e.KeyChar !=13 && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if(e.KeyChar==13)
            {
                TextBox tb = sender as TextBox;
                Label labPagesCount = (Label)this.Control.Controls.Find(this.ControlName + "_labPagesCount", true)[0];
                if (Convert.ToInt32(tb.Text) > 0 && Convert.ToInt32(tb.Text) <= Convert.ToInt32(labPagesCount.Text))
                { 
                    foreach (string key in this.FC.FormPanels.Keys)
                    {
                        if (this.ControlName.IndexOf(this.FC.FormPanels[key].ControlName) > -1)
                        {
                            (((FormDesign.Control.DataViewClass)((FormPanel.BodyPanelClass)this.FC.FormPanels[key]).Childrens["Children1"])).PageRowsCount = this.PageRowsCount;

                            ((FormPanel.BodyPanelClass)this.FC.FormPanels[key]).ChangePage(Convert.ToInt32(tb.Text));
                        }
                    }
                }
            }
        }

        public void UpdatePageInfo(int RowCount,int PageNow)
        {
            Label labCount = (Label)this.Control.Controls.Find(this.ControlName + "_labCount",true)[0];
            labCount.Text = "共" + RowCount + "条数据";

            Label labPagesCount = (Label)this.Control.Controls.Find(this.ControlName + "_labPagesCount", true)[0];
            if (RowCount % PageRowsCount == 0)
            {
                labPagesCount.Text = (RowCount / PageRowsCount).ToString();
            }
            else
            {
                labPagesCount.Text = ((RowCount / PageRowsCount) +1).ToString();
            }

            TextBox txtPageNum = (TextBox)this.Control.Controls.Find(this.ControlName + "_txtPageNum", true)[0];
            txtPageNum.Text = PageNow.ToString();
        }

        public void setPageRowsCount(int PageRowsCount)
        {
            this.PageRowsCount = PageRowsCount;
        }

        private void B_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            
            switch (b.Name.Replace(this.ControlName+@"_",""))
            {
                case "btnNextPage":
                     foreach(string key in this.FC.FormPanels.Keys)
                    {
                        if (this.ControlName.IndexOf(this.FC.FormPanels[key].ControlName)>-1)
                        {
                            (((FormDesign.Control.DataViewClass)((FormPanel.BodyPanelClass)this.FC.FormPanels[key]).Childrens["Children1"])).PageRowsCount = this.PageRowsCount;

                            ((FormPanel.BodyPanelClass)this.FC.FormPanels[key]).NextPage();
                        }
                    }
                    break;
                case "btnBackPage":
                    foreach (string key in this.FC.FormPanels.Keys)
                    {
                        if (this.ControlName.IndexOf(this.FC.FormPanels[key].ControlName) > -1)
                        {
                            (((FormDesign.Control.DataViewClass)((FormPanel.BodyPanelClass)this.FC.FormPanels[key]).Childrens["Children1"])).PageRowsCount = this.PageRowsCount;

                            ((FormPanel.BodyPanelClass)this.FC.FormPanels[key]).BackPage();
                        }
                    }
                    break;
                case "btnFirstPage":
                    foreach (string key in this.FC.FormPanels.Keys)
                    {
                        if (this.ControlName.IndexOf(this.FC.FormPanels[key].ControlName) > -1)
                        {
                            (((FormDesign.Control.DataViewClass)((FormPanel.BodyPanelClass)this.FC.FormPanels[key]).Childrens["Children1"])).PageRowsCount = this.PageRowsCount;

                            ((FormPanel.BodyPanelClass)this.FC.FormPanels[key]).FirstPage();
                        }
                    }
                    break;
                case "btnLastPage":
                    foreach (string key in this.FC.FormPanels.Keys)
                    {
                        if (this.ControlName.IndexOf(this.FC.FormPanels[key].ControlName) > -1)
                        {
                            (((FormDesign.Control.DataViewClass)((FormPanel.BodyPanelClass)this.FC.FormPanels[key]).Childrens["Children1"])).PageRowsCount = this.PageRowsCount;

                            ((FormPanel.BodyPanelClass)this.FC.FormPanels[key]).LastPage();
                        }
                    }
                    break;

            }

        }
    }
}
