using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MaterialSkin.Controls.MaterialForm;

namespace KPIINPUT
{
    public partial class Repair : MaterialForm
    {
        
        public Repair()
        {
            InitializeComponent();
        }
        public void clear()
        {
            PRODLINE.Text = "";
            TOTALRECEIVED.Text = "";
            TOTALREPAIRED.Text = "";
            REMAININGQTY.Text = "";
            txtRepairReason.Text = "";
            txtupdate.Text = "";
            REPAIRREASON.SelectedIndex = -1;
            txtRepairReason.Visible = false;
            label5.Visible = false;
        }
        public void LoadQueryItem()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("REPAIRNAME", REPAIRREASON.Text);
           
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.RepairsDataserver",
                 "SelectRepairData", Program.client.UserToken, JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    
                    REASON.Items.Add(dtJson.Rows[i]["REPAIRNAME"].ToString());
                    REPAIRREASON.Items.Add(dtJson.Rows[i]["REPAIRNAME"].ToString());
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
       
        private void TOTALRECEIVED_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void TOTALREPAIRED_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        
        private void Repair_Load(object sender, EventArgs e)
        {
            LoadQueryItem();
            LoadProd_Line();
            btn_update.Visible = false;
            btn_cancel.Visible = false;
            txtupdate.Visible = false;
            label12.Visible = false;
            REPAIRDATE.MinDate = DateTime.Today.AddDays(-4);
            REPAIRDATE.MaxDate = DateTime.Today.AddDays(0);

        }

        public void LoadProd_Line()
        {
            PRODLINE.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            PRODLINE.AutoCompleteSource = AutoCompleteSource.CustomSource;
            LINE.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            LINE.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> p = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.RepairsDataserver", "GetMESDepts", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    autocompleteMenu1.MaximumSize = new Size(250, 350);
                    var columnWidth = new[] { 50, 200 };

                    int n = 1;
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dtJson.Rows[i]["DEPARTMENT_CODE"].ToString() }, dtJson.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }
                }
            }
        }

        private void REPAIRREASON_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            if (REPAIRREASON.Text.Trim().ToLower() == "others")
            {
                if (txtRepairReason == null) 
                {
                    txtRepairReason = new TextBox();
                    txtRepairReason.Location = new Point(507, 75);
                    txtRepairReason.Size = new Size(264, 50);
                    retData.Add("REPAIRREASON", txtRepairReason.Text);
                }
                txtRepairReason.Visible = true;
                label5.Visible = true;
                txtRepairReason.Text = "";
                retData.Add("REPAIRREASON", txtRepairReason.Text);


            }
            else
            {
                if (txtRepairReason != null)
                {
                    txtRepairReason.Visible = false;  
                    label5.Visible = false;
                }
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            GetRepairsData();
        }

        private void GetRepairsData()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("FROMDATE", FROMDATE.Text);
            retData.Add("TODATE", TODATE.Text);
            retData.Add("PRODLINE", LINE.Text);
            retData.Add("REPAIRREASON", REASON.Text);

            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.RepairsDataserver",
                 "ViewRepairData",
