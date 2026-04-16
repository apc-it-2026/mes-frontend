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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TierAudits_New
{
    public partial class Plant_kaizen : Form
    {
        string Prodline = "";
        public Plant_kaizen()
        {
            InitializeComponent();
        }

        private void kaizen_Load(object sender, EventArgs e)
        {
           // LoadDept();
        }
        

        //private void GetDept()
        //{
        //    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetDept", Program.Client.UserToken, string.Empty);
        //    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
        //    {
        //        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
        //        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
        //        Prodline = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
        //        // d_dept_name = dtJson.Rows[0]["DEPARTMENT_NAME"].ToString();
        //        // orgId = dtJson.Rows[0]["ORG_ID"].ToString();
        //    }
        //    else
        //    {
        //        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
        //    }
        //}

        //private void LoadDept()
        //{
        //    try
        //    {
        //        GetDept();
        //    }
        //    catch (Exception ex)
        //    {
        //        SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
        //    }
        //    Plant.Text = Prodline;
        //    //labelDeptName.Text = d_dept_name;
        //}
        //private void button1_Click(object sender, EventArgs e)
        //{

        //    string Line = line.Text;  
        //    string kdate = kaizenTimePicker1.Text; 
        //    string kaizendtls = kaizendtl.Text;
        //    string bfrkzn = befrkzn.Text; 
        //    string afrkzn = aftrkzn.Text; 


        //    Dictionary<string, object> p = new Dictionary<string, object>();
        //    p.Add("Line", Line);
        //    p.Add("kdate", kdate);
        //    p.Add("kaizendtls", kaizendtls);
        //    p.Add("bfrkzn", bfrkzn);
        //    p.Add("afrkzn", afrkzn);


        //    string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "Insertkaizen", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
        //    var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
        //    if (Convert.ToBoolean(retJson["IsSuccess"]))
        //    {
        //        string json = retJson["RetData"].ToString();

        //        if (!string.IsNullOrEmpty(json))
        //        {
        //            MessageHelper.ShowSuccess(this, "Successfully Submitted！");
        //        }
        //        else
        //        {
        //            MessageHelper.ShowErr(this, "Failed to Submit！");
        //        }
        //    }
        //    else
        //    {
        //        MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            // Check the length again before processing the data
            string PlantValue = Plant.Text.ToUpper();

            // Check the length again before processing the data
            //string lineValue = line.Text;

            if (PlantValue.Length < 3)
            {
                // Display an error message and do not proceed with the operation
                MessageBox.Show("Plant length should be 3 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string kaizendtls = kaizendtl.Text;

            // Validate kaizendtls
            Regex regex = new Regex(@"^[A-Za-z]\d{10}$");

            if (!regex.IsMatch(kaizendtls))
            {
                // Display an error message and do not proceed with the operation
                MessageBox.Show("Please enter Correct KAIZEN No.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check the remaining TextBoxes for at least one word and not empty
            if (!IsTextBoxValid(befrkzn) ||
                !IsTextBoxValid(aftrkzn))
            {
                // Display an error message and do not proceed with the operation
                MessageBox.Show("Please fill all text boxes with at least one word and they should not be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Continue with the rest of your code...
            DateTime kaizenDate = kaizenTimePicker1.Value;
            string kdate = kaizenDate.ToString("yyyy/MM/dd");
            //sring kaizendtls = kaizendtl.Text;
            string bfrkzn = befrkzn.Text;
            string afrkzn = aftrkzn.Text;

            Dictionary<string, object> p = new Dictionary<string, object>
    {
        { "Plant", PlantValue },
        { "kdate", kdate },
        { "kaizendtls", kaizendtls },
        { "bfrkzn", bfrkzn },
        { "afrkzn", afrkzn }
    };

            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "PlantKaizen", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

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

        private bool IsTextBoxValid(TextBox textBox)
        {
            // Check if the TextBox contains at least one word and is not empty
            string value = textBox.Text.Trim();
            return !string.IsNullOrEmpty(value) && value.Contains(" ");
        }

       
    }
}
