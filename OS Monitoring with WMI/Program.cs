using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Collections;
using System.Net.Mail;
using System.Management;
using System.ComponentModel;
using System.Threading;


namespace HealthcheckApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The Appmon Tool");

            int count = 0;
            //Adding queries to a list
            foreach (var item in ConfigurationManager.AppSettings.AllKeys)
            {
                if (item.StartsWith("SV"))
                {
                    count = count + 1;
                }

            }

            string[] connList = new string[count];

            int arrcount = 0;
            foreach (var item in ConfigurationManager.AppSettings.AllKeys)
            {
                if (item.StartsWith("SV"))
                {
                    string query = ConfigurationManager.AppSettings[item];
                    // Console.ReadLine();
                    Console.WriteLine("Adding server list");
                    Console.WriteLine(query + Environment.NewLine);
                    connList[arrcount] = query;
                    arrcount = arrcount + 1;
                }

            }

            //Looping through each query in the list and getting data
            foreach (var connect in connList)
            {
                Console.WriteLine("Checking WMI - Physical Memory.Please wait" + Environment.NewLine);
                string[] sv = connect.Split(',');
                string connectionstring = "\\\\" + sv[1];
                Console.WriteLine(connectionstring);
                String path = sv[3];
                Console.WriteLine(path);




                ConnectionOptions connection = new ConnectionOptions();
                //connection.Authority = "ntlmdomain:GDS";
                ManagementScope scope = new ManagementScope(@connectionstring + "\\root\\CIMV2", connection);
                scope.Connect();

                //Console.WriteLine(connect + Environment.NewLine);

                checkParam.checkQueueLength(@connectionstring, @path, scope);
                checkParam.checkAvgDiskSecRead(@connectionstring, @path, scope);
                checkParam.checkAvgDiskSecWrite(@connectionstring, @path, scope);
                checkParam.checkReadQueueLength(@connectionstring, @path, scope);
                checkParam.checkWriteQueueLength(@connectionstring, @path, scope);
                checkParam.checkPhysicalMemory(@connectionstring, @path, scope);
                checkParam.checkCpuUtilization(@connectionstring, @path, scope);
                checkParam.checkProcessorTime(@connectionstring, @path, scope);
                checkParam.checkSqlserverCpuUtilization(@connectionstring, @path, scope);
                checkParam.checkSqlserverMemory(@connectionstring, @path, scope);
                checkParam.checkAllProcessMemory(@connectionstring, @path, scope);
                checkParam.checkAllProcessCpuUtilization(@connectionstring, @path, scope);
                checkParam.checkPageFile(@connectionstring, @path, scope);
                Console.WriteLine("Done with " + connectionstring + Environment.NewLine);
            }


        }
    }
}


