using F_SendReceive_Manage;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_Work_SendReceive_Manage
{

    public partial class F_Work_SendReceive_Manage : MaterialForm
    {
        DataTable SampleInfo = new DataTable();
        DataTable SampleInfo2 = new DataTable();
        DataTable SampleSizeTable = new DataTable();

        DateTimePicker dtp = new DateTimePicker();
        DateTimePicker dtp2 = new DateTimePicker();
        Rectangle _Rectangle;


        public F_Work_SendReceive_Manage()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            dataGridView3.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView1.AutoGenerateColumns = false;
            this.dataGridView3.Controls.Add(dtp);
            this.dataGridView2.Controls.Add(dtp2);
            dtp.Visible = false;  //先不显示
            dtp2.Visible = false;  //先不显示
            //dtp.Format = DateTimePickerFormat.Custom;  //设置日期格式，2017-11-11
           // dtp.TextChanged += new EventHandler(dtp_TextChange); //为时间控件加入事件dtp_TextChange
            dtp.CloseUp += new EventHandler(dtp_TextChange);
            dtp2.CloseUp += new EventHandler(dtp_TextChange2);

            GetComboxList();
            Column12.DefaultCellStyle.NullValue = "查看";
            this.comboBox2.SelectedValue = "1";
            this.button2.Text = this.comboBox2.Text;
            this.comboBox1.SelectedValue = "1";
            this.button9.Text = this.comboBox1.Text;
            GetSampleList();
        }

        public void GetComboxList()
        {
            List<ComboboxEntry> InOut = new List<ComboboxEntry> { };
            InOut.Add(new ComboboxEntry() { ENUM_CODE = "", ENUM_VALUE = "" });
            List<ComboboxEntry> status = new List<ComboboxEntry> { };
            status.Add(new ComboboxEntry() { ENUM_CODE = "", ENUM_VALUE = "" });
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "GetComboxList", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, DataTable> dictionary1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, DataTable>>(json);
                DataTable dtJson = dictionary1["INOUT"];
                DataTable dt = dictionary1["STATUS"];
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    InOut.Add(new ComboboxEntry() { ENUM_CODE = dtJson.Rows[i]["ENUM_CODE"].ToString(), ENUM_VALUE = dtJson.Rows[i]["ENUM_VALUE"].ToString() });
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    status.Add(new ComboboxEntry() { ENUM_CODE = dt.Rows[i]["ENUM_CODE"].ToString(), ENUM_VALUE = dt.Rows[i]["ENUM_VALUE"].ToString() });
                }

                cbStatus1.DataSource = status;
                cbStatus1.DisplayMember = "ENUM_VALUE";
                cbStatus1.ValueMember = "ENUM_CODE";
                cbSendReceiveType1.DataSource = InOut;
                cbSendReceiveType1.DisplayMember = "ENUM_VALUE";
                cbSendReceiveType1.ValueMember = "ENUM_CODE";

                List<ComboboxEntry> Inout1 = new List<ComboboxEntry> { };
                InOut.ForEach(i => Inout1.Add(i));
                Column10.DataSource = Inout1;
                Column10.DisplayMember = "ENUM_VALUE";
                Column10.ValueMember = "ENUM_CODE";
                List<ComboboxEntry> status1 = new List<ComboboxEntry> { };
                status.ForEach(i => status1.Add(i));
                List<ComboboxEntry> status2 = new List<ComboboxEntry> { };
                status.ForEach(i => status2.Add(i));
                List<ComboboxEntry> status3 = new List<ComboboxEntry> { };
                status.ForEach(i => status3.Add(i));

                Column11.DataSource = status1;
                Column11.DisplayMember = "ENUM_VALUE";
                Column11.ValueMember = "ENUM_CODE";
                comboBox1.DataSource = status2;
                comboBox1.DisplayMember = "ENUM_VALUE";
                comboBox1.ValueMember = "ENUM_CODE";
                comboBox2.DataSource = status3;
                comboBox2.DisplayMember = "ENUM_VALUE";
                comboBox2.ValueMember = "ENUM_CODE";
            }
        }

        public void GetSampleList()
        {
            var orderSource = new AutoCompleteStringCollection();
            string sql = string.Format(@" select distinct sample_no from mes_sample_logo_list");
            Dictionary<string, string> valuePairs = new Dictionary<string, string>();
            valuePairs.Add("sql", sql);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Sample_ReceiveService", "GetSQLData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(valuePairs));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    orderSource.Add(dataTable.Rows[i]["sample_no"].ToString());
                }



                txtSampleNo2.AutoCompleteCustomSource = orderSource;
                txtSampleNo2.AutoCompleteMode = AutoCompleteMode.Suggest;    //显示相关下拉
                txtSampleNo2.AutoCompleteSource = AutoCompleteSource.CustomSource;   //设置属性

                txtSampleNo3.AutoCompleteCustomSource = orderSource;
                txtSampleNo3.AutoCompleteMode = AutoCompleteMode.Suggest;    //显示相关下拉
                txtSampleNo3.AutoCompleteSource = AutoCompleteSource.CustomSource;   //设置属性

                txtSampleNo1.AutoCompleteCustomSource = orderSource;
                txtSampleNo1.AutoCompleteMode = AutoCompleteMode.Suggest;    //显示相关下拉
                txtSampleNo1.AutoCompleteSource = AutoCompleteSource.CustomSource;   //设置属性
            }
        }
        private void dtp_TextChange(object sender, EventArgs e)
        {
            dataGridView3.CurrentCell.Value = dtp.Value.ToString("yyyy/MM/dd");  //时间控件选择时间时，将时间内容赋给所在的单元格
        }

        private void dtp_TextChange2(object sender, EventArgs e)
        {
            dataGridView2.CurrentCell.Value = dtp2.Value.ToString("yyyy/MM/dd");  //时间控件选择时间时，将时间内容赋给所在的单元格
        }

        private void btnBadRegister1_Click(object sender, EventArgs e)
        {
            Bad_Registration bad_Registration = new Bad_Registration();
            bad_Registration.ShowDialog();
        }

        private void btnSelectSampleOrder_Click(object sender, EventArgs e)
        {
            CreateReceOrder();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sampleId", this.txtSampleNo2.Text.ToString().Trim());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "GetSampleOrder", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, DataTable> dictionary1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, DataTable>>(json);
                DataTable dtJson = dictionary1["Header"];

                SampleInfo = dtJson;
                this.txtArtNo2.Text = dtJson.Rows[0]["ART_NO"].ToString();
                this.txtArtName2.Text = dtJson.Rows[0]["NAME_T"].ToString();
                this.txtPurpose2.Text = dtJson.Rows[0]["PURPOSE"].ToString();
                this.txtSeason2.Text = dtJson.Rows[0]["SEASON"].ToString();
                this.txtSendUnit2.Text = dtJson.Rows[0]["staff_department"].ToString();
                this.txtMatchColor2.Text = dtJson.Rows[0]["COLOR_WAY"].ToString();
                this.txtTypeMaster2.Text = dtJson.Rows[0]["TYPE_LEADER"].ToString();
                this.txtSampleDesigner2.Text = dtJson.Rows[0]["SAMPLE_LEADER"].ToString();
                this.dataGridView3.DataSource = dictionary1["body"];
                dataGridView3.ReadOnly = false;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btnSelectDetails2_Click(object sender, EventArgs e)
        {

            string sample_no = txtSampleNo2.Text.ToString();
            string art_no = txtArtNo2.Text.ToString();
            string art_name = txtArtName2.Text.ToString();
            string purpose = txtPurpose2.Text.ToString();
            string season = txtSeason2.Text.ToString();

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("sample", txtSampleNo2.Text.Trim().ToString());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "QuertBtnInfo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retData);
                SampleInfo sampleInfo = new SampleInfo(dataTable, sample_no, art_no, art_name, purpose, season);
                sampleInfo.ShowDialog();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            CreateReceOrder();
        }



        private void btnSave2_Click(object sender, EventArgs e)
        {
            this.btnSave2.Enabled = false;
            if (this.comboBox2.SelectedValue.ToString() == "7" || this.comboBox2.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("只有新单或待审核可保存");
                this.btnSave2.Enabled = true;
                return;
            }
           /* for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                if (dataGridView3.Rows[i].Cells["delivery_day"].Value == null || dataGridView3.Rows[i].Cells["delivery_day1"].Value.ToString() == "")
                {
                    MessageBox.Show("预排交期不可为空");
                    return;
                }
            }*/
            DataTable dataTable1 = dataGridView3.DataSource as DataTable;
            DataTable dataTable = new DataTable();
            dataTable = dataTable1.Clone();
            for(int i = 0; i < dataGridView3.Rows.Count; i++) 
            {
                if (dataGridView3.Rows[i].Cells["Column13"].Value != null && dataGridView3.Rows[i].Cells["Column13"].Value.ToString() == "True") 
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int j = 1; j < dataGridView3.Rows[i].Cells.Count; j++)
                    {
                        dataRow[dataGridView3.Columns[j].Name.ToString()] = dataGridView3.Rows[i].Cells[dataGridView3.Columns[j].Name.ToString()].Value==null? DBNull.Value: dataGridView3.Rows[i].Cells[dataGridView3.Columns[j].Name.ToString()].Value;
                    }
                    /*if (dataGridView3.Rows[i].Cells["delivery_day"].Value == null || dataGridView3.Rows[i].Cells["delivery_day"].Value.ToString() == "")
                    {
                        MessageBox.Show("预排交期不可为空");
                        this.btnSave2.Enabled = true;
                        return;
                    }*/
                    dataTable.Rows.Add(dataRow);
                }
            }
            List<double> lstID = (from d in dataTable.AsEnumerable() select d.Field<double>("qty")).ToList();
            bool flag = true;
            foreach (var item in lstID)
            {
                if (item != 0)
                {
                    flag = false;
                }
            }
            if (flag)
            {
                MessageBox.Show("请勿保存空数据！");
                this.btnSave2.Enabled = true;
                return;
            }

            this.comboBox2.SelectedValue = "2";
            this.button2.Text = this.comboBox2.Text;

            string rcpt_no = this.txtSendNo2.Text.ToString();
            string sampleNo = this.txtSampleNo2.Text.ToString();
            string SendUnit = this.txtSendUnit2.Text.ToString();
            string Receive_date = txtSendDate2.Value.ToString("yyyyMMdd");
            string art_no = this.txtArtNo2.Text.ToString();
            string art_name = this.txtArtName2.Text.ToString();
            string purpose = this.txtPurpose2.Text.ToString();
            string season = this.txtSeason2.Text.ToString();
            string dept = this.txtSampleDesigner2.Text.ToString();
            string typeMast = this.txtTypeMaster2.Text.ToString();
            string sampdesiger = this.txtSampleDesigner2.Text.ToString();

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("rcpt_no", rcpt_no);
            dictionary.Add("sampleNo", sampleNo);
            dictionary.Add("SendUnit", SendUnit);
            dictionary.Add("Receive_date", Receive_date);
            dictionary.Add("art_no", art_no);
            dictionary.Add("art_name", art_name);
            dictionary.Add("purpose", purpose);
            dictionary.Add("season", season);
            dictionary.Add("dept", dept);
            dictionary.Add("typeMast", typeMast);
            dictionary.Add("sampdesiger", sampdesiger);
            dictionary.Add("data", dataTable);
            dictionary.Add("type_name", "OUT");
            dictionary.Add("COL1", "N");


            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", 
                "CreateSampleRcept",Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, DataTable> dictionary1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, DataTable>>(retData);
                this.dataGridView3.DataSource = dictionary1["body"];
                DataTable dtJson = dictionary1["Heard"];
                this.txtArtNo2.Text = dtJson.Rows[0]["ART_NO"].ToString();
                this.txtArtName2.Text = dtJson.Rows[0]["ART_NAME"].ToString();
                this.txtPurpose2.Text = dtJson.Rows[0]["PURPOSE"].ToString();
                this.txtSeason2.Text = dtJson.Rows[0]["SEASON"].ToString();
                this.txtSendUnit2.Text = dtJson.Rows[0]["staff_department"].ToString();
                this.txtMatchColor2.Text = dtJson.Rows[0]["COLOR_WAY"].ToString();
                this.txtTypeMaster2.Text = dtJson.Rows[0]["TYPE_LEADER"].ToString();
                this.txtSampleDesigner2.Text = dtJson.Rows[0]["SAMPLE_LEADER"].ToString();
                this.txtSendNo2.Text = dtJson.Rows[0]["RCPT_NO"].ToString();
                this.txtCreateUser2.Text = dtJson.Rows[0]["CREATE_NAME"].ToString();
                this.txtCreateUserNo2.Text = dtJson.Rows[0]["CREATE_USER"].ToString();
                this.txtCreateDate2.Text = dtJson.Rows[0]["INSERT_DATE"].ToString();
                this.txtModifyUser2.Text = dtJson.Rows[0]["LAST_USER"].ToString();
                this.txtModifyUserNo2.Text = dtJson.Rows[0]["LAST_NAME"].ToString();
                this.txtModifyDate2.Text = dtJson.Rows[0]["LAST_DATE"].ToString();
                this.btnSave2.Enabled = true;
                MessageBox.Show("保存成功");
                dataGridView3.ReadOnly = true;
            }
            else
            {
                MessageBox.Show( Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString(),"错误");
                this.btnSave2.Enabled = true;
            }
        }

        //dataGridView转dataTable
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                if (dgv.Rows[count].Cells["qty"].Value == null|| dgv.Rows[count].Cells["qty"].Value.ToString()=="" || dgv.Rows[count].Cells["qty"].Value.ToString() == "0")
                {
                    continue;
                }
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public DataTable GetDgvToTable2(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                if (dgv.Rows[count].Cells["qty1"].Value == null || dgv.Rows[count].Cells["qty1"].Value.ToString() == "")
                {
                    continue;
                }
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void dataGridView3_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           /* List<int> indexs = new List<int>() { 1 };
            MergeCellInOneColumn(dataGridView3, indexs, e);*/
        }

        /// <summary>
        /// 合并同一列中值相同的相邻单元格
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="columnIndexList">要合并的列的索引列表</param>
        /// <param name="e">当前单元格的属性访问器</param>
        private void MergeCellInOneColumn(DataGridView dgv, List<int> columnIndexList, DataGridViewCellPaintingEventArgs e)
        {
            if (columnIndexList.Contains(e.ColumnIndex) && e.RowIndex != -1)
            {
                Brush gridBrush = new SolidBrush(dgv.GridColor);
                Brush backBrush = new SolidBrush(e.CellStyle.BackColor);
                Pen gridLinePen = new Pen(gridBrush);

                //擦除
                e.Graphics.FillRectangle(backBrush, e.CellBounds);

                //画右边线
                e.Graphics.DrawLine(gridLinePen,
                   e.CellBounds.Right - 1,
                   e.CellBounds.Top,
                   e.CellBounds.Right - 1,
                   e.CellBounds.Bottom - 1);

                //画底边线
                if ((e.RowIndex < dgv.Rows.Count - 1 && dgv.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString()) ||
                    e.RowIndex == dgv.Rows.Count - 1)
                {
                    e.Graphics.DrawLine(gridLinePen,
                        e.CellBounds.Left,
                        e.CellBounds.Bottom - 1,
                        e.CellBounds.Right - 1,
                        e.CellBounds.Bottom - 1);
                }

                //写文本
                if (e.RowIndex == 0 || dgv.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
                {
                    e.Graphics.DrawString((String)e.Value, e.CellStyle.Font,
                        Brushes.Black, e.CellBounds.X + 2,
                        e.CellBounds.Y + 5, StringFormat.GenericDefault);
                }
                e.Handled = true;
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.dataGridView3.ReadOnly) 
            {
                if (e.ColumnIndex == this.dataGridView3.Columns["delivery_day"].Index)
                {
                    _Rectangle = dataGridView3.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true); //得到所在单元格位置和大小
                    dtp.Size = new Size(_Rectangle.Width, _Rectangle.Height); //把单元格大小赋给时间控件
                    dtp.Location = new Point(_Rectangle.X, _Rectangle.Y); //把单元格位置赋给时间控件
                    dtp.Visible = true;  //显示控件
                }
                else
                    dtp.Visible = false;
            }
           
        }

        private void dataGridView3_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            dtp.Visible = false;
        }

        private void dataGridView3_Scroll(object sender, ScrollEventArgs e)
        {
            dtp.Visible = false;
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
            string issue_qty = dataGridView3.Rows[e.RowIndex].Cells["issued_quantity"].Value == null|| dataGridView3.Rows[e.RowIndex].Cells["issued_quantity"].Value.ToString() == "" ? "0" : this.dataGridView3.Rows[e.RowIndex].Cells["issued_quantity"].Value.ToString();
            string order_qty = this.dataGridView3.Rows[e.RowIndex].Cells["quantity"].Value == null ? "0" : this.dataGridView3.Rows[e.RowIndex].Cells["quantity"].Value.ToString();

            if (e.ColumnIndex == this.dataGridView3.Columns["qty"].Index)
            {
                if (this.dataGridView3.Rows[e.RowIndex].Cells["qty"].Value != null)
                {
                    try
                    {
                        decimal b_qty = Convert.ToDecimal(this.dataGridView3.Rows[e.RowIndex].Cells["qty"].Value.ToString());
                        decimal i_qty = Convert.ToDecimal(issue_qty);
                        decimal o_qty = Convert.ToDecimal(order_qty);
                        if (o_qty < (b_qty + i_qty))
                        {
                            dataGridView3.Rows[e.RowIndex].Cells["qty"].Value = 0;
                            dataGridView3.Rows[e.RowIndex].Cells["qty_l"].Value = 0;
                            dataGridView3.Rows[e.RowIndex].Cells["qty_r"].Value = 0;
                            throw new Exception("数量超出订单数");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        return;
                    }

                    this.dataGridView3.Rows[e.RowIndex].Cells["qty_l"].Value = this.dataGridView3.Rows[e.RowIndex].Cells["qty"].Value;
                    this.dataGridView3.Rows[e.RowIndex].Cells["qty_r"].Value = this.dataGridView3.Rows[e.RowIndex].Cells["qty"].Value;
                }
            }
            if (e.ColumnIndex == this.dataGridView3.Columns["qty_l"].Index|| e.ColumnIndex == this.dataGridView3.Columns["qty_r"].Index)
            {
                string l = this.dataGridView3.Rows[e.RowIndex].Cells["qty_l"].Value == null ? SetZero("l",e.RowIndex) : this.dataGridView3.Rows[e.RowIndex].Cells["qty_l"].Value.ToString();
                string r = this.dataGridView3.Rows[e.RowIndex].Cells["qty_r"].Value ==null ? SetZero("r", e.RowIndex) : this.dataGridView3.Rows[e.RowIndex].Cells["qty_r"].Value.ToString();
                decimal temp = 0;
                try
                {
                    temp = Convert.ToDecimal(l) + Convert.ToDecimal(r);
                }
                catch (Exception)
                {
                    MessageBox.Show("输入了非数字的字符，请修改", "Error");
                    return;
                    throw;
                }
                
                this.dataGridView3.Rows[e.RowIndex].Cells["qty"].Value = temp / 2;

            }
        }
        public string SetZero(string flag,int index) 
        {
            if (flag =="l")
                this.dataGridView3.Rows[index].Cells["qty_l"].Value = 0;
            if (flag == "r")
                this.dataGridView3.Rows[index].Cells["qty_r"].Value = 0;
            return "0";
        }

        public string SetZero2(string flag, int index)
        {
            if (flag == "l")
                this.dataGridView2.Rows[index].Cells["qty_l1"].Value = 0;
            if (flag == "r")
                this.dataGridView2.Rows[index].Cells["qty_r1"].Value = 0;
            return "0";
        }

        private void btnExam2_Click(object sender, EventArgs e)
        {
            this.btnExam2.Enabled = false;
            if (this.comboBox2.SelectedValue.ToString() != "2") 
            {
                MessageBox.Show("只有待审核的才可审核！", "提示");
                this.btnExam2.Enabled = true;
                return;
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("rcpt_no", this.txtSendNo2.Text.ToString().Trim());
            dictionary.Add("type_name", "OUT");

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "UpdateRece", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"])) 
            {
                this.comboBox2.SelectedValue = "7";
                this.button2.Text = this.comboBox2.Text;
                MessageBox.Show("审核成功!");

            }
            else 
            {
                MessageBox.Show(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString(),"错误");

                //SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            this.btnExam2.Enabled = true;


        }

        private void btnDelete2_Click(object sender, EventArgs e)
        {
            this.btnDelete2.Enabled = false;
            DialogResult dialogResult = MessageBox.Show("请三思，确认要删除此单据嘛？", "提示", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.Cancel)
            {
                this.btnDelete2.Enabled = true;
                return;
            }
            if (this.comboBox2.SelectedValue.ToString() == "7") 
            {
                MessageBox.Show("审核后的单子无法删除！");
                this.btnDelete2.Enabled = true;

                return;
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("rcpt_no", this.txtSendNo2.Text.ToString().Trim());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "DeteleRece", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                MessageBox.Show("success!");
                CreateReceOrder();
            }
            else
            {
                MessageBox.Show("fail!");
            }
            this.btnDelete2.Enabled = true;

        }

        //创建新单
        public void CreateReceOrder() 
        {
            this.comboBox2.SelectedValue = "1";
            this.button2.Text = this.comboBox2.Text;
            this.txtArtNo2.Text = "";
            this.txtArtName2.Text = "";
            this.txtPurpose2.Text = "";
            this.txtSeason2.Text = "";
            this.txtSendUnit2.Text = "";
            this.txtMatchColor2.Text = "";
            this.txtTypeMaster2.Text = "";
            this.txtSampleDesigner2.Text = "";
            this.txtSendNo2.Text = "";
            this.txtCreateUser2.Text = "";
            this.txtCreateUserNo2.Text = "";
            this.txtCreateDate2.Text = "";
            this.txtModifyUser2.Text = "";
            this.txtModifyUserNo2.Text = "";
            this.txtModifyDate2.Text = "";
            //this.txtSampleNo2.Text = "";
            this.dataGridView3.DataSource = null;
            SampleInfo.Clear();
        }
        public void CreateReceOrder2()
        {
            comboBox1.SelectedValue = "1";
            this.button9.Text = this.comboBox1.Text;
            this.txtArtNo3.Text = "";
            this.txtArtName3.Text = "";
            this.txtPurpose3.Text = "";
            this.txtSeason3.Text = "";
            this.txtReceiveUnit3.Text = "";
            this.txtMatchColor3.Text = "";
            this.txtTypeMaster3.Text = "";
            this.txtSampleDesigner3.Text = "";
            this.txtReceiveNo3.Text = "";
            this.txtCreateUser3.Text = "";
            this.txtCreateUserNo3.Text = "";
            this.txtCreateDate3.Text = "";
            this.txtModifyUser3.Text = "";
            this.txtModifyUserNo3.Text = "";
            this.txtModifyDate3.Text = "";
           // this.txtSampleNo3.Text = "";
            this.dataGridView2.DataSource = null;
            SampleInfo.Clear();
        }

        //保存后禁用，不允许修改
        public void DisableButton()
        {
            this.dataGridView3.ReadOnly = true;
        }

        private void btnUpdate2_Click(object sender, EventArgs e)
        {
            if(this.comboBox2.SelectedValue.ToString() =="7"|| this.comboBox2.SelectedValue.ToString() == "0") 
            {
                MessageBox.Show("只有新单或待审核可修改");
                return;
            }
            this.dataGridView3.ReadOnly = false;
            this.comboBox2.SelectedValue = "5";
            this.button2.Text = this.comboBox2.Text;
            //this.button2.BackColor = Color.Green;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string sample_no = txtSampleNo1.Text.Trim().ToString();
            string rcpt_no = txtSendReceiveNo1.Text.Trim().ToString();
            string art_no = txtArtNo1.Text.Trim().ToString();
            string create_user = txtCreateUser1.Text.Trim().ToString();
            string status = cbStatus1.SelectedValue.ToString();
            string type_name = cbSendReceiveType1.SelectedValue.ToString();
            string startDate = txtStartDate1.Value.ToString("yyyyMMdd");
            string endDate = txtEndDate1.Value.ToString("yyyyMMdd");

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sample_no", sample_no);
            dictionary.Add("rcpt_no", rcpt_no);
            dictionary.Add("art_no", art_no);
            dictionary.Add("create_user", create_user);
            dictionary.Add("status", status);
            dictionary.Add("type_name", type_name);
            dictionary.Add("startDate", startDate);
            dictionary.Add("endDate", endDate);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "QueryReceice", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retData);
                // Dictionary<string, DataTable> dictionary1 = dataTable;
                this.dataGridView1.DataSource = dataTable;
                //DataTable dtJson = dictionary1["Heard"];
                //MessageBox.Show("success!");
            }
            else
            {
                MessageBox.Show("查无数据!");
                this.dataGridView1.DataSource = null;
            }

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

            string sample_no = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Column5"].Value.ToString();
            string rcpt_no = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Column3"].Value.ToString();
            string type_name = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Column10"].Value.ToString();
            //string v = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Column11"].Value.ToString();
           
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sample_no", sample_no);
            dictionary.Add("rcpt_no", rcpt_no);
            dictionary.Add("type_name", type_name);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "DoubleClickReceive", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if(Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"])) 
            {
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, DataTable> dictionary1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,DataTable>>(retData);
                DataTable dtJson = dictionary1["Header"];
                DataTable dataTable2 = dictionary1["body"];
                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Column10"].Value.ToString() == "OUT") 
                {
                    SampleInfo = dictionary1["SampleInfo"];
                    dataGridView3.ReadOnly = true;
                    comboBox2.SelectedValue = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Column11"].Value.ToString();
                    this.button2.Text = this.comboBox2.Text;
                    dataGridView3.DataSource = null;
                    this.txtArtNo2.Text = dtJson.Rows[0]["ART_NO"].ToString();
                    this.txtSampleNo2.Text = dtJson.Rows[0]["SOURCE_NO"].ToString();
                    this.txtArtName2.Text = dtJson.Rows[0]["ART_NAME"].ToString();
                    this.txtPurpose2.Text = dtJson.Rows[0]["PURPOSE"].ToString();
                    this.txtSeason2.Text = dtJson.Rows[0]["SEASON"].ToString();
                    this.txtSendUnit2.Text = dtJson.Rows[0]["staff_department"].ToString();
                    this.txtMatchColor2.Text = dtJson.Rows[0]["COLOR_WAY"].ToString();
                    this.txtTypeMaster2.Text = dtJson.Rows[0]["TYPE_LEADER"].ToString();
                    this.txtSampleDesigner2.Text = dtJson.Rows[0]["SAMPLE_LEADER"].ToString();
                    this.txtSendNo2.Text = dtJson.Rows[0]["RCPT_NO"].ToString();
                    this.txtCreateUser2.Text = dtJson.Rows[0]["CREATE_USER"].ToString();
                    this.txtCreateUserNo2.Text = dtJson.Rows[0]["CREATE_NAME"].ToString();
                    this.txtCreateDate2.Text = dtJson.Rows[0]["INSERT_DATE"].ToString();
                    this.txtModifyUser2.Text = dtJson.Rows[0]["LAST_USER"].ToString();
                    this.txtModifyUserNo2.Text = dtJson.Rows[0]["LAST_NAME"].ToString();
                    this.txtModifyDate2.Text = dtJson.Rows[0]["LAST_DATE"].ToString();
                    //this.btnSave2.Enabled = true;
                    dataGridView3.DataSource = dataTable2;
                    tabControl1.SelectedIndex = 1;
                }
                else if(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Column10"].Value.ToString() == "IN") 
                {
                    SampleInfo2 = dictionary1["SampleInfo"];
                    dataGridView2.ReadOnly = true;
                    comboBox1.SelectedValue = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Column11"].Value.ToString();
                    this.button9.Text = this.comboBox1.Text;
                    dataGridView2.DataSource = null;
                    this.txtArtNo3.Text = dtJson.Rows[0]["ART_NO"].ToString();
                    this.txtSampleNo3.Text = dtJson.Rows[0]["SOURCE_NO"].ToString();
                    this.txtArtName3.Text = dtJson.Rows[0]["ART_NAME"].ToString();
                    this.txtPurpose3.Text = dtJson.Rows[0]["PURPOSE"].ToString();
                    this.txtSeason3.Text = dtJson.Rows[0]["SEASON"].ToString();
                    this.txtReceiveUnit3.Text = dtJson.Rows[0]["staff_department"].ToString();
                    this.txtMatchColor3.Text = dtJson.Rows[0]["COLOR_WAY"].ToString();
                    this.txtTypeMaster3.Text = dtJson.Rows[0]["TYPE_LEADER"].ToString();
                    this.txtSampleDesigner3.Text = dtJson.Rows[0]["SAMPLE_LEADER"].ToString();
                    this.txtReceiveNo3.Text = dtJson.Rows[0]["RCPT_NO"].ToString();
                    this.txtCreateUser3.Text = dtJson.Rows[0]["CREATE_USER"].ToString();
                    this.txtCreateUserNo3.Text = dtJson.Rows[0]["CREATE_NAME"].ToString();
                    this.txtCreateDate3.Text = dtJson.Rows[0]["INSERT_DATE"].ToString();
                    this.txtModifyUser3.Text = dtJson.Rows[0]["LAST_USER"].ToString();
                    this.txtModifyUserNo3.Text = dtJson.Rows[0]["LAST_NAME"].ToString();
                    this.txtModifyDate3.Text = dtJson.Rows[0]["LAST_DATE"].ToString();
                    //this.btnSave2.Enabled = true;
                    dataGridView2.DataSource = dataTable2;
                    tabControl1.SelectedIndex = 2;
                }
                
                
            }
        }

        //跳转代码
        public void ToInfo(string sample_no,string rcpt_no,string type_name) 
        {
           // string sample_no = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Column5"].Value.ToString();
           // string rcpt_no = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Column3"].Value.ToString();

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sample_no", sample_no);
            dictionary.Add("rcpt_no", rcpt_no);
            dictionary.Add("type_name", type_name);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "DoubleClickReceive", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                dataGridView3.DataSource = null;
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, DataTable> dictionary1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, DataTable>>(retData);
                DataTable dtJson = dictionary1["Header"];
                DataTable dataTable2 = dictionary1["body"];
                this.txtArtNo2.Text = dtJson.Rows[0]["ART_NO"].ToString();
                this.txtSampleNo2.Text = dtJson.Rows[0]["SOURCE_NO"].ToString();
                this.txtArtName2.Text = dtJson.Rows[0]["ART_NAME"].ToString();
                this.txtPurpose2.Text = dtJson.Rows[0]["PURPOSE"].ToString();
                this.txtSeason2.Text = dtJson.Rows[0]["SEASON"].ToString();
                this.txtSendUnit2.Text = dtJson.Rows[0]["staff_department"].ToString();
                this.txtMatchColor2.Text = dtJson.Rows[0]["COLOR_WAY"].ToString();
                this.txtTypeMaster2.Text = dtJson.Rows[0]["TYPE_LEADER"].ToString();
                this.txtSampleDesigner2.Text = dtJson.Rows[0]["SAMPLE_LEADER"].ToString();
                this.txtSendNo2.Text = dtJson.Rows[0]["RCPT_NO"].ToString();
                this.txtCreateUser2.Text = dtJson.Rows[0]["CREATE_USER"].ToString();
                this.txtCreateUserNo2.Text = dtJson.Rows[0]["CREATE_USER"].ToString();
                this.txtCreateDate2.Text = dtJson.Rows[0]["INSERT_DATE"].ToString();
                this.txtModifyUser2.Text = dtJson.Rows[0]["LAST_USER"].ToString();
                this.txtModifyUserNo2.Text = dtJson.Rows[0]["LAST_NAME"].ToString();
                this.txtModifyDate2.Text = dtJson.Rows[0]["LAST_DATE"].ToString();
                //this.btnSave2.Enabled = true;
                dataGridView3.DataSource = dataTable2;
                tabControl1.SelectedIndex = 1;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Columns["Column12"].Index == e.ColumnIndex) 
            {
               // if (dataGridView1.Rows[e.RowIndex].Cells["Column10"].Value.ToString() == "IN") 
               // {
                   /* string sample_no = dataGridView1.Rows[e.RowIndex].Cells["Column5"].Value.ToString();
                    string rcpt_no = dataGridView1.Rows[e.RowIndex].Cells["Column3"].Value.ToString();
                    string type_name = dataGridView1.Rows[e.RowIndex].Cells["Column10"].Value.ToString();
                    ToInfo(sample_no, rcpt_no, type_name);*/

                // }
                dataGridView1_DoubleClick(sender, e);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            CreateReceOrder2();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sampleId", this.txtSampleNo3.Text.ToString().Trim());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "GetSampleOrderIN", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, DataTable> dictionary1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, DataTable>>(json);
                DataTable dtJson = dictionary1["Header"];
                SampleInfo2 = dtJson;
                dataGridView2.ReadOnly = false;
                this.txtArtNo3.Text = dtJson.Rows[0]["ART_NO"].ToString();
                this.txtArtName3.Text = dtJson.Rows[0]["NAME_T"].ToString();
                this.txtPurpose3.Text = dtJson.Rows[0]["PURPOSE"].ToString();
                this.txtSeason3.Text = dtJson.Rows[0]["SEASON"].ToString();
                this.txtReceiveUnit3.Text = dtJson.Rows[0]["staff_department"].ToString();
                this.txtMatchColor3.Text = dtJson.Rows[0]["COLOR_WAY"].ToString();
                this.txtTypeMaster3.Text = dtJson.Rows[0]["TYPE_LEADER"].ToString();
                this.txtSampleDesigner3.Text = dtJson.Rows[0]["SAMPLE_LEADER"].ToString();
                this.dataGridView2.DataSource = dictionary1["body"];
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btnSave3_Click(object sender, EventArgs e)
        {

            this.btnSave3.Enabled = false;
            if (this.comboBox1.SelectedValue.ToString() == "7" || this.comboBox1.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("只有新单或待审核可保存");
                this.btnSave3.Enabled = true;
                return;
            }

           /* for(int i = 0; i<dataGridView2.Rows.Count; i++) 
            {
                if(dataGridView2.Rows[i].Cells["delivery_day1"].Value == null || dataGridView2.Rows[i].Cells["delivery_day1"].Value.ToString() == "") 
                {
                    MessageBox.Show("预排交期不可为空");
                    this.btnSave3.Enabled = true;
                    return;
                }
            }*/
            DataTable dataTable = GetDgvToTable2(this.dataGridView2);
            
            DataTable dataTable2 = new DataTable();
            dataTable2 = dataTable.Clone();
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells["Column14"].Value != null && dataGridView2.Rows[i].Cells["Column14"].Value.ToString() == "True")
                {
                    DataRow dataRow = dataTable2.NewRow();
                    for (int j = 1; j < dataGridView2.Rows[i].Cells.Count; j++)
                    {
                        dataRow[dataGridView2.Columns[j].Name.ToString()] = dataGridView2.Rows[i].Cells[dataGridView2.Columns[j].Name.ToString()].Value == null ? DBNull.Value : dataGridView2.Rows[i].Cells[dataGridView2.Columns[j].Name.ToString()].Value;
                    }

                    dataTable2.Rows.Add(dataRow);
                }
            }

            if (dataTable2.Rows.Count <= 0) 
            {
                MessageBox.Show("请勿保存空数据！");
                this.btnSave3.Enabled = true;
                return;
            }
            List<string> lstID = (from d in dataTable2.AsEnumerable() select d.Field<string>("qty1")).ToList();
            foreach (var item in lstID)
            {
                if (item == "" || item == "0")
                {
                    MessageBox.Show("请勿保存空数据！");
                    this.btnSave3.Enabled = true;
                    return;
                }
            }
            this.comboBox1.SelectedValue = "2";
            this.button9.Text = this.comboBox1.Text;

            string rcpt_no = this.txtReceiveNo3.Text.ToString();
            string sampleNo = this.txtSampleNo3.Text.ToString();
            string Receive_date = txtReceiveDate3.Value.ToString("yyyyMMdd");
            string SendUnit = this.txtReceiveUnit3.Text.ToString();
            string art_no = this.txtArtNo3.Text.ToString();
            string art_name = this.txtArtName3.Text.ToString();
            string purpose = this.txtPurpose3.Text.ToString();
            string season = this.txtSeason3.Text.ToString();
            string dept = this.txtSampleDesigner3.Text.ToString();
            string typeMast = this.txtTypeMaster3.Text.ToString();
            string sampdesiger = this.txtSampleDesigner3.Text.ToString();

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("rcpt_no", rcpt_no);
            dictionary.Add("sampleNo", sampleNo);
            dictionary.Add("Receive_date", Receive_date);
            dictionary.Add("SendUnit", SendUnit);
            dictionary.Add("art_no", art_no);
            dictionary.Add("art_name", art_name);
            dictionary.Add("purpose", purpose);
            dictionary.Add("season", season);
            dictionary.Add("dept", dept);
            dictionary.Add("typeMast", typeMast);
            dictionary.Add("sampdesiger", sampdesiger);
            dictionary.Add("data", dataTable2);
            dictionary.Add("type_name", "IN");
            dictionary.Add("COL1", "N");

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "CreateSampleRcept", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, DataTable> dictionary1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, DataTable>>(retData);
                this.dataGridView2.DataSource = dictionary1["body"];
                DataTable dtJson = dictionary1["Heard"];
                this.txtArtNo3.Text = dtJson.Rows[0]["ART_NO"].ToString();
                this.txtArtName3.Text = dtJson.Rows[0]["ART_NAME"].ToString();
                this.txtPurpose3.Text = dtJson.Rows[0]["PURPOSE"].ToString();
                this.txtSeason3.Text = dtJson.Rows[0]["SEASON"].ToString();
                this.txtReceiveUnit3.Text = dtJson.Rows[0]["staff_department"].ToString();
                this.txtMatchColor3.Text = dtJson.Rows[0]["COLOR_WAY"].ToString();
                this.txtTypeMaster3.Text = dtJson.Rows[0]["TYPE_LEADER"].ToString();
                this.txtSampleDesigner3.Text = dtJson.Rows[0]["SAMPLE_LEADER"].ToString();
                this.txtReceiveNo3.Text = dtJson.Rows[0]["RCPT_NO"].ToString();
                this.txtCreateUser3.Text = dtJson.Rows[0]["CREATE_NAME"].ToString();
                this.txtCreateUserNo3.Text = dtJson.Rows[0]["CREATE_USER"].ToString();
                this.txtCreateDate3.Text = dtJson.Rows[0]["INSERT_DATE"].ToString();
                this.txtModifyUser3.Text = dtJson.Rows[0]["LAST_USER"].ToString();
                this.txtModifyUserNo3.Text = dtJson.Rows[0]["LAST_NAME"].ToString();
                this.txtModifyDate3.Text = dtJson.Rows[0]["LAST_DATE"].ToString();
                this.btnSave3.Enabled = true;
                MessageBox.Show("保存成功");
                dataGridView2.ReadOnly = true;
            }
            else
            {
                MessageBox.Show(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString(), "错误");
                this.btnSave3.Enabled = true;
            }
        }

        private void btnDelete3_Click(object sender, EventArgs e)
        {
            this.btnDelete3.Enabled = false;
            DialogResult dialogResult = MessageBox.Show("请三思，确认要删除此单据嘛？", "提示", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.Cancel) 
            {
                this.btnDelete3.Enabled = true;
                return;
            }
            if (this.comboBox1.SelectedValue.ToString() == "7")
            {
                MessageBox.Show("审核后的单子无法删除！");
                this.btnDelete3.Enabled = true;
                return;
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("rcpt_no", this.txtReceiveNo3.Text.ToString().Trim());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "DeteleRece", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                MessageBox.Show("删除成功!");
                CreateReceOrder2();
                this.btnDelete3.Enabled = true;
            }
            else
            {
                MessageBox.Show("删除失败!");
                this.btnDelete3.Enabled = true;

            }
        }

        private void btnAdd3_Click(object sender, EventArgs e)
        {
            CreateReceOrder2();
        }

        private void btnDelayRegister1_Click(object sender, EventArgs e)
        {
            Delay_Registration delay_Registration = new Delay_Registration();
            delay_Registration.ShowDialog();
        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void txtSendDate2_CloseUp(object sender, EventArgs e)
        {
            Console.WriteLine(this.txtSendDate2.Text.ToString());
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string receive_qty = dataGridView2.Rows[e.RowIndex].Cells["RECEIVED_QUANTITY"].Value == null || dataGridView2.Rows[e.RowIndex].Cells["RECEIVED_QUANTITY"].Value.ToString() == "" ? "0" : this.dataGridView2.Rows[e.RowIndex].Cells["RECEIVED_QUANTITY"].Value.ToString();
            string order_qty = this.dataGridView2.Rows[e.RowIndex].Cells["quantity1"].Value == null ? "0" : this.dataGridView2.Rows[e.RowIndex].Cells["quantity1"].Value.ToString();

            if (e.ColumnIndex == this.dataGridView2.Columns["qty1"].Index)
            {
                if (this.dataGridView2.Rows[e.RowIndex].Cells["qty1"].Value != null)
                {
                    try
                    {
                        decimal b_qty = Convert.ToDecimal(this.dataGridView2.Rows[e.RowIndex].Cells["qty1"].Value.ToString());
                        decimal i_qty = Convert.ToDecimal(receive_qty);
                        decimal o_qty = Convert.ToDecimal(order_qty);
                        if (o_qty < (b_qty + i_qty))
                        {
                            dataGridView2.Rows[e.RowIndex].Cells["qty1"].Value = 0;
                            dataGridView2.Rows[e.RowIndex].Cells["qty_l1"].Value = 0;
                            dataGridView2.Rows[e.RowIndex].Cells["qty_r1"].Value = 0;
                            throw new Exception("数量超出订单数");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        return;
                    }

                    this.dataGridView2.Rows[e.RowIndex].Cells["qty_l1"].Value = this.dataGridView2.Rows[e.RowIndex].Cells["qty1"].Value;
                    this.dataGridView2.Rows[e.RowIndex].Cells["qty_r1"].Value = this.dataGridView2.Rows[e.RowIndex].Cells["qty1"].Value;
                }
            }
            if (e.ColumnIndex == this.dataGridView2.Columns["qty_l1"].Index || e.ColumnIndex == this.dataGridView2.Columns["qty_r1"].Index)
            {
                string l = this.dataGridView2.Rows[e.RowIndex].Cells["qty_l1"].Value == null ? SetZero2("l", e.RowIndex) : this.dataGridView2.Rows[e.RowIndex].Cells["qty_l1"].Value.ToString();
                string r = this.dataGridView2.Rows[e.RowIndex].Cells["qty_r1"].Value == null ? SetZero2("r", e.RowIndex) : this.dataGridView2.Rows[e.RowIndex].Cells["qty_r1"].Value.ToString();
                decimal temp = 0;
                try
                {
                    temp = Convert.ToDecimal(l) + Convert.ToDecimal(r);
                }
                catch (Exception)
                {
                    MessageBox.Show("输入了非数字的字符，请修改", "Error");
                    return;
                    throw;
                }

                this.dataGridView2.Rows[e.RowIndex].Cells["qty"].Value = temp / 2;

            }

        }

        private void btnExam3_Click(object sender, EventArgs e)
        {
            this.btnExam3.Enabled = false;
            if (this.comboBox1.SelectedValue.ToString() != "2")
            {
                MessageBox.Show("只有待审核的才可审核！", "提示");
                this.btnExam3.Enabled = true;
                return;
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("rcpt_no", this.txtReceiveNo3.Text.ToString().Trim());
            dictionary.Add("type_name", "IN");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "UpdateRece", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                this.comboBox1.SelectedValue = "7";
                this.button9.Text = this.comboBox1.Text;
                this.dataGridView2.ReadOnly = true;
                MessageBox.Show("审核成功");
            }
            else
            {
                MessageBox.Show(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString(), "错误");

                //SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            this.btnExam3.Enabled = true;
        }

        private void btnUpdate3_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedValue.ToString() == "7" || this.comboBox1.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("只有新单或待审核可修改");
                return;
            }
            this.dataGridView2.ReadOnly = false;
            this.comboBox1.SelectedValue = "5";
            this.button9.Text = this.comboBox1.Text;
            //this.button2.BackColor = Color.Green;
        }

        private void btnSelectDetails3_Click(object sender, EventArgs e)
        {
            string sample_no = txtSampleNo3.Text.ToString();
            string art_no = txtArtNo3.Text.ToString();
            string art_name = txtArtName3.Text.ToString();
            string purpose = txtPurpose3.Text.ToString();
            string season = txtSeason3.Text.ToString();
           
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("sample", txtSampleNo3.Text.Trim().ToString());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "QuertBtnInfo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retData);
                SampleInfo sampleInfo = new SampleInfo(dataTable, sample_no, art_no, art_name, purpose, season);
                sampleInfo.ShowDialog();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btnSelectAgain_Click(object sender, EventArgs e)
        {
            txtSampleNo1.Text = "";
            txtArtNo1.Text = "";
            txtSendReceiveNo1.Text = "";
            txtCreateUser1.Text = "";
            cbSendReceiveType1.SelectedValue = "";
            cbStatus1.SelectedValue = "";
        }

        private void btnExamMore_Click(object sender, EventArgs e)
        {
            if(cbStatus1.SelectedValue.ToString() != "2") 
            {
                MessageBox.Show("选择待审核的状态");
                return;
            }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("rcpt_no", typeof(string));
            dataTable.Columns.Add("type_name", typeof(string));
            for(int i = 0; i < dataGridView1.Rows.Count; i++) 
            {
                if (dataGridView1.Rows[i].Cells["Column1"].Value == null) 
                {
                    continue;
                }
                if(dataGridView1.Rows[i].Cells["Column1"].Value.ToString() == "True") 
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["rcpt_no"] = dataGridView1.Rows[i].Cells["Column3"].Value.ToString();
                    dataRow["type_name"] = dataGridView1.Rows[i].Cells["Column10"].Value.ToString();
                    dataTable.Rows.Add(dataRow);
                }
            }
           
            /*string rcptNo = "";
            foreach(string item in rcptList) 
            {
                rcptNo += "'" + item + "',";
            }
            rcptNo = rcptNo.Substring(0, rcptNo.Length - 1);
*/
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("rcpt_list", dataTable);
            //dictionary.Add("type_name", "IN");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "UpdateRcptList", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
               // btnSelect_Click(sender, e);
                MessageBox.Show("审核成功");
            }
            else
            {
                MessageBox.Show(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString(), "错误");
            }

        }

        private void btnLinePrint_Click(object sender, EventArgs e)
        {
            if(cbSendReceiveType1.SelectedValue.ToString() != "") 
            {
                List<string> list = new List<string>();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Column1"].Value == null)
                    {
                        continue;
                    }
                    if (dataGridView1.Rows[i].Cells["Column1"].Value.ToString() == "True")
                    {
                        //  DataRow dataRow = dataTable.NewRow();
                        list.Add(dataGridView1.Rows[i].Cells["Column3"].Value.ToString());
                       // dataRow["type_name"] = dataGridView1.Rows[i].Cells["Column10"].Value.ToString();
                      //  dataTable.Rows.Add(dataRow);
                    }
                }
                if (list.Count <= 0) 
                {
                    MessageBox.Show("未选中行");
                    return;
                }
                string rcptNo = "";
                foreach(string item in list) 
                {
                    rcptNo += "'" + item + "',";
                }
                rcptNo = rcptNo.Substring(0, rcptNo.Length - 1);
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("rcpt_list", rcptNo);
                Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
                dictionary1.Add("Parameter",txtStartDate1.Value.ToString("yyyy/MM/dd"));
                dictionary1.Add("Parameter1", txtEndDate1.Value.ToString("yyyy/MM/dd"));
                if (cbSendReceiveType1.SelectedValue.ToString() == "IN") 
                {
                    dictionary1.Add("Parameter3", "工艺委外收料单");
                    dictionary1.Add("Parameter4", "收料数量");
                    dictionary1.Add("Parameter5", "收料日期");
                }else if(cbSendReceiveType1.SelectedValue.ToString() == "OUT")
                {
                    dictionary1.Add("Parameter3", "工艺委外发料单");
                    dictionary1.Add("Parameter4", "发料数量");
                    dictionary1.Add("Parameter5", "发料日期");
                }
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "QueryPrint", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    Dictionary<string, string> dictionary2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(retData);
                    //this.dataGridView2.DataSource = dictionary2["body"];
                    dictionary1.Add("Parameter2", dictionary2["operate_staff"].ToString());
                    DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dictionary2["body"]);
                    
                    string path = Application.StartupPath + @"\报表" + "\\样品室收发料.frx";
                    SamplePrint samplePrint = new SamplePrint(dataTable, path,dictionary1);
                    samplePrint.Show();
                }
            }
            else 
            {
                MessageBox.Show("请选择收发类型", "提示");
            }
        }

        private void cbSendReceiveType1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnSelect_Click(sender, e);
        }

        private void btnBadRegister2_Click(object sender, EventArgs e)
        {
            string sample_no = txtSampleNo3.Text.Trim().ToString();
            string art_no = txtArtNo3.Text.Trim().ToString();
            string art_name = txtArtName3.Text.Trim().ToString();
            string purpose = txtPurpose3.Text.Trim().ToString();
            string season = txtSeason3.Text.Trim().ToString();
            string colorWay = txtMatchColor3.Text.Trim().ToString();
            string MODEL_MASTER = txtTypeMaster3.Text.Trim().ToString();
            string PATTERN_MASTER = txtSampleDesigner3.Text.Trim().ToString();
            Bad_Registration bad_Registration = new Bad_Registration(sample_no,art_no,art_name,purpose,season,colorWay,MODEL_MASTER,PATTERN_MASTER);
            bad_Registration.ShowDialog();
        }

        private void btnDelayRegister2_Click(object sender, EventArgs e)
        {
            string sample_no = txtSampleNo3.Text.Trim().ToString();
            string art_no = txtArtNo3.Text.Trim().ToString();
            string art_name = txtArtName3.Text.Trim().ToString();
            string purpose = txtPurpose3.Text.Trim().ToString();
            string season = txtSeason3.Text.Trim().ToString();
            string colorWay = txtMatchColor3.Text.Trim().ToString();
            string MODEL_MASTER = txtTypeMaster3.Text.Trim().ToString();
            string PATTERN_MASTER = txtSampleDesigner3.Text.Trim().ToString();
            
            Delay_Registration delay_Registration = new Delay_Registration(sample_no, art_no, art_name, purpose, season, colorWay, MODEL_MASTER, PATTERN_MASTER);
            delay_Registration.ShowDialog();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.dataGridView2.ReadOnly)
            {
                if (e.ColumnIndex == this.dataGridView2.Columns["delivery_day1"].Index)
                {
                    _Rectangle = dataGridView2.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true); //得到所在单元格位置和大小
                    dtp2.Size = new Size(_Rectangle.Width, _Rectangle.Height); //把单元格大小赋给时间控件
                    dtp2.Location = new Point(_Rectangle.X, _Rectangle.Y); //把单元格位置赋给时间控件
                    dtp2.Visible = true;  //显示控件
                }
                else
                    dtp2.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells["Column14"].Value == null || dataGridView2.Rows[i].Cells["Column14"].Value.ToString() == "false") 
                {
                    dataGridView2.Rows[i].Cells["Column14"].Value = "True";
                }
                else 
                {
                    dataGridView2.Rows[i].Cells["Column14"].Value = "false";

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                if (dataGridView3.Rows[i].Cells["Column13"].Value == null || dataGridView3.Rows[i].Cells["Column13"].Value.ToString() == "false")
                {
                    dataGridView3.Rows[i].Cells["Column13"].Value = "True";
                }
                else
                {
                    dataGridView3.Rows[i].Cells["Column13"].Value = "false";

                }
            }


           
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.ReadOnly == false)
            {
                
                if (dataGridView3.Columns[e.ColumnIndex].HeaderText == "收料单位")
                {
                    ResponsibleUnit responsibleUnit = new ResponsibleUnit();
                    responsibleUnit.DataChange += new ResponsibleUnit.DataChangeHandler(DataChanged_ResUnit);
                    responsibleUnit.ShowDialog();
                }
            }
        }

        public void DataChanged_ResUnit(object sender, ResponsibleUnit.DataChangeEventArgs args)
        {
            dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["suppliers_code"].Value = args.result1;
            dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["suppliers_name"].Value = args.result2;
        }
        public void DataChanged_ResUnit2(object sender, ResponsibleUnit.DataChangeEventArgs args)
        {
            dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["suppliers_code1"].Value = args.result1;
            dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["suppliers_name1"].Value = args.result2;
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.ReadOnly == false)
            {
                /*if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "不良原因")
                {
                    BadReason badReason = new BadReason();
                    badReason.DataChange += new BadReason.DataChangeHandler(DataChanged_BadReason);
                    badReason.ShowDialog();
                }*/
                if (dataGridView2.Columns[e.ColumnIndex].HeaderText == "发料单位")
                {
                    ResponsibleUnit responsibleUnit = new ResponsibleUnit();
                    responsibleUnit.DataChange += new ResponsibleUnit.DataChangeHandler(DataChanged_ResUnit2);
                    responsibleUnit.ShowDialog();
                }
            }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            decimal pp = 0;
            try
            {
                foreach (DataGridViewCell dgvCell in dataGridView3.SelectedCells)
                {
                    /*if (dgvCell.ColumnIndex == 1 || dgvCell.ColumnIndex == 2 || dgvCell.ColumnIndex == 3 || dgvCell.ColumnIndex == 4 || dgvCell.ColumnIndex == 5 || dgvCell.ColumnIndex == 0)
                    {
                        continue;
                    }*/

                    if (dgvCell.Value == null)
                    {
                        break;
                    }
                    if (IsNumeric(dgvCell.Value.ToString()))
                    {
                        pp += Convert.ToDecimal(dgvCell.Value.ToString());

                    }
                    else
                    {
                        continue;
                    }

                }

                this.textBox1.Text = pp.ToString();

            }
            catch (Exception ex)
            {

            }
        }
        private bool IsNumeric(string number)
        {
            try
            {
                decimal.Parse(number);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            decimal pp = 0;
            try
            {
                foreach (DataGridViewCell dgvCell in dataGridView2.SelectedCells)
                {
                    /*if (dgvCell.ColumnIndex == 1 || dgvCell.ColumnIndex == 2 || dgvCell.ColumnIndex == 3 || dgvCell.ColumnIndex == 4 || dgvCell.ColumnIndex == 5 || dgvCell.ColumnIndex == 0)
                    {
                        continue;
                    }*/

                    if (dgvCell.Value == null)
                    {
                        break;
                    }
                    if (IsNumeric(dgvCell.Value.ToString()))
                    {
                        pp += Convert.ToDecimal(dgvCell.Value.ToString());

                    }
                    else
                    {
                        continue;
                    }

                }

                this.textBox2.Text = pp.ToString();

            }
            catch (Exception ex)
            {

            }
        }
    }


    public class ComboboxEntry
    {
        public string ENUM_CODE { get; set; }
        public string ENUM_VALUE { get; set; }
    }
}
