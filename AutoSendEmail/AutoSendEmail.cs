using MaterialSkin.Controls;
using NewExportExcels;
using Newtonsoft.Json;
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

namespace AutoSendEmail
{
    public partial class AutoSendEmail : MaterialForm
    {
        delegate void SetTextCallBack(string text, Color color);

        Thread thread1;
        Thread thread2;
        Thread thread3;
        Thread thread4;
        Thread threadDN;
        Thread threadPackAuto;


        List<string> listEmailReceiver = new List<string>() { "lihong-chang@apachefootwear.com,jie-tao@apachefootwear.com" };
        List<string> listEmailCopy = new List<string>() { "freiheit-kao@apachefootwear.com,haibing-wang@apachefootwear.com,caizhen-gu@apachefootwear.com,zhicheng-xia@apachefootwear.com,tyler-mao@apachefootwear.com,joy-chen@apachefootwear.com,echo-liu@apachefootwear.com,lihua-zhong@apachefootwear.com,pengtao-xu@apachefootwear.com,xiaojun-jiang@apachefootwear.com,libi-yuan@apachefootwear.com,Adam-Ji@apachefootwear.com,yuejiao-qu@apachefootwear.com,sunwy-xie@apachefootwear.com" };
        List<string> listError = new List<string>() { "yuejiao-qu@apachefootwear.com,pengtao-xu@apachefootwear.com" };
        string errmessage = "";

        public AutoSendEmail()
        {
            InitializeComponent();
            WindowState = FormWindowState.Normal;
        }

