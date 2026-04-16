using Newtonsoft.Json;
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

namespace TierAudits_New
{
    public partial class WorkInjury : Form
    {
        string Prodline = "";
        public WorkInjury()
        {
            InitializeComponent();
        }

        private void WorkInjury_Load(object sender, EventArgs e)
        {
            LoadDept();
        }

        private void GetDept()
        {
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetDept", Program.Client.UserToken, string.Empty);
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                Prodline = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
                // d_dept_name = dtJson.Rows[0]["DEPARTMENT_NAME"].ToString();
                // orgId = dtJson.Rows[0]["ORG_ID"].ToString();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void LoadDept()
        {
            try
            {
                GetDept();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            line.Text = Prodline;
            //labelDeptName.Text = d_dept_name;
        }

        private void issueSubmit_Click(object sender, EventArgs e)
        {
            // Convert the line text to upper case
            string lineValue = line.Text.ToUpper();

            // Check the length of the line value
            if (lineValue.Length < 7)
            {
                // Display an error message and do not proceed with the operation
                MessageBox.Show("Line length should be 7 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected item from the issue_box ComboBox
            if (workInjury_details.SelectedItem == null)
            {
                MessageBox.Show("Please select an injury details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string injury_details = workInjury_details.SelectedItem.ToString();

            // Get the date from the end_date DateTimePicker and format it
            DateTime injuryDate = Injury_date.Value;
            string Idate = injuryDate.ToString("yyyy/MM/dd");

            // Prepare the data for the API call
            Dictionary<string, object> p = new Dictionary<string, object>
    {
        { "Line", lineValue },
        { "Idate", Idate },
        { "injury_details", injury_details }
    };

            // Make the API call
            // Make the API call
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "InsertInjury", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            // Process the API response
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();

                if (!string.IsNullOrEmpty(json))
                {
                    MessageBox.Show("Successfully Submitted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to Submit!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(retJson["ErrMsg"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
