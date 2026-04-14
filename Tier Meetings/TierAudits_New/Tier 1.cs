using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms.DataVisualization.Charting;



namespace TierAudits_New
{
    public partial class Form1 : Form
    {
        private Timer timer3;
        string production_target = "";
        string production_output = "";
        
       
        string ie_acheivement = "";
        
        string pph = "";
        string rft_percentage = "";
       // string bgrade = "";
       // string total_inspected = "";
        string line = "";
       
        string outputpercent = "";
        string Talktime = "";
        string Shoe_name= "";
       string Manpower = "";
        string downtime = "";
        string Req_date = "";
        string Total_Cycle_Time = "";
        string IdealOperators = "";
        string LLERatio = "";
        string KaizenPM = "";
        string rftIssue = "";
        string injury = "";
        string LineKaizen = "";
        string work_injury = "";
        string ProdLineIssues = "";
        string ER_prcnt = "";
        string ieTarget = "";
        string ieAchv = "";
        string targetPPH = "";



        public Form1()
        {
            InitializeComponent();
            // AddButtonColumn();
            //dataGridView_tier2.CellContentClick += dataGridView_tier2_CellContentClick;
            timer1 = new Timer();
            
            timer1.Interval = 300000; //5 min

            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start(); // Start the timer
            GetDept();

        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (this.IsDisposed || !this.Visible)
            {
                // The form is disposed or not visible, so stop the timer or return.
                timer1.Stop(); // Stop the timer to prevent future tick events from occurring.
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Simulating an asynchronous data loading operation
                await Task.Run(() =>
                {

                    System.Threading.Thread.Sleep(5000); // Simulating a 5-second delay
                });
                // Refresh your form here
                this.Refresh();
            GetformLoadData();
            rftStatus();
            IEeffiStatus();
            pphStatus();
            outputStatus();
            erStatus();
            fillChartDB();
            LLERStatus();
            injuryStatus();
            lineKaizenStatus();
            fillChartRFT();
            fillChartIE();
            fillChartKaizen();
            fillChartWrkInj();

            }
            finally
            {
                this.Cursor = Cursors.Default; // Reset the cursor when the operation is complete
            }

        }

       

        // Tier1 Search Method
        private void Button29_Click(object sender, EventArgs e)
        {
            GetformLoadData();
            // getFormLoadIssues();
            // getMachineLoadData();
            rftStatus();
            IEeffiStatus();
            pphStatus();
            outputStatus();
            erStatus();
            fillChartDB();
            LLERStatus();
            injuryStatus();
            lineKaizenStatus();
            fillChartRFT();
            fillChartIE();
            fillChartKaizen();
            fillChartWrkInj();
        }



       private async void Form1_Load(object sender, EventArgs e)
        {
          try
              {
                 this.Cursor = Cursors.WaitCursor;

                 // Simulating an asynchronous data loading operation
                 await Task.Run(() =>
                  {

                    System.Threading.Thread.Sleep(5000); // Simulating a 5-second delay
                  });
                    LoadDept();
            GetformLoadData();
           // getFormLoadIssues();
            //getMachineLoadData();
            this.Refresh();
            fillChartDB();
            rftStatus();
            IEeffiStatus();
            pphStatus();
            outputStatus();
            erStatus();
            LLERStatus();
            injuryStatus();
            lineKaizenStatus();
            fillChartRFT();
            fillChartIE();
            fillChartKaizen();
            fillChartWrkInj();
                }
                finally
                {
                    this.Cursor = Cursors.Default; // Reset the cursor when the operation is complete
                }
            }
        private void GetformLoadData()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string lineValue = textDept.Text.ToUpper();
            p.Add("vline", lineValue);
             p.Add("vstart_date", start_date.Text);
           


            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetformLoadData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                 production_target= dtJson1.Rows[0]["TARGET"].ToString();
                 production_output = dtJson1.Rows[0]["OUTPUT"].ToString();
                
