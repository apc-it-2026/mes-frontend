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
    public partial class PlantSixS : Form
    {
        public PlantSixS()
        {
            InitializeComponent();
        }

       
           

        private void smeSubmit_Click_1(object sender, EventArgs e)
        {

                // Convert the line text to upper case
                string sixSscore = sixS_score.Text;

                // Check the length of the line value
                if (sixSscore.Length < 2)
                {
                    // Display an error message and do not proceed with the operation
                    MessageBox.Show("seams invalid score", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the selected item from the issue_box ComboBox
                if (Plant.SelectedItem == null)
                {
                    MessageBox.Show("Please select Plant", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string Plant_details = Plant.SelectedItem.ToString();

                // Get the date from the end_date DateTimePicker and format it
                DateTime SDate = sixSDate.Value;
                string Idate = SDate.ToString("yyyy/MM/dd");

                // Prepare the data for the API call
                Dictionary<string, object> p = new Dictionary<string, object>
    {
        { "sixSscore", sixSscore }, 
        { "SDate", SDate },
        { "Plant_details", Plant_details }
    };

                // Make the API call
                // Make the API call
                string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "InsertSixS", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

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
