using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthCheck;
namespace HealthcheckApplication
{
    public class ReportMailer
    {
        List<string> To;
        List<string> CC;
        List<string> BCC;
        string From;
        string Subject;
        String ServerIP;
        int Port;
        string Body;
        Mailing m;
        public ReportMailer(int passcount, int failcount, string body)
        {
            To = MailConfiguration.GetReceivers();
            CC = MailConfiguration.GetCCReceivers();
            BCC = MailConfiguration.GetBCCReceivers();
            From = MailConfiguration.GetMailFroms();
            Subject = MailConfiguration.GetMailSubject();
            ServerIP = MailConfiguration.GetServerIP();
            Port = MailConfiguration.GetServerPort();         
            Body = body;
            //From = "SharePoint@us.astellas.com";
            //Subject = "SP & .net HealthCheck Details : " + passcount + " Pass ------ " + failcount + " Alerts";
            //ServerIP = "10.202.65.58";
            //ServerIP = "IN-MUM-SMTP1.IN.CAPGEMINI.COM"; //Corp Exchange server
            // Port = 25;
            
        }


        public ReportMailer(int passcount, int failcount, string body, string subject)
        {
            To = MailConfiguration.GetReceivers();
            CC = MailConfiguration.GetCCReceivers();
            BCC = MailConfiguration.GetBCCReceivers();
            From = MailConfiguration.GetMailFroms();
            Subject = subject;
            ServerIP = MailConfiguration.GetServerIP();
            Port = MailConfiguration.GetServerPort();
            Body = body;
            //From = "SharePoint@us.astellas.com";
            //Subject = "SP & .net HealthCheck Details : " + passcount + " Pass ------ " + failcount + " Alerts";
            //ServerIP = "10.202.65.58";
            //ServerIP = "IN-MUM-SMTP1.IN.CAPGEMINI.COM"; //Corp Exchange server
            // Port = 25;

        }

        public void SendMail()
        {

            Mailing m = new Mailing();
            //Console.WriteLine("Sending");
            //Console.WriteLine("Body = " + Body);
            //Console.WriteLine("Send?");
            //Console.ReadLine();
            m.SendMail(To, CC, BCC, From, Subject, Body, ServerIP, Port);
            //Console.WriteLine("Sent!");
            //Console.ReadLine();
        }



    }
}
