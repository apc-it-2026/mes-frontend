using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.FormXML.Control
{
    public class DataViewClass : ControlClass
    {
        public List<int> DelId=new List<int>();
        public List<int> DelRow = new List<int>();
        public string Title;
        public string DataSource;
        public List<string> HeadKeys;
        public List<string> DataKeys;
        public List<string> OrderKeys;
        public List<string> OrderTypes;
        public string DataType;
        public int ColumnsCount;
        public ComboBox ComBox;
        public DateTimePicker dateTimePicker;

        public int EditRowIndex=-1;

        public string BodySQL;

        public Dictionary<string, DataViewColumnClass> Columns;

        public System.Data.DataRow HeadData;
        public System.Data.DataTable ShowData;
        public System.Data.DataTable Data;

        public Dictionary<int, DataRow> NewDatas;
        
        public int RowsCount;

        public bool RowEditing = false;

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
                    string strColumn = Common.StringHelper.GetDataFromFirstTag(strColumns, "<Column" + i + ">", "</Column" + i + ">");

                    Columns.Add("Column" + i, new DataViewColumnClass(this.FC, strColumn));
                }

            }
            catch { }
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
            

                DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                DGV.RowStateChanged += DGV_RowStateChanged;
                DGV.RowHeadersWidth = 50;


                DataGridViewCell dgvcell = new DataGridViewTextBoxCell();

                if (this.DataType == "ShowData")
                {
                    DGV.AutoGenerateColumns = true;
                }
                else
                {
                    

                    for (int i = 1; i <= this.ColumnsCount; i++)
                    {
                        DataViewColumnClass dvcc = this.Columns["Column" + i];
                        DataGridViewColumn dgvc;

                       dgvc = new DataGridViewTextBoxColumn();

                        dgvc.Name = "Column" + i;
                        dgvc.HeaderText = dvcc.Title;
                        dgvc.Width = dvcc.Width;
                        dgvc.DataPropertyName = dvcc.DataKey;



                       

                        dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        DGV.Columns.Add(dgvc);

                    }


                    DGV.SelectionChanged += DGV_SelectionChanged;
                    DGV.CellMouseDoubleClick += DGV_CellMouseDoubleClick;
                    DGV.KeyPress += DGV_KeyPress;
                    DGV.CurrentCellChanged += DGV_CurrentCellChanged;
                    DGV.Scroll += DGV_Scroll;
                    DGV.ColumnWidthChanged += DGV_ColumnWidthChanged;
                    DGV.CellLeave += DGV_CellLeave;
                    DGV.CellValueChanged += DGV_CellValueChanged; ;
                    DGV.UserDeletingRow += DGV_UserDeletingRow;
                }


                panelRet.Controls.Add(DGV);




            }
            catch { }
            return panelRet;
        }

        private void DGV_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
           
            e.Cancel = true;
            try
            {
                DataGridView dgv = sender as DataGridView;

               
                if (string.IsNullOrEmpty(this.ShowData.Rows[dgv.CurrentCell.RowIndex]["id"].ToString()) ||
                     this.ShowData.Rows[dgv.CurrentCell.RowIndex]["id"].ToString() == "-1")
                {

                    DelRow.Add(dgv.CurrentCell.RowIndex);
                }
                else
                {
                    DelId.Add(Convert.ToInt32(this.ShowData.Rows[dgv.CurrentCell.RowIndex]["id"].ToString()));
                    DelRow.Add(dgv.CurrentCell.RowIndex);
                }

                if (this.ShowData.Rows.Count > 0)
                {
                    dgv.Rows[this.ShowData.Rows.Count - 1].Cells[0].Selected = true;
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DGV_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (ComBox != null)
            //    ComBox.Visible = false;
            //if (dateTimePicker != null) 
            //    dateTimePicker.Visible = false;
        }

        private void DGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.RowEditing && e.ColumnIndex >-1)
            {
                DataGridView dgv = sender as DataGridView;
                try
                {
                    DataViewColumnClass dvcc = this.Columns["Column" + (e.ColumnIndex + 1)];

                   

                    if (!dvcc.IsNull && dvcc.Enable && (dvcc.Add || dvcc.Edit))
                    {
                        if (!string.IsNullOrEmpty(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
                        }
                        else
                        {
                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "该列数据不能为空";
                        }

                    }

                    if (dvcc.DataType=="Int" && dvcc.Enable && (dvcc.Add || dvcc.Edit))
                    {
                        if (!string.IsNullOrEmpty(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            try
                            {
                                Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                            }
                            catch
                            {
                                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                                this.ShowData.Rows[e.RowIndex][dvcc.DataKey] = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                                return;
                            }

                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
                        }
                        else
                        {
                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "该列数据只能填写整数";
                        }

                    }

                    if (dvcc.DataType == "Float" && dvcc.Enable && (dvcc.Add || dvcc.Edit))
                    {
                        if (!string.IsNullOrEmpty(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            try
                            {
                                Convert.ToDouble(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                            }
                            catch
                            {
                                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                                this.ShowData.Rows[e.RowIndex][dvcc.DataKey] = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                                return;
                            }

                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
                        }
                        else
                        {
                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "该列数据只能填写数字";
                        }

                    }

                    this.ShowData.Rows[e.RowIndex][dvcc.DataKey] = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DGV_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (ComBox != null)
                this.ComBox.Visible = false;
            if (dateTimePicker != null)
                dateTimePicker.Visible = false;
        }

        private void DGV_Scroll(object sender, ScrollEventArgs e)
        {
            if (ComBox != null)
                this.ComBox.Visible = false;
            if (dateTimePicker != null)
                dateTimePicker.Visible = false;
        }

        private void DGV_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if(ComBox !=null)
            ComBox.Visible = false;
            if(dateTimePicker !=null)
            dateTimePicker.Visible = false;
            if (Status == "Edit")
            {
              

                if (dgv.CurrentCell.RowIndex > -1 && dgv.CurrentCell.ColumnIndex > -1)
                {
                    if (EditRowIndex != dgv.CurrentCell.RowIndex )
                    {
                      if(checkRowData())
                            if (string.IsNullOrEmpty(ShowData.Rows[dgv.CurrentCell.RowIndex]["id"].ToString()))
                            {
                               

                                EndEdit();
                                EditRowIndex = dgv.CurrentCell.RowIndex;
                                SetDefaultValueData(dgv.CurrentCell.RowIndex);
                                EditRow();
                                
                            }
                            else
                            {
                                EndEdit();
                                EditRowIndex = dgv.CurrentCell.RowIndex;
                                EditRow();
                            }
                        
                    }

                  

                    DataViewColumnClass dvcc = this.Columns["Column" + (dgv.CurrentCell.ColumnIndex + 1)];
                    if(dvcc.DataType == "Enum" || dvcc.DataType == "Bool")
                    {
                        ComBox = new ComboBox();
                        ComBox.Enabled = dvcc.Enable;
                        
                        ComBox.Font = new System.Drawing.Font("黑体", 9);
                        ComBox.ForeColor = System.Drawing.Color.Black;
                        string Value = dgv.CurrentCell.Value.ToString();

                        if (dvcc.DataType == "Bool")
                        {
                            ComBox.Items.Add("是");
                            ComBox.Items.Add("否");

                            ComBox.Text = Value;
                        }
                        else if (dvcc.DataType == "Enum")
                        {
                            foreach (string key in dvcc.DataEnum.Keys)
                            {
                                ComBox.Items.Add(key);
                            }

                            ComBox.Text = Value;
                        }
                        ComBox.SelectedIndexChanged += ComBox_SelectedIndexChanged;
                        ComBox.LostFocus += ComBox_LostFocus;

                        dgv.Controls.Add(ComBox);

                        Rectangle rect = dgv.GetCellDisplayRectangle(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex, false);

                        ComBox.Left = rect.Left;
                        ComBox.Top = rect.Top;
                        ComBox.Width = rect.Width;
                        ComBox.Height = rect.Height;
                        ComBox.Visible = true;
                    }
                    else if(dvcc.DataType == "Date" || dvcc.DataType == "Time"||dvcc.DataType=="DateTime")
                    {
                        dateTimePicker = new DateTimePicker();
                        dateTimePicker.Enabled = dvcc.Enable;
                        dateTimePicker.CustomFormat = dvcc.DateTimeFormat;
                        dateTimePicker.Format = DateTimePickerFormat.Custom;
                        dateTimePicker.TextChanged += DateTimePicker_TextChanged;
                        dateTimePicker.LostFocus += DateTimePicker_LostFocus;
                        dgv.Controls.Add(dateTimePicker);

                       
                        Rectangle rect = dgv.GetCellDisplayRectangle(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex, false);

                        dateTimePicker.Left = rect.Left;
                        dateTimePicker.Top = rect.Top;
                        dateTimePicker.Width = rect.Width;
                        dateTimePicker.Height = rect.Height;
                        dateTimePicker.Visible = true;
                    }
                    else
                    {
                        if(ComBox!=null && ComBox.Visible)
                        {
                            ComBox.Visible = false;
                        }

                        if (dateTimePicker != null && dateTimePicker.Visible)
                        {
                            dateTimePicker.Visible = false;
                        }

                        dgv.BeginEdit(true);
                    }
                    
                }
            }
        }

        private void ComBox_LostFocus(object sender, EventArgs e)
        {
            DataGridView dgv = ((DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0]);
            DataViewColumnClass dvcc = this.Columns["Column" + (dgv.CurrentCell.ColumnIndex + 1)];
            this.ShowData.Rows[dgv.CurrentCell.RowIndex][dvcc.DataKey] = ComBox.Text;
            ComBox.Visible = false;
        }

        private void DateTimePicker_LostFocus(object sender, EventArgs e)
        {
            DataGridView dgv = ((DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0]);
            DataViewColumnClass dvcc = this.Columns["Column" + (dgv.CurrentCell.ColumnIndex + 1)];
            this.ShowData.Rows[dgv.CurrentCell.RowIndex][dvcc.DataKey] = dateTimePicker.Text;
            dateTimePicker.Visible = false;
        }

        private void DateTimePicker_TextChanged(object sender, EventArgs e)
        {
            DataGridView dgv = ((DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0]);
            DataViewColumnClass dvcc = this.Columns["Column" + (dgv.CurrentCell.ColumnIndex + 1)];
            this.ShowData.Rows[dgv.CurrentCell.RowIndex][dvcc.DataKey] = dateTimePicker.Text;
            dateTimePicker.Visible = false;
        }

        private void ComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridView dgv = ((DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0]);
            DataViewColumnClass dvcc = this.Columns["Column" + (dgv.CurrentCell.ColumnIndex + 1)];
            this.ShowData.Rows[dgv.CurrentCell.RowIndex][dvcc.DataKey] = ComBox.Text;
            ComBox.Visible = false;
        }

        private void DGV_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.KeyChar == 13)
            {
                if (this.Status == "Edit")
                {
                    if (checkRowData())
                    {
                        if (dgv.CurrentCell.RowIndex == dgv.Rows.Count - 1)
                        {
                            DataRow dr = this.ShowData.NewRow();
                            this.ShowData.Rows.Add(dr);
                        }

                    }
                }

            }

            

        }

        public bool checkRowData()
        {
          
            bool ret = true;
            if (EditRowIndex > -1 && EditRowIndex <this.ShowData.Rows.Count)
            {
                DataGridView dgv = ((DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0]);

                foreach (DataGridViewColumn dc in dgv.Columns)
                {
                    if (!string.IsNullOrEmpty(dgv.Rows[EditRowIndex].Cells[dc.Index].ErrorText))
                    {
                        MessageBox.Show("当前行数据存在错误，请确保录入数据正确");
                        dgv.Rows[EditRowIndex].Cells[dc.Index].Selected=true;
                        ret = false;
                        return ret;
                    }
                }


            }


            return ret;
        }

        private void DGV_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
            //if(e.Row.Index>=99)
            //{
            //    ((DataGridView)sender).RowHeadersWidth = 60;
            //}
        }

        private void DGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = ((DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0]);

            if (e.RowIndex > -1 && e.ColumnIndex >-1)
            {
                #region 打印报表？？？
                //if (!string.IsNullOrEmpty(FormReportCode))
                //{


                //    string doc_no = DGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                //    string serial_no = DGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                //    string sqlWhere = " and " + this.Columns["Column1"].DataKey + "='" +
                //        DGV.Rows[e.RowIndex].Cells[1].Value.ToString() + "' and " + this.Columns["Column2"].DataKey + "= '" + DGV.Rows[e.RowIndex].Cells[2].Value.ToString() + "'";
                //    FastReportForm.frmFastReport frm = new FastReportForm.frmFastReport(FormReportCode, Program.Org, Program.WebServiceUrl, sqlWhere);
                //    frm.ShowDialog();
                //} 
                #endregion

                if (this.Status == "Edit")
                    if (e.RowIndex == EditRowIndex)
                    {
                        DataViewColumnClass dvcc = this.Columns["Column" + (e.ColumnIndex + 1)];

                        if ((dvcc.Add || dvcc.Edit) && dvcc.DataType == "DataSource")
                        {
                            string sql = dvcc.DataSelectSQL;

                            if (sql.Contains("@DATA."))
                            {
                                List<string> ss = SJeMES_Framework.Common.StringHelper.GetDataFromTag(sql, "@DATA.", "@");

                                foreach (string s in ss)
                                {
                                    for (int i = 1; i <= this.Columns.Count; i++)
                                    {
                                        DataViewColumnClass dc = this.Columns["Column" + i];

                                        if (dc.DataKey == s)
                                        {
                                            sql = sql.Replace("@DATA." + s + "@", dgv.Rows[EditRowIndex].Cells[i - 1].Value.ToString());
                                        }
                                    }
                                }
                            }

                            CommonForm.frmSearchData frm = new CommonForm.frmSearchData(Org, WebServiceUrl, sql, true, true);
                            frm.ShowDialog();

                            string sTmp = string.Empty;

                            List<string> sRows = Common.StringHelper.GetDataFromTag(frm.ReturnDataXML, "<Row>", "</Row>");

                            if (sRows.Count > 0)
                            {
                                string Wheresql = string.Empty;

                                if (dvcc.Keys.Count > 0)
                                {
                                    sTmp = Common.StringHelper.GetDataFromFirstTag(frm.ReturnDataXML, "<" + dvcc.Keys[0] + ">", "</" + dvcc.Keys[0] + ">");
                                    dgv.Rows[EditRowIndex].Cells[e.ColumnIndex].Value = sTmp;
                                    SetOtherData();

                                }
                            }


                        }


                    }
            }
        }

        private void SetOtherData()
        {
            DataGridView dgv = ((DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0]);
            for(int i=1;i<this.Columns.Count;i++)
            {
                DataViewColumnClass dvcc = this.Columns["Column" + i];

                if(dvcc.DataType =="OtherData")
                {
                    int ColumnsIndex = -1;
                    for (int k = 1; k < this.Columns.Count; k++)
                    {
                        if(this.Columns["Column" + k].DataKey == dvcc.DefaultValue)
                        {
                            ColumnsIndex = k - 1;
                        }
                    }

                    if(ColumnsIndex >-1)
                        dgv.Rows[EditRowIndex].Cells[i - 1].Value = Common.WebServiceHelper.GetString(Org, WebServiceUrl, dvcc.DataSelectSQL.Replace("?", dgv.Rows[EditRowIndex].Cells[ColumnsIndex].Value.ToString()), new Dictionary<string, string>());
                }
            }
        }



        //private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (Status == "Edit")
        //    {
        //        if (e.ColumnIndex > -1 && e.RowIndex > -1)
        //        {
        //            if (EditRowIndex != e.RowIndex)
        //            {
        //                //if (!string.IsNullOrEmpty(ShowData.Rows[e.RowIndex-1]["id"].ToString()))
        //                //{
        //                //    EndEdit();
        //                //    EditRowIndex = e.RowIndex;
        //                //    SetDefaultValueData(e.RowIndex);
        //                //    EditRow();
        //                //}
        //                //else
        //                //{
        //                    EndEdit();
        //                    EditRowIndex = e.RowIndex;
        //                    EditRow();
        //                //}
        //            }
        //        }
                

        //    }
        //}

        private void EndEdit()
        {
            if (EditRowIndex > -1)
            {
                DataGridView dgv = ((DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0]);

                dgv.EndEdit();
                RowEditing = false;
                for (int i = 1; i <= dgv.ColumnCount; i++)
                {


                    dgv.Rows[EditRowIndex].Cells[i - 1].ReadOnly = true;
                    if(DelRow.Contains(EditRowIndex))
                        dgv.Rows[EditRowIndex].Cells[i - 1].Style.BackColor = Color.Red;
                    else
                    dgv.Rows[EditRowIndex].Cells[i - 1].Style.BackColor = SystemColors.Control;
                }

                if(string.IsNullOrEmpty(ShowData.Rows[EditRowIndex]["id"].ToString()))
                {
                    ShowData.Rows[EditRowIndex]["id"] = -1;
                }
            }


        }

        private void EditRow()
        {
            if (EditRowIndex > -1 )
            {


                DataGridView dgv = ((DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0]);

                RowEditing = true;

                for (int i = 1; i <= dgv.ColumnCount; i++)
                {

                    DataViewColumnClass dvcc = this.Columns["Column" + i];



                    if (dvcc.Enable)
                    {
                        if (dvcc.Edit || dvcc.Add)
                        {
                            if (dvcc.DataType == "String" || dvcc.DataType == "Int" || dvcc.DataType == "Float"
                      || dvcc.DataType == "OtherData" || dvcc.DataType == "DataSource" || dvcc.DataType == "Enum" || dvcc.DataType == "Bool")
                            {


                                if (dvcc.DataType == "DataSource")
                                {
                                    dgv.Rows[EditRowIndex].Cells[i - 1].Style.BackColor = Color.LightYellow;
                                    dgv.Rows[EditRowIndex].Cells[i - 1].ReadOnly = true;
                                }
                                else
                                {
                                    dgv.Rows[EditRowIndex].Cells[i - 1].Style.BackColor = Color.White;
                                    dgv.Rows[EditRowIndex].Cells[i - 1].ReadOnly = false;
                                }

                                if (dvcc.DataType == "SYSUserCode" || dvcc.DefaultValueType == "HeadData" ||dvcc.DefaultValue== "Sorting")
                                {
                                    dgv.Rows[EditRowIndex].Cells[i - 1].ReadOnly = true;
                                    dgv.Rows[EditRowIndex].Cells[i - 1].Style.BackColor = SystemColors.Control;
                                }

                                if (!dvcc.IsNull && dvcc.Enable && (dvcc.Add || dvcc.Edit))
                                {
                                    if (string.IsNullOrEmpty(dgv.Rows[EditRowIndex].Cells[i - 1].Value.ToString()))
                                    {
                                        dgv.Rows[EditRowIndex].Cells[i - 1].ErrorText ="该列不能为空";
                                    }
                                }
                            }
                           
                        }
                        else
                        {
                            dgv.Rows[EditRowIndex].Cells[i - 1].ReadOnly = true;
                            dgv.Rows[EditRowIndex].Cells[i - 1].Style.BackColor = SystemColors.Control;
                        }
                    }
                    else
                    {
                        dgv.Rows[EditRowIndex].Cells[i - 1].ReadOnly = true;
                        dgv.Rows[EditRowIndex].Cells[i - 1].Style.BackColor = SystemColors.Control;
                    }
                }
            }
        }

        private void DGV_SelectionChanged(object sender, EventArgs e)
        {
            if (Status != "Edit")
            {
                DataGridView DGV = ((DataGridView)this.Control.Controls[ParentName + "_DataView"]);
                if (DGV.SelectedRows.Count > 0)
                {
                    FC.DataSource.Tables["Table" + this.ParentName.Split('_')[2].Replace("TabPage", "")].ShowDataRow = ShowData.Rows[DGV.SelectedRows[0].Index];
                    FC.DataSource.Tables["Table" + this.ParentName.Split('_')[2].Replace("TabPage", "")].DataRow = Data.Rows[DGV.SelectedRows[0].Index];
                }
                if (DGV.SelectedCells.Count > 0)
                {
                    FC.DataSource.Tables["Table" + this.ParentName.Split('_')[2].Replace("TabPage", "")].ShowDataRow = ShowData.Rows[DGV.SelectedCells[0].RowIndex];
                    FC.DataSource.Tables["Table" + this.ParentName.Split('_')[2].Replace("TabPage", "")].DataRow = Data.Rows[DGV.SelectedCells[0].RowIndex];
                }
            }
          
        }

        public void ClearData()
        {
            DelId = new List<int>();
            ((DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0]).DataSource = new System.Data.DataTable().DefaultView;
        }

        public void SetHeadData(System.Data.DataRow HeadData)
        {
            DelId = new List<int>();
            this.HeadData = HeadData;
            LoadData(HeadData);
        }

        /// <summary>
        /// 设置表身SQL条件
        /// </summary>
        /// <param name="bodySql"></param>
        public void SetBodySQL(string bodySql)
        {
            this.BodySQL = bodySql;
        }

        public void LoadData(System.Data.DataRow HeadData)
        {
            DateTime d1 = DateTime.Now;
            try
            {
                this.HeadData = HeadData;
                

                DataGridView dgv = ((DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0]);

                if (this.DataType == "ShowData")
                {
                    string sql = FC.DataSource.Tables[this.DataSource].SearchSQL.Replace(this.HeadKeys[0].Split(':')[1], this.HeadData[this.HeadKeys[0].Split(':')[0]].ToString());

                    this.Data = this.ShowData = FC.DataSource.Tables[this.DataSource].DataTable= FC.DataSource.Tables[this.DataSource].GetShowDataTable(Org, WebServiceUrl, sql);


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
                    this.Data = this.ShowData = FC.DataSource.Tables[this.DataSource].DataTable = FC.DataSource.Tables[this.DataSource].GetDataTable(Org, WebServiceUrl, strWhere);

                    try
                    {
                        dgv.DataSource = this.ShowData.DefaultView;
                    }
                    catch { }


                    //增加拼接SQL条件 BodySQL
                    this.RowsCount = FC.DataSource.Tables[this.DataSource].GetRowsCount(Org, WebServiceUrl, strWhere);

                    
                }


                

               


                ShowOtherData();


                DateTime d2 = DateTime.Now;

                //((FormDesignTMP)((FormPanel.HeadPanelClass)FC.FormPanels["Panel1"]).Control.FindForm()).SetLabText("加载数据用时" + d2.Subtract(d1).TotalSeconds.ToString() + @"秒");
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

                        for (int i = 1; i <= dgv.ColumnCount; i++)
                        {

                            DataViewColumnClass dvcc = this.Columns["Column" + i];
                            DataGridViewColumn dgvc = dgv.Columns[i - 1];

                            dgvc.DefaultCellStyle.BackColor = SystemColors.Control;
                            dgvc.ReadOnly = true;

                        }


                        if (this.HeadData != null)
                            SetHeadData(this.HeadData);

                        DelId = new List<int>();
                        break;

                    case "Edit":
                        dgv.AllowUserToAddRows = false;
                        dgv.AllowUserToDeleteRows = true;
                        dgv.EditMode = DataGridViewEditMode.EditProgrammatically;

                        if(this.ShowData.Rows.Count ==0)
                        {
                            DataRow dr = this.ShowData.NewRow();
                            this.ShowData.Rows.Add(dr);
                        }

                        break;
                    case "Add":
                        ClearData();
                        
                        //SetStatus("Edit");


                        //for (int i=1;i<=dgv.ColumnCount;i++)
                        //{

                        //    DataViewColumnClass dvcc = this.Columns["Column" + i];
                        //    DataGridViewColumn dgvc = dgv.Columns[i-1];


                        //    if (dvcc.Enable)
                        //    {
                        //        if (dvcc.Edit || dvcc.Add)
                        //        {
                        //            if (dvcc.DataType == "String" || dvcc.DataType == "Int" || dvcc.DataType == "Float"
                        //      || dvcc.DataType == "OtherData" || dvcc.DataType == "DataSource" || dvcc.DataType == "SYSUserCode"
                        //      || dvcc.DataType == "HeadDataShow")
                        //            {


                        //                if (dvcc.DataType == "DataSource")
                        //                {
                        //                    dgvc.DefaultCellStyle.BackColor = Color.LightYellow;
                        //                    dgvc.ReadOnly = true;
                        //                }
                        //                else
                        //                {
                        //                    dgvc.DefaultCellStyle.BackColor = Color.White;
                        //                }

                        //                if (dvcc.DataType == "SYSUserCode" || dvcc.DataType == "HeadDataShow")
                        //                {
                        //                    dgvc.ReadOnly = true;
                        //                    dgvc.DefaultCellStyle.BackColor = SystemColors.Control;
                        //                }
                        //            }
                        //            else if (dvcc.DataType == "Enum" || dvcc.DataType == "Bool")
                        //            {

                        //            }
                        //            else
                        //            {

                        //            }
                        //        }
                        //        else
                        //        {
                        //            dgvc.ReadOnly = true;
                        //            dgvc.DefaultCellStyle.BackColor = SystemColors.Control;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        dgvc.ReadOnly = true;
                        //        dgvc.DefaultCellStyle.BackColor = SystemColors.Control;
                        //    }
                        //}

                        break;
                }
            
            }
        }

        public void SetDefaultValueData(int RowIndex)
        {
            try
            {
                

                DataGridView dgv = (DataGridView)this.Control.Controls.Find(this.ParentName + "_DataView", false)[0];
                for (int i = 1; i <= dgv.ColumnCount; i++)
                {

                    DataViewColumnClass dvcc = this.Columns["Column" + i];


                     dgv.Rows[RowIndex].Cells[i - 1].Value = dvcc.DefaultValue;

                    if(dvcc.DefaultValue.ToLower() == "sorting")
                    {
                        dgv.Rows[RowIndex].Cells[i - 1].Value = (getMaxSorting(dgv, i - 1)+1).ToString("000");
                        //if(dgv.Rows[RowIndex].Cells[i - 1].Value.ToString().ToLower() =="sorting")
                        //{
                        //    dgv.Rows[RowIndex].Cells[i - 1].Value = "001";
                        //}
                    }

                    if (dvcc.DataType == "SYSUserCode" ||dvcc.DefaultValueType == "SYSUserCode")
                    {

                        dgv.Rows[RowIndex].Cells[i - 1].Value = Program.Org.User.UserCode;
                    }



                    if (dvcc.DataType == "OtherData")
                    {
                        dgv.Rows[RowIndex].Cells[i - 1].Value = Common.WebServiceHelper.GetString(Org, WebServiceUrl, dvcc.DataShowSQL.Replace("?", dvcc.DefaultValue), new Dictionary<string, string>());
                    }

                    if (dvcc.DefaultValueType == "HeadData")
                    {
                        if (dvcc.DataType == "HeadDataShow")
                        {
                            dgv.Rows[RowIndex].Cells[i - 1].Value = Common.WebServiceHelper.GetString(Org, WebServiceUrl, dvcc.DataShowSQL.Replace("?", HeadData[dvcc.DefaultValue].ToString()), new Dictionary<string, string>());
                        }
                        else
                        {
                            dgv.Rows[RowIndex].Cells[i - 1].Value = HeadData[dvcc.DefaultValue].ToString();
                        }
                    }

                }

                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int getMaxSorting(DataGridView dgv,int ColumnIndex)
        {
            int ret = 0;

            foreach(DataGridViewRow dgvr in dgv.Rows)
            {
                string num = dgvr.Cells[ColumnIndex].Value.ToString();
                if(num.ToLower() == "sorting")
                {
                    continue;
                }

                while(num.StartsWith("0"))
                {
                    num = num.Substring(1);
                }

                if (Convert.ToInt32(num) >ret)
                {
                    ret = Convert.ToInt32(num);
                }
            }

            return ret;
        }
    }
}
