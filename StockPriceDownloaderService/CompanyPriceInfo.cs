using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPriceDownloaderService
{
    public class CompanyPriceInfo
    {
        public double Price { get; set; }
        public DateTime LastTradeDateTime { get; set; }
        public int Id_Company { get; set; }
        public int Id_Exchange { get; set; }

        public CompanyPriceInfo(double Price, DateTime LastTradeDateTime, int Id_Company, int Id_Exchange)
        {
            this.Price = Price;
            this.LastTradeDateTime = LastTradeDateTime;
            this.Id_Company = Id_Company;
            this.Id_Exchange = Id_Exchange;
        }

        public CompanyPriceInfo() { }

    }
}
