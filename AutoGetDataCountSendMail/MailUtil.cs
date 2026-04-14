using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AutoGetDataCountSendMail
{
    class MailUtil
    {
        /// <summary>
        ///     send email
        /// </summary>
        /// <param name="sendToList">Recipients (multiple email addresses must be separated by comma characters (","))</param>
        /// <param name="sendCCList">Cc (multiple email addresses must be separated by comma character (","))</param>
        /// <param name="subject">theme</param>
        /// <param name="body">content</param>
        /// <param name="attachmentsPath">attachment path</param>
        /// <param name="errorMessage">error message</param>
        public static bool SendMessage(List<string> sendToList, List<string> sendCCList, string subject, string body, string[] attachmentsPath, out string errorMessage)
        {
            string userEmailAddress = "ape-mes@apachefootwear.com";
            string userName = "ape-mes@apachefootwear.com";
            string password = "Aa123456";
            string host = "10.2.1.186";
            int port = 25;

            //string userEmailAddress = "466910876@qq.com";
            //string userName = "466910876@qq.com";
            ////string userName = "pengtao-xu@apachefootwear.com";
            //string password = "xie198511";
            //string host = "smtp.qq.com";
            //int port = 25;


            //string userEmailAddress = "yintan-cai@apachefootwear.com";
            //string userName = "yintan-cai@apachefootwear.com";
            //string password = "Aa123456";
            //string host = "10.2.19.121";

            errorMessage = string.Empty;
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(userEmailAddress, password); //Username Password
            client.DeliveryMethod = SmtpDeliveryMethod.Network; //Specify how to send email  
            client.Host = host; //Mail Server
            client.Port = port; //Port number Non-SSL mode, the default port number is: 25
            client.UseDefaultCredentials = false;

            MailMessage msg = new MailMessage();
            //add sender
            foreach (string send in sendToList)
            {
                msg.To.Add(send);
            }

            //plus cc
            foreach (string cc in sendCCList)
            {
                msg.CC.Add(cc);
            }

            //Add attachments when there are attachments
            if (attachmentsPath != null && attachmentsPath.Length > 0)
            {
                foreach (string path in attachmentsPath)
                {
                    var attachFile = new Attachment(path);
                    msg.Attachments.Add(attachFile);
                }
            }

            msg.From = new MailAddress(userEmailAddress, userName); //sender address
            msg.Subject = subject; //mail title   
            msg.Body = body; //content of email   
            msg.BodyEncoding = Encoding.UTF8; //message content encoding   
            msg.IsBodyHtml = true; //Is it an HTML email   
            msg.Priority = MailPriority.High; //mail priority   

            try
            {
                client.Send(msg);
                return true;
            }
            catch (SmtpException ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}