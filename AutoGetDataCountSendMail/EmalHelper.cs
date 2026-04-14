using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AutoGetDataCountSendMail
{
    public class EmalHelper
    {
        /// <summary>
        ///     SMTP instance
        /// </summary>
        static SmtpClient client;

        /// <summary>
        ///     send Message
        /// </summary>
        /// <param name="Receiver">mail recipient</param>
        /// <param name="Subject">Email Subject</param>
        /// <param name="content">content of email</param>
        public static void SendEmail(string Receiver, string Subject, string content)
        {
            if (string.IsNullOrEmpty(Receiver) || string.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(content))
            {

                throw new ArgumentNullException("SendEmail parameter null exception！");
            }

            if (client == null)
            {
                try
                {
                    //163 send configuration                    
                    client = new SmtpClient();
                    client.Host = "smtp.163.com";
                    client.Port = 25;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = true;


                    //qq发送配置的参数//切EnableSsl必须设置为true  
                    //client = new System.Net.Mail.SmtpClient();
                    //client.Host = "smtp.qq.com";
                    //client.Port = 25;
                    //client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    //client.EnableSsl = true;
                    //client.UseDefaultCredentials = false;

                    client.Credentials = new NetworkCredential("sunwyxie_19@163.com", "PBEOVFVKAYGNQSFU");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            try
            {
                MailMessage Message = new MailMessage();
                Message.SubjectEncoding = Encoding.UTF8;
                Message.BodyEncoding = Encoding.UTF8;
                Message.Priority = MailPriority.High;

                Message.From = new MailAddress("sunwyxie_19@163.com", "张三");
                //Add email recipient addresses
                string[] receivers = Receiver.Split(',');
                Array.ForEach(receivers.ToArray(), ToMail => { Message.To.Add(ToMail); });

                Message.Subject = Subject;
                Message.Body = content;
                Message.IsBodyHtml = true;
                client.Send(Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}