using AutocompleteMenuNS;
using MaterialSkin.Controls;
using NewExportExcels;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPIINPUT
{
    public partial class Haulting : MaterialForm
    {
        public Haulting()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
        public void clear()
        {
            CONTAINER_TYPE.SelectedIndex = -1;
            TRUCK_NO.Text = "";
            DESTINATION.Text = "";
            HAULTING_TYPE.SelectedIndex = -1;
            RESPONSIBLE_DEPARTMENT.Text="";
            HAULTING_QTY.Text = "";
            DELAY_PO.Text = "";
            HAULTING_REASON.Text = "";
            NO_DAYS.Text="";




        }
        public void Load_Po()
        {
            DELAY_PO.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            DELAY_PO.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Ponum.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Ponum.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> p = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.HaultingsServer", "Get_Po", Program.client.UserToken, JsonConvert.SerializeObject(p));

            ResultObject retObject = JsonConvert.DeserializeObject<ResultObject>(ret);
            if (retObject.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(retObject.RetData);
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson.Rows.Count > 0)
                {
                          autocompleteMenu2.MaximumSize = new Size(250, 350);
                          var columnWidth = new[] { 50, 200 };

                           int n = 1;
                           for (int i = 0; i < dtJson.Rows.Count; i++)
                           {
                             autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dtJson.Rows[i]["CUSTOMER_PO"].ToString() }, dtJson.Rows[i]["CUSTOMER_PO"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                             n++;
                           }

                }
              
            }
        }
        public void LoadProd_Line()
        {
            RESPONSIBLE_DEPARTMENT.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            RESPONSIBLE_DEPARTMENT.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Responsible_dept.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Responsible_dept.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> p = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.HaultingsServer", "GetDepts", Program.client.UserToken, JsonConvert.SerializeObject(p));
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
        public void RefreshData()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Fromdate", Fromdate.Text);
            retData.Add("Todate", Todate.Text);
            retData.Add("Responsible_dept", Responsible_dept.Text);

            retData.Add("Ponum", Ponum.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.HaultingsServer",
                     "FilterHaultingsData",
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
                   
                }

            }
            else
            {
                dataGridView2.IsEmpty();
                dataGridView2.DataSource = null;
                Ponum.Text = "";
                Responsible_dept.Text = "";
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");

            }
        }
            private void Label5_Click(object sender, EventArgs e)
        {

        }
        private void LoadTodayInputData()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("HAULTING_DATE", DateTime.Now.Date.ToString("yyyy-MM-dd"));


            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.HaultingsServer",
                 "ViewHaultingsData",
