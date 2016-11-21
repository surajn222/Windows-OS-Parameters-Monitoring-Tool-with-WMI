using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Collections;
using System.IO;
using System.Net.Mail;
using System.Management;
using System.ComponentModel;
using System.Threading;


namespace HealthcheckApplication
{
    class checkParam
    {
        //Check Server Connection
        public static String checkServerConnection(string d, string p, ManagementScope scope)
        {
            //Number of tries if connection fails
            int retry = 5;
            tryconnection:

            string[] connList = new string[3];
            string text = "";
            String[,] data = new String[10, 10];
            
            //Connection Object
            ConnectionOptions connection = new ConnectionOptions();

            try
            {

                //Query Object to get whatever data is required
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PerfFormattedData_PerfDisk_PhysicalDisk");

                //Query the server
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                //For each in Query Object
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    String value = String.Empty;
                    //Property of Query Object
                    value = queryObj["AvgDiskQueueLength"].ToString();
                    Double threshold = Convert.ToDouble(queryObj["AvgDiskQueueLength"].ToString());
                }
            }
            catch (Exception e)
            {
                //Try till retry ! = 0(Try 5 times)
                if (retry != 0)
                {
                    Thread.Sleep(5000);
                    retry = retry - 1;
                    goto tryconnection;
                }
                else
                {
                    try
                    {
                        //Send mail
                        String subject = "UNABLE TO REACH SERVER";
                        String failreport = "Criticality : High <br >" + "Server IP Address: " + @d + "<br >" + e.Message;
                        ReportMailer mailer = new ReportMailer(0, 0, failreport, subject);
                        mailer.SendMail();
                    }
                    catch (Exception er)
                    {
                        Console.WriteLine(er.Message);
                    }
                }
            }
            return text;
        }

        
    //check Disk Queue Length
     public static String checkQueueLength(string d, string p, ManagementScope scope)
        {
            string[] connList = new string[3];
            string text = "";
            String[,] data = new String[10, 10];           

         //Query
         ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PerfFormattedData_PerfDisk_PhysicalDisk where Name = '_Total'");
          ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
        foreach (ManagementObject queryObj in searcher.Get())
        {
            String value = String.Empty;
            value = queryObj["AvgDiskQueueLength"].ToString();
            Double threshold = Convert.ToDouble(queryObj["AvgDiskQueueLength"].ToString());
            text = Environment.NewLine + text + DateTime.Now.ToString() + ";";
            text = text + value + ";";

            String newContent = String.Empty;
            String content = String.Empty;
            String file = @p + "wmiAverageDiskQueueLength.txt";
                
            //Write to File
            if (File.Exists(file))
            {
                content = File.ReadAllText(file);
                newContent = content + text;
                File.WriteAllText(file, newContent);
            }

            //Check Threshold and send mail
            if (threshold > 100.00)
            {
                try
                {
                    String subject = "HIGH QUEUE LENGTH";
                    String emailtext = "Criticality : High <br>" +                             
                                        "Server IP Address: " + @d + "<br >" +
                                    "Value: " + threshold  + "<br >";
                    ReportMailer mailer = new ReportMailer(0, 0, emailtext, subject);
                    mailer.SendMail();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }        
        return text;
    }





     //Disk Read Queue Length
     public static String checkReadQueueLength(string d, string p, ManagementScope scope)
     {
         string[] connList = new string[3];
         string text = "";
         String[,] data = new String[10, 10];

         //Query
         ObjectQuery query = new ObjectQuery
                     ("SELECT * FROM Win32_PerfFormattedData_PerfDisk_PhysicalDisk  where Name = '_Total'");
         ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, query);
         foreach (ManagementObject queryObj in searcher.Get())
         {
             String value = String.Empty;
             value = queryObj["AvgDiskReadQueueLength"].ToString();
             Double threshold = Convert.ToDouble(queryObj["AvgDiskReadQueueLength"].ToString());
             text = Environment.NewLine + text + DateTime.Now.ToString() + ";";
             text = text + value + ";";

             String newContent = String.Empty;
             String content = String.Empty;
             String file = @p + "wmiAverageDiskReadQueueLength.txt";

             //Write To file
             if (File.Exists(file))
             {
                 content = File.ReadAllText(file);
                 newContent = content + text;
                 File.WriteAllText(file, newContent);
             }

             //Check Threshold and send mail
             if (threshold > 100.00)
             {
                 try
                 {
                     String subject = "HIGH READ QUEUE LENGTH";
                     String emailtext = "Criticality : High <br>" +
                                       "Server IP Address: " + @d + "<br >" +
                                     "Value: " + threshold + "<br >";
                     ReportMailer mailer = new ReportMailer(0, 0, emailtext, subject);
                     mailer.SendMail();
                 }
                 catch (Exception e)
                 {
                     Console.WriteLine(e.Message);
                 }

             }
         }
         return text;
     }

    //Disk Write Queue Length
     public static String checkWriteQueueLength(string d, string p, ManagementScope scope)
     {
         string[] connList = new string[3];
         string text = "";
         String[,] data = new String[10, 10];

         ObjectQuery query = new ObjectQuery
                     ("SELECT * FROM Win32_PerfFormattedData_PerfDisk_PhysicalDisk  where Name = '_Total'");
         ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, query);
         foreach (ManagementObject queryObj in searcher.Get())
         {
             String value = String.Empty;
             value = queryObj["AvgDiskWriteQueueLength"].ToString();
             Double threshold = Convert.ToDouble(queryObj["AvgDiskWriteQueueLength"].ToString());
             text = Environment.NewLine + text + DateTime.Now.ToString() + ";";
             text = text + value + ";";

             //Write To file
             String newContent = String.Empty;
             String content = String.Empty;
             String file = @p + "wmiAverageDiskWriteQueueLength.txt";
             if (File.Exists(file))
             {
                 content = File.ReadAllText(file);
                 newContent = content + text;
                 File.WriteAllText(file, newContent);
             }


             //Check Threshold and send mail
             if (threshold > 100.00)
             {
                 try
                 {
                     String subject = "HIGH WRITE QUEUE LENGTH";
                     String emailtext = "Criticality : High <br>" +
                                       "Server IP Address: " + @d + "<br >" +
                                     "Value: " + threshold + "<br >";
                     ReportMailer mailer = new ReportMailer(0, 0, emailtext, subject);
                     mailer.SendMail();
                 }
                 catch (Exception e)
                 {
                     Console.WriteLine(e.Message);
                 }

             }
         }
         return text;
     }




