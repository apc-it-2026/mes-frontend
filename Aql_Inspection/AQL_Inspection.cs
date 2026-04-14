using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using ClosedXML.Excel;
using SJeMES_Framework.WebAPI;



namespace Aql_Inspection
{
    public partial class AQL_Inspection : MaterialForm
    {
        public AQL_Inspection()
        {
            InitializeComponent();
        }
        private bool isFromDateSelected = false;
        private bool isToDateSelected = false;
        private void AQL_Inspection_Load(object sender, EventArgs e)
        {

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = " ";

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = " ";
        }

        private void Upload_btn_Click(object sender, EventArgs e)
        {

           

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xls;*.xlsx;";

            if (ofd.ShowDialog() != DialogResult.OK) return;

            string filePath = ofd.FileName;
            string extension = Path.GetExtension(filePath);

            string conStr = extension == ".xls"
                ? @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + @";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'"
                : @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + @";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";

            try
            {
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    con.Open();
                    DataTable dtSheet = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    string sheetName = dtSheet.Rows[0]["TABLE_NAME"].ToString();
                    if (!sheetName.EndsWith("$")) sheetName += "$";

                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [" + sheetName + "]", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, "Excel Load Error: " + ex.Message);
                return;
            }

            


        }



        private void Check_btn_Click(object sender, EventArgs e)
        {
            AQL_Inspection_All_POs childForm = new AQL_Inspection_All_POs();

            // When child closes → show this form again
            childForm.FormClosed += (s, args) => this.Show();

            childForm.Show();
            this.Hide();
        }

       






        private void Submit_btn_Click(object sender, EventArgs e)
        {

        }


       





        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void template_btn_Click(object sender, EventArgs e)
        {


            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Save Excel Template";
                    saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = "SO_Template.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var ws = workbook.Worksheets.Add("Template");

                           
                            var header = ws.Cell(1, 1);
                            header.Value = "SO";

                           
                            header.Style.Fill.BackgroundColor = XLColor.LightGray;

                           
                            header.Style.Font.Bold = true;

                           
                            header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            header.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                           
                            for (int row = 1; row <= 10; row++)
                            {
                                var cell = ws.Cell(row, 1);
                                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                cell.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                            }

                            ws.Column(1).Width = 25;

                            workbook.SaveAs(saveFileDialog.FileName);
                        }

                        MessageBox.Show("Template downloaded successfully!",
                                        "Success",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


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
           

            { "From_Date", fromDate },
            { "To_Date", toDate }


        };



              
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,                                    

               "AQL_Walmart",
               "AQL_Wallmart.Controllers.AQL_Walmart_Controller",        
                    "GetSearch",                                                     
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

                    //// Optional UI formatting
                    dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                    dataGridView1.EnableHeadersVisualStyles = false;
                    //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    //dataGridView1.AutoResizeColumns();

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    //dataGridView1.DataSource = dt.DefaultView;

                    //// Set all columns to fill the grid
                    //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    //// Optional: Evenly distribute width across all 5 columns
                    //foreach (DataGridViewColumn col in dataGridView1.Columns)
                    //{
                    //    col.FillWeight = 1;   // Equal weight for all columns
                    //}


                }
                else
                {
                    MessageBox.Show(" " + errmsg);
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Submit_button_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Upload the file", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> soList = new List<string>();

            // Collect SO values
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["SO"].Value == null) continue;

                soList.Add(row.Cells["SO"].Value.ToString());
            }

            if (soList.Count == 0)
            {
                MessageBox.Show("No SO values found.");
                return;
            }

            // Prepare dictionary
            Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "SOList", soList }
        };

            // ------------------- INSERT API CALL -------------------
            string retInsert = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.Client.APIURL,
                "AQL_Walmart",
                "AQL_Wallmart.Controllers.AQL_Walmart_Controller",
                "InsertWalmartPOS",
                Program.Client.UserToken,
                JsonConvert.SerializeObject(data)
            );

            var resultInsert = JsonConvert.DeserializeObject<Dictionary<string, object>>(retInsert);

            if (Convert.ToBoolean(resultInsert["IsSuccess"]))
            {
                MessageBox.Show("All SO inserted successfully!");
            }
            else
            {
                MessageBox.Show("This SO " + resultInsert["ErrMsg"]);
            }

            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //isFromDateSelected = true;
            //dateTimePicker1.CustomFormat = "yyyy/MM/dd";

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

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            isToDateSelected = true;
            dateTimePicker2.CustomFormat = "yyyy/MM/dd";
        }
    }
}
