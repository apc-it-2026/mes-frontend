using AutocompleteMenuNS;
using MaterialSkin.Controls;
using NewExportExcels;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SizeLabelEntry
{
    public partial class SizeLabelEntry : MaterialForm
    {
        public Boolean isTitle = false;
        IList<object[]> data = null;
        private ExcelProcessor _currentExcelProcessor = null;
        public SizeLabelEntry()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetLineWiseSizeLabelData();
        }
        public void GetLineWiseSizeLabelData()
        {

            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Fromdate", fromdate.Text);
            Data.Add("Todate", todate.Text);
            Data.Add("ProdLine", txtprodline.Text);
            Cursor.Current = Cursors.WaitCursor;
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.SizeLabelServer",
                "GetLineWiseSizeLabelData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson1.Rows.Count > 0)
                {
                    dataGridView3.Rows.Clear(); 
                    int i = 0;  
                        foreach (DataRow dr in dtJson1.Rows)
                        {
                            dataGridView3.Rows.Add();
                            DataGridViewRow dgvr = dataGridView3.Rows[i];

                            dgvr.Cells["PROD_DATE"].Value = dr["PROD_DATE"].ToString();
                            dgvr.Cells["PROD_LINE"].Value = dr["PROD_LINE"].ToString();
                            dgvr.Cells["PO_NUM"].Value = dr["PO_NUM"].ToString();
                            dgvr.Cells["PO_QTY_PAIRS"].Value = dr["PO_QTY_PAIRS"].ToString();
                            dgvr.Cells["PO_QTY_PCS"].Value = dr["PO_QTY_PCS"].ToString();
                            dgvr.Cells["SIZE_LABEL_QTY"].Value = dr["SIZE_LABEL_QTY"].ToString();
                            dgvr.Cells["SIZE_LABEL_PERCENT"].Value = dr["SIZE_LABEL_PERCENT"].ToString();
                            dgvr.Cells["DISCIPLINE_ACTION"].Value = dr["DISCIPLINE_ACTION"].ToString();
                            dgvr.Cells["REASON"].Value = dr["REASON"].ToString();
                            dgvr.Cells["ASST_BARCODE"].Value = dr["ASST_BARCODE"].ToString();
                            dgvr.Cells["ASST_NAME"].Value = dr["ASST_NAME"].ToString();
                            dgvr.Cells["ASST_WARNINGS"].Value = dr["ASST_WARNINGS"].ToString();
                            dgvr.Cells["SUP_BARCODE"].Value = dr["SUP_BARCODE"].ToString();
                            dgvr.Cells["SUP_NAME"].Value = dr["SUP_NAME"].ToString();
                            dgvr.Cells["SUP_WARNINGS"].Value = dr["SUP_WARNINGS"].ToString();
                            dgvr.Cells["SH_BARCODE"].Value = dr["SH_BARCODE"].ToString();
                            dgvr.Cells["SH_NAME"].Value = dr["SH_NAME"].ToString();
                            dgvr.Cells["SH_WARNINGS"].Value = dr["SH_WARNINGS"].ToString();
                            dgvr.Cells["INCHARGE_BARCODE"].Value = dr["INCHARGE_BARCODE"].ToString();
                            dgvr.Cells["INCHARGE_NAME"].Value = dr["INCHARGE_NAME"].ToString();
                            dgvr.Cells["INCHARGE_WARNINGS"].Value = dr["INCHARGE_WARNINGS"].ToString();
                            dgvr.Cells["CREATED_BY"].Value = dr["CREATED_BY"].ToString();
                            dgvr.Cells["CREATED_AT"].Value = dr["CREATED_AT"].ToString();
                            dgvr.Cells["MODIFIED_BY"].Value = dr["MODIFIED_BY"].ToString();
                            dgvr.Cells["MODIFIED_AT"].Value = dr["MODIFIED_AT"].ToString();
                            dgvr.Cells["LOCK_STATUS"].Value = dr["LOCK_STATUS"].ToString();
                            dgvr.Cells["UPDATE_REASON"].Value = dr["UPDATE_REASON"].ToString();
                        dataGridView3.Rows[i].ReadOnly = true;
                        i++;
                        }
                    

                }
                else
                {
                    dataGridView3.Rows.Clear();
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                }
            }

        }

        private void SizeLabelEntry_Load(object sender, EventArgs e)
        {
            LoadSeDept();
            Get_PO_List();
            btnupdate.Visible = false;
            btncancel.Visible = false;
            label22 .Visible = false;
            txtupdate_reason.Visible = false;
            proddate.MinDate = DateTime.Today.AddDays(-4);
            proddate.MaxDate = DateTime.Today.AddDays(0);
        }
        private void LoadSeDept()
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new System.Drawing.Size(350, 350);
            var columnWidth = new int[] { 50, 250 };
            DataTable dt = GetAllDepts();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"].ToString() + " " + dt.Rows[i]["DEPARTMENT_NAME"].ToString() }, dt.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }

        public void Get_PO_List()
        {
            try
            {

                //textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                Dictionary<string, object> p = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.client.APIURL,
                                           "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.SizeLabelServer",
                                            "Get_PO_List",
                                            Program.client.UserToken,
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    autocompleteMenu1.MaximumSize = new Size(250, 350);
                    var columnWidth = new[] { 50, 200 };
                    int n = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["MER_PO"].ToString() }, dt.Rows[i]["MER_PO"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.client, Program.client.WebServiceUrl, Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        private DataTable GetAllDepts()
        {
            DataTable dt = new DataTable();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetAllDepts", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00003", Program.client, "", Program.client.Language);
                string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg + exceptionMsg);
            }
            return dt;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            //{
            //    e.Handled = true;
            //}
            //if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            //{
            //    e.Handled = true;
            //}
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void clear()
        {
            proddate.Text = string.Empty;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            txtpopairs.Text = string.Empty;
            txtpopcs.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox10.Text = string.Empty;
            textBox12.Text = string.Empty;
            textBox13.Text = string.Empty;
            textBox14.Text = string.Empty;
            textBox15.Text = string.Empty;
            textBox16.Text = string.Empty;
            textBox17.Text = string.Empty;
            textBox19.Text = string.Empty;
            textBox18.Text = string.Empty;
            textBox11.Text = string.Empty;
            cbreason.Text = string.Empty;
            txtupdate_reason.Text = string.Empty;
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text)||string.IsNullOrEmpty(textBox2.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Production Line and PO Can't be Empty");
                return;
            }
            if (string.IsNullOrEmpty(txtpopcs.Text) || string.IsNullOrEmpty(textBox6.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "PO Count and Size label Percent Can't be Empty");
                return;
            }
            if (string.IsNullOrEmpty(cbreason.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Missing Reason Can't be Empty");
                return;
            }
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("ProdDate", proddate.Text);
            Data.Add("ProdLine", textBox1.Text);
            Data.Add("PoNum", textBox2.Text);
            Data.Add("PoQtyPairs", txtpopairs.Text);
            Data.Add("PoQtyPcs", txtpopcs.Text);
            Data.Add("SizelabelQty", textBox5.Text);
            Data.Add("SizelabelPercent", textBox6.Text);
            Data.Add("DisciplineAction", textBox7.Text);
            Data.Add("Reason", cbreason.Text);
            Data.Add("AsstBarcode", textBox8.Text);
            Data.Add("AsstName", textBox9.Text);
            Data.Add("AsstWarnings", textBox10.Text);
            Data.Add("SupBarcode", textBox12.Text);
            Data.Add("SupName", textBox13.Text);
            Data.Add("SupWarnings", textBox14.Text);
            Data.Add("SHBarcode", textBox15.Text);
            Data.Add("SHName", textBox16.Text);
            Data.Add("SHWarnings", textBox17.Text);
            Data.Add("InchargeBarcode", textBox19.Text);
            Data.Add("Inchargename", textBox18.Text);
            Data.Add("InchargeWarnings", textBox11.Text);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.SizeLabelServer",
                "SaveSizelabelData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Added Successfully");
                clear();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                clear();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetPOQuantity();
            }
        }
        public void GetPOQuantity()
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("PONum", textBox2.Text);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.SizeLabelServer",
                "GetPOQuantity", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson1.Rows.Count > 0)
                {
                    txtpopairs.Text = dtJson1.Rows[0]["PO_PAIRS"].ToString();
                    txtpopcs.Text = dtJson1.Rows[0]["PO_PCS"].ToString();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                    textBox2.Text = "";
                    txtpopairs.Text = "";
                    txtpopcs.Text = "";
                }
            }

        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = GetEmpName(textBox8.Text);
                if (dt.Rows.Count > 0)
                {
                    textBox9.Text = dt.Rows[0]["EMP_NAME"].ToString();
                }
                else
                {
                    textBox9.Text = "";
                }
            }
            else
            {
                textBox9.Text = "";
            }
        }
        public DataTable GetEmpName(string empmo)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("EmpNo", empmo);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.SizeLabelServer",
                "GetEmpName", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            }
            return dt;
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = GetEmpName(textBox12.Text);
                if (dt.Rows.Count > 0)
                {
                    textBox13.Text = dt.Rows[0]["EMP_NAME"].ToString();
                }
                else
                {
                    textBox13.Text = "";
                }
            }
            else
            {
                textBox13.Text = "";
            }
        }

        private void textBox15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = GetEmpName(textBox15.Text);
                if (dt.Rows.Count > 0)
                {
                    textBox16.Text = dt.Rows[0]["EMP_NAME"].ToString();
                }
                else
                {
                    textBox16.Text = "";
                }
            }
            else
            {
                textBox16.Text = "";
            }
        }

        private void textBox19_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = GetEmpName(textBox19.Text);
                if (dt.Rows.Count > 0)
                {
                    textBox18.Text = dt.Rows[0]["EMP_NAME"].ToString();
                }
                else
                {
                    textBox18.Text = "";
                }
            }
            else
            {
                textBox18.Text = "";
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                if (dataGridView3.Columns[e.ColumnIndex].Name == "delete")
                {
                    if(dataGridView3.Rows[e.RowIndex].Cells["LOCK_STATUS"].Value.ToString()=="Locked")
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Data Locked");
                        return;
                    }
                    DialogResult result = MessageBox.Show($@"Are you sure you want to delete the data?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {

                        try
                        {
                            string ProdDate = dataGridView3.Rows[e.RowIndex].Cells["PROD_DATE"].Value.ToString();
                            string ProdLine = dataGridView3.Rows[e.RowIndex].Cells["PROD_LINE"].Value.ToString();
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("ProdDate", ProdDate);
                            p.Add("ProdLine", ProdLine);
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.SizeLabelServer",
                    "DeleteSizelabelData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Deleted Successfully");
                                GetLineWiseSizeLabelData();
                            }
                            else
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to delete the data");
                                GetLineWiseSizeLabelData();
                            }
                        }
                        catch (Exception ex)
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to delete the data");
                        }
                    }
                }
                else if (dataGridView3.Columns[e.ColumnIndex].Name == "edit")
                {
                    if (dataGridView3.Rows[e.RowIndex].Cells["LOCK_STATUS"].Value.ToString() == "Locked")
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Data Locked");
                        return;
                    }
                    tabControl1.SelectedIndex = 0;
                    proddate.MinDate = DateTime.Today.AddDays(-90);
                    proddate.Text = dataGridView3.Rows[e.RowIndex].Cells["PROD_DATE"].Value.ToString();
                    textBox1.Text = dataGridView3.Rows[e.RowIndex].Cells["PROD_LINE"].Value.ToString();
                    textBox2.Text = dataGridView3.Rows[e.RowIndex].Cells["PO_NUM"].Value.ToString();
                    txtpopairs.Text = dataGridView3.Rows[e.RowIndex].Cells["PO_QTY_PAIRS"].Value.ToString();
                    txtpopcs.Text = dataGridView3.Rows[e.RowIndex].Cells["PO_QTY_PCS"].Value.ToString();
                    textBox5.Text = dataGridView3.Rows[e.RowIndex].Cells["SIZE_LABEL_QTY"].Value.ToString();
                    textBox6.Text = dataGridView3.Rows[e.RowIndex].Cells["SIZE_LABEL_PERCENT"].Value.ToString();
                    textBox7.Text = dataGridView3.Rows[e.RowIndex].Cells["DISCIPLINE_ACTION"].Value.ToString();
                    cbreason.Text = dataGridView3.Rows[e.RowIndex].Cells["REASON"].Value.ToString();
                    textBox8.Text = dataGridView3.Rows[e.RowIndex].Cells["ASST_BARCODE"].Value.ToString();
                    textBox9.Text = dataGridView3.Rows[e.RowIndex].Cells["ASST_NAME"].Value.ToString();
                    textBox10.Text = dataGridView3.Rows[e.RowIndex].Cells["ASST_WARNINGS"].Value.ToString();
                    textBox12.Text = dataGridView3.Rows[e.RowIndex].Cells["SUP_BARCODE"].Value.ToString();
                    textBox13.Text = dataGridView3.Rows[e.RowIndex].Cells["SUP_NAME"].Value.ToString();
                    textBox14.Text = dataGridView3.Rows[e.RowIndex].Cells["SUP_WARNINGS"].Value.ToString();
                    textBox15.Text = dataGridView3.Rows[e.RowIndex].Cells["SH_BARCODE"].Value.ToString();
                    textBox16.Text = dataGridView3.Rows[e.RowIndex].Cells["SH_NAME"].Value.ToString();
                    textBox17.Text = dataGridView3.Rows[e.RowIndex].Cells["SH_WARNINGS"].Value.ToString();
                    textBox19.Text = dataGridView3.Rows[e.RowIndex].Cells["INCHARGE_BARCODE"].Value.ToString();
                    textBox18.Text = dataGridView3.Rows[e.RowIndex].Cells["INCHARGE_NAME"].Value.ToString();
                    textBox11.Text = dataGridView3.Rows[e.RowIndex].Cells["INCHARGE_WARNINGS"].Value.ToString();
                    txtupdate_reason.Text = dataGridView3.Rows[e.RowIndex].Cells["UPDATE_REASON"].Value.ToString();

                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                    proddate.Enabled = false;   
                    btn_clear.Visible = false;
                    btn_submit.Visible = false;
                    btnupdate.Visible = true;
                    btncancel.Visible = true;
                    label22.Visible = true;
                    txtupdate_reason.Visible = true;

                }
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Production Line and PO Can't be Empty");
                return;
            }
            if (string.IsNullOrEmpty(txtpopcs.Text) || string.IsNullOrEmpty(textBox6.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "PO Count and Size label Percent Can't be Empty");
                return;
            }
            if (string.IsNullOrEmpty(cbreason.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Missing Reason Can't be Empty");
                return;
            }
            if (string.IsNullOrEmpty(txtupdate_reason.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Update Reason Can't be Empty");
                return;
            }
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("ProdDate", proddate.Text);
            Data.Add("ProdLine", textBox1.Text);
            Data.Add("PoNum", textBox2.Text);
            Data.Add("PoQtyPairs", txtpopairs.Text);
            Data.Add("PoQtyPcs", txtpopcs.Text);
            Data.Add("SizelabelQty", textBox5.Text);
            Data.Add("SizelabelPercent", textBox6.Text);
            Data.Add("DisciplineAction", textBox7.Text);
            Data.Add("Reason", cbreason.Text);
            Data.Add("UpdateReason", txtupdate_reason.Text);
            Data.Add("AsstBarcode", textBox8.Text);
            Data.Add("AsstName", textBox9.Text);
            Data.Add("AsstWarnings", textBox10.Text);
            Data.Add("SupBarcode", textBox12.Text);
            Data.Add("SupName", textBox13.Text);
            Data.Add("SupWarnings", textBox14.Text);
            Data.Add("SHBarcode", textBox15.Text);
            Data.Add("SHName", textBox16.Text);
            Data.Add("SHWarnings", textBox17.Text);
            Data.Add("InchargeBarcode", textBox19.Text);
            Data.Add("Inchargename", textBox18.Text);
            Data.Add("InchargeWarnings", textBox11.Text);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.SizeLabelServer",
                "UpdateSizelabelData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Updated Successfully");
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to Update");
            }
            clear();
            proddate.MinDate = DateTime.Today.AddDays(-4);
            tabControl1.SelectedIndex = 1;
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            proddate.Enabled = true;
            btn_clear.Visible = true;
            btn_submit.Visible = true;
            btnupdate.Visible = false;
            btncancel.Visible = false;
            label22.Visible = false;
            txtupdate_reason.Visible = false;
            GetLineWiseSizeLabelData();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            clear();
            proddate.MinDate = DateTime.Today.AddDays(-4);
            tabControl1.SelectedIndex = 1;
            proddate.Enabled = true;
            label22.Visible = false;
            txtupdate_reason.Visible = false;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("POQty", txtpopairs.Text);
            Data.Add("SizelabelQty", textBox5.Text);
            Data.Add("SizelabelPercent", textBox6.Text);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.SizeLabelServer",
               "GetDesciplineAction", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if(ret.IsSuccess)
            {
                textBox7.Text = ret.RetData;
            }
            else
            {
                textBox7.Text = "";
            }



        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtpopcs.Text) || (Convert.ToInt32(txtpopcs.Text) == 0))
                {
                    textBox5.Text = "";
                    textBox6.Text = "";
                }
                else if(string.IsNullOrEmpty(txtpopcs.Text) || (Convert.ToInt32(txtpopcs.Text)< Convert.ToInt32(textBox5.Text)))
                {
                    textBox5.Text = "";
                    textBox6.Text = "";
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Size label Qty not more than PO Qty");
                }
                else if (!string.IsNullOrEmpty(textBox5.Text))
                {
                    textBox6.Text = Math.Round((Convert.ToDouble(textBox5.Text) / Convert.ToDouble(txtpopcs.Text) * 100), 2).ToString();
                }
                else
                {
                    textBox6.Text = "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "Size_label_Data.xls";
                ExportExcels.Export(a, dataGridView3);
            }
        }
    }
    
}
