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
    public partial class Form4 : Form
    {
        //private Timer timer3;
       
        private int angle = 0;


        string orgKaizenCOUNT = "";
      
        string ORGkAIZENCH = "";
        string OrgDTChat = "";
        string orgDTval = "";
        string orgSME = "";
        string orgSMEchat = "";
        string orgCot = "";
        string orgCOchat = "";
        string orgCopt = "";
        string orgRut = "";
        string trgPPH = "";
        string ActPPH = "";
        string IE_Ach = "";
        string orgPPHchat = "";
        string orgIEchat = "";
        string orgIEchatTop = "";
        string orgIEchatbottom = "";
        string PIE = "";


        private Label loadingLabel;

        public Form4()
        {
            InitializeComponent();
            // AddButtonColumn();
            //dataGridView_tier2.CellContentClick += dataGridView_tier2_CellContentClick;
            timer2 = new Timer();
            
            timer2.Interval = 600000; //10 min

            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Start(); // Start the timer


            // Initialize the loading icon before starting the timer

            loadingLabel = new Label();
            loadingLabel.Text = "Loading...";
            loadingLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            loadingLabel.ForeColor = Color.Blue;
            loadingLabel.AutoSize = true;
            loadingLabel.Visible = false;

            // Add the loading label to the form
            Controls.Add(loadingLabel);




        }

        private async void timer2_Tick(object sender, EventArgs e)
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
            
            kaizenORG();
           orgKaizenStatus(); 
           fillChartKaizenT4();
            fillChartDT();
            orgDTstate();
            DTbyORG();
            SMEbyORG();
            fillChartSME();
            orgSMEstate();
            ChangeOverbyORG();
            fillChartChangeover();
            orgCOTstate();
            orgCOPTstate();
            orgRUTstate();
            PPH_IEachiByORG();
            IEeffiStatus();
            OrgPPHStatus();
            fillChartPPH();
            fillChartIE();
            fillChartIEtopThree();
            fillChartIEBottomThree();
            FillPiChartIE();

        }
            finally
            {
                this.Cursor = Cursors.Default; // Reset the cursor when the operation is complete
            }

}

       

        // Tier1 Search Method
        private void Button29_Click(object sender, EventArgs e)
        {
            kaizenORG();
            orgKaizenStatus();
            fillChartKaizenT4();
           
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

                
            kaizenORG();
           
            this.Refresh();
            
            orgKaizenStatus();
           
            fillChartKaizenT4();
            fillChartDT();
            orgDTstate();
            DTbyORG();
            SMEbyORG();
            fillChartSME();
            orgSMEstate();
            ChangeOverbyORG();
            fillChartChangeover();
            orgCOTstate();
            orgCOPTstate();
            orgRUTstate();
            PPH_IEachiByORG();
            IEeffiStatus();
            OrgPPHStatus();
            fillChartPPH();
            fillChartIE();
            fillChartIEtopThree();
            fillChartIEBottomThree();
            FillPiChartIE();

            }
            finally
            {
                this.Cursor = Cursors.Default; // Reset the cursor when the operation is complete
            }


        }


       


       private void kaizenORG()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vstart_date", start_date.Text);
            p.Add("vline", OrgCombo.Text);
          
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "ORGKaizens", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                if (dtJson1.Rows.Count > 0) 
                {
                    orgKaizenCOUNT = dtJson1.Rows[0]["KAIZENS_COUNT"].ToString();
                    ORGKaizens.Text = orgKaizenCOUNT;

                    
                }
                else
                {
                   
                    ORGKaizens.Text = "0";
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }





        private void fillChartKaizenT4()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            // p.Add("vline", textDept.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "TM4ChartKaizen", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        ORGkAIZENCH = dtJson.Rows[0]["KAIZENS_COUNT"] != DBNull.Value ? dtJson.Rows[0]["KAIZENS_COUNT"].ToString() : "";

                        T4kaizenChart.Series["Month kaizens"].XValueMember = "LINE_CATEGORY";
                        T4kaizenChart.Series["Month kaizens"].YValueMembers = "KAIZENS_COUNT";

                        T4kaizenChart.DataSource = dtJson;

                        T4kaizenChart.Series["Month kaizens"].ToolTip = "Month Kaizen count: #VALY";

                        T4kaizenChart.DataBind();
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


        private void orgKaizenStatus()
        {
            if (float.TryParse(ORGKaizens.Text, out float kaizensState))
            {
                if (float.TryParse(orgKaizenTrgt.Text, out float orgKtarget))
                {
                    if (kaizensState >= orgKtarget)
                    {
                        kaizenStatus.Text = "⚫";
                        kaizenStatus.ForeColor = Color.Green;
                    }
                    else if (kaizensState < orgKtarget)
                    {
                        kaizenStatus.Text = "⚫";
                        kaizenStatus.ForeColor = Color.Red;
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
                kaizenStatus.Text = "-";
                kaizenStatus.ForeColor = Color.Black;
            }
        }

        private void orgDTstate()
        {
            if (float.TryParse(orgDTval, out float OrgDTState))
            {
               
                    if (OrgDTState <= 0)
                    {
                        dtStatusORG.Text = "⚫";
                        dtStatusORG.ForeColor = Color.Green;
                    }
                    else if (OrgDTState > 0)
                    {
                        dtStatusORG.Text = "⚫";
                        dtStatusORG.ForeColor = Color.Red;
                    }

            }
            else
            {
                dtStatusORG.Text = "-";
                dtStatusORG.ForeColor = Color.Black;
            }
        }

        private void DTbyORG()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vstart_date", start_date.Text);
            p.Add("vline", OrgCombo.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "DTofORG", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                if (dtJson1.Rows.Count > 0)
                {
                    orgDTval = dtJson1.Rows[0]["pausehour"].ToString();
                    orgDT.Text = orgDTval;
                }
                else
                {

                    orgDT.Text = "0";
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //org SME
        private void SMEbyORG()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vstart_date", start_date.Text);
            p.Add("vline", OrgCombo.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "SMEofORG", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                if (dtJson1.Rows.Count > 0)
                {
                    orgSME = dtJson1.Rows[0]["avg_smescore"].ToString();
                    ORGsmeLbl.Text = orgSME;
                }
                else
                {

                    ORGsmeLbl.Text = "0";
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

       

        private void fillChartDT()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            // p.Add("vline", textDept.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "ORGChartDT", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        OrgDTChat = dtJson.Rows[0]["pausehour"] != DBNull.Value ? dtJson.Rows[0]["pausehour"].ToString() : "";





                        OrgDTChart.Series["DT"].XValueMember = "ORG_name";
                        OrgDTChart.Series["DT"].YValueMembers = "pausehour";

                        OrgDTChart.DataSource = dtJson;

                        OrgDTChart.Series["DT"].ToolTip = "Down Time: #VALY";

                        OrgDTChart.DataBind();

                        OrgDTChart.ChartAreas[0].AxisX.Interval = 1;

                        foreach (Series series in OrgDTChart.Series)
                        {
                            foreach (DataPoint point in series.Points)
                            {
                                if (point.YValues[0] <= 0)
                                {
                                    point.Color = Color.DarkGreen;
                                }
                                else
                                {
                                    point.Color = Color.OrangeRed;
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


        private void fillChartSME()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            // p.Add("vline", textDept.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "ORGChartSME", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        orgSMEchat = dtJson.Rows[0]["avg_smescore"] != DBNull.Value ? dtJson.Rows[0]["avg_smescore"].ToString() : "";

                    



                    OrgSMEchart.Series["SME"].XValueMember = "org_name";
                    OrgSMEchart.Series["SME"].YValueMembers = "avg_smescore";

                    OrgSMEchart.DataSource = dtJson;

                    OrgSMEchart.Series["SME"].ToolTip = "SME: #VALY";


                        OrgSMEchart.DataBind();

                        foreach (Series series in OrgSMEchart.Series)
                        {
                            foreach (DataPoint point in series.Points)
                            {
                                if (point.YValues[0] >= 80)
                                {
                                    point.Color = Color.Green;
                                }
                                else if (point.YValues[0] >= 75 && point.YValues[0] <80)
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

        private void orgSMEstate()
        {
            if (float.TryParse(orgSME, out float OrgsmeState))
            {

                if (OrgsmeState >= 80)
                {
                    SmeSI.Text = "⚫";
                    SmeSI.ForeColor = Color.Green;
                }
                else if (OrgsmeState >= 75 && OrgsmeState < 80)
                {
                    SmeSI.Text = "⚫";
                    SmeSI.ForeColor = Color.Blue;
                }

                else if (OrgsmeState < 75)
                {
                    SmeSI.Text = "⚫";
                    SmeSI.ForeColor = Color.Red;
                }

            }
            else
            {
                SmeSI.Text = "-";
                SmeSI.ForeColor = Color.Black;
            }
        }

        private void ChangeOverbyORG()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vstart_date", start_date.Text);
            p.Add("vline", OrgCombo.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "changeoverbyORG", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                if (dtJson1.Rows.Count > 0)
                {
                    orgCot = dtJson1.Rows[0]["cot"].ToString();
                    orgCopt = dtJson1.Rows[0]["copt"].ToString();
                    orgRut = dtJson1.Rows[0]["rut"].ToString();

                    cotLbl.Text = orgCot;
                    coptLbl.Text = orgCopt;
                    rutLbl.Text = orgRut;
                }
                else
                {

                    cotLbl.Text = "0";
                    coptLbl.Text = "0";
                    rutLbl.Text = "0";
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void orgCOTstate()
        {
            if (float.TryParse(orgCot, out float OrgCotState))
            {

                if (OrgCotState <= 24)
                {
                    statusInCOT.Text = "⚫";
                    statusInCOT.ForeColor = Color.Green;
                }
              

                else if (OrgCotState >24 )
                {
                    statusInCOT.Text = "⚫";
                    statusInCOT.ForeColor = Color.Red;
                }

            }
            else
            {
                statusInCOT.Text = "-";
                statusInCOT.ForeColor = Color.Black;
            }
        }

        private void orgCOPTstate()
        {
            if (float.TryParse(orgCopt, out float OrgCoptState))
            {

                if (OrgCoptState < 78)
                {
                    coptSI.Text = "⚫";
                    coptSI.ForeColor = Color.Green;
                }


                else if (OrgCoptState > 77)
                {
                    coptSI.Text = "⚫";
                    coptSI.ForeColor = Color.Red;
                }

            }
            else
            {
                coptSI.Text = "-";
                coptSI.ForeColor = Color.Black;
            }
        }

        private void orgRUTstate()
        {
            if (float.TryParse(orgRut, out float OrgRutState))
            {

                if (OrgRutState <= 24)
                {
                   rutSI.Text = "⚫";
                   rutSI.ForeColor = Color.Green;
                }


                else if (OrgRutState > 24)
                {
                    rutSI.Text = "⚫";
                    rutSI.ForeColor = Color.Red;
                }

            }
            else
            {
                rutSI.Text = "-";
                rutSI.ForeColor = Color.Black;
            }
        }

       

        private void fillChartChangeover()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", OrgCombo.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "changeoverCHARTbyORG", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        orgCOchat = dtJson.Rows[0]["org"] != DBNull.Value ? dtJson.Rows[0]["org"].ToString() : "";






                        ChangeOverchart.Series["COT"].XValueMember = "CO_DATE";
                        ChangeOverchart.Series["COT"].YValueMembers = "COT";
                        ChangeOverchart.Series["COPT"].XValueMember = "CO_DATE";
                        ChangeOverchart.Series["COPT"].YValueMembers = "COPT";
                        ChangeOverchart.Series["RUT"].XValueMember = "CO_DATE";
                        ChangeOverchart.Series["RUT"].YValueMembers = "RUT";



                        ChangeOverchart.DataSource = dtJson;

                        ChangeOverchart.Series["COT"].ToolTip = "Actual COT: #VALY";
                        ChangeOverchart.Series["COPT"].ToolTip = "Actual COPT: #VALY";
                        ChangeOverchart.Series["RUT"].ToolTip = "Actual RUT: #VALY";

                        ChangeOverchart.DataBind();

                        ChangeOverchart.ChartAreas[0].AxisX.Interval = 1;

                        
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


        //org pph IE achi
        private void PPH_IEachiByORG()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vstart_date", start_date.Text);
            p.Add("vline", OrgCombo.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "PPH_IEAofORG", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                if (dtJson1.Rows.Count > 0)
                {
                    trgPPH = dtJson1.Rows[0]["TARGET_PPH"].ToString();
                    ActPPH = dtJson1.Rows[0]["ACTUAL_PPH"].ToString();
                    IE_Ach = dtJson1.Rows[0]["IE_Acheivement"].ToString();

                    PPhTrgLBL.Text = trgPPH;
                    actual_pphLBL.Text = ActPPH;
                    ieAchieveLbl.Text = IE_Ach;
                }
                else
                {

                    PPhTrgLBL.Text = "0";
                    actual_pphLBL.Text = "0";
                    ieAchieveLbl.Text = "0";
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void IEeffiStatus() 
        {
            if (float.TryParse(IE_Ach, out float ieParsed))
            {
                if (ieParsed >= 80)
                {
                    orgIEstate.Text = "⚫";
                    orgIEstate.ForeColor = Color.Green;
                }
                else if (ieParsed >= 75 && ieParsed < 80)
                {
                    orgIEstate.Text = "⚫";
                    orgIEstate.ForeColor = Color.Blue;
                }

                else if (ieParsed < 75)
                {
                    orgIEstate.Text = "⚫";
                    orgIEstate.ForeColor = Color.Red;
                }
            }
            else
            {
                orgIEstate.Text = "-";
                orgIEstate.ForeColor = Color.Black;
            }
        }

        private void OrgPPHStatus()
        {
            if (float.TryParse(ActPPH, out float ActPPHState))
            {
                if (float.TryParse(trgPPH, out float trgtPPH))
                {
                    if (ActPPHState >= trgtPPH)
                    {
                        OrgPPHState.Text = "⚫";
                        OrgPPHState.ForeColor = Color.Green;
                    }
                    else if (ActPPHState < trgtPPH)
                    {
                        OrgPPHState.Text = "⚫";
                        OrgPPHState.ForeColor = Color.Red;
                    }
                  
                }

            }
            else
            {
                OrgPPHState.Text = "-";
                OrgPPHState.ForeColor = Color.Black;
            }
        }


        //PPH chart 

        private void fillChartPPH()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
           // p.Add("vline", OrgCombo.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "PPHchartByORG", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        orgPPHchat = dtJson.Rows[0]["ORG_name"] != DBNull.Value ? dtJson.Rows[0]["ORG_name"].ToString() : "";

                        ORGchartPPH.Series["Trg_PPH"].XValueMember = "ORG_name";
                        ORGchartPPH.Series["Trg_PPH"].YValueMembers = "TARGET_PPH";
                        ORGchartPPH.Series["Act_PPH"].XValueMember = "ORG_name";
                        ORGchartPPH.Series["Act_PPH"].YValueMembers = "ACTUAL_PPH";


                        ORGchartPPH.DataSource = dtJson;

                        // Customize tooltip to show pph value
                        ORGchartPPH.Series["Act_PPH"].ToolTip = "Month Avg Actual PPH: #VALY";
                        ORGchartPPH.Series["Trg_PPH"].ToolTip = "Month Avg Target PPH: #VALY";

                        ORGchartPPH.DataBind();

                        ORGchartPPH.ChartAreas[0].AxisX.Interval = 1;

                       
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


        //IE chart 

        private void fillChartIE()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            // Additional parameters setup if needed
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "PPHchartByORG", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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
                        IEchart.Series["IE_Acheiv"].XValueMember = "ORG_name";
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




        //IE chart Top 3

        private void fillChartIEtopThree()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
             p.Add("vline", OrgCombo.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "TopIEchartByORG", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        orgIEchatTop = dtJson.Rows[0]["MAX_TCT_MODEL_ARTICLE"] != DBNull.Value ? dtJson.Rows[0]["MAX_TCT_MODEL_ARTICLE"].ToString() : "";

                        TopThreeIEchart.Series["Top-3"].XValueMember = "MAX_TCT_MODEL_ARTICLE";
                        TopThreeIEchart.Series["Top-3"].YValueMembers = "IE_Acheivement";


                        TopThreeIEchart.DataSource = dtJson;

                        // Customize tooltip to show IE_Achievement value
                        TopThreeIEchart.Series["Top-3"].ToolTip = "Achievement: #VALY";
                        

                        TopThreeIEchart.DataBind();

                        TopThreeIEchart.ChartAreas[0].AxisX.Interval = 1;

                       
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


        //IE chart Bottom 3

        private void fillChartIEBottomThree()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", OrgCombo.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "BottomIEchartByORG", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                try
                {
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        orgIEchatbottom = dtJson.Rows[0]["MAX_TCT_MODEL_ARTICLE"] != DBNull.Value ? dtJson.Rows[0]["MAX_TCT_MODEL_ARTICLE"].ToString() : "";

                        BottomThreeIEchart.Series["Bottom-3"].XValueMember = "MAX_TCT_MODEL_ARTICLE";
                        BottomThreeIEchart.Series["Bottom-3"].YValueMembers = "IE_Acheivement";


                        BottomThreeIEchart.DataSource = dtJson;

                        BottomThreeIEchart.Series["Bottom-3"].ToolTip = "Acheivement: #VALY";

                        BottomThreeIEchart.DataBind();

                        BottomThreeIEchart.ChartAreas[0].AxisX.Interval = 1;

                       
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


        //IE PI chart 

        private void FillPiChartIE()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", OrgCombo.Text);
            p.Add("vstart_date", start_date.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "PIEchartByORG", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();

                try
                {
                    
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                    if (dtJson.Rows.Count > 0)
                    {
                        // Clear existing points
                        PIchartIE.Series["Arc"].Points.Clear();

                        // Set the chart type to Pie
                        PIchartIE.Series["Arc"].ChartType = SeriesChartType.Pie;

                        // Set X and Y value members
                        PIchartIE.Series["Arc"].Points.DataBind(dtJson.Rows, "MAX_TCT_MODEL_ARTICLE", "IE_Acheivement", "");

                        // Show data labels for each point
                        PIchartIE.Series["Arc"].IsValueShownAsLabel = true;

                        // Set the chart area properties
                        PIchartIE.ChartAreas[0].AxisX.Interval = 1;

                        // Clear existing legends
                        PIchartIE.Legends.Clear();

                        // Optionally, add a new legend
                        Legend legend = new Legend();
                        PIchartIE.Legends.Add(legend);
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


        







        //Load Form Production Issues
        private void GetIssues()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
          //  p.Add("vline", textDept.Text);
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
       
        private void Tier2_Search_Click(object sender, EventArgs e)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
          //  p.Add("vline", textDept.Text);
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

        private async void TierFourSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Simulating an asynchronous data loading operation
                await Task.Run(() =>
                {

                    System.Threading.Thread.Sleep(5000); // Simulating a 5-second delay
                });

                kaizenORG();
            this.Refresh();
            orgKaizenStatus();
            fillChartKaizenT4();
            fillChartDT();
            orgDTstate();
            DTbyORG();
            SMEbyORG();
            fillChartSME();
            orgSMEstate();
            ChangeOverbyORG();
            fillChartChangeover();
            orgCOTstate();
            orgCOPTstate();
            orgRUTstate();
            PPH_IEachiByORG();
            IEeffiStatus();
            OrgPPHStatus();
            fillChartPPH();
            fillChartIE();
            fillChartIEtopThree();
            fillChartIEBottomThree();
            FillPiChartIE();

            }
            finally
            {
                this.Cursor = Cursors.Default; // Reset the cursor when the operation is complete
            }

        }

        


    }
}
