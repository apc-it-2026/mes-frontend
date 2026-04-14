using NewExportExcels;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaizenForm
{
    public partial class Kaizan_reports : Form
    {
        public Kaizan_reports()
        {
            InitializeComponent();
        }

        

        private void Button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("fromDate", dateTimePicker1.Text);
            p.Add("toDate", dateTimePicker2.Text);
            p.Add("Kaizen_number", textBox1.Text);
            p.Add("Kaizen_Type", comboBox1.Text);
            p.Add("Status", comboBox6.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Get_kaizen_reports", Program.client.UserToken, JsonConvert.SerializeObject(p));
            ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(ret);
            if (ret1.IsSuccess)
            {

                // Get JSON data as a string
                string jsonData = ret1.RetData;

                // Deserialize JSON array into a List of Dictionaries
                List<Dictionary<string, object>> listData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);

                // Convert to DataTable
                DataTable dtJson = ConvertToDataTable(listData);

                // Bind to DataGridView
                BindToGrid(dtJson);
               

                
            }
           
        
    }

        private DataTable ConvertToDataTable(List<Dictionary<string, object>> list)
        {
            DataTable dt = new DataTable();

            if (list == null || list.Count == 0)
                return dt;

            // Add columns to DataTable
            foreach (var key in list[0].Keys)
            {
                dt.Columns.Add(key);
            }

            // Add rows to DataTable
            foreach (var dict in list)
            {
                DataRow row = dt.NewRow();
                foreach (var kvp in dict)
                {
                    row[kvp.Key] = kvp.Value ?? DBNull.Value;
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        private void BindToGrid(DataTable dtJson)
        {
            if (dtJson.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtJson;

                // Set all cells to ReadOnly
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ReadOnly = true;
                    }
                }
            }
            else
            {
                dataGridView1.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {

            try
            {

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("Kaizen_number", textBox1.Text);
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver",
                    "Get_File",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
                var dataArray = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ret.RetData);

                if (dataArray != null && dataArray.Count > 0)
                {

                    foreach (var item in dataArray)
                    {
                        if (item.ContainsKey("FILE_URL") && item.ContainsKey("FILE_NAME"))
                        {
                            string fileUrl = item["FILE_URL"].ToString();
                            string FILE_NAME = item["FILE_NAME"].ToString();
                            //string filename = fileUrl.Substring(fileUrl.LastIndexOf('/') + 1);

                            ShowFileHelper.ShowFile(fileUrl, FILE_NAME);
                        }

                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Attachment Uploaded");
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("Kaizen_number", textBox1.Text);
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver",
                    "Get_images",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
                var dataArray = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ret.RetData);

                if (dataArray != null && dataArray.Count > 0)
                {


                    foreach (var item in dataArray)
                    {
                        if (item.ContainsKey("FILE_URL"))
                        {
                            try
                            {
                                string fileUrl = item["FILE_URL"].ToString();
                                var webC = new System.Net.WebClient();
                                string url = Program.client.PicUrl + fileUrl;
                                Image image = new Bitmap(webC.OpenRead(url));
                                Imageview sc = new Imageview(image);
                                sc.Show();
                            }
                            catch (Exception ex)
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, $"Error loading file: {ex.Message}");
                            }
                            
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, "No file_url key found.");
                        }
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Image Uploaded");
                }


                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {




            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "Get_Kaizen_form_details.xls";
                ExportExcels.Export(a, dataGridView1);
               // SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }

        private void Kaizan_reports_Load(object sender, EventArgs e)
        {
            LoadQueryItem();
        }



        public void LoadQueryItem()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();

            p.Add("Proposer_department", comboBox5.Text);

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                                         "KZ_RTDMAPI.Controllers.Kaizenserver",
                                         "GetAllDepts",
                 Program.client.UserToken,
                 Newtonsoft.Json.JsonConvert.SerializeObject(p)
             );



           // string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_OUTAPI", "KZ_OUTAPI.Controllers.Kaizenserver", "GetAllDepts", Program.client.UserToken, JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    
                    comboBox1.Items.Add(dtJson.Rows[i]["DEPARTMENT"].ToString());

                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }



        }


        


    }
}
