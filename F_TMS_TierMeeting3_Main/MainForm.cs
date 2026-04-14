using F_TMS_TierMeeting3_Main.Delivery;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ExcelApp = Microsoft.Office.Interop.Excel;

namespace F_TMS_TierMeeting3_Main
{
    public partial class MainForm : MaterialForm
    {
        private string strPath = Parameters.strPath;
        private int RFTTarget = 90;
        private int SDPTarget = 98;
        private int WITarget = 0;
        private string dept = "";
        private string userPlant = "";
        private string userSection = "";
        private int type = 0;
        private Color textColor = Color.White;
        private DateTime currentDate;
        private delegate void SetTextCallback(TextBox txt, string text);
        private delegate void SetBackColorCallback(TextBox txt, Color color);
        private delegate void GetDataCallback();
        Waiting wait = new Waiting();
        public MainForm()
        {
            //wait.Show();
            InitializeComponent();
            InitUI();
        }



        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        private void GetDepartmentByUser() {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetDepartmentByUser",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                //tier 3 - plant
                var tmp = !string.IsNullOrEmpty(dtJson.Rows[0]["UDF05"].ToString()) ? dtJson.Rows[0]["UDF05"].ToString() : dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
                type = (int)Parameters.QueryDeptType.Plant;
                txtDepartment.Text = tmp;
                this.userPlant = tmp;
                this.userSection = tmp;
                this.dept = tmp;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        #region Init
        private void SafetyKaizen_Load(object sender, EventArgs e)
        {
            SetBG();
            GetDepartmentByUser();
            GetDate();
            GetData();
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            wait.Hide();
        }
        private void SetBG()
        {
            tableLayoutPanel2.BackColor = Color.FromArgb(25, Color.Black);
            tableLayoutPanel3.BackColor = Color.FromArgb(25, Color.Black);
            tableLayoutPanel4.BackColor = Color.FromArgb(25, Color.Black);
            tableLayoutPanel5.BackColor = Color.FromArgb(25, Color.Black);
            tableLayoutPanel6.BackColor = Color.FromArgb(25, Color.Black);
            tableLayoutPanel7.BackColor = Color.FromArgb(25, Color.Black);
            tableLayoutPanel8.BackColor = Color.FromArgb(25, Color.Black);
            tableLayoutPanel9.BackColor = Color.FromArgb(25, Color.Black);
            tableLayoutPanel10.BackColor = Color.FromArgb(25, Color.Black);
            tableLayoutPanel11.BackColor = Color.FromArgb(25, Color.Black);
            tableLayoutPanel12.BackColor = Color.FromArgb(25, Color.Black);
            tableLayoutPanel13.BackColor = Color.FromArgb(25, Color.Black);
            panel2.BackColor = Color.FromArgb(25, Color.Black);

            lblSafetyUntilDate.BackColor = Color.FromArgb(25, Color.Black);
            lblSafetyByDate.BackColor = Color.FromArgb(25, Color.Black);
            lblSafetyDays.BackColor = Color.FromArgb(25, Color.Black);
            lblRFT.BackColor = Color.FromArgb(25, Color.Black);
            lblSDP.BackColor = Color.FromArgb(25, Color.Black);
            lblPOCompletionRate.BackColor = Color.FromArgb(25, Color.Black);
            lblPPH.BackColor = Color.FromArgb(25, Color.Black);
            lblAccept.BackColor = Color.FromArgb(25, Color.Black);
            lblAcceptPerPeople.BackColor = Color.FromArgb(25, Color.Black);

            chartMainBar.BackColor = Color.FromArgb(25, Color.Black);
            chartMainPie.BackColor = Color.FromArgb(25, Color.Black);
            chartDelivery.BackColor = Color.FromArgb(25, Color.Black);
            chartEfficiency.BackColor = Color.FromArgb(25, Color.Black);
            chartKaizen.BackColor = Color.FromArgb(25, Color.Black);
            chartQuality.BackColor = Color.FromArgb(25, Color.Black);

            lblTier3Header.ForeColor = textColor;
            lblSafetyHeader.ForeColor = Color.LightGreen;
            lblQualityHeader.ForeColor = Color.LightGreen;
            lblDeliveryHeader.ForeColor = Color.LightGreen;
            lblEfficiencyHeader.ForeColor = Color.LightGreen;
            lblKaizenHeader.ForeColor = Color.LightGreen;
            lblOtherHeader.ForeColor = Color.LightGreen;

            lblSafetyUntilDate.ForeColor = textColor;
            lblSafetyByDate.ForeColor = textColor;
            lblSafetyDays.ForeColor = textColor;
            lblRFT.ForeColor = textColor;
            lblSDP.ForeColor = textColor;
            lblPOCompletionRate.ForeColor = textColor;
            lblPPH.ForeColor = textColor;
            lblAccept.ForeColor = textColor;
            lblAcceptPerPeople.ForeColor = textColor;


            chartEfficiency.ChartAreas[0].BackColor = Color.Transparent;
            chartEfficiency.ChartAreas[0].AxisX.LabelStyle.ForeColor = textColor;
            chartEfficiency.ChartAreas[0].AxisY.LabelStyle.ForeColor = textColor;
            chartMainBar.ChartAreas[0].BackColor = Color.Transparent;
            chartMainBar.ChartAreas[0].AxisX.LabelStyle.ForeColor = textColor;
            chartMainBar.ChartAreas[0].AxisY.LabelStyle.ForeColor = textColor;
            chartMainPie.ChartAreas[0].BackColor = Color.Transparent;
            chartMainPie.ChartAreas[0].AxisX.LabelStyle.ForeColor = textColor;
            chartMainPie.ChartAreas[0].AxisY.LabelStyle.ForeColor = textColor;
            chartQuality.ChartAreas[0].BackColor = Color.Transparent;
            chartQuality.ChartAreas[0].AxisX.LabelStyle.ForeColor = textColor;
            chartQuality.ChartAreas[0].AxisY.LabelStyle.ForeColor = textColor;
            chartDelivery.ChartAreas[0].BackColor = Color.Transparent;
            chartDelivery.ChartAreas[0].AxisX.LabelStyle.ForeColor = textColor;
            chartDelivery.ChartAreas[0].AxisY.LabelStyle.ForeColor = textColor;
            chartKaizen.ChartAreas[0].BackColor = Color.Transparent;
            chartKaizen.ChartAreas[0].AxisX.LabelStyle.ForeColor = textColor;
            chartKaizen.ChartAreas[0].AxisY.LabelStyle.ForeColor = textColor;



        }
        private void GetData() {
            Thread DeliveryChartThread = new Thread(GetDeliveryChart);
            DeliveryChartThread.Start();
            //LoadDeliveryChart();
            Thread kaizenchartThread = new Thread(GetKaizenChart);
            kaizenchartThread.Start();
            Thread gridThread = new Thread(GetGrid);
            gridThread.Start();
            Thread mainBarThread = new Thread(GetMainBarChart);
            mainBarThread.Start();
            Thread mainPieThread = new Thread(GetMainPieChart);
            mainPieThread.Start();
            Thread safetyThread = new Thread(GetSafety);
            safetyThread.Start();
            Thread kaizenThread = new Thread(GetKaizen);
            kaizenThread.Start();
            Thread deliveryThread = new Thread(GetDelivery);
            deliveryThread.Start();
            Thread efficiencyThread = new Thread(GetEfficiency);
            efficiencyThread.Start();
            Thread qualityThread = new Thread(GetQuality);
            qualityThread.Start();

        }
        private void InitUI()
        {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            this.WindowState = FormWindowState.Maximized;
            int height = base.Height;
            int width = base.Width;
            if (height < 800)
            {
                lblTier3Header.Dock = DockStyle.Left;
            }
            gridKZAP.ColumnHeadersHeight = Convert.ToInt32(height / 25);
            gridKZAP.RowTemplate.Height = Convert.ToInt32(height / 25);
            gridKZAP.EnableHeadersVisualStyles = false;
            gridKZAP.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", (float)height / 80f, FontStyle.Bold, GraphicsUnit.Point, 134);
            gridKZAP.DefaultCellStyle.Font = new Font("宋体", (float)height / 80f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridKZAP.Columns["colItem"].Width = Convert.ToInt32(width / 22);
            gridKZAP.Columns["colProblemPoint"].Width = Convert.ToInt32(width / 8);
            gridKZAP.Columns["colMeasure"].Width = Convert.ToInt32(width / 8);
            gridKZAP.Columns["colPrincipal"].Width = Convert.ToInt32(width / 10);
            gridKZAP.Columns["colDueDate"].Width = Convert.ToInt32(width / 15);
            gridKZAP.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridKZAP.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            #region Header
            //date.Top = (int)height / 20;
            date.Size = new Size((int)width / 15, (int) height / 10);
            date.Font = new Font(Parameters.textFont, height / 90f, FontStyle.Regular, GraphicsUnit.Point, 134);
            date.CustomFormat = Parameters.dateFormat;
            //txtDepartment.Top = (int)height / 20;
            txtDepartment.Size = new Size((int)width / 15, (int) height / 10);
            txtDepartment.Font = new Font(Parameters.textFont, height / 90f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblTier3Header.Font = new Font(Parameters.headerFont, height/40f, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblSafetyHeader.Font = new Font(Parameters.headerFont, height / 40f, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblQualityHeader.Font = new Font(Parameters.headerFont, height / 40f, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblDeliveryHeader.Font = new Font(Parameters.headerFont, height / 40f, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblEfficiencyHeader.Font = new Font(Parameters.headerFont, height / 40f, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblKaizenHeader.Font = new Font(Parameters.headerFont, height / 40f, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblOtherHeader.Font = new Font(Parameters.headerFont, height / 40f, FontStyle.Bold, GraphicsUnit.Point, 134);

            #endregion
            #region Safety
            txtSafetyUntilDate.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);           
            txtSafetyByDate.Font = new Font(Parameters.textFont, (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtSafetyDays.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblSafetyUntilDate.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblSafetyByDate.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblSafetyDays.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            #endregion
            #region Quality
            cbbRFTType.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            cbbRFTType.SelectedIndex = cbbRFTType.Items.IndexOf("L");
            txtQuality.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);          
            lblRFT.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            #endregion
            #region Delivery
            txtSDP.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblSDP.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtPOCompletionRate.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblPOCompletionRate.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            #endregion
            #region Efficiency
            txtPPH.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblPPH.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtTotalProduced.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            #endregion
            #region Kaizen
            txtAccept.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblAccept.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtAcceptPerPeople.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblAcceptPerPeople.Font = new Font(Parameters.textFont, height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            #endregion
        }
        private void GetDate()
        {
            string text3 = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Efficiency_KanbanServer", "QueryWorkDate", Program.client.UserToken, string.Empty);
            bool flag3 = Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(text3)["IsSuccess"]);
            if (flag3)
            {
                string strDate = JsonConvert.DeserializeObject<Dictionary<string, object>>(text3)["RetData"].ToString();
                this.date.Text = strDate;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(text3)["ErrMsg"].ToString());
            }
            DateTime temp = DateTime.Now.AddDays(-1.0);
            date.MaxDate = new DateTime(temp.Year, temp.Month, temp.Day, 23, 59, 59);
            date.MinDate = new DateTime(temp.Year, 1, 1, 0, 0, 0);
        }
       
        private void GetQuality()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetQuality);
                this.Invoke(d);
            }
            else
            {
                GetQualityData();
            }
        }
        private void GetEfficiency()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetEfficiency);
                this.Invoke(d);
            }
            else
            {
                GetEfficiencyData();
            }
        }
        private void GetDelivery()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetDelivery);
                this.Invoke(d);
            }
            else
            {
                GetDeliveryData();
            }
        }
        private void GetSafety()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetSafety);
                this.Invoke(d);
            }
            else
            {
                GetSafetyData();
            }
        }
        private void GetKaizen()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetKaizen);
                this.Invoke(d);
            }
            else
            {
                GetKaizenData();
            }
        }
        private void GetGrid()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetGrid);
                this.Invoke(d);
            }
            else
            {
                LoadGrid();
            }
        }
        private void GetMainBarChart()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetMainBarChart);
                this.Invoke(d);
            }
            else
            {
                LoadMainBarChart();
            }
        }
        private void GetMainPieChart()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetMainPieChart);
                this.Invoke(d);
            }
            else
            {
                LoadMainPieChart();
            }
        }
        private void GetKaizenChart()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetKaizenChart);
                this.Invoke(d);
            }
            else
            {
                LoadKaizenChart();
            }
        }
        private void GetDeliveryChart()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetDeliveryChart);
                this.Invoke(d);
            }
            else
            {
                LoadDeliveryChart();
            }
        }
        #endregion
        #region Link
        private void btnOther_Click(object sender, EventArgs e)
        {
            KaizenActionForm frm = new KaizenActionForm(date.Text, dept, type,userPlant);
            frm.Show();
        }
        private void btnMA_Click(object sender, EventArgs e)
        {
            MaturityAssessmentForm frm = new MaturityAssessmentForm(dept, type, userPlant, userSection);
            frm.Show();
        }
        private void btnKPI_Click(object sender, EventArgs e)
        {
            KPIDeliveryForm frm = new KPIDeliveryForm();
            frm.Show();
        }
        private void lblDeliveryHeader_Click(object sender, EventArgs e)
        {
            //Tier2DeliveryForm frm = new Tier2DeliveryForm(date.Text, userPlant);
            //frm.Show();
            Assembly assembly = null;
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
            assembly = Assembly.LoadFrom(path + @"\" + "RPT_JMS_SFC_Variant" + ".dll");
            Type type = assembly.GetType("RPT_JMS_SFC_Variant.Interface");
            object instance = Activator.CreateInstance(type);
            MethodInfo mi = type.GetMethod("RunCustomize");
            object[] args = new object[2];
            args[0] = Program.client;
            args[1] = date.Value.ToString(Parameters.dateFormat);
            object obj = mi.Invoke(instance, args);
        }
        private void lblQualityHeader_Click(object sender, EventArgs e)
        {
            DetailQualityForm frm = new DetailQualityForm(currentDate, dept, type, cbbRFTType.SelectedItem.ToString());
            frm.Show();
        }
        private void lblEfficiencyHeader_Click(object sender, EventArgs e)
        {
            ThirdEfficiencyForm thform = new ThirdEfficiencyForm(dept, date.Text);
            thform.Show();
            //string str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
            //Assembly assembly = Assembly.LoadFrom(str + "\\ThirdEfficiency_Kanban.dll");
            //Type type = assembly.GetType("ThirdEfficiency_Kanban.Interface");
            //object obj = Activator.CreateInstance(type);
            //MethodInfo method = type.GetMethod("RunCustomize");
            //object obj2 = method.Invoke(obj, new object[]
            //{
            //        Program.client,
            //        userPlant,
            //        currentDate.ToString(Parameters.dateFormat)
            //});
        }
        private void lblKaizenHeader_Click(object sender, EventArgs e)
        {
            DetailKaizenForm frm = new DetailKaizenForm(currentDate, dept, type);
            frm.Show();
        }
        #endregion
        #region Safety
        private void GetSafetyData()
        {
            GetSafetyDataByDate();
            GetSafetyDataUntilDate();
            GetSafetyDays();
        }
        private void GetSafetyDataByDate()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("date", currentDate);
            p.Add("dept", dept);
            p.Add("type", type);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetSafetyDataByDate",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows[0][0].ToInt() > WITarget)
                {
                    SetBackColor(txtSafetyByDate, Color.Red);
                }
                else
                {
                    SetBackColor(txtSafetyByDate, Color.Green);
                }
                SetText(txtSafetyByDate, dtJson.Rows[0][0].ToInt().ToString());
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetSafetyDataUntilDate()
        {
            DateTime tempDate = new DateTime(currentDate.ToDate().Year, 1, 1);
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("firstDateOfYear", tempDate);
            p.Add("date", currentDate);
            p.Add("dept", dept);
            p.Add("type", type);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetSafetyDataUntilDate",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows[0][0].ToInt() > WITarget)
                {
                    SetBackColor(txtSafetyUntilDate, Color.Red);
                }
                else
                {
                    SetBackColor(txtSafetyUntilDate, Color.Green);
                }
                SetText(txtSafetyUntilDate, dtJson.Rows[0][0].ToInt().ToString());
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetSafetyDays()
        {
            DateTime tempDate = new DateTime(currentDate.ToDate().Year, 1, 1);
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("firstDateOfYear", tempDate);
            p.Add("date", currentDate);
            p.Add("dept", dept);
            p.Add("type", type);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetSafetyDays",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                SetBackColor(txtSafetyDays, Color.Green);
                int temp = dtJson.Rows[0][0].ToInt() < 0 ? 0 : dtJson.Rows[0][0].ToInt();
                SetText(txtSafetyDays, temp.ToString());
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        #endregion
        private void GetQualityData()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("date", date.Text);
            p.Add("type", cbbRFTType.SelectedItem.ToString());
            string ret = "";
            if (type == (int)Parameters.QueryDeptType.Plant)
            {
                p.Add("vPlant", dept);
                ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_QCMAPI",
                "KZ_QCMAPI.Controllers.TierMeetingServer", "Tier3_WeekQuery",
                Program.client.UserToken, JsonConvert.SerializeObject(p));
            }
            else if (type == (int)Parameters.QueryDeptType.Section)
            {
                p.Add("vSection", dept);
                ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_QCMAPI",
                "KZ_QCMAPI.Controllers.TierMeetingServer", "Tier2_WeekQuery",
                Program.client.UserToken, JsonConvert.SerializeObject(p));
            }
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                double rft = 0;
                foreach (DataRow r in dtJson.Rows)
                {
                    if(currentDate == DateTime.Parse(r["RIQI"].ToString()))
                        rft += r["RFT"].ToDouble();
                    r["RIQI"] = DateTime.Parse(r["RIQI"].ToString()).ToString("MM/dd");
                }
                //rft = rft / dtJson.Rows.Count;
                SetText(txtQuality, rft.ToString());
                if (cbbRFTType.Text == "S")
                {
                    RFTTarget = 97;
                }
                else if (cbbRFTType.Text == "C")
                {
                    RFTTarget = 98;
                }
                double tmp = rft.ToDouble() * 100 / RFTTarget.ToDouble();
                if (tmp >= 100)
                {
                    SetBackColor(txtQuality, Color.Green);
                }
                else if (tmp >= 95)
                {
                    SetBackColor(txtQuality, Color.Yellow);
                }
                else
                {
                    SetBackColor(txtQuality, Color.Red);
                }
                chartQuality.Series.Clear();
                chartQuality.Legends.Clear();
                chartQuality.Titles.Clear();
                chartQuality.DataBindTable(dtJson.DefaultView, "RIQI");
                if (base.Height > 800) chartQuality.ChartAreas[0].AxisX.Interval = 1;
                chartQuality.Titles.Add(lblQualityChartTitle.Text);
                chartQuality.Titles[0].ForeColor = textColor;
                chartQuality.Series[0].ToolTip = "#VALY";
                chartQuality.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartQuality.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            }
            else
            {
                SetText(txtQuality, "");
                SetBackColor(txtQuality, Color.White);
                chartQuality.Series.Clear();
                chartQuality.Legends.Clear();
                chartQuality.Titles.Clear();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetDeliveryData()
        {
            string month = date.Value.ToString("yyyy") + date.Value.ToString("MM");
            int tempCol = 0;
            Dictionary<int,string> dic = new Dictionary<int,string>();
            ExcelApp.Application excelApp = new ExcelApp.Application();
            if (excelApp == null)
            {
                Console.WriteLine("Excel is not installed!!");
                return;
            }
            //ExcelApp.Workbook excelBook = excelApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "delivery.xlsx");
            ExcelApp.Workbook excelBook = excelApp.Workbooks.Open(strPath);
            ExcelApp._Worksheet excelSheet = excelBook.Sheets["KPI"];
            
            ExcelApp.Range rangeTitle = excelSheet.get_Range("E2:P2", Missing.Value);
            if (rangeTitle != null)
                foreach (ExcelApp.Range r in rangeTitle)
                {
                    dic.Add(r.Column, r.Text);
                }
            foreach (KeyValuePair<int, string> kvp in dic)
            {
                if (kvp.Value == month) tempCol = kvp.Key;
            }
            ExcelApp.Range range = excelSheet.get_Range("E10:P10", Missing.Value);
            if (range != null)
            {
                foreach (ExcelApp.Range r in range)
                {
                    if (r.Column == tempCol)
                    {
                        string strTmp = r.Text;
                        var arr = strTmp.Split('%');
                        double tmp = arr[0].ToDouble() * 100 / SDPTarget.ToDouble();
                        if (tmp >= Parameters.GreenRate)
                        {
                            //txtSDP.BackColor = Color.Green;
                            SetBackColor(txtSDP, Color.Green);
                        }
                        else if (tmp >= Parameters.YellowRate)
                        {
                            //txtSDP.BackColor = Color.Yellow;
                            SetBackColor(txtSDP, Color.Yellow);
                        }
                        else {
                            //txtSDP.BackColor = Color.Red;
                            SetBackColor(txtSDP, Color.Red);
                        }
                        //txtSDP.Text = r.Text;
                        SetText(txtSDP, r.Text);
                    }
                    else
                    {
                        SetBackColor(txtSDP, Color.Green);
                        SetText(txtSDP, "100%");
                    }
                }
            }
            excelBook.Close(false, Missing.Value, Missing.Value);
            excelApp.Quit();
        }
        private void GetEfficiencyData()
        {
            DateTime tempDate = new DateTime(currentDate.ToDate().Year, 1, 1);
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("date", currentDate);
            string ret = "";
            if (type == (int)Parameters.QueryDeptType.Plant)
            {
                p.Add("vPlant", dept);
                ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.client.APIURL,
                        "TierMeeting", "TierMeeting.Controllers.TierMeetingServer",
                                            "Tier3_WeekPPH",
                        Program.client.UserToken,
                        JsonConvert.SerializeObject(p));
            }
            else if (type == (int)Parameters.QueryDeptType.Section)
            {
                p.Add("vSection", dept);
                ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                            Program.client.APIURL,
                            "TierMeeting", "TierMeeting.Controllers.TierMeetingServer",
                                                "Tier2_WeekPPH",
                            Program.client.UserToken,
                            JsonConvert.SerializeObject(p));
            }
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                double pph = 0;
                double pphTarget = 0;
                DataTable dtChart = new DataTable();
                dtChart.Columns.Add("PPH");
                dtChart.Columns.Add("DATE");
                foreach (DataRow r in dtJson.Rows)
                {
                    double tempPPH = 0;
                    DataRow dr = dtChart.NewRow();
                    if (r["MANPOWER"] != null) {
                        if (!(string.IsNullOrEmpty(r["MANPOWER"].ToString()) || r["MANPOWER"].ToString() == "0")) {
                            tempPPH = r["QTY"].ToDouble() / r["MANPOWER"].ToDouble();
                            tempPPH = Math.Round(tempPPH, 2);
                            if (currentDate == DateTime.Parse(r["WORK_DATE"].ToString()))
                            {
                                pph = tempPPH;
                                pphTarget = r["TARGET"].ToDouble() / r["MANPOWER"].ToDouble();
                            }
                        }
                        else
                        {
                            tempPPH = 0;
                        }
                    }
                    dr["PPH"] = tempPPH;
                    dr["DATE"] = DateTime.Parse(r["WORK_DATE"].ToString()).ToString("MM/dd");
                    dtChart.Rows.Add(dr);
                }
                SetText(txtPPH, pph.ToString());
                if (pphTarget != 0 && (pph / pphTarget) * 100 >= 100)
                {
                    SetBackColor(txtPPH, Color.Green);
                }
                else if (pphTarget != 0 && (pph / pphTarget) * 100 >= 95)
                {
                    SetBackColor(txtPPH, Color.Yellow);
                }
                else
                {
                    SetBackColor(txtPPH, Color.Red);
                }
                chartEfficiency.Series.Clear();
                chartEfficiency.Legends.Clear();
                chartEfficiency.Titles.Clear();
                chartEfficiency.DataBindTable(dtChart.DefaultView, "DATE");
                if (base.Height > 800) chartEfficiency.ChartAreas[0].AxisX.Interval = 1;
                chartEfficiency.ChartAreas[0].AxisY.Interval = 1;
                chartEfficiency.Titles.Add(lblEfficiencyChartTitle.Text);
                chartEfficiency.Titles[0].ForeColor = textColor;
                chartEfficiency.Series[0].ToolTip = "#VALY";
                chartEfficiency.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartEfficiency.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            }
            else
            {
                SetText(txtPPH, "");
                chartEfficiency.Series.Clear();
                chartEfficiency.Legends.Clear();
                chartEfficiency.Titles.Clear();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetKaizenData()
        {
            string firstDate = new DateTime(currentDate.Year, 1, 1).ToString(Parameters.dateFormat);
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("date", currentDate.ToString(Parameters.dateFormat));
            p.Add("firstDate", firstDate);
            p.Add("dept", dept);
            p.Add("type", type);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetKaizenUntilDate",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                SetText(txtAccept,dtJson.Rows[0][0].ToInt().ToString());
                SetBackColor(txtAcceptPerPeople, Color.Red);
                GetManpower();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetManpower()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", dept);
            p.Add("type", type);
            p.Add("date", currentDate.ToString(Parameters.dateFormat));
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetManpower",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                double temp = 0;
                if(dtJson.Rows[0][0].ToInt() != 0)
                    temp = Math.Round((double)(double.Parse(txtAccept.Text) / dtJson.Rows[0][0].ToDouble()),2);
                SetText(txtAcceptPerPeople, temp.ToString());
                if (dtJson.Rows[0][1].ToInt() != 0)
                {
                    double tmp = txtAccept.Text.ToDouble() * 100 / dtJson.Rows[0][1].ToDouble();
                    if (tmp >= Parameters.GreenRate)
                    {
                        SetBackColor(txtAcceptPerPeople, Color.Green);
                    }
                    else if (tmp >= Parameters.YellowRate)
                    {
                        SetBackColor(txtAcceptPerPeople, Color.Yellow);
                    }
                    else
                    {
                        SetBackColor(txtAcceptPerPeople, Color.Red);
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }         
        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }
        private void SetText(TextBox txt, string text)
        {
            if (txt.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { txt, text });
            }
            else
            {
                txt.Text = text;
            }
        }
        private void SetBackColor(TextBox txt, Color color)
        {
            if (txt.InvokeRequired)
            {
                SetBackColorCallback d = new SetBackColorCallback(SetBackColor);
                this.Invoke(d, new object[] { txt, color });
            }
            else
            {
                txt.BackColor = color;
            }
        }
        private void DeptSelected(object sender, SelectDepartmentForm.DataChangeEventArgs args) {
            txtDepartment.Text = args.deptCode;
            dept = args.deptCode;
            type = args.deptType;
        }
        private void txtDepartment_DoubleClick(object sender, EventArgs e)
        {
            SelectDepartmentForm frm = new SelectDepartmentForm((int) Parameters.QueryDeptType.Section,userPlant);
            frm.DataChange += new SelectDepartmentForm.DataChangeHandler(DeptSelected);
            frm.ShowDialog();
        }
        private void date_ValueChanged(object sender, EventArgs e)
        {
            currentDate = DateTime.Parse(date.Value.ToString(Parameters.dateFormat));
        }
        private void lblSafetyHeader_Click(object sender, EventArgs e)
        {
            DetailSafetyForm frm = new DetailSafetyForm(date.Text, dept, type);
            frm.Show();
        }
        private void LoadDeliveryChart()
        {
            DateTime tempDate = new DateTime(currentDate.ToDate().Year, 1, 1);
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("date", currentDate.ToString("yyyy/MM/dd"));
            //string ret =
            //    SJeMES_Framework.WebAPI.WebAPIHelper.Post(
            //        Program.client.APIURL,
            //        "TierMeeting",
            //        "TierMeeting.Controllers.TierMeetingServer",
            //                            "GetMainSafetyChart",
            //        Program.client.UserToken,
            //        JsonConvert.SerializeObject(p));  
            #region  po_rate  

            p.Add("dept", dept);
            p.Add("type", 3);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetPoRate",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));

            #endregion
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                //Console.WriteLine();
                //txtPOCompletionRate.Text = dtJson.Rows[dtJson.Rows.Count-1][0].ToString()+"%";
                SetText(txtPOCompletionRate, dtJson.Rows[dtJson.Rows.Count - 1][0].ToString());

                // chartDelivery.DataBindTable(dtJson.DefaultView);


                Series dataTable1Series = new Series("dataTable1");
                dataTable1Series.Points.DataBind(dtJson.AsEnumerable(), "T_DATE", "FINISH_RATE", "");
                dataTable1Series.XValueType = ChartValueType.DateTime; //设置X轴类型为时间
                dataTable1Series.ChartType = SeriesChartType.Column;

                chartDelivery.Series.Clear();
                chartDelivery.Series.Add(dataTable1Series);
                chartDelivery.Titles.Clear();
                //chartDelivery.Titles.Add("生产近七天PO达成状况");
                chartDelivery.Titles.Add("The status of PO achievement in the past seven days of production");

                if (base.Height > 800) chartDelivery.ChartAreas[0].AxisX.Interval = 0;
                chartDelivery.Series["dataTable1"].XValueMember = "T_DATE";
                chartDelivery.Series["dataTable1"].YValueMembers = "FINISH_RATE";

                chartDelivery.ChartAreas[0].AxisY.Minimum = 0;
                chartDelivery.ChartAreas[0].AxisY.Maximum = 100;

                chartDelivery.Titles[0].ForeColor = textColor;

            }
            else
            {

                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void LoadKaizenChart()
        {
            DateTime tempDate = new DateTime(currentDate.ToDate().Year, 1, 1);
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("date", tempDate);
            p.Add("type", type);
            p.Add("dept", dept);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetMainKaizenChart",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                chartKaizen.Series.Clear();
                chartKaizen.Legends.Clear();
                chartKaizen.Titles.Clear();
                chartKaizen.DataBindTable(dtJson.DefaultView, "month");
                if (base.Height > 800) chartKaizen.ChartAreas[0].AxisX.Interval = 1;
                chartKaizen.ChartAreas[0].AxisX.Minimum = 0;
                chartKaizen.ChartAreas[0].AxisX.Maximum = 12;
                //chartKaizen.Series[0].ChartType = SeriesChartType.Bar;
                chartKaizen.Titles.Add(lblKaizenChartTitle.Text);
                chartKaizen.Titles[0].ForeColor = textColor;
                chartKaizen.Series[0].ToolTip = "#VALY";
                chartKaizen.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartKaizen.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            }
            else
            {
                chartKaizen.Series.Clear();
                chartKaizen.Legends.Clear();
                chartKaizen.Titles.Clear();
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void LoadMainBarChart()
        {
            DateTime tempDate = new DateTime(currentDate.ToDate().Year, 1, 1);
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", dept);
            p.Add("type", type);
            p.Add("process", 3);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetKZAPDataBarChart",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                int temp = 0;
                foreach (DataRow r in dtJson.Rows) {
                    temp += r["COUNT"].ToInt();
                }
                chartMainBar.Series.Clear();
                chartMainBar.Legends.Clear();
                chartMainBar.Titles.Clear();
                chartMainBar.DataBindTable(dtJson.DefaultView,"DEPARTMENT");
                if (base.Height > 800) chartMainBar.ChartAreas[0].AxisX.Interval = 1;
                //chartMainBar.Titles.Add( "改善快报总个数: " + temp.ToString());
                chartMainBar.Titles.Add("Improve the total number of reports: " + temp.ToString());
                chartMainBar.Series[0].ChartType = SeriesChartType.Bar;
                chartMainBar.Titles[0].ForeColor = textColor;
                chartMainBar.Series[0].ToolTip = "#VALY";
                chartMainBar.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartMainBar.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void LoadMainPieChart()
        {
            DateTime tempDate = new DateTime(currentDate.ToDate().Year, 1, 1);
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", dept);
            p.Add("type", type);
            p.Add("process", 3);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetKZAPDataPieChart",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                DataTable dt = new DataTable();
                dt.Columns.Add("COUNT"); 
                dt.Columns.Add("STATUS");
                DataRow dr1 = dt.NewRow();
                dr1["COUNT"] = dtJson.Rows[0]["TOTAL"].ToInt() - dtJson.Rows[0]["OPEN"].ToInt();
                dr1["STATUS"] = lblClosedText.Text+":"+ dr1["COUNT"].ToString();
                dt.Rows.Add(dr1);
                DataRow dr2 = dt.NewRow();
                dr2["COUNT"] = dtJson.Rows[0]["OPEN"].ToInt();
                dr2["STATUS"] = lblOpenText.Text+":" + dr2["COUNT"].ToString(); 
                dt.Rows.Add(dr2);
                double percent = 0;
                if (dtJson.Rows[0]["TOTAL"].ToInt() != 0)
                {
                    percent = Math.Round((double)dr1["COUNT"].ToInt() * 100 / dtJson.Rows[0]["TOTAL"].ToInt(), 2);
                }
               
                chartMainPie.Series.Clear();
                chartMainPie.Titles.Clear();
                chartMainPie.DataBindTable(dt.DefaultView,"STATUS");
                //chartMainPie.Titles.Add("完成比率%: "+ percent.ToString());
                chartMainPie.Titles.Add("Completion Rate%: " + percent.ToString());
                chartMainPie.Series[0].ChartType = SeriesChartType.Pie;
                chartMainPie.Series[0]["PieLabelStyle"] = "Disabled";
                chartMainPie.Titles[0].ForeColor = textColor;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void LoadGrid()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", dept);
            p.Add("type", type);
            p.Add("process", 3);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetKZAPDataTable",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
               ///DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                DataTable dtJson = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                //DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                gridKZAP.DataSource = dtJson;
                gridKZAP.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
                foreach (DataGridViewRow row in gridKZAP.Rows)
                {
                    if (!string.IsNullOrEmpty(row.Cells["colFinishDate"].Value.ToString()))
                    {
                        row.DefaultCellStyle.BackColor = Color.Green;
                    }
                    else if (row.Cells["colDueDate"].Value.ToDate() < DateTime.Now)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
                gridKZAP.ClearSelection();
            }
            else
            {
                DataTable dt = (DataTable) gridKZAP.DataSource;
                if (dt != null)
                {
                    dt.Clear();
                }
                gridKZAP.Refresh();
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            StringFormat centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, gridKZAP.ColumnHeadersDefaultCellStyle.Font, SystemBrushes.ActiveBorder, headerBounds, centerFormat);

        }

        private void txtPOCompletionRate_Click(object sender, EventArgs e)
        {
            POComRateForm poComRateForm = new POComRateForm(dept, currentDate, "3");
            poComRateForm.Show();
        }

        private void txtPOCompletionRate_TextChanged(object sender, EventArgs e)
        {
            double rate = Convert.ToDouble(txtPOCompletionRate.Text);
            if (rate >= 100)
            {
                SetBackColor(txtPOCompletionRate, Color.Green);
            }
            else if (rate >= 95)
            {
                SetBackColor(txtPOCompletionRate, Color.Yellow);
            }
            else
            {
                SetBackColor(txtPOCompletionRate, Color.Red);
            }
        }

        private void txtPPH_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
