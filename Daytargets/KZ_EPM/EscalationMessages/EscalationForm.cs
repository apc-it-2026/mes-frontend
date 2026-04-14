using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Collections.Generic;

namespace EscalationMessages
{
    public partial class EscalationForm : Form
    {
        public EscalationForm()
        {
            InitializeComponent();
            
        }
        // Here I created RequestPayload Class and Declare Properties, I can use this class any whare by creating Instance
        public class RequestPayload
        {
            public string Subject { get; set; }
            public string Body { get; set; }
            public string SendAll { get; set; }
            public string EmpNoPz { get; set; }
            public string OrgIdPz { get; set; }
            public string DeptNoPz { get; set; }
            public string OthersPz { get; set; }
            public List<string> UserList { get; set; }
        }

        public async Task<string> SendApiRequestAsync(string usertoken, string subject, string body, string sendAll, string empnopz, string orgidpz, string deptnopz, string otherspz, string Reciever)
        {
            try
            {
                //Create an object to hold your request parameters
                var requestPayload = new
                {
                    subject,
                    body,
                    sendAll,
                    empnopz,
                    orgidpz,
                    deptnopz,
                    otherspz,
                    userList = new[] { Reciever }
                };
                //List<string> receivers = new List<string> { Reciever };
                //RequestPayload requestPayload = new RequestPayload
                //{
                //    Subject = subject,
                //    Body = body,
                //    SendAll = sendAll,
                //    EmpNoPz = empnopz,
                //    OrgIdPz = orgidpz,
                //    DeptNoPz = deptnopz,
                //    OthersPz = otherspz,
                //    UserList = receivers
                //};

                // Serialize the object to JSON
                string jsonPayload = JsonSerializer.Serialize(requestPayload);

                // Create an HttpClient instance
                using (HttpClient client = new HttpClient())
                {
                    // Set the API endpoint URL//Replace with your actual API URL
                    string apiUrl = "https://apc.apachefootwear.com/Platform/message/EscalateAppMessgae"; 

                    // Add the usertoken to the HTTP headers
                    client.DefaultRequestHeaders.Add("Token", usertoken);

                    // Create a JSON content from the serialized payload
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    // Send a POST request to the API
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read and return the response content
                        string responseContent = await response.Content.ReadAsStringAsync();
                        return responseContent;
                    }
                    else
                    {
                        // Handle error responses here
                        return "API request failed: " + response.ReasonPhrase;
                    }
                }
            }
            catch (Exception e)
            {
                // Handle exceptions here
                return "Error: " + e.Message;
            }
        }

        private async void Requstclick_Click(object sender, EventArgs e)
        {
            string usertoken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiJRQ08wMDEiLCJuYmYiOjE3MTQ3MzA1OTEsImV4cCI6MTcxNTU5NDU5MSwiaWF0IjoxNzE0NzMwNTkxfQ.iIvC7DSlgckR5Gx1NOEqi4SNYAhGvhzH_WCUv8NRdEY".ToString();
            string subject   = txtsubject.Text;
            string body      = txtbody.Text;
            string sendAll   = "0";
            string empnopz   = "N";
            string orgidpz   = "N";
            string deptnopz  = "N";
            string otherspz  = "N";
            string Reciever  = userListtxt.Text;

            // Call SendApiRequestAsync with parameters
            string response = await SendApiRequestAsync(usertoken, subject, body, sendAll, empnopz, orgidpz, deptnopz, otherspz, Reciever);

            // Display the API response in the read-only textbox
            Responcetxt.Text = response;
        }

    }
}
