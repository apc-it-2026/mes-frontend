using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.FormDesign.Control
{
    public class DataViewClass : ControlClass
    {
        public string Title;
        public string DataSource;
        public List<string> HeadKeys;
        public List<string> DataKeys;
        public List<string> OrderKeys;
        public List<string> OrderTypes;
        public string DataType;
        public int ColumnsCount;
        public int PageRowsCount;

        public string BodySQL;

        public Dictionary<string, DataViewColumnClass> Columns;

        public System.Data.DataRow HeadData;
        public System.Data.DataTable ShowData;
        public System.Data.DataTable Data;
        public int PageNum;
        public int RowsCount;
        public int PageCount;

        public Class.OrgClass Org;

        public string WebServiceUrl;

        private string FormReportCode;

        public DataViewClass(string XML, string WebServiceUrl,Class.OrgClass Org)
        {
            try
            {
                this.Org = Org;
                this.WebServiceUrl = WebServiceUrl;
                Title = Common.StringHelper.GetDataFromFirstTag(XML, "<Title>", "</Title>").Trim();
                DataSource = Common.StringHelper.GetDataFromFirstTag(XML, "<DataSource>", "</DataSource>").Trim();
                DataType = Common.StringHelper.GetDataFromFirstTag(XML, "<DataType>", "</DataType>").Trim();
                ControlType = Common.StringHelper.GetDataFromFirstTag(XML, "<ControlType>", "</ControlType>").Trim();
                ColumnsCount = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<ColumnsCount>", "</ColumnsCount>").Trim());
                PageRowsCount = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<PageRowsCount>", "</PageRowsCount>").Trim());



                string[] split = new string[1];

                HeadKeys = new List<string>();
                string strHeadKeys = Common.StringHelper.GetDataFromFirstTag(XML, "<HeadKeys>", "</HeadKeys>").Trim();
                if (strHeadKeys.IndexOf(";") > -1)
                {
                    split[0] = ";";
                    foreach (string s in strHeadKeys.Split(split, StringSplitOptions.RemoveEmptyEntries))
                    {
                        HeadKeys.Add(s);
                    }
                }
                else
                {
                    HeadKeys.Add(strHeadKeys);
                }

                DataKeys = new List<string>();
                string strDataKeys = Common.StringHelper.GetDataFromFirstTag(XML, "<DataKeys>", "</DataKeys>").Trim();
                if (strDataKeys.IndexOf(",") > -1)
                {
                    foreach (string s in strDataKeys.Split(','))
                    {
                        DataKeys.Add(s);
                    }
                }
                else
                {
                    DataKeys.Add(strDataKeys);
                }

                OrderKeys = new List<string>();
                string strOrderKeys = Common.StringHelper.GetDataFromFirstTag(XML, "<OrderKeys>", "</OrderKeys>").Trim();
                if (strOrderKeys.IndexOf(",") > -1)
                {
                    foreach (string s in strOrderKeys.Split(','))
                    {
                        OrderKeys.Add(s);
                    }
                }
                else
                {
                    OrderKeys.Add(strOrderKeys);
                }

                OrderTypes = new List<string>();
                string strOrderTypes = Common.StringHelper.GetDataFromFirstTag(XML, "<OrderTypes>", "</OrderTypes>").Trim();
                if (strOrderTypes.IndexOf(",") > -1)
                {
                    foreach (string s in strOrderTypes.Split(','))
                    {
                        OrderTypes.Add(s);
                    }
                }
                else
                {
                    OrderTypes.Add(strOrderTypes);
                }

                Columns = new Dictionary<string, DataViewColumnClass>();
                string strColumns = Common.StringHelper.GetDataFromFirstTag(XML, "<Columns>", "</Columns>").Trim();
                for (int i = 1; i <= ColumnsCount; i++)
                {
                    if(i==1)
                    {
                        var a = "";
                    }
                    string strColumn = Common.StringHelper.GetDataFromFirstTag(strColumns, "<Column" + i + ">", "</Column" + i + ">");

                    Columns.Add("Column" + i, new DataViewColumnClass(this.FC, strColumn));
                }

            }
            catch { }
        }

        internal void SetPageRowsCount(int pageRowsCount)
        {
            this.PageRowsCount = pageRowsCount;
        }

        public Panel GetControls(string ParentName, Forms.FormClass FC)
        {
            this.ParentName = ParentName;
            this.FC = FC;
            Panel panelRet = new Panel();
            
            try
            {
                FormReportCode = FC.FormReportCode;
                this.Control = panelRet;
                this.ControlName = panelRet.Name = ParentName + "_PanelDataView" + this.Title;
                panelRet.Dock = DockStyle.Fill;
                panelRet.Margin = new Padding(0);

                DataGridView DGV = new DataGridView();
                DGV.Name = ParentName + "_DataView";
                DGV.Dock = DockStyle.Fill;
                DGV.AutoGenerateColumns = false;

                DGV.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

                DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


                DataGridViewCell dgvcell = new DataGridViewTextBoxCell();

                if (this.DataType == "ShowData")
                {
                    DGV.AutoGenerateColumns = true;
                }
                else
                {
                    DataGridViewColumn dgvcRow = new DataGridViewColumn();
                    dgvcRow.Name = "Column0";
                    dgvcRow.HeaderText = "行号";
                    dgvcRow.Width = 60;
                    dgvcRow.DataPropertyName = "行号";
                    dgvcRow.CellTemplate = dgvcell;

                    DGV.DataSource = new System.Data.DataTable().DefaultView;
                    DGV.Columns.Add(dgvcRow);



                    for (int i = 1; i <= this.ColumnsCount; i++)
                    {
                        DataViewColumnClass dvcc = this.Columns["Column" + i];
                        DataGridViewColumn dgvc = new DataGridViewColumn();
                        dgvc.Name = dvcc.DataKey;
                        dgvc.Name = "Column" + i;
                        dgvc.HeaderText = dvcc.Title;
                        dgvc.Width = dvcc.Width;
                        dgvc.DataPropertyName = dvcc.DataKey;


                       
                        dgvcell = new DataGridViewTextBoxCell();
                        
                        dgvc.CellTemplate = dgvcell;

                        dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        DGV.Columns.Add(dgvc);

                    }


                    DGV.SelectionChanged += DGV_SelectionChanged;
                    DGV.CellClick += DGV_CellClick;
                    DGV.CellMouseDoubleClick += DGV_CellMouseDoubleClick; ;
                }


                panelRet.Controls.Add(DGV);




            }
            catch { }
            return panelRet;
        }

        private void DGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView DGV = sender as DataGridView;

            if (e.RowIndex > -1)
            {
                if (!string.IsNullOrEmpty(FormReportCode))
                {

                   
                    string doc_no = DGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string serial_no = DGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                    string sqlWhere = " and " + this.Columns["Column1"].DataKey + "='" +
                        DGV.Rows[e.RowIndex].Cells[1].Value.ToString() + "' and " + this.Columns["Column2"].DataKey + "= '" + DGV.Rows[e.RowIndex].Cells[2].Value.ToString() + "'";
                    FastReportForm.frmFastReport frm = new FastReportForm.frmFastReport(FormReportCode, Program.Org, Program.WebServiceUrl, sqlWhere);
                    frm.ShowDialog();
                }

            }
        }

        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DGV_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView DGV = ((DataGridView)this.Control.Controls[ParentName + "_DataView"]);
            if (DGV.SelectedRows.Count>0)
            {
                FC.DataSource.Tables["Table" + this.ParentName.Split('_')[2].Replace("TabPage", "")].ShowDataRow = ShowData.Rows[DGV.SelectedRows[0].Index];
                FC.DataSource.Tables["Table" + this.ParentName.Split('_')[2].Replace("TabPage", "")].DataRow = Data.Rows[DGV.SelectedRows[0].Index];
            }
            if(DGV.SelectedCells.Count>0)
            {
                FC.DataSource.Tables["Table" + this.ParentName.Split('_')[2].Replace("TabPage", "")].ShowDataRow = ShowData.Rows[DGV.SelectedCells[0].RowIndex];
                FC.DataSource.Tables["Table" + this.ParentName.Split('_')[2].Replace("TabPage", "")].DataRow = Data.Rows[DGV.SelectedCells[0].RowIndex];
            }
        }

        public void ClearData()
        {
            ((DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0]).DataSource = new System.Data.DataTable().DefaultView;
        }

        public void SetHeadData(System.Data.DataRow HeadData)
        {
            this.HeadData = HeadData;
        }

        /// <summary>
        /// 设置表身SQL条件
        /// </summary>
        /// <param name="bodySql"></param>
        public void SetBodySQL(string bodySql)
        {
            this.BodySQL = bodySql;
        }

        public void LoadData(System.Data.DataRow HeadData, int PageNum, int PageRowsCount)
        {
            DateTime d1 = DateTime.Now;
            try
            {
                this.HeadData = HeadData;
                this.PageNum = PageNum;
                this.PageRowsCount = PageRowsCount;

                DataGridView dgv = ((DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0]);

                if (this.DataType == "ShowData")
                {
                    string sql = FC.DataSource.Tables[this.DataSource].SearchSQL.Replace(this.HeadKeys[0].Split(':')[1], this.HeadData[this.HeadKeys[0].Split(':')[0]].ToString());

                    this.Data = this.ShowData = FC.DataSource.Tables[this.DataSource].DataTable= FC.DataSource.Tables[this.DataSource].GetShowDataTable(Org, WebServiceUrl, sql, this.PageNum, this.PageRowsCount);


                    dgv.DataSource = this.ShowData.DefaultView;

                    this.RowsCount = FC.DataSource.Tables[this.DataSource].GetShowRowsCount(Org, WebServiceUrl, sql);
                }
                else
                {
                    string strWhere = string.Empty;

                    foreach (string s in HeadKeys)
                    {
                        string Key = s.Split(':')[1];
                        string Value = s.Split(':')[0];

                        strWhere += " AND [" + Key + "]='" + HeadData[Value] + @"' ";
                    }
                    //增加拼接SQL条件 BodySQL
                    this.Data = this.ShowData = FC.DataSource.Tables[this.DataSource].DataTable = FC.DataSource.Tables[this.DataSource].GetDataTable(Org, WebServiceUrl, strWhere, this.PageNum, this.PageRowsCount);


                    dgv.DataSource = this.ShowData.DefaultView;


                    //增加拼接SQL条件 BodySQL
                    this.RowsCount = FC.DataSource.Tables[this.DataSource].GetRowsCount(Org, WebServiceUrl, strWhere);

                    
                }


                if (RowsCount % PageRowsCount == 0)
                {
                    this.PageCount = RowsCount / PageRowsCount;
                }
                else
                {
                    this.PageCount = (RowsCount / PageRowsCount) + 1;
                }

                foreach (string key in this.FC.FormPanels.Keys)
                {
                    if (this.ControlName.IndexOf(this.FC.FormPanels[key].ControlName) > -1)
                    {
                        ((FormPanel.BodyPanelClass)this.FC.FormPanels[key]).UpdatePageInfo(this.RowsCount, this.PageNum);
                    }
                }


                ShowOtherData();


                DateTime d2 = DateTime.Now;

                ((FormDesignTMP)((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).Control.FindForm()).SetLabText("加载数据用时" + d2.Subtract(d1).TotalSeconds.ToString() + @"秒");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void ShowOtherData()
        {
            try
            {
                DataGridView dgv = ((DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0]);
                for(int i=1; i<= this.Columns.Keys.Count;i++)
                {
                    DataViewColumnClass dvcc = this.Columns["Column"+i];

                    int k = 0;
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {
                        if (dvcc.DataType == "DataSource")
                        {
                        //    if (dvcc.DataType == "DataSource" || dvcc.DataType == "OtherData")
                        //{
                            //if(dgvr.Cells["Column" + i].Value ==null && dvcc.DataType=="OtherData")
                            //{
                            //    foreach (string s in this.Columns.Keys)
                            //    {
                            //        if (this.Columns[s].DataKey == dvcc.DefaultValue)
                            //        {
                            //            dgvr.Cells["Column" + i].Value = dgvr.Cells[s].Value;
                            //        }
                            //    }
                            //}

                           
                            dgvr.Cells["Column" + i].Value = Common.WebServiceHelper.GetString(Org, WebServiceUrl, dvcc.DataShowSQL.Replace("?", dgvr.Cells["Column" + i].Value.ToString()), new Dictionary<string, string>());

                        }
                        else if (dvcc.DataType == "Enum")
                        {
                            foreach (string key in dvcc.DataEnum.Keys)
                            {
                                if (dvcc.DataEnum[key] == dgvr.Cells["Column" + i].Value.ToString())
                                {
                                    dgvr.Cells["Column" + i].Value = key;
                                }
                            }
                        }
                        else if(dvcc.DataType == "Bool")
                        {
                            if(dgvr.Cells["Column" + i].Value.ToString().ToLower()=="true")
                            {
                                dgvr.Cells["Column" + i].Value = "是";
                            }
                            else
                            {
                                dgvr.Cells["Column" + i].Value = "否";
                            }
                        }
                    }
                    k++;
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChangePage(int PageNum)
        {
           
                LoadData(this.HeadData, PageNum, this.PageRowsCount);
            
        }


        public void NextPage()
        {
            if ((PageNum + 1) <= PageCount)
            {
                LoadData(this.HeadData, PageNum + 1, this.PageRowsCount);
            }
        }

        public void BackPage()
        {
            if ((PageNum - 1) >= 1)
            {
                LoadData(this.HeadData, PageNum - 1, this.PageRowsCount);
            }
        }

        public void FirstPage()
        {
            if(this.HeadData!=null)
              LoadData(this.HeadData, 1, this.PageRowsCount);

        }

        public void LastPage()
        {
            if (this.HeadData != null)
                LoadData(this.HeadData, this.PageCount, this.PageRowsCount);

        }

        public void SetStatus(string Status)
        {
            this.Status = Status;
            if (this.Control != null)
            {
                
                DataGridView dgv = (DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0];
                switch (Status)
                {
                    case "Normal":
                        
                        dgv.AllowUserToAddRows = false;
                        dgv.AllowUserToDeleteRows = false;
                        dgv.EditMode = DataGridViewEditMode.EditProgrammatically;

                        if(this.HeadData !=null)
                            FirstPage();

                        break;

                    case "Edit":
                        if(dgv.SelectedCells.Count >0)
                        {
                            string tableNum = dgv.Name.Split('_')[2].Replace("TabPage","");
                            Forms.FormBodyEditAndAdd frm = new Forms.FormBodyEditAndAdd("Edit",this.FC, this.FC.FormPanels["Panel"+ tableNum], this.FC.DataSource.Tables["Table"+tableNum],WebServiceUrl,Org);
                            frm.ShowDialog();
                            SetStatus("Normal");
                        }
                        else
                        {
                            MessageBox.Show("请先选择数据");
                            SetStatus("Normal");
                        }
                        break;
                }
            
            }
        }
    }
}