Program.client.UserToken,
Newtonsoft.Json.JsonConvert.SerializeObject(retData)
);

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    dataGridView2.DataSource = dtJson;
                    REASON.SelectedIndex = -1;
                }

            }
            else
            {
                dataGridView2.IsEmpty();
                dataGridView2.DataSource = null;
                REASON.SelectedIndex = -1;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");

            }
        }

        private void TOTALREPAIRED_TextChanged(object sender, EventArgs e)
        {
            

                double value11 = double.TryParse(TOTALRECEIVED.Text, out var result11) ? result11 : 0;
                double value12 = double.TryParse(TOTALREPAIRED.Text, out var result12) ? result12 : 0;
                REMAININGQTY.Text = (value11 - value12).ToString();
            
            
            if(value12>value11)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Total Repaired Is More Than The Total Received ");
                if (TOTALREPAIRED.Text.Length > 0)
                {
                    TOTALREPAIRED.Text = TOTALREPAIRED.Text.Substring(0, TOTALREPAIRED.Text.Length - 1);
                    TOTALREPAIRED.SelectionStart = TOTALREPAIRED.Text.Length;

                }
                
                return;
            }




        }

        private void TOTALRECEIVED_TextChanged(object sender, EventArgs e)
        {
            if (TOTALRECEIVED.Text=="")
            {
                REMAININGQTY.Text = "";
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PRODLINE.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Production Line");
                return;
            }
            if (string.IsNullOrEmpty(TOTALRECEIVED.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Total Received");
                return;
            }
            if (string.IsNullOrEmpty(TOTALREPAIRED.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Total Repaired");
                return;
            }
            if (string.IsNullOrEmpty(REPAIRREASON.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Repair Reason");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;

            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("REPAIRDATE", REPAIRDATE.Text);
            retData.Add("PRODLINE", PRODLINE.Text);
            retData.Add("TOTALRECEIVED", TOTALRECEIVED.Text);
            retData.Add("TOTALREPAIRED", TOTALREPAIRED.Text);
            retData.Add("REMAININGQTY", REMAININGQTY.Text);
            retData.Add("UPDATEREASON", txtupdate.Text);
            if (REPAIRREASON.Text.Trim().ToLower() != "others")
            {
                retData.Add("REPAIRREASON", REPAIRREASON.Text);
            }
            else
            {
                if (REPAIRREASON.Text.Trim().ToLower() == "others")
                {
                    if (txtRepairReason == null)
                    {
                        txtRepairReason = new TextBox();
                        txtRepairReason.Location = new Point(507, 75);
                        txtRepairReason.Size = new Size(264, 50); 
                        retData.Add("REPAIRREASON", txtRepairReason.Text);
                    }
                    label5.Visible = true;
                    txtRepairReason.Visible = true;
                    retData.Add("REPAIRREASON", txtRepairReason.Text);
                    if (string.IsNullOrEmpty(txtRepairReason.Text))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Other Repair Reason");
                        return;

                    }
                }
                else
                {
                    if (txtRepairReason != null)
                    {
                        txtRepairReason.Visible = false;
                        label5.Visible = false;
                    }
                }

            }
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.RepairsDataserver",
                 "SendBGradeRepairData",
Program.client.UserToken,
Newtonsoft.Json.JsonConvert.SerializeObject(retData)
);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Submitted Successfully");
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
            }
            clear();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrEmpty(TOTALRECEIVED.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Total Received");
                return;
            }
            if (string.IsNullOrEmpty(TOTALREPAIRED.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Total Repaired");
                return;
            }
            if (string.IsNullOrEmpty(REPAIRREASON.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Repair Reason");
                return;
            }
            if (string.IsNullOrEmpty(txtupdate.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter Update Reason");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;

            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("REPAIRDATE", REPAIRDATE.Text);
            retData.Add("PRODLINE", PRODLINE.Text);
            retData.Add("TOTALRECEIVED", TOTALRECEIVED.Text);
            retData.Add("TOTALREPAIRED", TOTALREPAIRED.Text);
            retData.Add("REMAININGQTY", REMAININGQTY.Text);
            retData.Add("UPDATEREASON", txtupdate.Text);
            if (REPAIRREASON.Text.Trim().ToLower() != "others")
            {
                retData.Add("REPAIRREASON", REPAIRREASON.Text);
            }
            else
            {
                if (REPAIRREASON.Text.Trim().ToLower() == "others")
                {
                    if (txtRepairReason == null)
                    {
                        txtRepairReason = new TextBox();
                        txtRepairReason.Location = new Point(507, 75);
                        txtRepairReason.Size = new Size(264, 50);  
                        retData.Add("REPAIRREASON", txtRepairReason.Text);
                    }
                    label5.Visible = true;
                    txtRepairReason.Visible = true;
                    retData.Add("REPAIRREASON", txtRepairReason.Text);
                    if (string.IsNullOrEmpty(txtRepairReason.Text))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Other Repair Reason");
                        return;
                    }
                }
                else
                {
                    if (txtRepairReason != null)
                    {
                        txtRepairReason.Visible = false;  
                        label5.Visible = false;
                    }
                }
            }
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.RepairsDataserver",
                 "UpdateBGradeRepairData",
Program.client.UserToken,
Newtonsoft.Json.JsonConvert.SerializeObject(retData)
);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Updated Successfully");
                GetRepairsData();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                GetRepairsData();
            }
            clear();
            tabControl1.SelectedIndex = 1;
            btn_update.Visible = false;
            btn_cancel.Visible = false;
            txtupdate.Visible = false;
            label12.Visible = false;
            btn_clear.Visible = true;
            btn_submit.Visible = true;
            REPAIRREASON.Enabled = true;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                if (dataGridView2.Columns[e.ColumnIndex].Name == "DELETE")
                {
                    if (dataGridView2.Rows[e.RowIndex].Cells["LOCK_STATUS"].Value.ToString() == "Locked")
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Data Locked");
                        return;
                    }
                    DialogResult result = MessageBox.Show($@"Are you sure you want to delete the data?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {

                        try
                        {
                            string RepairDate = dataGridView2.Rows[e.RowIndex].Cells["REPAIR_DATE"].Value.ToString();
                            string ProdLine = dataGridView2.Rows[e.RowIndex].Cells["PROD_LINE"].Value.ToString();
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("ProdDate", RepairDate);
                            p.Add("ProdLine", ProdLine);
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.RepairsDataserver",
                    "DeleteRepairData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Deleted Successfully");
                                GetRepairsData();
                            }
                            else
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to delete the data");
                                GetRepairsData();
                            }
                        }
                        catch (Exception ex)
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to delete the data");
                        }
                    }
                }
                else if (dataGridView2.Columns[e.ColumnIndex].Name == "EDIT")
                {
                    int index = 0;
                    if (dataGridView2.Rows[e.RowIndex].Cells["LOCK_STATUS"].Value.ToString() == "Locked")
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Data Locked");
                        return;
                    }
                    tabControl1.SelectedIndex = 0;
                    REPAIRDATE.Text = dataGridView2.Rows[e.RowIndex].Cells["REPAIR_DATE"].Value.ToString();
                    PRODLINE.Text = dataGridView2.Rows[e.RowIndex].Cells["PROD_LINE"].Value.ToString();
                    TOTALRECEIVED.Text = dataGridView2.Rows[e.RowIndex].Cells["TOTAL_RECEIVED"].Value.ToString();
                    TOTALREPAIRED.Text = dataGridView2.Rows[e.RowIndex].Cells["TOTAL_REPAIRED"].Value.ToString();
                    REMAININGQTY.Text = dataGridView2.Rows[e.RowIndex].Cells["REMAINING_QTY"].Value.ToString();
                    txtupdate.Text = dataGridView2.Rows[e.RowIndex].Cells["UPDATE_REASON"].Value.ToString();
                    index = REPAIRREASON.FindStringExact(dataGridView2.Rows[e.RowIndex].Cells["REPAIR_REASON"].Value.ToString());
                    if (index != -1)
                    {
                        REPAIRREASON.SelectedIndex = index;
                    }
                    else
                    {
                        index = REPAIRREASON.FindStringExact("Others");
                        REPAIRREASON.SelectedIndex = index;
                        REPAIRREASON.Enabled = false;
                        txtRepairReason.Text = dataGridView2.Rows[e.RowIndex].Cells["REPAIR_REASON"].Value.ToString();
                        txtRepairReason.Visible = true;
                        label5.Visible = true;
                    }
                    REPAIRDATE.Enabled= false;
                    PRODLINE.ReadOnly= true;
                    btn_update.Visible = true;
                    btn_cancel.Visible = true;
                    txtupdate.Visible = true;
                    label12.Visible = true;
                    btn_clear.Visible = false;   
                    btn_submit.Visible = false;
                    

                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            clear();
            REPAIRDATE.Enabled = true;
            PRODLINE.ReadOnly = false;
            tabControl1.SelectedIndex = 1;
            btn_update.Visible = false;
            btn_cancel.Visible = false;
            txtupdate.Visible = false;
            label12.Visible = false;
            btn_clear.Visible = true;
            btn_submit.Visible = true;
            REPAIRREASON.Enabled = true;
        }
    }
}
    

    