        #region Default time setting
        private void AutoSendEmail_Load(object sender, EventArgs e)
        {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
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
            ////2
            if (string.IsNullOrWhiteSpace(txtbegin.Text))
            {
                txtbegin.Text = "22";
            }

            if (string.IsNullOrWhiteSpace(txtend.Text))
            {
                txtend.Text = "23";
            }

            if (string.IsNullOrWhiteSpace(xgxc1.Text))
            {
                xgxc1.Text = "01";
            }

            if (string.IsNullOrWhiteSpace(xgxc2.Text))
            {
                xgxc2.Text = "05";
            }
            /////3
            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                textBox7.Text = "09";
            }

            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                textBox6.Text = "10";
            }

            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                textBox5.Text = "01";
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                textBox4.Text = "07";
            }
            ////4
            if (string.IsNullOrWhiteSpace(textBox11.Text))
            {
                textBox11.Text = "09";
            }

            if (string.IsNullOrWhiteSpace(textBox10.Text))
            {
                textBox10.Text = "10";
            }

            if (string.IsNullOrWhiteSpace(textBox9.Text))
            {
                textBox9.Text = "01";
            }

            if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
                textBox8.Text = "07";
            }
            //Packaging material recoil reminder
            if (string.IsNullOrWhiteSpace(textBox11.Text))
            {
                textPackAutoStart.Text = "07";
            }
            if (string.IsNullOrWhiteSpace(textBox11.Text))
            {
                textPackAutoEnd.Text = "08";
            }

            //DN

            if (string.IsNullOrWhiteSpace(txt_first_Hours_DN.Text))
            {
                txt_first_Hours_DN.Text = "09";
            }

            if (string.IsNullOrWhiteSpace(txt_last_Hours_DN.Text))
            {
                txt_last_Hours_DN.Text = "10";
            }

            if (string.IsNullOrWhiteSpace(txt_DN_time_start.Text))
            {
                txt_DN_time_start.Text = "01";
            }

            if (string.IsNullOrWhiteSpace(txt_DN_time_end.Text))
            {
                txt_DN_time_end.Text = "07";
            }



        }
        #endregion
        private void AutoSendEmail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread1 != null)
            {
                thread1.Abort();
            }
            if (thread2 != null)
            {
                thread2.Abort();
            }
            if (thread3 != null)
            {
                thread3.Abort();
            }
            if (thread4 != null)
            {
                thread4.Abort();
            }
            if (threadPackAuto != null)
            {
                threadPackAuto.Abort();
            }
            if (threadDN != null)
            {
                threadDN.Abort();
            }
        }


        #region Daily delivery data report

        private void SetText(string text, Color color)
        {
            if (richTextBox1.InvokeRequired)
            {
                SetTextCallBack stcb = SetText;
                Invoke(stcb, text, color);
            }
            else
            {
                richTextBox1.Focus();
                richTextBox1.Select(richTextBox1.TextLength, 0);
                richTextBox1.SelectionColor = color;
                richTextBox1.ScrollToCaret();
                richTextBox1.AppendText("\n" + text);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = DateTime.Now + "\t thread started---->";
            thread1 = new Thread(Go);
            thread1.Start();
        }
        public void Go()
        {
            bool l_vali_sync = false;


            //List<string> listEmailReceiver = new List<string>() { "jing-tian@apachefootwear.com" };
            //List<string> listEmailCopy = new List<string>() { };
            //List<string> listError = new List<string>() { "jing-tian@apachefootwear.com" };
            //string errmessage = "";
            while (true)
            {
                //20-22
                if (DateTime.Now.Hour >= int.Parse(txtHours.Text) && DateTime.Now.Hour <= int.Parse(textBox1.Text) && l_vali_sync == false)
                {
                    l_vali_sync = true;
                    SetText(DateTime.Now + "\t Automatic generation of schedule data starts---->", Color.FromName("Green"));
                    try
                    {
                        double backgroundStoc;
                        double backgroundStoc1;
                        double backgroundStoc2;
                        double soleLayingStoc;
                        double soleLayingStoc1;
                        double soleLayingStoc2;
                        DataTable data = new DataTable();
                        string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "GetMailDataTest", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
                        if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                            List<double> list = JsonConvert.DeserializeObject<List<double>>(json);
                            //background warehouse
                            backgroundStoc = list[0];//in stock
                            backgroundStoc1 = list[1];//Number of warehousing
                            backgroundStoc2 = list[2];//Number of outbound
                            //Bottom-mounted pipe stocks
                            soleLayingStoc = list[3];//in stock
                            soleLayingStoc1 = list[4];//Number of warehousing
                            soleLayingStoc2 = list[5];//Number of outbound



                            SetText(DateTime.Now + "\t " + string.Format(@"One: Background position" + "\r\n" + "1: There are {0} pairs in book inventory at present" + "\r\n" + "2: The stock-out quantity of the background warehouse today is {2} pairs, and the stock-in quantity is {1} pairs" + "\r\n" + "2: Bottom-mounted production management stocks" + "\r\n " + "1: The current book inventory has {3} pairs" + "\r\n" + "2: The outbound quantity of the material warehouse at the bottom today is {5} pairs, and the inbound quantity is {4} pairs" + " \r\n", backgroundStoc, backgroundStoc1, backgroundStoc2, soleLayingStoc, soleLayingStoc1, soleLayingStoc2), Color.FromName("Green"));
                            //SendMail(string.Format(@"一：本底仓" + "\r\n" + "1:目前账面库存有{0}双" + "\r\n" + "2:今天本底仓的出库数量是{1}双 ，入库数量是{2}双" + "\r\n" + "二：贴底生管股" + "\r\n" + "1:目前账面库存有{3}双" + "\r\n" + "2:今天底部材料仓的出库数量是{4}双 ,入库数量是{5}双" + "\r\n", backgroundStoc, backgroundStoc1, backgroundStoc2, soleLayingStoc, soleLayingStoc1, soleLayingStoc2) + "\n" + "执行时间：" + DateTime.Now);
                            string body = string.Format(@"One: Background position" + "\r\n" + "1: There are {0} pairs in the current book inventory" + "\r\n" + "2: Today's background position The out-stock quantity is {2} pairs, and the in-stock quantity is {1} pairs" + "\r\n" + "2: bottom-mounted production management stocks" + "\r\n" + "1: current book inventory There are {3}" + "\r\n" + "2: the outbound quantity of the bottom material warehouse today is {5}, and the inbound quantity is {4}" + "\r\n", backgroundStoc, backgroundStoc1, backgroundStoc2, soleLayingStoc, soleLayingStoc1, soleLayingStoc2) + "\n" + "Execution time:" + DateTime.Now;
                            MailUtil.SendMessage(listEmailReceiver, listEmailCopy, "Daily Sending Report", body, null, out errmessage);
                            //MailUtil.SendMessage(listError, listError, "每日发料数报告", body, null, out errmessage);
                            SetText(DateTime.Now + "\t Sleeping----> \n", Color.FromName("Green"));
                        }
                        else
                        {
                            SetText(DateTime.Now + "\t " + "Query not connected to API " + JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString(), Color.FromName("Red"));
                            //SendMail("The query is not connected to the API" + "\n" + DateTime.Now);
                            string body = "The query is not connected to the API" + JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString() + "\n" + DateTime.Now;
                            //MailUtil.SendMessage(listEmailReceiver, listEmailCopy, "Daily Sending Report", body, null, out errmessage);
                            MailUtil.SendMessage(listError, listError, "Daily Sending Report", body, null, out errmessage);
                            SetText(DateTime.Now + "\t Sleeping----> \n", Color.FromName("Green"));
                        }
                    }
                    catch (Exception ex)
                    {
                        SetText(DateTime.Now + "\t " + "Query exception " + ex.Message + "\n", Color.FromName("Red"));
                        string body = "Query exception" + "\n" + ex.Message + "\n" + DateTime.Now + "\n";
                        //MailUtil.SendMessage(listEmailReceiver, listEmailCopy, "Daily Sending Report", body, null, out errmessage);
                        MailUtil.SendMessage(listError, listError, "Daily Sending Report", body, null, out errmessage);
                        //SendMail("Query exception occurred" + "\n" + DateTime.Now + ex.InnerException.InnerException + "\n");
                        SetText(DateTime.Now + "\t Sleeping----> \n", Color.FromName("Green"));
                    }
                }

                //01-07
                if (DateTime.Now.Hour >= int.Parse(textBox2.Text) && DateTime.Now.Hour <= int.Parse(textBox3.Text))
                {
                    l_vali_sync = false;
                }

                //Set the sleep interval. The time difference from the current time to the next hour
                DateTime dt = DateTime.Now.AddHours(1);
                string time_hour = dt.ToString("yyyy-MM-dd HH");//2008-9-4 20:12:12
                time_hour += ":00:10";//The next milliseconds must be converted into integers, and the decimal part will be removed, so that the sleep duration cannot locate the time to the next whole point, so add 10 seconds here. Example: After sleeping at 10:37, it should be 11:00, but because the fractional part of the sleeping milliseconds is removed, it can only reach 10:59 after sleeping.
                DateTime time_next_hours = DateTime.Parse(time_hour);
                TimeSpan ts = time_next_hours - DateTime.Now;
                double Milliseconds = ts.TotalMilliseconds;

                Thread.Sleep((int)Milliseconds);
                //Thread.Sleep(1000 * 60 * 60);
            }
        }
        //close the thread
        private void button7_Click(object sender, EventArgs e)
        {
            if (thread1 != null)
            {
                thread1.Abort();
                richTextBox1.Text += "\n" + DateTime.Now + "\t thread closed---->";
            }
        }

        #endregion

        #region Daily production rolling data
        private void SetText2(string text, Color color)
        {
            if (richTextBox2.InvokeRequired)
            {
                SetTextCallBack stcb = SetText2;
                Invoke(stcb, text, color);
            }
            else
            {
                richTextBox2.Focus();
                richTextBox2.Select(richTextBox1.TextLength, 0);
                richTextBox2.SelectionColor = color;
                richTextBox2.ScrollToCaret();
                richTextBox2.AppendText("\n" + text);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = DateTime.Now + "\t Daily production rolling data query thread has been started---->";
            thread2 = new Thread(Go2);
            thread2.Start();
        }


        public void Go2()
        {
            bool l_vali_sync = false;
            while (true)
            {
                //20-22
                if ((DateTime.Now.Hour >= int.Parse(txtbegin.Text) && DateTime.Now.Hour < int.Parse(txtend.Text) && l_vali_sync == false) ||
                    (DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 7 && l_vali_sync == false) ||
                    (DateTime.Now.Hour >= 7 && DateTime.Now.Hour < 8 && l_vali_sync == false))
                {
                    //7:20
                    if (DateTime.Now.Hour == 7)
                    {
                        Thread.Sleep(1200000);//休眠20分钟   
                        //Thread.Sleep(30000);
                    }
                    //l_vali_sync = true;
                    SetText2(DateTime.Now + "\t Automatic generation of schedule data starts---->", Color.FromName("Green"));
                    try
                    {
                        string dataCount = null;
                        string nextDate = null;
                        string nextQty = null;
                        string thisDate = null;
                        string thisQty = null;
                        DataTable data = new DataTable();
                        Dictionary<string, Object> p = new Dictionary<string, object>();
                        string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "GetMailData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                            data = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                            if (data.Rows.Count == 2)
                            {
                                dataCount = data.Rows[1]["dataCount"].ToString();    //The total amount of data on the second day after automatic scheduling
                                nextDate = data.Rows[1]["work_day"].ToString(); //The date of the second day after the automatic schedule
                                nextQty = data.Rows[1]["CY_QTY"].ToString();    //Total unproduced second day after automatic scheduling
                                thisDate = data.Rows[0]["work_day"].ToString(); //today's date
                                thisQty = data.Rows[0]["CY_QTY"].ToString();    //Total unproduced for the day
                                SetText2(DateTime.Now + "\t " + string.Format(@"{0} queried {1} pieces of data, remaining unproduced total {2} pairs; {3} remaining unproduced total {4} pairs .", nextDate, dataCount, nextQty, thisDate, thisQty), Color.FromName("Green"));
                                //SendMail2(string.Format(@"{0} queried {1} pieces of data, the remaining unproduced total {2} pairs; {3} the remaining unproduced total {4} pairs.", nextDate, dataCount, nextQty, thisDate, thisQty) + "\n" + "Execution time: " + DateTime.Now);
                                MailUtil.SendMessage(listEmailReceiver, listEmailCopy, "Daily Sending Report", string.Format(@"{0} queried {1} pieces of data, the remaining unproduced total {2} pairs; {3} the remaining unproduced total {4} pairs.", nextDate, dataCount, nextQty, thisDate, thisQty) + "\n" + "Execution time: " + DateTime.Now, null, out errmessage);
                                SetText2(DateTime.Now + "\t Sleeping----> \n", Color.FromName("Green"));
                            }
                            else if (data.Rows.Count == 1)
                            {
                                SetText2(DateTime.Now + "\t " + string.Format(@"MES server status is normal!!"), Color.FromName("Green"));
                                //SendMail2(string.Format(@"MES server status is normal!!") + "\n" + "Execution time: " + DateTime.Now);
                                MailUtil.SendMessage(listEmailReceiver, listEmailCopy, "Daily Sending Report", string.Format(@"MES server status is normal!!") + "\n" + "Execution time: " + DateTime.Now, null, out errmessage);
                                SetText2(DateTime.Now + "\t Sleeping----> \n", Color.FromName("Green"));
                            }
                            else
                            {
                                SetText2(DateTime.Now + "\t " + string.Format(@"Query exception, no data found!"), Color.FromName("Green"));
                                //SendMail2(string.Format(@"The query is abnormal, no data is found!!") + "\n" + "Execution time: " + DateTime.Now);
                                MailUtil.SendMessage(listEmailReceiver, listEmailCopy, "Daily Sending Report", string.Format(@"The query is abnormal, no data is found!!") + "\n" + "Execution time: " + DateTime.Now, null, out errmessage);
                                SetText2(DateTime.Now + "\t Sleeping----> \n", Color.FromName("Green"));
                            }
                        }
                        else
                        {
                            //MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                            SetText2(DateTime.Now + "\t " + "Query not connected to API " + JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString(), Color.FromName("Red"));
                            //SendMail2("The query is not connected to the API" + JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString() + "\n" + DateTime.Now);
                            MailUtil.SendMessage(listEmailReceiver, listEmailCopy, "Daily Sending Report", "The query is not connected to the API" + JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString() + "\n" + DateTime.Now, null, out errmessage);
                            SetText2(DateTime.Now + "\t Sleeping----> \n", Color.FromName("Green"));
                        }
                    }
                    catch (Exception ex)
                    {
                        SetText2(DateTime.Now + "\t " + "Query exception " + ex.Message + "\n", Color.FromName("Red"));
                        //SendMail2("An exception occurred in the query" + "\n" + ex.Message + DateTime.Now + "\n");
                        MailUtil.SendMessage(listEmailReceiver, listEmailCopy, "Daily Sending Report", "An exception occurred in the query" + "\n" + ex.Message + DateTime.Now + "\n", null, out errmessage);
                        SetText2(DateTime.Now + "\t Sleeping----> \n", Color.FromName("Green"));
                    }

                    if (DateTime.Now.Hour == 22 || DateTime.Now.Hour == 7)
                    {
                        l_vali_sync = true;
                    }
                }

                //01 - 05//16-17
                if ((DateTime.Now.Hour >= int.Parse(xgxc1.Text) && DateTime.Now.Hour <= int.Parse(xgxc2.Text)) || (DateTime.Now.Hour >= 16 && DateTime.Now.Hour <= 18))
                {
                    l_vali_sync = false;
                }

                //Set the sleep interval. The time difference from the current time to the next hour
                DateTime dt = DateTime.Now.AddHours(1);
                string time_hour = dt.ToString("yyyy-MM-dd HH");//2008-9-4 20:12:12
                time_hour += ":00:10";//The next milliseconds must be converted into integers, and the decimal part will be removed, so that the sleep duration cannot locate the time to the next whole point, so add 10 seconds here. Example: After sleeping at 10:37, it should be 11:00, but because the fractional part of the sleeping milliseconds is removed, it can only reach 10:59 after sleeping.
                DateTime time_next_hours = DateTime.Parse(time_hour);
                TimeSpan ts = time_next_hours - DateTime.Now;
                double Milliseconds = ts.TotalMilliseconds;

                Thread.Sleep((int)Milliseconds);
                //Thread.Sleep(1000 * 60 * 60);
            }
        }

        private void SendMail2(string mailInfo)
        {
            List<string> sendToList = new List<string>();
            List<string> sendCCList = new List<string>();
            //sendCCList.Add("466910876@qq.com");
            //sendCCList.Add("624595594@qq.com");
            //sendToList.Add("969093390@qq.com");

            string body = "Rolling daily production scheduling query results：" + mailInfo;
            string[] attachmentsPath = { };
            try
            {
                //MailUtil.SendMessage(sendToList, sendCCList, subject, body, null, out errorMessage);
                EmalHelper.SendEmail("466910876@qq.com,624595594@qq.com,969093390@qq.com,2802764178@qq.com,948192439@qq.com", "work report", body);   //Set the mailbox for sending emails, here is QQ mailbox     
                //EmalHelper.SendEmail("2802764178@qq.com", "工作汇报", body);   //设置邮件发送的邮箱，这里是QQ邮箱     

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "", "Email sending exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (thread2 != null)
            {
                thread2.Abort();
                richTextBox2.Text += "\n" + DateTime.Now + "\t thread closed---->";
            }
        }


        #endregion

        #region one-button start
        private void button3_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
            button2_Click(sender, e);
            button4_Click(sender, e);
            btn_start_DN_Click(sender, e);
        }
        #endregion

        #region Interface exception reminder
        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = DateTime.Now + "\t thread started---->";
            thread3 = new Thread(Go3);
            thread3.Start();
        }

        public void Go3()
        {
            bool l_vali_sync = false;
            List<string> listEmailReceiver = new List<string>() { "pengtao-xu@apachefootwear.com" };
            List<string> listEmailCopy = new List<string>() { "dapeng-fang@apachefootwear.com,sunwy-xie@apachefootwear.com" };
            List<string> listError = new List<string>() { "dapeng-fang@apachefootwear.com,pengtao-xu@apachefootwear.com,sunwy-xie@apachefootwear.com" };
            string errmessage = "";
            while (true)
            {
                //20-22
                if (DateTime.Now.Hour >= int.Parse(textBox7.Text) && DateTime.Now.Hour <= int.Parse(textBox6.Text) && l_vali_sync == false)
                {
                    l_vali_sync = true;
                    SetText3(DateTime.Now + "\t Automatic query data start---->", Color.FromName("Green"));
                    try
                    {
                        DataTable data = new DataTable();
                        string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "GetInterfaceError", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
                        if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                            DataTable jsondata = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);



                            SetText3(DateTime.Now + "\t " + string.Format(@"Interface synchronization exception: {0}, call synchronization exception: {1}", jsondata.Rows[0]["InterError"].ToString(), jsondata.Rows[0]["TransferError"].ToString()), Color.FromName("Green"));

                            string body = string.Format(@"Interface synchronization exception: {0}," + "\r\n" + "Call synchronization exception: {1}", jsondata.Rows[0]["InterError"].ToString(), jsondata.Rows[0]["TransferError"].ToString()) + ".\n" + "Execution time---" + DateTime.Now;
                            MailUtil.SendMessage(listEmailReceiver, listEmailCopy, "Interface upload exception reminder", body, null, out errmessage);
                            SetText3(DateTime.Now + "\t Sleeping----> \n", Color.FromName("Green"));
                        }
                        else
                        {
                            SetText3(DateTime.Now + "\t " + "Query not connected to API " + JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString(), Color.FromName("Red"));
                            //SendMail("The query is not connected to the API" + "\n" + DateTime.Now);
                            string body = "The query is not connected to the API" + JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString() + "\n" + DateTime.Now;
                            //MailUtil.SendMessage(listEmailReceiver, listEmailCopy, "Daily Sending Report", body, null, out errmessage);
                            MailUtil.SendMessage(listError, listError, "Interface upload exception reminder", body, null, out errmessage);
                            SetText3(DateTime.Now + "\t Sleeping----> \n", Color.FromName("Green"));
                        }
                    }
                    catch (Exception ex)
                    {

                        SetText3(DateTime.Now + "\t " + "Query exception " + ex.Message + "\n", Color.FromName("Red"));
                        //SendMail("Query exception occurred" + "\n" + DateTime.Now + ex.InnerException + "\n");
                        string body = "Query exception" + "\n" + ex.Message + DateTime.Now + "\n";
                        //MailUtil.SendMessage(listEmailReceiver, listEmailCopy, "Daily Sending Report", body, null, out errmessage);
                        MailUtil.SendMessage(listError, listError, "Interface upload exception reminder", body, null, out errmessage);
                        SetText3(DateTime.Now + "\t Sleeping----> \n", Color.FromName("Green"));

                    }
                }

                //01-07
                if (DateTime.Now.Hour >= int.Parse(textBox5.Text) && DateTime.Now.Hour <= int.Parse(textBox4.Text))
                {
                    l_vali_sync = false;
                }

                //Set the sleep interval. The time difference from the current time to the next hour
                DateTime dt = DateTime.Now.AddHours(1);
                string time_hour = dt.ToString("yyyy-MM-dd HH");//2008-9-4 20:12:12
                time_hour += ":00:10";//The next milliseconds must be converted into integers, and the decimal part will be removed, so that the sleep duration cannot locate the time to the next whole point, so add 10 seconds here. Example: After sleeping at 10:37, it should be 11:00, but because the fractional part of the sleeping milliseconds is removed, it can only reach 10:59 after sleeping.
                DateTime time_next_hours = DateTime.Parse(time_hour);
                TimeSpan ts = time_next_hours - DateTime.Now;
                double Milliseconds = ts.TotalMilliseconds;

                Thread.Sleep((int)Milliseconds);
                //Thread.Sleep(1000 * 60 * 60);
            }
        }
        private void SetText3(string text, Color color)
        {
            if (richTextBox3.InvokeRequired)
            {
                SetTextCallBack stcb = SetText3;
                Invoke(stcb, text, color);
            }
            else
            {
                richTextBox3.Focus();
                richTextBox3.Select(richTextBox3.TextLength, 0);
                richTextBox3.SelectionColor = color;
                richTextBox3.ScrollToCaret();
                richTextBox3.AppendText("\n" + text);
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (thread3 != null)
            {
                thread3.Abort();
                richTextBox3.Text += "\n" + DateTime.Now + "\t thread closed---->";
            }
        }
        #endregion

        #region Finished product warehouse full box reminder


        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDate();
        }
        private DataTable GetDate()
        {
            DataTable dtexcel = new DataTable();
            try
            {
                dtexcel = null;
                Dictionary<string, object> p = new Dictionary<string, object>();
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "GetShoseFull", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

                    // DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    //dataGridView1.DataSource = dt;
                    if (dt.Rows.Count > 0)
                    {
                        dtexcel = dt;
                        SetText4(DateTime.Now + "\t " + string.Format(DateTime.Now.ToString() + "total query" + dtexcel.Rows.Count + "Article data"), Color.FromName("Green"));
                    }

                }
                else
                {
                    SetText4(DateTime.Now + "\t " + string.Format(DateTime.Now.ToString() + "Query exception, API error。"), Color.FromName("Red"));
                }
            }
            catch (Exception ex)
            {
                SetText4(DateTime.Now + "\t " + string.Format(DateTime.Now.ToString() + "Query exception，" + ex.Message.ToString()), Color.FromName("Red"));
            }

            return dtexcel;
        }
        private void SetText4(string text, Color color)
        {
            if (richTextBox4.InvokeRequired)
            {
                SetTextCallBack stcb = SetText4;
                Invoke(stcb, text, color);
            }
            else
            {
                richTextBox4.Focus();
                richTextBox4.Select(richTextBox4.TextLength, 0);
                richTextBox4.SelectionColor = color;
                richTextBox4.ScrollToCaret();
                richTextBox4.AppendText("\n" + text);
            }
        }
        public void Go4()
        {

            bool l_vali_sync = false;
            List<string> listEmailReceiver = new List<string>() { "jing-tian@apachefootwear.com" };
            List<string> listEmailCopy = new List<string>() { "jing-tian@apachefootwear.com" };
            List<string> listError = new List<string>() { "jing-tian@apachefootwear.com" };
            string file = Application.StartupPath + @"\成品仓实时满仓报表\成品仓满箱数据" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            //string file = "D:/APEMES/source/KZ_MES/AutoSendEmail/Resources/成品仓满箱数据" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string[] str = new string[1];
            str[0] = file + ".xlsx";
            string errmessage = "";

            while (true)
            {
                //20-22

                if (DateTime.Now.Hour >= int.Parse(textBox11.Text) && DateTime.Now.Hour < int.Parse(textBox10.Text) && l_vali_sync == false)
                {
                    try
                    {
                        l_vali_sync = true;
                        SetText4(DateTime.Now + "\t Automatic query data start---->", Color.FromName("Green"));

                        // DataTable data = new DataTable();                        
                        DataTable d = GetDate();
                        if (d.Rows.Count > 0)
                        {
                            //dataGridView1.DataSource = d;
                            excel ex = new excel();
                            ex.ExportExcel(d, file, "成品仓满仓数据");
                            MailUtil.SendMessage(listEmailReceiver, listEmailCopy, "Finished product warehouse full box data reminder", DateTime.Now.ToString(), str, out errmessage);
                            SetText4(DateTime.Now + "\t ----Mail sent successfully。> \n", Color.FromName("Green"));
                        }
                        else
                        {
                            MailUtil.SendMessage(listError, listError, "Finished product warehouse full box data reminder", DateTime.Now.ToString() + "/no data。", null, out errmessage);
                            SetText4(DateTime.Now + "\t ----no data。> \n", Color.FromName("Red"));
                        }
                    }
                    catch (Exception ex)
                    {
                        MailUtil.SendMessage(listError, listError, "Finished product warehouse full box data reminder", DateTime.Now.ToString() + ex.Message.ToString(), null, out errmessage);
                        SetText4(DateTime.Now + "\t ----execution error。\n" + ex.Message.ToString(), Color.FromName("Red"));
                    }

                }

                //01-07
                if (DateTime.Now.Hour >= int.Parse(textBox9.Text) && DateTime.Now.Hour <= int.Parse(textBox8.Text))
                {
                    l_vali_sync = false;
                }

                //Set the sleep interval. The time difference from the current time to the next hour
                DateTime dt = DateTime.Now.AddHours(1);
                string time_hour = dt.ToString("yyyy-MM-dd HH");//2008-9-4 20:12:12
                time_hour += ":00:10";//The next milliseconds must be converted into integers, and the decimal part will be removed, so that the sleep duration cannot locate the time to the next whole point, so add 10 seconds here. Example: After sleeping at 10:37, it should be 11:00, but because the fractional part of the sleeping milliseconds is removed, it can only reach 10:59 after sleeping.
                DateTime time_next_hours = DateTime.Parse(time_hour);
                TimeSpan ts = time_next_hours - DateTime.Now;
                double Milliseconds = ts.TotalMilliseconds;

                Thread.Sleep((int)Milliseconds);
                //Thread.Sleep(1000 * 60 * 60);

            }

        }


        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox4.Text += DateTime.Now + "\t thread started---->";
            thread4 = new Thread(Go4);
            thread4.Start();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (thread4 != null)
            {
                thread4.Abort();
                richTextBox4.Text += "\n" + DateTime.Now + "\t thread closed---->";
            }
        }

        #endregion


        #region Packaging material recoil failure reminder

        bool is_startPackAuto = false;

        private void btnPackAutoStart_Click(object sender, EventArgs e)
        {
            if (is_startPackAuto)
            {
                MessageBox.Show("Unable to re-open the thread", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                return;
            }
            textPackAuto.Text = DateTime.Now + "\t thread started---->";
            threadPackAuto = new Thread(TreadPackAuto);
            threadPackAuto.Start();
        }

        public void TreadPackAuto()
        {
            bool l_vali_sync = false;
            is_startPackAuto = true;
            List<string> listEmailReceiver = new List<string>() { "lihong-chang@apachefootwear.com," };
            List<string> listEmailCopy = new List<string>() { "pengtao-xu@apachefootwear.com" };
            List<string> listError = new List<string>() { "pengtao-xu@apachefootwear.com" };
            string errmessage = "";
            while (true)
            {
                //20-22
                if (DateTime.Now.Hour >= int.Parse(textPackAutoStart.Text) && DateTime.Now.Hour <= int.Parse(textPackAutoEnd.Text) && l_vali_sync == false)
                {
                    l_vali_sync = true;
                    SetText_PackAuto(DateTime.Now + "\t Automatic query data start---->", Color.FromName("Green"));
                    try
                    {
                        Dictionary<string, Object> p = new Dictionary<string, object>();
                        p.Add("type", "A");
                        string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Auto_AbnormalServer", "GetPackAutoErrorList", Program.client.UserToken, JsonConvert.SerializeObject(p));
                        if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                            DataTable jsondata = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                            if (jsondata.Rows.Count > 0)
                            {
                                string piath = System.Windows.Forms.Application.StartupPath + "\\包材反冲失败报表\\";
                                //string binPath = piath.Substring(0, piath.LastIndexOf("\\") + 1);
                                string fileNme = "包材反冲失败明细" + DateTime.Now.ToString("yyyyMMdd");
                                string filePath = piath + fileNme + ".xlsx";
                                ExportExcels.ExportNoPop(fileNme, jsondata, filePath);
                                string body = "包材反冲失败明细如附件";
                                string[] attachList = new string[] { filePath };
                                MailUtil.SendMessage(listEmailCopy, listEmailCopy, "包材反冲失败提醒", body, attachList, out errmessage);
                            }
                            SetText_PackAuto(DateTime.Now + "\t " + string.Format(@"包材反冲失败：{0} 条数据", jsondata.Rows[0]["TransferError"].ToString()), Color.FromName("Green"));
                            SetText_PackAuto(DateTime.Now + "\t 休眠中----> \n", Color.FromName("Green"));
                        }
                        else
                        {
                            SetText_PackAuto(DateTime.Now + "\t " + "查询没连上API " + JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString(), Color.FromName("Red"));
                            string body = "查询没连上API" + JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString() + "\n" + DateTime.Now;
                            MailUtil.SendMessage(listError, listError, "包材反冲失败提醒", body, null, out errmessage);
                            SetText_PackAuto(DateTime.Now + "\t 休眠中----> \n", Color.FromName("Green"));
                        }
                    }
                    catch (Exception ex)
                    {
                        SetText_PackAuto(DateTime.Now + "\t " + "查询发生异常 " + ex.Message + "\n", Color.FromName("Red"));
                        string body = "查询发生异常" + "\n" + ex.Message + DateTime.Now + "\n";
                        MailUtil.SendMessage(listError, listError, "包材反冲失败提醒", body, null, out errmessage);
                        SetText_PackAuto(DateTime.Now + "\t 休眠中----> \n", Color.FromName("Green"));
                    }
                }

                //01-07
                if (DateTime.Now.Hour > int.Parse(textPackAutoEnd.Text) && DateTime.Now.Hour < int.Parse(textPackAutoStart.Text))
                {
                    l_vali_sync = false;
                }

                //设置休眠间隔时间。从当前时间到下一个整点的时间差
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

        private void SetText_PackAuto(string text, Color color)
        {
            if (textPackAuto.InvokeRequired)
            {
                SetTextCallBack stcb = SetText_PackAuto;
                Invoke(stcb, text, color);
            }
            else
            {
                textPackAuto.Focus();
                textPackAuto.Select(textPackAuto.TextLength, 0);
                textPackAuto.SelectionColor = color;
                textPackAuto.ScrollToCaret();
                textPackAuto.AppendText("\n" + text);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要关闭线程吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (threadPackAuto != null || is_startPackAuto)
                {
                    SetText_PackAuto(DateTime.Now + "\t 线程关闭---->\n", Color.FromName("Green"));
                    threadPackAuto.Abort();
                    is_startPackAuto = false;
                }
            }
        }


        #endregion


        #region  DN提醒
        private void btn_start_DN_Click(object sender, EventArgs e)
        {
            rtb_DN.Text += DateTime.Now + "\t 线程已启动---->";
            threadDN = new Thread(GoDN);
            threadDN.Start();
        }
        private void btn_query_DN_Click(object sender, EventArgs e)
        {
            dgv_DN.DataSource = GetDnTable();
            dgv_DN.Update();
        }
        private void SetTextDN(string text, Color color)
        {
            if (rtb_DN.InvokeRequired)
            {
                SetTextCallBack stcb = SetTextDN;
                Invoke(stcb, text, color);
            }
            else
            {
                rtb_DN.Focus();
                rtb_DN.Select(rtb_DN.TextLength, 0);
                rtb_DN.SelectionColor = color;
                rtb_DN.ScrollToCaret();
                rtb_DN.AppendText("\n" + text);
            }
        }
        private DataTable GetDnTable()
        {
            DataTable dtexcel = new DataTable();
            try
            {
                dtexcel = null;
                Dictionary<string, object> p = new Dictionary<string, object>();
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "GetDnMessage", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

                    if (dt.Rows.Count > 0)
                    {
                        dtexcel = dt;
                        SetTextDN(DateTime.Now + "\t " + string.Format(DateTime.Now.ToString() + "共查询到" + dtexcel.Rows.Count + "条数据"), Color.FromName("Green"));
                    }
                }
                else
                {
                    SetTextDN(DateTime.Now + "\t " + string.Format(DateTime.Now.ToString() + "查询异常，API有误。" + Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString()), Color.FromName("Red"));
                }
            }
            catch (Exception ex)
            {
                SetTextDN(DateTime.Now + "\t " + string.Format(DateTime.Now.ToString() + "查询异常，" + ex.Message.ToString()), Color.FromName("Red"));
            }

            return dtexcel;
        }


        public void GoDN()
        {

            bool l_vali_sync = false;
            List<string> listEmailReceiver = new List<string>() { "jing-tian@apachefootwear.com", "zeheng-chen@apachefootwear.com" };
            List<string> listEmailCopy = new List<string>() { "jing-tian@apachefootwear.com", "zeheng-chen@apachefootwear.com" };
            List<string> listError = new List<string>() { "jing-tian@apachefootwear.com", "zeheng-chen@apachefootwear.com" };
            string file = Application.StartupPath + @"\DN报表\DN过账失败目录" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            //string file = "D:/APEMES/source/KZ_MES/AutoSendEmail/Resources/成品仓满箱数据" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string[] str = new string[1];
            str[0] = file + ".xlsx";
            string errmessage = "";

            while (true)
            {
                //20-22
                if (DateTime.Now.Hour >= int.Parse(txt_first_Hours_DN.Text) && DateTime.Now.Hour < int.Parse(txt_last_Hours_DN.Text) && l_vali_sync == false)
                {
                    try
                    {
                        l_vali_sync = true;
                        SetTextDN(DateTime.Now + "\t 自动查询数据开始---->", Color.FromName("Green"));

                        DataTable data = new DataTable();
                        DataTable d = GetDnTable();
                        if (d.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = d;
                            excel ex = new excel();
                            ex.ExportExcel(d, file, "DN过账失败目录");

                            MailUtil.SendMessage(listEmailReceiver, listEmailCopy, "DN过账失败提醒", DateTime.Now.ToString(), str, out errmessage);
                            SetTextDN(DateTime.Now + "\t ----邮件发送成功。> \n", Color.FromName("Green"));
                        }
                        else
                        {
                            MailUtil.SendMessage(listError, listError, "DN过账失败提醒", DateTime.Now.ToString() + "/无数据。", null, out errmessage);
                            SetTextDN(DateTime.Now + "\t ----无数据。> \n", Color.FromName("Red"));
                        }
                    }
                    catch (Exception ex)
                    {
                        MailUtil.SendMessage(listError, listError, "DN过账失败提醒", DateTime.Now.ToString() + ex.Message.ToString(), null, out errmessage);
                        SetTextDN(DateTime.Now + "\t ----执行错误。\n" + ex.Message.ToString(), Color.FromName("Red"));
                    }

                }

                //01-07
                if (DateTime.Now.Hour >= int.Parse(txt_DN_time_start.Text) && DateTime.Now.Hour <= int.Parse(txt_DN_time_end.Text))
                {
                    l_vali_sync = false;
                }

                //设置休眠间隔时间。从当前时间到下一个整点的时间差
                DateTime dt = DateTime.Now.AddHours(1);
                string time_hour = dt.ToString("yyyy-MM-dd HH");//2008-9-4 20:12:12
                time_hour += ":00:10";//后面毫秒要强转成整数型，会去掉小数部分，导致休眠时长无法将时间定位到下一个整点，这里加10秒。例：10:37休眠后应该要到11:00，但由于休眠的毫秒去掉了小数部分，休眠后只能到10:59。
                DateTime time_next_hours = DateTime.Parse(time_hour);
                TimeSpan ts = time_next_hours - DateTime.Now;
                //  double Milliseconds = ts.TotalMilliseconds;

                Thread.Sleep(ts);
                //Thread.Sleep(1000 * 60 * 60);

            }

        }


        #endregion
    }
}