Program.client.UserToken,
Newtonsoft.Json.JsonConvert.SerializeObject(retData)
);

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
               if(dtJson.Rows.Count>0) 
                {
                    dataGridView1.DataSource = dtJson;
                }
                else
                {
                    dataGridView1.DataSource = null;
                }

            }
        }
            private void Button1_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(CONTAINER_TYPE.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Container Type");
                    return;
                }
                if (string.IsNullOrEmpty(TRUCK_NO.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Truck Number");
                    return;
                }
                if (string.IsNullOrEmpty(DESTINATION.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Destination Location");
                    return;
                }
                if (string.IsNullOrEmpty(HAULTING_TYPE.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Haulting Type");
                    return;
                }
                if (string.IsNullOrEmpty(HAULTING_QTY.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Haulting Qty");
                    return;
                }
                if (string.IsNullOrEmpty(DELAY_PO.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Delay PO Number");
                    return;
                }
                if (string.IsNullOrEmpty(HAULTING_REASON.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Haulting Reason");
                    return;
                }
                if (string.IsNullOrEmpty(RESPONSIBLE_DEPARTMENT.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Responsible Department");
                    return;
                }
                if (string.IsNullOrEmpty(NO_DAYS.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Number of Delay Days");
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;

                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("HAULTING_DATE", HAULTING_DATE2.Text);
                retData.Add("CONTAINER_TYPE", CONTAINER_TYPE.Text);
                retData.Add("TRUCK_NO", TRUCK_NO.Text);
                retData.Add("DESTINATION", DESTINATION.Text);
                retData.Add("HAULTING_TYPE", HAULTING_TYPE.Text);
                retData.Add("HAULTING_QTY", HAULTING_QTY.Text);
                retData.Add("DELAY_PO", DELAY_PO.Text);
                retData.Add("HAULTING_REASON", HAULTING_REASON.Text);
                retData.Add("RESPONSIBLE_DEPARTMENT", RESPONSIBLE_DEPARTMENT.Text);
                retData.Add("NO_DAYS", NO_DAYS.Text);

                


                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.HaultingsServer",
                     "SendHaultingsData",
    Program.client.UserToken,
    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
    );
              ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Data Inserted Successfully");
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to insert the data");
               
            }


            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        dataGridView1.DataSource = dtJson;

                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["ErrMsg"].ToString());
                }
                clear();
            LoadTodayInputData();
        }

            private void Button3_Click(object sender, EventArgs e)
            {
            clear();
            HAULTING_DATE2.MinDate = DateTime.Today.AddDays(-4);
            button1.Visible = true;
            button2.Visible = false;
            HAULTING_DATE2.Enabled = true;
            DELAY_PO.ReadOnly = false;
            label15.Visible = false;
            textBox1.Visible = false;
             }

            private void Button4_Click(object sender, EventArgs e)
            {
                 RefreshData();
            }

            private void Button2_Click(object sender, EventArgs e)
            {
            if (string.IsNullOrEmpty(CONTAINER_TYPE.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Container Type");
                return;
            }
            if (string.IsNullOrEmpty(TRUCK_NO.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Truck Number");
                return;
            }
            if (string.IsNullOrEmpty(DESTINATION.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Destination Location");
                return;
            }
            if (string.IsNullOrEmpty(HAULTING_TYPE.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Haulting Type");
                return;
            }
            if (string.IsNullOrEmpty(HAULTING_QTY.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Haulting Qty");
                return;
            }
            if (string.IsNullOrEmpty(HAULTING_REASON.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Haulting Reason");
                return;
            }
            if (string.IsNullOrEmpty(RESPONSIBLE_DEPARTMENT.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Responsible Department");
                return;
            }
            if (string.IsNullOrEmpty(NO_DAYS.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Number of Delay Days");
                return;
            }
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Update Reason");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;

            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("HAULTING_DATE", HAULTING_DATE2.Text);
            retData.Add("CONTAINER_TYPE", CONTAINER_TYPE.Text);
            retData.Add("TRUCK_NO", TRUCK_NO.Text);
            retData.Add("DESTINATION", DESTINATION.Text);
            retData.Add("HAULTING_TYPE", HAULTING_TYPE.Text);
            retData.Add("HAULTING_QTY", HAULTING_QTY.Text);
            retData.Add("DELAY_PO", DELAY_PO.Text);
            retData.Add("UPDATE_REASON", textBox1.Text);
            retData.Add("HAULTING_REASON", HAULTING_REASON.Text);
            retData.Add("RESPONSIBLE_DEPARTMENT", RESPONSIBLE_DEPARTMENT.Text);
            retData.Add("NO_DAYS", NO_DAYS.Text);




            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.HaultingsServer",
                 "UpdateHaultingsData",
Program.client.UserToken,
Newtonsoft.Json.JsonConvert.SerializeObject(retData)
);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Data Updated Successfully");
                clear();
                HAULTING_DATE2.MinDate = DateTime.Today.AddDays(-4);
                label15.Visible = false;
                textBox1.Text = "";
                textBox1.Visible = false;
                button2.Visible = false;
                button1.Visible = true;
                clear();
                RefreshData();
                DELAY_PO.ReadOnly = false;
                HAULTING_DATE2.Enabled = true;
                tabControl1.SelectedIndex = 1;


            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to update the data");
               
            }


        }
    
        


        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TabPage2_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
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
                            //string Date = dataGridView2.Rows[e.RowIndex].Cells["DATE"].Value.ToString();
                            string Date = dataGridView2.Rows[e.RowIndex].Cells["HAULTING_DATE"].Value.ToString();
                            string ProdDate = Date.Split(' ')[0];
                            string ProdLine = dataGridView2.Rows[e.RowIndex].Cells["Column8"].Value.ToString();
                            string DelayPo = dataGridView2.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("ProdDate", ProdDate);
                            p.Add("ProdLine", ProdLine);
                            p.Add("DelayPo", DelayPo);
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.HaultingsServer",
                    "DeleteHaultingsData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Deleted Successfully");
                                RefreshData();
                            }
                            else
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to delete the data");
                               
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
                    if (dataGridView2.Rows[e.RowIndex].Cells["LOCK_STATUS"].Value.ToString() == "Locked")
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Data Locked");
                        return;
                    }
                    tabControl1.SelectedIndex = 0;
                    HAULTING_DATE2.MinDate = DateTime.Today.AddDays(-90);
                    HAULTING_DATE2.Text = dataGridView2.Rows[e.RowIndex].Cells["HAULTING_DATE"].Value.ToString();
                    CONTAINER_TYPE.Text = dataGridView2.Rows[e.RowIndex].Cells["Column2"].Value.ToString();
                    TRUCK_NO.Text = dataGridView2.Rows[e.RowIndex].Cells["Column3"].Value.ToString();
                    DESTINATION.Text = dataGridView2.Rows[e.RowIndex].Cells["Column4"].Value.ToString();
                    HAULTING_TYPE.Text = dataGridView2.Rows[e.RowIndex].Cells["Column5"].Value.ToString();
                    HAULTING_QTY.Text = dataGridView2.Rows[e.RowIndex].Cells["Column6"].Value.ToString();
                    DELAY_PO.Text = dataGridView2.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
                    HAULTING_REASON.Text = dataGridView2.Rows[e.RowIndex].Cells["Column7"].Value.ToString();
                    RESPONSIBLE_DEPARTMENT.Text = dataGridView2.Rows[e.RowIndex].Cells["Column8"].Value.ToString();
                    NO_DAYS.Text = dataGridView2.Rows[e.RowIndex].Cells["Column9"].Value.ToString();
                    HAULTING_DATE2.Enabled = false;
                    CONTAINER_TYPE.Enabled = true;
                    TRUCK_NO.ReadOnly = false;
                    DESTINATION.Enabled = false;
                    HAULTING_TYPE.Enabled = true;
                    HAULTING_QTY.ReadOnly = false;
                    DELAY_PO.ReadOnly = true;
                    HAULTING_REASON.ReadOnly = false;
                    RESPONSIBLE_DEPARTMENT.Enabled = true;
                    NO_DAYS.ReadOnly = false;
                    label15.Visible = true;
                    textBox1.Visible = true;
                    button2.Visible = true;
                    button1.Visible = false;

                }
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex==0)
            {
                LoadTodayInputData();
            }
        }
        


        private void TableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HAULTING_DATE_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Haulting_Load(object sender, EventArgs e)
        {
            LoadTodayInputData();
            LoadProd_Line();
            Load_Po();
            DESTINATION.Enabled = false;
            HAULTING_DATE2.MinDate = DateTime.Today.AddDays(-4);
            HAULTING_DATE2.MaxDate = DateTime.Today;
        }

        private void DESTINATION_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DELAY_PO_TextChanged(object sender, EventArgs e)
        {


        }

        private void DELAY_PO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();

                retData.Add("DELAY_PO", DELAY_PO.Text);


                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.HaultingsServer",
                     "GetDestination",
    Program.client.UserToken,
    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
    );

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    if (dtJson.Rows.Count > 0)
                    {
                        DESTINATION.Text = dtJson.Rows[0]["C_NAME"].ToString(); // Replace "YourColumnName" with the actual column name
                    }

                }


            }
        }

        private void HAULTING_QTY_KeyPress(object sender, KeyPressEventArgs e)
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

        private void NO_DAYS_KeyPress(object sender, KeyPressEventArgs e)
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

        private void SplitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SplitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SplitContainer1_Panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void SplitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void Splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void SplitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "HaultingReport.xls";
                ExportExcels.Export(a, dataGridView2);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }
    }
}
