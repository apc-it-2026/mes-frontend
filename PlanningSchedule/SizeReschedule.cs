using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using SJeMES_Framework.Common; 

namespace PlanningSchedule
{
    public partial class SizeReschedule : Form
    {
        public SizeReschedule(object datasource) 
        {
            InitializeComponent(); 
            GetSizeWiseReschedule(datasource); 
        } 
        private void GetSizeWiseReschedule(object datasource )
        {
            try
            {
                var data = new Dictionary<string, object>
                {
                    ["datasource"] = datasource
                };
                string response = WebAPIHelper.Post(
                        Program.client.APIURL,
                        "KZ_CUTMNT",
                        "KZ_CUTMNT.Controllers.PlanningController",
                        "GetSizeWiseReschedule", 
                        Program.client.UserToken,
                        JsonConvert.SerializeObject(data)
                    );

                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
                if (result == null || !result.ContainsKey("IsSuccess"))
                {
                    MessageHelper.ShowErr(this, "Invalid or empty response from server.");
                    return;
                }

                bool isSuccess = Convert.ToBoolean(result["IsSuccess"]);
                if (isSuccess && result.ContainsKey("RetData") && !string.IsNullOrEmpty(result["RetData"]?.ToString()))
                {
                    string json = result["RetData"].ToString();
                    DataTable dt = JsonHelper.GetDataTableByJson(json);
                    dataGridView4.DataSource = dt;
                }
                else if (result.ContainsKey("ErrMsg") && !string.IsNullOrEmpty(result["ErrMsg"]?.ToString()))
                {
                    string apiErrorMessage = result["ErrMsg"].ToString();
                    MessageHelper.ShowErr(this, $"API Error: {apiErrorMessage}\nPlease contact the IT department.");
                }
                else
                {
                    dataGridView4.DataSource = null;
                    MessageHelper.ShowErr(this, "No data found matching the search criteria.");
                }
            } catch (Exception ex)
            {
                MessageHelper.ShowErr(this, $"An error occurred. Please contact IT Department.\nDetails: {ex.Message}"); 
            } 
        } 

    }
}
