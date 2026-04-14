using CusContorl;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace Production_Kanban
{
    public partial class Production_KanbanForm : MaterialForm
    {



        Timer timer1 = new Timer();
        private string ART = "";
        private delegate void GetDataCallback();
        public delegate void SetTextCallBack(string setTextMethod, DataTable dtJson);
        int from_status = 0;

        // List<string> page_loaded = new List<string>();



        //闪烁测试
        #region   
        //const int WM_SYSCOMMAND = 0x112;
        //const int SC_CLOSE = 0xF060;
        //const int SC_MINIMIZE = 0xF020;
        //const int SC_MAXIMIZE = 0xF030;
        //const int WM_SHOW = 0x0000f120;

        private bool AntiFlicker = false;





        /// <summary>
        /// 设置容器下所有type类型的控件双重缓冲
        /// </summary>
        /// <param name="control">容器</param>
        /// <param name="type">需要设置双重缓冲的类型</param>
        private void SetDoubleBuffered(Control control, Type type)
        {
            if (control.GetType() == type.GetType() || control.GetType().IsSubclassOf(type))
            {

                Type controlType = control.GetType();
                PropertyInfo pi = controlType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(controlType, true, null);

            }
            else
            {
                for (int i = 0; i < control.Controls.Count; i++)
                {
                    SetDoubleBuffered(control.Controls[i], type);
                }
            }
        }





        #endregion


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }




        private void MySetTxetMehod(string setTextMethod, DataTable dtJson)
        {
            Type type;
            Object obj;
            string className = this.GetType().FullName;
            type = Type.GetType(className);
            obj = System.Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod(setTextMethod);
            object[] args = new object[1];
            args[0] = dtJson;
            method.Invoke(obj, args);
        }

        public delegate void MyDelegate(string pageName);

        private void MyMehod(string pageName)
        {
            Type type;
            Object obj;
            string className = this.GetType().FullName;
            type = Type.GetType(className);
            obj = System.Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod(pageName, new Type[] { });
            object[] parameters = null;
            method.Invoke(obj, parameters);
        }


        public Production_KanbanForm()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // Disable background erasure.

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();

            SetTextChanged();
            this.WindowState = FormWindowState.Maximized;
            tabPage8.Parent = null;

            try
            {
                SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            }
            catch (Exception ex)
            {

            }

        }

        private void Production_KanbanForm_Load(object sender, EventArgs e)
        {
            /// this.WindowState = FormWindowState.Maximized;
            /// 
          //  SetDoubleBuffered(this, typeof(TableLayoutPanel));
            if (!string.IsNullOrEmpty(Interface.line) && !string.IsNullOrEmpty(Interface.date))
            {
                txtLine.Text = Interface.line;
                dateTimePicker1.Text = Interface.date;
            }
            else if (!string.IsNullOrEmpty(Interface.date))
            {
                getLine();
            }
            else
            {
                try
                {
                    getLine();
                    getWorkDate();
                }
                catch (Exception ex)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            //   SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            //tabPage8_query();
            //    tabControl1_SelectedIndexChanged(sender, e);

            tabPage1_query();
            if (string.IsNullOrWhiteSpace(txtTimer.Text))
            {
                txtTimer.Text = (60 * 30).ToString();
            }
            timer1.Interval = int.Parse(txtTimer.Text.ToString()) * 1000;
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(timer1_Tick);//添加事件

        }

        private void getLine()
        {
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Efficiency_KanbanServer", "QueryDeptNo", Program.Client.UserToken, string.Empty);
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string line = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                txtLine.Text = line;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
            }
        }
        private void getWorkDate()
        {
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "TierMeeting", "TierMeeting.Controllers.TierMeetingServer", "QueryWorkDate", Program.Client.UserToken, string.Empty);
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string date = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dateTimePicker1.Text = date;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            int index = tabControl1.SelectedIndex;
            if (index == tabControl1.TabCount - 2)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            this.tabControl1.SelectedIndex = index;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {


            MyDelegate myDelegate = MyMehod;
            string pageName = this.tabControl1.SelectedTab.Name + "_query";
            //if (page_loaded.Contains(tabControl1.SelectedTab.Name)) {
            //    return;
            //}
            //page_loaded.Add(this.tabControl1.SelectedTab.Name);

            //myDelegate(pageName);
            switch (pageName)
            {
                case "tabPage1_query":
                    // tabPage1_query();
                    break;
                case "tabPage2_query":
                    //tabPage2_query();
                    GetTier1();
                    break;
                case "tabPage3_query":
                    //tabPage3_query();
                    GetTier1Standard();
                    break;
                case "tabPage4_query":
                    GetTier1_WeekSafety();
                    GetTier1Data();
                    tabPage4_query();
                    break;
                case "tabPage5_query":
                    tabPage5_query();
                    break;
                case "tabPage6_query":
                    tabPage6_fast_query();
                    GetSeason();
                    //tabPage6_query();
                    break;
                case "tabPage7_query":
                    tabPage7_query();
                    break;
                case "tabPage8_query":
                    tabPage8_query();
                    break;
                case "tabPage9_query":
                    tabPage9_query();
                    break;
            }
        }
        private void GetTier1Data()
        {
            GetWeekRFT();
            GetWeekPPHTarget();
            GetWeekPPH();
            GetWeekOutput();
            GetKaizen();
            GetWeekLLER();
            GetWeekMulti();
            GetWeekDT();
            GetWeekCOT();
            GetWeekWIP();
        }
        private void GetWeekRFT()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetWeekRFT);
                this.Invoke(d);
            }
            else
            {
                GetTier1_WeekRFT();
            }
        }
        private void GetWeekPPHTarget()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetWeekPPHTarget);
                this.Invoke(d);
            }
            else
            {
                GetTier1_WeekPPHTarget();
            }
        }
        private void GetWeekPPH()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetWeekPPH);
                this.Invoke(d);
            }
            else
            {
                GetTier1_WeekPPH();
            }
        }
        private void GetWeekOutput()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetWeekOutput);
                this.Invoke(d);
            }
            else
            {
                GetTier1_WeekOutput();
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
                GetTier1_Kaizen();
            }
        }
        private void GetWeekLLER()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetWeekLLER);
                this.Invoke(d);
            }
            else
            {
                GetTier1_WeekLLER();
            }
        }
        private void GetWeekMulti()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetWeekMulti);
                this.Invoke(d);
            }
            else
            {
                GetTier1_WeekMulti();
            }
        }
        private void GetWeekDT()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetWeekDT);
                this.Invoke(d);
            }
            else
            {
                GetTier1_WeekDT();
            }
        }
        private void GetWeekCOT()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetWeekCOT);
                this.Invoke(d);
            }
            else
            {
                GetTier1_WeekCOT();
            }
        }
        private void GetWeekWIP()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(GetWeekWIP);
                this.Invoke(d);
            }
            else
            {
                GetTier1_WeekWIP();
            }
        }
        /// <summary>
        /// 基础资料
        /// </summary>
        public void tabPage1_query()
        {
            try
            {
                if (!string.IsNullOrEmpty(dateTimePicker1.Value.ToString()) && !string.IsNullOrEmpty(txtLine.Text))
                {
                    Dictionary<string, Object> p = new Dictionary<string, object>();
                    p.Add("date", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
                    p.Add("line", txtLine.Text);
                    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "TierMeeting",
                        "TierMeeting.Controllers.TierMeetingServer", "TabPage1_Query", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                        //不能使用委托更新UI
                        //SetTextCallBack mySetTxetMehod = MySetTxetMehod;
                        //mySetTxetMehod("Set_Page1Text", dtJson);
                        //可以启动另外一个线程来更新UI
                        //this.Invoke(new Action(() =>
                        //{
                        //    label3.Text = dtJson.Rows[0]["ModelName"].ToString();
                        //    label53.Text = dtJson.Rows[0]["Date"].ToString();
                        //    label54.Text = dtJson.Rows[0]["Target"].ToString();
                        //    label55.Text = dtJson.Rows[0]["TaktTime"].ToString();
                        //    label56.Text = dtJson.Rows[0]["WaterSpiderNo"].ToString();
                        //    label58.Text = dtJson.Rows[0]["ModelTHT"].ToString();
                        //    label52.Text = dtJson.Rows[0]["OperatorNo"].ToString();
                        //}));
                        SetText(label3, dtJson.Rows[0]["ModelName"].ToString());
                        SetText(label53, dtJson.Rows[0]["Date"].ToString());
                        SetText(label54, dtJson.Rows[0]["Target"].ToString());
                        if (dtJson.Rows[0]["Target"].ToString() != "0")
                            SetText(label55, (3600 / (int.Parse(dtJson.Rows[0]["Target"].ToString()))).ToString());
                        //SetText(label55, (3600 / (int.Parse(dtJson.Rows[0]["Target"].ToString()) / 10)).ToString());

                        else
                            SetText(label55, "");
                        SetText(label56, dtJson.Rows[0]["WaterSpiderNo"].ToString());
                        //SetText(label58, dtJson.Rows[0]["ModelTHT"].ToString());
                        SetText(label52, dtJson.Rows[0]["OperatorNo"].ToString());
                        ART = dtJson.Rows[0]["ART"].ToString();
                        string PROCESS = "";
                        if (txtLine.Text.Contains("C"))
                        {
                            PROCESS = "C";
                        }
                        else if (txtLine.Text.Contains("S"))
                        {
                            PROCESS = "S";
                        }
                        else if (txtLine.Text.Contains("L"))
                        {
                            PROCESS = "L";
                        }
                        else if (txtLine.Text.Contains("T"))
                        {
                            PROCESS = "T";
                        }
                        Dictionary<string, Object> pTHT = new Dictionary<string, object>();
                        pTHT.Add("ART", ART);
                        pTHT.Add("PROCESS", PROCESS);
                        pTHT.Add("DEPT", txtLine.Text);
                        string retTHT = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                            "TierMeeting", "TierMeeting.Controllers.TierMeetingServer",
                                        "GetTHTByART", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(pTHT));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retTHT)["IsSuccess"]))
                        {
                            string jsonTHT = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retTHT)["RetData"].ToString();
                            DataTable dtJsonTHT = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(jsonTHT);
                            rtbTHT.Text = dtJsonTHT.Rows[0][0].ToString();
                        }
                        else
                        {
                            rtbTHT.Text = "";
                        }
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 一层层级会议
        /// </summary>
        public void tabPage2_query()
        {
            try
            {
                if (!string.IsNullOrEmpty(dateTimePicker1.Value.ToString()) && !string.IsNullOrEmpty(txtLine.Text))
                {
                    Dictionary<string, Object> p = new Dictionary<string, object>();
                    p.Add("date", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
                    p.Add("line", txtLine.Text);
                    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.TierMeetingServer", "TabPage2_Query", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                        this.Invoke(new Action(() =>
                        {
                            lblTier1Time.Text = dateTimePicker1.Value.ToString("yyyy/MM/dd");
                            if (dtJson.Rows.Count == 0)
                            {
                                //label32.BackColor = Color.FromArgb(255, 255, 255);
                                //label33.BackColor = Color.FromArgb(255, 255, 255);
                                //label34.BackColor = Color.FromArgb(255, 255, 255);
                                //label35.BackColor = Color.FromArgb(255, 255, 255);
                                //label36.BackColor = Color.FromArgb(255, 255, 255);
                                //label37.BackColor = Color.FromArgb(255, 255, 255);
                                //label38.BackColor = Color.FromArgb(255, 255, 255);
                                //label39.BackColor = Color.FromArgb(255, 255, 255);
                                //label122.BackColor = Color.FromArgb(255, 255, 255);
                                //label32.Text = "";
                                //label33.Text= "";
                                //label34 .Text= "";
                                //label35 .Text= "";
                                //label36 .Text= "";
                                //label37 .Text= "";
                                //label38.Text = "";
                                //label39 .Text= "";
                                //label122.Text= "";
                                //label51.Text = "";
                            }
                            for (int i = 0; i < dtJson.Rows.Count; i++)
                            {
                                if (i == 0)
                                {
                                    //    if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                                    //    {
                                    //        label32.Text = "√";
                                    //        label32.BackColor = Color.FromArgb(255, 255, 255);
                                    //    }
                                    //    else
                                    //    {
                                    //        label32.Text = "×";
                                    //        label32.BackColor = Color.FromArgb(255, 0, 0);

                                    //    }
                                    //    label51.Text = dtJson.Rows[i]["audit_person"].ToString();
                                    //}
                                    //if (i == 1)
                                    //{
                                    //    if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                                    //    {
                                    //        label33.Text = "√";
                                    //        label33.BackColor = Color.FromArgb(255, 255, 255);

                                    //    }
                                    //    else
                                    //    {
                                    //        label33.Text = "×";
                                    //        label33.BackColor = Color.FromArgb(255, 0, 0);

                                    //    }
                                    //}
                                    //if (i == 2)
                                    //{
                                    //    if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                                    //    {
                                    //        label34.Text = "√";
                                    //        label34.BackColor = Color.FromArgb(255, 255, 255);

                                    //    }
                                    //    else
                                    //    {
                                    //        label34.Text = "×";
                                    //        label34.BackColor = Color.FromArgb(255, 0, 0);

                                    //    }
                                    //}
                                    //if (i == 3)
                                    //{
                                    //    if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                                    //    {
                                    //        label35.Text = "√";
                                    //        label35.BackColor = Color.FromArgb(255, 255, 255);

                                    //    }
                                    //    else
                                    //    {
                                    //        label35.Text = "×";
                                    //        label35.BackColor = Color.FromArgb(255, 0, 0);

                                    //    }
                                    //}
                                    //if (i == 4)
                                    //{
                                    //    if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                                    //    {
                                    //        label36.Text = "√";
                                    //        label36.BackColor = Color.FromArgb(255, 255, 255);

                                    //    }
                                    //    else
                                    //    {
                                    //        label36.Text = "×";
                                    //        label36.BackColor = Color.FromArgb(255, 0, 0);

                                    //    }
                                    //}
                                    //if (i == 5)
                                    //{
                                    //    if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                                    //    {
                                    //        label37.Text = "√";
                                    //        label37.BackColor = Color.FromArgb(255, 255, 255);

                                    //    }
                                    //    else
                                    //    {
                                    //        label37.Text = "×";
                                    //        label37.BackColor = Color.FromArgb(255, 0, 0);

                                    //    }
                                    //}
                                    //if (i == 6)
                                    //{
                                    //    if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                                    //    {
                                    //        label38.Text = "√";
                                    //        label38.BackColor = Color.FromArgb(255, 255, 255);

                                    //    }
                                    //    else
                                    //    {
                                    //        label38.Text = "×";
                                    //        label38.BackColor = Color.FromArgb(255, 0, 0);

                                    //    }
                                    //}
                                    //if (i == 7)
                                    //{
                                    //    if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                                    //    {
                                    //        label39.Text = "√";
                                    //        label39.BackColor = Color.FromArgb(255, 255, 255);

                                    //    }
                                    //    else
                                    //    {
                                    //        label39.Text = "×";
                                    //        label39.BackColor = Color.FromArgb(255, 0, 0);

                                    //    }
                                }
                                if (i == 8)
                                {
                                    //if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                                    //{
                                    //    label122.Text = "√";
                                    //    label122.BackColor = Color.FromArgb(255, 255, 255);

                                    //}
                                    //else
                                    //{
                                    //    label122.Text = "×";
                                    //    label122.BackColor = Color.FromArgb(255, 0, 0);

                                    //}
                                }
                            }

                        }));
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 标准制造环境
        /// </summary>
        public void tabPage3_query()
        {
            try
            {
                if (!string.IsNullOrEmpty(dateTimePicker1.Value.ToString()) && !string.IsNullOrEmpty(txtLine.Text))
                {
                    Dictionary<string, Object> p = new Dictionary<string, object>();
                    p.Add("date", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
                    p.Add("line", txtLine.Text);
                    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.TierMeetingServer", "TabPage3_Query", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                        this.Invoke(new Action(() =>
                        {
                            //label62.Text = dateTimePicker1.Value.ToString("yyyy/MM/dd");
                            //if (dtJson.Rows.Count==0)
                            //{
                            //    setManagerToDefault();
                            //    setVsmLableToDefault();
                            //    setThirdPartySpotCheckLableToDefault();
                            //    setNonImpentationReasonsToDefalut();
                            //}
                            //else
                            //{
                            //    if (dtJson.Select("AUDIT_ITEM='A'").Count() == 0)
                            //    {
                            //        setManagerToDefault();
                            //    }
                            //    if (dtJson.Select("AUDIT_ITEM='B'").Count() == 0)
                            //    {
                            //        setVsmLableToDefault();
                            //    }
                            //    if (dtJson.Select("AUDIT_ITEM='C'").Count() == 0)
                            //    {
                            //        setThirdPartySpotCheckLableToDefault();
                            //    }
                            //    //没有执行的原因
                            //    if (dtJson.Select("AUDIT_SEQ='1'").Count()==0)
                            //    {
                            //        label74.Text = "";
                            //    }
                            //    else 
                            //    {
                            //        string tempStr = "";
                            //        DataRow[] foundRows;
                            //        foundRows = dtJson.Select("AUDIT_SEQ='1'");
                            //        for (int j=0;j<foundRows.Count();j++)
                            //        {
                            //            tempStr += foundRows[j]["audit_memo"];
                            //        }
                            //        label74.Text = tempStr;
                            //    }

                            //    if (dtJson.Select("AUDIT_SEQ='2'").Count() == 0)
                            //    {
                            //        label80.Text = "";
                            //    }
                            //    else
                            //    {
                            //        string tempStr = "";
                            //        DataRow[] foundRows;
                            //        foundRows = dtJson.Select("AUDIT_SEQ='2'");
                            //        for (int j = 0; j < foundRows.Count(); j++)
                            //        {
                            //            tempStr += foundRows[j]["audit_memo"];
                            //        }
                            //        label80.Text = tempStr;
                            //    }

                            //    if (dtJson.Select("AUDIT_SEQ='3'").Count() == 0)
                            //    {
                            //        label86.Text = "";
                            //    }
                            //    else
                            //    {
                            //        string tempStr = "";
                            //        DataRow[] foundRows;
                            //        foundRows = dtJson.Select("AUDIT_SEQ='3'");
                            //        for (int j = 0; j < foundRows.Count(); j++)
                            //        {
                            //            tempStr += foundRows[j]["audit_memo"];
                            //        }
                            //        label86.Text = tempStr;
                            //    }


                            //    if (dtJson.Select("AUDIT_SEQ='4'").Count() == 0)
                            //    {
                            //        label92.Text = "";
                            //    }
                            //    else
                            //    {
                            //        string tempStr = "";
                            //        DataRow[] foundRows;
                            //        foundRows = dtJson.Select("AUDIT_SEQ='4'");
                            //        for (int j = 0; j < foundRows.Count(); j++)
                            //        {
                            //            tempStr += foundRows[j]["audit_memo"];
                            //        }
                            //        label92.Text = tempStr;
                            //    }


                            //    if (dtJson.Select("AUDIT_SEQ='5'").Count() == 0)
                            //    {
                            //        label98.Text = "";
                            //    }
                            //    else
                            //    {
                            //        string tempStr = "";
                            //        DataRow[] foundRows;
                            //        foundRows = dtJson.Select("AUDIT_SEQ='5'");
                            //        for (int j = 0; j < foundRows.Count(); j++)
                            //        {
                            //            tempStr += foundRows[j]["audit_memo"];
                            //        }
                            //        label98.Text = tempStr;
                            //    }


                            //    if (dtJson.Select("AUDIT_SEQ='6'").Count() == 0)
                            //    {
                            //        label104.Text = "";
                            //    }
                            //    else
                            //    {
                            //        string tempStr = "";
                            //        DataRow[] foundRows;
                            //        foundRows = dtJson.Select("AUDIT_SEQ='6'");
                            //        for (int j = 0; j < foundRows.Count(); j++)
                            //        {
                            //            tempStr += foundRows[j]["audit_memo"];
                            //        }
                            //        label104.Text = tempStr;
                            //    }

                            //    if (dtJson.Select("AUDIT_SEQ='7'").Count() == 0)
                            //    {
                            //        label110.Text = "";
                            //    }
                            //    else
                            //    {
                            //        string tempStr = "";
                            //        DataRow[] foundRows;
                            //        foundRows = dtJson.Select("AUDIT_SEQ='7'");
                            //        for (int j = 0; j < foundRows.Count(); j++)
                            //        {
                            //            tempStr += foundRows[j]["audit_memo"];
                            //        }
                            //        label110.Text = tempStr;
                            //    }

                            //    if (dtJson.Select("AUDIT_SEQ='8'").Count() == 0)
                            //    {
                            //        label116.Text = "";
                            //    }
                            //    else
                            //    {
                            //        string tempStr = "";
                            //        DataRow[] foundRows;
                            //        foundRows = dtJson.Select("AUDIT_SEQ='8'");
                            //        for (int j = 0; j < foundRows.Count(); j++)
                            //        {
                            //            tempStr += foundRows[j]["audit_memo"];
                            //        }
                            //        label116.Text = tempStr;
                            //    }

                            //}                        
                            //for (int i = 0; i < dtJson.Rows.Count; i++)
                            //{
                            //    #region 主管自评
                            //    if (dtJson.Rows[i]["audit_item"].ToString().Equals("A"))
                            //    {
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("1"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label71.Text = "√";
                            //                label71.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label71.Text = "×";
                            //                label71.BackColor = Color.FromArgb(255, 0, 0);
                            //            }
                            //            label117.Text = dtJson.Rows[i]["audit_person"].ToString();
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("2"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label77.Text = "√";
                            //                label77.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label77.Text = "×";
                            //                label77.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("3"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label83.Text = "√";
                            //                label83.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label83.Text = "×";
                            //                label83.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("4"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label89.Text = "√";
                            //                label89.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label89.Text = "×";
                            //                label89.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("5"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label95.Text = "√";
                            //                label95.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label95.Text = "×";
                            //                label95.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("6"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label101.Text = "√";
                            //                label101.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label101.Text = "×";
                            //                label101.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("7"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label107.Text = "√";
                            //                label107.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label107.Text = "×";
                            //                label107.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("8"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label113.Text = "√";
                            //                label113.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label113.Text = "×";
                            //                label113.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //    }
                            //    #endregion 主管自评

                            //    #region VSM复核
                            //    if (dtJson.Rows[i]["audit_item"].ToString().Equals("B"))
                            //    {
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("1"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label72.Text = "√";
                            //                label72.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label72.Text = "×";
                            //                label72.BackColor = Color.FromArgb(255, 0, 0);
                            //            }
                            //            label119.Text = dtJson.Rows[i]["audit_person"].ToString();
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("2"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label78.Text = "√";
                            //                label78.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label78.Text = "×";
                            //                label78.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("3"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label84.Text = "√";
                            //                label84.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label84.Text = "×";
                            //                label84.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("4"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label90.Text = "√";
                            //                label90.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label90.Text = "×";
                            //                label90.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("5"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label96.Text = "√";
                            //                label96.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label96.Text = "×";
                            //                label96.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("6"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label102.Text = "√";
                            //                label102.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label102.Text = "×";
                            //                label102.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("7"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label108.Text = "√";
                            //                label108.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label108.Text = "×";
                            //                label108.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("8"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label114.Text = "√";
                            //                label114.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label114.Text = "×";
                            //                label114.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //    }
                            //    #endregion VSM复核

                            //    #region 第三方抽查
                            //    if (dtJson.Rows[i]["audit_item"].ToString().Equals("C"))
                            //    {
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("1"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label73.Text = "√";
                            //                label73.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label73.Text = "×";
                            //                label73.BackColor = Color.FromArgb(255, 0, 0);
                            //            }
                            //            label121.Text = dtJson.Rows[i]["audit_person"].ToString();
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("2"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label79.Text = "√";
                            //                label79.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label79.Text = "×";
                            //                label79.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("3"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label85.Text = "√";
                            //                label85.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label85.Text = "×";
                            //                label85.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("4"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label91.Text = "√";
                            //                label91.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label91.Text = "×";
                            //                label91.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("5"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label97.Text = "√";
                            //                label97.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label97.Text = "×";
                            //                label97.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("6"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label103.Text = "√";
                            //                label103.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label103.Text = "×";
                            //                label103.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("7"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label109.Text = "√";
                            //                label109.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label109.Text = "×";
                            //                label109.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //        if (dtJson.Rows[i]["audit_seq"].ToString().Equals("8"))
                            //        {
                            //            if (dtJson.Rows[i]["audit_result"].ToString().Equals("Y"))
                            //            {
                            //                label115.Text = "√";
                            //                label115.BackColor = Color.FromArgb(255, 255, 255);
                            //            }
                            //            else
                            //            {
                            //                label115.Text = "×";
                            //                label115.BackColor = Color.FromArgb(255, 0, 0);

                            //            }
                            //        }
                            //    }
                            //    #endregion 第三方抽查
                            //}

                        }));
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void setManagerToDefault()
        {
            #region 主管自评
            //label71.Text = "";
            //label71.BackColor = Color.FromArgb(255, 255, 255);
            //label77.Text = "";
            //label77.BackColor = Color.FromArgb(255, 255, 255);
            //label83.Text = "";
            //label83.BackColor = Color.FromArgb(255, 255, 255);
            //label89.Text = "";
            //label89.BackColor = Color.FromArgb(255, 255, 255);
            //label95.Text = "";
            //label95.BackColor = Color.FromArgb(255, 255, 255);
            //label101.Text = "";
            //label101.BackColor = Color.FromArgb(255, 255, 255);
            //label107.Text = "";
            //label107.BackColor = Color.FromArgb(255, 255, 255);
            //label113.Text = "";
            //label113.BackColor = Color.FromArgb(255, 255, 255);
            //label117.Text = "";
            //label117.BackColor = Color.FromArgb(255, 255, 255);
            #endregion 主管自评
        }
        private void setVsmLableToDefault()
        {

            #region vsm复合
            //label72.Text = "";
            //label72.BackColor = Color.FromArgb(255, 255, 255);
            //label78.Text = "";
            //label78.BackColor = Color.FromArgb(255, 255, 255);
            //label84.Text = "";
            //label84.BackColor = Color.FromArgb(255, 255, 255);
            //label90.Text = "";
            //label90.BackColor = Color.FromArgb(255, 255, 255);
            //label96.Text = "";
            //label96.BackColor = Color.FromArgb(255, 255, 255);
            //label102.Text = "";
            //label102.BackColor = Color.FromArgb(255, 255, 255);
            //label108.Text = "";
            //label108.BackColor = Color.FromArgb(255, 255, 255);
            //label114.Text = "";
            //label114.BackColor = Color.FromArgb(255, 255, 255);
            //label119.Text = "";
            //label119.BackColor = Color.FromArgb(255, 255, 255);
            #endregion vsm复合
        }

        private void setNonImpentationReasonsToDefalut()
        {
            //label74.Text = "";
            //label80.Text = "";
            //label86.Text = "";
            //label92.Text = "";
            //label98.Text = "";
            //label104.Text = "";
            //label110.Text = "";
            //label116.Text = "";
        }

        private void setThirdPartySpotCheckLableToDefault()
        {
            #region 第三方抽查
            //label73.Text = "";
            //label73.BackColor = Color.FromArgb(255, 255, 255);
            //label79.Text = "";
            //label79.BackColor = Color.FromArgb(255, 255, 255);
            //label85.Text = "";
            //label85.BackColor = Color.FromArgb(255, 255, 255);
            //label91.Text = "";
            //label91.BackColor = Color.FromArgb(255, 255, 255);
            //label97.Text = "";
            //label97.BackColor = Color.FromArgb(255, 255, 255);
            //label103.Text = "";
            //label103.BackColor = Color.FromArgb(255, 255, 255);
            //label109.Text = "";
            //label109.BackColor = Color.FromArgb(255, 255, 255);
            //label115.Text = "";
            //label115.BackColor = Color.FromArgb(255, 255, 255);
            //label121.Text = "";
            //label121.BackColor = Color.FromArgb(255, 255, 255);
            #endregion 第三方抽查
        }
        /// <summary>
        /// 层级会议 & 标准小线绩效
        /// </summary>
        public void tabPage4_query()
        {
            //try
            //{
            //    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Production_KanbanServer", "TabPage4_Query", Program.client.UserToken, string.Empty);
            //    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            //    {
            //        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
            //        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            //        this.Invoke(new Action(() =>
            //        {

            //        }));
            //    }
            //    else
            //    {
            //        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        /// <summary>
        /// KPI定义
        /// </summary>
        public void tabPage5_query()
        {
            //try
            //{
            //    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Production_KanbanServer", "TabPage5_Query", Program.client.UserToken, string.Empty);
            //    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            //    {
            //        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
            //        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            //        this.Invoke(new Action(() =>
            //        {

            //        }));
            //    }
            //    else
            //    {
            //        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        public void tabPage6_fast_query()
        {
            try
            {
                if (!string.IsNullOrEmpty(dateTimePicker1.Value.ToString()) && !string.IsNullOrEmpty(txtLine.Text))
                {
                    Dictionary<string, Object> p = new Dictionary<string, object>();
                    p.Add("date", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
                    p.Add("line", txtLine.Text);
                    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "TierMeeting", "TierMeeting.Controllers.TierMeetingServer", "TabPage6_Query_ScanDetail", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                        string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "TierMeeting", "TierMeeting.Controllers.TierMeetingServer", "TabPage6_Query_OtherDetail", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
                        {
                            string otherJson = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                            DataTable otherDtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(otherJson);
                            set_tabPage6_info(dtJson, otherDtJson);
                            string ret2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "TierMeeting", "TierMeeting.Controllers.TierMeetingServer", "Tier1_HourQuery", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
                            {
                                string rftJson = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                                DataTable rftDtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(rftJson);
                                set_tabPage6_rft(rftDtJson);
                                //InitHourlyReason();
                            }
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
                        }
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //弃用差异原因
        //private void InitHourlyReason() {
        //    RemoveSelectedReasonChanged();
        //    InitHourlyCombobox();
        //    GetTier1Hourly();
        //    SetSelectedReasonChanged();
        //}
        private void RemoveSelectedReasonChanged()
        {
            for (int i = 1; i <= 10; i++)
            {
                string cbxStr = "cbxReason" + i;
                ComboBox cbx = this.Controls.Find(cbxStr, true).FirstOrDefault() as ComboBox;
                cbx.SelectedValueChanged -= new EventHandler(SetTier1Hourly);
            }
        }
        private void SetSelectedReasonChanged()
        {
            for (int i = 1; i <= 10; i++)
            {
                string cbxStr = "cbxReason" + i;
                ComboBox cbx = this.Controls.Find(cbxStr, true).FirstOrDefault() as ComboBox;
                cbx.SelectedValueChanged += new EventHandler(SetTier1Hourly);
            }
        }
        private void InitHourlyCombobox()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("isActive", string.Empty);
            p.Add("cn", string.Empty);
            p.Add("en", string.Empty);
            p.Add("yn", string.Empty);
            p.Add("order", string.Empty);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetTier1HourlyReason",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    DataRow emptyRow = dtJson.NewRow();
                    emptyRow["id"] = "-1";
                    emptyRow["cn"] = "";
                    emptyRow["en"] = "";
                    emptyRow["yn"] = "";
                    emptyRow["g_order"] = "-1";
                    dtJson.Rows.Add(emptyRow);
                    dtJson.Columns.Add("Int32_Order", typeof(int), "g_order");
                    dtJson.DefaultView.Sort = "Int32_Order asc";
                    dtJson = dtJson.DefaultView.ToTable();
                    for (int i = 1; i <= 10; i++)
                    {
                        string cbxStr = "cbxReason" + i;
                        ComboBox cbx = this.Controls.Find(cbxStr, true).FirstOrDefault() as ComboBox;
                        cbx.DataSource = new DataView(dtJson);
                        cbx.ValueMember = "id";
                        cbx.DisplayMember = Program.Client.Language.Equals("hk") ? "yn" : Program.Client.Language;
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetTier1Hourly()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", txtLine.Text);
            p.Add("date", dateTimePicker1.Value.ToString("yyyy/MM/dd").Replace('-', '/'));
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetTier1Hourly",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        foreach (DataRow dr in dtJson.Rows)
                        {
                            if (i == dr["g_time_id"].ToInt())
                            {
                                string cbxStr = "cbxReason" + i;
                                ComboBox cbx = this.Controls.Find(cbxStr, true).FirstOrDefault() as ComboBox;
                                cbx.SelectedValue = dr["g_reason_id"].ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void SetTier1Hourly(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", txtLine.Text);
            p.Add("date", dateTimePicker1.Value.ToString("yyyy/MM/dd").Replace('-', '/'));
            p.Add("reason", cbx.SelectedValue);
            p.Add("time", System.Text.RegularExpressions.Regex.Split(cbx.Name, "cbxReason")[1]);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "SetTier1Hourly",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();

            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void set_tabPage6_rft_info(DataTable dtJson)
        {
            setRftToDefault(dtJson);
            for (int i = 0; i < dtJson.Rows.Count; i++)
            {
                if (i == 0)
                {
                    if (!string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) && decimal.Parse(dtJson.Rows[i]["num"].ToString()) < 85)
                    {
                        label248.BackColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        label248.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    label248.Text = string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) ? "" : decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                }
                if (i == 1)
                {
                    if (!string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) && decimal.Parse(dtJson.Rows[i]["num"].ToString()) < 85)
                    {
                        label255.BackColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        label255.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    //label255.Text = decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                    label255.Text = string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) ? "" : decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                }
                if (i == 2)
                {
                    if (!string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) && decimal.Parse(dtJson.Rows[i]["num"].ToString()) < 85)
                    {
                        label262.BackColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        label262.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    //label262.Text = decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                    label262.Text = string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) ? "" : decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                }
                if (i == 3)
                {
                    if (!string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) && decimal.Parse(dtJson.Rows[i]["num"].ToString()) < 85)
                    {
                        label269.BackColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        label269.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    //label269.Text = decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                    label269.Text = string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) ? "" : decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                }
                if (i == 4)
                {
                    if (!string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) && decimal.Parse(dtJson.Rows[i]["num"].ToString()) < 85)
                    {
                        label276.BackColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        label276.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    //label276.Text = decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                    label276.Text = string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) ? "" : decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");

                }
                if (i == 5)
                {
                    if (!string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) && decimal.Parse(dtJson.Rows[i]["num"].ToString()) < 85)
                    {
                        label283.BackColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        label283.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    //label283.Text = decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                    label283.Text = string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) ? "" : decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                }
                if (i == 6)
                {
                    if (!string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) && decimal.Parse(dtJson.Rows[i]["num"].ToString()) < 85)
                    {
                        label290.BackColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        label290.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    //label290.Text = decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                    label290.Text = string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) ? "" : decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                }
                if (i == 7)
                {
                    if (!string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) && decimal.Parse(dtJson.Rows[i]["num"].ToString()) < 85)
                    {
                        label297.BackColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        label297.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    //label297.Text = decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                    label297.Text = string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) ? "" : decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                }
                if (i == 8)
                {
                    if (!string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) && decimal.Parse(dtJson.Rows[i]["num"].ToString()) < 85)
                    {
                        label304.BackColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        label304.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    //label304.Text = decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                    label304.Text = string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) ? "" : decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                }
                if (i == 9)
                {
                    if (!string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) && decimal.Parse(dtJson.Rows[i]["num"].ToString()) < 85)
                    {
                        label311.BackColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        label311.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    //label311.Text = decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                    label311.Text = string.IsNullOrEmpty(dtJson.Rows[i]["num"].ToString()) ? "" : decimal.Parse(dtJson.Rows[i]["num"].ToString()).ToString("0.0");
                }
                if (i == dtJson.Rows.Count - 1)
                {
                    decimal sum = 0;
                    decimal rft = 0;
                    int cishu = 0;
                    for (int s = 0; s < dtJson.Rows.Count; s++)
                    {
                        if (!string.IsNullOrEmpty(dtJson.Rows[s]["num"].ToString()))
                        {
                            sum += decimal.Parse(dtJson.Rows[s]["num"].ToString());
                            cishu++;
                        }
                    }
                    rft = cishu == 0 ? 0 : (sum / cishu);
                    if (rft < 85)
                    {
                        label318.BackColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        label318.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    label318.Text = cishu == 0 ? "" : rft.ToString("0.0");

                }
            }
        }
        private void set_tabPage6_rft(DataTable dtJson)
        {
            this.Invoke(new Action(() =>
            {
                decimal total = 0;
                int totallun = 0;
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        label248.Text = dtJson.Rows[i][0].ToString();
                        if (txtLine.Text.Contains("C"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 98)
                            {
                                label248.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 93.1)
                            {
                                label248.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label248.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else if (txtLine.Text.Contains("S"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 97)
                            {
                                label248.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 92.15)
                            {
                                label248.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label248.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 90)
                            {
                                label248.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 85.5)
                            {
                                label248.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label248.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }

                    }
                    if (i == 1)
                    {
                        label255.Text = dtJson.Rows[i][0].ToString();
                        if (txtLine.Text.Contains("C"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 98)
                            {
                                label255.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 93.1)
                            {
                                label255.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label255.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else if (txtLine.Text.Contains("S"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 97)
                            {
                                label255.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 92.15)
                            {
                                label255.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label255.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 90)
                            {
                                label255.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 85.5)
                            {
                                label255.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label255.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                    }
                    if (i == 2)
                    {
                        label262.Text = dtJson.Rows[i][0].ToString();
                        if (txtLine.Text.Contains("C"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 98)
                            {
                                label262.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 93.1)
                            {
                                label262.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label262.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else if (txtLine.Text.Contains("S"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 97)
                            {
                                label262.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 92.15)
                            {
                                label262.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label262.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 90)
                            {
                                label262.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 85.5)
                            {
                                label262.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label262.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                    }
                    if (i == 3)
                    {
                        label269.Text = dtJson.Rows[i][0].ToString();
                        if (txtLine.Text.Contains("C"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 98)
                            {
                                label269.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 93.1)
                            {
                                label269.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label269.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else if (txtLine.Text.Contains("S"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 97)
                            {
                                label269.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 92.15)
                            {
                                label269.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label269.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 90)
                            {
                                label269.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 85.5)
                            {
                                label269.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label269.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                    }
                    if (i == 4)
                    {
                        label276.Text = dtJson.Rows[i][0].ToString();
                        if (txtLine.Text.Contains("C"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 98)
                            {
                                label276.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 93.1)
                            {
                                label276.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label276.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else if (txtLine.Text.Contains("S"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 97)
                            {
                                label276.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 92.15)
                            {
                                label276.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label276.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 90)
                            {
                                label276.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 85.5)
                            {
                                label276.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label276.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                    }
                    if (i == 5)
                    {
                        label283.Text = dtJson.Rows[i][0].ToString();
                        if (txtLine.Text.Contains("C"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 98)
                            {
                                label283.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 93.1)
                            {
                                label283.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label283.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else if (txtLine.Text.Contains("S"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 97)
                            {
                                label283.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 92.15)
                            {
                                label283.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label283.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 90)
                            {
                                label283.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 85.5)
                            {
                                label283.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label283.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                    }
                    if (i == 6)
                    {
                        label290.Text = dtJson.Rows[i][0].ToString();
                        if (txtLine.Text.Contains("C"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 98)
                            {
                                label290.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 93.1)
                            {
                                label290.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label290.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else if (txtLine.Text.Contains("S"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 97)
                            {
                                label290.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 92.15)
                            {
                                label290.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label290.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 90)
                            {
                                label290.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 85.5)
                            {
                                label290.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label290.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                    }
                    if (i == 7)
                    {
                        label297.Text = dtJson.Rows[i][0].ToString();
                        if (txtLine.Text.Contains("C"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 98)
                            {
                                label297.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 93.1)
                            {
                                label297.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label297.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else if (txtLine.Text.Contains("S"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 97)
                            {
                                label297.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 92.15)
                            {
                                label297.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label297.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 90)
                            {
                                label297.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 85.5)
                            {
                                label297.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label297.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                    }
                    if (i == 8)
                    {
                        label304.Text = dtJson.Rows[i][0].ToString();
                        if (txtLine.Text.Contains("C"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 98)
                            {
                                label304.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 93.1)
                            {
                                label304.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label304.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else if (txtLine.Text.Contains("S"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 97)
                            {
                                label304.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 92.15)
                            {
                                label304.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label304.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 90)
                            {
                                label304.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 85.5)
                            {
                                label304.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label304.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                    }
                    if (i == 9)
                    {
                        label311.Text = dtJson.Rows[i][0].ToString();
                        if (txtLine.Text.Contains("C"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 98)
                            {
                                label311.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 93.1)
                            {
                                label311.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label311.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else if (txtLine.Text.Contains("S"))
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 97)
                            {
                                label311.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 92.15)
                            {
                                label311.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label311.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            if (decimal.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 90)
                            {
                                label311.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (double.Parse(dtJson.Rows[i]["RFT"].ToString()) >= 85.5)
                            {
                                label311.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                label311.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                    }

                    ////////////////////////其他时间rft
                    double else_rft_sum = 0;
                    int else_rft_count = 0;
                    double else_rft = 0;
                    for (int k = 10; k < dtJson.Rows.Count; k++)
                    {
                        else_rft_sum += Convert.ToDouble(dtJson.Rows[k][0].ToString());
                        else_rft_count++;

                    }
                    if (else_rft_count > 0)
                    {
                        else_rft = Math.Round(else_rft_sum / else_rft_count, 2);
                    }

                    lb_else_rft.Text = else_rft == 0 ? "" : else_rft.ToString();
                    if (else_rft != 0)
                    {
                        if (txtLine.Text.Contains("C"))
                        {
                            if (else_rft >= 98)
                            {
                                lb_else_rft.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (else_rft >= 93.1)
                            {
                                lb_else_rft.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                lb_else_rft.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else if (txtLine.Text.Contains("S"))
                        {
                            if (else_rft >= 97)
                            {
                                lb_else_rft.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (else_rft >= 92.15)
                            {
                                lb_else_rft.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                lb_else_rft.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            if (else_rft >= 90)
                            {
                                lb_else_rft.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            else if (else_rft >= 85.5)
                            {
                                lb_else_rft.BackColor = Color.FromArgb(239, 222, 64);
                            }
                            else
                            {
                                lb_else_rft.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                    }
                    else
                    {


                        lb_else_rft.BackColor = Color.FromArgb(255, 0, 0);
                    }
















                    total += decimal.Parse(dtJson.Rows[i][0].ToString());
                    label318.Text = decimal.Parse((total / (i + 1)).ToString()).ToString("0.0");
                    if (i == dtJson.Rows.Count - 1)
                    {
                        Console.WriteLine("");

                    }
                    if (txtLine.Text.Contains("C"))
                    {
                        if (decimal.Parse(label318.Text.ToString()) >= 98)
                        {
                            label318.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        else if (double.Parse(label318.Text.ToString()) >= 93.1)
                        {
                            label318.BackColor = Color.FromArgb(239, 222, 64);
                        }
                        else
                        {
                            label318.BackColor = Color.FromArgb(255, 0, 0);
                        }
                    }
                    else if (txtLine.Text.Contains("S"))
                    {
                        if (decimal.Parse(label318.Text.ToString()) >= 97)
                        {
                            label318.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        else if (double.Parse(label318.Text.ToString()) >= 92.15)
                        {
                            label318.BackColor = Color.FromArgb(239, 222, 64);
                        }
                        else
                        {
                            label318.BackColor = Color.FromArgb(255, 0, 0);
                        }
                    }
                    else
                    {
                        if (decimal.Parse(label318.Text.ToString()) >= 90)
                        {
                            label318.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        else if (double.Parse(label318.Text.ToString()) >= 85.5)
                        {
                            label318.BackColor = Color.FromArgb(239, 222, 64);
                        }
                        else
                        {
                            label318.BackColor = Color.FromArgb(255, 0, 0);
                        }
                    }
                }
            }));
        }

        private void setRftToDefault(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0)
            {
                label248.BackColor = Color.FromArgb(255, 255, 255);
                label248.Text = "";
                label255.BackColor = Color.FromArgb(255, 255, 255);
                label255.Text = "";
                label262.BackColor = Color.FromArgb(255, 255, 255);
                label262.Text = "";
                label269.BackColor = Color.FromArgb(255, 255, 255);
                label269.Text = "";
                label276.BackColor = Color.FromArgb(255, 255, 255);
                label276.Text = "";
                label283.BackColor = Color.FromArgb(255, 255, 255);
                label283.Text = "";
                label290.BackColor = Color.FromArgb(255, 255, 255);
                label290.Text = "";
                label297.BackColor = Color.FromArgb(255, 255, 255);
                label297.Text = "";
                label304.BackColor = Color.FromArgb(255, 255, 255);
                label304.Text = "";
                label311.BackColor = Color.FromArgb(255, 255, 255);
                label311.Text = "";
                label318.BackColor = Color.FromArgb(255, 255, 255);
                label318.Text = "";
            }
            if (dataTable.Rows.Count == 1)
            {
                label255.BackColor = Color.FromArgb(255, 255, 255);
                label255.Text = "";
                label262.BackColor = Color.FromArgb(255, 255, 255);
                label262.Text = "";
                label269.BackColor = Color.FromArgb(255, 255, 255);
                label269.Text = "";
                label276.BackColor = Color.FromArgb(255, 255, 255);
                label276.Text = "";
                label283.BackColor = Color.FromArgb(255, 255, 255);
                label283.Text = "";
                label290.BackColor = Color.FromArgb(255, 255, 255);
                label290.Text = "";
                label297.BackColor = Color.FromArgb(255, 255, 255);
                label297.Text = "";
                label304.BackColor = Color.FromArgb(255, 255, 255);
                label304.Text = "";
                label311.BackColor = Color.FromArgb(255, 255, 255);
                label311.Text = "";
            }
            if (dataTable.Rows.Count == 2)
            {
                label262.BackColor = Color.FromArgb(255, 255, 255);
                label262.Text = "";
                label269.BackColor = Color.FromArgb(255, 255, 255);
                label269.Text = "";
                label276.BackColor = Color.FromArgb(255, 255, 255);
                label276.Text = "";
                label283.BackColor = Color.FromArgb(255, 255, 255);
                label283.Text = "";
                label290.BackColor = Color.FromArgb(255, 255, 255);
                label290.Text = "";
                label297.BackColor = Color.FromArgb(255, 255, 255);
                label297.Text = "";
                label304.BackColor = Color.FromArgb(255, 255, 255);
                label304.Text = "";
                label311.BackColor = Color.FromArgb(255, 255, 255);
                label311.Text = "";
            }
            if (dataTable.Rows.Count == 3)
            {
                label269.BackColor = Color.FromArgb(255, 255, 255);
                label269.Text = "";
                label276.BackColor = Color.FromArgb(255, 255, 255);
                label276.Text = "";
                label283.BackColor = Color.FromArgb(255, 255, 255);
                label283.Text = "";
                label290.BackColor = Color.FromArgb(255, 255, 255);
                label290.Text = "";
                label297.BackColor = Color.FromArgb(255, 255, 255);
                label297.Text = "";
                label304.BackColor = Color.FromArgb(255, 255, 255);
                label304.Text = "";
                label311.BackColor = Color.FromArgb(255, 255, 255);
                label311.Text = "";
            }
            if (dataTable.Rows.Count == 4)
            {
                label276.BackColor = Color.FromArgb(255, 255, 255);
                label276.Text = "";
                label283.BackColor = Color.FromArgb(255, 255, 255);
                label283.Text = "";
                label290.BackColor = Color.FromArgb(255, 255, 255);
                label290.Text = "";
                label297.BackColor = Color.FromArgb(255, 255, 255);
                label297.Text = "";
                label304.BackColor = Color.FromArgb(255, 255, 255);
                label304.Text = "";
                label311.BackColor = Color.FromArgb(255, 255, 255);
                label311.Text = "";
            }
            if (dataTable.Rows.Count == 5)
            {
                label283.BackColor = Color.FromArgb(255, 255, 255);
                label283.Text = "";
                label290.BackColor = Color.FromArgb(255, 255, 255);
                label290.Text = "";
                label297.BackColor = Color.FromArgb(255, 255, 255);
                label297.Text = "";
                label304.BackColor = Color.FromArgb(255, 255, 255);
                label304.Text = "";
                label311.BackColor = Color.FromArgb(255, 255, 255);
                label311.Text = "";
            }
            if (dataTable.Rows.Count == 6)
            {
                label290.BackColor = Color.FromArgb(255, 255, 255);
                label290.Text = "";
                label297.BackColor = Color.FromArgb(255, 255, 255);
                label297.Text = "";
                label304.BackColor = Color.FromArgb(255, 255, 255);
                label304.Text = "";
                label311.BackColor = Color.FromArgb(255, 255, 255);
                label311.Text = "";
            }
            if (dataTable.Rows.Count == 7)
            {
                label297.BackColor = Color.FromArgb(255, 255, 255);
                label297.Text = "";
                label304.BackColor = Color.FromArgb(255, 255, 255);
                label304.Text = "";
                label311.BackColor = Color.FromArgb(255, 255, 255);
                label311.Text = "";
            }
            if (dataTable.Rows.Count == 8)
            {
                label304.BackColor = Color.FromArgb(255, 255, 255);
                label304.Text = "";
                label311.BackColor = Color.FromArgb(255, 255, 255);
                label311.Text = "";
            }
            if (dataTable.Rows.Count == 9)
            {
                label311.BackColor = Color.FromArgb(255, 255, 255);
                label311.Text = "";
            }
        }

        //private void set_tabPage6_info(DataTable pDtJson, DataTable pOtherDtJson)
        //{
        //    if (pDtJson.Rows.Count==0)
        //        return;
        //    DataTable timeParams = new DataTable();
        //    timeParams.Columns.Add("time");
        //    DataRow dr1 = timeParams.NewRow();
        //    dr1[0] = label193.Text;
        //    DataRow dr2 = timeParams.NewRow();
        //    dr2[0] = label194.Text;
        //    DataRow dr3 = timeParams.NewRow();
        //    dr3[0] = label195.Text;
        //    DataRow dr4 = timeParams.NewRow();
        //    dr4[0] = label196.Text;
        //    DataRow dr5 = timeParams.NewRow();
        //    dr5[0] = label197.Text;
        //    DataRow dr6 = timeParams.NewRow();
        //    dr6[0] = label198.Text;
        //    DataRow dr7 = timeParams.NewRow();
        //    dr7[0] = label199.Text;
        //    DataRow dr8 = timeParams.NewRow();
        //    dr8[0] = label200.Text;
        //    DataRow dr9 = timeParams.NewRow();
        //    dr9[0] = label201.Text;
        //    DataRow dr10 = timeParams.NewRow();
        //    dr10[0] = label202.Text;
        //    timeParams.Rows.Add(dr1);
        //    timeParams.Rows.Add(dr2);
        //    timeParams.Rows.Add(dr3);
        //    timeParams.Rows.Add(dr4);
        //    timeParams.Rows.Add(dr5);
        //    timeParams.Rows.Add(dr6);
        //    timeParams.Rows.Add(dr7);
        //    timeParams.Rows.Add(dr8);
        //    timeParams.Rows.Add(dr9);
        //    timeParams.Rows.Add(dr10);

        //    DataTable dtJson = new DataTable();
        //    dtJson.Columns.Add("moldelName");
        //    dtJson.Columns.Add("workQty");
        //    dtJson.Columns.Add("Target");
        //    dtJson.Columns.Add("OperatorNo");
        //    dtJson.Columns.Add("WaterSpiderNo");
        //    dtJson.Columns.Add("amfrom");
        //    dtJson.Columns.Add("amto");
        //    dtJson.Columns.Add("pmfrom");
        //    dtJson.Columns.Add("pmto");
        //    dtJson.Columns.Add("totalHours");

        //    for (int i = 0; i < timeParams.Rows.Count; i++)
        //    {
        //        string from =timeParams.Rows[i][0].ToString().Split('~')[0].ToString()+":"+"00";
        //        string to = timeParams.Rows[i][0].ToString().Split('~')[1].ToString()+":"+"00";
        //        string expression;
        //        expression = string.Format("INSERT_TIME>='{0}'  and  INSERT_TIME<'{1}'", from,to);
        //        DataRow[] foundRows;
        //        foundRows = pDtJson.Select(expression);
        //        Console.WriteLine("从{0}到{1}有{2}行",from,to,foundRows.Count());

        //        DataRow dr = dtJson.NewRow();
        //        if (foundRows.Count()>=1)
        //        {
        //            dr[0] = foundRows[0][2];
        //        }
        //        var temp = 0;
        //        for (int k = 0; k < foundRows.Count(); k++) {
        //            temp += foundRows[k][3].ToInt();
        //        }
        //        //dr[1] = foundRows.Count();
        //        dr[1] = temp;
        //        if (pOtherDtJson.Rows.Count > 0)
        //        {
        //            dr[2] = pOtherDtJson.Rows[0][0];
        //            dr[3] = pOtherDtJson.Rows[0][1];
        //            dr[4] = pOtherDtJson.Rows[0][2];
        //            dr[9] = pOtherDtJson.Rows[0][3];

        //            dr[5] = pOtherDtJson.Rows[0][4];
        //            dr[6] = pOtherDtJson.Rows[0][5];
        //            dr[7] = pOtherDtJson.Rows[0][6];
        //            dr[8] = pOtherDtJson.Rows[0][7];

        //        }
        //        dtJson.Rows.Add(dr);
        //    }


        //    this.Invoke(new Action(() =>
        //    {
        //        int totalFinishQty = 0;
        //        int totalOperatorHours = 0;
        //        string target = "0";
        //        double hourQty = 0;
        //        for (int i = 0; i < dtJson.Rows.Count; i++)
        //        {
        //            target = dtJson.Rows[i][2].ToString();
        //            hourQty = double.Parse(dtJson.Rows[i][9].ToString()) == 0 ? 0 : double.Parse(dtJson.Rows[i][2].ToString()) / double.Parse(dtJson.Rows[i][9].ToString());
        //            totalFinishQty += int.Parse(dtJson.Rows[i][1].ToString());

        //            if (i == 0)
        //            {
        //                double time1 = Convert.ToDateTime(label193.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
        //                double time2 = Convert.ToDateTime(label193.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
        //                label244.Text = dtJson.Rows[0][0].ToString();
        //                label245.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
        //                label246.Text = dtJson.Rows[0][1].ToString();
        //                label247.Text = (decimal.Parse(label246.Text.ToString()) - decimal.Parse(label245.Text.ToString())).ToString();
        //                if (decimal.Parse(label246.Text.ToString()) - decimal.Parse(label245.Text.ToString()) < 0)
        //                {
        //                    label247.BackColor = Color.FromArgb(255, 0, 0);
        //                }
        //                else
        //                {
        //                    label247.BackColor = Color.FromArgb(255, 255, 255);
        //                }
        //            }
        //            else if (i == 1)
        //            {
        //                double time1 = Convert.ToDateTime(label194.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
        //                double time2 = Convert.ToDateTime(label194.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
        //                label243.Text = dtJson.Rows[1][0].ToString();
        //                label252.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
        //                label253.Text = dtJson.Rows[1][1].ToString();
        //                label254.Text = (decimal.Parse(label253.Text.ToString()) - decimal.Parse(label252.Text.ToString())).ToString();
        //                if (decimal.Parse(label253.Text.ToString()) - decimal.Parse(label252.Text.ToString()) < 0)
        //                {
        //                    label254.BackColor = Color.FromArgb(255, 0, 0);
        //                }
        //                else
        //                {
        //                    label254.BackColor = Color.FromArgb(255, 255, 255);
        //                }
        //            }
        //            else if (i == 2)
        //            {
        //                double time1 = Convert.ToDateTime(label195.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
        //                double time2 = Convert.ToDateTime(label195.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
        //                label242.Text = dtJson.Rows[2][0].ToString();
        //                label259.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
        //                label260.Text = dtJson.Rows[2][1].ToString();
        //                label261.Text = (decimal.Parse(label260.Text.ToString()) - decimal.Parse(label259.Text.ToString())).ToString();
        //                if (decimal.Parse(label260.Text.ToString()) - decimal.Parse(label259.Text.ToString()) < 0)
        //                {
        //                    label261.BackColor = Color.FromArgb(255, 0, 0);
        //                }
        //                else
        //                {
        //                    label261.BackColor = Color.FromArgb(255, 255, 255);
        //                }
        //            }
        //            else if (i == 3)
        //            {
        //                double time1 = Convert.ToDateTime(label196.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
        //                double time2 = Convert.ToDateTime(label196.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
        //                label241.Text = dtJson.Rows[3][0].ToString();
        //                label266.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
        //                label267.Text = dtJson.Rows[3][1].ToString();
        //                label268.Text = (decimal.Parse(label267.Text.ToString()) - decimal.Parse(label266.Text.ToString())).ToString();
        //                if (decimal.Parse(label267.Text.ToString()) - decimal.Parse(label266.Text.ToString()) < 0)
        //                {
        //                    label268.BackColor = Color.FromArgb(255, 0, 0);
        //                }
        //                else
        //                {
        //                    label268.BackColor = Color.FromArgb(255, 255, 255);
        //                }
        //            }
        //            else if (i == 4)
        //            {
        //                double time1 = Convert.ToDateTime(label197.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
        //                double time2 = Convert.ToDateTime(label197.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
        //                double time3 = dtJson.Rows[i][6] == null || string.IsNullOrEmpty(dtJson.Rows[i][6].ToString()) ? 0 : Convert.ToDateTime(dtJson.Rows[i][6].ToString()).TimeOfDay.TotalSeconds;
        //                double time4 = dtJson.Rows[i][7] == null || string.IsNullOrEmpty(dtJson.Rows[i][7].ToString()) ? 0 : Convert.ToDateTime(dtJson.Rows[i][7].ToString()).TimeOfDay.TotalSeconds;
        //                label240.Text = dtJson.Rows[4][0].ToString();
        //                label273.Text = (hourQty * (time2 - time1 - (time4 - time3)) / 3600).ToString("0");
        //                label274.Text = dtJson.Rows[4][1].ToString();
        //                label275.Text = (decimal.Parse(label274.Text.ToString()) - decimal.Parse(label273.Text.ToString())).ToString();
        //                if (decimal.Parse(label274.Text.ToString()) - decimal.Parse(label273.Text.ToString()) < 0)
        //                {
        //                    label275.BackColor = Color.FromArgb(255, 0, 0);
        //                }
        //                else
        //                {
        //                    label275.BackColor = Color.FromArgb(255, 255, 255);
        //                }
        //            }
        //            else if (i == 5)
        //            {
        //                double time1 = Convert.ToDateTime(label198.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
        //                double time2 = Convert.ToDateTime(label198.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
        //                label239.Text = dtJson.Rows[5][0].ToString();
        //                label280.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
        //                label281.Text = dtJson.Rows[5][1].ToString();
        //                label282.Text = (decimal.Parse(label281.Text.ToString()) - decimal.Parse(label280.Text.ToString())).ToString();
        //                if (decimal.Parse(label281.Text.ToString()) - decimal.Parse(label280.Text.ToString()) < 0)
        //                {
        //                    label282.BackColor = Color.FromArgb(255, 0, 0);
        //                }
        //                else
        //                {
        //                    label282.BackColor = Color.FromArgb(255, 255, 255);
        //                }

        //            }
        //            else if (i == 6)
        //            {
        //                double time1 = Convert.ToDateTime(label199.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
        //                double time2 = Convert.ToDateTime(label199.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
        //                label238.Text = dtJson.Rows[6][0].ToString();
        //                label287.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
        //                label288.Text = dtJson.Rows[6][1].ToString();
        //                label289.Text = (decimal.Parse(label288.Text.ToString()) - decimal.Parse(label287.Text.ToString())).ToString();
        //                if (decimal.Parse(label288.Text.ToString()) - decimal.Parse(label287.Text.ToString()) < 0)
        //                {
        //                    label289.BackColor = Color.FromArgb(255, 0, 0);
        //                }
        //                else
        //                {
        //                    label289.BackColor = Color.FromArgb(255, 255, 255);
        //                }
        //            }
        //            else if (i == 7)
        //            {
        //                double time1 = Convert.ToDateTime(label200.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
        //                double time2 = Convert.ToDateTime(label200.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
        //                label237.Text = dtJson.Rows[7][0].ToString();
        //                label294.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
        //                label295.Text = dtJson.Rows[7][1].ToString();
        //                label296.Text = (decimal.Parse(label295.Text.ToString()) - decimal.Parse(label294.Text.ToString())).ToString();
        //                if (decimal.Parse(label295.Text.ToString()) - decimal.Parse(label294.Text.ToString()) < 0)
        //                {
        //                    label296.BackColor = Color.FromArgb(255, 0, 0);
        //                }
        //                else
        //                {
        //                    label296.BackColor = Color.FromArgb(255, 255, 255);
        //                }
        //            }
        //            else if (i == 8)
        //            {
        //                double time1 = Convert.ToDateTime(label201.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
        //                double time2 = Convert.ToDateTime(label201.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
        //                label236.Text = dtJson.Rows[8][0].ToString();
        //                label301.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
        //                label302.Text = dtJson.Rows[8][1].ToString();
        //                label303.Text = (decimal.Parse(label302.Text.ToString()) - decimal.Parse(label301.Text.ToString())).ToString();
        //                if (decimal.Parse(label302.Text.ToString()) - decimal.Parse(label301.Text.ToString()) < 0)
        //                {
        //                    label303.BackColor = Color.FromArgb(255, 0, 0);
        //                }
        //                else
        //                {
        //                    label303.BackColor = Color.FromArgb(255, 255, 255);
        //                }
        //            }
        //            else if (i == 9)
        //            {
        //                double time1 = Convert.ToDateTime(label202.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
        //                double time2 = Convert.ToDateTime(label202.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
        //                label235.Text = dtJson.Rows[9][0].ToString();
        //                label308.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
        //                label309.Text = dtJson.Rows[9][1].ToString();
        //                label310.Text = (decimal.Parse(label309.Text.ToString()) - decimal.Parse(label308.Text.ToString())).ToString();
        //                if (decimal.Parse(label309.Text.ToString()) - decimal.Parse(label308.Text.ToString()) < 0)
        //                {
        //                    label310.BackColor = Color.FromArgb(255, 0, 0);
        //                }
        //                else
        //                {
        //                    label310.BackColor = Color.FromArgb(255, 255, 255);
        //                }
        //            }
        //        }

        //        label316.Text = totalFinishQty.ToString();
        //        label315.Text = target;
        //        label317.Text = (totalFinishQty - int.Parse(target)).ToString("0");
        //        if (totalFinishQty - int.Parse(target) < 0)
        //        {
        //            label317.BackColor = Color.FromArgb(255, 0, 0);
        //        }
        //        else
        //        {
        //            label317.BackColor = Color.FromArgb(255, 255, 255);
        //        }
        //        //label32.Text = "√";
        //        //label33.Text = "√";
        //        //label34.Text = "√";
        //        //label35.Text = "√";
        //        //label36.Text = "√";
        //        //label37.Text = "√";
        //        //label38.Text = "√";
        //        //label39.Text = "√";
        //        //label51.Text = "";
        //        //label122.Text = "√";
        //    }));
        //}
        private void set_tabPage6_info(DataTable pDtJson, DataTable pOtherDtJson)
        {
            if (pDtJson.Rows.Count == 0)
                return;
            DataTable timeParams = new DataTable();
            timeParams.Columns.Add("time");
            DataRow dr1 = timeParams.NewRow();
            dr1[0] = label193.Text;
            DataRow dr2 = timeParams.NewRow();
            dr2[0] = label194.Text;
            DataRow dr3 = timeParams.NewRow();
            dr3[0] = label195.Text;
            DataRow dr4 = timeParams.NewRow();
            dr4[0] = label196.Text;
            DataRow dr5 = timeParams.NewRow();
            dr5[0] = label197.Text;
            DataRow dr6 = timeParams.NewRow();
            dr6[0] = label198.Text;
            DataRow dr7 = timeParams.NewRow();
            dr7[0] = label199.Text;
            DataRow dr8 = timeParams.NewRow();
            dr8[0] = label200.Text;
            DataRow dr9 = timeParams.NewRow();
            dr9[0] = label201.Text;
            DataRow dr10 = timeParams.NewRow();
            dr10[0] = label202.Text;
            // DataRow dr11 = timeParams.NewRow();
            // dr11[0] = label39.Text;
            timeParams.Rows.Add(dr1);
            timeParams.Rows.Add(dr2);
            timeParams.Rows.Add(dr3);
            timeParams.Rows.Add(dr4);
            timeParams.Rows.Add(dr5);
            timeParams.Rows.Add(dr6);
            timeParams.Rows.Add(dr7);
            timeParams.Rows.Add(dr8);
            timeParams.Rows.Add(dr9);
            timeParams.Rows.Add(dr10);
            //   timeParams.Rows.Add(dr11);

            //
            DataTable dtJson = new DataTable();
            dtJson.Columns.Add("moldelName");
            dtJson.Columns.Add("workQty");
            dtJson.Columns.Add("Target");
            dtJson.Columns.Add("OperatorNo");
            dtJson.Columns.Add("WaterSpiderNo");
            dtJson.Columns.Add("amfrom");
            dtJson.Columns.Add("amto");
            dtJson.Columns.Add("pmfrom");
            dtJson.Columns.Add("pmto");
            dtJson.Columns.Add("totalHours");

            for (int i = 0; i < timeParams.Rows.Count; i++)
            {


                string from = timeParams.Rows[i][0].ToString().Split('~')[0].ToString() + ":" + "00";
                string to = timeParams.Rows[i][0].ToString().Split('~')[1].ToString() + ":" + "00";
                string expression;
                expression = string.Format("INSERT_TIME>='{0}'  and  INSERT_TIME<'{1}'", from, to);
                DataRow[] foundRows;
                foundRows = pDtJson.Select(expression);
                Console.WriteLine("从{0}到{1}有{2}行", from, to, foundRows.Count());

                DataRow dr = dtJson.NewRow();
                if (foundRows.Count() >= 1)
                {
                    dr[0] = foundRows[0][2];



                }
                var temp = 0;
                for (int k = 0; k < foundRows.Count(); k++)
                {
                    temp += foundRows[k][3].ToInt();
                }
                //dr[1] = foundRows.Count();
                dr[1] = temp;
                if (pOtherDtJson.Rows.Count > 0)
                {
                    dr[2] = pOtherDtJson.Rows[0][0];
                    dr[3] = pOtherDtJson.Rows[0][1];
                    dr[4] = pOtherDtJson.Rows[0][2];
                    dr[9] = pOtherDtJson.Rows[0][3];

                    dr[5] = pOtherDtJson.Rows[0][4];
                    dr[6] = pOtherDtJson.Rows[0][5];
                    dr[7] = pOtherDtJson.Rows[0][6];
                    dr[8] = pOtherDtJson.Rows[0][7];

                }
                dtJson.Rows.Add(dr);
            }


            this.Invoke(new Action(() =>
            {
                int totalFinishQty = 0;
                int totalOperatorHours = 0;
                decimal totalZQRenShi = 0;
                decimal totalCY = 0;
                string target = "0";
                double hourQty = 0;
                //int avqty = 0;
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    target = dtJson.Rows[i][2].ToString();
                    hourQty = double.Parse(dtJson.Rows[i][9].ToString()) == 0 ? 0 : double.Parse(dtJson.Rows[i][2].ToString()) / double.Parse(dtJson.Rows[i][9].ToString());
                    totalFinishQty += int.Parse(dtJson.Rows[i][1].ToString());
                    label316.Text = totalFinishQty.ToString();
                    //avqty = int.Parse(label316.Text.ToString()) / 10;
                    if (i == 0)
                    {
                        double time1 = Convert.ToDateTime(label193.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                        double time2 = Convert.ToDateTime(label193.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                        label244.Text = dtJson.Rows[0][0].ToString();
                        label245.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
                        label246.Text = dtJson.Rows[0][1].ToString();
                        label247.Text = (decimal.Parse(label246.Text.ToString()) - decimal.Parse(label245.Text.ToString())).ToString();
                        if (decimal.Parse(label246.Text.ToString()) - decimal.Parse(label245.Text.ToString()) < 0)
                        {
                            label247.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label247.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        label249.Text = label52.Text;
                        totalOperatorHours = totalOperatorHours + int.Parse((string.IsNullOrEmpty(label52.Text) ? "0" : label52.Text));
                        label250.Text = (int.Parse(label246.Text.ToString()) * decimal.Parse(string.IsNullOrEmpty(rtbTHT.Text) ? "0" : rtbTHT.Text.ToString()) / 3600).ToString();
                        label250.Text = decimal.Round(decimal.Parse(label250.Text.ToString())).ToString();
                        totalZQRenShi = totalZQRenShi + (int)(decimal.Parse(label250.Text.ToString()));
                        label251.Text = (decimal.Parse(label250.Text.ToString()) - decimal.Parse(label249.Text.ToString())).ToString();
                        totalCY = totalCY + decimal.Parse(label251.Text.ToString());
                        if (decimal.Parse(label251.Text.ToString()) < 0)
                        {
                            label251.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label251.BackColor = Color.FromArgb(58, 152, 54);
                        }
                    }
                    else if (i == 1)
                    {
                        double time1 = Convert.ToDateTime(label194.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                        double time2 = Convert.ToDateTime(label194.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                        label243.Text = dtJson.Rows[1][0].ToString();
                        label252.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
                        label253.Text = dtJson.Rows[1][1].ToString();
                        label254.Text = (decimal.Parse(label253.Text.ToString()) - decimal.Parse(label252.Text.ToString())).ToString();
                        if (decimal.Parse(label253.Text.ToString()) - decimal.Parse(label252.Text.ToString()) < 0)
                        {
                            label254.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label254.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        label256.Text = label52.Text;
                        totalOperatorHours = totalOperatorHours + int.Parse((string.IsNullOrEmpty(label52.Text) ? "0" : label52.Text));
                        label257.Text = (decimal.Parse(label253.Text.ToString()) * decimal.Parse(string.IsNullOrEmpty(rtbTHT.Text) ? "0" : rtbTHT.Text.ToString()) / 3600).ToString();
                        label257.Text = decimal.Round(decimal.Parse(label257.Text.ToString())).ToString();
                        totalZQRenShi = totalZQRenShi + decimal.Parse(label257.Text.ToString());

                        label258.Text = (decimal.Parse(label257.Text.ToString()) - decimal.Parse(label256.Text.ToString())).ToString();
                        totalCY = totalCY + decimal.Parse(label258.Text.ToString());
                        if (decimal.Parse(label258.Text.ToString()) < 0)
                        {
                            label258.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label258.BackColor = Color.FromArgb(58, 152, 54);
                        }
                    }
                    else if (i == 2)
                    {
                        double time1 = Convert.ToDateTime(label195.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                        double time2 = Convert.ToDateTime(label195.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                        label242.Text = dtJson.Rows[2][0].ToString();
                        label259.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
                        label260.Text = dtJson.Rows[2][1].ToString();
                        label261.Text = (decimal.Parse(label260.Text.ToString()) - decimal.Parse(label259.Text.ToString())).ToString();
                        if (decimal.Parse(label260.Text.ToString()) - decimal.Parse(label259.Text.ToString()) < 0)
                        {
                            label261.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label261.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        label263.Text = label52.Text;
                        totalOperatorHours = totalOperatorHours + int.Parse((string.IsNullOrEmpty(label52.Text) ? "0" : label52.Text));
                        label264.Text = (decimal.Parse(label260.Text.ToString()) * decimal.Parse(string.IsNullOrEmpty(rtbTHT.Text) ? "0" : rtbTHT.Text.ToString()) / 3600).ToString();
                        label264.Text = decimal.Round(decimal.Parse(label264.Text.ToString())).ToString();
                        totalZQRenShi = totalZQRenShi + decimal.Parse(label264.Text.ToString());


                        label265.Text = (decimal.Parse(label264.Text.ToString()) - decimal.Parse(label263.Text.ToString())).ToString();
                        totalCY = totalCY + decimal.Parse(label265.Text.ToString());
                        if (decimal.Parse(label265.Text.ToString()) < 0)
                        {
                            label265.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label265.BackColor = Color.FromArgb(58, 152, 54);
                        }
                    }
                    else if (i == 3)
                    {
                        double time1 = Convert.ToDateTime(label196.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                        double time2 = Convert.ToDateTime(label196.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                        label241.Text = dtJson.Rows[3][0].ToString();
                        label266.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
                        label267.Text = dtJson.Rows[3][1].ToString();
                        label268.Text = (decimal.Parse(label267.Text.ToString()) - decimal.Parse(label266.Text.ToString())).ToString();
                        if (decimal.Parse(label267.Text.ToString()) - decimal.Parse(label266.Text.ToString()) < 0)
                        {
                            label268.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label268.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        label270.Text = label52.Text;
                        totalOperatorHours = totalOperatorHours + int.Parse((string.IsNullOrEmpty(label52.Text) ? "0" : label52.Text));
                        label271.Text = (decimal.Parse(label267.Text.ToString()) * decimal.Parse(string.IsNullOrEmpty(rtbTHT.Text) ? "0" : rtbTHT.Text.ToString()) / 3600).ToString();
                        label271.Text = decimal.Round(decimal.Parse(label271.Text.ToString())).ToString();

                        totalZQRenShi = totalZQRenShi + decimal.Parse(label271.Text.ToString());

                        label272.Text = (decimal.Parse(label271.Text.ToString()) - decimal.Parse(label270.Text.ToString())).ToString();
                        totalCY = totalCY + decimal.Parse(label272.Text.ToString());
                        if (decimal.Parse(label272.Text.ToString()) < 0)
                        {
                            label272.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label272.BackColor = Color.FromArgb(58, 152, 54);
                        }

                    }
                    else if (i == 4)
                    {
                        double time1 = Convert.ToDateTime(label197.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                        double time2 = Convert.ToDateTime(label197.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                        double time3 = dtJson.Rows[i][6] == null || string.IsNullOrEmpty(dtJson.Rows[i][6].ToString()) ? 0 : Convert.ToDateTime(dtJson.Rows[i][6].ToString()).TimeOfDay.TotalSeconds;
                        double time4 = dtJson.Rows[i][7] == null || string.IsNullOrEmpty(dtJson.Rows[i][7].ToString()) ? 0 : Convert.ToDateTime(dtJson.Rows[i][7].ToString()).TimeOfDay.TotalSeconds;
                        label240.Text = dtJson.Rows[4][0].ToString();
                        label273.Text = (hourQty * (time2 - time1 - (time4 - time3)) / 3600).ToString("0");
                        label274.Text = dtJson.Rows[4][1].ToString();
                        label275.Text = (decimal.Parse(label274.Text.ToString()) - decimal.Parse(label273.Text.ToString())).ToString();
                        if (decimal.Parse(label274.Text.ToString()) - decimal.Parse(label273.Text.ToString()) < 0)
                        {
                            label275.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label275.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        label277.Text = label52.Text;
                        totalOperatorHours = totalOperatorHours + int.Parse((string.IsNullOrEmpty(label52.Text) ? "0" : label52.Text));


                        label278.Text = (decimal.Parse(label274.Text.ToString()) * decimal.Parse(string.IsNullOrEmpty(rtbTHT.Text) ? "0" : rtbTHT.Text.ToString()) / 3600).ToString();
                        label278.Text = decimal.Round(decimal.Parse(label278.Text.ToString())).ToString();
                        totalZQRenShi = totalZQRenShi + decimal.Parse(label278.Text.ToString());
                        label279.Text = (decimal.Parse(label278.Text.ToString()) - decimal.Parse(label277.Text.ToString())).ToString();
                        totalCY = totalCY + decimal.Parse(label279.Text.ToString());
                        if (decimal.Parse(label279.Text.ToString()) < 0)
                        {
                            label279.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label279.BackColor = Color.FromArgb(58, 152, 54);
                        }

                    }
                    else if (i == 5)
                    {
                        double time1 = Convert.ToDateTime(label198.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                        double time2 = Convert.ToDateTime(label198.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                        label239.Text = dtJson.Rows[5][0].ToString();
                        label280.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
                        label281.Text = dtJson.Rows[5][1].ToString();
                        label282.Text = (decimal.Parse(label281.Text.ToString()) - decimal.Parse(label280.Text.ToString())).ToString();
                        if (decimal.Parse(label281.Text.ToString()) - decimal.Parse(label280.Text.ToString()) < 0)
                        {
                            label282.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label282.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        label284.Text = label52.Text;
                        totalOperatorHours = totalOperatorHours + int.Parse((string.IsNullOrEmpty(label52.Text) ? "0" : label52.Text));


                        label285.Text = (decimal.Parse(label281.Text.ToString()) * decimal.Parse(string.IsNullOrEmpty(rtbTHT.Text) ? "0" : rtbTHT.Text.ToString()) / 3600).ToString();
                        label285.Text = decimal.Round(decimal.Parse(label285.Text.ToString())).ToString();
                        totalZQRenShi = totalZQRenShi + decimal.Parse(label285.Text.ToString());
                        label286.Text = (decimal.Parse(label285.Text.ToString()) - decimal.Parse(label284.Text.ToString())).ToString();
                        totalCY = totalCY + decimal.Parse(label286.Text.ToString());
                        if (decimal.Parse(label286.Text.ToString()) < 0)
                        {
                            label286.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label286.BackColor = Color.FromArgb(58, 152, 54);
                        }

                    }
                    else if (i == 6)
                    {
                        double time1 = Convert.ToDateTime(label199.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                        double time2 = Convert.ToDateTime(label199.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                        label238.Text = dtJson.Rows[6][0].ToString();
                        label287.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
                        label288.Text = dtJson.Rows[6][1].ToString();
                        label289.Text = (decimal.Parse(label288.Text.ToString()) - decimal.Parse(label287.Text.ToString())).ToString();
                        if (decimal.Parse(label288.Text.ToString()) - decimal.Parse(label287.Text.ToString()) < 0)
                        {
                            label289.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label289.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        label291.Text = label52.Text;
                        totalOperatorHours = totalOperatorHours + int.Parse((string.IsNullOrEmpty(label52.Text) ? "0" : label52.Text));


                        label292.Text = (decimal.Parse(label288.Text.ToString()) * decimal.Parse(string.IsNullOrEmpty(rtbTHT.Text) ? "0" : rtbTHT.Text.ToString()) / 3600).ToString();
                        label292.Text = decimal.Round(decimal.Parse(label292.Text.ToString())).ToString();
                        totalZQRenShi = totalZQRenShi + decimal.Parse(label292.Text.ToString());
                        label293.Text = (decimal.Parse(label292.Text.ToString()) - decimal.Parse(label291.Text.ToString())).ToString();
                        totalCY = totalCY + decimal.Parse(label293.Text.ToString());
                        if (decimal.Parse(label293.Text.ToString()) < 0)
                        {
                            label293.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label293.BackColor = Color.FromArgb(58, 152, 54);
                        }
                    }
                    else if (i == 7)
                    {
                        double time1 = Convert.ToDateTime(label200.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                        double time2 = Convert.ToDateTime(label200.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                        label237.Text = dtJson.Rows[7][0].ToString();
                        label294.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
                        label295.Text = dtJson.Rows[7][1].ToString();
                        label296.Text = (decimal.Parse(label295.Text.ToString()) - decimal.Parse(label294.Text.ToString())).ToString();
                        if (decimal.Parse(label295.Text.ToString()) - decimal.Parse(label294.Text.ToString()) < 0)
                        {
                            label296.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label296.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        label298.Text = label52.Text;
                        totalOperatorHours = totalOperatorHours + int.Parse((string.IsNullOrEmpty(label52.Text) ? "0" : label52.Text));



                        label299.Text = (decimal.Parse(label295.Text.ToString()) * decimal.Parse(string.IsNullOrEmpty(rtbTHT.Text) ? "0" : rtbTHT.Text.ToString()) / 3600).ToString();
                        label299.Text = decimal.Round(decimal.Parse(label299.Text.ToString())).ToString();
                        totalZQRenShi = totalZQRenShi + decimal.Parse(label299.Text.ToString());
                        label300.Text = (decimal.Parse(label299.Text.ToString()) - decimal.Parse(label298.Text.ToString())).ToString();
                        totalCY = totalCY + decimal.Parse(label300.Text.ToString());
                        if (decimal.Parse(label300.Text.ToString()) < 0)
                        {
                            label300.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label300.BackColor = Color.FromArgb(58, 152, 54);
                        }

                    }
                    else if (i == 8)
                    {
                        double time1 = Convert.ToDateTime(label201.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                        double time2 = Convert.ToDateTime(label201.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                        label236.Text = dtJson.Rows[8][0].ToString();
                        label301.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
                        label302.Text = dtJson.Rows[8][1].ToString();
                        label303.Text = (decimal.Parse(label302.Text.ToString()) - decimal.Parse(label301.Text.ToString())).ToString();
                        if (decimal.Parse(label302.Text.ToString()) - decimal.Parse(label301.Text.ToString()) < 0)
                        {
                            label303.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label303.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        label305.Text = label52.Text;
                        totalOperatorHours = totalOperatorHours + int.Parse((string.IsNullOrEmpty(label52.Text) ? "0" : label52.Text));


                        label306.Text = (decimal.Parse(label302.Text.ToString()) * decimal.Parse(string.IsNullOrEmpty(rtbTHT.Text) ? "0" : rtbTHT.Text.ToString()) / 3600).ToString();
                        label306.Text = decimal.Round(decimal.Parse(label306.Text.ToString())).ToString();
                        totalZQRenShi = totalZQRenShi + decimal.Parse(label306.Text.ToString());

                        label307.Text = (decimal.Parse(label306.Text.ToString()) - decimal.Parse(label305.Text.ToString())).ToString();
                        totalCY = totalCY + decimal.Parse(label307.Text.ToString());
                        if (decimal.Parse(label307.Text.ToString()) < 0)
                        {
                            label307.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label307.BackColor = Color.FromArgb(58, 152, 54);
                        }


                    }
                    else if (i == 9)
                    {
                        double time1 = Convert.ToDateTime(label202.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                        double time2 = Convert.ToDateTime(label202.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                        label235.Text = dtJson.Rows[9][0].ToString();
                        label308.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
                        label309.Text = dtJson.Rows[9][1].ToString();
                        label310.Text = (decimal.Parse(label309.Text.ToString()) - decimal.Parse(label308.Text.ToString())).ToString();
                        if (decimal.Parse(label309.Text.ToString()) - decimal.Parse(label308.Text.ToString()) < 0)
                        {
                            label310.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label310.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        label312.Text = label52.Text;
                        totalOperatorHours = totalOperatorHours + int.Parse((string.IsNullOrEmpty(label52.Text) ? "0" : label52.Text));



                        label313.Text = (decimal.Parse(label309.Text.ToString()) * decimal.Parse(string.IsNullOrEmpty(rtbTHT.Text) ? "0" : rtbTHT.Text) / 3600).ToString();
                        label313.Text = decimal.Round(decimal.Parse(label313.Text.ToString())).ToString();
                        totalZQRenShi = totalZQRenShi + decimal.Parse(label313.Text.ToString());

                        label314.Text = (decimal.Parse(label313.Text.ToString()) - decimal.Parse(label312.Text.ToString())).ToString();
                        totalCY = totalCY + decimal.Parse(label314.Text.ToString());
                        if (decimal.Parse(label314.Text.ToString()) < 0)
                        {
                            label314.BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else
                        {
                            label314.BackColor = Color.FromArgb(58, 152, 54);
                        }

                    }
                }








                //label293.Text = (decimal.Parse(label292.Text.ToString()) - decimal.Parse(label291.Text.ToString())).ToString();
                //totalCY = totalCY + decimal.Parse(label293.Text.ToString());




                #region   //other time
                //pOtherDtJson//pDtJson
                double sum_targe = GetSumFromLayout(tableLayoutPanel45, 0, 0, 9);//Get the target output at 18:30 and
                double else_plan_qty = double.Parse(target) - sum_targe;//Calculate target output at other times
                lb_else_plan.Text = else_plan_qty.ToString();//Set target output for other times
                int else_actual_qty = 0;
                string expression;
                expression = string.Format("INSERT_TIME>='{0}'  and  INSERT_TIME<'{1}'", "18:30:00", "24:00:00");
                DataRow[] foundRows;
                foundRows = pDtJson.Select(expression);
                if (foundRows.Length > 0)
                {
                    lb_else_model.Text = foundRows[0][2].ToString();
                    foreach (DataRow dataRow in foundRows)
                    {
                        else_actual_qty += Convert.ToInt16(dataRow[3].ToString());


                    }
                }
                else
                {
                    lb_else_model.Text = "";
                }
                lb_else_actual.Text = else_actual_qty.ToString();//设置其他时间实际产量
                lb_else_difference.Text = (else_actual_qty - else_plan_qty).ToString();//计算其他时间产量差异

                totalFinishQty += else_actual_qty;//增加 其他时间实际产量到合计

                lb_else_hours.Text = label52.Text;//设置其他时间人时
                totalOperatorHours = totalOperatorHours + int.Parse((string.IsNullOrEmpty(label52.Text) ? "0" : label52.Text));
                //     label52.Text = (decimal.Parse(lb_else_actual.Text.ToString()) * decimal.Parse(string.IsNullOrEmpty(rtbTHT.Text)?"0": rtbTHT.Text) / 3600).ToString();
                //     label52.Text = decimal.Round(decimal.Parse((string.IsNullOrEmpty(label52.Text)?"0": label52.Text) )).ToString();
                //   totalZQRenShi = totalZQRenShi + decimal.Parse((string.IsNullOrEmpty(label52.Text)?"0": label52.Text) );

                lb_else_et.Text = (decimal.Parse(lb_else_actual.Text.ToString()) * decimal.Parse(string.IsNullOrEmpty(rtbTHT.Text) ? "0" : rtbTHT.Text) / 3600).ToString();
                lb_else_et.Text = decimal.Round(decimal.Parse(lb_else_et.Text.ToString())).ToString();
                totalZQRenShi += decimal.Parse(lb_else_et.Text.ToString());

                lb_else_hours_difference.Text = (decimal.Parse(lb_else_et.Text.ToString()) - decimal.Parse(lb_else_hours.Text.ToString())).ToString();
                if (decimal.Parse(lb_else_difference.Text.ToString()) < 0)
                {
                    lb_else_difference.BackColor = Color.FromArgb(255, 0, 0);
                }
                else
                {
                    lb_else_difference.BackColor = Color.FromArgb(58, 152, 54);
                }
                if (decimal.Parse(lb_else_hours_difference.Text.ToString()) < 0)
                {
                    lb_else_hours_difference.BackColor = Color.FromArgb(255, 0, 0);
                }
                else
                {
                    lb_else_hours_difference.BackColor = Color.FromArgb(58, 152, 54);
                }




                #endregion



                #region   合计部分
                label316.Text = totalFinishQty.ToString();
                label317.Text = (totalFinishQty - int.Parse(target)).ToString("0");//计算设置合计差异


                label315.Text = target;

                label319.Text = totalOperatorHours.ToString();
                label320.Text = totalZQRenShi.ToString();
                label321.Text = totalCY.ToString();

                if (totalFinishQty - int.Parse(target) < 0)
                {
                    label317.BackColor = Color.FromArgb(255, 0, 0);
                }
                else
                {
                    label317.BackColor = Color.FromArgb(58, 152, 54);
                }

                if (totalCY < 0)
                {
                    label321.BackColor = Color.FromArgb(255, 0, 0);
                }
                else
                {
                    label321.BackColor = Color.FromArgb(58, 152, 54);
                }
                if (int.Parse(label317.Text) < 0)
                {
                    label317.BackColor = Color.FromArgb(255, 0, 0);
                }
                else
                {
                    label317.BackColor = Color.FromArgb(58, 152, 54);
                }
                #endregion

                //  label266.Text = (hourQty * (time2 - time1) / 3600).ToString("0");
                //label267.Text = dtJson.Rows[3][1].ToString();
                //label268.Text = (decimal.Parse(label267.Text.ToString()) - decimal.Parse(label266.Text.ToString())).ToString();
                //if (decimal.Parse(label267.Text.ToString()) - decimal.Parse(label266.Text.ToString()) < 0)
                //{
                //    label268.BackColor = Color.FromArgb(255, 0, 0);
                //}
                //else
                //{
                //    label268.BackColor = Color.FromArgb(58, 152, 54);
                //}
                //label270.Text = label52.Text;
                //totalOperatorHours = totalOperatorHours + int.Parse((string.IsNullOrEmpty(label52.Text)?"0": label52.Text) );
                //label271.Text = (decimal.Parse(label267.Text.ToString()) * decimal.Parse(string.IsNullOrEmpty(rtbTHT.Text)?"0": rtbTHT.Text) / 3600).ToString();
                //label271.Text = decimal.Round(decimal.Parse(label271.Text.ToString())).ToString();

                //totalZQRenShi = totalZQRenShi + decimal.Parse(label271.Text.ToString());

                //label272.Text = (decimal.Parse(label271.Text.ToString()) - decimal.Parse(label270.Text.ToString())).ToString();
                //totalCY = totalCY + decimal.Parse(label272.Text.ToString());
                //if (decimal.Parse(label272.Text.ToString()) < 0)
                //{
                //    label272.BackColor = Color.FromArgb(255, 0, 0);
                //}
                //else
                //{
                //    label272.BackColor = Color.FromArgb(58, 152, 54);
                //}
                //label32.Text = "√";
                //label33.Text = "√";
                //label34.Text = "√";
                //label35.Text = "√";
                //label36.Text = "√";
                //label37.Text = "√";
                //label38.Text = "√";
                //label39.Text = "√";
                //label51.Text = "";
                //label122.Text = "√";
            }));


        }
        /// <summary>
        /// 获取TableLayoutPanel中符合条件的lable的和
        /// </summary>
        /// <param name="tlp">TableLayoutPanel</param>
        /// <param name="column_index">列索引</param>
        /// <param name="row_statr">行起始索引</param>
        /// <param name="row_end">行结束索引</param>
        /// <returns>和</returns>
        public double GetSumFromLayout(TableLayoutPanel tlp, int column_index, int row_statr, int row_end)
        {
            double sum = 0;
            foreach (Control lb in tlp.Controls)
            {
                int row_index = tlp.GetRow(lb);
                if (lb is Label && tlp.GetColumn(lb) == column_index && row_statr <= row_index && row_index <= row_end)
                {
                    sum += Convert.ToDouble(lb.Text.ToString());

                }


            }
            return sum;
        }


        /// <summary>
        /// 每小时生产管理表
        /// </summary>
        public void tabPage6_query()
        {
            try
            {
                if (!string.IsNullOrEmpty(dateTimePicker1.Value.ToString()) && !string.IsNullOrEmpty(txtLine.Text))
                {
                    DataTable timeParams = new DataTable();
                    timeParams.Columns.Add("time");
                    DataRow dr1 = timeParams.NewRow();
                    dr1[0] = label193.Text;
                    DataRow dr2 = timeParams.NewRow();
                    dr2[0] = label194.Text;
                    DataRow dr3 = timeParams.NewRow();
                    dr3[0] = label195.Text;
                    DataRow dr4 = timeParams.NewRow();
                    dr4[0] = label196.Text;
                    DataRow dr5 = timeParams.NewRow();
                    dr5[0] = label197.Text;
                    DataRow dr6 = timeParams.NewRow();
                    dr6[0] = label198.Text;
                    DataRow dr7 = timeParams.NewRow();
                    dr7[0] = label199.Text;
                    DataRow dr8 = timeParams.NewRow();
                    dr8[0] = label200.Text;
                    DataRow dr9 = timeParams.NewRow();
                    dr9[0] = label201.Text;
                    DataRow dr10 = timeParams.NewRow();
                    dr10[0] = label202.Text;
                    //DataRow dr11 = timeParams.NewRow();
                    //   dr11[0] = label39.Text;
                    timeParams.Rows.Add(dr1);
                    timeParams.Rows.Add(dr2);
                    timeParams.Rows.Add(dr3);
                    timeParams.Rows.Add(dr4);
                    timeParams.Rows.Add(dr5);
                    timeParams.Rows.Add(dr6);
                    timeParams.Rows.Add(dr7);
                    timeParams.Rows.Add(dr8);
                    timeParams.Rows.Add(dr9);
                    timeParams.Rows.Add(dr10);
                    // timeParams.Rows.Add(dr11);

                    Dictionary<string, Object> p = new Dictionary<string, object>();
                    p.Add("date", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
                    p.Add("line", txtLine.Text);
                    p.Add("clinetTimeParams", timeParams);
                    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Production_KanbanServer", "TabPage6_Query", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();

                        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                        this.Invoke(new Action(() =>
                        {
                            int totalFinishQty = 0;
                            int totalOperatorHours = 0;
                            string target = "0";
                            double hourQty = 0;
                            for (int i = 0; i < dtJson.Rows.Count; i++)
                            {
                                target = dtJson.Rows[i][2].ToString();
                                hourQty = double.Parse(dtJson.Rows[i][9].ToString()) == 0 ? 0 : double.Parse(dtJson.Rows[i][2].ToString()) / double.Parse(dtJson.Rows[i][9].ToString());
                                totalFinishQty += int.Parse(dtJson.Rows[i][1].ToString());

                                if (i == 0)
                                {
                                    double time1 = Convert.ToDateTime(label193.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                                    double time2 = Convert.ToDateTime(label193.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                                    label244.Text = dtJson.Rows[0][0].ToString();
                                    label245.Text = (hourQty * (time2 - time1) / 3600).ToString();
                                    label246.Text = dtJson.Rows[0][1].ToString();
                                    label247.Text = (decimal.Parse(label246.Text.ToString()) - decimal.Parse(label245.Text.ToString())).ToString();
                                    if (decimal.Parse(label246.Text.ToString()) - decimal.Parse(label245.Text.ToString()) < 0)
                                    {
                                        label247.BackColor = Color.FromArgb(255, 0, 0);
                                    }
                                }
                                else if (i == 1)
                                {
                                    double time1 = Convert.ToDateTime(label194.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                                    double time2 = Convert.ToDateTime(label194.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                                    label243.Text = dtJson.Rows[1][0].ToString();
                                    label252.Text = (hourQty * (time2 - time1) / 3600).ToString();
                                    label253.Text = dtJson.Rows[1][1].ToString();
                                    label254.Text = (decimal.Parse(label253.Text.ToString()) - decimal.Parse(label252.Text.ToString())).ToString();
                                    if (decimal.Parse(label253.Text.ToString()) - decimal.Parse(label252.Text.ToString()) < 0)
                                    {
                                        label254.BackColor = Color.FromArgb(255, 0, 0);
                                    }
                                }
                                else if (i == 2)
                                {
                                    double time1 = Convert.ToDateTime(label195.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                                    double time2 = Convert.ToDateTime(label195.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                                    label242.Text = dtJson.Rows[2][0].ToString();
                                    label259.Text = (hourQty * (time2 - time1) / 3600).ToString();
                                    label260.Text = dtJson.Rows[2][1].ToString();
                                    label261.Text = (decimal.Parse(label260.Text.ToString()) - decimal.Parse(label259.Text.ToString())).ToString();
                                    if (decimal.Parse(label260.Text.ToString()) - decimal.Parse(label259.Text.ToString()) < 0)
                                    {
                                        label261.BackColor = Color.FromArgb(255, 0, 0);
                                    }
                                }
                                else if (i == 3)
                                {
                                    double time1 = Convert.ToDateTime(label196.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                                    double time2 = Convert.ToDateTime(label196.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                                    label241.Text = dtJson.Rows[3][0].ToString();
                                    label266.Text = (hourQty * (time2 - time1) / 3600).ToString();
                                    label267.Text = dtJson.Rows[3][1].ToString();
                                    label268.Text = (decimal.Parse(label267.Text.ToString()) - decimal.Parse(label266.Text.ToString())).ToString();
                                    if (decimal.Parse(label267.Text.ToString()) - decimal.Parse(label266.Text.ToString()) < 0)
                                    {
                                        label268.BackColor = Color.FromArgb(255, 0, 0);
                                    }
                                }
                                else if (i == 4)
                                {
                                    double time1 = Convert.ToDateTime(label197.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                                    double time2 = Convert.ToDateTime(label197.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                                    double time3 = dtJson.Rows[i][6] == null || string.IsNullOrEmpty(dtJson.Rows[i][6].ToString()) ? 0 : Convert.ToDateTime(dtJson.Rows[i][6].ToString()).TimeOfDay.TotalSeconds;
                                    double time4 = dtJson.Rows[i][7] == null || string.IsNullOrEmpty(dtJson.Rows[i][7].ToString()) ? 0 : Convert.ToDateTime(dtJson.Rows[i][7].ToString()).TimeOfDay.TotalSeconds;
                                    label240.Text = dtJson.Rows[4][0].ToString();
                                    label273.Text = (hourQty * (time2 - time1 - (time4 - time3)) / 3600).ToString();
                                    label274.Text = dtJson.Rows[4][1].ToString();
                                    label275.Text = (decimal.Parse(label274.Text.ToString()) - decimal.Parse(label273.Text.ToString())).ToString();
                                    if (decimal.Parse(label274.Text.ToString()) - decimal.Parse(label273.Text.ToString()) < 0)
                                    {
                                        label275.BackColor = Color.FromArgb(255, 0, 0);
                                    }
                                    else
                                    {
                                        label275.BackColor = Color.FromArgb(255, 255, 255);
                                    }
                                }
                                else if (i == 5)
                                {
                                    double time1 = Convert.ToDateTime(label198.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                                    double time2 = Convert.ToDateTime(label198.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                                    label239.Text = dtJson.Rows[5][0].ToString();
                                    label280.Text = (hourQty * (time2 - time1) / 3600).ToString();
                                    label281.Text = dtJson.Rows[5][1].ToString();
                                    label282.Text = (decimal.Parse(label281.Text.ToString()) - decimal.Parse(label280.Text.ToString())).ToString();
                                    if (decimal.Parse(label281.Text.ToString()) - decimal.Parse(label280.Text.ToString()) < 0)
                                    {
                                        label282.BackColor = Color.FromArgb(255, 0, 0);
                                    }

                                }
                                else if (i == 6)
                                {
                                    double time1 = Convert.ToDateTime(label199.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                                    double time2 = Convert.ToDateTime(label199.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                                    label238.Text = dtJson.Rows[6][0].ToString();
                                    label287.Text = (hourQty * (time2 - time1) / 3600).ToString();
                                    label288.Text = dtJson.Rows[6][1].ToString();
                                    label289.Text = (decimal.Parse(label288.Text.ToString()) - decimal.Parse(label287.Text.ToString())).ToString();
                                    if (decimal.Parse(label288.Text.ToString()) - decimal.Parse(label287.Text.ToString()) < 0)
                                    {
                                        label289.BackColor = Color.FromArgb(255, 0, 0);
                                    }
                                }
                                else if (i == 7)
                                {
                                    double time1 = Convert.ToDateTime(label200.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                                    double time2 = Convert.ToDateTime(label200.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                                    label237.Text = dtJson.Rows[7][0].ToString();
                                    label294.Text = (hourQty * (time2 - time1) / 3600).ToString();
                                    label295.Text = dtJson.Rows[7][1].ToString();
                                    label296.Text = (decimal.Parse(label295.Text.ToString()) - decimal.Parse(label294.Text.ToString())).ToString();
                                    if (decimal.Parse(label295.Text.ToString()) - decimal.Parse(label294.Text.ToString()) < 0)
                                    {
                                        label296.BackColor = Color.FromArgb(255, 0, 0);
                                    }
                                }
                                else if (i == 8)
                                {
                                    double time1 = Convert.ToDateTime(label201.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                                    double time2 = Convert.ToDateTime(label201.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                                    label236.Text = dtJson.Rows[8][0].ToString();
                                    label301.Text = (hourQty * (time2 - time1) / 3600).ToString();
                                    label302.Text = dtJson.Rows[8][1].ToString();
                                    label303.Text = (decimal.Parse(label302.Text.ToString()) - decimal.Parse(label301.Text.ToString())).ToString();
                                    if (decimal.Parse(label302.Text.ToString()) - decimal.Parse(label301.Text.ToString()) < 0)
                                    {
                                        label303.BackColor = Color.FromArgb(255, 0, 0);
                                    }
                                }
                                else if (i == 9)
                                {
                                    double time1 = Convert.ToDateTime(label202.Text.Split('~')[0].ToString()).TimeOfDay.TotalSeconds;
                                    double time2 = Convert.ToDateTime(label202.Text.Split('~')[1].ToString()).TimeOfDay.TotalSeconds;
                                    label235.Text = dtJson.Rows[9][0].ToString();
                                    label308.Text = (hourQty * (time2 - time1) / 3600).ToString();
                                    label309.Text = dtJson.Rows[9][1].ToString();
                                    label310.Text = (decimal.Parse(label309.Text.ToString()) - decimal.Parse(label308.Text.ToString())).ToString();
                                    if (decimal.Parse(label309.Text.ToString()) - decimal.Parse(label308.Text.ToString()) < 0)
                                    {
                                        label310.BackColor = Color.FromArgb(255, 0, 0);
                                    }
                                }
                            }

                            label316.Text = totalFinishQty.ToString();
                            label315.Text = target;
                            label317.Text = (totalFinishQty - int.Parse(target)).ToString();
                            if (totalFinishQty - int.Parse(target) < 0)
                            {
                                label317.BackColor = Color.FromArgb(255, 0, 0);
                            }
                            else
                            {
                                label317.BackColor = Color.FromArgb(58, 152, 54);
                            }
                            //label32.Text = "√";
                            //label33.Text = "√";
                            //label34.Text = "√";
                            //label35.Text = "√";
                            //label36.Text = "√";
                            //label37.Text = "√";
                            //label38.Text = "√";
                            //label39.Text = "√";
                            //label51.Text = "";
                            //label122.Text = "√";
                        }));
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void tabPage7_query()
        {
            //try
            //{
            //    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Production_KanbanServer", "TabPage7_Query", Program.client.UserToken, string.Empty);
            //    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            //    {
            //        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
            //        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            //        this.Invoke(new Action(() =>
            //        {

            //        }));
            //    }
            //    else
            //    {
            //        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        /// <summary>
        /// 日小时产量表
        /// </summary>
        public void tabPage8_query()
        {
            try
            {
                if (!string.IsNullOrEmpty(dateTimePicker1.Value.ToString()) && !string.IsNullOrEmpty(txtLine.Text))
                {
                    Dictionary<string, Object> p = new Dictionary<string, object>();
                    p.Add("date", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
                    p.Add("line", txtLine.Text);
                    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI",
                        "KZ_MESAPI.Controllers.Production_KanbanServer", "TabPage8_Query", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                        this.Invoke(new Action(() =>
                        {
                            label226.Text = Convert.ToInt32(dtJson.Rows[0]["PresentQty"].ToString()).ToString();
                            label208.Text = dtJson.Rows[0]["SumQty"].ToString();
                            label207.Text = Convert.ToInt32(dtJson.Rows[0]["HourTargetQty"].ToString()).ToString();
                            label206.Text = dtJson.Rows[0]["HourQty"].ToString();
                            label204.Text = Convert.ToDouble(dtJson.Rows[0]["Ration"].ToString()).ToString("0.0");
                            label228.Text = dtJson.Rows[0]["Title"].ToString();
                            label230.Text = Convert.ToInt32(dtJson.Rows[0]["Target"].ToString()).ToString();
                            label205.Text = Convert.ToDouble(dtJson.Rows[0]["BA"].ToString()).ToString("0.0");
                        }));
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void tabPage9_query()
        {
            //try
            //{
            //    if (!string.IsNullOrEmpty(dateTimePicker1.Value.ToString()) && !string.IsNullOrEmpty(txtLine.Text))
            //    {
            //        Dictionary<string, Object> p = new Dictionary<string, object>();
            //        p.Add("date", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
            //        p.Add("line", txtLine.Text);
            //        string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Production_KanbanServer", "TabPage8_Query", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            //        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            //        {
            //            string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
            //            DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            //            this.Invoke(new Action(() =>
            //            {
            //                label226.Text = Convert.ToInt32(dtJson.Rows[0]["PresentQty"].ToString()).ToString();
            //                label208.Text = dtJson.Rows[0]["SumQty"].ToString();
            //                label207.Text = dtJson.Rows[0]["HourTargetQty"].ToString();
            //                label206.Text = dtJson.Rows[0]["HourQty"].ToString();
            //                label204.Text = Convert.ToDouble(dtJson.Rows[0]["Ration"].ToString()).ToString("0.0");
            //                label228.Text = dtJson.Rows[0]["Title"].ToString();
            //                label230.Text = Convert.ToInt32(dtJson.Rows[0]["Target"].ToString()).ToString();
            //                label205.Text = Convert.ToDouble(dtJson.Rows[0]["BA"].ToString()).ToString("0.0");
            //            }));
            //        }
            //        else
            //        {
            //            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        public void Set_Page1Text(DataTable dtJson)
        {
            label3.Text = dtJson.Rows[0]["ModelName"].ToString();
            label53.Text = dtJson.Rows[0]["Date"].ToString();
            label54.Text = dtJson.Rows[0]["Target"].ToString();
            label55.Text = dtJson.Rows[0]["TaktTime"].ToString();
            label56.Text = dtJson.Rows[0]["WaterSpiderNo"].ToString();
            //label58.Text = dtJson.Rows[0]["ModelTHT"].ToString();
            label52.Text = dtJson.Rows[0]["OperatorNo"].ToString();
        }

        public void Set_Page8Text(DataTable dtJson)
        {
            label226.Text = Convert.ToInt32(dtJson.Rows[0]["PresentQty"].ToString()).ToString();
            label208.Text = dtJson.Rows[0]["SumQty"].ToString();
            label207.Text = dtJson.Rows[0]["HourTargetQty"].ToString();
            label206.Text = dtJson.Rows[0]["HourQty"].ToString();
            label204.Text = Convert.ToDouble(dtJson.Rows[0]["Ration"].ToString()).ToString("0.0");
            label228.Text = dtJson.Rows[0]["Title"].ToString();
            label230.Text = Convert.ToInt32(dtJson.Rows[0]["Target"].ToString()).ToString();
            label205.Text = Convert.ToDouble(dtJson.Rows[0]["BA"].ToString()).ToString("0.0");
        }

        delegate void SetTextCallback(Label lable, string text);

        private void SetText(Label label, string text)
        {
            if (label != null)
            {
                if (label.InvokeRequired)
                {
                    SetTextCallback stcb = new SetTextCallback(SetText);
                    label.Invoke(stcb, new object[] { label, text });
                }
                else
                {
                    label.Text = text;
                }
            }
        }

        private void label32_Click(object sender, EventArgs e)
        {
            //TierMettingMaintain frm = new TierMettingMaintain(label49.Text);
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.TopMost = true;
            //frm.ShowDialog();
        }

        private void label33_Click(object sender, EventArgs e)
        {
            //TierMettingMaintain frm = new TierMettingMaintain(label49.Text);
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.TopMost = true;
            //frm.ShowDialog();
        }

        private void label34_Click(object sender, EventArgs e)
        {
            //TierMettingMaintain frm = new TierMettingMaintain(label49.Text);
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.TopMost = true;
            //frm.ShowDialog();
        }

        private void label35_Click(object sender, EventArgs e)
        {
            //TierMettingMaintain frm = new TierMettingMaintain(label49.Text);
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.TopMost = true;
            //frm.ShowDialog();
        }

        private void label36_Click(object sender, EventArgs e)
        {
            //TierMettingMaintain frm = new TierMettingMaintain(label49.Text);
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.TopMost = true;
            //frm.ShowDialog();
        }

        private void label37_Click(object sender, EventArgs e)
        {
            //TierMettingMaintain frm = new TierMettingMaintain(label49.Text);
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.TopMost = true;
            //frm.ShowDialog();
        }

        private void label38_Click(object sender, EventArgs e)
        {
            //TierMettingMaintain frm = new TierMettingMaintain(label49.Text);
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.TopMost = true;
            //frm.ShowDialog();
        }

        private void label39_Click(object sender, EventArgs e)
        {
            //TierMettingMaintain frm = new TierMettingMaintain(label49.Text);
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.TopMost = true;
            //frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Tick -= new EventHandler(timer1_Tick);//添加事件
            try
            {
                timer1.Interval = int.Parse(txtTimer.Text) * 1000;
                MessageBox.Show("设置成功！", "提示：");
            }
            catch
            {
                MessageBox.Show("请输入正确的整数！", "提示：");
            }
            timer1.Tick += new EventHandler(timer1_Tick);//添加事件
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Tick -= new EventHandler(timer1_Tick);//添加事件
        }

        private void Production_KanbanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            timer1.Tick -= new EventHandler(timer1_Tick);//添加事件
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearData();

            tabPage1_query();
        }
        #region APH changed
        private void GetTier1()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetTier1",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                TextToCheckbox(cbxTier1_1, dtJson.Rows[0]["G_1"].ToString());
                TextToCheckbox(cbxTier1_2, dtJson.Rows[0]["G_2"].ToString());
                TextToCheckbox(cbxTier1_3, dtJson.Rows[0]["G_3"].ToString());
                TextToCheckbox(cbxTier1_4, dtJson.Rows[0]["G_4"].ToString());
                TextToCheckbox(cbxTier1_5, dtJson.Rows[0]["G_5"].ToString());
                TextToCheckbox(cbxTier1_6, dtJson.Rows[0]["G_6"].ToString());
                TextToCheckbox(cbxTier1_7, dtJson.Rows[0]["G_7"].ToString());
                TextToCheckbox(cbxTier1_8, dtJson.Rows[0]["G_8"].ToString());
                rtbTier1_1.Text = dtJson.Rows[0]["G_RESULT"].ToString();
                rtbTier1_2.Text = dtJson.Rows[0]["G_AUDITOR"].ToString();
                if (!string.IsNullOrEmpty(dtJson.Rows[0]["G_LAST_UPDATE_DATE"].ToString()))
                {
                    lblTier1Time.Text = dtJson.Rows[0]["G_LAST_UPDATE_DATE"].ToString();
                }
                else
                {
                    lblTier1Time.Text = dtJson.Rows[0]["G_CREATED_DATE"].ToString();
                }
            }
            else
            {
                GetTier1Auditor();
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetTier1Auditor()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetTier1Auditor",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                rtbTier1_2.Text = dtJson.Rows[0]["G_AUDITOR"].ToString();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetTier1Standard()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetTier1Standard",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                TextToCheckbox(cbxStandard1_1, dtJson.Rows[0]["G_SUPERVISOR_1"].ToString());
                TextToCheckbox(cbxStandard1_2, dtJson.Rows[0]["G_SUPERVISOR_2"].ToString());
                TextToCheckbox(cbxStandard1_3, dtJson.Rows[0]["G_SUPERVISOR_3"].ToString());
                TextToCheckbox(cbxStandard1_4, dtJson.Rows[0]["G_SUPERVISOR_4"].ToString());
                TextToCheckbox(cbxStandard1_5, dtJson.Rows[0]["G_SUPERVISOR_5"].ToString());
                TextToCheckbox(cbxStandard1_6, dtJson.Rows[0]["G_SUPERVISOR_6"].ToString());
                TextToCheckbox(cbxStandard1_7, dtJson.Rows[0]["G_SUPERVISOR_7"].ToString());
                TextToCheckbox(cbxStandard1_8, dtJson.Rows[0]["G_SUPERVISOR_8"].ToString());
                rtbStandard1.Text = dtJson.Rows[0]["G_SUPERVISOR_AUDITOR"].ToString();
                TextToCheckbox(cbxStandard2_1, dtJson.Rows[0]["G_VSM_1"].ToString());
                TextToCheckbox(cbxStandard2_2, dtJson.Rows[0]["G_VSM_2"].ToString());
                TextToCheckbox(cbxStandard2_3, dtJson.Rows[0]["G_VSM_3"].ToString());
                TextToCheckbox(cbxStandard2_4, dtJson.Rows[0]["G_VSM_4"].ToString());
                TextToCheckbox(cbxStandard2_5, dtJson.Rows[0]["G_VSM_5"].ToString());
                TextToCheckbox(cbxStandard2_6, dtJson.Rows[0]["G_VSM_6"].ToString());
                TextToCheckbox(cbxStandard2_7, dtJson.Rows[0]["G_VSM_7"].ToString());
                TextToCheckbox(cbxStandard2_8, dtJson.Rows[0]["G_VSM_8"].ToString());
                rtbStandard2.Text = dtJson.Rows[0]["G_VSM_AUDITOR"].ToString();
                TextToCheckbox(cbxStandard3_1, dtJson.Rows[0]["G_THIRD_PARTY_1"].ToString());
                TextToCheckbox(cbxStandard3_2, dtJson.Rows[0]["G_THIRD_PARTY_2"].ToString());
                TextToCheckbox(cbxStandard3_3, dtJson.Rows[0]["G_THIRD_PARTY_3"].ToString());
                TextToCheckbox(cbxStandard3_4, dtJson.Rows[0]["G_THIRD_PARTY_4"].ToString());
                TextToCheckbox(cbxStandard3_5, dtJson.Rows[0]["G_THIRD_PARTY_5"].ToString());
                TextToCheckbox(cbxStandard3_6, dtJson.Rows[0]["G_THIRD_PARTY_6"].ToString());
                TextToCheckbox(cbxStandard3_7, dtJson.Rows[0]["G_THIRD_PARTY_7"].ToString());
                TextToCheckbox(cbxStandard3_8, dtJson.Rows[0]["G_THIRD_PARTY_8"].ToString());
                rtbStandard3.Text = dtJson.Rows[0]["G_THIRD_PARTY_AUDITOR"].ToString();
                if (!string.IsNullOrEmpty(dtJson.Rows[0]["G_LAST_UPDATE_DATE"].ToString()))
                {
                    lblStandardTime.Text = dtJson.Rows[0]["G_LAST_UPDATE_DATE"].ToString();
                }
                else
                {
                    lblStandardTime.Text = dtJson.Rows[0]["G_CREATED_DATE"].ToString();
                }
            }
            else
            {
                GetTier1StandardAuditor();
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetTier1StandardAuditor()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetTier1StandardAuditor",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                rtbStandard1.Text = dtJson.Rows[0]["G_SUPERVISOR_AUDITOR"].ToString();
                rtbStandard2.Text = dtJson.Rows[0]["G_VSM_AUDITOR"].ToString();
                rtbStandard3.Text = dtJson.Rows[0]["G_THIRD_PARTY_AUDITOR"].ToString();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void btnTier1Save_Click(object sender, EventArgs e)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("G_DEPTCODE", txtLine.Text);
            p.Add("G_DATE", dateTimePicker1.Value);
            p.Add("G_1", CheckboxToText(cbxTier1_1));
            p.Add("G_2", CheckboxToText(cbxTier1_2));
            p.Add("G_3", CheckboxToText(cbxTier1_3));
            p.Add("G_4", CheckboxToText(cbxTier1_4));
            p.Add("G_5", CheckboxToText(cbxTier1_5));
            p.Add("G_6", CheckboxToText(cbxTier1_6));
            p.Add("G_7", CheckboxToText(cbxTier1_7));
            p.Add("G_8", CheckboxToText(cbxTier1_8));
            p.Add("G_RESULT", rtbTier1_1.Text);
            p.Add("G_AUDITOR", rtbTier1_2.Text);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "SaveTier1",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {

            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void btnStandardSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("G_DEPTCODE", txtLine.Text);
            p.Add("G_DATE", dateTimePicker1.Value);
            p.Add("G_SUPERVISOR_1", CheckboxToText(cbxStandard1_1));
            p.Add("G_SUPERVISOR_2", CheckboxToText(cbxStandard1_2));
            p.Add("G_SUPERVISOR_3", CheckboxToText(cbxStandard1_3));
            p.Add("G_SUPERVISOR_4", CheckboxToText(cbxStandard1_4));
            p.Add("G_SUPERVISOR_5", CheckboxToText(cbxStandard1_5));
            p.Add("G_SUPERVISOR_6", CheckboxToText(cbxStandard1_6));
            p.Add("G_SUPERVISOR_7", CheckboxToText(cbxStandard1_7));
            p.Add("G_SUPERVISOR_8", CheckboxToText(cbxStandard1_8));
            p.Add("G_SUPERVISOR_AUDITOR", rtbStandard1.Text);
            p.Add("G_VSM_1", CheckboxToText(cbxStandard2_1));
            p.Add("G_VSM_2", CheckboxToText(cbxStandard2_2));
            p.Add("G_VSM_3", CheckboxToText(cbxStandard2_3));
            p.Add("G_VSM_4", CheckboxToText(cbxStandard2_4));
            p.Add("G_VSM_5", CheckboxToText(cbxStandard2_5));
            p.Add("G_VSM_6", CheckboxToText(cbxStandard2_6));
            p.Add("G_VSM_7", CheckboxToText(cbxStandard2_7));
            p.Add("G_VSM_8", CheckboxToText(cbxStandard2_8));
            p.Add("G_VSM_AUDITOR", rtbStandard2.Text);
            p.Add("G_THIRD_PARTY_1", CheckboxToText(cbxStandard3_1));
            p.Add("G_THIRD_PARTY_2", CheckboxToText(cbxStandard3_2));
            p.Add("G_THIRD_PARTY_3", CheckboxToText(cbxStandard3_3));
            p.Add("G_THIRD_PARTY_4", CheckboxToText(cbxStandard3_4));
            p.Add("G_THIRD_PARTY_5", CheckboxToText(cbxStandard3_5));
            p.Add("G_THIRD_PARTY_6", CheckboxToText(cbxStandard3_6));
            p.Add("G_THIRD_PARTY_7", CheckboxToText(cbxStandard3_7));
            p.Add("G_THIRD_PARTY_8", CheckboxToText(cbxStandard3_8));
            p.Add("G_THIRD_PARTY_AUDITOR", rtbStandard3.Text);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "SaveTier1Standard",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {

            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private string CheckboxToText(CheckBox cbxName)
        {
            string str = "";
            str = cbxName.Checked ? "Y" : "N";
            return str;
        }
        private string TextToCheckbox(CheckBox cbxName, string val)
        {
            if (val.Equals("Y"))
            {
                cbxName.Checked = true;
            }
            else
            {
                cbxName.Checked = false;
            }
            return "";
        }

        #endregion

        private void rtbTHT_TextChanged(object sender, EventArgs e)
        {
            string PROCESS = "";
            if (txtLine.Text.Contains("C"))
            {
                PROCESS = "C";
            }
            else if (txtLine.Text.Contains("S"))
            {
                PROCESS = "S";
            }
            else if (txtLine.Text.Contains("L"))
            {
                PROCESS = "L";
            }
            else if (txtLine.Text.Contains("T"))
            {
                PROCESS = "T";
            }
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("ART", ART);
            p.Add("THT", rtbTHT.Text);
            p.Add("PROCESS", PROCESS);
            p.Add("DEPT", txtLine.Text);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting", "TierMeeting.Controllers.TierMeetingServer", "SaveTHT",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {

                //rtbTHT.SelectAll();
                //rtbTHT.SelectionAlignment = HorizontalAlignment.Center;
                //rtbTHT.DeselectAll();
                rtbTHT.Select(rtbTHT.Text.Length, 0);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                rtbTHT.Text = "";
            }
        }

        private void GetTier1_WeekSafety()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vLine", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "Tier1_WeekSafety",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count - 1; i++)
                {
                    string lblDateName = "lblDate" + (i + 1);
                    Label lblDate = this.Controls.Find(lblDateName, true).FirstOrDefault() as Label;
                    DateTime tempDate = Convert.ToDateTime(dtJson.Rows[i]["day"].ToString());
                    SetText(lblDate, tempDate.ToString("yyyy/MM/dd"));
                    if (tempDate <= dateTimePicker1.Value)
                    {
                        string lblSafetyTargetName = "lbl" + (i + 1) + "_1";
                        string lblSafetyActualName = "lbl" + (i + 1) + "_2";
                        Label lblSafetyTarget = this.Controls.Find(lblSafetyTargetName, true).FirstOrDefault() as Label;
                        Label lblSafetyActual = this.Controls.Find(lblSafetyActualName, true).FirstOrDefault() as Label;
                        SetText(lblSafetyTarget, "0");
                        SetText(lblSafetyActual, dtJson.Rows[i]["count"].ToString());
                        if (Double.Parse(dtJson.Rows[i]["count"].ToString()) <= 0)
                        {
                            lblSafetyActual.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        else
                        {
                            lblSafetyActual.BackColor = Color.FromArgb(255, 0, 0);
                        }
                    }
                }
                DisableFuture();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetTier1_WeekPPHTarget()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vLine", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "Tier1_WeekPPHTarget",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (dateTimePicker1.Value >= DateTime.Parse(dtJson.Rows[i]["WORK_day"].ToString()))
                    {
                        string lblPPHTargetName = "lbl" + (i + 1) + "_7";
                        Label lblPPHTarget = this.Controls.Find(lblPPHTargetName, true).FirstOrDefault() as Label;

                        SetText(lblPPHTarget, dtJson.Rows[i]["PPHTarget"].ToString());



                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetTier1_WeekPPH()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vLine", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "Tier1_WeekPPH",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                double pph = 0;
                DataTable dtPPH = new DataTable();
                dtPPH.Columns.Add("PPH");
                dtPPH.Columns.Add("DATE");
                foreach (DataRow r in dtJson.Rows)
                {
                    double tempPPH = 0;
                    DataRow dr = dtPPH.NewRow();
                    if (r["MANPOWER"] != null)
                    {
                        if (!(string.IsNullOrEmpty(r["MANPOWER"].ToString()) || r["MANPOWER"].ToString() == "0"))
                        {
                            tempPPH = r["QTY"].ToDouble() / r["MANPOWER"].ToDouble();
                            tempPPH = Math.Round(tempPPH, 2);
                        }
                        else
                        {
                            tempPPH = 0;
                        }
                    }
                    if (dateTimePicker1.Value >= DateTime.Parse(r["WORK_DATE"].ToString()))
                    {
                        pph = tempPPH;
                        dr["PPH"] = tempPPH;
                        dr["DATE"] = DateTime.Parse(r["WORK_DATE"].ToString()).ToString("MM/dd");
                        dtPPH.Rows.Add(dr);
                    }
                }
                for (int i = 0; i < dtPPH.Rows.Count; i++)
                {
                    string lblPPHTargetName = "lbl" + (i + 1) + "_7";
                    Label lblPPHTarget = this.Controls.Find(lblPPHTargetName, true).FirstOrDefault() as Label;

                    string lblPPHActualName = "lbl" + (i + 1) + "_8";
                    Label lblPPHActual = this.Controls.Find(lblPPHActualName, true).FirstOrDefault() as Label;
                    SetText(lblPPHActual, dtPPH.Rows[i]["PPH"].ToString());



                    try
                    {
                        if (Double.Parse(dtPPH.Rows[i]["PPH"].ToString()) / Double.Parse(lblPPHTarget.Text.ToString()) >= 1)
                        {
                            lblPPHActual.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        else if (Double.Parse(dtPPH.Rows[i]["PPH"].ToString()) / Double.Parse(lblPPHTarget.Text.ToString()) >= 0.95)
                        {
                            lblPPHActual.BackColor = Color.FromArgb(239, 222, 64);
                        }
                        else
                        {
                            lblPPHActual.BackColor = Color.FromArgb(255, 0, 0);
                        }
                    }
                    catch
                    {


                    }

                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetTier1_WeekRFT()
        {
            string RFTTarget = GetDeptType();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDept", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "Tier1_WeekRFT",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (dateTimePicker1.Value >= DateTime.Parse(dtJson.Rows[i]["RIQI"].ToString()))
                    {
                        string lblRFTTargetName = "lbl" + (i + 1) + "_3";
                        string lblRFTActualName = "lbl" + (i + 1) + "_4";
                        Label lblRFTTarget = this.Controls.Find(lblRFTTargetName, true).FirstOrDefault() as Label;
                        Label lblRFTActual = this.Controls.Find(lblRFTActualName, true).FirstOrDefault() as Label;
                        SetText(lblRFTTarget, RFTTarget);
                        SetText(lblRFTActual, dtJson.Rows[i]["RFT"].ToString());
                        if (Double.Parse(dtJson.Rows[i]["RFT"].ToString()) / Double.Parse(RFTTarget.ToString()) >= 1)
                        {
                            lblRFTActual.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        else if (Double.Parse(dtJson.Rows[i]["RFT"].ToString()) / Double.Parse(RFTTarget.ToString()) >= 0.95)
                        {
                            lblRFTActual.BackColor = Color.FromArgb(239, 222, 64);
                        }
                        else
                        {
                            lblRFTActual.BackColor = Color.FromArgb(255, 0, 0);
                        }
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private string GetDeptType()
        {
            string RFT = "";
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", txtLine.Text);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetDeptType",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                switch (dtJson.Rows[0][0])
                {
                    case "S":
                        RFT = "97";
                        break;
                    case "C":
                        RFT = "98";
                        break;
                    case "L":
                        RFT = "90";
                        break;
                    case "T":
                        RFT = "90";
                        break;
                    default:
                        RFT = "90";
                        break;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return RFT;
        }
        private void GetTier1_WeekOutput()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vLine", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "Tier1_WeekOutput",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (dateTimePicker1.Value >= DateTime.Parse(dtJson.Rows[i]["Work_day"].ToString()))
                    {
                        string lblOutputTargetName = "lbl" + (i + 1) + "_5";
                        string lblOutputActualName = "lbl" + (i + 1) + "_6";
                        Label lblOutputTarget = this.Controls.Find(lblOutputTargetName, true).FirstOrDefault() as Label;
                        Label lblOutputActual = this.Controls.Find(lblOutputActualName, true).FirstOrDefault() as Label;
                        SetText(lblOutputTarget, dtJson.Rows[i]["WORK_QTY"].ToString());
                        SetText(lblOutputActual, dtJson.Rows[i]["QTY"].ToString());
                        if (Double.Parse(dtJson.Rows[i]["QTY"].ToString()) / Double.Parse(dtJson.Rows[i]["WORK_QTY"].ToString()) >= 1)
                        {
                            lblOutputActual.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        else if (Double.Parse(dtJson.Rows[i]["QTY"].ToString()) / Double.Parse(dtJson.Rows[i]["WORK_QTY"].ToString()) >= 0.95)
                        {
                            lblOutputActual.BackColor = Color.FromArgb(239, 222, 64);
                        }
                        else
                        {
                            lblOutputActual.BackColor = Color.FromArgb(255, 0, 0);
                        }
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetTier1_Kaizen()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "Tier1_Kaizen",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                SetText(lblKaizen1, dtJson.Rows[0]["MONTH"].ToString());
                SetText(lblKaizen2, dtJson.Rows[0]["YEAR"].ToString());
                SetText(lblKaizen3, dtJson.Rows[0]["PERPEOPLE"].ToString());
                //lbl1_15.Text = "0";
                //lbl2_15.Text = "0";
                //lbl3_15.Text = "0";
                //lbl4_15.Text = "0";
                //lbl5_15.Text = "0";
                //lbl6_15.Text = "0";
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetTier1_WeekLLER()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vLine", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "Tier1_WeekLLER",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (dateTimePicker1.Value >= DateTime.Parse(dtJson.Rows[i]["work_day"].ToString()))
                    {
                        string lblLLERTargetName = "lbl" + (i + 1) + "_9";
                        string lblLLERActualName = "lbl" + (i + 1) + "_10";
                        Label lblLLERTarget = this.Controls.Find(lblLLERTargetName, true).FirstOrDefault() as Label;
                        Label lblLLERActual = this.Controls.Find(lblLLERActualName, true).FirstOrDefault() as Label;
                        SetText(lblLLERTarget, "85");
                        SetText(lblLLERActual, dtJson.Rows[i]["LLER"].ToString());
                        if (string.IsNullOrEmpty(dtJson.Rows[i]["LLER"].ToString()))
                        {

                        }
                        else if (Double.Parse(dtJson.Rows[i]["LLER"].ToString()) / 85 >= 1 && null != lblLLERActual)
                        {
                            lblLLERActual.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        else if (Double.Parse(dtJson.Rows[i]["LLER"].ToString()) / 85 >= 0.95 && null != lblLLERActual)
                        {
                            lblLLERActual.BackColor = Color.FromArgb(239, 222, 64);
                        }

                        else
                        {
                            if (null != lblLLERActual)
                            {
                                lblLLERActual.BackColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetTier1_WeekMulti()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vLine", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "Tier1_WeekMulti",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (dateTimePicker1.Value >= DateTime.Parse(dtJson.Rows[i]["day"].ToString()))
                    {
                        string lblMultiTargetName = "lbl" + (i + 1) + "_11";
                        string lblMultiActualName = "lbl" + (i + 1) + "_12";
                        Label lblMultiTarget = this.Controls.Find(lblMultiTargetName, true).FirstOrDefault() as Label;
                        Label lblMultiActual = this.Controls.Find(lblMultiActualName, true).FirstOrDefault() as Label;
                        SetText(lblMultiTarget, "40");
                        SetText(lblMultiActual, dtJson.Rows[i]["Multi"].ToString());
                        if (Double.Parse(dtJson.Rows[i]["Multi"].ToString()) / 40 >= 1)
                        {
                            lblMultiActual.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        else if (Double.Parse(dtJson.Rows[i]["Multi"].ToString()) / 40 >= 0.95)
                        {
                            lblMultiActual.BackColor = Color.FromArgb(239, 222, 64);
                        }
                        else
                        {
                            lblMultiActual.BackColor = Color.FromArgb(255, 0, 0);
                        }
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void SaveDownTime(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            if (rtb != null)
            {
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("dept", txtLine.Text);
                p.Add("downtime", rtb.Text);
                switch (rtb.Name)
                {
                    case "rtb1_16":
                        p.Add("date", lblDate1.Text);
                        break;
                    case "rtb2_16":
                        p.Add("date", lblDate2.Text);
                        break;
                    case "rtb3_16":
                        p.Add("date", lblDate3.Text);
                        break;
                    case "rtb4_16":
                        p.Add("date", lblDate4.Text);
                        break;
                    case "rtb5_16":
                        p.Add("date", lblDate5.Text);
                        break;
                    case "rtb6_16":
                        p.Add("date", lblDate6.Text);
                        break;
                    default:
                        break;
                }
                string ret =
                    SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.Client.APIURL,
                        "TierMeeting",
                        "TierMeeting.Controllers.TierMeetingServer",
                                            "SaveTier1_Downtime",
                        Program.Client.UserToken,
                        JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {

                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }
        private void SaveActualWIP(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            if (rtb != null)
            {
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("dept", txtLine.Text);
                p.Add("wip", rtb.Text);
                switch (rtb.Name)
                {
                    case "rtb1_14":
                        p.Add("date", lblDate1.Text);
                        break;
                    case "rtb2_14":
                        p.Add("date", lblDate2.Text);
                        break;
                    case "rtb3_14":
                        p.Add("date", lblDate3.Text);
                        break;
                    case "rtb4_14":
                        p.Add("date", lblDate4.Text);
                        break;
                    case "rtb5_14":
                        p.Add("date", lblDate5.Text);
                        break;
                    case "rtb6_14":
                        p.Add("date", lblDate6.Text);
                        break;
                    default:
                        break;
                }
                string ret =
                    SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.Client.APIURL,
                        "TierMeeting",
                        "TierMeeting.Controllers.TierMeetingServer",
                                            "SaveTier1_WIP",
                        Program.Client.UserToken,
                        JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {

                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }

        private void DisableFuture()
        {
            for (int i = 1; i < 7; i++)
            {
                string lblDateName = "lblDate" + i;
                Label lblDate = this.Controls.Find(lblDateName, true).FirstOrDefault() as Label;
                DateTime temp = Convert.ToDateTime(lblDate.Text);
                if (temp > dateTimePicker1.Value)
                {
                    string lblDTTargetName = "lbl" + i + "_15";
                    Label lblDTTarget = this.Controls.Find(lblDTTargetName, true).FirstOrDefault() as Label;
                    lblDTTarget.Text = "";
                    string rtbDTActualName = "rtb" + i + "_16";
                    RichTextBox rtbDTActual = this.Controls.Find(rtbDTActualName, true).FirstOrDefault() as RichTextBox;
                    rtbDTActual.Enabled = false;
                    string rtbCOTTargetName = "rtb" + i + "_17";
                    RichTextBox rtbCOTTarget = this.Controls.Find(rtbCOTTargetName, true).FirstOrDefault() as RichTextBox;
                    rtbCOTTarget.Enabled = false;
                    string rtbCOTActualName = "rtb" + i + "_18";
                    RichTextBox rtbCOTActual = this.Controls.Find(rtbCOTActualName, true).FirstOrDefault() as RichTextBox;
                    rtbCOTActual.Enabled = false;
                    string rtbWIPActualName = "rtb" + i + "_14";
                    RichTextBox rtbWIPActual = this.Controls.Find(rtbWIPActualName, true).FirstOrDefault() as RichTextBox;
                    rtbWIPActual.Enabled = false;
                }
                else
                {
                    string lblDTTargetName = "lbl" + i + "_15";
                    Label lblDTTarget = this.Controls.Find(lblDTTargetName, true).FirstOrDefault() as Label;
                    //  lblDTTarget.Text = "";
                    string rtbDTActualName = "rtb" + i + "_16";
                    RichTextBox rtbDTActual = this.Controls.Find(rtbDTActualName, true).FirstOrDefault() as RichTextBox;
                    rtbDTActual.Enabled = true;
                    string rtbCOTTargetName = "rtb" + i + "_17";
                    RichTextBox rtbCOTTarget = this.Controls.Find(rtbCOTTargetName, true).FirstOrDefault() as RichTextBox;
                    rtbCOTTarget.Enabled = true;
                    string rtbCOTActualName = "rtb" + i + "_18";
                    RichTextBox rtbCOTActual = this.Controls.Find(rtbCOTActualName, true).FirstOrDefault() as RichTextBox;
                    rtbCOTActual.Enabled = true;
                    string rtbWIPActualName = "rtb" + i + "_14";
                    RichTextBox rtbWIPActual = this.Controls.Find(rtbWIPActualName, true).FirstOrDefault() as RichTextBox;
                    rtbWIPActual.Enabled = true;


                }
            }
        }
        private void GetTier1_WeekDT()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vLine", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "Tier1_WeekDowntime",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (dateTimePicker1.Value >= DateTime.Parse(dtJson.Rows[i]["G_date"].ToString()))
                    {
                        string lblDTTargetName = "lbl" + (i + 1) + "_15";
                        Label lblDTTarget = this.Controls.Find(lblDTTargetName, true).FirstOrDefault() as Label;

                        string rtbDTActualName = "rtb" + (i + 1) + "_16";
                        RichTextBox rtbDTActual = this.Controls.Find(rtbDTActualName, true).FirstOrDefault() as RichTextBox;
                        rtbDTActual.Text = dtJson.Rows[i]["G_DOWNTIME"].ToString();
                        double rtbDTActual_value = 0;
                        bool is_empty = string.IsNullOrEmpty(rtbDTActual.Text.ToString());
                        try
                        {
                            if (!is_empty)
                            {
                                rtbDTActual_value = Convert.ToDouble(rtbDTActual.Text.ToString());
                            }
                        }
                        catch (Exception exp)
                        {
                            //  SJeMES_Control_Library.MessageHelper.ShowErr(this, exp.Message);

                        }

                        if (string.IsNullOrEmpty(lblDTTarget.Text.ToString()) || is_empty)
                        {
                            rtbDTActual.BackColor = Color.FromArgb(255, 255, 255);
                        }
                        //else if (Double.Parse(lblDTTarget.Text.ToString()) >= Double.Parse(rtbDTActual.Text.ToString()))
                        else if (Convert.ToDouble(lblDTTarget.Text.ToString()) >= rtbDTActual_value)
                        {
                            rtbDTActual.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        else
                        {
                            rtbDTActual.BackColor = Color.FromArgb(255, 0, 0);
                        }
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetTier1_WeekActualWIP()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vLine", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "Tier1_WeekWIP",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (dateTimePicker1.Value >= DateTime.Parse(dtJson.Rows[i]["G_date"].ToString()))
                    {
                        //string rtbDTTargetName = "rtb" + (i + 1) + "_13";
                        //RichTextBox rtbDTTarget = this.Controls.Find(rtbDTTargetName, true).FirstOrDefault() as RichTextBox;
                        string rtbDTActualName = "rtb" + (i + 1) + "_14";
                        RichTextBox rtbDTActual = this.Controls.Find(rtbDTActualName, true).FirstOrDefault() as RichTextBox;
                        rtbDTActual.Text = dtJson.Rows[i]["G_WIP"].ToString();
                        //if  (  rtbDTActual.Text == "" || Double.Parse(dtJson.Rows[i]["G_WIP"].ToString()) / Double.Parse(rtbDTTarget.Text.ToString()) >= 0.95 )
                        //{
                        //    rtbDTActual.BackColor = Color.FromArgb(239, 222, 64);
                        //}
                        //else if (Double.Parse(dtJson.Rows[i]["G_WIP"].ToString()) / Double.Parse(rtbDTTarget.Text.ToString()) >= 1)
                        //{
                        //     rtbDTActual.BackColor = Color.FromArgb(58, 152, 54);
                        //}
                        //else
                        //{
                        //    rtbDTActual.BackColor = Color.FromArgb(255, 0, 0);
                        //}
                    }
                }
            }
            else
            {
                //MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void SaveCOTTarget(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            if (rtb != null)
            {
                if (rtb.Text == "")
                    return;
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("dept", txtLine.Text);
                p.Add("target", rtb.Text);
                p.Add("actual", "");
                switch (rtb.Name)
                {
                    case "rtb1_17":
                        p.Add("date", lblDate1.Text);
                        break;
                    case "rtb2_17":
                        p.Add("date", lblDate2.Text);
                        break;
                    case "rtb3_17":
                        p.Add("date", lblDate3.Text);
                        break;
                    case "rtb4_17":
                        p.Add("date", lblDate4.Text);
                        break;
                    case "rtb5_17":
                        p.Add("date", lblDate5.Text);
                        break;
                    case "rtb6_17":
                        p.Add("date", lblDate6.Text);
                        break;
                    default:
                        break;
                }
                string ret =
                    SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.Client.APIURL,
                        "TierMeeting",
                        "TierMeeting.Controllers.TierMeetingServer",
                                            "SaveTier1_COT",
                        Program.Client.UserToken,
                        JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {

                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }
        private void SaveCOTActual(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            if (rtb != null)
            {
                if (rtb.Text == "")
                    return;
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("dept", txtLine.Text);
                p.Add("target", "");
                p.Add("actual", rtb.Text);
                switch (rtb.Name)
                {
                    case "rtb1_18":
                        p.Add("date", lblDate1.Text);
                        break;
                    case "rtb2_18":
                        p.Add("date", lblDate2.Text);
                        break;
                    case "rtb3_18":
                        p.Add("date", lblDate3.Text);
                        break;
                    case "rtb4_18":
                        p.Add("date", lblDate4.Text);
                        break;
                    case "rtb5_18":
                        p.Add("date", lblDate5.Text);
                        break;
                    case "rtb6_18":
                        p.Add("date", lblDate6.Text);
                        break;
                    default:
                        break;
                }
                string ret =
                    SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.Client.APIURL,
                        "TierMeeting",
                        "TierMeeting.Controllers.TierMeetingServer",
                                            "SaveTier1_COT",
                        Program.Client.UserToken,
                        JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {

                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }
        private void GetTier1_WeekCOT()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vLine", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "Tier1_WeekCOT",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (dateTimePicker1.Value >= DateTime.Parse(dtJson.Rows[i]["G_date"].ToString()))
                    {
                        string rtbCOTTargetName = "rtb" + (i + 1) + "_17";
                        string rtbCOTActualName = "rtb" + (i + 1) + "_18";
                        RichTextBox rtbCOTTarget = this.Controls.Find(rtbCOTTargetName, true).FirstOrDefault() as RichTextBox;
                        RichTextBox rtbCOTActual = this.Controls.Find(rtbCOTActualName, true).FirstOrDefault() as RichTextBox;
                        rtbCOTTarget.Text = dtJson.Rows[i]["G_TARGET_COT"].ToString();
                        rtbCOTActual.Text = dtJson.Rows[i]["G_ACTUAL_COT"].ToString();

                        if (string.IsNullOrEmpty(rtbCOTTarget.Text.ToString()) || string.IsNullOrEmpty(rtbCOTActual.Text.ToString()))
                        {
                            rtbCOTActual.BackColor = Color.FromArgb(255, 255, 255);
                        }
                        else if (Double.Parse(rtbCOTTarget.Text.ToString()) >= Double.Parse(rtbCOTActual.Text.ToString()))
                        {
                            rtbCOTActual.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        else
                        {
                            rtbCOTActual.BackColor = Color.FromArgb(255, 0, 0);
                        }
                    }
                }
                //  SetTextChanged();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            // SetTextChanged();
        }
        private void SetTextChanged()
        {
            rtb1_14.TextChanged += new EventHandler(SaveActualWIP);
            rtb2_14.TextChanged += new EventHandler(SaveActualWIP);
            rtb3_14.TextChanged += new EventHandler(SaveActualWIP);
            rtb4_14.TextChanged += new EventHandler(SaveActualWIP);
            rtb5_14.TextChanged += new EventHandler(SaveActualWIP);
            rtb6_14.TextChanged += new EventHandler(SaveActualWIP);
            rtb1_16.TextChanged += new EventHandler(SaveDownTime);
            rtb2_16.TextChanged += new EventHandler(SaveDownTime);
            rtb3_16.TextChanged += new EventHandler(SaveDownTime);
            rtb4_16.TextChanged += new EventHandler(SaveDownTime);
            rtb5_16.TextChanged += new EventHandler(SaveDownTime);
            rtb6_16.TextChanged += new EventHandler(SaveDownTime);
            rtb1_17.TextChanged += new EventHandler(SaveCOTTarget);
            rtb2_17.TextChanged += new EventHandler(SaveCOTTarget);
            rtb3_17.TextChanged += new EventHandler(SaveCOTTarget);
            rtb4_17.TextChanged += new EventHandler(SaveCOTTarget);
            rtb5_17.TextChanged += new EventHandler(SaveCOTTarget);
            rtb6_17.TextChanged += new EventHandler(SaveCOTTarget);
            rtb1_18.TextChanged += new EventHandler(SaveCOTActual);
            rtb2_18.TextChanged += new EventHandler(SaveCOTActual);
            rtb3_18.TextChanged += new EventHandler(SaveCOTActual);
            rtb4_18.TextChanged += new EventHandler(SaveCOTActual);
            rtb5_18.TextChanged += new EventHandler(SaveCOTActual);
            rtb6_18.TextChanged += new EventHandler(SaveCOTActual);
        }
        private void ClearTextChanged()
        {
            rtb1_14.TextChanged -= new EventHandler(SaveActualWIP);
            rtb2_14.TextChanged -= new EventHandler(SaveActualWIP);
            rtb3_14.TextChanged -= new EventHandler(SaveActualWIP);
            rtb4_14.TextChanged -= new EventHandler(SaveActualWIP);
            rtb5_14.TextChanged -= new EventHandler(SaveActualWIP);
            rtb6_14.TextChanged -= new EventHandler(SaveActualWIP);
            rtb1_16.TextChanged -= new EventHandler(SaveDownTime);
            rtb2_16.TextChanged -= new EventHandler(SaveDownTime);
            rtb3_16.TextChanged -= new EventHandler(SaveDownTime);
            rtb4_16.TextChanged -= new EventHandler(SaveDownTime);
            rtb5_16.TextChanged -= new EventHandler(SaveDownTime);
            rtb6_16.TextChanged -= new EventHandler(SaveDownTime);
            rtb1_17.TextChanged -= new EventHandler(SaveCOTTarget);
            rtb2_17.TextChanged -= new EventHandler(SaveCOTTarget);
            rtb3_17.TextChanged -= new EventHandler(SaveCOTTarget);
            rtb4_17.TextChanged -= new EventHandler(SaveCOTTarget);
            rtb5_17.TextChanged -= new EventHandler(SaveCOTTarget);
            rtb6_17.TextChanged -= new EventHandler(SaveCOTTarget);
            rtb1_18.TextChanged -= new EventHandler(SaveCOTActual);
            rtb2_18.TextChanged -= new EventHandler(SaveCOTActual);
            rtb3_18.TextChanged -= new EventHandler(SaveCOTActual);
            rtb4_18.TextChanged -= new EventHandler(SaveCOTActual);
            rtb5_18.TextChanged -= new EventHandler(SaveCOTActual);
            rtb6_18.TextChanged -= new EventHandler(SaveCOTActual);
        }
        private void GetTier1_WeekWIP()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vLine", txtLine.Text);
            p.Add("date", dateTimePicker1.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "Tier1_WeekHourlyOutput",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (dateTimePicker1.Value >= DateTime.Parse(dtJson.Rows[i]["WORK_day"].ToString()))
                    {
                        string lblWIPTargetName = "lbl" + (i + 1) + "_13";
                        string lblWIPActualName = "rtb" + (i + 1) + "_14";
                        Label lblWIPTarget = this.Controls.Find(lblWIPTargetName, true).FirstOrDefault() as Label;
                        RichTextBox lblWIPActual = this.Controls.Find(lblWIPActualName, true).FirstOrDefault() as RichTextBox;
                        int target = 0;
                        //int actual = 0;
                        if (dtJson.Rows[i]["work_hours"].ToInt() > 0)
                        {
                            target = dtJson.Rows[i]["work_qty"].ToInt() / dtJson.Rows[i]["work_hours"].ToInt();
                            target = target > 1314 ? 240 : target * 4;
                            //actual = dtJson.Rows[i]["qty"].ToInt() / dtJson.Rows[i]["work_hours"].ToInt();
                        }
                        SetText(lblWIPTarget, target.ToString());
                        //SetText(lblWIPActual, actual.ToString());
                        GetTier1_WeekActualWIP();
                        double lblWIPActual_value = 0;
                        bool is_empty = string.IsNullOrEmpty(lblWIPActual.Text.ToString());
                        try
                        {
                            if (!is_empty)
                            {
                                lblWIPActual_value = Convert.ToDouble(lblWIPActual.Text.ToString());
                            }
                        }
                        catch (Exception exp)
                        {
                            // SJeMES_Control_Library.MessageHelper.ShowErr(this, exp.Message);

                        }
                        if (string.IsNullOrEmpty(lblWIPTarget.Text.ToString()) || is_empty)
                        {
                            lblWIPActual.BackColor = Color.FromArgb(255, 255, 255);
                        }
                        //else if (Double.Parse(lblWIPActual.Text.ToString()) <=Convert.ToDouble(lblWIPTarget.Text.ToString()))
                        else if (lblWIPActual_value <= Convert.ToDouble(lblWIPTarget.Text.ToString()))
                        {
                            lblWIPActual.BackColor = Color.FromArgb(58, 152, 54);
                        }
                        else
                        {
                            lblWIPActual.BackColor = Color.FromArgb(255, 0, 0);
                        }
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void ClearData()
        {
            //page_loaded.Clear();

            SetColor(tabPage4, typeof(CusLabel), Color.Transparent);
            SetColor(tabPage6, typeof(CusLabel), Color.Transparent);
            ClearTextChanged();
            //   rtb1_16.TextChanged.
            Console.WriteLine();
            //EventHandler.RemoveAll();
            //clear Richtextbox
            for (int i = 16; i <= 18; i++)
                for (int j = 1; j <= 6; j++)
                {
                    RichTextBox rtb = this.Controls.Find("rtb" + j + "_" + i, true).FirstOrDefault() as RichTextBox;
                    rtb.Text = "";
                }
            for (int j = 1; j <= 6; j++)
            {
                RichTextBox rtb = this.Controls.Find("rtb" + j + "_14", true).FirstOrDefault() as RichTextBox;
                rtb.Text = "";
            }
            //clear label
            for (int j = 1; j <= 6; j++)
                for (int i = 1; i < 15; i++)
                {
                    if (i != 14)
                    {
                        Label lbl = this.Controls.Find("lbl" + j + "_" + i, true).FirstOrDefault() as Label;
                        lbl.Text = "";
                    }
                }
            for (int i = 1; i <= 8; i++)
            {
                //clear checkbox Tier
                CheckBox cbb1 = this.Controls.Find("cbxTier1_" + i, true).FirstOrDefault() as CheckBox;
                cbb1.Checked = false;
                for (int j = 1; j <= 3; j++)
                {
                    //clear cbxStandard
                    CheckBox cbb = this.Controls.Find("cbxStandard" + j + "_" + i, true).FirstOrDefault() as CheckBox;
                    cbb.Checked = false;
                }
            }
            for (int i = 234; i <= 321; i++)
            {
                Label lbl_mangesheet = this.Controls.Find("label" + i, true).FirstOrDefault() as Label;
                lbl_mangesheet.Text = "";
            }
            for (int i = 1; i <= 3; i++)
            {
                RichTextBox rtbStandard = this.Controls.Find("rtbStandard" + i, true).FirstOrDefault() as RichTextBox;
                rtbStandard.Text = "";
                Label lblKaizen = this.Controls.Find("lblKaizen" + i, true).FirstOrDefault() as Label;
                lblKaizen.Text = "";
            }
            rtbTier1_1.Text = "";
            rtbTier1_2.Text = "";

            SetTextChanged();

        }

        private void cbxReason1_TextChanged(object sender, EventArgs e)
        {
            string TimeType = label193.Text;
            string reason = cbxReason1.Text;
            SetDifff(reason, TimeType);
        }

        private void cbxReason2_TextChanged(object sender, EventArgs e)
        {
            string TimeType = label194.Text;
            string reason = cbxReason2.Text;
            SetDifff(reason, TimeType);
        }

        private void cbxReason3_TextChanged(object sender, EventArgs e)
        {
            string TimeType = label195.Text;
            string reason = cbxReason3.Text;
            SetDifff(reason, TimeType);
        }

        private void cbxReason4_TextChanged(object sender, EventArgs e)
        {
            string TimeType = label196.Text;
            string reason = cbxReason4.Text;
            SetDifff(reason, TimeType);
        }

        private void cbxReason5_TextChanged(object sender, EventArgs e)
        {
            string TimeType = label197.Text;
            string reason = cbxReason5.Text;
            SetDifff(reason, TimeType);
        }

        private void cbxReason6_TextChanged(object sender, EventArgs e)
        {
            string TimeType = label198.Text;
            string reason = cbxReason6.Text;
            SetDifff(reason, TimeType);
        }

        private void cbxReason7_TextChanged(object sender, EventArgs e)
        {
            string TimeType = label199.Text;
            string reason = cbxReason7.Text;
            SetDifff(reason, TimeType);
        }

        private void cbxReason8_TextChanged(object sender, EventArgs e)
        {
            string TimeType = label200.Text;
            string reason = cbxReason8.Text;
            SetDifff(reason, TimeType);
        }

        private void cbxReason9_TextChanged(object sender, EventArgs e)
        {
            string TimeType = label201.Text;
            string reason = cbxReason9.Text;
            SetDifff(reason, TimeType);
        }

        private void cbxReason10_TextChanged(object sender, EventArgs e)
        {
            string TimeType = label202.Text;
            string reason = cbxReason10.Text;
            SetDifff(reason, TimeType);
        }

        public void SetDifff(string reason, string TimeType)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("ART", ART);
            p.Add("Reason", reason);
            //p.Add("PROCESS", PROCESS);
            p.Add("DEPT", txtLine.Text);
            p.Add("TimeType", TimeType);
            p.Add("date", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "TierMeeting", "TierMeeting.Controllers.TierMeetingServer", "SaveReason",
                    Program.Client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {

            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        public void GetSeason()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("ART", ART);
            p.Add("DEPT", txtLine.Text);
            p.Add("date", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                "TierMeeting", "TierMeeting.Controllers.TierMeetingServer",
                            "GetSeason", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            cbxReason1.Text = "";
            cbxReason2.Text = "";
            cbxReason3.Text = "";
            cbxReason4.Text = "";
            cbxReason5.Text = "";
            cbxReason6.Text = "";
            cbxReason7.Text = "";
            cbxReason8.Text = "";
            cbxReason9.Text = "";
            cbxReason10.Text = "";
            cb_else_reason.Text = "";


            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                //rtbTHT.Text = dtJson.Rows[0][0].ToString();
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (dtJson.Rows[i]["G_TIME"].ToString() == "07:30~08:30")
                    {
                        cbxReason1.Text = dtJson.Rows[i][0].ToString();
                    }
                    if (dtJson.Rows[i]["G_TIME"].ToString() == "08:30~09:30")
                    {
                        cbxReason2.Text = dtJson.Rows[i][0].ToString();
                    }
                    if (dtJson.Rows[i]["G_TIME"].ToString() == "09:30~10:30")
                    {
                        cbxReason3.Text = dtJson.Rows[i][0].ToString();
                    }
                    if (dtJson.Rows[i]["G_TIME"].ToString() == "10:30~11:30")
                    {
                        cbxReason4.Text = dtJson.Rows[i][0].ToString();
                    }
                    if (dtJson.Rows[i]["G_TIME"].ToString() == "11:30~13:30")
                    {
                        cbxReason5.Text = dtJson.Rows[i][0].ToString();
                    }
                    if (dtJson.Rows[i]["G_TIME"].ToString() == "13:30~14:30")
                    {
                        cbxReason6.Text = dtJson.Rows[i][0].ToString();
                    }
                    if (dtJson.Rows[i]["G_TIME"].ToString() == "14:30~15:30")
                    {
                        cbxReason7.Text = dtJson.Rows[i][0].ToString();
                    }
                    if (dtJson.Rows[i]["G_TIME"].ToString() == "15:30~16:30")
                    {
                        cbxReason8.Text = dtJson.Rows[i][0].ToString();
                    }
                    if (dtJson.Rows[i]["G_TIME"].ToString() == "16:30~17:30")
                    {
                        cbxReason9.Text = dtJson.Rows[i][0].ToString();
                    }
                    if (dtJson.Rows[i]["G_TIME"].ToString() == "17:30~18:30")
                    {
                        cbxReason10.Text = dtJson.Rows[i][0].ToString();
                    }
                    if (dtJson.Rows[i]["G_TIME"].ToString() == lb_else_time.Text)
                    {
                        cb_else_reason.Text = dtJson.Rows[i][0].ToString();
                    }
                }
            }
            else
            {




            }
        }

        private void cb_else_reason_TextChanged(object sender, EventArgs e)
        {
            string TimeType = lb_else_time.Text;
            string reason = cb_else_reason.Text;
            SetDifff(reason, TimeType);
        }

        /// <summary>
        /// 设置父容器下的控件统一颜色
        /// </summary>
        /// <param name="father_contorl"></param>
        /// <param name="type"></param>
        /// <param name="color"></param>

        private void SetColor(Control father_contorl, Type type, Color color)
        {
            try
            {
                foreach (Control control in father_contorl.Controls)
                {

                    if (control.Controls.Count > 0)
                    {
                        SetColor(control, type, color);

                    }
                    else if (control.GetType().Equals(type))
                    {
                        control.BackColor = color;

                    }


                }
            }
            catch (Exception e)
            {
                throw;

            }





        }
    }






}
