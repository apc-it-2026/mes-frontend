using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TierAudits_New
{
    public partial class smeFrm : Form
    {
        public smeFrm()
        {
            InitializeComponent();
        }

       
           

        private void smeSubmit_Click_1(object sender, EventArgs e)
        {

                // Convert the line text to upper case
                string smescore = sme_score.Text;

                // Check the length of the line value
                if (smescore.Length < 2)
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
                DateTime SmeDate = smeDate.Value;
                string Idate = SmeDate.ToString("yyyy/MM/dd");

                // Prepare the data for the API call
                Dictionary<string, object> p = new Dictionary<string, object>
    {
        { "smeScore", smescore },
        { "SmeDate", SmeDate },
        { "Plant_details", Plant_details }
    };

                // Make the API call
                // Make the API call
                string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "InsertSME", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

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


        //test for escalations
        public class WhatsAppService
        {
            private readonly string apiUrl = "http://10.3.0.70:9090/whatsapp/WhatsappApi/SendMessage";

            public async Task SendMessageAsync(string phoneNumber, string textMsg)
            {
                var payload = new
                {
                    numbers = new[] { phoneNumber }, // Use the fetched phone number
                    groups = new List<string>(),
                    textMsg = textMsg,
                    mediaurl = "",
                    filename = ""
                };

                var jsonPayload = JsonConvert.SerializeObject(payload);

                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        // Set the content type to application/json
                        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                        // Send the POST request
                        var response = await httpClient.PostAsync(apiUrl, content); // Ensure url is defined

                        // Optionally log the response or handle errors here, but do not return
                        if (!response.IsSuccessStatusCode)
                        {
                            var responseBody = await response.Content.ReadAsStringAsync();
                            // Log the failure if needed
                            Console.WriteLine($"Error: {response.StatusCode}, {responseBody}");
                        }
                        else
                        {
                            Console.WriteLine("Message sent successfully!");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (logging, etc.) but do not return
                        Console.WriteLine($"Exception: {ex.Message}");
                    }
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string phoneNumber = "7095947553"; // Replace with the actual phone number
            string textMsg = "Hi Manu"; // Replace with the actual message

            WhatsAppService whatsAppService = new WhatsAppService();
            await whatsAppService.SendMessageAsync(phoneNumber, textMsg);
        }

    }
}
