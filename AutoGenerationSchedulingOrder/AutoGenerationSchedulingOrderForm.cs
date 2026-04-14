using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace AutoGenerationSchedulingOrder
{
    public partial class AutoGenerationSchedulingOrderForm : MaterialForm
    {
        delegate void SetTextCallBack(string text, Color color);

        Thread thread = null;
        Thread thread1 = null;
        Thread threadInStoc = null;
        Thread threadPack = null;

        private void SetText(string text, Color color)
        {

            if (this.richTextBox1.InvokeRequired)
            {
                SetTextCallBack stcb = new SetTextCallBack(SetText);
                this.Invoke(stcb, new object[] { text, color });
            }
            else
            {
                this.richTextBox1.Focus();
                this.richTextBox1.Select(this.richTextBox1.TextLength, 0);
                this.richTextBox1.SelectionColor = color;
                this.richTextBox1.ScrollToCaret();
                this.richTextBox1.AppendText("\n" + text);
            }
        }

        private void SetTempText(string text, Color color)
        {

            if (this.richTextBox2.InvokeRequired)
            {
                SetTextCallBack stcb = new SetTextCallBack(SetTempText);
                this.Invoke(stcb, new object[] { text, color });
            }
            else
            {
                this.richTextBox2.Focus();
                this.richTextBox2.Select(this.richTextBox2.TextLength, 0);
                this.richTextBox2.SelectionColor = color;
                this.richTextBox2.ScrollToCaret();
                this.richTextBox2.AppendText("\n" + text);
            }
        }

        private void SetInStocText(string text, Color color)
        {

            if (this.richTextBox3.InvokeRequired)
            {
                SetTextCallBack stcb = new SetTextCallBack(SetInStocText);
                this.Invoke(stcb, new object[] { text, color });
            }
            else
            {
                this.richTextBox3.Focus();
                this.richTextBox3.Select(this.richTextBox3.TextLength, 0);
                this.richTextBox3.SelectionColor = color;
                this.richTextBox3.ScrollToCaret();
                this.richTextBox3.AppendText("\n" + text);
            }
        }

        private void SetAutoText(string text, Color color)
        {

            if (this.richTextBox4.InvokeRequired)
            {
                SetTextCallBack stcb = new SetTextCallBack(SetAutoText);
                this.Invoke(stcb, new object[] { text, color });
            }
            else
            {
                this.richTextBox4.Focus();
                this.richTextBox4.Select(this.richTextBox4.TextLength, 0);
                this.richTextBox4.SelectionColor = color;
                this.richTextBox4.ScrollToCaret();
                this.richTextBox4.AppendText("\n" + text);
            }
        }

        public AutoGenerationSchedulingOrderForm()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            this.tabPage3.Parent = null;
        }


        private void btn_start_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = DateTime.Now + "\t in startup thread---->";
            thread = new Thread(new ThreadStart(Go1));
            thread.Start();
        }

        public void Go()
        {
            bool l_vali_sync = false;
            while (true)
            {
                //20-22
                if (DateTime.Now.Hour >= (int.Parse(txtHours.Text.ToString())) && DateTime.Now.Hour <= (int.Parse(textBox1.Text.ToString())) && l_vali_sync == false)
                {
                    l_vali_sync = true;
                    SetText(DateTime.Now + "\t Automatic generation of schedule data starts---->", Color.FromName("Green"));
                    DataTable data = new DataTable();
                    try
                    {
                        Dictionary<string, Object> p = new Dictionary<string, object>();
                        string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "Query", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                            //data = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                            data = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                            throw new Exception("No data scheduling");
                        }

                        SetText(DateTime.Now + "\t Automatically generate schedule data query completion,---->Queried" + data.Rows.Count + "Article data\n", Color.FromName("Green"));
                        int count = data.Rows.Count;
                        int cishu = count / 10 + 1;
                        for (int i = 0; i < cishu; i++)
                        {
                            DataTable data1 = new DataTable();
                            data1 = data.Clone();
                            DataRow[] dr = data.Select(); // Clone dt's structure, including all dt schemas and constraints, and no data
                            int takenum = i * 10 + 10 < count ? 10 : count - i * 10;
                            for (int j = i * 10; j < i * 10 + takenum; j++)
                            {
                                data1.ImportRow((DataRow)dr[j]);
                            }
                            Dictionary<string, Object> d1 = new Dictionary<string, object>();
                            if (data1.Rows.Count > 0)
                            {
                                d1.Add("data", data1);
                                string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "Insert", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d1));
                                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
                                {
                                    SetText(DateTime.Now + "\t End of automatic generation of schedule data,---->insert" + Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString() + "Article data\n", Color.FromName("Green"));
                                }
                                else
                                {
                                    SetText(DateTime.Now + "\t Automatically generate scheduling data---->Insert data exception" + Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString() + "\n", Color.FromName("Red"));
                                    //SendWX(DateTime.Now + "\t 自动生成排程数据---->插入数据发生异常" + Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
                                }
                            }
                        }
                        //SendWX(DateTime.Now + "\t 自动生成排程数据---->自动生成排程数据结束");
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            SetText(DateTime.Now + "\t Automatically generate scheduling data---->Insert data exception" + ex.InnerException.InnerException + "\n", Color.FromName("Red"));
                            //SendWX(DateTime.Now + "\t 自动生成排程数据---->插入数据发生异常" + ex.InnerException.InnerException);
                        }
                        else
                        {
                            if (data.Rows.Count == 0)
                            {
                                SetText(DateTime.Now + "\t No data scheduling" + "\n", Color.FromName("Red"));
                            }
                            else
                            {
                                SetText(DateTime.Now + "\t Automatically generate scheduling data---->Insert data exception" + ex.InnerException + "\n", Color.FromName("Red"));
                                //SendWX(DateTime.Now + "\t 自动生成排程数据---->插入数据发生异常" + ex.InnerException);
                            }
                        }
                    }
                }
                //01-07
                if (DateTime.Now.Hour >= (int.Parse(textBox2.Text)) && DateTime.Now.Hour <= (int.Parse(textBox3.Text)))
                {
                    l_vali_sync = false;
                }
                SetText(DateTime.Now + "\t suspended----> \n", Color.FromName("Green"));

                DateTime dt = DateTime.Now.AddHours(1);
                string time_hour = dt.ToString("yyyy-MM-dd HH");//2008-9-4 20:12:12
                time_hour += ":00:10";//后面毫秒要强转成整数型，会去掉小数部分，导致休眠时长无法将时间定位到下一个整点，这里加10秒。例：10:37休眠后应该要到11:00，但由于休眠的毫秒去掉了小数部分，休眠后只能到10:59。
                DateTime time_next_hours = DateTime.Parse(time_hour);
                TimeSpan ts = time_next_hours - DateTime.Now;
                double Milliseconds = ts.TotalMilliseconds;

                Thread.Sleep((int)Milliseconds);
                //Thread.Sleep(1000 * 60 * 60);   
            }
        }
        public void Go1()
        {
            bool l_vali_sync = false;
            while (true)
            {
                //20-22
                if (DateTime.Now.Hour >= (int.Parse(txtHours.Text.ToString())) && DateTime.Now.Hour <= (int.Parse(textBox1.Text.ToString())) && l_vali_sync == false)
                {
                    if (int.Parse(textBox4.Text.ToString()) - DateTime.Now.Minute < 0)
                    {
                        if (thread != null)
                        {
                            SetText(DateTime.Now + "\t Minute already Passed\n", Color.FromName("Red"));
                            SetText(DateTime.Now + "\t thread close---->\n", Color.FromName("Red"));
                            thread.Abort();
                            return;
                        }
                    }
                    if (DateTime.Now.Minute != (int.Parse(textBox4.Text.ToString())))
                    {
                        int sleeptime = (int.Parse(textBox4.Text.ToString()) - DateTime.Now.Minute) * 1000 * 60;
                        //int sleeptime = int.Parse(textBox4.Text) * 1000 * 60;
                        Thread.Sleep(sleeptime);

                    }

                    l_vali_sync = true;
                    SetText(DateTime.Now + "\t Automatic generation of schedule data starts---->", Color.FromName("Green"));
                    DataTable data = new DataTable();
                    try
                    {
                        Dictionary<string, Object> p = new Dictionary<string, object>();
                        string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "Query", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                            //data = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                            data = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                            throw new Exception("No data scheduling");
                        }

                        SetText(DateTime.Now + "\t Automatically generate schedule data query completion,---->Queried" + data.Rows.Count + "Article data\n", Color.FromName("Green"));
                        int count = data.Rows.Count;
                        int cishu = count / 10 + 1;
                        for (int i = 0; i < cishu; i++)
                        {
                            DataTable data1 = new DataTable();
                            data1 = data.Clone();
                            DataRow[] dr = data.Select(); // Clone dt's structure, including all dt schemas and constraints, and no data
                            int takenum = i * 10 + 10 < count ? 10 : count - i * 10;
                            for (int j = i * 10; j < i * 10 + takenum; j++)
                            {
                                data1.ImportRow((DataRow)dr[j]);
                            }
                            Dictionary<string, Object> d1 = new Dictionary<string, object>();
                            if (data1.Rows.Count > 0)
                            {
                                d1.Add("data", data1);
                                string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "Insert", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d1));
                                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
                                {
                                    SetText(DateTime.Now + "\t End of automatic generation of schedule data,---->insert" + Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString() + "Article data\n", Color.FromName("Green"));
                                }
                                else
                                {
                                    SetText(DateTime.Now + "\t Automatically generate scheduling data---->Insert data exception" + Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString() + "\n", Color.FromName("Red"));
                                    //SendWX(DateTime.Now + "\t 自动生成排程数据---->插入数据发生异常" + Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
                                }
                            }
                        }
                        //SendWX(DateTime.Now + "\t 自动生成排程数据---->自动生成排程数据结束");
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            SetText(DateTime.Now + "\t Automatically generate scheduling data---->Insert data exception" + ex.InnerException.InnerException + "\n", Color.FromName("Red"));
                            //SendWX(DateTime.Now + "\t 自动生成排程数据---->插入数据发生异常" + ex.InnerException.InnerException);
                        }
                        else
                        {
                            if (data.Rows.Count == 0)
                            {
                                SetText(DateTime.Now + "\t No data scheduling" + "\n", Color.FromName("Red"));
                            }
                            else
                            {
                                SetText(DateTime.Now + "\t Automatically generate scheduling data---->Insert data exception" + ex.InnerException + "\n", Color.FromName("Red"));
                                //SendWX(DateTime.Now + "\t 自动生成排程数据---->插入数据发生异常" + ex.InnerException);
                            }
                        }
                    }
                }
                //01-07
                if (DateTime.Now.Hour >= (int.Parse(textBox2.Text)) && DateTime.Now.Hour <= (int.Parse(textBox3.Text)))
                {
                    l_vali_sync = false;
                }
                SetText(DateTime.Now + "\t suspended----> \n", Color.FromName("Green"));

                DateTime dt = DateTime.Now.AddHours(1);
                string time_hour = dt.ToString("yyyy-MM-dd HH");//2008-9-4 20:12:12
                time_hour += ":00:10";//后面毫秒要强转成整数型，会去掉小数部分，导致休眠时长无法将时间定位到下一个整点，这里加10秒。例：10:37休眠后应该要到11:00，但由于休眠的毫秒去掉了小数部分，休眠后只能到10:59。
                DateTime time_next_hours = DateTime.Parse(time_hour);
                TimeSpan ts = time_next_hours - DateTime.Now;
                double Milliseconds = ts.TotalMilliseconds;

                Thread.Sleep((int)Milliseconds);
                //Thread.Sleep(1000 * 60 * 60);   
            }
        }

        private void SendWX(string mailInfo)
        {
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("data", mailInfo);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "SendWX", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                SetText(DateTime.Now + "\t WeChat push successfully" + "\n", Color.FromName("Green"));
            }
            else
            {
                SetText(DateTime.Now + "\t WeChat push failed" + "\n", Color.FromName("Red"));
            }
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the thread？", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (thread != null)
                {
                    SetText(DateTime.Now + "\t thread close---->\n", Color.FromName("Green"));
                    thread.Abort();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = DateTime.Now + "\t Pull over the previous schedule---->";
            SetText(DateTime.Now + "\t Automatic generation of schedule data starts---->\n", Color.FromName("Green"));
            try
            {
                DataTable data = new DataTable();
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "Query_thread1", Program.client.UserToken, string.Empty);
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    data = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
                SetText(DateTime.Now + "\t Automatically generate schedule data query completion,---->Queried" + data.Rows.Count + "Article data\n", Color.FromName("Green"));

                int count = data.Rows.Count;
                int cishu = count / 10 + 1;
                for (int i = 0; i < cishu; i++)
                {

                    DataTable data1 = new DataTable();
                    data1 = data.Clone();
                    DataRow[] dr = data.Select(); // Clone dt's structure, including all dt schemas and constraints, and no data
                    int takenum = i * 10 + 10 < count ? 10 : count - i * 10;
                    for (int j = i * 10; j < i * 10 + takenum; j++)
                    {
                        data1.ImportRow((DataRow)dr[j]);
                    }
                    Dictionary<string, Object> d1 = new Dictionary<string, object>();
                    if (data1.Rows.Count > 0)
                    {
                        d1.Add("data", data1);
                        string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "Insert_thread1", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d1));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
                        {
                            SetText(DateTime.Now + "\t End of automatic generation of schedule data,---->insert" + Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString() + "Article data\n", Color.FromName("Green"));
                        }
                        else
                        {
                            SetText(DateTime.Now + "\t Automatically generate scheduling data---->Insert data exception" + Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString() + "\n", Color.FromName("Red"));
                            //SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
                            //SendWX(DateTime.Now + "\t 自动生成排程数据---->插入数据发生异常" + Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
                        }
                    }
                }
                //SendWX(DateTime.Now + "\t 自动生成排程数据---->自动生成排程数据结束");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    SetText(DateTime.Now + "\t Automatically generate scheduling data---->Insert data exception" + ex.InnerException.InnerException + "\n", Color.FromName("Red"));
                    //SendWX(DateTime.Now + "\t 自动生成排程数据---->插入数据发生异常" + ex.InnerException.InnerException);
                }
                else
                {
                    SetText(DateTime.Now + "\t Automatically generate scheduling data---->Insert data exception" + ex.InnerException + "\n", Color.FromName("Red"));
                    //SendWX(DateTime.Now + "\t 自动生成排程数据---->插入数据发生异常" + ex.InnerException);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
                SetText(DateTime.Now + "\t thread close---->\n", Color.FromName("Green"));
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete tomorrow's rolling schedule data？", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "Delete", Program.client.UserToken, string.Empty);
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
                    {
                        SetText(DateTime.Now + "\t Deletion of daily schedule data completed,------------->", Color.FromName("Green"));
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
                    }


                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        SetText(DateTime.Now + "\t An exception occurred when deleting data" + ex.InnerException.InnerException + "\n", Color.FromName("Red"));
                    }
                    else
                    {
                        SetText(DateTime.Now + "\t An exception occurred when deleting data" + ex.InnerException + "\n", Color.FromName("Red"));
                    }
                }
            }

            SetText(DateTime.Now + "\t Please restart the thread！---->\n", Color.FromName("Green"));
        }

        private void AutoGenerationSchedulingOrderForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHours.Text))
            {
                txtHours.Text = "21";
            }
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "22";
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "01";
            }
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = "07";
            }
            //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.richTextBox2.Text = DateTime.Now + "\t in startup thread---->【This is the input of ERP、Synchronize output data to MES DB。。。。】";
            thread1 = new Thread(new ThreadStart(TempGo));
            thread1.Start();
        }


        public void TempGo()
        {
            while (true)
            {
                if (DateTime.Now.Hour.Equals(int.Parse(txtHours.Text.ToString())))
                {
                    SetTempText(DateTime.Now + "\t Production data synchronization starts---->", Color.FromName("Green"));
                    try
                    {
                        string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "TempDataSynchronization", Program.client.UserToken, string.Empty);
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            SetTempText(DateTime.Now + "\t Production data synchronization ends---->", Color.FromName("Green"));
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                        }
                        //SendWX(DateTime.Now + "\t 生产数据同步开始---->生产数据同步结束");
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            SetTempText(DateTime.Now + "\t Production data synchronization--->Insert data exception" + ex.InnerException.InnerException + "\n", Color.FromName("Red"));
                            //SendWX(DateTime.Now + "\t 生产数据同步---->插入数据发生异常" + ex.InnerException.InnerException);
                        }
                        else
                        {
                            SetTempText(DateTime.Now + "\t Production data synchronization---->Insert data exception" + ex.InnerException + "\n", Color.FromName("Red"));
                            //SendWX(DateTime.Now + "\t 生产数据同步---->插入数据发生异常" + ex.InnerException);
                        }

                    }
                }
                SetTempText(DateTime.Now + "\t suspended----> \n", Color.FromName("Green"));
                Thread.Sleep(1000 * 60 * 60);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the thread？", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (thread1 != null)
                {
                    SetTempText(DateTime.Now + "\t thread close---->\n", Color.FromName("Green"));
                    thread1.Abort();
                }
            }
        }


        private void btn_InStocStart_Click(object sender, EventArgs e)
        {
            this.richTextBox3.Text = DateTime.Now + "\t in startup thread---->";
            threadInStoc = new Thread(new ThreadStart(InStocGo));
            threadInStoc.Start();
        }

        public void InStocGo()
        {
            SetInStocText(DateTime.Now + "\t Auto-execution interface started---->", Color.FromName("Green"));

            while (true)
            {
                try
                {
                    string ProductTrackIn = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.WMS_Finished_ProductTrackIn_ListServer", "InStoc", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
                }
                catch (Exception exp)
                {
                    SetInStocText(DateTime.Now + "\t Interface execution exception：" + exp.Message + "\n", Color.FromName("Red"));
                }

                //SetInStocText(DateTime.Now + "\t 休眠中----> \n", Color.FromName("Green"));
                Thread.Sleep(1000 * 60 * 1);
            }
        }

        private void btn_InStocStop_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the thread？", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (threadInStoc != null)
                {
                    SetInStocText(DateTime.Now + "\t thread close---->\n", Color.FromName("Green"));
                    threadInStoc.Abort();
                }
            }
        }

        private void AutoGenerationSchedulingOrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
            }

            if (thread1 != null)
            {
                thread1.Abort();
            }

            if (threadInStoc != null)
            {
                threadInStoc.Abort();
            }

            if (threadPack != null)
            {
                threadPack.Abort();
            }
        }

        // ------------------------------------------------ Packaging material recoil ---------------------------------------------
        #region

        bool is_start4 = false;

        private void btnStart5_Click(object sender, EventArgs e)
        {
            if (is_start4)
            {
                MessageBox.Show("Unable to re-open the thread", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                return;
            }
            //if ("保存".Equals(btnChangeTime5.Text))
            if ("Save".Equals(btnChangeTime5.Text))
            {
                MessageBox.Show("Please save the execution time period first", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                return;
            }
            this.richTextBox4.Text = DateTime.Now + "\t in startup thread---->";
            threadPack = new Thread(new ThreadStart(PackAuto));
            threadPack.Start();

        }

        public void PackAuto()
        {
            bool pack_sync = false;
            is_start4 = true;
            while (true)
            {
                if (DateTime.Now.Hour >= int.Parse(textBoxPack1.Text.ToString()) && DateTime.Now.Hour <= int.Parse(textBoxPack2.Text.ToString()) && !pack_sync)
                {
                    SetAutoText(DateTime.Now + "\t Automatic execution starts---->", Color.FromName("Green"));
                    try
                    {
                        pack_sync = true;
                        //Generate documents by date and work order summary
                        string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Stoc_TransServer", "InsertAutoPackData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
                        {
                            string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                            int insertCount = int.Parse(json);
                            SetAutoText(DateTime.Now + "\t Aggregate Recoil Source Data---->generate" + insertCount + "Article data", Color.FromName("Green"));
                        }
                        else
                        {
                            string msg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString();
                            SetAutoText(DateTime.Now + "\t Aggregate recoil source data exceptions：" + msg + "", Color.FromName("Red"));
                        }

                        //Process the documents with historical SAP success and WMS unsuccessful
                        string ret2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Stoc_TransServer", "UpdateStockByAutoPackErrorRemain", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
                        {
                            string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                            string[] result = json.Split(',');
                            int _all = int.Parse(result[0]);
                            int _sucess_ = int.Parse(result[1]);
                            SetAutoText(DateTime.Now + "\t legacy asynchronous data processing---->Queried" + _all + "pieces of data, successfully processed" + _sucess_ + "Article data", Color.FromName("Green"));
                        }
                        else
                        {
                            string msg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["ErrMsg"].ToString();
                            SetAutoText(DateTime.Now + "\t Legacy asynchronous data processing exception：" + msg + "", Color.FromName("Red"));
                        }

                        //Synchronous data acquisition
                        string ret3 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Stoc_TransServer", "GetAutoPackData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["IsSuccess"]))
                        {
                            string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["RetData"].ToString();
                            DataTable table = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                            int all_count = table.Rows.Count;
                            int sucess_count = 0;
                            for (int i = 0; i < all_count; i++)
                            {
                                string vProductionOrder = table.Rows[i]["PRODUCTION_ORDER"].ToString();
                                string vWorkDay = table.Rows[i]["WORK_DAY"].ToString();
                                string vPlanSeq = table.Rows[i]["PLAN_SEQ"].ToString();

                                Dictionary<string, Object> p = new Dictionary<string, object>();
                                p.Add("vProductionOrder", vProductionOrder);
                                p.Add("vWorkDay", vWorkDay);
                                p.Add("vPlanSeq", vPlanSeq);
                                p.Add("isThisSplit", "N");
                                string ret4 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Stoc_TransServer", "updateStocByPackByOne", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["IsSuccess"]))
                                {
                                    sucess_count++;
                                }
                            }

                            SetAutoText(DateTime.Now + "\t Synchronous data processing---->Queried" + all_count + "pieces of data, successfully processed" + sucess_count + "Article data", Color.FromName("Green"));
                            SetAutoText(DateTime.Now + "\t This execution ends---->", Color.FromName("Green"));
                            SetAutoText(DateTime.Now + "\t suspended---->\n", Color.FromName("Green"));
                        }
                        else
                        {
                            string msg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["ErrMsg"].ToString();
                            SetAutoText(DateTime.Now + "\t Synchronous data processing exception：" + msg + "\n", Color.FromName("Red"));
                            SetAutoText(DateTime.Now + "\t This execution ends---->", Color.FromName("Red"));
                            SetAutoText(DateTime.Now + "\t suspended---->\n", Color.FromName("Red"));
                        }
                    }
                    catch (Exception ex)
                    {
                        pack_sync = false;
                        SetAutoText(DateTime.Now + "\t Execution exception" + ex.Message + "\n", Color.FromName("Red"));
                        SetAutoText(DateTime.Now + "\t This execution ends---->", Color.FromName("Red"));
                        SetAutoText(DateTime.Now + "\t suspended---->\n", Color.FromName("Red"));
                    }
                }
                if (DateTime.Now.Hour > (int.Parse(textBoxPack2.Text)) || DateTime.Now.Hour < (int.Parse(textBoxPack1.Text)))
                {
                    pack_sync = false;
                }
                DateTime dt = DateTime.Now.AddHours(1);
                string time_hour = dt.ToString("yyyy-MM-dd HH");//2008-9-4 20:12:12
                time_hour += ":00:10";//The following milliseconds must be converted to an integer type, and the decimal part will be removed, so that the sleep duration cannot locate the time to the next whole point. Here, add 10 seconds. Example: After sleeping at 10:37, it should be 11:00, but because the fractional part of the sleeping milliseconds is removed, it can only reach 10:59 after sleeping.
                DateTime time_next_hours = DateTime.Parse(time_hour);
                TimeSpan ts = time_next_hours - DateTime.Now;
                double Milliseconds = ts.TotalMilliseconds;

                Thread.Sleep((int)Milliseconds);
                //Thread.Sleep(1000 * 60 * 1);
            }
        }

        private void btnStop5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the thread？", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (threadPack != null || is_start4)
                {
                    SetAutoText(DateTime.Now + "\t thread close---->\n", Color.FromName("Green"));
                    threadPack.Abort();
                    is_start4 = false;
                }
            }
        }

        private void btnChangeTime5_Click(object sender, EventArgs e)
        {
            if ("Modify time period".Equals(btnChangeTime5.Text))
            {
                if (is_start4)
                {
                    MessageBox.Show("thread starting，cannot be changed", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    return;
                }
                textBoxPack1.ReadOnly = false;
                textBoxPack2.ReadOnly = false;

                btnChangeTime5.Text = "Save";
            }
            else
            {
                try
                {
                    int startHours = int.Parse(textBoxPack1.Text);
                    int endHours = int.Parse(textBoxPack2.Text);

                    if (endHours <= startHours)
                    {
                        MessageBox.Show("End time must be greater than start time", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        return;
                    }
                    if (startHours < 0 || startHours > 23 || endHours < 1 || endHours > 23)
                    {
                        MessageBox.Show("Time must be in the range 1-23", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        return;
                    }
                    textBoxPack1.ReadOnly = true;
                    textBoxPack2.ReadOnly = true;
                    btnChangeTime5.Text = "Modify time period";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
            }
        }

        #endregion
    }
}


