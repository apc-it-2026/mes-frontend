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

namespace Aql_Inspection
{
    public partial class AQL_Inspection_All_POs : MaterialForm
    {
        public AQL_Inspection_All_POs()
        {
            InitializeComponent();
            LoadSOItem();
            LoadDestinationItem();
            LoadStatusItem();

        }



        private bool isFromDateSelected = false;
        private bool isToDateSelected = false;

        public void LoadSOItem()
        {
            string typedValue = SO_comboBox.Text;

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("SO", typedValue);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.Client.APIURL,
                "AQL_Walmart",
                "AQL_Wallmart.Controllers.AQL_Walmart_Controller",
                "LoadQueryItem",
                Program.Client.UserToken,
                Newtonsoft.Json.JsonConvert.SerializeObject(p)
            );

            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

            if (Convert.ToBoolean(response["IsSuccess"]))
            {
                string json = response["RetData"].ToString();
                DataTable dtJson = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

               
                if (dtJson.Rows.Count > 0)
                {
                    SO_comboBox.Items.Clear();

                    List<string> items = new List<string>();
                    foreach (DataRow row in dtJson.Rows)
                    {
                        items.Add(row["SO"].ToString());
                    }

                    SO_comboBox.Items.AddRange(items.ToArray());
                }

              
                SO_comboBox.Text = typedValue;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, response["ErrMsg"].ToString());
            }
        }

        public void LoadStatusItem()
        {
            string typedValue = Status_comboBox.Text;

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Status", typedValue);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.Client.APIURL,
                "AQL_Walmart",
                "AQL_Wallmart.Controllers.AQL_Walmart_Controller",
                "LoadStatusItem",
                Program.Client.UserToken,
                Newtonsoft.Json.JsonConvert.SerializeObject(p)
            );

            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

            if (Convert.ToBoolean(response["IsSuccess"]))
            {
                string json = response["RetData"].ToString();
                DataTable dtJson = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

             
                if (dtJson.Rows.Count > 0)
                {
                    //Status_comboBox.Items.Clear();
                    // List<string> Factorynames = new List<string> { "All" };

                    List<string> items = new List<string> { "All" };
                    foreach (DataRow row in dtJson.Rows)
                    {
                        items.Add(row["STATUS"].ToString());
                    }

                    Status_comboBox.Items.AddRange(items.ToArray());
                }


                ////////////////////

                // if (dtJson.Rows.Count > 0)
                //{
                //    List<string> Factorynames = new List<string> { "All" };

                //    foreach (DataRow row in dtJson.Rows)
                //    {
                //        Factorynames.Add(row["UDF05"].ToString());
                //    }
                //    PlantcomboBox.Items.AddRange(Factorynames.ToArray());
                //}




                // ALWAYS restore the manual text
                Status_comboBox.Text = typedValue;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, response["ErrMsg"].ToString());
            }
        }



        public void LoadDestinationItem()
        {
            string typedValue = Destination_comboBox.Text;

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Status", typedValue);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.Client.APIURL,
                "AQL_Walmart",
                "AQL_Wallmart.Controllers.AQL_Walmart_Controller",
                "LoadDestinationItem",
                Program.Client.UserToken,
                Newtonsoft.Json.JsonConvert.SerializeObject(p)
            );

            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

            if (Convert.ToBoolean(response["IsSuccess"]))
            {
                string json = response["RetData"].ToString();
                DataTable dtJson = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);


                if (dtJson.Rows.Count > 0)
                {
                    //Status_comboBox.Items.Clear();
                    // List<string> Factorynames = new List<string> { "All" };

                    List<string> items = new List<string> ();
                    foreach (DataRow row in dtJson.Rows)
                    {
                        items.Add(row["DESTINATION"].ToString());
                    }

                    Destination_comboBox.Items.AddRange(items.ToArray());
                }


                ////////////////////

                // if (dtJson.Rows.Count > 0)
                //{
                //    List<string> Factorynames = new List<string> { "All" };

                //    foreach (DataRow row in dtJson.Rows)
                //    {
                //        Factorynames.Add(row["UDF05"].ToString());
                //    }
                //    PlantcomboBox.Items.AddRange(Factorynames.ToArray());
                //}




                // ALWAYS restore the manual text
                Destination_comboBox.Text = typedValue;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, response["ErrMsg"].ToString());
            }
        }





        private void AQL_Inspection_All_POs_Load(object sender, EventArgs e)
        {

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = " ";

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = " ";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SO_label_Click(object sender, EventArgs e)
        {

        }

        private void Search_btn_Click(object sender, EventArgs e)
        {
            string fromDate = isFromDateSelected
    ? dateTimePicker1.Value.ToString("yyyy/MM/dd")
    : "";

            string toDate = isToDateSelected
                ? dateTimePicker2.Value.ToString("yyyy/MM/dd")
                : "";
           



            try
            {
                
                Dictionary<string, object> dict = new Dictionary<string, object>
        {
            { "SO", SO_comboBox.SelectedItem?.ToString() ?? "" },
            { "Status", Status_comboBox.SelectedItem?.ToString() ?? "" },
            { "Destination", Destination_comboBox.SelectedItem?.ToString() ?? "" },

            { "From_Date", fromDate },
            { "To_Date", toDate }


        };



              
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,                                        

               "AQL_Walmart",
               "AQL_Wallmart.Controllers.AQL_Walmart_Controller",        
                    "Search",                                                  
                    Program.Client.UserToken,                                     
                    Newtonsoft.Json.JsonConvert.SerializeObject(dict)            
                );

                
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

                string errmsg = result["ErrMsg"].ToString();

           
                if (Convert.ToBoolean(result["IsSuccess"]))
                {
                    string json = result["RetData"].ToString();                   // Extract data
                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable)); // Deserialize to DataTable

                    dataGridView1.DataSource = dt.DefaultView;                    // Bind data to DataGridView

                    // Optional UI formatting
                    dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                    dataGridView1.EnableHeadersVisualStyles = false;
                    //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    //dataGridView1.AutoResizeColumns();

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                }
                else
                {
                    if (errmsg == "No data found")
                    {
                        dataGridView1.DataSource = null;   // Clears data
                        dataGridView1.Rows.Clear();        // Ensures rows are removed
                        dataGridView1.Refresh();           // Optional: refresh UI
                       // ShowAutoClosePopup($"Overall total POs : 0 ", 3000);
                    }

                    MessageBox.Show(errmsg);
                }

            }
            catch (Exception ex)
            {
              
                MessageBox.Show("Error: " + ex.Message);
            }


            if (Status_comboBox.Text == "PASS")
            {
                int count = dataGridView1.Rows.Count;

                if (count == 0) {
                    ShowAutoClosePopup($"Overall total POs : 0 ", 3000);
                }

                else if (dataGridView1.AllowUserToAddRows)
                {
                    count -= 1;

                    ShowAutoClosePopup($"Total Passed PO's : {count}", 3000);
                }



            }
            else if (Status_comboBox.Text == "FAIL")
            {
                int count = dataGridView1.Rows.Count;
                if (count == 0)
                {
                    ShowAutoClosePopup($"Overall total POs : 0 ", 3000);
                }

                else if (dataGridView1.AllowUserToAddRows)
                    count -= 1;

                ShowAutoClosePopup($"Total Failed PO's : {count}", 3000);
            }
            else
            {
                int count = dataGridView1.Rows.Count;
                if (count == 0)
                {
                    ShowAutoClosePopup($"Overall total POs : 0 ", 3000);
                }

                else if (dataGridView1.AllowUserToAddRows)
                    count -= 1;

                ShowAutoClosePopup($"Overall total POs : {count} ", 3000);
            }




        }


        private async void ShowAutoClosePopup(string message, int durationMs)
        {
            Form popup = new Form();
            popup.FormBorderStyle = FormBorderStyle.None;
            popup.StartPosition = FormStartPosition.CenterScreen;
            popup.Width = 250;
            popup.Height = 40;
            popup.BackColor = Color.FromArgb(47, 79, 79);


            popup.TopMost = true;

            Label lbl = new Label();
            lbl.Text = message;
            lbl.ForeColor = Color.White;
            lbl.Font = new Font("Lucida Bright", 12, FontStyle.Bold);
            lbl.AutoSize = false;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Dock = DockStyle.Fill;

            popup.Controls.Add(lbl);
            popup.Show();

            await Task.Delay(durationMs);
            popup.Close();
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Destination_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            // Clear combobox selections
            SO_comboBox.SelectedIndex = -1;
            SO_comboBox.Text = "";
            Status_comboBox.SelectedIndex = -1;
            Status_comboBox.Text = "";
            Destination_comboBox.SelectedIndex = -1;
            Destination_comboBox.Text = "";

            // Reset DateTimePicker1
            isFromDateSelected = false;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
           // dateTimePicker1.CustomFormat = " ";   // make it empty

            // Reset DateTimePicker2
            isToDateSelected = false;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
           // dateTimePicker2.CustomFormat = " ";   // make it empty
        }



        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            isToDateSelected = true;
            dateTimePicker2.CustomFormat = "yyyy/MM/dd";


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            //isToDateSelected = true;
            //dateTimePicker2.CustomFormat = "yyyy/MM/dd";

            isFromDateSelected = true;
            dateTimePicker1.CustomFormat = "yyyy/MM/dd";

            // Set min date for dateTimePicker2
            dateTimePicker2.MinDate = dateTimePicker1.Value;

            // Optional: If previously selected date is invalid, reset it
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
        }
    }
}
