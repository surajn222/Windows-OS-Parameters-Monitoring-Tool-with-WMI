using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace HealthcheckApplication
{
    public static class MailConfiguration
    {
        public static List<string> GetReceivers()
        {
            string rawValue = ConfigurationManager.AppSettings["Report_Receiver"];

            return rawValue.Split(';').ToList<string>();

        }

        public static List<string> GetCCReceivers()
        {
            List<string> ccReceivers = new List<string>();
            string rawValue = ConfigurationManager.AppSettings["Report_Receiver_CC"];
            ccReceivers =  rawValue.Split(';').ToList<string>();
            if (ccReceivers.Count == 1 && ccReceivers[0] == string.Empty)
                ccReceivers.Clear();

            return ccReceivers;
    
        }

        public static List<string> GetBCCReceivers()
        {
            List<string> bccReceivers = new List<string>();

            string rawValue = ConfigurationManager.AppSettings["Report_Receiver_BCC"];
            bccReceivers =  rawValue.Split(';').ToList<string>();
            if (bccReceivers.Count == 1 && bccReceivers[0] == string.Empty)
                bccReceivers.Clear();

            return bccReceivers;
        }
        public static string GetMailFroms()
        {
            string rawValue = ConfigurationManager.AppSettings["Report_MailFrom"];

            return rawValue;

        }
        public static string GetMailSubject()
        {
            string rawValue = ConfigurationManager.AppSettings["Report_MailSubject"];

            return rawValue;

        }
        public static string GetServerIP()
        {
            string rawValue = ConfigurationManager.AppSettings["Report_ServerIP"];

            return rawValue;

        }
        public static int GetServerPort()
        {
            int rawValue = int.Parse(ConfigurationManager.AppSettings["Report_Port"]);

            return rawValue;

        }

    }
}
