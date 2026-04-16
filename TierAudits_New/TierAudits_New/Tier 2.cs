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
using SJeMES_Framework.WebAPI;
using SJeMES_Control_Library;
using AutocompleteMenuNS;
using System.Windows.Forms.DataVisualization.Charting;
//using System.Windows.Forms.DataVisualization.Charting.Chart;



namespace TierAudits_New
{
    public partial class Form2 : Form
    {
        private Timer timer3;
        string production_target = "";
        string production_output = "";
      //  string Team_production_target = "";
       // string Team_Prod_Output = "";


        string final_hc = "";
        string ie_bonus = "";
        string ie_target = "";
        string ie_acheivement = "";
        
        string pph = "";
        string rft_percentage = "";
        string bgrade = "";
        string total_inspected = "";
        string line = "";
        string team = "";
       
        string outputpercent = "";
        string teamoutputpercent = "";
      //  string Talktime = "";
        string Shoe_name= "";
       string Manpower = "";
        string downtime = "";
        string Req_date = "";
       // string Total_Cycle_Time = "";
        string IdealOperators = "";
        string LLERatio = "";
        string KaizenPM = "";
       // string rftIssue = "";
        string injury = "";
        string LineKaizen = "";
        string PlantKaizenCRT = "";
        string PlantsixS = "";
        //string mp = "";
        string plantCO = "";
        



        public Form2()
        {
            InitializeComponent();

            tableLayoutPanel6.Height = Screen.GetWorkingArea(this).Height - 30;
            autocompleteMenu1.SetAutocompleteMenu(team_data, autocompleteMenu1);


            //LoadTeam();
            // AddButtonColumn();
            //dataGridView_tier2.CellContentClick += dataGridView_tier2_CellContentClick;
            timer1 = new Timer();
            
            timer1.Interval = 600000; //10 min

            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start(); // Start the timer
            //GetDept();
           

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
            //GetformLoadData();
           // GetTeamDataRecord();
            ChartPlantOutput();
            fillChartKaizenT2();
            T2fillChartWrkInj();
            T2fillChartRFT();
          //  T2fillChartIE();
            Plant6Schart();
            //rftStatus();
            //IEeffiStatus();
            //pphStatus();
            //outputStatus();
            //erStatus();
            //// fillChartDB();
            //LLERStatus();
            injuryStatus();
            lineKaizenStatus();
                

            }
            finally
            {
                this.Cursor = Cursors.Default; // Reset the cursor when the operation is complete
            }

        }

        //Load department list
        //private void LoadTeam()
        //{
        //    autocompleteMenu1.Items = null;
        //    autocompleteMenu1.MaximumSize = new Size(350, 350);
        //    var columnWidth = new[] { 50, 250 };
        //    DataTable dt = GetAllTeams();
        //    int n = 1;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(
        //              new[] { n + "", dt.Rows[i]["team"].ToString() }, dt.Rows[i]["team"].ToString())
        //        { ColumnWidth = columnWidth, ImageIndex = n });
        //        n++;
        //    }
        //}

        ///     Get department list
        /// </summary>
        /// <returns></returns>
        //private DataTable GetAllTeams()
        //{
        //    DataTable dt = new DataTable();
        //    string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetAllTeams", Program.Client.UserToken, JsonConvert.SerializeObject(string.Empty));
        //    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
        //    {
        //        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
        //        dt = JsonConvert.DeserializeObject<DataTable>(json);
        //    }
        //    else
        //    {
        //        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
        //    }

        //    return dt;
        //}
        //private void AddButtonColumn()
        //{
        //    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
        //    btn.Name = "Cleared";
        //    btn.Text = "Cleared";
        //    btn.UseColumnTextForButtonValue = true;
        //    //dataGridView_tier2.Columns.Add(btn);
        //}


        //private void dataGridView_tier2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    var senderGrid = (DataGridView)sender;

