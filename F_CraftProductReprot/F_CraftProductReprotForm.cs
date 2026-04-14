using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
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

namespace F_CraftProductReprot
{
    public partial class F_CraftProductReprotForm : MaterialForm
    {
        public F_CraftProductReprotForm()
        {
            InitializeComponent();
        }
        private void F_CraftProductReprot_Load(object sender, EventArgs e)
        {
            SelectSampleNO();
        }
        /// <summary>
        /// 查询样品单
        /// </summary>
        private void SelectSampleNO()
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductReprotServer", "SelectSampleNO", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    collection.Add(dtJson.Rows[i - 1]["sample_no"].ToString());
                }
                txtSampleNo1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtSampleNo1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtSampleNo1.AutoCompleteCustomSource = collection;
                txtSampleNo2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtSampleNo2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtSampleNo2.AutoCompleteCustomSource = collection;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void btnSelectDay_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("sample_no", txtSampleNo1.Text);
            dic.Add("productUnit", txtProductUnit1.Text);
            dic.Add("process",txtProcess1.Text);
            dic.Add("startDate",txtStartDate.Value.ToString("d"));
            dic.Add("endDate", txtEndDate.Value.ToString("d"));
            dic.Add("partNo",txtPartNo1.Text);
            dic.Add("inoutType", cbType.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductReprotServer", "SelectCraftByDay", Program.client.UserToken, JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                DataRow dr = WinFormLib.TotalRow.GetTotalRow(dt);
                dr["SAMPLE_NO"] = "";
                dr["STAGE"] = "";
                dr["PURPOSE"] = "";
                dr["ART_NO"] = "";
                dr["art_name"] = "";
                dr["color_way"] = "";
                dr["SIZE_NO"] = "";
                dr["part_name"] = "";
                dr["PROCESS_NAME"] = "";
                dr["work_date"] = "";
                dr["GRP_DEPT"] = "总计";
                dr["INOUT_TYPE"] = "";
                dr["sample_seq"] = "";
                dt.Rows.Add(dr);
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = dt;
                if (cbType.Text=="投入")
                {
                    dataGridView1.Columns["QTY"].HeaderText = "投入数量";
                }
                if (cbType.Text=="产出")
                {
                    dataGridView1.Columns["QTY"].HeaderText = "产出数量";
                }
                if (cbType.Text=="")
                {
                    dataGridView1.Columns["QTY"].HeaderText = "投入/产出数量";
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                dataGridView1.DataSource = null;
                if (cbType.Text == "投入")
                {
                    dataGridView1.Columns["QTY"].HeaderText = "投入数量";
                }
                if (cbType.Text == "产出")
                {
                    dataGridView1.Columns["QTY"].HeaderText = "产出数量";
                }
                if (cbType.Text == "")
                {
                    dataGridView1.Columns["QTY"].HeaderText = "投入/产出数量";
                }
            }
        }

        private void btnExportDay_Click(object sender, EventArgs e)
        {
            NewExportExcels.ExportExcels.Export("工艺生产"+(cbType.Text == "" ? "投入产出" : cbType.Text) +"报表",dataGridView1);
        }

        private void btnResetDay_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is DateTimePicker)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("sample_no", txtSampleNo2.Text);
            dic.Add("productUnit", txtProductUnit2.Text);
            dic.Add("process", txtProcess2.Text);
            dic.Add("partNo", txtPartNo2.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_CraftProductReprotServer", "SelectCraftByAll", Program.client.UserToken, JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                DataRow dr = WinFormLib.TotalRow.GetTotalRow(dt);
                dr["SAMPLE_NO"] = "";
                dr["STAGE"] = "";
                dr["PURPOSE"] = "";
                dr["ART_NO"] = "";
                dr["art_name"] = "";
                dr["color_way"] = "";
                dr["SIZE_NO"] = "";
                dr["part_name"] = "";
                dr["PROCESS_NAME"] = "";
                dr["productUnit"] = "总计";
                dr["SAMPLE_SEQ"] = "";
                dt.Rows.Add(dr);
                dataGridView2.AutoGenerateColumns = false;
                dataGridView2.DataSource = dt;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                dataGridView2.DataSource = null;
            }
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnExportAll_Click(object sender, EventArgs e)
        {
            NewExportExcels.ExportExcels.Export("工艺生产汇总报表", dataGridView2);
        }
    }
}
