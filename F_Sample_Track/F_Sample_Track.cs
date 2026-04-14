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
    public partial class F_Sample_Track : MaterialForm
    {
        public DataTable partDt;
        public F_Sample_Track()
        {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            List < ComboboxEntry > pp= new List<ComboboxEntry>();
            pp.Add(new ComboboxEntry(){ ENUM_CODE="Y",ENUM_VALUE="样品室"});
            pp.Add(new ComboboxEntry(){ ENUM_CODE="N",ENUM_VALUE="工艺部"});
            comboBox1.DataSource = pp;
            comboBox1.DisplayMember = "ENUM_VALUE";
            comboBox1.ValueMember = "ENUM_CODE";
            GetSampleList();
            GetSampleUnit();
            GetPartList();
        }

        public void GetSampleList()
        {
            var orderSource = new AutoCompleteStringCollection();
            string sql = string.Format(@" select distinct sample_no from mes_sample_logo_list");
            Dictionary<string, string> valuePairs = new Dictionary<string, string>();
            valuePairs.Add("sql", sql);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Sample_ReceiveService", "GetSQLData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(valuePairs));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    orderSource.Add(dataTable.Rows[i]["sample_no"].ToString());
                }



                txtSampleOrder1.AutoCompleteCustomSource = orderSource;
                txtSampleOrder1.AutoCompleteMode = AutoCompleteMode.Suggest;    //显示相关下拉
                txtSampleOrder1.AutoCompleteSource = AutoCompleteSource.CustomSource;   //设置属性

               
            }
        }

        public void GetSampleUnit()
        {
            var orderSource = new AutoCompleteStringCollection();
            string sql = string.Format(@"select department_code||'|'||department_name as unit from base005m");
            Dictionary<string, string> valuePairs = new Dictionary<string, string>();
            valuePairs.Add("sql", sql);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Sample_ReceiveService", "GetSQLData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(valuePairs));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    orderSource.Add(dataTable.Rows[i]["unit"].ToString());
                }
                txtSendUnit1.AutoCompleteCustomSource = orderSource;
                txtSendUnit1.AutoCompleteMode = AutoCompleteMode.Suggest;    //显示相关下拉
                txtSendUnit1.AutoCompleteSource = AutoCompleteSource.CustomSource;   //设置属性
            }
        }

        public void GetPartList()
        {
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Sample_NG_ReportService", "GetPartList", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                partDt = dataTable;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        private void btnSelect_Click(object sender, EventArgs e)
        {
          
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sample_no",txtSampleOrder1.Text.ToString() );
            //dictionary.Add("sample",txtSampleOrder1 );
            dictionary.Add("part_no",txtPartNo1.Text.ToString() );
            dictionary.Add("sendUnit", txtSendUnit1.Text.ToString() );
            dictionary.Add("startDate",txtStartDate1.Value.ToString("yyyyMMdd") );
            dictionary.Add("endDate",txtEndDate1.Value.ToString("yyyyMMdd"));
            dictionary.Add("COL",comboBox1.SelectedValue.ToString());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Sample_NG_ReportService", "GetTrackDataIfon", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"])) 
            {
                dataGridView1.DataSource = null;
                string v = JsonConvert.DeserializeObject<Dictionary<string,object>>(ret)["RetData"].ToString();
                DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(v);
                dataGridView1.DataSource = dataTable;
            }
            else 
            {
                dataGridView1.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());

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

        private void btnSelectSampleOrder_Click(object sender, EventArgs e)
        {

        }

        private void txtSendUnit1_TextChanged(object sender, EventArgs e)
        {
            string s = txtSendUnit1.Text.ToString();
           // int i = s.IndexOf('|')==-1? 0: s.IndexOf('|');
            if (s.IndexOf('|') != -1) 
            {
                s = s.Substring(0, s.IndexOf('|'));
                txtSendUnit1.Text = s;
                Console.WriteLine(txtSendUnit1.Text.ToString());
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            partList partList = new partList(partDt);
            partList.sengMessage += new partList.sengPartList(ToShowGetStationMessage);
            partList.ShowDialog();
        }

        public void ToShowGetStationMessage(string name, string values)
        {
            txtPartNo1.Text = name;
            textBox1.Text = values;
        }
    }

    public class ComboboxEntry
    {
        public string ENUM_CODE { get; set; }
        public string ENUM_VALUE { get; set; }
    }
}
