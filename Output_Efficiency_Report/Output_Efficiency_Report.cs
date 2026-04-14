using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutocompleteMenuNS;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using MaterialSkin.Controls;
using PO_Completion_Upload_Data;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace Output_Efficiency_Report
{
    public partial class Output_Efficiency_Report : MaterialForm
    {
        public Output_Efficiency_Report()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

        }

        private void Output_Efficiency_Report_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage2);
            txt_coefficient.BackColor = System.Drawing.SystemColors.Window;
            //cmbPosition.BackColor = System.Drawing.SystemColors.Window;
            //cmbWorkFlow.BackColor = System.Drawing.SystemColors.Window;
            label32.Visible = false;
            cmbCapacity.Visible = false;

            lblbarcode.Visible = false;
            txtBarcode.Visible = false;
            label40.Visible = false;
            label43.Visible = false;
            lbl_Emp_Name.Visible = false;
            lbl_Dept.Visible = false;

            label44.Visible = false;
            txt_coefficient.Visible = false;

            LoadDepts();
            txt_line.Select();
            //dateTimePicker1.MaxDate = DateTime.Now;

            LoadPosition();
            LoadWorkFlow();
            LoadCapacityInfo();

            timer1.Start();
            timer1.Enabled = true;

            ucRollText2.Visible = false;


        }


        private void Timer1_Tick(object sender, EventArgs e)
        {

            Random ran = new Random();
            int col1 = ran.Next(0, 255);
            int col2 = ran.Next(0, 225);
            int col3 = ran.Next(0, 200);
            int col4 = ran.Next(0, 175);

            //lbl_Total_Bonus.ForeColor = Color.FromArgb(col1, col2, col3, col4);
            lblBonus.ForeColor = Color.FromArgb(col1, col2, col3, col4);

            if (lblBonus.Text != "..")
                lblBonus.Visible = !lblBonus.Visible;

            //panel9.BackColor = Color.FromArgb(col1, col2, col3, col4);
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

        private void Btn_search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_line.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Line");
                return;
            }
            if (txt_line.Text.Length < 6)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Correct Line");
                return;
            }

            string date = dateTimePicker1.Text;
            string line = txt_line.Text.ToUpper().Trim();

            Cursor.Current = Cursors.WaitCursor;


            Getoutput_Efficiency_Po_Report(date, line);
            //LoadPOCompletionDetails(date, line);

            // LoadEfficiencyInfo(date, line);
            //txt_line.Text = null;

            Cursor.Current = Cursors.WaitCursor;

        }




        //private void Btn_search_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txt_line.Text))
        //    {
        //        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Line");
        //        return;
        //    }

        //    string date = dateTimePicker1.Text;
        //    string line = txt_line.Text.ToUpper().Trim();

        //    // Show loading progress form on a separate thread
        //    Thread t = new Thread(() => ShowLoading(date, line));
        //    t.Start();
        //}

        //private void ShowLoading(string date, string line)
        //{
        //    LoadingProgress lp = new LoadingProgress();
        //    lp.ShowDialog(); // Use ShowDialog to block the calling thread until the form is closed

        //    // Perform time-consuming operations on the UI thread after the loading form is closed
        //    this.Invoke((MethodInvoker)delegate
        //    {
        //        Getoutput_Efficiency_Po_Report(date, line);
        //    });
        //}


        public class StatusEntry
        {
            public string Code { get; set; }

            public string Name { get; set; }
        }

        private void LoadWorkFlow()    //Get all processes
        {

            var items4 = new List<AutocompleteItem>();
            string ret4 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "LoadWorkFlow", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items4.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items4.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["rout_no"].ToString(), dtJson.Rows[i - 1]["rout_name_z"].ToString() }, dtJson.Rows[i - 1]["rout_no"].ToString() + " | " + dtJson.Rows[i - 1]["rout_name_z"].ToString()));
                }
            }
            cmbWorkFlow.DataSource = items4;
            cb_process.DataSource = items4;


        }

        private DataTable LoadPosition()    //Get all processes        LoadCapacityInfo
        {
            List<string> partList1 = new List<string>();
            DataTable dt = null;
            partList1.Clear();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "Get_Position_Info", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                partList1.Add("");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        partList1.Add(dt.Rows[i]["POSITION"].ToString().Trim());
                    }
                    cmbPosition.Items.AddRange(partList1.ToArray());
                    cb_position.Items.AddRange(partList1.ToArray());
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "");
                }


            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

        private DataTable LoadCapacityInfo()    //Get all processes        LoadCapacityInfo
        {
            List<string> partList2 = new List<string>();
            DataTable dt = null;
            partList2.Clear();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "LoadCapacityInfo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                partList2.Add("");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        partList2.Add(dt.Rows[i]["CAPACITY"].ToString().Trim());
                    }
                    cmbCapacity.Items.AddRange(partList2.ToArray());
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "");
                }


            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }




        private void Getoutput_Efficiency_Po_Report(string date, string line)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Date", date);
            p.Add("Line", line);

            //string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "Getoutput_Efficiency_Po_Report", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "Getoutput_Efficiency_Po_Report", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));

                if (dtJson.Rows.Count > 0)
                {
                    lbl_target.Text = string.IsNullOrEmpty(dtJson.Rows[0]["TARGET"].ToString()) ? "No Target" : dtJson.Rows[0]["TARGET"].ToString();
                    lbl_out.Text = string.IsNullOrEmpty(dtJson.Rows[0]["QTY"].ToString()) ? "No Output" : dtJson.Rows[0]["QTY"].ToString();
                    lbl_output_line.Text = dtJson.Rows[0]["DEPT"].ToString();

                    decimal target = string.IsNullOrEmpty(dtJson.Rows[0]["TARGET"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[0]["TARGET"].ToString());
                    decimal output = string.IsNullOrEmpty(dtJson.Rows[0]["QTY"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[0]["QTY"].ToString());
                    decimal workhours = string.IsNullOrEmpty(dtJson.Rows[0]["WORKHOURS"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[0]["WORKHOURS"].ToString());
                    decimal manpower = string.IsNullOrEmpty(dtJson.Rows[0]["MANPOWER"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[0]["MANPOWER"].ToString());
                    //  string highestoutputarticle = string.IsNullOrEmpty(dtJson.Rows[0]["ART_NO1"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[0]["ART_NO1"].ToString());
                    decimal targetpph = string.IsNullOrEmpty(dtJson.Rows[0]["TARGETPPH"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[0]["TARGETPPH"].ToString());

                    label23.Text = string.IsNullOrEmpty(dtJson.Rows[0]["TOTAL_PO"].ToString()) ? "No Data" : dtJson.Rows[0]["TOTAL_PO"].ToString();
                    label27.Text = string.IsNullOrEmpty(dtJson.Rows[0]["ONTIME_FINISHED"].ToString()) ? "No Data" : dtJson.Rows[0]["ONTIME_FINISHED"].ToString();

                    decimal tota_po = string.IsNullOrEmpty(dtJson.Rows[0]["TOTAL_PO"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[0]["TOTAL_PO"].ToString());
                    decimal ontime_finshed = string.IsNullOrEmpty(dtJson.Rows[0]["ONTIME_FINISHED"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[0]["ONTIME_FINISHED"].ToString());


                    if (tota_po == 0 || ontime_finshed == 0)
                    {
                        label36.Text = "0 %";
                    }
                    else
                    {
                        decimal finish_rate = decimal.Round((ontime_finshed / tota_po) * 100, 2);
                        label36.Text = finish_rate.ToString() + " %";
                    }


                    if (target == 0 && output == 0)
                    {
                        lbl_achievement.Text = "No Achievement";
                    }
                    else
                    {
                        decimal IE_achieverate = 0;
                        decimal pph = 0;
                        decimal percentage = 0;
                        // Achievement 
                        if (target == 0 || output == 0)
                        {
                            lbl_achievement.Text = "No Achievement";
                        }

                        else
                        {
                            percentage = decimal.Round((output / target) * 100, 2);
                            lbl_achievement.Text = percentage + " %";
                        }

                        if (manpower == 0 || workhours == 0 || targetpph == 0)
                        {
                            if (manpower == 0)
                                lbl_manpower.Text = "0";
                            else
                                lbl_manpower.Text = manpower.ToString();
                            if (manpower == 0)
                                lbl_workhours.Text = "0";
                            else
                                lbl_workhours.Text = workhours.ToString();

                            if (targetpph == 0)
                                label9.Text = "0";
                            else
                                label9.Text = targetpph.ToString();
                            label6.Text = "0";
                            label10.Text = "0";
                        }
                        else
                        {
                            lbl_manpower.Text = manpower.ToString();
                            lbl_workhours.Text = workhours.ToString();
                            label9.Text = targetpph.ToString();

                            // PPH 
                            pph = decimal.Round((output / manpower / workhours), 2);
                            label6.Text = pph.ToString();

                            // IE Achievement Rate 

                            IE_achieverate = decimal.Round((output / manpower / workhours / targetpph) * 100, 2);
                            label10.Text = IE_achieverate + " %";

                        }

                    }
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
        }



        private void Btn_clear_Click(object sender, EventArgs e)
        {
            clearUI();
            txt_line.Text = null;
        }

        private void clearUI()
        {

            lbl_output_line.Text = null;
            lbl_target.Text = null;
            lbl_out.Text = null;
            lbl_achievement.Text = null;
            lbl_manpower.Text = null;
            lbl_workhours.Text = null;
            label9.Text = null;
            label6.Text = null;
            label10.Text = null;
            label23.Text = null;
            label27.Text = null;
            label36.Text = null;
        }




        #region

        //private void Txt_line_TextChanged(object sender, EventArgs e)
        //{
        //    AutocompleteMenuNS.AutocompleteMenu autocompleteMenu2 = new AutocompleteMenuNS.AutocompleteMenu();
        //    autocompleteMenu2.Items = null;
        //    autocompleteMenu2.MaximumSize = new Size(350, 400);
        //    var columnWidth = new[] { 50, 350 };
        //    DataTable dt = GetAllDepts();
        //    int n = 1;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"] + "  |  " + dt.Rows[i]["DEPARTMENT_NAME"] }, dt.Rows[i]["DEPARTMENT_CODE"] + " | " + dt.Rows[i]["DEPARTMENT_NAME"]) { ColumnWidth = columnWidth, ImageIndex = n });
        //        //autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"] + " " + dt.Rows[i]["DEPARTMENT_NAME"] }, dt.Rows[i]["DEPARTMENT_CODE"] + "|" + dt.Rows[i]["DEPARTMENT_NAME"]) { ColumnWidth = columnWidth, ImageIndex = n });
        //        n++;
        //    }
        //    autocompleteMenu2.SetAutocompleteMenu(txt_line, autocompleteMenu2);

        //}


        //private DataTable GetAllDepts()
        //{
        //    DataTable dt = new DataTable();
        //    string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "GetAllDepts", Program.Client.UserToken, JsonConvert.SerializeObject(string.Empty));
        //    //string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetAllDepts", Program.Client.UserToken, JsonConvert.SerializeObject(string.Empty));
        //    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
        //    {
        //        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
        //        dt = JsonHelper.GetDataTableByJson(json);
        //    }
        //    else
        //    {
        //        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
        //    }

        //    return dt;
        //}

        //private void Txt_line_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (!txt_line.Text.Contains('|'))
        //    {
        //        return;
        //    }
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        string dept = txt_line.Text.Trim().ToUpper().Split('|')[0];
        //        txt_line.Text = dept;

        //    }
        //}


        #endregion



        private void Label32_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ClearUI();
            txtBarcode.ResetText();
            cmbPosition.Text = "";
            cmbWorkFlow.Text = "";
            cmbCapacity.Text = "";

        }

        private void ClearUI()
        {
            lbl_Emp_Name.Text = null;
            lbl_Dept.ResetText();
            txt_leave_Hours.ResetText();

            txtIE.Text = null;
            lbl_IE_Bonus.Text = null;
            txtOP.Text = null;
            lbl_Output_Bonus.Text = null;
            lbl_PO_Bonus.Text = null;
            txtPO.Text = null;
            lblBonus.Text = "..";
            ucRollText2.Visible = false;
        }



        private void btnCalculate_Click(object sender, EventArgs e)
        {

            //if (txtBarcode.Text == "" || string.IsNullOrEmpty(txtBarcode.Text))
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Barcode");
            //    return;
            //}


            if (string.IsNullOrEmpty(cmbPosition.Text.ToString()))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Position");
                return;
            }

            if (cmbPosition.Text == "Assistant" || cmbPosition.Text == "Supervisor" || cmbPosition.Text == "Section Head" || cmbPosition.Text == "Vice Manager" || cmbPosition.Text == "Plant Incharge")
            {
                if (cmbWorkFlow.Text == "" || cmbWorkFlow.Text == null)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Work Flow");
                    return;
                }
            }

            if (string.IsNullOrEmpty(txt_leave_Hours.Text.ToString()))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Leave Hours");
                return;
            }

            #region
            //if (string.IsNullOrEmpty(cmbWorkFlow.Text.ToString()))
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Process");
            //    return;
            //}
            //if (string.IsNullOrEmpty(cmbCapacity.Text.ToString()))
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Capacity");
            //    return;
            //}


            //if (string.IsNullOrEmpty(lbl_IE_Bonus.Text.ToString()))
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Confirm IE Efficiency Score");
            //    return;
            //}
            //if (string.IsNullOrEmpty(lbl_Output_Bonus.Text.ToString()))
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Confirm Output Score");
            //    return;
            //}
            //if (string.IsNullOrEmpty(lbl_PO_Bonus.Text.ToString()))
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Confirm PO Completion Score");
            //    return;
            //}
            #endregion


            if (lbl_IE_Bonus.Text.ToString() == "..")
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Confirm IE Efficiency Score");
                return;
            }
            if (lbl_Output_Bonus.Text.ToString() == "..")
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Confirm Output Score");
                return;
            }
            if (lbl_PO_Bonus.Text.ToString() == "..")
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Confirm PO Completion Score");
                return;
            }


            if (string.IsNullOrEmpty(txt_leave_Hours.Text.ToString()))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Leave Hrs");
                return;
            }


            double ie_bonus;
            double output_bonus;
            double po_bonus;
            //double excellence_bonus;

            double ie_perc = string.IsNullOrEmpty(lbl_IE_Bonus.Text.ToString()) ? 0 : double.Parse(lbl_IE_Bonus.Text.ToString());
            double output_perc = string.IsNullOrEmpty(lbl_Output_Bonus.Text.ToString()) ? 0 : double.Parse(lbl_Output_Bonus.Text.ToString());
            double po_perc = string.IsNullOrEmpty(lbl_PO_Bonus.Text.ToString()) ? 0 : double.Parse(lbl_PO_Bonus.Text.ToString());


            if (ie_perc == 0)
                ie_bonus = 0;
            else
                //ie_bonus = decimal.Round((ie_perc * (50 / 100)),2);
                ie_bonus = Math.Round(ie_perc * 0.5, 2);

            if (output_perc == 0)
                output_bonus = 0;
            else
                output_bonus = Math.Round(output_perc * 0.25, 2);

            if (po_perc == 0)
                po_bonus = 0;
            else
                po_bonus = Math.Round(po_perc * 0.25, 2);


            DataTable dt = null;
            DataTable dtJson = null;
            DataTable dtemp = null;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Position", cmbPosition.SelectedItem.ToString());
            //dic.Add("WorkFlow", cmbWorkFlow.SelectedItem.ToString());
            //dic.Add("WorkFlow", string.IsNullOrWhiteSpace(cmbWorkFlow.Text) ? cmbWorkFlow.Text : cmbWorkFlow.Text.Split('|')[0]);

            //dic.Add("Capacity", cmbCapacity.SelectedItem.ToString());

            dic.Add("Barcode", txtBarcode.Text.Trim());
            dic.Add("Month", dateTimePicker2.Value.ToString(dateTimePicker2.CustomFormat));


            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "GetProdution_Incharges_Bonus_Info", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                dt = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            }

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "Get_Co_Efficient_Info", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtJson = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            }

            string ret2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "Get_Employee_Info", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                dtemp = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            }




            double CoEfficient_Value;

            double Position_Bonus = double.Parse(dt.Rows[0]["BONUS"].ToString());
            // double CoEfficient_Value = double.Parse(dtJson.Rows[0]["COEFFICIENT"].ToString());
            if (txt_coefficient.Text == "" || txt_coefficient.Text == null)
            {
                CoEfficient_Value = 0;
            }
            else
            {
                CoEfficient_Value = double.Parse(txt_coefficient.Text.ToString());
            }


            //lbl_Emp_Name.Text = dtemp.Rows[0]["NAME_T"].ToString();
            //lbl_Dept.Text = dtemp.Rows[0]["DEPT_NM"].ToString();

            // lbl_leave_Hours.Text = dtemp.Rows[0]["TOTAL_LEAVE_HOURS"].ToString();

            //double l_hours = double.Parse(dtemp.Rows[0]["TOTAL_LEAVE_HOURS"].ToString());


            double l_hours = double.Parse(txt_leave_Hours.Text.ToString());

            //lbl_IE_Bonus.Text = Math.Round(ie_bonus * CoEfficient_Value * Position_Bonus, 0).ToString() + " " + "\u20B9";
            //lbl_Output_Bonus.Text = Math.Round(output_bonus * CoEfficient_Value * Position_Bonus, 0).ToString() + " " + "\u20B9";
            //lbl_PO_Bonus.Text = Math.Round(po_bonus * CoEfficient_Value * Position_Bonus, 0).ToString() + " " + "\u20B9";

            

            if (l_hours >= 24)
            {
                lblBonus.Text = "0" + " " + "\u20B9";
                ucRollText2.Visible = true;
            }

            else
            {
                if(cmbPosition.Text == "Assistant" || cmbPosition.Text == "Supervisor")
                {
                    lblBonus.Text = "\u20B9" + " " + Math.Round(Position_Bonus * (ie_bonus + output_bonus + po_bonus), 0).ToString() ;
                    ucRollText2.Visible = false;
                }
                else
                {
                   // lblBonus.Text = Math.Round((ie_bonus + output_bonus + po_bonus) * CoEfficient_Value * Position_Bonus, 0).ToString() + " " + "\u20B9";
                    lblBonus.Text = "\u20B9" + " " + Math.Round(Position_Bonus*(ie_bonus + output_bonus + po_bonus) * CoEfficient_Value, 0).ToString();
                    ucRollText2.Visible = false;
                }

            }



            #region   Old Code

            //txt_Emp_Name.Text = dtemp.Rows[0]["NAME_T"].ToString();
            //txt_Emp_Dept.Text = dtemp.Rows[0]["DEPT_NM"].ToString();
            //txt_Emp_LeaveHours.Text = dtemp.Rows[0]["TOTAL_LEAVE_HOURS"].ToString();


            //txtIEBonus.Text = Math.Round(ie_bonus * CoEfficient_Value * Position_Bonus, 0).ToString();
            //txtOPBonus.Text = Math.Round(output_bonus * CoEfficient_Value * Position_Bonus, 0).ToString();
            //txtPOBonus.Text = Math.Round(po_bonus * CoEfficient_Value * Position_Bonus, 0).ToString();


            //double sumoutput_po_ie = ie_bonus + output_bonus + po_bonus;
            //excellence_bonus = Math.Round((sumoutput_po_ie * CoEfficient_Value * Position_Bonus), 2);
            //excellence_bonus = (sumoutput_po_ie * CoEfficient_Value * Position_Bonus);
            //string total_bonus = excellence_bonus.ToString() + " " + "\u20B9";
            //txtIEBonus.Text = ie_bonus.ToString();
            //txtOPBonus.Text = output_bonus.ToString();
            //txtPOBonus.Text = po_bonus.ToString();
            //// label43.Text = excellence_bonus.ToString();
            //label43.Text = total_bonus;
            #endregion


        }

        private void TxtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control characters like backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Mark the event as handled, meaning the character input will be ignored
            }
        }

        private void TxtIE_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Check if the key is not a control character, not a digit, and not a decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Mark the event as handled, meaning the character input will be ignored
            }
            // If the key is a decimal point and there's already a decimal point or it's the first character,
            // or if there are already two decimal places after the decimal point, then ignore the input
            else if ((e.KeyChar == '.' && (txtIE.Text.Contains('.') || txtIE.Text.Length == 0)) || (txtIE.Text.Contains('.') && txtIE.Text.Substring(txtIE.Text.IndexOf('.') + 1).Length >= 2))
            {
                e.Handled = true; // Mark the event as handled, meaning the character input will be ignored
            }



        }



        private void TxtOP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            else if ((e.KeyChar == '.' && (txtOP.Text.Contains('.') || txtOP.Text.Length == 0)) || (txtOP.Text.Contains('.') && txtOP.Text.Substring(txtOP.Text.IndexOf('.') + 1).Length >= 2))
            {
                e.Handled = true;
            }
        }

        private void TxtPO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            else if ((e.KeyChar == '.' && (txtPO.Text.Contains('.') || txtPO.Text.Length == 0)) || (txtPO.Text.Contains('.') && txtPO.Text.Substring(txtPO.Text.IndexOf('.') + 1).Length >= 2))
            {
                e.Handled = true;
            }
        }

        private void CmbPosition_SelectedValueChanged(object sender, EventArgs e)
        {


            txtIE.Text = null;
            txtOP.Text = null;
            txtPO.Text = null;
            lbl_IE_Bonus.Text = "..";
            lbl_Output_Bonus.Text = "..";
            lbl_PO_Bonus.Text = "..";
            cmbWorkFlow.Text = "";
            lblBonus.Text = "..";
            txt_leave_Hours.Text = null;

            if (cmbPosition.Text == "Manager" ||  cmbPosition.Text == "Captain")   //  ||  || cmbPosition.Text == ""
            {
                cmbWorkFlow.ResetText();
                cmbWorkFlow.Enabled = false;
                txt_coefficient.Text = "0.85";
            }
            else if (cmbPosition.Text == "Production Executive Manager")
            {
                cmbWorkFlow.ResetText();
                cmbWorkFlow.Enabled = false;
                label44.Visible = true;
                txt_coefficient.Visible = true;
                txt_coefficient.Text = "0.80";
            }
            else
            {
                cmbWorkFlow.Enabled = true;
                if (cmbPosition.Text == "Section Head")
                {
                    label44.Visible = true;
                    txt_coefficient.Visible = true;
                    txt_coefficient.Text = "0.95";
                }
                else if (cmbPosition.Text == "Vice Manager" || cmbPosition.Text == "Plant Incharge")
                {
                    label44.Visible = true;
                    txt_coefficient.Visible = true;
                    txt_coefficient.Text = "0.90";
                }
                else
                {
                    label44.Visible = false;
                    txt_coefficient.Visible = false;
                    txt_coefficient.Text = null;
                }
            }
        }

        private void TxtIE_KeyDown(object sender, KeyEventArgs e)
        {

            DataTable dt = null;
            double IE_Zero_criteria;
            double IE_Hundred_criteria;
            double IEbonus_Amount;


            if (e.KeyCode == Keys.Enter)
            {

                if (cmbPosition.Text == "" || cmbPosition.Text == null)
                {
                    txtIE.ResetText();
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Position");
                    return;
                }
                if (cmbPosition.Text == "Assistant" || cmbPosition.Text == "Supervisor" || cmbPosition.Text == "Section Head" || cmbPosition.Text == "Vice Manager" || cmbPosition.Text == "Plant Incharge")
                {
                    if (cmbWorkFlow.Text == "" || cmbWorkFlow.Text == null)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Work Flow");
                        return;
                    }
                }

                string WorkFlow = Regex.Split(cmbWorkFlow.Text, "\\s+", RegexOptions.IgnoreCase)[0];
                string IE_Rate = "IE Rate";
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Position", cmbPosition.SelectedItem.ToString());
                dic.Add("IE_Rate", IE_Rate);
                dic.Add("WorkFlow", string.IsNullOrWhiteSpace(WorkFlow) ? WorkFlow : WorkFlow);



                string ret3 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "GetProdution_Incharges_IE_Criteria_Info", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dic));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["RetData"].ToString();
                    dt = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                }
                double Given_IE_Value = double.Parse(txtIE.Text.ToString());
                IE_Zero_criteria = double.Parse(dt.Rows[0]["0_PTS_STANDARD"].ToString());
                IE_Hundred_criteria = double.Parse(dt.Rows[0]["100_PTS_STANDARD"].ToString());



                if (Given_IE_Value < IE_Zero_criteria && WorkFlow == "C")
                {
                    lbl_IE_Bonus.Text = "0";
                }
                else if (Given_IE_Value < IE_Zero_criteria && WorkFlow == "S")
                {
                    lbl_IE_Bonus.Text = "0";
                }
                else if (Given_IE_Value < IE_Zero_criteria && WorkFlow == "L")
                {
                    lbl_IE_Bonus.Text = "0";
                }
                else if (Given_IE_Value < IE_Zero_criteria && WorkFlow == "T")
                {
                    lbl_IE_Bonus.Text = "0";
                }
                else if (Given_IE_Value < IE_Zero_criteria && WorkFlow == "C2S")
                {
                    lbl_IE_Bonus.Text = "0";
                }
                else if (Given_IE_Value < IE_Zero_criteria && WorkFlow == "C2B")
                {
                    lbl_IE_Bonus.Text = "0";
                }
                else if (Given_IE_Value < IE_Zero_criteria && (cmbPosition.Text == "Manager" || cmbPosition.Text == "Captain" || cmbPosition.Text == "Production Executive Manager"))
                {
                    lbl_IE_Bonus.Text = "0";
                }
                else
                {
                    IEbonus_Amount = Math.Round(20 + (Given_IE_Value - IE_Zero_criteria) * ((100 - 20) / (IE_Hundred_criteria - IE_Zero_criteria)), 0);
                    lbl_IE_Bonus.Text = IEbonus_Amount.ToString();
                }

                e.Handled = true;

                e.SuppressKeyPress = true; // Suppress default behavior of Enter key

                // Set focus back to the TextBox
                txtIE.Focus();
                txtIE.Select(txtIE.Text.Length, 0); // Move cursor to the end of text
            }




        }

        private void TxtOP_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable dt = null;
            double Output_Zero_criteria;
            double Output_Hundred_criteria;
            double OPbonus_Amount;


            if (e.KeyCode == Keys.Enter)
            {

                if (cmbPosition.Text == "" || cmbPosition.Text == null)
                {
                    txtOP.ResetText();
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Position");
                    return;
                }
                if (cmbPosition.Text == "Assistant" || cmbPosition.Text == "Supervisor" || cmbPosition.Text == "Section Head" || cmbPosition.Text == "Vice Manager" || cmbPosition.Text == "Plant Incharge")
                {
                    if (cmbWorkFlow.Text == "" || cmbWorkFlow.Text == null)
                    {
                        txtOP.ResetText();
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Work Flow");
                        return;
                    }
                }



                string WorkFlow = Regex.Split(cmbWorkFlow.Text, "\\s+", RegexOptions.IgnoreCase)[0];
                string Output = "Output";
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Position", cmbPosition.SelectedItem.ToString());
                dic.Add("Output", Output);
                dic.Add("WorkFlow", string.IsNullOrWhiteSpace(WorkFlow) ? WorkFlow : WorkFlow);



                string ret4 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "GetProdution_Incharges_Output_Criteria_Info", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dic));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["RetData"].ToString();
                    dt = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                }
                double Given_OP_Value = double.Parse(txtOP.Text.ToString());
                Output_Zero_criteria = double.Parse(dt.Rows[0]["0_PTS_STANDARD"].ToString());
                Output_Hundred_criteria = double.Parse(dt.Rows[0]["100_PTS_STANDARD"].ToString());



                if (Given_OP_Value < Output_Zero_criteria && WorkFlow == "C")
                {
                    lbl_Output_Bonus.Text = "0";
                }
                else if (Given_OP_Value < Output_Zero_criteria && WorkFlow == "S")
                {
                    lbl_Output_Bonus.Text = "0";
                }
                else if (Given_OP_Value < Output_Zero_criteria && WorkFlow == "L")
                {
                    lbl_Output_Bonus.Text = "0";
                }
                else if (Given_OP_Value < Output_Zero_criteria && WorkFlow == "T")
                {
                    lbl_Output_Bonus.Text = "0";
                }
                else if (Given_OP_Value < Output_Zero_criteria && WorkFlow == "C2S")
                {
                    lbl_Output_Bonus.Text = "0";
                }
                else if (Given_OP_Value < Output_Zero_criteria && WorkFlow == "C2B")
                {
                    lbl_Output_Bonus.Text = "0";
                }
                else if (Given_OP_Value < Output_Zero_criteria && (cmbPosition.Text == "Manager" || cmbPosition.Text == "Captain" || cmbPosition.Text == "Production Executive Manager"))
                {
                    lbl_Output_Bonus.Text = "0";
                }
                else
                {
                    OPbonus_Amount = Math.Round(20 + (Given_OP_Value - Output_Zero_criteria) * ((100 - 20) / (Output_Hundred_criteria - Output_Zero_criteria)), 0);
                    lbl_Output_Bonus.Text = OPbonus_Amount.ToString();
                }

                e.Handled = true;
                e.SuppressKeyPress = true; // Suppress default behavior of Enter key

                // Set focus back to the TextBox
                txtOP.Focus();
                txtOP.Select(txtOP.Text.Length, 0); // Move cursor to the end of text

            }

        }

        private void TxtPO_KeyDown(object sender, KeyEventArgs e)
        {

            DataTable dt = null;
            double POCompletion_Zero_criteria;
            double POCompletion_Hundred_criteria;
            double PObonus_Amount;


            if (e.KeyCode == Keys.Enter)
            {

                if (cmbPosition.Text == "" || cmbPosition.Text == null)
                {
                    txtPO.ResetText();
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Position");
                    return;
                }
                if (cmbPosition.Text == "Assistant" || cmbPosition.Text == "Supervisor" || cmbPosition.Text == "Section Head" || cmbPosition.Text == "Vice Manager" || cmbPosition.Text == "Plant Incharge")
                {
                    if (cmbWorkFlow.Text == "" || cmbWorkFlow.Text == null)
                    {
                        txtPO.ResetText();
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Work Flow");
                        return;
                    }
                }




                string WorkFlow = Regex.Split(cmbWorkFlow.Text, "\\s+", RegexOptions.IgnoreCase)[0];
                string POCompletion = "PO Completion";
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Position", cmbPosition.SelectedItem.ToString());
                dic.Add("POCompletion", POCompletion);
                dic.Add("WorkFlow", string.IsNullOrWhiteSpace(WorkFlow) ? WorkFlow : WorkFlow);



                string ret4 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "GetProdution_Incharges_POCompletion_Criteria_Info", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dic));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["RetData"].ToString();
                    dt = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                }
                double Given_PO_Value = double.Parse(txtPO.Text.ToString());
                POCompletion_Zero_criteria = double.Parse(dt.Rows[0]["0_PTS_STANDARD"].ToString());
                POCompletion_Hundred_criteria = double.Parse(dt.Rows[0]["100_PTS_STANDARD"].ToString());



                if (Given_PO_Value < POCompletion_Zero_criteria && WorkFlow == "C")
                {
                    lbl_PO_Bonus.Text = "0";
                }
                else if (Given_PO_Value < POCompletion_Zero_criteria && WorkFlow == "S")
                {
                    lbl_PO_Bonus.Text = "0";
                }
                else if (Given_PO_Value < POCompletion_Zero_criteria && WorkFlow == "L")
                {
                    lbl_PO_Bonus.Text = "0";
                }
                else if (Given_PO_Value < POCompletion_Zero_criteria && WorkFlow == "T")
                {
                    lbl_PO_Bonus.Text = "0";
                }
                else if (Given_PO_Value < POCompletion_Zero_criteria && WorkFlow == "C2S")
                {
                    lbl_PO_Bonus.Text = "0";
                }
                else if (Given_PO_Value < POCompletion_Zero_criteria && WorkFlow == "C2B")
                {
                    lbl_PO_Bonus.Text = "0";
                }
                else if (Given_PO_Value < POCompletion_Zero_criteria && (cmbPosition.Text == "Manager" || cmbPosition.Text == "Captain" || cmbPosition.Text == "Production Executive Manager"))
                {
                    lbl_PO_Bonus.Text = "0";
                }
                else
                {
                    PObonus_Amount = Math.Round(20 + (Given_PO_Value - POCompletion_Zero_criteria) * ((100 - 20) / (POCompletion_Hundred_criteria - POCompletion_Zero_criteria)), 0);
                    lbl_PO_Bonus.Text = PObonus_Amount.ToString();
                }

                e.Handled = true;
                e.SuppressKeyPress = true; // Suppress default behavior of Enter key

                // Set focus back to the TextBox
                txtPO.Focus();
                txtPO.Select(txtPO.Text.Length, 0); // Move cursor to the end of text


            }

        }



        private void Btn_criteria_search_Click(object sender, EventArgs e)
        {
            if ((cb_process.Text == "" || string.IsNullOrEmpty(cb_process.Text)) && (cb_position.Text == "" || string.IsNullOrEmpty(cb_position.Text)) && (cb_criteria.Text == "" || string.IsNullOrEmpty(cb_criteria.Text)))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Any One Condition");
                return;
            }

            dataGridView1.DataSource = null;

            string Process = Regex.Split(cb_process.Text, "\\s+", RegexOptions.IgnoreCase)[0];
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Position", string.IsNullOrWhiteSpace(cb_position.Text) ? cb_position.Text : cb_position.SelectedItem.ToString());
            dic.Add("Process", Process);
            dic.Add("Criteria", string.IsNullOrWhiteSpace(cb_criteria.Text) ? cb_criteria.Text : cb_criteria.Text.ToString());

            Cursor.Current = Cursors.WaitCursor;
            string ret4 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "Get_Excellence_Bonus_Criteria_Info", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["RetData"].ToString();
                DataTable dt = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                dataGridView1.DataSource = dt;
            }
            else
            {
                Cursor.Current = Cursors.Default;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["ErrMsg"].ToString());
            }

        }

        private void Btn_criteria_clear_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            cb_process.Text = "";
            cb_criteria.Text = "";
            cb_position.Text = "";

        }

        private void Btn_export_pdf_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Excellence Bonus Criteria";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, "Not Possible to Convert Data" + ex.Message);
                            //MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                            pdfTable.DefaultCell.Padding = 2;

                            BaseFont bfTahoma = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
                            iTextSharp.text.Font fontTahoma = new iTextSharp.text.Font(bfTahoma, 8, iTextSharp.text.Font.NORMAL);
                            pdfTable.DefaultCell.Phrase = new Phrase() { Font = fontTahoma };

                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;

                      

                            //pdfTable.WidthPercentage = 100;
                            //pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;


                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            //foreach (DataGridViewRow row in dataGridView1.Rows)
                            //{
                            //    foreach (DataGridViewCell cell in row.Cells)
                            //    {
                            //        pdfTable.AddCell(cell.Value.ToString());
                            //    }
                            //}

                            int rowCount = dataGridView1.Rows.Count;
                            for (int i = 0; i < rowCount - 1; i++)
                            {
                                DataGridViewRow row = dataGridView1.Rows[i];
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value?.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "PDF Downloaded Successfully");
                            //WebBrowser.Navigate(sfd.FileName);
                            //MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                //MessageBox.Show("No Record To Export !!!", "Info");
            }


        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            //lbl_date_info.Text = DateTime.Now.ToLongDateString();
            //lbl_time_info.Text = DateTime.Now.ToString("hh:mm:ss tt").ToString();
        }
    }
}
