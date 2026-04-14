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

namespace F_FormCompleteReport
{
    public partial class F_FormCompleteReport : MaterialForm
    {
        public F_FormCompleteReport()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("sampleNo", txtSampleOrder.Text);
            dic.Add("artNo", txtArtNo.Text);
            dic.Add("unit", txtUnit.Text);
            dic.Add("startDate", txtStartDate.Value.ToString("d"));
            dic.Add("endDate", txtEndDate.Value.ToString("d"));
            dic.Add("YN",cbSampleOrCraft.SelectedItem.ToString());
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_FormCompleteReportServer", "SelectInfo", Program.client.UserToken, JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //DataTable dt = JsonHelper.GetDataTableByJson(json);
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = dt;
                string path = Application.StartupPath + @"\\" + "工艺委外收发明细报表" + ".frx";
                Dictionary<string, string> p = new Dictionary<string, string>();
                FastReportHelper.LoadFastReport(panel1, path, p, dt, "Table");
                
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                panel1.Controls.Clear();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel2.Controls)
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

        private void btnExport_Click(object sender, EventArgs e)
        {
          
        }

        private void F_FormCompleteReport_Load(object sender, EventArgs e)
        {
            SelectSampleNO();
            cbSampleOrCraft.SelectedItem = "样品室";
        }
        /// <summary>
        /// 查询样品单
        /// </summary>
        private void SelectSampleNO()
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_FormCompleteReportServer", "SelectSampleNO", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    collection.Add(dtJson.Rows[i - 1]["sample_no"].ToString());
                }
                txtSampleOrder.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtSampleOrder.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtSampleOrder.AutoCompleteCustomSource = collection;
                txtSampleOrder.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtSampleOrder.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtSampleOrder.AutoCompleteCustomSource = collection;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
    }
}
