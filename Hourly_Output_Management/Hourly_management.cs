using AutocompleteMenuNS;
using NewExportExcels;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Hourly_Output_Management
{
    public partial class Hourly_management : Form
    {
        DataTable dtJson;

        public Hourly_management()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            this.WindowState = FormWindowState.Maximized;
        }

        private void Hourly_management_Load(object sender, EventArgs e)
        {
            //autocompleteMenu1.Items = null;
            //autocompleteMenu1.MaximumSize = new System.Drawing.Size(350, 350);

            tabControl1.TabPages.Remove(tabPage2);
             tabControl1.TabPages.Remove(tabPage3);

            this.Txt_Formdate.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);

            LoadOrgId();
            LoadPlant();
            LoadRoutNo();

           //Load_Hourly_Output_Query();

            string refreshtimer = string.Empty;

            LoadDepts();

            //if (string.IsNullOrWhiteSpace(txt_timer.Text))
            //{
            //    txt_timer.Text = "300";
            //    refreshtimer = txt_timer.Text;
            //}
            //timer1.Interval = int.Parse(refreshtimer.ToString()) * 1000;
            ////timer1.Interval = int.Parse(txt_timer.Text.ToString()) * 1000;
            //timer1.Enabled = true;
            //timer1.Tick += new EventHandler(timer1_Tick);//Add event



        }


   
        private void LoadDepts()
        {
            var columnWidth = new int[] { 30, 250 };
            DataTable dt = GetDepts();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"].ToString() + "   " + dt.Rows[i]["DEPARTMENT_NAME"].ToString() }, dt.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }

        private DataTable GetDepts()
        {
            DataTable dt = null;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetAllDepts", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }
 


        private void LoadRoutNo()
        {
            var items4 = new List<AutocompleteItem>();
            string ret4 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.MES_Hourly_Management_Server", "LoadRoutNo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items4.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items4.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["rout_no"].ToString(), dtJson.Rows[i - 1]["rout_name_z"].ToString() }, dtJson.Rows[i - 1]["rout_no"].ToString() + "|" + dtJson.Rows[i - 1]["rout_name_z"].ToString()));
                }
            }
            cb_process.DataSource = items4;
            combobox_process.DataSource = items4;
            comboBox1.DataSource = items4;
        }

        private void Btn_search_Click(object sender, EventArgs e)
        {
            timer1_Tick(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            Load_Hourly_Output_Query();


        }

        public void Load_Hourly_Output_Query()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.AutoGenerateColumns = false;
            try
            {
                GetHourlyProductioninputData();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void GetHourlyProductioninputData()
        {

            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("Date", dateTimePicker1.Text);
            p.Add("vCompany", cb_org.SelectedValue.ToString());
            //p.Add("vCompany", cb_org.Text);
            p.Add("vPlant", string.IsNullOrWhiteSpace(cb_plant.Text) ? cb_plant.Text : cb_plant.Text.Split('|')[0]);
            p.Add("vProcess", string.IsNullOrWhiteSpace(cb_process.Text) ? cb_process.Text : cb_process.Text.Split('|')[0]);
            p.Add("vDept", txt_line.Text);

            Cursor.Current = Cursors.WaitCursor;

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.MES_Hourly_Management_Server", "GetHourlyProductioninputData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                Cursor.Current = Cursors.Default;
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                decimal sum1 = 0;
                // int sum2 = 0;
                decimal sum2 = 0;
                int sum3 = 0;
                int sum4 = 0;
                int sum5 = 0;
                int sum6 = 0;
                decimal sum7 = 0;

                int first_hour_sum = 0;
                int second_hour_sum = 0;
                int third_hour_sum = 0;
                int fourth_hour_sum = 0;
                int fifth_hour_sum = 0;
                int sixth_hour_sum = 0;
                int seventh_hour_sum = 0;
                int eight_hour_sum = 0;
                int nine_hour_sum = 0;
                int ten_hour_sum = 0;
                int eleven_hour_sum = 0;
                int twelve_hour_sum = 0;
                int thirteen_hour_sum = 0;


                decimal? avg8 = 0;

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {

                    decimal target_qty = string.IsNullOrEmpty(dtJson.Rows[i]["target"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["target"].ToString());
                    decimal work_hours = string.IsNullOrEmpty(dtJson.Rows[i]["WORKHOURS"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["WORKHOURS"].ToString());
                  //  int work_hours = string.IsNullOrEmpty(dtJson.Rows[i]["WORKHOURS"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["WORKHOURS"].ToString());
                    int manpower_qty = string.IsNullOrEmpty(dtJson.Rows[i]["MANPOWER"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["MANPOWER"].ToString());
                    int First_half_qty = string.IsNullOrEmpty(dtJson.Rows[i]["TOTAL1"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["TOTAL1"].ToString());
                    int second_half_qty = string.IsNullOrEmpty(dtJson.Rows[i]["TOTAL2"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["TOTAL2"].ToString());
                    int balance_qty = string.IsNullOrEmpty(dtJson.Rows[i]["BALANCE"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["BALANCE"].ToString());
                    decimal Overall_Qty = string.IsNullOrEmpty(dtJson.Rows[i]["TOTAL"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["TOTAL"].ToString());

                    int firsthoursum = string.IsNullOrEmpty(dtJson.Rows[i]["H07"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["H07"].ToString());
                    int secondhoursum = string.IsNullOrEmpty(dtJson.Rows[i]["H08"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["H08"].ToString());
                    int thirdhoursum = string.IsNullOrEmpty(dtJson.Rows[i]["H09"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["H09"].ToString());
                    int fourthhoursum = string.IsNullOrEmpty(dtJson.Rows[i]["H10"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["H10"].ToString());
                    int fifthhoursum = string.IsNullOrEmpty(dtJson.Rows[i]["H11"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["H11"].ToString());
                    int sixthhoursum = string.IsNullOrEmpty(dtJson.Rows[i]["H12"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["H12"].ToString());
                    int seventhhoursum = string.IsNullOrEmpty(dtJson.Rows[i]["H13"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["H13"].ToString());
                    int eighthoursum = string.IsNullOrEmpty(dtJson.Rows[i]["H14"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["H14"].ToString());
                    int ninehoursum = string.IsNullOrEmpty(dtJson.Rows[i]["H15"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["H15"].ToString());
                    int tenhoursum = string.IsNullOrEmpty(dtJson.Rows[i]["H16"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["H16"].ToString());
                    int Elevenhoursum = string.IsNullOrEmpty(dtJson.Rows[i]["H17"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["H17"].ToString());
                    int twelvehoursum = string.IsNullOrEmpty(dtJson.Rows[i]["H18"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["H18"].ToString());
                    int thirteenhoursum = string.IsNullOrEmpty(dtJson.Rows[i]["H19"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["H19"].ToString());

                    sum1 += target_qty;
                    sum2 += work_hours;
                    //sum2 += work_hours;
                    sum3 += manpower_qty;
                    sum4 += First_half_qty;
                    sum5 += second_half_qty;
                    sum6 += balance_qty;
                    sum7 += Overall_Qty;

                    first_hour_sum += firsthoursum;
                    second_hour_sum += secondhoursum;
                    third_hour_sum += thirdhoursum;
                    fourth_hour_sum += fourthhoursum;
                    fifth_hour_sum += fifthhoursum;
                    sixth_hour_sum += sixthhoursum;
                    seventh_hour_sum += seventhhoursum;
                    eight_hour_sum += eighthoursum;
                    nine_hour_sum += ninehoursum;
                    ten_hour_sum += tenhoursum;
                    eleven_hour_sum += Elevenhoursum;
                    twelve_hour_sum += twelvehoursum;
                    thirteen_hour_sum += thirteenhoursum;

                }
                decimal target_qty11 = sum1;
                decimal total_qty11 = sum7;
                

                if (total_qty11 == 0 && target_qty11 == 0 || target_qty11 == 0)
                    avg8 = 0 ;
                else
                {
                    decimal overall_avg = total_qty11 / target_qty11;
                    avg8 = decimal.Round(overall_avg * 100, 2);
                }
                    
               

                string avg9 = avg8 + "%";

                DataRow row = dtJson.NewRow();
                row["target"] = sum1;
                row["WORKHOURS"] = sum2;
                row["MANPOWER"] = sum3;
                row["TOTAL1"] = sum4;
                row["TOTAL2"] = sum5;
                row["BALANCE"] = sum6;
                row["TOTAL"] = sum7;
                row["ACHEIVEMENT"] = avg9;            //ACHEIVEMENT  


                row["H07"] = first_hour_sum;
                row["H08"] = second_hour_sum;
                row["H09"] = third_hour_sum;
                row["H10"] = fourth_hour_sum;
                row["H11"] = fifth_hour_sum;
                row["H12"] = sixth_hour_sum;
                row["H13"] = seventh_hour_sum;
                row["H14"] = eight_hour_sum;
                row["H15"] = nine_hour_sum;
                row["H16"] = ten_hour_sum;
                row["H17"] = eleven_hour_sum;
                row["H18"] = twelve_hour_sum;
                row["H19"] = thirteen_hour_sum;


                dtJson.Rows.Add(row);

                dataGridView1.DataSource = dtJson.DefaultView;
                dataGridView1.Update();



                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    decimal acheivementQty = 0;
                    string target_qty = string.IsNullOrEmpty(dtJson.Rows[i]["Target"].ToString()) ? "0" : dtJson.Rows[i]["Target"].ToString();
                    string total_qty = string.IsNullOrEmpty(dtJson.Rows[i]["total"].ToString()) ? "0" : dtJson.Rows[i]["total"].ToString();
                    decimal result;
                    if (!target_qty.Equals("0") && decimal.TryParse(target_qty, out result) && decimal.TryParse(total_qty, out result))
                    {
                        acheivementQty = decimal.Parse(total_qty) / decimal.Parse(target_qty) * 100;
                        if (acheivementQty >= 100)
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(44, 250, 7);
                        }
                        else if (acheivementQty >= 85 && acheivementQty < 100)
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;

                        }
                        else if (acheivementQty < 85)
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(244, 157, 172);
                           // dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(245, 183, 177);
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 192);
                            //dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            //dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                            //dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Azure;
                    }

                }




                //dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView1_RowPostPaint);
            }
            else
            {
                Cursor.Current = Cursors.Default;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        #region dummy Code
        //private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        //{


        //    for (int i = 0; i < dtJson.Rows.Count; i++)
        //    {
        //        decimal acheivementQty = 0;
        //        string target_qty = string.IsNullOrEmpty(dtJson.Rows[i]["Target"].ToString()) ? "0" : dtJson.Rows[i]["Target"].ToString();
        //        string total_qty = string.IsNullOrEmpty(dtJson.Rows[i]["total"].ToString()) ? "0" : dtJson.Rows[i]["total"].ToString();
        //        decimal result;
        //        if (!target_qty.Equals("0") && decimal.TryParse(target_qty, out result) && decimal.TryParse(total_qty, out result))
        //        {
        //            acheivementQty = decimal.Parse(total_qty) / decimal.Parse(target_qty) * 100;
        //            if (acheivementQty >= 100)
        //            {
        //                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(44, 250, 7);
        //            }
        //            else if (acheivementQty >= 85 && acheivementQty < 100)
        //            {
        //                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;

        //            }
        //            else if (acheivementQty < 85)
        //            {
        //                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 182, 193);
        //                //dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(247, 143, 147);
        //            }
        //        }
        //        else
        //        {
        //            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LavenderBlush;
        //        }

        //    }




        //}
        #endregion


        private void Btn_clear_Click(object sender, EventArgs e)
        {
            cb_org.Text = "";
            cb_plant.Text = "";
            txt_line.Text = "";
            cb_process.Text = "";
        }

        public class ComboBoxData
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        private void LoadOrgId()
        {
            List<ComboBoxData> WMSorgEntries = new List<ComboBoxData> { };
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.MES_Hourly_Management_Server", "LoadOrgId", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                WMSorgEntries.Add(new ComboBoxData() { Code = "", Name = "" });
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    WMSorgEntries.Add(new ComboBoxData() { Code = dtJson.Rows[i]["ORG_CODE"].ToString(), Name = dtJson.Rows[i]["ORG_NAME"].ToString() });
                }

                cb_org.DataSource = WMSorgEntries;
                cb_org.DisplayMember = "Name";
                cb_org.ValueMember = "Code";

                cb_factory.DataSource = WMSorgEntries;
                cb_factory.DisplayMember = "Name";
                cb_factory.ValueMember = "Code";

                comboBox2.DataSource = WMSorgEntries;
                comboBox2.DisplayMember = "Name";
                comboBox2.ValueMember = "Code";


            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }



        private void LoadPlant()
        {
            var items1 = new List<AutocompleteItem>();

            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.MES_Hourly_Management_Server", "LoadPlant", Program.Client.UserToken, string.Empty);


            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items1.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["CODE"].ToString() }, dtJson.Rows[i - 1]["CODE"].ToString()));
                }
            }
            cb_plant.DataSource = items1;
            combobox_plant.DataSource = items1;
            comboBox3.DataSource = items1;


        }

        private void Btn_export_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "Hourly Output Report";
                ExportExcels.Export(a, dataGridView1);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(comboBox4.Text)))
            {
                MessageBox.Show(" Shift Cannot be Empty !", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Load_Shift_Hourly_Output_Query();
        }


        public void Load_Shift_Hourly_Output_Query()
        {
            this.dataGridView2.DataSource = null;
            this.dataGridView2.AutoGenerateColumns = false;
            try
            {
                GetShiftOutputData();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            if (dataGridView2.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                string a = comboBox4.Text + " Hourly Output Report";
                ExportExcels.Export(a, dataGridView2);
            }
        }

        private void Btn_dayoutput_submit_Click(object sender, EventArgs e)
        {
            Load_Daily_Output_Query();
        }

        private void Load_Daily_Output_Query()
        {

            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("FromDate", Txt_Formdate.Text);
            p.Add("ToDate", Txt_Todate.Text);
            p.Add("vCompany", cb_factory.SelectedValue.ToString());
            p.Add("vPlant", string.IsNullOrWhiteSpace(combobox_plant.Text) ? combobox_plant.Text : combobox_plant.Text.Split('|')[0]);
            p.Add("vProcess", string.IsNullOrWhiteSpace(combobox_process.Text) ? combobox_process.Text : combobox_process.Text.Split('|')[0]);
            p.Add("vDept", txt_daywise_line.Text);

            Cursor.Current = Cursors.WaitCursor;

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.Hourly_Output_Server", "Load_Daily_Output_Query", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                Cursor.Current = Cursors.Default;
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                //dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.Blue;
                //dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.AntiqueWhite;
                //dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
                dataGridView3.DataSource = dtJson.DefaultView;


                int sum1 = 0;
                int sum2 = 0;
                int sum3 = 0;
                decimal? avgpercentage = 0;
             
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {

                    int target_qty = string.IsNullOrEmpty(dtJson.Rows[i]["TARGET"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["TARGET"].ToString());
                    int output = string.IsNullOrEmpty(dtJson.Rows[i]["OUTPUT"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["OUTPUT"].ToString());
                    int balance = string.IsNullOrEmpty(dtJson.Rows[i]["BALANCE"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["BALANCE"].ToString());
                    //decimal percentage = string.IsNullOrEmpty(dtJson.Rows[i]["PERCENTAGE"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["PERCENTAGE"].ToString());

                    sum1 += target_qty;
                    sum2 += output;
                    sum3 += balance;
                    // sum4 += percentage;

                    int target_check_qty = string.IsNullOrEmpty(dtJson.Rows[i]["TARGET"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["TARGET"].ToString());
                    int output_check_qty = string.IsNullOrEmpty(dtJson.Rows[i]["OUTPUT"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["OUTPUT"].ToString());

                    if (output_check_qty >= target_check_qty)
                    {
                        dataGridView3.Rows[i].Cells["BALANCE"].Style.BackColor = Color.ForestGreen;
                        dataGridView3.Rows[i].Cells["BALANCE"].Style.ForeColor = Color.White;

                    }
                    else
                    {
                        dataGridView3.Rows[i].Cells["BALANCE"].Style.BackColor = Color.LightPink;
                    }



                }

                decimal totaltarget = sum1;
                decimal totaloutput = sum2;

                

                if (totaltarget == 0 && totaloutput == 0 || totaloutput == 0 || totaltarget == 0)
                {
                    avgpercentage = 0;
                }

                else
                {
                    decimal overall_avg = totaloutput / totaltarget;
                    avgpercentage = decimal.Round(overall_avg * 100, 2);
                }
                    

                string average = avgpercentage + "%";
               
                DataRow row = dtJson.NewRow();
                row["TARGET"] = sum1;
                row["OUTPUT"] = sum2;
                row["BALANCE"] = sum3;
               // row["PERCENTAGE"] = average;
                row["OP_ACHEIVE"] = average;      //ACHEIVEMENT


                dtJson.Rows.Add(row);
                dataGridView3.Update();


            }
            else
            {
                Cursor.Current = Cursors.Default;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void Btn_dayoutput_clear_Click(object sender, EventArgs e)
        {
            Txt_Formdate.Text = "";
            Txt_Todate.Text = "";
            cb_factory.ResetText();
            combobox_plant.ResetText();
            combobox_process.ResetText();
            txt_daywise_line.ResetText();
            dataGridView3.DataSource = null;
        }

        private void Btn_Dayoutput_excel_Click(object sender, EventArgs e)
        {

            if (dataGridView3.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                string a = "Daily Output Report";
                ExportExcels.Export(a, dataGridView3);
            }
        }

        private void GetShiftOutputData()
        {

            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("Date", dateTimePicker2.Text);
            p.Add("vCompany", comboBox2.SelectedValue.ToString());
            //p.Add("vCompany", cb_org.Text);
            p.Add("vPlant", string.IsNullOrWhiteSpace(comboBox3.Text) ? comboBox3.Text : comboBox3.Text.Split('|')[0]);
            p.Add("vProcess", string.IsNullOrWhiteSpace(comboBox1.Text) ? comboBox1.Text : comboBox1.Text.Split('|')[0]);
            p.Add("vDept", textBox7.Text);
            p.Add("vShift", comboBox4.Text);

            Cursor.Current = Cursors.WaitCursor;

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.MES_Hourly_Management_Server", "GetShiftOutputData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            //string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.MES_Hourly_Management_Server", "GetHourlyProductioninputData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                Cursor.Current = Cursors.Default;
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dataGridView2.DataSource = dtJson;

                #region
                //decimal sum1 = 0;
                //int sum2 = 0;
                //int sum3 = 0;
                //int sum4 = 0;
                //int sum5 = 0;
                //int sum6 = 0;
                //decimal sum7 = 0;

                //decimal? avg8 = 0;

                //for (int i = 0; i < dtJson.Rows.Count; i++)
                //{

                //    decimal target_qty = string.IsNullOrEmpty(dtJson.Rows[i]["target"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["target"].ToString());
                //    int work_hours = string.IsNullOrEmpty(dtJson.Rows[i]["WORKHOURS"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["WORKHOURS"].ToString());
                //    int manpower_qty = string.IsNullOrEmpty(dtJson.Rows[i]["MANPOWER"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["MANPOWER"].ToString());
                //    int balance_qty = string.IsNullOrEmpty(dtJson.Rows[i]["BALANCE"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["BALANCE"].ToString());
                //    decimal Overall_Qty = string.IsNullOrEmpty(dtJson.Rows[i]["TOTAL"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["TOTAL"].ToString());

                //    sum1 += target_qty;
                //    sum2 += work_hours;
                //    sum3 += manpower_qty;
                //    sum6 += balance_qty;
                //    sum7 += Overall_Qty;


                //}
                //decimal target_qty11 = sum1;
                //decimal total_qty11 = sum7;
                //decimal overall_avg = total_qty11 / target_qty11;

                //if (total_qty11 == 0 && target_qty11 == 0 || target_qty11 == 0)
                //    avg8 = 0;
                //else
                //    avg8 = decimal.Round(overall_avg * 100, 2);

                //string avg9 = avg8 + "%";

                //DataRow row = dtJson.NewRow();
                //row["target"] = sum1;
                //row["WORKHOURS"] = sum2;
                //row["MANPOWER"] = sum3;
                //row["BALANCE"] = sum6;
                //row["TOTAL"] = sum7;
                //row["ACHEIVEMENT"] = avg9;            //ACHEIVEMENT

                //dtJson.Rows.Add(row);

                #endregion

               

                #region

                //for (int i = 0; i < dtJson.Rows.Count; i++)
                //{
                //    decimal acheivementQty = 0;
                //    string target_qty = string.IsNullOrEmpty(dtJson.Rows[i]["Target"].ToString()) ? "0" : dtJson.Rows[i]["Target"].ToString();
                //    string total_qty = string.IsNullOrEmpty(dtJson.Rows[i]["total"].ToString()) ? "0" : dtJson.Rows[i]["total"].ToString();
                //    decimal result;
                //    if (!target_qty.Equals("0") && decimal.TryParse(target_qty, out result) && decimal.TryParse(total_qty, out result))
                //    {
                //        acheivementQty = decimal.Parse(total_qty) / decimal.Parse(target_qty) * 100;
                //        if (acheivementQty >= 100)
                //        {
                //            dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(44, 250, 7);
                //        }
                //        else if (acheivementQty >= 85 && acheivementQty < 100)
                //        {
                //            dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;

                //        }
                //        else if (acheivementQty < 85)
                //        {
                //            dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 182, 193);
                //            //dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(247, 143, 147);
                //        }
                //    }
                //    else
                //    {
                //        dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LavenderBlush;
                //    }

                //}


                #endregion

            }
            else
            {
                Cursor.Current = Cursors.Default;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            comboBox2.Text = null;
            comboBox3.Text = null;
            comboBox1.Text = null;
            textBox7.Text = null;
           // comboBox3.Text = null;

        }

        private void Btn_search_MouseHover(object sender, EventArgs e)
        {
            btn_search.BackColor = Color.DarkBlue;
            btn_search.ForeColor = Color.White;
        }

        private void Btn_search_MouseLeave(object sender, EventArgs e)
        {
            btn_search.ForeColor = Color.White;
            btn_search.BackColor = Color.DarkGreen;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            int width = panel1.Width;
            int height = panel1.Height;

            Point startpoint = new Point(0, 0);
            Point endpoint = new Point(width, height);


            Color color1 = ColorTranslator.FromHtml("#1bb6df");
            Color color2 = ColorTranslator.FromHtml("#1dfd64");
            Color color3 = ColorTranslator.FromHtml("#3a8ab4");

            using (LinearGradientBrush lBrush = new LinearGradientBrush(startpoint, endpoint, color1, color2))
            {
                // Define a ColorBlend to handle multiple colors
                ColorBlend colorBlend = new ColorBlend();

                // Set the positions where the colors should be blended (0 to 1)
                colorBlend.Positions = new float[] { 0.0f, 0.5f, 1.0f };

                // Set the colors that should appear at the corresponding positions
                colorBlend.Colors = new Color[] { color1, color2, color3 };

                // Apply the ColorBlend to the brush
                lBrush.InterpolationColors = colorBlend;

                e.Graphics.FillRectangle(lBrush, 0, 0, width, height);
            }



            //// Create the LinearGradientBrush with dynamic dimensions
            //using (LinearGradientBrush lgb = new LinearGradientBrush(startpoint, endpoint, Color.FromArgb(170, 249, 165, 78), Color.FromArgb(255, 255, 255, 0)))
            //{
            //    // Fill the panel with the gradient
            //    e.Graphics.FillRectangle(lgb, 0, 0, width, height);
            //}




        }
    }
}