        //    if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
        //    {
        //        DialogResult dialogResult = MessageBox.Show("Is this issue Cleared ?", "Confirm Deletion", MessageBoxButtons.YesNo);
        //        if (dialogResult == DialogResult.Yes)
        //        {
        //            dataGridView_tier2.Rows.RemoveAt(e.RowIndex);
        //        }
        //    }
        //}
        private void GetDayIE_Efficiency(string line)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", line);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetDayIE_Efficiency", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            // string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetDayIE_Efficiency", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                // dayFinishQty = int.Parse(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        // Tier1 Search Method
        private void Button29_Click(object sender, EventArgs e)
        {
           // GetformLoadData();
           // GetTeamDataRecord();
            // getFormLoadIssues();
            // getMachineLoadData();
           // rftStatus();
           // IEeffiStatus();
           // pphStatus();
          //  outputStatus();
          //  erStatus();
            // fillChartDB();
          //  LLERStatus();
          //  injuryStatus();
          //  lineKaizenStatus();

        }



        private async void Form1_Load(object sender, EventArgs e)
        {

            LoadTeam();
            
        }
        //Load department list
        private void LoadTeam()
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new Size(350, 350);
            var columnWidth = new[] { 50, 250 };
            DataTable dt = GetAllTeams();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(
                      new[] { n + "", dt.Rows[i]["team"].ToString() }, dt.Rows[i]["team"].ToString())
                { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        } 
        ///     Get department list
          /// </summary>
          /// <returns></returns>
        private DataTable GetAllTeams()
        {
            DataTable dt = new DataTable();
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetAllTeams", Program.Client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return dt;
        }


