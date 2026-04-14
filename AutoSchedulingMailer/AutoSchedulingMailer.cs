using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.IO;

namespace AutoSchedulingMailer
{
    public partial class AutoSchedulingMailer : MaterialForm
    {
        delegate void SetTextCallBack(string text, Color color);
        Thread thread = null;
        public AutoSchedulingMailer()
        {
            InitializeComponent();
        }

        private void AutoSchedulingMailer_Load(object sender, EventArgs e)
        {
            enableStopOnSunday.Checked = true;

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "email_list.txt");
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                richEmailList.Text = text;
            }
        }

        private void loadStatus(bool status)
        {
            txtEndTime.Enabled = status;
            txtStartTime.Enabled = status;
            txtSystemID.Enabled = status;
            txtSystemPWD.Enabled = status;
            richEmailList.Enabled = status;
            enableStopOnSunday.Enabled = status;
            disableStopOnSunday.Enabled = status;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtEndTime.Text == String.Empty || txtStartTime.Text == String.Empty || txtSystemID.Text == String.Empty || txtSystemPWD.Text == String.Empty || richEmailList.Text == String.Empty)
            {
                MessageBox.Show("Configure is required");
                return;
            }

            string debugFolderPath = Path.GetDirectoryName(Application.ExecutablePath);

            string filePath = Path.Combine(debugFolderPath, "email_list.txt");
            string content = richEmailList.Text;
            File.WriteAllText(filePath, content);

            loadStatus(false);

            this.richScheduling.Text = DateTime.Now + "\t " + "AutoMailer: Thread start" + "---->";
            thread = new Thread(new ThreadStart(Go));
            thread.Start();
        }

        private void SetText(string text, Color color)
        {

            if (this.richScheduling.InvokeRequired)
            {
                SetTextCallBack stcb = new SetTextCallBack(SetText);
                this.Invoke(stcb, new object[] { text, color });
            }
            else
            {
                this.richScheduling.Focus();
                this.richScheduling.Select(this.richScheduling.TextLength, 0);
                this.richScheduling.SelectionColor = color;
                this.richScheduling.ScrollToCaret();
                this.richScheduling.AppendText("\n" + text);
            }
        }

        public void Go()
        {
            bool l_vali_sync = false;
            while (true)
            {
                //20-22
                if (DateTime.Now.Hour >= (int.Parse(txtStartTime.Text.ToString())) && DateTime.Now.Hour <= (int.Parse(txtEndTime.Text.ToString())) && l_vali_sync == false)
                {
                    if (enableStopOnSunday.Checked && DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                    {
                        Thread.Sleep(3600000); //1 hour
                        continue;
                    }
                    l_vali_sync = true;
                    SetText(DateTime.Now + "\t " + "AutoMailer: Automatic AEQS mail starts" + "---->", Color.FromName("Green"));
                    DataTable data = new DataTable();

                    Dictionary<string, Object> d1 = new Dictionary<string, object>();
                    string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.AutoGenerationSchedulingOrderServer", "GetAEQSReport", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d1));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
                    {
                        string dataJson = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();

                        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(dataJson);

                        try
                        {
                            Mailer(dtJson);
                            SetText(DateTime.Now + "\t " + "AutoMailer: Send AEQS mail successfully" + ",---->", Color.FromName("Green"));
                        }
                        catch
                        {
                            SetText(DateTime.Now + "\t " + "AutoMailer: Error send AEQS mail" + ",---->", Color.FromName("Red"));
                        }

                        SetText(DateTime.Now + "\t " + "AutoMailer: Automatic AEQS mail ends" + ",---->", Color.FromName("Green"));

                    }
                }
                //01-07
                if (DateTime.Now.Hour >= (int.Parse("01")) && DateTime.Now.Hour <= (int.Parse("07")))
                {
                    l_vali_sync = false;
                }
                SetText(DateTime.Now + "\t " + "AutoMailer: Sleep" + "----> \n", Color.FromName("Green"));

                DateTime dt = DateTime.Now.AddHours(1);
                string time_hour = dt.ToString("yyyy-MM-dd HH");
                time_hour += ":00:10";
                DateTime time_next_hours = DateTime.Parse(time_hour);
                TimeSpan ts = time_next_hours - DateTime.Now;
                double Milliseconds = ts.TotalMilliseconds;

                Thread.Sleep((int)Milliseconds);
            }
        }

        private void Mailer(DataTable table = null)
        {
            string textBody = String.Empty;

            DateTime today = DateTime.Today;  // lấy ngày hiện tại
            int daysUntilTuesday = ((int)DayOfWeek.Tuesday - (int)today.DayOfWeek) % 7;  // + 7
            DateTime nextTuesday = today.AddDays(daysUntilTuesday);  // lấy ngày thứ 3
            DateTime monday = nextTuesday.AddDays(-1);  // lấy ngày thứ 2
            DateTime wednesday = nextTuesday.AddDays(1);  // lấy ngày thứ 4
            DateTime thursday = nextTuesday.AddDays(2);  // lấy ngày thứ 5
            DateTime friday = nextTuesday.AddDays(3);  // lấy ngày thứ 6
            DateTime saturday = nextTuesday.AddDays(4);  // lấy ngày thứ 7

            int number = 0;
            textBody += @"<br>
                            <style>
                                table {font-family: '等线';
                                font-size: 15px;
                                border-collapse: collapse;
                                }
                                 td, th {border: 1px solid black;
                                 text-align: left;
                                 padding: 4px;
                                 color: #003366;
                                }
                               th {background-color: #ADD8E6;
                                 }
                                 th {color: black;
                                 }
                                 td {background-color: #FFFFE0;}
                                  .border {background-color: transparent;border: none;}
                              </style>";
            textBody += $@"
           <label>Dear All,<br><br>This Report is from Production Quality System real-time data for your reference.</label><br>
<table role=\""presentation\"" >
        	<tr>
                   <td class=\""border\"" align=\""center\"">
        			<table role=\""presentation\"">
        				<tr>
                         <th style=\""width: 40%\"">Function</th>
                         <th style=\""width: 30%\"">Frequency of using</th>
                         <th style=\""width: 10%\"">{monday.ToString("yyyy-MM-dd")}</th>
                         <th style=\""width: 10%\"">{nextTuesday.ToString("yyyy-MM-dd")}</th>
                         <th style=\""width: 10%\"">{wednesday.ToString("yyyy-MM-dd")}</th>
                         <th style=\""width: 10%\"">{thursday.ToString("yyyy-MM-dd")}</th>
                         <th style=\""width: 10%\"">{friday.ToString("yyyy-MM-dd")}</th>
                         <th style=\""width: 10%\"">{saturday.ToString("yyyy-MM-dd")}</th>
                      </tr>
                      <tr>
                         <td>IQC</td>
                         <td>Daily</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>LAB</td>
                         <td>Daily</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>DQA</td>
                         <td>Daily</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>MQA</td>
                         <td>Untime</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>First product confirmation</td>
                         <td>Daily</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>Production trial</td>
                         <td>Untime</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>Quality Abnormal</td>
                         <td>Untime</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>TQC</td>
                         <td>Daily</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>RQC</td>
                         <td>Daily</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>RQC audit</td>
                         <td>Monthly</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>Compliance</td>
                         <td>Monthly</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>Market feedback</td>
                         <td>Monthly</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>Chemical management</td>
                         <td>Daily</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>Metal detection</td>
                         <td>Untime</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>Needle management</td>
                         <td>Daily</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
                      <tr>
                         <td>AQL</td>
                         <td>Daily</td>
                         <td>{table.Rows[number][1]}</td>
                         <td>{table.Rows[number][2]}</td>
                         <td>{table.Rows[number][3]}</td>
                         <td>{table.Rows[number][4]}</td>
                         <td>{table.Rows[number][5]}</td>
                         <td>{table.Rows[number++][6]}</td>
                      </tr>
        </table><br>
<label>Thanks!!<br><b>APC IT-Software</b><br><br><b>Important Points From IT</b><br>
                          1.This Mail is Auto Scheduled by IT-Team Please Donot Reply to this Email.<br>2.If you would like to unsubscribe please let IT-Department Know.</label><br>
        ";

            //textBody += @"<label>Thanks!!<br><b>APC IT-Software</b><br><br><b>Important Points From IT</b><br>
            //              1.This Mail is Auto Scheduled by IT-Team Please Donot Reply to this Email.<br>2.If you would like to unsubscribe please let IT-Department Know.</label><br>";


            SmtpClient Smtp = new SmtpClient();
            Smtp.UseDefaultCredentials = false;
            var NetworkCredentials = new NetworkCredential() { UserName = txtSystemID.Text, Password = txtSystemPWD.Text };
            Smtp.Port = 25;
            Smtp.EnableSsl = false;
            Smtp.Host = "mail.apachefootwear.com";
            Smtp.Credentials = NetworkCredentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(txtSystemID.Text, "APC-System");

            string emails = String.Empty;
            richEmailList.Invoke((MethodInvoker)(() =>
            {
                emails = richEmailList.Text;
            }));
            string[] emailArray = emails.Split(',');

            foreach (string email in emailArray)
            {
                msg.To.Add(email.Trim());
            }


            msg.IsBodyHtml = true;
            msg.Subject = "AEQS usage report";
            msg.Body = textBody;
            Smtp.Send(msg);
        }

        //private void Mailer(DataTable table = null)
        //{
        //    string textBody = String.Empty; //<br>(模块)   //(使用频率)
        //    int num = 0;
        //    textBody += @"<br>
        //	                    <style>
        //                         table {font-family: arial, sans-serif;
        //                         border-collapse: collapse;
        //                         }
        //                          td, th {border: 1px solid black;
        //                          text-align: left;
        //                          padding: 4px;
        //                         }
        //                        th {background-color: #ADD8E6;
        //                          }
        //                          th {color: black;
        //                          }
        //                          td {background-color: #FFFFE0;}
        //                           .border {background-color: transparent;border: none;}
        //                       </style>";
        //    textBody += $@"
        //<label>Dear All,</label><br>
        //	<table role=\""presentation\"" >
        //		<tr>
        //            <td class=\""border\"" align=\""center\"">
        //				<table role=\""presentation\"">
        //					<tr>
        //                  <th style=\""width: 40%\"">Function </th>
        //                  <th style=\""width: 30%\"">Frequency of using </th>
        //                  <th style=\""width: 20%\"">{DateTime.Now.ToString("yyyy/MM/dd")}</th>
        //               </tr>
        //               <tr>
        //                  <td>TQC</td>
        //                  <td>Daily</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //               <tr>
        //                  <td>AQL</td>
        //                  <td>Daily</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //               <tr>
        //                  <td>IQC</td>
        //                  <td>Daily</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //               <tr>
        //                  <td>LAB</td>
        //                  <td>Daily</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //               <tr>
        //                  <td>RQC</td>
        //                  <td>Daily</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //               <tr>
        //                  <td>First product confirmation</td>
        //                  <td>Daily</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //               <tr>
        //                  <td>Chemical management</td>
        //                  <td>Daily</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //               <tr>
        //                  <td>Needle management</td>
        //                  <td>Daily</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //                <tr>
        //                  <td>Quality Abnormal</td>
        //                  <td>Untime</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //               <tr>
        //                  <td>Metal detection</td>
        //                  <td>Untime</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //               <tr>
        //                  <td>Compliance</td>
        //                  <td>Monthly</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //                <tr>
        //                  <td>RQC audit</td>
        //                  <td>Monthly</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //               <tr>
        //                  <td>Market feedback</td>
        //                  <td>Monthly</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //                <tr>
        //                  <td>Production trial</td>
        //                  <td>Untime</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //               <tr>
        //                  <td>DQA</td>
        //                  <td>Daily</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr>
        //               <tr>
        //                  <td>MQA</td>
        //                  <td>Untime</td>
        //                  <td>{table.Rows[0][num++]}</td>
        //               </tr> 
        //	</table>";


        //    SmtpClient Smtp = new SmtpClient();
        //    Smtp.UseDefaultCredentials = false;
        //    var NetworkCredentials = new NetworkCredential() { UserName = txtSystemID.Text, Password = txtSystemPWD.Text };
        //    Smtp.Port = 25;
        //    Smtp.EnableSsl = false;
        //    Smtp.Host = "mail.apachefootwear.com"; //change this
        //    Smtp.Credentials = NetworkCredentials;

        //    MailMessage msg = new MailMessage();
        //    msg.From = new MailAddress(txtSystemID.Text, "APC-System"); //change this

        //    string emails = String.Empty;
        //    richEmailList.Invoke((MethodInvoker)(() =>
        //    {
        //        emails = richEmailList.Text;
        //    }));
        //    string[] emailArray = emails.Split(',');

        //    foreach (string email in emailArray)
        //    {
        //        msg.To.Add(email.Trim());
        //    }


        //    msg.IsBodyHtml = true;
        //    msg.Subject = "AEQS usage report";
        //    msg.Body = textBody;
        //    Smtp.Send(msg);
        //}

        private void btnStop_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("AutoMailer: Are you sure to close the thread?", "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (thread != null)
                {
                    SetText(DateTime.Now + "\t " + "AutoMailer: Thread closed" + "---->\n", Color.FromName("Green"));
                    thread.Abort();
                    loadStatus(true);
                }
            }
        }
    }
}
