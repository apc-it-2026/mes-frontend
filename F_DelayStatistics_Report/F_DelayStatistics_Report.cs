using MaterialSkin.Controls;
using NewExportExcels;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_DelayStatistics_Report
{
    public partial class F_DelayStatistics_Report :   MaterialForm
    {
       
        public F_DelayStatistics_Report()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            //设置窗体的双缓冲
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            //利用反射设置DataGridView的双缓冲
            Type dgvType = this.dataGridView1.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dataGridView1, true, null);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectSampleNo();
        }

        private void selectSampleNo()
        {

            dataGridView1.DataSource = null;

            Dictionary<string, Object> p = new Dictionary<string, object>();

            p.Add("Sample_NO", txtSampleOrder1.Text);
            p.Add("ART_NO", txtArtNo1.Text);
            p.Add("Suppliers_Code", txtVendor1.Text);
            p.Add("Part_No", txtPartNo1.Text);
            p.Add("Receive_Date", txtStartDate1.Value.ToString("yyyyMMdd"));
            p.Add("Receive_Date2", txtEndDate1.Value.ToString("yyyyMMdd"));
            p.Add("Receive_Unit", txtSendUnit1.Text);
            // p.Add("status", txtSendUnit1.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_DelayStatistics_ReportServer", "selectSampleNo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson == null || dtJson.Rows.Count <= 0)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "查无此数据！"); 
                    return;
                }
                else
                {
                    dataGridView1.DataSource = dtJson;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btnExport1_Click(object sender, EventArgs e)
        {
           
            ExportExcels.Export("发料明细", dataGridView1);
           
        }

        private void btnResetSelect1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
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
            }
        }

        private void btnResetSelect2_Click(object sender, EventArgs e)
        {
            this.dataGridView2.DataSource = null;
            foreach (Control item in panel3.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is DateTimePicker)
                {
                    item.Text = "";
                }
            }
        }

        private void btnSelect2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;

            Dictionary<string, Object> p = new Dictionary<string, object>();

            p.Add("Receive_Unit", textBox1.Text);
            p.Add("suppliers_code", textBox2.Text);
            p.Add("Receive_Date", dateTimePicker2.Value.ToString("yyyyMMdd"));
            p.Add("Receive_Date2", dateTimePicker1.Value.ToString("yyyyMMdd"));
            // p.Add("status", txtSendUnit1.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_DelayStatistics_ReportServer", "selectDelay", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson == null || dtJson.Rows.Count <= 0)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "查无此数据！");
                    return;
                }
                else
                {
                    dtJson.Columns.Add("delay_qty",typeof(int));
                    dtJson.Columns.Add("punctualradio");
                    dtJson.Columns.Add("Delayratio");
                    for(int i=0;i< dtJson.Rows.Count; i++)
                    {
                        dtJson.Rows[i]["delay_qty"] = Convert.ToInt32(dtJson.Rows[i]["tatol"]) - Convert.ToInt32(dtJson.Rows[i]["pp"]);
                        dtJson.Rows[i]["punctualradio"] = Math.Round(Convert.ToDouble(dtJson.Rows[i]["pp"])/Convert.ToDouble(dtJson.Rows[i]["tatol"])*100,2)+"%";
                        dtJson.Rows[i]["Delayratio"] = Math.Round(Convert.ToDouble(dtJson.Rows[i]["delay_qty"])/Convert.ToDouble(dtJson.Rows[i]["tatol"])*100,2)+"%";
                    }

                    dataGridView2.DataSource = dtJson;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btnSelectSampleOrder_Click(object sender, EventArgs e)
        {
            selectSampleNo();
        }

        private void btnExport2_Click(object sender, EventArgs e)
        {
            ExportExcels.Export("发料汇总", dataGridView2);
        }
    }
}