        private void GetformLoadData()
      {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", team_data.Text);
             p.Add("vstart_date", start_date.Text);

            
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetTeamData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
              {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

              
               
                ie_target = dtJson1.Rows[0]["IE_TARGET"].ToString();
                ie_acheivement = dtJson1.Rows[0]["IE_ACHEIVEMENT"].ToString();
                pph = dtJson1.Rows[0]["ACTUAL_PPH"].ToString();
                final_hc = dtJson1.Rows[0]["FINAL_HC"].ToString();
                ie_bonus = dtJson1.Rows[0]["IE_BONUS"].ToString();
       
            
                injury = dtJson1.Rows[0]["INJURY_COUNT"].ToString();
                LineKaizen = dtJson1.Rows[0]["kaizens"].ToString();

                ProdTarget.Text = production_target;
                ProdOutput.Text = production_output;
             
                LLER.Text = LLERatio;
                workInjury.Text = injury;
                LineKaizencount.Text = LineKaizen;
           
                R_FT.Text = rft_percentage;

              
               ieAchieve.Text = ie_acheivement;
                er.Text = ie_acheivement;
                // label15.Text = work_hours;
                actual_pph.Text = pph;
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
             //   Plantlbl.Text = Total_Cycle_Time;
                idlOper.Text = IdealOperators;
                // b_grade.Text = bgrade;
                // total_inspect.Text = total_inspected;

                //if (dtJson1.Rows.Count > 0)
                //{

                //    dataGridView_tier2.DataSource = dtJson1;

                //}
                //else
                //{
                //    MessageBox.Show("No such data");
                //}



            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        // GetTeamDataRecord
        private void GetTeamDataRecord()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vTeam", team_data.Text);
            p.Add("vstart_date", start_date.Text);


            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetTeamDataRecord", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);


                if (dtJson1.Rows.Count > 0)
                {


                    ProdTarget.Text = dtJson1.Rows[0]["TARGET_QTY"].ToString();
                    ProdOutput.Text = dtJson1.Rows[0]["OUTPUT_QTY"].ToString();
                    outputpercent = dtJson1.Rows[0]["OUTPUTPERCENT"].ToString();
                    mp.Text = dtJson1.Rows[0]["TEAM_MANPOWER"].ToString();
                    model.Text = dtJson1.Rows[0]["MODEL_NAMES"].ToString();
                    R_FT.Text = dtJson1.Rows[0]["RFT"].ToString();
                    idlOper.Text = dtJson1.Rows[0]["IDEALOPERATOR"].ToString();
                    LLER.Text = dtJson1.Rows[0]["LLER"].ToString();
                    label6.Text = dtJson1.Rows[0]["TargetPPH"].ToString();
                    actual_pph.Text = dtJson1.Rows[0]["ActualPPH"].ToString();
                    ieAchieve.Text = dtJson1.Rows[0]["IE_Achievement"].ToString();

                }
                else
                {
                    ProdTarget.Text = "0";
                    ProdOutput.Text = "0";
                    outputpercent = "0";
                    mp.Text = "0";
                    model.Text = "0";
                    R_FT.Text = "0";
                    idlOper.Text = "0";
                    LLER.Text = "0";
                    label6.Text = "0";
                    actual_pph.Text = "0";
                    ieAchieve.Text = "0";
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        //private void GetTeamDataRecord()
        //{
        //    Dictionary<string, Object> p = new Dictionary<string, object>();
        //    p.Add("vTeam", team_data.Text);
        //    p.Add("vstart_date", start_date.Text);


        //    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetTeamDataRecord", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

        //    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
        //    {
        //        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


        //        DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

        //        production_target = dtJson1.Rows[0]["TARGET_QTY"].ToString();
        //        production_output = dtJson1.Rows[0]["OUTPUT_QTY"].ToString();
        //        outputpercent = dtJson1.Rows[0]["OUTPUTPERCENT"].ToString();
        //        Manpower = dtJson1.Rows[0]["TEAM_MANPOWER"].ToString();
        //        Shoe_name = dtJson1.Rows[0]["MODEL_NAMES"].ToString();
        //       // production_output = dtJson1.Rows[0]["Articles"].ToString();
        //        rft_percentage = dtJson1.Rows[0]["RFT"].ToString();
        //        IdealOperators = dtJson1.Rows[0]["IDEALOPERATOR"].ToString();
        //        LLERatio = dtJson1.Rows[0]["LLER"].ToString();




        //    }
        //    else
        //    {
        //        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
        //    }
        //}

        //Team DownTime
        private void GetTeamDownTime()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vTeam", team_data.Text);
            p.Add("vstart_date", start_date.Text);


            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetTeamDownTime", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                if (dtJson1.Rows.Count > 0)
                {
                    Downtime.Text = dtJson1.Rows[0]["DownTime"].ToString();
                }
                else
                {
                    Downtime.Text = "0";
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //Team Safety
        private void GetTeamInjuries()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vTeam", team_data.Text);
            p.Add("vstart_date", start_date.Text);


            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetTeamInjuries", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                if (dtJson1.Rows.Count > 0)
                {
                    workInjury.Text = dtJson1.Rows[0]["Injury_Count"].ToString();
                }
                else
                {
                    workInjury.Text = "0";
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //Team Kaizen
        private void GetTeamKaizen()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vTeam", team_data.Text);
            p.Add("vstart_date", start_date.Text);


            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetTeamKaizen", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                if (dtJson1.Rows.Count > 0)
                {

                    kaizenTargetLBL.Text = dtJson1.Rows[0]["KaizenTargetPM"].ToString();
                    LineKaizencount.Text = dtJson1.Rows[0]["kaizen_count"].ToString();
                }
                else
                {
                    LineKaizencount.Text = "0";
                    kaizenTargetLBL.Text = "0";
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        //GetTeamIssues
        private void GetTeamIssues()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vTeam", team_data.Text);
            p.Add("vstart_date", start_date.Text);
            string team = team_data.Text;

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetTeamIssues", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);


                if (dtJson1.Rows.Count > 0)
                {

                    Team_Problems.Text = dtJson1.Rows[0]["issue_box_listtag"].ToString();
                    Team_Name.Text = dtJson1.Rows[0]["team"].ToString();


                }
                else
                {
                    Team_Problems.Text = "No Issues";
                    
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //GetPlantIssues
        private void GetPlantIssues()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vPlant", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);
            p.Add("vProcess", comboBox2.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetPlantIssues", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);


                if (dtJson1.Rows.Count > 0)
                {



                    Plant_Problems.Text = dtJson1.Rows[0]["issue_box_listtag"].ToString();
                    Plant_Name.Text = dtJson1.Rows[0]["plant"].ToString();

                }
                else
                {
                    Plant_Problems.Text = "No Issues";
                   

                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }



        //GetPlantDataRecord
        private void GetPlantDataRecord()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
              p.Add("vPlant", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);
            p.Add("vProcess", comboBox2.Text);


            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetPlantDataRecord", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);


                if (dtJson1.Rows.Count > 0)
                {


                    ProdTarget.Text = dtJson1.Rows[0]["TARGET_QTY"].ToString();
                    ProdOutput.Text = dtJson1.Rows[0]["OUTPUT_QTY"].ToString();
                    outputpercent = dtJson1.Rows[0]["OUTPUTPERCENT"].ToString();
                    mp.Text = dtJson1.Rows[0]["PLANT_MANPOWER"].ToString();
                    model.Text = dtJson1.Rows[0]["MODEL_NAMES"].ToString();
                    R_FT.Text = dtJson1.Rows[0]["RFT"].ToString();
                    idlOper.Text = dtJson1.Rows[0]["IDEALOPERATOR"].ToString();
                    LLER.Text = dtJson1.Rows[0]["LLER"].ToString();
                    label6.Text = dtJson1.Rows[0]["TargetPPH"].ToString();
                    actual_pph.Text = dtJson1.Rows[0]["ActualPPH"].ToString();
                    ieAchieve.Text = dtJson1.Rows[0]["IE_Achievement"].ToString();

                }
                else
                {
                    Downtime.Text = "No data";
                }

               



            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //GetPlantDownTime
        private void GetPlantDownTime()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vPlant", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);
            p.Add("vProcess", comboBox2.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetPlantDownTime", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                if (dtJson1.Rows.Count > 0)
                {
                    Downtime.Text = dtJson1.Rows[0]["DownTime"].ToString();
                }
                else
                {
                    Downtime.Text = "0";
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //Plant Safety
        private void GetPlantInjuries()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vPlant", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);
            p.Add("vProcess", comboBox2.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetPlantInjuries", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                if (dtJson1.Rows.Count > 0)
                {
                    workInjury.Text = dtJson1.Rows[0]["Injury_Count"].ToString();
                }
                else
                {
                    workInjury.Text = "0";
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //Plant 6S
        private void GetPlant_6S()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vPlant", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);
            p.Add("vProcess", comboBox2.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetPlant_6S", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                if (dtJson1.Rows.Count > 0)
                {
                    //Six_S_Count.Text = dtJson1.Rows[0]["score"].ToString();
                }
                else
                {
                  //  Six_S_Count.Text = "0";
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //Plant Kaizen
        private void GetPlantKaizen()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vPlant", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);
            p.Add("vProcess", comboBox2.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetPlantKaizen", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                if (dtJson1.Rows.Count > 0)
                {

                    kaizenTargetLBL.Text = dtJson1.Rows[0]["KAIZEN_TARGET"].ToString();
                    LineKaizencount.Text = dtJson1.Rows[0]["KAIZEN_COUNT"].ToString();
                }
                else
                {
                    LineKaizencount.Text = "0";
                    kaizenTargetLBL.Text = "0";
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        // Plant Kaizenstatus

        private void PlantKaizenStatus()
        {
            if (float.TryParse(LineKaizencount.Text, out float kaizensState))
            {
                if (float.TryParse(kaizenTargetLBL.Text, out float LKtarget))
                {
                    if (kaizensState > LKtarget)
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


        
        // GetPlantER 
        private void GetPlantER()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vPlant", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);
            p.Add("vProcess", comboBox2.Text);


            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetPlantER", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);


                if (dtJson1.Rows.Count > 0)
                {

                    er.Text = dtJson1.Rows[0]["ER_PERCENT"].ToString();

                }
                else
                {
                    er.Text = "0";
                }




            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        // PlantCOt
        private void GetPlantCOT()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vPlant", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);
            p.Add("vProcess", comboBox2.Text);


            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetPlantCOT", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);


                if (dtJson1.Rows.Count > 0)
                {

                    PlantCOT.Text = dtJson1.Rows[0]["cot"].ToString();

                }
                else
                {
                    PlantCOT.Text = "0";
                }




            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        // GetPlantPO_Completion
        private void GetPlantPO_Completion()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vPlant", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);
            p.Add("vProcess", comboBox2.Text);



            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetPlantPO_Completion", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);


                if (dtJson1.Rows.Count > 0)
                {

                    PO_Target.Text = dtJson1.Rows[0]["Total_Po"].ToString();
                    PO_QTY.Text = dtJson1.Rows[0]["OT_Finished"].ToString();
                    PO_Perc.Text = dtJson1.Rows[0]["PO_Completion_Perc"].ToString();

                }
                else
                {
                    PO_QTY.Text = "0";
                    PO_Perc.Text = "0";
                    PO_Target.Text = "0";
                }




            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        //T2 Plant Po completion chart
        private void ChartPlantPOcomp()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetPlantPO_Completion_chart", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        production_output = dtJson.Rows[0]["PO_Completion_Perc"] != DBNull.Value ? dtJson.Rows[0]["PO_Completion_Perc"].ToString() : "";
                        Req_date = dtJson.Rows[0]["PLANT"] != DBNull.Value ? dtJson.Rows[0]["PLANT"].ToString() : "";

                        //production_output = dtJson.Rows[0]["Delivery_percent"].ToString();
                        //Req_date = dtJson.Rows[0]["SCAN_DATE"].ToString();

                        POchart.Series["PO finish %"].XValueMember = "CDATE";
                        POchart.Series["PO finish %"].YValueMembers = "PO_Completion_Perc";

                        POchart.DataSource = dtJson;

                        POchart.Series["PO finish %"].ToolTip = "MONTH AVG PO COMPLETION  %: #VALY";

                        POchart.DataBind();

                        POchart.ChartAreas[0].AxisX.Interval = 1;

                        foreach (Series series in POchart.Series)
                        {
                            foreach (DataPoint point in series.Points)
                            {
                                if (point.YValues[0] >= 100)
                                {
                                    point.Color = Color.Green;
                                }

                                else if (point.YValues[0] >= 95 && point.YValues[0] < 100)
                                {
                                    point.Color = Color.Blue;
                                }

                                else if (point.YValues[0] < 95)
                                {
                                    point.Color = Color.Red;
                                }
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


        //T2 output chart
        private void ChartPlantOutput()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "TM2ChartProd", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

                            else if (point.YValues[0] >= 95 && point.YValues[0] < 100)
                            {
                                point.Color = Color.Blue;
                            }

                            else if (point.YValues[0] < 95)
                            {
                                point.Color = Color.Red;
                            }
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



        //T2 kaizen chart
        private void fillChartKaizenT2()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "PlantKaizensChart", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        PlantKaizenCRT = dtJson.Rows[0]["KAIZENS_COUNT"] != DBNull.Value ? dtJson.Rows[0]["KAIZENS_COUNT"].ToString() : "";

                        PlantKaizenChart.Series["Month kaizens"].XValueMember = "PLANT";
                        PlantKaizenChart.Series["Month kaizens"].YValueMembers = "KAIZENS_COUNT";

                        PlantKaizenCRT = dtJson.Rows[0]["KAIZENS_COUNT"] != DBNull.Value ? dtJson.Rows[0]["KAIZENS_COUNT"].ToString() : "";

                        LineKaizencount.Text = PlantKaizenCRT;



                        PlantKaizenChart.DataSource = dtJson;

                        PlantKaizenChart.Series["Month kaizens"].ToolTip = "Month Kaizen count: #VALY";

                        PlantKaizenChart.DataBind();
                    }
                    else
                    {
                        Console.WriteLine("No data available.");
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

        //T2 work injury chart
        private void T2fillChartWrkInj()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "TM2ChartWorkInjury", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

                        workInjury.Text = injury;

                        safetyChart.Series["work Injuries"].XValueMember = "IDATE";
                        safetyChart.Series["work Injuries"].YValueMembers = "INJURY_COUNT";

                        safetyChart.DataSource = dtJson;

                        safetyChart.Series["work Injuries"].ToolTip = "work Injuries count: #VALY";

                        safetyChart.DataBind();

                        safetyChart.ChartAreas[0].AxisX.Interval = 1;

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
        private void T2fillChartRFT()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "TM2ChartRFT", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

                        //production_output = dtJson.Rows[0]["RFTpercent"].ToString();
                        //Req_date = dtJson.Rows[0]["createdate"].ToString();


                        RFTChart.Series["RFT%"].XValueMember = "createdate";
                        RFTChart.Series["RFT%"].YValueMembers = "RFTpercent";

                        RFTChart.DataSource = dtJson;

                        RFTChart.Series["RFT%"].ToolTip = "RFT %: #VALY";

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


        //T2 IE chart 

        private void T2fillChartIE()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "IEchartByPLANT", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        // Setup chart data source
                        IEchart.DataSource = dtJson;
                        IEchart.Series["IE_Acheiv"].XValueMember = "PLANT";
                        IEchart.Series["IE_Acheiv"].YValueMembers = "IE_Acheivement";

                        // Customize tooltip to show IE_Achievement value
                        IEchart.Series["IE_Acheiv"].ToolTip = "Month Avg Achievement : #VALY";

                        // Data bind to reflect changes
                        IEchart.DataBind();

                        // Customize chart appearance
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


        //T2 6S chart 

        private void Plant6Schart()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", comboBox1.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "PLANT6S", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        // Setup chart data source
                        Chart6S.DataSource = dtJson;
                        Chart6S.Series["6S"].XValueMember = "PLANT";
                        Chart6S.Series["6S"].YValueMembers = "score";
                        PlantsixS = dtJson.Rows[0]["score"] != DBNull.Value ? dtJson.Rows[0]["score"].ToString() : "";

                        sixS_lbl.Text = PlantsixS;

                        // Customize tooltip to show IE_Achievement value
                        Chart6S.Series["6S"].ToolTip = "Plant 6S score : #VALY";

                        // Data bind to reflect changes
                        Chart6S.DataBind();

                        // Customize chart appearance
                        Chart6S.ChartAreas[0].AxisX.Interval = 1;

                        foreach (Series series in Chart6S.Series)
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
            //team_data.Text = line;
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
                if (rftParsed >= 90)
                {
                    rft_status.Text = "⚫";
                    rft_status.ForeColor = Color.Green;
                }
                else if (rftParsed < 90)
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
            if (float.TryParse(ie_acheivement, out float ieParsed))
            {
                if (ieParsed >= 70)
                {
                    ieEffiStatus.Text = "⚫";
                    ieEffiStatus.ForeColor = Color.Green;
                }
                else if (ieParsed < 70)
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
            if (float.TryParse(ie_acheivement, out float ieParsed))
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
                if (float.TryParse(pphTrg.Text, out float pphtg))
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
                if (outputper >= 90)
                {
                    outputstate.Text = "⚫";
                    outputstate.ForeColor = Color.Green;
                }
                else if (outputper < 90)
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
        //Team Output

        //private void TeamoutputStatus()
        //{
        //    if (float.TryParse(teamoutputpercent, out float outputper))
        //    {
        //        if (outputper >= 90)
        //        {
        //            outputstate.Text = "⚫";
        //            outputstate.ForeColor = Color.Green;
        //        }
        //        else if (outputper < 90)
        //        {
        //            outputstate.Text = "⚫";
        //            outputstate.ForeColor = Color.Red;
        //        }
        //    }
        //    else
        //    {
        //        outputstate.Text = "-";
        //        outputstate.ForeColor = Color.Black;
        //    }
        //}

        //Team Output Indicator

        private void TeamoutputStatus()
        {
            if (float.TryParse(outputpercent, out float outputper))
            {
                if (outputper >= 90)
                {
                    outputstate.Text = "⚫";
                    outputstate.ForeColor = Color.Green;
                }
                else if (outputper < 90)
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

        //Team RFT Indicator

        private void TeamRFTStatus()
        {
            // float percentageFloat = float.Parse(percentageString.TrimEnd('%')) / 100;
            if (float.TryParse(R_FT.Text.TrimEnd('%'), out float outputper))
            // if (float.TryParse(R_FT.Text, out float outputper))
            {
                if (outputper >= 90)
                {
                    rft_status.Text = "⚫";
                    rft_status.ForeColor = Color.Green;
                }
                else if (outputper < 90)
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
        //Team TeampphStatus

        private void TeampphStatus()
        {
            if (float.TryParse(actual_pph.Text, out float pphvalue))
            {
                if (pphvalue >= 3.5)
                {
                    pphState.Text = "⚫";
                    pphState.ForeColor = Color.Green;
                }
                else if (pphvalue < 3.5)
                {
                    pphState.Text = "⚫";
                    pphState.ForeColor = Color.Red;
                }
            }
            else
            {
                pphState.Text = "-";
                pphState.ForeColor = Color.Black;
            }
        }

        // TeamLLERStatus

        private void TeamLLERStatus()
        {
            if (float.TryParse(LLER.Text, out float stateLLER))
            {
                if (stateLLER > 80)
                {
                    statusLLER.Text = "⚫";
                    statusLLER.ForeColor = Color.Green;
                }
                else if (stateLLER < 80)
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


        //Team IEefficiency
        private void TeamIEefficiencyStatus()
        {
            if (float.TryParse(ieAchieve.Text, out float ieParsed))
            {
                if (ieParsed >= 70)
                {
                    ieEffiStatus.Text = "⚫";
                    ieEffiStatus.ForeColor = Color.Green;
                }
                else if (ieParsed < 70)
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

        // Team DownTime

        private void TeamDowntimeStatus()
        {
            if (int.TryParse(Downtime.Text, out int downtimeValue))
            //  if (float.TryParse(Downtime.Text, out float Dt))
            {
                if (downtimeValue < 0)
                {
                    dtStatus.Text = "⚫";
                    dtStatus.ForeColor = Color.Green;
                }
                else if (downtimeValue > 0)
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

        // GetTeamER 
        private void GetTeamER()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vTeam", team_data.Text);
            p.Add("vstart_date", start_date.Text);


            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetTeamER", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);


                if (dtJson1.Rows.Count > 0)
                {

                    er.Text = dtJson1.Rows[0]["ER_PERCENT"].ToString();

                }
                else
                {
                    er.Text = "0";
                }




            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //Team erStatus
        private void TeamerStatus()
        {
            if (float.TryParse(er.Text, out float ieParsed))
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

        //Plant COT
        private void PlantCOTStatus()
        {
            if (float.TryParse(PlantCOT.Text, out float co))
            {
                if (co <= 24)
                {
                    colorPlantCOT.Text = "⚫";
                    colorPlantCOT.ForeColor = Color.Green;
                }
                else if (co > 24)
                {
                    colorPlantCOT.Text = "⚫";
                    colorPlantCOT.ForeColor = Color.Red;
                }
            }
            else 
            {
                colorPlantCOT.Text = "-";
                colorPlantCOT.ForeColor = Color.Black;
            }
        }
        // GetTeamPO_Completion
        private void GetTeamPO_Completion()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vTeam", team_data.Text);
            p.Add("vstart_date", start_date.Text);


            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetTeamPO_Completion", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();


                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);


                if (dtJson1.Rows.Count > 0)
                {

                    PO_Target.Text = dtJson1.Rows[0]["Total_Po"].ToString();
                    PO_QTY.Text = dtJson1.Rows[0]["OT_Finished"].ToString();
                    PO_Perc.Text = dtJson1.Rows[0]["PO_Completion_Perc"].ToString();

                }
                else
                {
                    PO_QTY.Text = "0";
                    PO_Perc.Text = "0";
                    PO_Target.Text = "0";
                }




            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        //TeamPO_Completion_Status
        //private void PlantPO_Completion_Status()
        //{
        //    //if (float.TryParse(PO_Perc.Text, out float ieParsed))
        //    //{
        //    //    if (ieParsed >= 100)
        //    //    {
        //    //        PO_Perc.Text = "⚫";
        //    //        PO_Perc.ForeColor = Color.Green;
        //    //    }
        //    //    else if (ieParsed < 100)
        //    //    {
        //    //        PO_Perc.Text = "⚫";
        //    //        PO_Perc.ForeColor = Color.Red;
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    PO_Perc.Text = "-";
        //    //    PO_Perc.ForeColor = Color.Black;
        //    //}
        //}


        //TeamPO_Completion_Status
        private void TeamPO_Completion_Status()
        {
            if (float.TryParse(PO_Perc.Text, out float ieParsed))
            {
                if (ieParsed >= 100)
                {
                    PO_Perc.Text = "⚫";
                    PO_Perc.ForeColor = Color.Green;
                }
                else if (ieParsed < 100)
                {
                    PO_Perc.Text = "⚫";
                    PO_Perc.ForeColor = Color.Red;
                }
            }
            else
            {
                PO_Perc.Text = "-";
                PO_Perc.ForeColor = Color.Black;
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
                if (stateLLER > 80)
                {
                    statusLLER.Text = "⚫";
                    statusLLER.ForeColor = Color.Green;
                }
                else if (stateLLER < 80)
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
                if (InjuryState <= 0)
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
        //Plant InjuryStatus
        private void PlantInjuryStatus()
        {
            if (float.TryParse(workInjury.Text, out float InjuryState))
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


        //Plant erStatus
        private void Plant_ERStatus()
        {
            if (float.TryParse(er.Text, out float ieParsed))
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
        //PlantPO_Completion_Status
        private void PlantPO_Completion_Status()
        {
            if (float.TryParse(PO_Perc.Text, out float ieParsed))
            {
                if (ieParsed >= 100)
                {
                    PO_Perc.Text = "⚫";
                    PO_Perc.ForeColor = Color.Green;
                }
                else if (ieParsed < 100)
                {
                    PO_Perc.Text = "⚫";
                    PO_Perc.ForeColor = Color.Red;
                }
            }
            else
            {
                PO_Perc.Text = "-";
                PO_Perc.ForeColor = Color.Black;
            }
        }

        //Plant 6S_Status
        private void SIXS_Status()
        {
            if (float.TryParse(sixS_lbl.Text, out float ieParsed))
            {
                if (ieParsed >= 85)
                {
                    sixSstate.Text = "⚫";
                    sixSstate.ForeColor = Color.Green;
                }
                else if (ieParsed >= 75 && ieParsed < 85)
                {
                    sixSstate.Text = "⚫";
                    sixSstate.ForeColor = Color.Blue;
                }

                else if (ieParsed < 75)
                {
                    sixSstate.Text = "⚫";
                    sixSstate.ForeColor = Color.Red;
                }
            }
            else
            {
                sixSstate.Text = "-";
                sixSstate.ForeColor = Color.Black;
            }
        }

        private void lineKaizenStatus()
        {
            if (float.TryParse(PlantKaizenCRT, out float kaizensState))
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

        private void GetTeam()
        {
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetTeam", Program.Client.UserToken, string.Empty);
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
            p.Add("vline", team_data.Text);
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
            p.Add("vline", team_data.Text);
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
            p.Add("vteam", team_data.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetTeamData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));


            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

               // ProdTarget.Text = production_target;
                ProdOutput.Text = production_output;
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

        //private void textDept_TextChanged(object sender, EventArgs e)

        //{

           
        //    LoadTeam();
        //}
        

        private async void Button29_Click_1(object sender, EventArgs e)

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
                GetTeamDataRecord();
            GetTeamDownTime();
            GetTeamInjuries();
            GetTeamKaizen();
            TeamoutputStatus();
            TeamRFTStatus();
            TeampphStatus();
            TeamLLERStatus();
            TeamIEefficiencyStatus();
            TeamDowntimeStatus();
            GetTeamER();
            TeamerStatus();
            GetTeamPO_Completion();
            TeamPO_Completion_Status();
            
            PlantPO_Completion_Status();
                GetTeamKaizen();
                GetTeamIssues();

            }
            finally
            {
                this.Cursor = Cursors.Default; // Reset the cursor when the operation is complete
            }

            //Dictionary<string, Object> p = new Dictionary<string, object>();

            //var team = team_data.Text.ToUpper();
            //p.Add("vTeam", team);
            //p.Add("vstart_date", start_date.Text);

            //string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetTeamData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));


            //if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            //{
            //    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
            //    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

            //    outputpercent = dtJson.Rows[0]["TEAM_PROD_OUTPUT"].ToString();

            //    // ProdOutput.Text = production_output;
            //    ProdOutput.Text = dtJson.Rows[0]["Team_Prod_Output"].ToString();

            //    //if (dtJson.Rows.Count > 0)
            //    //{

            //    //    //dataGridView_tier2.DataSource = dtJson;

            //    //}
            //    //else
            //    //{
            //    //    MessageBox.Show("No such data");
            //    //}

            //}
            //else
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            //}
            //GetformLoadData();
            // getFormLoadIssues();
            //getMachineLoadData();
            // this.Refresh();
        }

        private void Panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
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

                ChartPlantOutput();
            fillChartKaizenT2();
            T2fillChartWrkInj();
            T2fillChartRFT();
           // T2fillChartIE();
            Plant6Schart();
            injuryStatus();
                //  lineKaizenStatus();

                GetPlantDataRecord();
                GetPlantDownTime();
                GetPlantInjuries();
                GetPlant_6S();
                GetPlantKaizen();
                TeamoutputStatus();
                TeamRFTStatus();
                TeampphStatus();
                TeamLLERStatus();
                TeamIEefficiencyStatus();
                TeamDowntimeStatus();
                PlantKaizenStatus();
                PlantInjuryStatus();
                GetPlantER();
                Plant_ERStatus();
                GetPlantPO_Completion();
                PlantPO_Completion_Status();
                ChartPlantPOcomp();
                GetPlantIssues();

                SIXS_Status();
                GetPlantCOT();
                PlantCOTStatus();

            }
            finally
            {
                this.Cursor = Cursors.Default; // Reset the cursor when the operation is complete
            }
        }
    }
}
