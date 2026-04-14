using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutocompleteMenuNS;
using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;

namespace MonthlyExcessManPowerUpload
{
    public partial class MonthlyExcessManPowerUpload : MaterialForm
    {
        public MonthlyExcessManPowerUpload()
        {
            InitializeComponent();
        }

        private void MonthlyExcessManPowerUpload_Load(object sender, EventArgs e)
        {
            LoadSeDept();
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

        private void button2_Click(object sender, EventArgs e)
        {
            GetExcessApproveList();
        }

        public void GetExcessApproveList()
        {
         try 
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("ProdLine", textBox2.Text);
                retData.Add("ProdMonth", dateTimePicker5.Text);
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
                                            "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                                            "GetExcessApproveList",
                    Program.client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }

            }
             catch (Exception ex)
             {
             SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
             }
    }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($@"Are you sure you want to {button3.Text} the data?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveExcessApproveList(button3.Text);
            }
        }
        public void SaveExcessApproveList(string SaveType)
        {
            DataTable dt1 = new DataTable();

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    dt1.Columns.Add(column.Name);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["Check"].Value);
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
                    Dictionary<string, object> retData = new Dictionary<string, object>();
                    retData.Add("ProdLine", textBox2.Text);
                    retData.Add("ProdMonth", dateTimePicker5.Text);
                    retData.Add("SaveType", SaveType);
                    retData.Add("data", dt1);

                    string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
                                                "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                                                "SaveExcessApproveList",
                        Program.client.UserToken,
                        Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                    );
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (ret.IsSuccess)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                        GetExcessApproveList();
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                        GetExcessApproveList();
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

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            GetExcessApproveList();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show($@"Are you sure you want to {button4.Text} the data?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveExcessApproveList(button4.Text);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GetExcessApprovedRejectedList();
        }

        public void GetExcessApprovedRejectedList()
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("ProdLine", textBox1.Text);
                retData.Add("ProdMonth", dateTimePicker1.Text);
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder",
                                            "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer",
                                            "GetExcessApprovedRejectedList",
                    Program.client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());
                    DataTable dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
                   
                    dataGridView2.DataSource = dt1;
                    dataGridView3.DataSource = dt2;
                    
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }

            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }
    }
}
