using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace HealthCheck
{
    public class Mailing
    {
        public void SendMail(List<string> lto,List<string> lcc,List<string> lbcc,string strfrm,string strsubject,string strbody,string smtpserverip,int port)
        {

            //Email Formation
            MailMessage message = new MailMessage();

            //From Details            
            MailAddress mailfrom = new MailAddress(strfrm);
            message.From= mailfrom;

            //To Details
            foreach (string s in lto)
            {
                if (!string.IsNullOrEmpty(s))
                {
                message.To.Add(s);
                }
            }
            //CC Details
            foreach (string s in lcc)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    message.CC.Add(s);
                }
            }
            //BCC Details
            foreach (string s in lbcc)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    message.Bcc.Add(s);
                }
            }

            //Subject Details
            message.Subject = strsubject;
            message.IsBodyHtml = true;


            //Body Details
            message.Body = strbody;

            //Sending Email
            //SmtpClient cl = new SmtpClient(smtpserverip, port);//ip="10.202.65.58" port=25
            SmtpClient cl = new SmtpClient(smtpserverip);//ip="10.202.65.58"
           // cl.UseDefaultCredentials = true;
            cl.UseDefaultCredentials = true;
            //cl.Credentials = new System.Net.NetworkCredential("balamurugan.iyer@Capgemini.com", "Nov@2015");
            cl.Send(message);
        }
    }
}
