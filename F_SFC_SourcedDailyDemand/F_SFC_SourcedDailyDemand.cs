using MaterialSkin.Controls;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DataTotalRow;
using NewExportExcels;

namespace F_SFC_SourcedDailyDemand
{
    public partial class F_SFC_SourcedDailyDemand : MaterialForm
    {
        public DataTable excelDataTable = null;
        public F_SFC_SourcedDailyDemand()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            this.dataGridView2.AutoGenerateColumns = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView3.AutoGenerateColumns = false;
        }
        private static DataTable ReadExcelToTable(string path)
        {
            try
            {
                // Connection string (Office 07 and above can not have extra spaces and semicolons)
                string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
                // Connection string (for versions below Office 07, basically the connection string above is fine) 
                //string connstring = Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";

                using (OleDbConnection conn = new OleDbConnection(connstring))
                {
                    conn.Open();
                    // Get the names of all sheets
                    DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
                    // Get the name of the first sheet
                    string firstSheetName = sheetsName.Rows[0][2].ToString();

                    // query string 
                    string sql = string.Format("SELECT * FROM [{0}]", firstSheetName);

                    // OleDbDataAdapter acts as a bridge between the DataSet and the data source for retrieving and saving data
                    OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);

                    // A DataSet is an independent collection of data that does not depend on a database
                    DataSet set = new DataSet();

                    // Use Fill to load data from a data source into a DataSet
                    ada.Fill(set);

                    return set.Tables[0];
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           // dataGridView1.AutoGenerateColumns = false;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files|*.xls;*.xlsx";              // Set open file types           
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);  // Open the desktop

            // If file is selected
            string filePath = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get file path and file name
                filePath = openFileDialog.FileName;
                label3.Text = filePath;
                dataGridView1.DataSource = null;                       // Clear content every time you open
                excelDataTable = null;
                dataGridView1.AllowUserToAddRows = false;               
                this.excelDataTable= ReadExcelToTable(filePath); //read out excel and put into datatable
                //Set datagridview header         
                DataRow headrow = excelDataTable.Rows[0];                
                dataGridView1.DataSource = this.excelDataTable;        // , output to dataGridView
                for (int i = 0; i < excelDataTable.Columns.Count; i++)
                {
                    dataGridView1.Columns["F" + (i + 1) + ""].HeaderText = headrow[i].ToString();
                };
                excelDataTable.Rows[0].Delete();// delete the first row header
            }
        }
      
    

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1!=null)
            {
                dataGridView1.DataSource=null;
            }
            if (excelDataTable != null)
            {
                excelDataTable = null;
            }            
            label3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (excelDataTable == null || dataGridView1 == null)
            {
                MessageBox.Show("please import file。");
                return;
            }
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select a file type。");
                return;
            }
            string textt =string.Format($"you sure（{comboBox1.Text}）do you？");
            DialogResult dialog = MessageBox.Show(textt, "hint", MessageBoxButtons.YesNo,MessageBoxIcon.Information );
            if (dialog == DialogResult.Yes)
            {
                DataTable dt = GetData();
                //MessageBox.Show("en");
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("categories", comboBox1.Text.Split('|')[0]);
                p.Add("excelDataTable", dt);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_JMSAPI", "KZ_JMSAPI.Controllers.JMS_SOURCEDDAILYDEMAND_LISTServer", "UploadData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    MessageBox.Show("Import successful！！", "hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //SJeMES_Framework.Common.UIHelper.UImsg("导入成功！", Program.client, "", Program.client.Language); 
                    //SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }           
        }
        public DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ORDER_MONTH", typeof(DateTime));//order date
            dt.Columns.Add("VEND_NO", typeof(string));  //Manufacturer
            dt.Columns.Add("ART_NAME", typeof(string)); //Shoe type
            dt.Columns.Add("PRODUCT_NO", typeof(string)); //ART/model number             
            dt.Columns.Add("REQUIREDATE", typeof(DateTime));//demand date
            dt.Columns.Add("QTY", typeof(decimal));//quantity of order
            dt.Columns.Add("REMARK", typeof(string));//Remark
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                if (dr.Cells[0].Value.ToString() == "")
                {
                    continue;
                }
                DataRow r = dt.NewRow();
                r["ORDER_MONTH"] = DateTime.Parse(dr.Cells[0].Value.ToString()).ToShortDateString();
                r["VEND_NO"] = (dr.Cells[1].Value).ToString().Replace(" ","");
                r["ART_NAME"] = dr.Cells[2].Value;
                r["PRODUCT_NO"] = (dr.Cells[3].Value).ToString().Replace(" ", "");
                r["REQUIREDATE"] = DateTime.Parse(dr.Cells[4].Value.ToString()).ToShortDateString();
                r["QTY"] = dr.Cells[5].Value;
                r["REMARK"] = dr.Cells[6].Value;
               
                dt.Rows.Add(r);
            }
            return dt;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = Application.StartupPath + @"\JMSReport" + "\\委外需求计划模板.xlsx";
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = "委外每日需求计划模板.xlsx";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileName = saveDialog.FileName;
                File.Copy(path, saveFileName, true);
                System.Diagnostics.Process.Start(saveFileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string oderdate1 = "";
            string oderdate2 = "";
            string requiredate1 = "";
            string requiredate2 = "";
            if (comboBox2.Text == "")
            {
                MessageBox.Show("Please enter query category！！", "hint", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            if (checkBox1.Checked)
            {
                oderdate1 = dateTimePicker1.Value.ToString();
                oderdate2 = dateTimePicker2.Value.ToString();
            }
            if (checkBox2.Checked)
            {
                requiredate1 = dateTimePicker3.Value.ToString();
                requiredate2 = dateTimePicker4.Value.ToString();
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("categories", comboBox2.Text.Split('|')[0]);//category
            p.Add("VEND_NO", textBox3.Text);//Manufacturer code
            p.Add("MODEL_NO", textBox2.Text);//model number
            p.Add("ART_NO", textBox1.Text);//ART 
            //order date
            p.Add("oderdate1", oderdate1);
            p.Add("oderdate2", oderdate2);
            //demand date
            p.Add("requiredate1", requiredate1);
            p.Add("requiredate2", requiredate2);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_JMSAPI", "KZ_JMSAPI.Controllers.JMS_SOURCEDDAILYDEMAND_LISTServer", "GetPlanData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {   //Generate "total" row
                    DataRow TotaldgvRow1 = WinFormLib.TotalRow.GetTotalRow(dtJson);        //return DataRow
                    //TotaldgvRow1[dtJson.Columns["REQUIREDATE"]] = string.Empty;
                    //TotaldgvRow1[dtJson.Columns["SE_ID"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["VEND_NO"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["VEND_Name"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["ART_NAME"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["MODEL_NO"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["ORDER_MONTH"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["REMARK"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["REQUIREDATE"]] = "total:";
                    dtJson.Rows.Add(TotaldgvRow1);
                }
                dataGridView2.DataSource = dtJson;
                if (comboBox2.Text.Split('|')[0] == "1")
                {
                    dataGridView2.Columns["Column4"].Visible = false;
                    dataGridView2.Columns["Column5"].Visible = true;
                }
                else if(comboBox2.Text.Split('|')[0] == "2")
                {
                    dataGridView2.Columns["Column4"].Visible = true;
                    dataGridView2.Columns["Column5"].Visible = false;                    
                }                                
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
       

        private void button5_Click_1(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(dateTimePicker5.Value.ToString()) || string.IsNullOrEmpty(dateTimePicker6.Value.ToString()))
            {
                MessageBox.Show("Please enter time！");
                return;
            }
            if (comboBox3.Text=="")
            {
                MessageBox.Show("Please select a data category！");
                return;
            }
             // vamp
             //0000030050 Fengtai
             //0000030003 Zhonglian
             //0000030017 Xingsheng
             //background
             //0000030028 Shang You
             //0000030038 Liangshuo
             //0000030033 Yusheng Xincheng
             //0000030063 Jin Hong
            Dictionary<string, object> p = new Dictionary<string, object>();
            string A = "'0000030038','0000030033','0000030028','0000030063'";
            string B = "'0000030050','0000030003','0000030017'";
            p.Add("A", A);//background manufacturer    
            p.Add("B", B);//upper manufacturer
            p.Add("VEND_Name", textBox5.Text);//Trade Names           
            p.Add("art_no", textBox7.Text);//art_no    
            p.Add("model_no", textBox8.Text);//model number  
            p.Add("art_name", textBox9.Text);//shoe name
            p.Add("datefront", dateTimePicker5.Value);
            p.Add("datelast", dateTimePicker6.Value);
            p.Add("ty",comboBox3.Text.Split('|')[0]);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_JMSAPI", "KZ_JMSAPI.Controllers.JMS_SOURCEDDAILYDEMAND_LISTServer", "GetAllData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {   //Generate "total" row
                    DataRow TotaldgvRow1 = WinFormLib.TotalRow.GetTotalRow(dtJson);        //return DataRow
                    TotaldgvRow1[dtJson.Columns["review_date"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["art_name"]] = string.Empty;
                    //TotaldgvRow1[dtJson.Columns["art_no"]] = string.Empty;
                    //TotaldgvRow1[dtJson.Columns["mold_no"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["vend_code"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["vend_name"]] = "total:";
                    TotaldgvRow1[dtJson.Columns["art_name"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["BCS"]] = string.Empty;
                    dtJson.Rows.Add(TotaldgvRow1);
                }              
                if (comboBox3.Text.Split('|')[0] == "1")
                {
                    dataGridView4.Visible = false;
                    dataGridView3.Visible = true;
                    dataGridView3.DataSource = dtJson.DefaultView;
                    dataGridView3.Update();
                    dataGridView3.DataSource = dtJson;
                }
                else if (comboBox3.Text.Split('|')[0] == "2")
                {
                    dataGridView3.Visible = false;
                    dataGridView4.Visible = true;
                    dataGridView4.DataSource = dtJson.DefaultView;
                    dataGridView4.Update();
                    dataGridView4.DataSource = dtJson;
                }
                

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (comboBox3.Text.Split('|')[0] == "1")
            {
                ExportExcels.Export("本底BCS数据", dataGridView3);
            }
            else if (comboBox3.Text.Split('|')[0] == "2")
            {
                ExportExcels.Export("鞋面BCS数据", dataGridView4);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {      
            ExportExcels.Export("委外每日目标量", dataGridView2);
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text.Split('|')[0] == "1")
            {
                textBox7.Enabled = false;
                textBox8.Enabled = true;
            }
            else if(comboBox3.Text.Split('|')[0] == "2")
            {
                textBox7.Enabled = true;
                textBox8.Enabled = false;
            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text.Split('|')[0] == "1")
            {
                textBox1.Enabled = false;
                textBox2.Enabled = true;
            }
            else if (comboBox2.Text.Split('|')[0] == "2")
            {
                textBox1.Enabled = true;
                textBox2.Enabled = false;
            }
        }

        
    }
}
