using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;

namespace AutoGetDataCountSendMail
{
    public partial class AutoGetDataCountSendMailForm : MaterialForm
    {
        delegate void SetTextCallBack(string text, Color color);

        Thread thread;

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

        public AutoGetDataCountSendMailForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Normal;//Set the default size of the window
        }

        private void AutoGetDataCountSendMailForm_Load(object sender, EventArgs e)
        {
            //Set to run after 21:00 pm and end before 24:00 pm
            if (string.IsNullOrWhiteSpace(txtStartHours.Text))
            {
                txtStartHours.Text = "21";
            }

            if (string.IsNullOrWhiteSpace(txtEndTime.Text))
            {
                txtEndTime.Text = "24";
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = DateTime.Now + "\t in startup thread---->";
            thread = new Thread(Go);
            thread.Start();
        }

        public void Go()
        {
            bool isSync = false;
            while (true)
            {
                if (DateTime.Now.Hour >= int.Parse(txtStartHours.Text) && DateTime.Now.Hour <= int.Parse(txtEndTime.Text) && isSync == false)
                {
                    isSync = true;
                    SetText(DateTime.Now + "\t Automatic timing service monitoring starts---->", Color.FromName("Green"));
                    try
                    {
                        //Dictionary<string, object> p = new Dictionary<string, object>();
                        ////p.Add("job","");
                        //string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetDBMSJob", Program.client.UserToken, JsonConvert.SerializeObject(p));
                        //if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        //{
                        //    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        //    bool isDbmsJobRun = JsonConvert.DeserializeObject<bool>(json);
                        //    if (isDbmsJobRun==false)
                        //    {
                        //        SendMail($"{DateTime.Now}：查询到数据库定时任务没有运行");
                        //    }

                        //}

                        Dictionary<string, object> parm = new Dictionary<string, object>();
                        parm.Add("status", 0); //online record
                        string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetTodayOnlineAndOfflines", Program.client.UserToken, JsonConvert.SerializeObject(parm));
                        if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                SendMail($"{DateTime.Now}：It is found that the Windows timing service task is not running or there is an error in the timing service update.！");
                            }
                        }
                        else
                        {
                            MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                            SetText(DateTime.Now + "\t An exception occurred in the query" + JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"] + "\n", Color.FromName("Red"));
                            SendMail(DateTime.Now + "\t The query is not connected to the API");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            SetText(DateTime.Now + "\t An exception occurred in the query" + ex.InnerException.InnerException + "\n", Color.FromName("Red"));
                            SendMail(DateTime.Now + "\t An exception occurred in the query" + ex.InnerException.InnerException + "\n");
                        }
                        else
                        {
                            SetText(DateTime.Now + "\t An exception occurred in the query" + ex.InnerException + "\n", Color.FromName("Red"));
                            SendMail(DateTime.Now + "\t An exception occurred in the query" + ex.InnerException + "\n");
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(txtEditEndTime.Text) && !string.IsNullOrWhiteSpace(txtEditStartHours.Text) && DateTime.Now.Hour >= int.Parse(txtEditStartHours.Text) && DateTime.Now.Hour <= int.Parse(txtEditEndTime.Text))
                {
                    isSync = false;
                }

                SetText(DateTime.Now + "\t suspended----> \n", Color.FromName("Green"));
                Thread.Sleep(1000 * 60 * 60); //Sleep for 1 hour, 1 second = 1000 milliseconds.
            }
        }


        private void SendMail(string mailInfo)
        {
            List<string> sendToList = new List<string>();
            List<string> sendCCList = new List<string>();
            //sendCCList.Add("466910876@qq.com");
            //sendCCList.Add("624595594@qq.com");
            //sendToList.Add("969093390@qq.com");

            string body = "Working hours record timing service query results：" + mailInfo;
            string[] attachmentsPath = { };//There are corresponding parameter names in SendEmail that can be assigned
            try
            {
                //string subject = "";//标题
                //string errorMessage = "";//错误信息
                //MailUtil.SendMessage(sendToList, sendCCList, subject, body, null, out errorMessage);
                EmalHelper.SendEmail("466910876@qq.com,624595594@qq.com,969093390@qq.com,@401984805@qq.com", "work report", body);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException + "", "Email sending exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the thread？", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (thread != null)
                {
                    SetText(DateTime.Now + "\t thread close---->\n", Color.FromName("Green"));
                    thread.Abort();//中止线程
                }
            }
        }

        private void AutoGetDataCountSendMailForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread != null && thread.IsAlive)
            {
                MessageBox.Show("Please abort the thread first");
                e.Cancel = true;//Close window is not allowed
            }
        }
    }
}