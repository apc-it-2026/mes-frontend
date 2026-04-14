using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace Domestic_PO_Tracking
{
    public partial class SizeFinishReport : MaterialForm
    {
        string se_id = null;
        string size_no = null;
        string Type = null;

        public SizeFinishReport(string vSeId, string vSizeNo, string vType)
        {
            InitializeComponent();
            se_id = vSeId;
            size_no = vSizeNo;
            Type = vType;
            switch (Type)
            {
                case "C":
                    this.Text = "Cutting_Report";
                    break;

                case "S":
                    this.Text = "Stitching_Report";
                    break;

                case "L":
                    this.Text = "Assembly_Report";
                    break;

                case "A":
                    this.Text = "Packing_Report";
                    break;
            }
        }

      

        private void GetSizeFinishDetail(string se_id, string size_no)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vSeId", se_id);
            p.Add("vSizeNo", size_no);
            p.Add("vType", Type);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Transactions_Server", "GetSizeFinishDetail", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
               // DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                // Deserialize RetData into another dictionary that holds two tables
                var tablesDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                // Convert each table's JSON to DataTable
                DataTable dt1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(tablesDict["dt1"].ToString());
                DataTable dt2 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(tablesDict["dt2"].ToString());


                if (dt1.Rows.Count > 0)
                    dataGridView1.DataSource = dt2;

                if (dt2.Rows.Count > 0)
                    dataGridView2.DataSource = dt1;

                if (dt1.Rows.Count == 0 && dt2.Rows.Count == 0)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("查无此数据！", Program.Client, "", Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void SizeFinishReport_Load(object sender, EventArgs e)
        {
            GetSizeFinishDetail(se_id, size_no);
        }
    }
}
