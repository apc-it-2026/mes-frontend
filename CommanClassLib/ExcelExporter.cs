using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;

namespace CommanClassLib
{
    public partial class ExcelExporter : MaterialForm

    {
        string APIURL;
        string UserToken;

        public ExcelExporter()
        {
            InitializeComponent();
        }

        public ExcelExporter(string sAPIURL, string sUserToken)
        {
            APIURL = sAPIURL;
            UserToken = sUserToken;
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string vPlantNo = string.IsNullOrWhiteSpace(cbPlant.Text) ? cbPlant.Text : cbPlant.Text.Split('|')[0];
            string vRoutNo = string.IsNullOrWhiteSpace(cbRout.Text) ? cbRout.Text : cbRout.Text.Split('|')[0];
            //dgvFullWorkingHours.DataSource = null;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vOrgNo", cbOrg.Text);
            p.Add("vPlantNo", vPlantNo);
            p.Add("vDeptNo", tbDept.Text);
            p.Add("vRoutNo", vRoutNo);
            p.Add("vWorkFromDate", dtpFrom.Value.ToString("yyyy/MM/dd"));
            p.Add("vWorkToDate", dtpEnd.Value.ToString("yyyy/MM/dd"));

            string ret = WebAPIHelper.Post(APIURL,
                "KZ_SFCAPI",
                "KZ_SFCAPI.Controllers.ProductionInputServer",
                "QueryFullWorkingHour",
                UserToken, JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    dgvFullWorkingHours.DataSource = dtJson;
                    //dgvFullWorkingHours.Rows[0].Selected = false;
                    /* decimal sum_work_hours = 0;
                     int sum_jockey_qty = 0;
                     int sum_pluripotent_worker = 0;
                     int sum_omnipotent_worker = 0;
                     int sum_udf01 = 0;
                     decimal sum_TRANIN_HOURS = 0;
                     decimal sum_TRANOUT_HOURS = 0;
                     decimal sum_AllWorkTime = 0;

                     foreach (DataGridViewRow row in dgvFullWorkingHours.Rows)
                     {
                         sum_work_hours += Convert.ToDecimal(row.Cells["work_hours"].Value);
                         sum_jockey_qty += Convert.ToInt32(row.Cells["jockey_qty"].Value);
                         sum_pluripotent_worker += Convert.ToInt32(row.Cells["pluripotent_worker"].Value);
                         sum_omnipotent_worker += Convert.ToInt32(row.Cells["omnipotent_worker"].Value);
                         sum_udf01 += Convert.ToInt32(row.Cells["udf01"].Value);
                         sum_TRANIN_HOURS += Convert.ToDecimal(row.Cells["TRANIN_HOURS"].Value);
                         sum_TRANOUT_HOURS += Convert.ToDecimal(row.Cells["TRANOUT_HOURS"].Value);
                         sum_AllWorkTime += Convert.ToDecimal(row.Cells["AllWorkTime"].Value);
                     }

                      ((DataTable)dgvFullWorkingHours.DataSource).Rows.Add(string.Empty, "合计: ", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                         sum_work_hours, sum_jockey_qty, sum_pluripotent_worker, sum_omnipotent_worker, sum_udf01, sum_TRANIN_HOURS, sum_TRANOUT_HOURS, sum_AllWorkTime);
                     dgvFullWorkingHours.Rows[dgvFullWorkingHours.Rows.Count - 2].DefaultCellStyle.BackColor = Color.SkyBlue;
                     dgvFullWorkingHours.Rows[dgvFullWorkingHours.Rows.Count - 1].ReadOnly = true;*/
                    dgvFullWorkingHours.AllowUserToAddRows = false;
                    object[] TotaldgvRow =WinFormLib.TotalRow.GetTotalObject(dgvFullWorkingHours);
                    ((DataTable)dgvFullWorkingHours.DataSource).Rows.Add(TotaldgvRow);

                    dgvFullWorkingHours.Rows[dgvFullWorkingHours.Rows.Count - 1].Cells[0].Value = "Total";
                    dgvFullWorkingHours.Rows[dgvFullWorkingHours.Rows.Count - 1].DefaultCellStyle.BackColor = Color.SkyBlue;
                    dgvFullWorkingHours.Rows[dgvFullWorkingHours.Rows.Count - 1].ReadOnly = true;
                }
                else
                {
                    //SJeMES_Control_Library.MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UIVisiable(this, "查无此数据！", Program.client));
                    MessageBox.Show("No such data");
                }
            }
            else
            {
                MessageBox.Show("No such data");
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export Excel file to";

            saveFileDialog.ShowDialog();
            try
            {
                Stream myStream;
                myStream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, Encoding.GetEncoding("gb2312"));
                //Sheet.RepeatingRows = new CellRangeAddress(0, 5, 0, 5);
                string str = "";

                //write a title  
                for (int i = 0; i < dgvFullWorkingHours.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        str += "\t";
                    }

                    str += dgvFullWorkingHours.Columns[i].HeaderText;
                }