        //Average Disk Sec per Read
    public static String checkAvgDiskSecRead(string d, string p, ManagementScope scope)
        {
            string[] connList = new string[3];
            string text = "";
            String[,] data = new String[10, 10];

            text = Environment.NewLine;

          ObjectQuery query = new ObjectQuery
              ("SELECT * FROM Win32_PerfFormattedData_PerfDisk_PhysicalDisk  where Name = '_Total'");
          ManagementObjectSearcher searcher =
                      new ManagementObjectSearcher(scope, query);
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        String value = String.Empty;
                        value = queryObj["AvgDisksecPerRead"].ToString();
                        Double diskread = Convert.ToDouble(value);
                        text = text + DateTime.Now.ToString() + ";";
                        text = text + value + ";" + Environment.NewLine;
          
                        //Check Threshold and Send Mail
                        if(diskread > 20)
                        {

                            try
                            {
                                String subject = "AVERAGE DISK SEC PER READ";                         
                                String emailtext = "CRITICALITY : HIGH <br > High Average Disk Read <br>" + "Value: " + diskread; 
                                ReportMailer mailer = new ReportMailer(0, 0, emailtext, subject);
                                mailer.SendMail();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                        }                      
                    }

                    //Write To file
                    String newContent = String.Empty;
                    String content = String.Empty;
                    String file = @p + "wmiAverageDiskSecRead.txt";
                    if (File.Exists(file))
                    {
                        content = File.ReadAllText(file);
                        newContent = content + text;
                        File.WriteAllText(file, newContent);
                    }
                return text;
            }
        

