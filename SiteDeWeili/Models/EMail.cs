using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Net.Mail;
using System.Configuration;

namespace SendingEmail.Models
{
    public class EMail
    {

        private String strToAddress;
        private String strToSmtpClient;
        private bool BoolEnableSsl ;
        private String strPort;
        private string strUserId;
        private string strPasseword;
   

        /*Initialisation of the variables*/
        private void InitMail()
        {
            /* Smtp server name*/
            strToSmtpClient = ConfigurationManager.AppSettings.Get("SmtpClient");
            /* enable SSL */
            BoolEnableSsl = (ConfigurationManager.AppSettings.Get("EnableSSL").ToUpper() == "YES") ? true : false;
            /* Smtp port number */
            strPort = ConfigurationManager.AppSettings.Get("SMTPPort");
            /* Mail User id*/
            strUserId = ConfigurationManager.AppSettings.Get("UserID");
            /* Mail User password */
            strPasseword = ConfigurationManager.AppSettings.Get("Password");
            /* To Address */
            strToAddress =ConfigurationManager.AppSettings.Get("ToAddress");
        }
         
        public void SendMail(String FromAdresse, String Subject, String message, String name)
        {
            InitMail();
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(FromAdresse,name);
                msg.To.Add(strToAddress);
                msg.Subject = Subject + " "+FromAdresse;
                /*I use the tag <table> here because some webmails don't recongnize the tags <html><body>...*/
                string body = "<html><head/><body><table cellspacing='0' cellpadding='10' border='0'><tr><td width='600'>"+message+"</td></tr></table></body></html>";
                msg.Body = body;
                msg.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(strToSmtpClient);
                smtp.EnableSsl = BoolEnableSsl;
                smtp.Port = Convert.ToInt32(strPort);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(strUserId, strPasseword);

                //send the message
                smtp.Send(msg);
            }
            catch (SmtpFailedRecipientsException e) 
            {
                //the exeception will be caught in the controller
            }
        }
    }
}