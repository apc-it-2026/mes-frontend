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

namespace Data_Lock
{
    public partial class Data_Lock : MaterialForm
    {
        public Data_Lock()
        {
            InitializeComponent();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            //GetData();
        }
        private void GetData()
        {
            if (string.IsNullOrEmpty(cb_criteria.Text))
            {
                return;
            }

            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("StartDate", dt_sdate.Text);
            Data.Add("EndDate", dt_edate.Text);
            Data.Add("ProdLine", txtprodline.Text);
            Data.Add("Criteria", cb_criteria.Text);
            Data.Add("LockStatus", cb_lock_status.Text);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.Lock_UnlockServer",
                "GetData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson1.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtJson1;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (col.Name != "check")
                        {
                            col.ReadOnly = true;  
                        }
                    }
                }
                else
                {
                    dataGridView1.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                   // cb_criteria.SelectedIndex = -1;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
              //  cb_criteria.SelectedIndex = -1;
            }
        }


        private void Data_Lock_Load(object sender, EventArgs e)
        {
            LoadSeDept();
            dt_sdate.MaxDate = DateTime.Today;
            dt_edate.MaxDate = DateTime.Today;
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

        private void btn_select_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.RowCount > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells["Check"].Value = true;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
        }

        private void btn_deselect_Click(object sender, EventArgs e)
        {
           
            if (dataGridView1.RowCount > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells["Check"].Value = false;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
        }

        private void btn_lock_Click(object sender, EventArgs e)
        {
            UpdateStatus(btn_lock.Text);
        }
        private void btn_unlock_Click(object sender, EventArgs e)
        {
            UpdateStatus(btn_unlock.Text);
        }

        public void UpdateStatus(string ButtonText)
        {
            DataTable dt1 = new DataTable();

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    dt1.Columns.Add(column.Name);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["check"].Value);
                    if (isSelected)
                    {
                        DataRow dRow = dt1.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dt1.Rows.Add(dRow);

                    }
                }

                if (dt1.Rows.Count > 0)
                {
                    DialogResult result = MessageBox.Show($@"Are you sure you want to {ButtonText} the data?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        UpdateLockStatus(ButtonText, cb_criteria.Text, dt1);
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Selected");
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
        }
        
        public void UpdateLockStatus(string Status,string Criteria, DataTable dt)
        {
            
                    Dictionary<string, object> retData = new Dictionary<string, object>();
                    retData.Add("data", dt);
                    retData.Add("Criteria", Criteria);
                    retData.Add("Status", Status);
                    retData.Add("Date_s", dt_sdate.ToString());
                    retData.Add("Date_e", dt_edate.ToString());

                    string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.Lock_UnlockServer",
                                                "UpdateLockStatus",
                        Program.client.UserToken,
                        Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                    );
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (ret.IsSuccess)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Data"+ Status+"ed Successfully");
                        GetData();
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                        GetData();
                    }
        }
        private void btn_export_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = $@"{cb_criteria.Text}.xls";
                ExportExcels.Export(a, dataGridView1);
            }
        }

        private void cb_criteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void cb_lock_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void Btn_unlock_new_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cb_criteria.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Criteria");
                return;
            }
            Unlock_for_New_Entry();
        }
        public void Unlock_for_New_Entry()
        {

            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Criteria", cb_criteria.Text);
            retData.Add("Date_s", dt_sdate.Value.ToString("yyyy/MM/dd"));
            retData.Add("Date_e", dt_edate.Value.ToString("yyyy/MM/dd"));

            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.Lock_UnlockServer",
                                        "Unlock_for_New_Entry",
                Program.client.UserToken,
                Newtonsoft.Json.JsonConvert.SerializeObject(retData)
            );
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "UnLocked Successfully");
                GetData();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                GetData();
            }


        }
    }
}