        //Average Disk Sec per Write
        public static String checkAvgDiskSecWrite(string d, string p, ManagementScope scope)
        {
            string[] connList = new string[3];
            string text = "";
            String[,] data = new String[10, 10];
            text = Environment.NewLine;
            
            ObjectQuery query = new ObjectQuery
                        ("SELECT * FROM Win32_PerfFormattedData_PerfDisk_PhysicalDisk  where Name = '_Total'");
            ManagementObjectSearcher searcher =
                            new ManagementObjectSearcher(scope, query);
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        String value = String.Empty;
                        value = queryObj["AvgDisksecPerWrite"].ToString();
                        Double diskwrite = Convert.ToDouble(value);
                        text = text + DateTime.Now.ToString() + ";";
                        text = text + value + ";";
                       
                        //Check Threshold and send mail
                        if (diskwrite > 20)
                        {
                            try
                            {
                                String subject = "HIGH AVERAGE DISK WRITE";
                                String emailtext = "Criticality : High <br>" + 
                                                    "Server IP Address : " + @d + "<br >"   +         
                                                    "Value: " + diskwrite + "<br>";
                                ReportMailer mailer = new ReportMailer(0, 0, emailtext,subject);
                                mailer.SendMail();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }

                    //Write To File
                    String newContent = String.Empty;
                    String content = String.Empty;
                    String file = @p + "wmiAverageDiskSecWrite.txt";

                    if (File.Exists(file))
                    {
                        content = File.ReadAllText(file);
                        newContent = content + text;
                        File.WriteAllText(file, newContent);
                    }
                return text;
            }
        

public static String checkPhysicalMemory(string d, string p, ManagementScope scope)
        {
            string[] connList = new string[3];
            string text = "";
            String[,] data = new String[10, 10];           
            
            ObjectQuery query2 = new ObjectQuery
                               ("SELECT * FROM Win32_ComputerSystem");
            ManagementObjectSearcher searcher2 =
                            new ManagementObjectSearcher(scope, query2);
            String totalMemory = String.Empty;
            double tm = 0.0;
            foreach (ManagementObject queryObj in searcher2.Get())
            {
                totalMemory = queryObj["TotalPhysicalMemory"].ToString();
                tm = Convert.ToDouble(Convert.ToDouble(totalMemory)/(1024.00*1024.00));
            }

           
            double fm = 0.0;
            ObjectQuery query = new ObjectQuery
                        ("SELECT * FROM Win32_PerfFormattedData_PerfOS_Memory");
    ManagementObjectSearcher searcher = 
                    new ManagementObjectSearcher(scope, query);
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        String value = String.Empty;
                        value = queryObj["AvailableMBytes"].ToString();
                        fm = Convert.ToDouble(value);
                        text = text + DateTime.Now.ToString() + ";";
                        double usedMem = tm - fm;
                        double freeMemperc = (fm/tm)*100;
                        text = text + fm + ";" + tm + ";" + usedMem + ";" + freeMemperc + ";";
                            
                        String newContent = String.Empty;
                        String content = String.Empty;
                        String file = @p + "wmiLogPhysicalMemory.txt";

                        if (File.Exists(file))
                        {
                            content = File.ReadAllText(file);
                            newContent = content + text +Environment.NewLine;
                            File.WriteAllText(file, newContent);
                        }


                        if((fm/tm*100) < 5)
                        {
                        try
                        {
                            String subject = "LOW PHYSICAL MEMORY";
                            String[] datatext = text.Split(';');
                            String emailtext = "Criticality : High <br>" + "IP Address:  " + @d + "<br >" + "Time:  " + datatext[0] + "<br>" + "FreeMemory in MB:  " + datatext[1] + "<br>" + "Total Memory in MB:  " + datatext[2] + "<br>" + "Used Memory in MB:  " + datatext[3] + "<br>" + "Free Memory Percentage:  " + datatext[4] + "<br>"; 
                            ReportMailer mailer = new ReportMailer(0, 0, emailtext, subject);
                            mailer.SendMail();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        }
                        break;
                    }

                return text;

            }
        

//CPU Utilization of Server
public static String checkCpuUtilization(string d, string p, ManagementScope scope)
        {
            int retry = 3;
            tryconnection:
            string[] connList = new string[3];
            string text = "";
            String[,] data = new String[10, 10];           
           
            
            ObjectQuery query = new ObjectQuery
                        ("SELECT * FROM Win32_PerfFormattedData_PerfOS_Processor where Name = '_Total'");
    ManagementObjectSearcher searcher = 
                    new ManagementObjectSearcher(scope, query);
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        String value = String.Empty;
                        value = queryObj["PercentProcessorTime"].ToString();
                        Double threshold = Convert.ToDouble(value);
                        text = Environment.NewLine + text + DateTime.Now.ToString() + ";";
                        text = text + value + ";";

                        if (threshold > 95)
                        {
                            if (retry != 0)
                            {
                                Thread.Sleep(5000);
                                retry = retry - 1;
                                goto tryconnection;
                            }
                            else
                            {
                                try
                                {
                                    String subject = "High Cpu Utilization";
                                    String[] datatext = text.Split(';');
                                    String emailtext = "Criticality : Low <br>" + "IP Address:  " + @d + "<br >" + "Time:  " + datatext[0] + "<br>" + "CPU Utilization:  " + datatext[1] + "<br>";
                                    ReportMailer mailer = new ReportMailer(0, 0, emailtext, subject);
                                    mailer.SendMail();
                                }

                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                        }
                      
                    }

                    //Write To File
                    String newContent = String.Empty;
                    String content = String.Empty;
                    String file = @p + "wmiCpuUtilization.txt";

                    if (File.Exists(file))
                    {
                        content = File.ReadAllText(file);
                        newContent = content + text;
                        File.WriteAllText(file, newContent);
                    }

                return text;

            }

public static String checkProcessorTime(string d, string p, ManagementScope scope)
{
    string[] connList = new string[3];
    string text = "";
    String[,] data = new String[10, 10];
    
    //Query
    ObjectQuery query = new ObjectQuery
                ("SELECT * FROM Win32_PerfFormattedData_PerfOS_Processor");
    ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, query);
    foreach (ManagementObject queryObj in searcher.Get())
    {
        String value = String.Empty;
        value = queryObj["PercentProcessorTime"].ToString();
        text = Environment.NewLine + text + DateTime.Now.ToString() + ";";
        text = text + value + ";";

        String newContent = String.Empty;
        String content = String.Empty;
        String file = @p + "wmiProcessorTime.txt";

        if (File.Exists(file))
        {
            content = File.ReadAllText(file);
            newContent = content + text;
            File.WriteAllText(file, newContent);
        }



        break;
    }

    return text;

}



//CpuUtilizationBySQLServer
public static String checkSqlserverCpuUtilization(string d, string p, ManagementScope scope)
{
    int retry = 3;
    tryconnection:
    string[] connList = new string[3];
    string text = "";
    String[,] data = new String[10, 10];

    //Query
        ObjectQuery query = new ObjectQuery
                    ("SELECT * FROM Win32_PerfFormattedData_PerfProc_Process where Name = 'sqlservr'");
        ManagementObjectSearcher searcher =
                   new ManagementObjectSearcher(scope, query);
        foreach (ManagementObject queryObj in searcher.Get())
        {
            String value = String.Empty;
            value = queryObj["PercentProcessorTime"].ToString();
            Double threshold = Convert.ToDouble(queryObj["PercentProcessorTime"].ToString());
            text = Environment.NewLine + DateTime.Now.ToString() + ";";
            text = text + value + ";";

            //Check Threshold and send mail
            if (threshold > 95)
            {
                if (retry != 0)
                {
                    Thread.Sleep(5000);
                    retry = retry - 1;
                    goto tryconnection;
                }
                else
                {

                    try
                    {
                        String subject = "High Cpu Utilization by Sql Server";
                        String emailtext = "Criticality : Low <br>" +
                                          "Server IP Address: " + @d + "<br >" +
                                        "Value: " + threshold + "<br> ";
                        ReportMailer mailer = new ReportMailer(0, 0, emailtext, subject);
                        mailer.SendMail();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }


            //Write To File
             String newContent = String.Empty;
            String content = String.Empty;
            String file = @p + "wmiSqlserverCpuUtilization.txt";
            if (File.Exists(file))
            {
                content = File.ReadAllText(file);
                newContent = content + text;
                File.WriteAllText(file, newContent);
            }
    return text;


}

public static String checkSqlserverMemory(string d, string p, ManagementScope scope)
{
    string[] connList = new string[3];
    string text = "";
    String[,] data = new String[10, 10];


    //Query
        ObjectQuery query2 = new ObjectQuery
                               ("SELECT * FROM Win32_ComputerSystem");
        ManagementObjectSearcher searcher2 =
                        new ManagementObjectSearcher(scope, query2);
        String totalMemory = String.Empty;
        double tm = 0.0;
        foreach (ManagementObject queryObj in searcher2.Get())
        {
            totalMemory = queryObj["TotalPhysicalMemory"].ToString();
            tm = Convert.ToDouble(Convert.ToDouble(totalMemory) / (1024.00 * 1024.00));
        }

     
    
    //Query2
    ObjectQuery query = new ObjectQuery
                    ("SELECT * FROM Win32_PerfRawData_PerfProc_Process where Name = 'sqlservr'");
    ManagementObjectSearcher searcher =
                   new ManagementObjectSearcher(scope, query);
        foreach (ManagementObject queryObj in searcher.Get())
        {
            String value = String.Empty;
            value = queryObj["WorkingSetPrivate"].ToString();
            Double threshold = Convert.ToDouble((Convert.ToDouble(queryObj["WorkingSetPrivate"].ToString()))/(1024.00*1024.00));
            text = Environment.NewLine + text + DateTime.Now.ToString() + ";";
            text = text + value + ";";

            //Write To File
            String newContent = String.Empty;
            String content = String.Empty;
            String file = @p + "wmiSqlserverMemory.txt";
            if (File.Exists(file))
            {
                content = File.ReadAllText(file);
                newContent = content + text;
                File.WriteAllText(file, newContent);
            }

            //Check Threshold and send mail
            if (((threshold / tm) * 100 ) >= 95)
            {
                try
                {
                    String subject = "HIGH MEMORY UTILIZATION BY SQL SERVER";
                    String emailtext = "Criticality : High <br>" +
                                      "Server IP Address: " + @d + "<br >" +
                                         "Value: " + threshold + "<br>" +
                                         "Percentage : " + ((threshold / tm) * 100) + "<br>";
                    ReportMailer mailer = new ReportMailer(0, 0, emailtext, subject);
                    mailer.SendMail();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    return text;


}


public static String checkAllProcessMemory(string d, string p, ManagementScope scope)
{
    string[] connList = new string[3];
    string text = "";
    String[,] data = new String[10, 10];


    //Query
    ObjectQuery query = new ObjectQuery
                    ("SELECT * FROM Win32_PerfRawData_PerfProc_Process");
    ManagementObjectSearcher searcher =
               new ManagementObjectSearcher(scope, query);
    foreach (ManagementObject queryObj in searcher.Get())
    {
        String value1 = String.Empty;
        String value2 = String.Empty;
        
        value1 = queryObj["Name"].ToString();
        value2 = queryObj["WorkingSetPrivate"].ToString();
        
        Double threshold = Convert.ToDouble((Convert.ToDouble(queryObj["WorkingSetPrivate"].ToString())) / (1024.00 * 1024.00));
        text = text + DateTime.Now.ToString() + ";";
        text = text + value1 + ";" + value2 + ";" + Environment.NewLine;
    }
        
        //Write to File    
        String newContent = String.Empty;
        String content = String.Empty;
        String file = @p + "wmiProcessMemory.txt";
        if (File.Exists(file))
        {
            content = File.ReadAllText(file);
            newContent = content + text;
            File.WriteAllText(file, newContent);
        }
    
    return text;
}


public static String checkAllProcessCpuUtilization(string d, string p, ManagementScope scope)
{
    string[] connList = new string[3];
    string text = "";
    String[,] data = new String[10, 10];



    //Query
    ObjectQuery query = new ObjectQuery
                    ("SELECT * FROM Win32_PerfFormattedData_PerfProc_Process");
    ManagementObjectSearcher searcher =
               new ManagementObjectSearcher(scope, query);
    foreach (ManagementObject queryObj in searcher.Get())
    {
        String value1 = String.Empty;
        String value2 = String.Empty;

        value1 = queryObj["Name"].ToString();
        value2 = queryObj["PercentProcessorTime"].ToString();

        Double threshold = Convert.ToDouble((Convert.ToDouble(queryObj["PercentProcessorTime"].ToString())));
        text = text + DateTime.Now.ToString() + ";";
        text = text + value1 + ";" + value2 + ";" + Environment.NewLine;
    }

    //Write To File
    String newContent = String.Empty;
    String content = String.Empty;
    String file = @p + "wmiProcessCpuUtilization.txt";
    if (File.Exists(file))
    {
        content = File.ReadAllText(file);
        newContent = content + text;
        File.WriteAllText(file, newContent);
    }

    return text;
}



        //Not Required
public static String checkPageFile(string d, string p, ManagementScope scope)
{
    Console.WriteLine("Checking PageFile");
    string[] connList = new string[3];
    string text = "";
    String[,] data = new String[10, 10];
 

    ObjectQuery query = new ObjectQuery
                    ("SELECT * FROM Win32_PageFileUsage");


    ManagementObjectSearcher searcher =
               new ManagementObjectSearcher(scope, query);

    foreach (ManagementObject queryObj in searcher.Get())
    {
        String value1 = String.Empty;
        String value2 = String.Empty;
        String value3 = String.Empty;
        
        value1 = queryObj["Name"].ToString();
        value2 = queryObj["CurrentUsage"].ToString();
        value3 = queryObj["AllocatedBaseSize"].ToString();

        //Double threshold = Convert.ToDouble((Convert.ToDouble(queryObj["Win32_PageFileUsage"].ToString())));
        text = text + DateTime.Now.ToString() + ";";
        text = text + value1 + ";" + value2 + ";" + value3 + ";"+ Environment.NewLine;
    }
    String newContent = String.Empty;
    String content = String.Empty;
    String file = @p + "wmiPageFile.txt";
    //Console.WriteLine(file);

    if (File.Exists(file))
    {
        content = File.ReadAllText(file);
        //Console.WriteLine("Current content of file:");
        //Console.WriteLine(content);
        newContent = content + text;
        File.WriteAllText(file, newContent);
    }

    return text;
}



















    
    }
       



       





    }


            
            //////Console.WriteLine(text);
    
//            File.AppendAllText("D:\\data.csv", Environment.NewLine + text);

            //////Console.WriteLine("Sending E-Mail at this time : {0}", DateTime.Now);
            //ReportMailer mailer = new ReportMailer(0, 0, text);
            //mailer.SendMail();
            //////Console.WriteLine("Mail sent");
     
          
            //for (int i = 0; i < queriesList.Count; i++)
            //{
            //    text = text + "<tr >";
            //    for (int j = 0; j < 7; j++)
            //    {
            //        text = text + "<td>";
            //        text = text + data[i,j];
            //        text = text + "</td>";
            //    }
            //    text = text + "</tr>";
            //}
            
            //////Console.WriteLine(text);
  
        
    


