using AutocompleteMenuNS;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaizenForm
{
    public partial class KaizenForm : Form
    {
        string mergedFileName = string.Empty;
        string mergedFileName1= string.Empty;
        string mergedFileName2 = string.Empty;
        string fileName = string.Empty;
        public object Items { get; private set; }

        public KaizenForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox13_TextChanged(object sender, EventArgs e)
        {

        }
        private void Upload_imagebtn_Click(object sender, EventArgs e)
        {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Please enter the Kaizen ID in textBox2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                OpenFileDialog ofd = new OpenFileDialog
                {
                    Multiselect = true,
                    Title = "Please select image files",
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*"
                };

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string kaizenID = textBox2.Text.Trim(); 
                    List<Dictionary<string, object>> filediclist = new List<Dictionary<string, object>>();

                foreach (string file in ofd.FileNames)
                {
                    try
                    {
                        byte[] fileContent = File.ReadAllBytes(file);
                        string safeFileName = Path.GetFileName(file);
                        string filePath = file;
                        string fileExtension = Path.GetExtension(file);
                        mergedFileName = $"{kaizenID}_BeforeKaizen{fileExtension}";
                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.client.UploadUrl, filePath, Program.client.UserToken);
                        if (res.IsSuccess)
                        {
                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            string Descipline_Score_Card = resultDIC["guid"].ToString();
                            //    //var webC = new System.Net.WebClient();
                            //    //string url = Program.client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                            //    //Image image = new Bitmap(webC.OpenRead(url));
                            UploadDescipline_Score_Card(Descipline_Score_Card);
                        }
                        Dictionary<string, object> filedic = new Dictionary<string, object>
            {
                { "file_content", fileContent },
                { "file_name", mergedFileName }
            };

                        filediclist.Add(filedic);
                        using (MemoryStream ms = new MemoryStream(fileContent))
                        {
                            pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                        }
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        Console.WriteLine($"File processed: {mergedFileName}");
                    }
                    catch (Exception ex)
                     {
                        MessageBox.Show($"Error uploading file {Path.GetFileName(file)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //MessageBox.Show($"{filediclist.Count} files processed and renamed with Kaizen ID.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



           
        }

        private void TableLayoutPanel13_Paint(object sender, PaintEventArgs e)
        {

        }

        public void UploadDescipline_Score_Card(string file_guid)
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("Kaizen_number", textBox2.Text);
                //data.Add("ProdMonth", dateTimePicker2.Text);
                p.Add("file_guid", file_guid);
                p.Add("mergedFileName", mergedFileName);
                p.Add("mergedFileName1", mergedFileName1);
                p.Add("mergedFileName2", mergedFileName2);
                p.Add("fileName", fileName);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "InsertImages", Program.client.UserToken, JsonConvert.SerializeObject(p));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Uploaded successfully!");
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, j["ErrMsg"].ToString());
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.client, Program.client.WebServiceUrl, Program.client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter the Kaizen ID in textBox2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true,
                Title = "Please select image files",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string kaizenID = textBox2.Text.Trim();
                List<Dictionary<string, object>> filediclist = new List<Dictionary<string, object>>();

                foreach (string file in ofd.FileNames)
                {
                    try
                    {
                        byte[] fileContent = File.ReadAllBytes(file);
                        string safeFileName = Path.GetFileName(file);
                        string filePath = file;
                        string fileExtension = Path.GetExtension(file);
                        mergedFileName1 = $"{kaizenID}_AfterKaizen{fileExtension}";
                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.client.UploadUrl, filePath, Program.client.UserToken);
                        if (res.IsSuccess)
                        {
                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            string Descipline_Score_Card = resultDIC["guid"].ToString();
                            //    //var webC = new System.Net.WebClient();
                            //    //string url = Program.client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                            //    //Image image = new Bitmap(webC.OpenRead(url));
                            UploadDescipline_Score_Card(Descipline_Score_Card);
                        }
                        Dictionary<string, object> filedic = new Dictionary<string, object>
            {
                { "file_content", fileContent },
                { "file_name", mergedFileName1 }
            };

                        filediclist.Add(filedic);
                        using (MemoryStream ms = new MemoryStream(fileContent))
                        {
                            pictureBox2.Image = System.Drawing.Image.FromStream(ms);
                        }
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                        Console.WriteLine($"File processed: {mergedFileName}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error uploading file {Path.GetFileName(file)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Optional: Display selected file names (trimming the last comma)
               // MessageBox.Show("Files uploaded: " + selectedFiles.TrimEnd(','));
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter the Kaizen ID in textBox2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true,
                Title = "Please select image files",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string kaizenID = textBox2.Text.Trim();
                List<Dictionary<string, object>> filediclist = new List<Dictionary<string, object>>();

                foreach (string file in ofd.FileNames)
                {
                    try
                    {
                        byte[] fileContent = File.ReadAllBytes(file);
                        string safeFileName = Path.GetFileName(file);
                        string filePath = file;
                        string fileExtension = Path.GetExtension(file);
                        mergedFileName2 = $"{kaizenID}_AfterKaizen{fileExtension}";
                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.client.UploadUrl, filePath, Program.client.UserToken);
                        if (res.IsSuccess)
                        {
                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            string Descipline_Score_Card = resultDIC["guid"].ToString();
                            UploadDescipline_Score_Card(Descipline_Score_Card);
                        }
                        Dictionary<string, object> filedic = new Dictionary<string, object>
            {
                { "file_content", fileContent },
                { "file_name", mergedFileName2 }
            };

                        filediclist.Add(filedic);
                        using (MemoryStream ms = new MemoryStream(fileContent))
                        {
                            pictureBox2.Image = System.Drawing.Image.FromStream(ms);
                        }
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                        Console.WriteLine($"File processed: {mergedFileName}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error uploading file {Path.GetFileName(file)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel18_Paint(object sender, PaintEventArgs e)
        {







        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter the Kaizen ID in textBox2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true,
                Title = "Please select image files",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string kaizenID = textBox2.Text.Trim();
                List<Dictionary<string, object>> filediclist = new List<Dictionary<string, object>>();

                foreach (string file in ofd.FileNames)
                {
                    try
                    {
                        byte[] fileContent = File.ReadAllBytes(file);
                        string safeFileName = Path.GetFileName(file);
                        string filePath = file;
                        string fileExtension = Path.GetExtension(file);
                        mergedFileName2 = $"{kaizenID}_Proposer_pic{fileExtension}";
                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.client.UploadUrl, filePath, Program.client.UserToken);
                        if (res.IsSuccess)
                        {
                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            string Descipline_Score_Card = resultDIC["guid"].ToString();
                            //    //var webC = new System.Net.WebClient();
                            //    //string url = Program.client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                            //    //Image image = new Bitmap(webC.OpenRead(url));
                            UploadDescipline_Score_Card(Descipline_Score_Card);
                        }
                        Dictionary<string, object> filedic = new Dictionary<string, object>
            {
                { "file_content", fileContent },
                { "file_name", mergedFileName1 }
            };

                        filediclist.Add(filedic);
                        using (MemoryStream ms = new MemoryStream(fileContent))
                        {
                            pictureBox3.Image = System.Drawing.Image.FromStream(ms);
                        }
                        pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                        Console.WriteLine($"File processed: {mergedFileName}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error uploading file {Path.GetFileName(file)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Optional: Display selected file names (trimming the last comma)
                // MessageBox.Show("Files uploaded: " + selectedFiles.TrimEnd(','));
            }




        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        private void Kaizen_Form_Load(object sender, EventArgs e)
        {
            LoadQueryItem();
            LoadProd_Line();
        }


        public void LoadQueryItem()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            
            p.Add("Proposer_department", comboBox5.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "GetAllDepts", Program.client.UserToken, JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    comboBox5.Items.Add(dtJson.Rows[i]["DEPARTMENT"].ToString());
                    comboBox1.Items.Add(dtJson.Rows[i]["DEPARTMENT"].ToString());


                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }



        }

        private void TextBox12_TextChanged(object sender, EventArgs e)
        {
            
            double value11 = double.TryParse(textBox11.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox12.Text, out var result12) ? result12 : 0;

            // Perform subtraction and set the result to textBox16
            textBox16.Text = (value11 - value12).ToString();
            double value16 = double.TryParse(textBox12.Text, out var result16) ? result16 : 0;
            textBox18.Text = (value16 / value11).ToString();
            textBox26.Text = (value11 - value12).ToString();
            textBox14.Text = (3600 / value11).ToString();
            textBox15.Text = (3600 / value12).ToString();
        }

        public void LoadProd_Line()
        {
            textBox30.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox30.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> p = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "GetMESDepts", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                if (dtJson.Rows.Count > 0)
                {
                    autocompleteMenu1.MaximumSize = new Size(250, 350);
                    var columnWidth = new[] { 50, 200 };
                    int n = 1;
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dtJson.Rows[i]["department_code"].ToString() }, dtJson.Rows[i]["department_code"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }
                }
            }



          

          
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            //List<string> strList = new List<string>();
            p.Add("department", comboBox5.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "GetAllcode", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                foreach (DataRow dr in dtJson.Rows)
                {

                    if (dtJson.Rows.Count > 0)
                    {
                        string departmentCodes = string.Empty; // Initialize an empty string

                       
                            departmentCodes += dr["DEPARTMENT_CODE"].ToString() + "\n"; // Add each department code to the string, separated by new lines
                        

                        textBox3.Text = departmentCodes.TrimEnd('\n'); // Set the concatenated string as the text of the TextBox
                    }
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void TextBox15_TextChanged(object sender, EventArgs e)
        {


            double value11 = double.TryParse(textBox14.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox15.Text, out var result12) ? result12 : 0;

            // Perform subtraction and set the result to textBox16
            textBox19.Text = (value12 - value11).ToString();
            double value15 = double.TryParse(textBox15.Text, out var result16) ? result16 : 0;
            textBox17.Text = (value15 / value11).ToString();
           
        }

        private void TextBox22_TextChanged(object sender, EventArgs e)
        {

            double value11 = double.TryParse(textBox21.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox22.Text, out var result12) ? result12 : 0;

            // Perform subtraction and set the result to textBox16
            textBox13.Text = (value11 - value12).ToString();
            double value17 = double.TryParse(textBox22.Text, out var result16) ? result16 : 0;
            textBox20.Text = (value17 / value11).ToString();

        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            //List<string> strList = new List<string>();
            //p.Add("department", comboBox5.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "KaizenID", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {

                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                // Proceed with ID generation regardless of the row count
                string ID = string.Empty;
                int selectedIndex = comboBox4.SelectedIndex;
                if (selectedIndex == 0)
                {
                    ID = "A";
                }
                else
                {
                    ID = "B";
                }

               
                if (comboBox5.SelectedIndex == -1)  // ComboBox5 has no selection
                {
                    MessageBox.Show("Select Department");
                }
                else
                {
                    // Append text from textBox3 if ComboBox5 is selected
                    ID += textBox3.Text;
                    ID += DateTime.Now.ToString("yyMMdd");
                    int rowCount = dtJson.Rows.Count;
                    if(rowCount < 10)
                    {
                        ID += "0";
                    }
                    ID += (rowCount + 1).ToString();  
                    textBox2.Text = ID;

                }
            }

        }

        private void TextBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox25_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(textBox25.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox26.Text, out var result12) ? result12 : 0;
            textBox4.Text = ((value11*value12)/3600*49).ToString();
            if (decimal.TryParse(textBox4.Text, out decimal textBox4Value))
            {
             
                decimal percentageValue = textBox4Value * 3.5m / 100;
                textBox27.Text = percentageValue.ToString("F2");
                textBox10.Text= percentageValue.ToString("F2");
            }
        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter the Kaizen ID in textBox2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true,
                Title = "Select a File",
                Filter = "All Files (*.*)|*.*",
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string kaizenID = textBox2.Text.Trim();
                List<Dictionary<string, object>> filediclist = new List<Dictionary<string, object>>();

                foreach (string file in ofd.FileNames)
                {
                    try
                    {
                        byte[] fileContent = File.ReadAllBytes(file);
                        string safeFileName = Path.GetFileName(file);
                        string filePath = file;
                        string fileExtension = Path.GetExtension(file);
                        fileName = $"{kaizenID}_Attachment_file{fileExtension}";
                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.client.UploadUrl, filePath, Program.client.UserToken);
                        if (res.IsSuccess)
                        {
                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            string Descipline_Score_Card = resultDIC["guid"].ToString();
                            //    //var webC = new System.Net.WebClient();
                            //    //string url = Program.client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                            //    //Image image = new Bitmap(webC.OpenRead(url));
                            UploadDescipline_Score_Card(Descipline_Score_Card);
                            label49.Text = fileName;
                            
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error uploading file {Path.GetFileName(file)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }

                       
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {




        }




        private void Button7_Click(object sender, EventArgs e)
        {


            //string Before_Image = pictureBox1.Text;
            //string After_Image = pictureBox2.Text;

            //if (string.IsNullOrEmpty(Name))
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your Name");
            //    return;
            //}

            //if (string.IsNullOrEmpty(Before_Image))
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select Before image");
            //    return;
            //}
            //if (string.IsNullOrEmpty(After_Image))
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select after image");
            //    return;
            //}
            Dictionary<string, object> p = new Dictionary<string, object>();

            p.Add("KaizenHeading", textBox1.Text);
            p.Add("Kaizen_number", textBox2.Text);
            p.Add("Kaizen_Type", comboBox4.Text);
            p.Add("Proposer_Department", comboBox5.Text);
            p.Add("Dept_Code", textBox3.Text);
            p.Add("Type(ECRS)", comboBox2.Text);
            p.Add("CW_Barcode", textBox6.Text);
            p.Add("CW_Name", textBox5.Text);
            p.Add("Projected_Department", comboBox1.Text);
            p.Add("Kaizen_Date", dateTimePicker1.Text);
            p.Add("Projected_Area", comboBox3.Text);
            p.Add("Projected_Line", textBox30.Text);
            p.Add("Status", comboBox6.Text);
            p.Add("Before_Image", pictureBox1.Text);
            p.Add("After_Image", pictureBox2.Text);
            p.Add("Proposer_Pic", pictureBox3.Text);
            p.Add("Proposer_Barcode", textBox8.Text);
            p.Add("Proposer_Name", textBox7.Text);
            p.Add("Proposer_Designation", textBox9.Text);
            p.Add("Bonus", textBox10.Text);
            p.Add("CT_Before", textBox11.Text);
            p.Add("CT_After", textBox12.Text);
            p.Add("CT_Savings", textBox16.Text);
            p.Add("CT_Improved", textBox18.Text);
            p.Add("Output_Before", textBox14.Text);
            p.Add("Output_After", textBox15.Text);
            p.Add("Output_Saved", textBox19.Text);
            p.Add("Output_Improve", textBox17.Text);
            p.Add("Manpower_Before", textBox21.Text);
            p.Add("Manpower_After", textBox22.Text);
            p.Add("Manpower_Saved", textBox13.Text);
            p.Add("Manpower_Improved", textBox20.Text);
            p.Add("Monthly_Order_Quantity", textBox25.Text);
            p.Add("Overall_CT_Savings", textBox26.Text);
            p.Add("Overall_Savings", textBox4.Text);
            p.Add("Bonus_Evalution", textBox27.Text);
            p.Add("Attachments", label49.Text);
            p.Add("Before_Kaizen", textBox23.Text);
            p.Add("After_Kaizen", textBox24.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_save_form", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))

            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                if (json == "Failed")
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data");
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Data inserted Successfully");
                }
                clear();
            }
        }

        private void clear()
        {
            textBox2.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox10.Text = string.Empty;
            textBox11.Text = string.Empty;
            textBox12.Text = string.Empty;
            textBox16.Text = string.Empty;
            textBox18.Text = string.Empty;
            textBox14.Text = string.Empty;
            textBox15.Text = string.Empty;
            textBox19.Text = string.Empty;
            textBox17.Text = string.Empty;
            textBox21.Text = string.Empty;
            textBox22.Text = string.Empty;
            textBox13.Text = string.Empty;
            textBox20.Text = string.Empty;
            textBox25.Text = string.Empty;
            textBox26.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox27.Text = string.Empty;
            textBox23.Text = string.Empty;
            textBox24.Text = string.Empty;

            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            comboBox5.Text = string.Empty;
            comboBox6.Text = string.Empty;

            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;


            label49.Text = string.Empty;
        }

        private void TextBox24_TextChanged(object sender, EventArgs e)
        {

        }
    }


   
}
    