                rft_percentage = dtJson1.Rows[0]["RFTPERCENT"].ToString();
                outputpercent = dtJson1.Rows[0]["outputpercent"].ToString();
                Talktime = dtJson1.Rows[0]["talktime"].ToString();
                Shoe_name = dtJson1.Rows[0]["model_name"].ToString();
                Manpower = dtJson1.Rows[0]["operators"].ToString();
                downtime = dtJson1.Rows[0]["pausehour"].ToString();
                Total_Cycle_Time = dtJson1.Rows[0]["SELECTED_TCT"].ToString();
                IdealOperators = dtJson1.Rows[0]["IdealOperator"].ToString();
                LLERatio = dtJson1.Rows[0]["ller"].ToString();
                KaizenPM = dtJson1.Rows[0]["KaizenTargetPM"].ToString();
                rftIssue = dtJson1.Rows[0]["inspection_names"].ToString();
                injury = dtJson1.Rows[0]["INJURY_COUNT"].ToString();
                LineKaizen = dtJson1.Rows[0]["kaizens"].ToString();
                work_injury = dtJson1.Rows[0]["INJURY_DETAILS_LIST"].ToString();
                ProdLineIssues = dtJson1.Rows[0]["ProdLineIssues"].ToString();
                ER_prcnt = dtJson1.Rows[0]["ER_percent"].ToString();
                pph = dtJson1.Rows[0]["ActualPPH"].ToString();
               // ieTarget = dtJson1.Rows[0]["IE_TARGET"].ToString();
                ieAchv = dtJson1.Rows[0]["IEacheivement"].ToString();
                targetPPH = dtJson1.Rows[0]["TargetPPH"].ToString();





                ProdTarget.Text = production_target;
                ProdOutput.Text = production_output;
                talktime.Text = Talktime;
                LLER.Text = LLERatio;
                workInjury.Text = injury;
                LineKaizencount.Text = LineKaizen;
                WorkInjuryLbl.Text = work_injury;
                PLIssuesLbl.Text = ProdLineIssues;
                R_FT.Text = rft_percentage;

                rftIssues.Text = rftIssue;
                //IeTarg.Text = ieTarget;
                ieAchieve.Text = ieAchv;
                er.Text = ER_prcnt;
               
                actual_pph.Text = pph;
                PPhTrg.Text = targetPPH;
                mp.Text = Manpower;
                kaizenTargetLBL.Text = KaizenPM;

                model.Text = Shoe_name;
                model.AutoEllipsis = true;


                if (!string.IsNullOrEmpty(model.Text))
                {
                    ToolTip toolTip = new ToolTip();
                    toolTip.AutoPopDelay = 5000; // ToolTip will disappear after 5 seconds
                    toolTip.SetToolTip(model, model.Text);
                }


                Downtime.Text = downtime;
                tct.Text = Total_Cycle_Time;
                idlOper.Text = IdealOperators;
                



            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


       

        private void fillChartDB()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", textDept.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "TM1ChartProd", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        production_output = dtJson.Rows[0]["Delivery_percent"] != DBNull.Value ? dtJson.Rows[0]["Delivery_percent"].ToString() : "";
                        Req_date = dtJson.Rows[0]["SCAN_DATE"] != DBNull.Value ? dtJson.Rows[0]["SCAN_DATE"].ToString() : "";
                    }
                    //production_output = dtJson.Rows[0]["Delivery_percent"].ToString();
                    //Req_date = dtJson.Rows[0]["SCAN_DATE"].ToString();

                    outputChart.Series["Delivery"].XValueMember = "SCAN_DATE";
                    outputChart.Series["Delivery"].YValueMembers = "Delivery_percent";

                    outputChart.DataSource = dtJson;

                    outputChart.Series["Delivery"].ToolTip = "Output %: #VALY";

                    outputChart.DataBind();

                    outputChart.ChartAreas[0].AxisX.Interval = 1;

