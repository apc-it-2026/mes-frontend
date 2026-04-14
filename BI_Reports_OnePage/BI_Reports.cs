using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace BI_Reports_OnePage
{
    public partial class BI_Reports : Form
    {
        public BI_Reports()
        {
            InitializeComponent();


            //// Check for internet connectivity
            //if (!IsInternetAvailable())
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No internet connection available. Please Contact IT to get Internet Connection.");
            //    //this.Close();
            //    Environment.Exit(0);
            //}
            //else
            //{
            //    // Continue with form initialization
            //    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Success. Please Wait....");
            //    SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            //    this.WindowState = FormWindowState.Maximized;
            //}


           // SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Success. Please Wait....");


            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            this.WindowState = FormWindowState.Maximized;
        }

        private void BI_Reports_Load(object sender, EventArgs e)
        {
            
            InitBrowser();
            
        }
        private async Task Initiated()
        {
            await webView21.EnsureCoreWebView2Async(null);
        }

        public async void InitBrowser()
        {
            try
            {
                if (IsInternetAvailable())
                {
                    await Initiated();
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Success. Please Wait....");
                    webView21.CoreWebView2.Navigate("https://app.powerbi.com/view?r=eyJrIjoiYTI4MjdkMjMtMDM3OS00OTQ5LTg3MDAtZjhkNTZlYzA5ZDBmIiwidCI6ImM5YTIwNGM4LWI0MjQtNDRlYS05MDRjLWFhZGVkYzZiY2Q5YyIsImMiOjEwfQ%3D%3D&pageName=ReportSection37adce0576575507596a");
                }
                else
                {

                    CustomMessageBox cus = new CustomMessageBox();
                    cus.ShowDialog();
                    
                   // MessageBox.Show("No internet connection available. Please contact IT to get Internet Connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   //// this.Close();
                }

                    
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An Error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private bool IsInternetAvailable()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send("8.8.8.8");             // Use a reliable IP address for testing internet connectivity, such as Google's public DNS server (8.8.8.8)
                    return reply.Status == IPStatus.Success;

                }
            }
            catch (Exception)
            {
                return false;                                    // Error occurred, assume no internet connection
            }
        }





    }
}
