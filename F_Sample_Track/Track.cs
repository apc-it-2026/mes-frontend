using MaterialSkin.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_Sample_Track
{
    public partial class Track : MaterialForm
    {
        public Track()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            /*if (txtSampleOrder1.Text.ToString() == "") 
            {
                MessageBox.Show("请输入样品单");
                return;
            }*/
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sample_no",txtSampleOrder1.Text.ToString() );
            //dictionary.Add("sample",txtSampleOrder1 );
            dictionary.Add("part_no",txtPartNo1.Text.ToString() );
            dictionary.Add("sendUnit", txtSendUnit1.Text.ToString() );
            dictionary.Add("startDate",txtStartDate1.Value.ToString("yyyyMMdd") );
            dictionary.Add("endDate",txtEndDate1.Value.ToString("yyyyMMdd"));
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Sample_NG_ReportService", "GetTrackDataIfon", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"])) 
            {
                dataGridView1.DataSource = null;
                string v = JsonConvert.DeserializeObject<Dictionary<string,object>>(ret)["RetData"].ToString();
                DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(v);
                dataGridView1.DataSource = dataTable;

            }
        }

        private void btnResetSelect1_Click(object sender, EventArgs e)
        {
            this.txtSampleOrder1.Text = "";
            txtPartNo1.Text = "";
            txtSendUnit1.Text = "";
        }

        private void btnExport1_Click(object sender, EventArgs e)
        {
            NewExportExcels.ExportExcels.Export("样品单追踪表",dataGridView1);
        }
    }
}