                sw.WriteLine(str);
                //Write content from line 0 needs to be decremented by one
                for (int j = 0; j < dgvFullWorkingHours.Rows.Count - 1; j++)
                {
                    string tempStr = "";
                    for (int k = 0; k < dgvFullWorkingHours.Columns.Count; k++)
                    {
                        if (k > 0)
                        {
                            tempStr += "\t";
                        }

                        string cell = dgvFullWorkingHours.Rows[j].Cells[k].Value.ToString();
                        cell = cell.Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("\r\n", "");
                        tempStr += cell;
                    }

                    sw.WriteLine(tempStr);
                }

                //Add a new line to sum

                MessageBox.Show("Export was successful", "hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sw.Close();
                myStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ExcelExporter_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.Height = Height - 60;
            dtpFrom.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now.AddDays(7);

            LoadQueryItem();
        }

        private void LoadQueryItem()
        {
            var items1 = new List<AutocompleteItem>();
            string ret1 = WebAPIHelper.Post(APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ProductionDashBoardServer", "LoadOrg", UserToken, JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                items1.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["company"].ToString() }, dtJson.Rows[i - 1]["company"].ToString()));
                }
            }

            cbOrg.DataSource = items1;

            var items2 = new List<AutocompleteItem>();
            string ret2 = WebAPIHelper.Post(APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ProductionDashBoardServer", "LoadPlant", UserToken, JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                items2.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items2.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["code"].ToString(), dtJson.Rows[i - 1]["org"].ToString() }, dtJson.Rows[i - 1]["code"] + "|" + dtJson.Rows[i - 1]["org"]));
                }
            }

            cbPlant.DataSource = items2;

            var items3 = new List<AutocompleteItem>();
            string ret3 = WebAPIHelper.Post(APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ProductionTargetsServer", "LoadSeDept", UserToken, JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["DEPARTMENT_CODE"].ToString(), dtJson.Rows[i - 1]["DEPARTMENT_NAME"].ToString() }, dtJson.Rows[i - 1]["DEPARTMENT_CODE"].ToString()));
                }
            }



            AutocompleteMenuNS.AutocompleteMenu autocompleteMenu3 = new AutocompleteMenuNS.AutocompleteMenu();
            autocompleteMenu3.MaximumSize = new Size(350, 350);
            autocompleteMenu3.SetAutocompleteMenu(tbDept, autocompleteMenu3);
            autocompleteMenu3.SetAutocompleteMenu(tbDept, autocompleteMenu3);
            autocompleteMenu3.SetAutocompleteItems(items3);


            var items4 = new List<AutocompleteItem>();
            string ret4 = WebAPIHelper.Post(APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ProductionDashBoardServer", "LoadRoutNo", UserToken, JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                items4.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items4.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["rout_no"].ToString(), dtJson.Rows[i - 1]["rout_name_z"].ToString() }, dtJson.Rows[i - 1]["rout_no"] + "|" + dtJson.Rows[i - 1]["rout_name_z"]));
                }
            }

            cbRout.DataSource = items4;
        }

        private void ExcelExporter_Resize(object sender, EventArgs e)
        {
            tableLayoutPanel1.Height = Height - 60;
        }
    }
}