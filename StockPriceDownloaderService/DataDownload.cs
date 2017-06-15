using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPriceDownloaderService
{
    public class DataDownload
    {
        static bool isLTDTChanged = false; //LTDT = LastTradeDateTime
        static CompanyPriceInfo[] tmpArr = new CompanyPriceInfo[5];
        static CompanyPriceInfo cpi = new CompanyPriceInfo();
        static string[] Companies = new string[5] { "BMW", "BAYN", "LHA", "SIE", "DBK" };
        static string json;

        public static void Download()
        {
            for (int i = 0; i < Companies.Length; i++)
            {
                using (var webClient = new System.Net.WebClient())
                {
                    string url = BuildURL(Companies[i]); ;
                    json = webClient.DownloadString(url);
                    json = json.Replace("//", "");
                    JArray arr = JArray.Parse(json);
                    foreach (JToken jtk in arr)
                    {
                        cpi.Id_Company = GetId_Company(jtk);
                        cpi.Id_Exchange = 1;
                        cpi.Price = (double)jtk.SelectToken("l");
                        TimeSpan offset = new TimeSpan(1, 0, 0);
                        cpi.LastTradeDateTime = ((DateTime)jtk.SelectToken("lt_dts"));
                        cpi.LastTradeDateTime = cpi.LastTradeDateTime + offset;
                    }

                    isLTDTChanged = CompareLastTradeDateTime(tmpArr[i].LastTradeDateTime, cpi.LastTradeDateTime);
                    //Console.WriteLine("Checks if LastTradeDateTime changed.");
                    if (isLTDTChanged == true)
                    {
                        //Console.WriteLine("And it changed.");
                        tmpArr[i].Price = cpi.Price;
                        tmpArr[i].LastTradeDateTime = cpi.LastTradeDateTime;
                        DBOperations.Insert(cpi);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        private static string BuildURL(string ticker)
        {
            return "http://www.google.com/finance/info?infotype=infoquoteall&q=FRA:" + ticker;
        }

        private static int GetId_Company(JToken jtk)
        {
            int result = 0;
            switch (jtk.SelectToken("t").ToString())
            {
                case "BMW":
                    result = 1;
                    break;
                case "BAYN":
                    result = 2;
                    break;
                case "LHA":
                    result = 3;
                    break;
                case "SIE":
                    result = 4;
                    break;
                case "DBK":
                    result = 5;
                    break;
            }
            return result;
        }

        private static bool CompareLastTradeDateTime(DateTime tmp, DateTime cpi)
        {
            if (tmp != cpi || tmp == default(DateTime))
            {
                return true;
            }
            return false;
        }

        public static void InitializeTmpArray()
        {
            for (int i = 0; i < tmpArr.Length; i++)
            {
                tmpArr[i] = new CompanyPriceInfo();
            }
        }

        public static void PopulateTmpArr()
        {
            for (int i = 0; i < tmpArr.Length; i++)
            {
                using (var webClient = new System.Net.WebClient())
                {
                    string url = BuildURL(Companies[i]); ;
                    json = webClient.DownloadString(url);
                    json = json.Replace("//", "");
                    JArray arr = JArray.Parse(json);
                    foreach (JToken jtk in arr)
                    {
                        tmpArr[i].Id_Company = GetId_Company(jtk);
                        tmpArr[i].Id_Exchange = 1;
                        tmpArr[i].Price = (double)jtk.SelectToken("l");
                        tmpArr[i].LastTradeDateTime = ((DateTime)jtk.SelectToken("lt_dts"));
                        TimeSpan offset = new TimeSpan(1, 0, 0);
                        tmpArr[i].LastTradeDateTime = tmpArr[i].LastTradeDateTime + offset;
                    }
                }
            }
        }
    }
}
