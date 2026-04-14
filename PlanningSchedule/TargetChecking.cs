using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SJeMES_Framework.WebAPI;
using Newtonsoft.Json;
using SJeMES_Framework.Common;
using SJeMES_Control_Library;
using Oracle.ManagedDataAccess.Client;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.IO;
using OfficeOpenXml.Style;
using NewExportExcels;
using Microsoft.Office.Interop.Excel;
using LicenseContext = OfficeOpenXml.LicenseContext;
using NPOI.SS.UserModel;
using System.Net.Sockets;
using System.Net;

namespace PlanningSchedule
{
    public partial class TargetChecking : Form
    {
        public TargetChecking()
        {
            InitializeComponent();
            textBox3.CharacterCasing = CharacterCasing.Upper;
        }
        private void GEtDaywiseTargets()
        {
            try
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show("Please Enter Line", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Dictionary<string, object> Data = new Dictionary<string, object>
        {
            { "line", textBox3.Text }
        };

                string retdata = WebAPIHelper.Post(
                    Program.client.APIURL, 
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.PlanningController",
                    "LineDayWiseTarget",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(Data)
                );

                // Deserialize into dictionary
                var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (response.ContainsKey("RetData"))
                {
                    // Convert Result into DataTable
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(response["RetData"].ToString()); 
                    dataGridView4.DataSource = dt;
                }
                else 
                { 
                    MessageBox.Show("No data found in response.");
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            GEtDaywiseTargets(); 
        } 

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