                    foreach (Series series in outputChart.Series)
                    {
                        foreach (DataPoint point in series.Points)
                        {
                            if (point.YValues[0] >= 100)
                            {
                                point.Color = Color.Green;
                            }

                            else if (point.YValues[0] >= 95 && point.YValues[0] <100)
                            {
                                point.Color = Color.Blue;
                            }

                            else if (point.YValues[0] <95 )
                            {
                                point.Color = Color.Red;
                            }
                        }
                    }
                }
                catch (Newtonsoft.Json.JsonSerializationException ex)
                {
                    Console.WriteLine("Error deserializing JSON: " + ex.Message);
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //RFT Chart
        private void fillChartRFT()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", textDept.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "TM1ChartRFT", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        production_output = dtJson.Rows[0]["RFTpercent"] != DBNull.Value ? dtJson.Rows[0]["RFTpercent"].ToString() : "";
                        Req_date = dtJson.Rows[0]["createdate"] != DBNull.Value ? dtJson.Rows[0]["createdate"].ToString() : "";
                    }
                    //production_output = dtJson.Rows[0]["RFTpercent"].ToString();
                    //Req_date = dtJson.Rows[0]["createdate"].ToString();


                    RFTChart.Series["RFT %"].XValueMember = "createdate";
                    RFTChart.Series["RFT %"].YValueMembers = "RFTpercent";

                    RFTChart.DataSource = dtJson;

                    RFTChart.Series["RFT %"].ToolTip = "RFT %: #VALY";

                    RFTChart.DataBind();

                    RFTChart.ChartAreas[0].AxisX.Interval = 1;

                    foreach (Series series in RFTChart.Series)
                    {
                        foreach (DataPoint point in series.Points)
                        {
                            if (point.YValues[0] >= 85)
                            {
                                point.Color = Color.Green;
                            }

                            else if (point.YValues[0] >= 75 && point.YValues[0] < 85)
                            {
                                point.Color = Color.Gold;
                            }

                            else if (point.YValues[0] < 75)
                            {
                                point.Color = Color.Red;
                            }
                        }
                    }
                }
                catch (Newtonsoft.Json.JsonSerializationException ex)
                {
                    // Handle the exception here
                    Console.WriteLine("Error deserializing JSON: " + ex.Message);
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        private void fillChartIE()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", textDept.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "TM1ChartIE", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        ieAchv = dtJson.Rows[0]["IEacheivement"] != DBNull.Value ? dtJson.Rows[0]["IEacheivement"].ToString() : "";
                        Req_date = dtJson.Rows[0]["scan_date"] != DBNull.Value ? dtJson.Rows[0]["scan_date"].ToString() : "";
                    }
                    

                    IEchart.Series["IE_Acheiv"].XValueMember = "scan_date";
                    IEchart.Series["IE_Acheiv"].YValueMembers = "IEacheivement";

                    IEchart.DataSource = dtJson;

                    IEchart.Series["IE_Acheiv"].ToolTip = "Achievement %: #VALY";

                    IEchart.DataBind();
                   IEchart.ChartAreas[0].AxisX.Interval = 1;

                    foreach (Series series in IEchart.Series)
                    {
                        foreach (DataPoint point in series.Points)
                        {
                            if (point.YValues[0] >= 85)
                            {
                                point.Color = Color.Green;
                            }

                            else if (point.YValues[0] >= 75 && point.YValues[0] < 85)
                            {
                                point.Color = Color.Blue;
                            }
                            else if (point.YValues[0] < 75)
                            {
                                point.Color = Color.Red;
                            }
                        }
                    }
                }
                catch (Newtonsoft.Json.JsonSerializationException ex)
                {
                    // Handle the exception here
                    Console.WriteLine("Error deserializing JSON: " + ex.Message);
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        private void fillChartKaizen()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", textDept.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetformLoadData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        LineKaizen = dtJson.Rows[0]["kaizens"] != DBNull.Value ? dtJson.Rows[0]["kaizens"].ToString() : "";
                       //eq_date = dtJson.Rows[0]["IDATE"] != DBNull.Value ? dtJson.Rows[0]["IDATE"].ToString() : "";
                    }
                   //ineKaizen = dtJson.Rows[0]["kaizens"].ToString();
                   // Req_date = dtJson.Rows[0]["createdate"].ToString();


                    //kaizenChart.Series[""].XValueMember = "createdate";
                    kaizenChart.Series["Month kaizens"].YValueMembers = "kaizens";

                    kaizenChart.DataSource = dtJson;

                    kaizenChart.Series["Month kaizens"].ToolTip = "Month kaizens: #VALY";

                    kaizenChart.DataBind();

                   // kaizenChart.ChartAreas[0].AxisX.Interval = 1;

                    //foreach (Series series in kaizenChart.Series)
                    //{
                    //    foreach (DataPoint point in series.Points)
                    //    {
                    //        if (point.YValues[0] >= 95)
                    //        {
                    //            point.Color = Color.DarkGreen;
                    //        }
                    //        else
                    //        {
                    //            point.Color = Color.OrangeRed;
                    //        }
                    //    }
                    //}
                }
                catch (Newtonsoft.Json.JsonSerializationException ex)
                {
                    // Handle the exception here
                    Console.WriteLine("Error deserializing JSON: " + ex.Message);
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //work injury chart
        private void fillChartWrkInj()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", textDept.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "TM1ChartWorkInjury", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    //injury = dtJson.Rows[0]["INJURY_COUNT"].ToString();
                    // Req_date = dtJson.Rows[0]["IDATE"].ToString();

                    if (dtJson.Rows.Count > 0)
                    {
                        injury = dtJson.Rows[0]["INJURY_COUNT"] != DBNull.Value ? dtJson.Rows[0]["INJURY_COUNT"].ToString() : "";
                        Req_date = dtJson.Rows[0]["IDATE"] != DBNull.Value ? dtJson.Rows[0]["IDATE"].ToString() : "";
                    }

                    safetyChart.Series["work Injuries"].XValueMember = "IDATE";
                    safetyChart.Series["work Injuries"].YValueMembers = "INJURY_COUNT";

                    safetyChart.DataSource = dtJson;

                    safetyChart.Series["work Injuries"].ToolTip = "work Injuries count: #VALY";

                    safetyChart.DataBind();

                   // safetyChart.ChartAreas[0].AxisX.Interval = 1;

                    //foreach (Series series in outputChart.Series)
                    //{
                    //    foreach (DataPoint point in series.Points)
                    //    {
                    //        if (point.YValues[0] >= 0)
                    //        {
                    //            point.Color = Color.DarkGreen;
                    //        }
                    //        else
                    //        {
                    //            point.Color = Color.OrangeRed;
                    //        }
                    //    }
                    //}
                }
                catch (Newtonsoft.Json.JsonSerializationException ex)
                {
                    Console.WriteLine("Error deserializing JSON: " + ex.Message);
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }



        private void LoadDept()
        {
            try
            {
                GetDept();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            textDept.Text = line;
            //labelDeptName.Text = d_dept_name;
        }
        private void getFormLoadIssues()
        {
            try
            {
                GetIssues();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            

        }


        private void rftStatus()
        {
            if (float.TryParse(rft_percentage, out float rftParsed))
            {
                if (rftParsed >= 85)
                {
                    rft_status.Text = "⚫";
                    rft_status.ForeColor = Color.Green;
                }

                else if (rftParsed >= 75 && rftParsed < 85)
                {
                    rft_status.Text = "⚫";
                    rft_status.ForeColor = Color.Gold;
                }

                else if (rftParsed < 75)
                {
                    rft_status.Text = "⚫";
                    rft_status.ForeColor = Color.Red;
                }
            }
            else
            {
                rft_status.Text = "-";
                rft_status.ForeColor = Color.Black;
            }
        }

        private void IEeffiStatus()
        {
            if (float.TryParse(ieAchv, out float ieParsed))
            {
                if (ieParsed >= 80)
                {
                    ieEffiStatus.Text = "⚫";
                    ieEffiStatus.ForeColor = Color.Green;
                }
                else if (ieParsed >= 75 && ieParsed < 80)
                {
                    ieEffiStatus.Text = "⚫";
                    ieEffiStatus.ForeColor = Color.Blue;
                }

                else if (ieParsed < 75)
                {
                    ieEffiStatus.Text = "⚫";
                    ieEffiStatus.ForeColor = Color.Red;
                }
            }
            else
            {
                ieEffiStatus.Text = "-";
                ieEffiStatus.ForeColor = Color.Black;
            }
        }

        private void erStatus()
        {
            if (float.TryParse(ER_prcnt, out float ieParsed))
            {
                if (ieParsed >= 70)
                {
                    erState.Text = "⚫";
                    erState.ForeColor = Color.Green;
                }
                else if (ieParsed < 70)
                {
                    erState.Text = "⚫";
                    erState.ForeColor = Color.Red;
                }
            }
            else
            {
                erState.Text = "-";
                erState.ForeColor = Color.Black;
            }
        }

        private void pphStatus()
        {
            if (float.TryParse(pph, out float pphvalue)) 
            {
                if (float.TryParse(PPhTrg.Text, out float pphtg))
                {
                    if (pphvalue >= pphtg)
                    {
                        pphState.Text = "⚫";
                        pphState.ForeColor = Color.Green;
                    }
                    else if (pphvalue < pphtg)
                    {
                        pphState.Text = "⚫";
                        pphState.ForeColor = Color.Red;
                    }
                }
            }
            else
            {
                pphState.Text = "-";
                pphState.ForeColor = Color.Black;
            }
        }

        private void outputStatus()
        {
            if (float.TryParse(outputpercent, out float outputper))
            {
                if (outputper >= 100)
                {
                    outputstate.Text = "⚫";
                    outputstate.ForeColor = Color.Green;
                }
                else if (outputper >= 95 && outputper < 100)
                {
                    outputstate.Text = "⚫";
                    outputstate.ForeColor = Color.Blue;
                }

                else if (outputper < 95)
                {
                    outputstate.Text = "⚫";
                    outputstate.ForeColor = Color.Red;
                }
            }
            else
            {
                outputstate.Text = "-";
                outputstate.ForeColor = Color.Black;
            }
        }

        private void DowntimeStatus()
        {
            if (float.TryParse(downtime, out float Downtime))
            {
                if (Downtime < 0)
                {
                    dtStatus.Text = "⚫";
                    dtStatus.ForeColor = Color.Green;
                }
                else if (Downtime > 0)
                {
                    dtStatus.Text = "⚫";
                    dtStatus.ForeColor = Color.Red;
                }
            }
            else
            {
                dtStatus.Text = "-";
                dtStatus.ForeColor = Color.Black;
            }
        }

        private void LLERStatus()
        {
            if (float.TryParse(LLERatio, out float stateLLER))
            {
                if (stateLLER >= 85)
                {
                    statusLLER.Text = "⚫";
                    statusLLER.ForeColor = Color.Green;
                }
                else if (stateLLER < 85)
                {
                    statusLLER.Text = "⚫";
                    statusLLER.ForeColor = Color.Red;
                }
            }
            else
            {
                statusLLER.Text = "-";
                statusLLER.ForeColor = Color.Black;
            }
        }

        private void injuryStatus()
        {
            if (float.TryParse(injury, out float InjuryState))
            {
                if (InjuryState < 0)
                {
                    StatusInjury.Text = "⚫";
                    StatusInjury.ForeColor = Color.Green;
                }
                else if (InjuryState > 0)
                {
                    StatusInjury.Text = "⚫";
                    StatusInjury.ForeColor = Color.Red;
                }
            }
            else
            {
                StatusInjury.Text = "-";
                StatusInjury.ForeColor = Color.Black;
            }
        }

        private void lineKaizenStatus()
        {
            if (float.TryParse(LineKaizen, out float kaizensState))
            {
                if (float.TryParse(kaizenTargetLBL.Text, out float LKtarget))
                {
                    if (kaizensState >= LKtarget)
                    {
                        kaizenStarus.Text = "⚫";
                        kaizenStarus.ForeColor = Color.Green;
                    }
                    else if (kaizensState < LKtarget)
                    {
                        kaizenStarus.Text = "⚫";
                        kaizenStarus.ForeColor = Color.Red;
                    }
                    //else if (kaizensState = LKtarget)
                    //{
                    //    kaizenStarus.Text = "⚫";
                    //    kaizenStarus.ForeColor = Color.Green;
                    //}

                }
                
            }
            else
            {
                kaizenStarus.Text = "-";
                kaizenStarus.ForeColor = Color.Black;
            }
        }





        //Load Line Machinary Data 
        private void getMachineLoadData()
        {
            try
            {
                GetMachineLoadData();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }


        }
        //Load Production Lines
        private void GetDept()
        {
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetDept", Program.Client.UserToken, string.Empty);
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                line = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
               // d_dept_name = dtJson.Rows[0]["DEPARTMENT_NAME"].ToString();
               // orgId = dtJson.Rows[0]["ORG_ID"].ToString();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //Load Form Production Issues
        private void GetIssues()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", textDept.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetProdIssuesData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson2 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson2.Rows.Count > 0)
                {

                    //dataGridView_tier2.DataSource = dtJson2;

                }
                else
                {
                    MessageBox.Show("No such data");
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetMachineLoadData()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", textDept.Text);
            //Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetMachineLoadData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson2 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson2.Rows.Count > 0)
                {

                    //Grd_machine.DataSource = dtJson2;
                        
                }
                else
                {
                    MessageBox.Show("No such data");
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void Tier2_Search_Click(object sender, EventArgs e)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", textDept.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetProdIssuesData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));


            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
               

                if (dtJson.Rows.Count > 0)
                {

                    //dataGridView_tier2.DataSource = dtJson;

                }
                else
                {
                    MessageBox.Show("No such data");
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textDept_TextChanged(object sender, EventArgs e)
        {
            GetDept();
        }

       

        private async void Button29_Click_1(object sender, EventArgs e)
        {
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    // Simulating an asynchronous data loading operation
                    await Task.Run(() =>
                    {

                        System.Threading.Thread.Sleep(5000); // Simulating a 5-second delay
                    });
                    string lineValue = textDept.Text.ToUpper();
                    GetformLoadData();
                    // getFormLoadIssues();
                    //getMachineLoadData();
                    this.Refresh();
                    rftStatus();
                    IEeffiStatus();
                    pphStatus();
                    outputStatus();
                    erStatus();
                    fillChartDB();
                    LLERStatus();
                    injuryStatus();
                    lineKaizenStatus();
                    fillChartRFT();
                    fillChartIE();
                    fillChartKaizen();
                    fillChartWrkInj();
                }
                finally
                {
                    this.Cursor = Cursors.Default; // Reset the cursor when the operation is complete
                }
            }
        }
    }
}
