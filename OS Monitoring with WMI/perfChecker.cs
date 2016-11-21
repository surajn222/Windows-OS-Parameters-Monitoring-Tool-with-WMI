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



namespace HealthcheckApplication
{
    class perfChecker
    {

        private DataTable dataTable;

        public static String perfCheckMethod(String svstring)
        {
            Dictionary<string, string> queriesList = new Dictionary<string, string>();          
            Dictionary<string, Int64> dbMonitorParamDict = new Dictionary<string, Int64>();
           string[]  connList = new string[3];
     
            foreach (var item in ConfigurationManager.AppSettings.AllKeys)
            {
                if (item.StartsWith("QR"))
                {
                    string query = ConfigurationManager.AppSettings[item];
                    Console.WriteLine(item + "   " + query);
                    queriesList.Add(item, query);
                }
            }


            int arrcount = 0;
             foreach (var item in ConfigurationManager.AppSettings.AllKeys)
            {
                if (item.StartsWith("SV"))
                {
                    
                    string query = ConfigurationManager.AppSettings[item];
                    Console.ReadLine();
                    Console.WriteLine("Query Check2");
                    Console.WriteLine(query);
                    string[] sv = query.Split(',');
                    string s = "Data Source=" + sv[1] + ";Initial Catalog=" + sv[2] + ";Integrated Security=sspi";
                    Console.WriteLine(item + "   " + s);
                    connList[arrcount]=s;
                    arrcount = arrcount + 1;
                }
                
            }



            string text = "";
            string[] ar2 = (ConfigurationManager.AppSettings["QR_target_Memory"]).Split(',');

            Console.WriteLine("Queries Count" + queriesList.Count + "," + ar2.Length);
            String[,] data = new String[queriesList.Count, 7];
            int start = 0;

            foreach (var item in queriesList)
            {
                Console.WriteLine("Querieslust");
                Console.WriteLine(queriesList.Count);
                string[] values = (item.Value).Split(',');
                Console.WriteLine(values[1]);
                Console.ReadLine();
                data[start, 0] = values[0];
                data[start, 1] = values[2];
            
                int column = 2;
                foreach (var connect in connList)
                {
                    Console.WriteLine(connList.Length);

                    Console.WriteLine("Server String");
                    Console.WriteLine(connect.ToString());
                    string connectionstring =connect.ToString();
                    Console.ReadLine();
                    SqlConnection connsql = new SqlConnection(@connectionstring);
                    try { connsql.Open(); }
                    catch (Exception e) { Console.WriteLine(e.Message); Console.ReadLine(); }
                    Console.WriteLine(connsql.State);

                    using (SqlDataAdapter a = new SqlDataAdapter(@values[1], connsql))
                    {
                        try
                        {
                            DataTable t = new DataTable();
                            a.Fill(t);

                            foreach (DataColumn col in t.Columns)
                            {

                                foreach (DataRow row in t.Rows)
                                {

                                    data[start, column] = row[col.ColumnName].ToString();
                                    Console.WriteLine("Printing data array");
                                    Console.WriteLine(row[col.ColumnName].ToString());
                                    Console.WriteLine(data[start, column]);
                                    Console.ReadLine();

                                    //text = text + "\n" + item.Key + " : " + row[col.ColumnName].ToString();
                                }
                            }

                        }

                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }                
                    }
                  
                   // command.Dispose();

                    Console.WriteLine("Increase the column");
                    column = column + 1;

                }
                Console.WriteLine("Increase the Start");
                data[start, 5] = values[3];
                data[start, 6] = values[4];         
                start = start + 1;
            }

            
            //Console.WriteLine(text);
            Console.ReadLine();

//            File.AppendAllText("D:\\data.csv", Environment.NewLine + text);

            //Console.WriteLine("Sending E-Mail at this time : {0}", DateTime.Now);
            //ReportMailer mailer = new ReportMailer(0, 0, text);
            //mailer.SendMail();
            //Console.WriteLine("Mail sent");
     
          
            for (int i = 0; i < queriesList.Count; i++)
            {
                text = text + "<tr >";
                for (int j = 0; j < 7; j++)
                {
                    text = text + "<td>";
                    text = text + data[i,j];
                    text = text + "</td>";
                }
                text = text + "</tr>";
            }
            
           


            Console.WriteLine(text);
  
            return text;
        }

        }
    }

