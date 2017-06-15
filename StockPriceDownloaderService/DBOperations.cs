using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPriceDownloaderService
{
    public class DBOperations
    {
        public static void Insert(CompanyPriceInfo cpi)
        {
            DBConnection dbc = new DBConnection();
            dbc.OpenConnection();
            string query = "INSERT INTO Data (Price, LastTradeDateTime, Id_Company, Id_Exchange) VALUES(@price, @lasttradedatetime, @idcompany, @idexchange)";
            MySqlCommand cmdInsert = new MySqlCommand(query, dbc.connection);
            cmdInsert.Parameters.AddWithValue("@price", cpi.Price);
            cmdInsert.Parameters.AddWithValue("@lasttradedatetime", cpi.LastTradeDateTime);
            cmdInsert.Parameters.AddWithValue("@idcompany", cpi.Id_Company);
            cmdInsert.Parameters.AddWithValue("@idexchange", cpi.Id_Exchange);
            cmdInsert.ExecuteNonQuery();
            query = "UPDATE Pret SET PretFRA = @price, Dt = @lasttradedatetime WHERE Id_Company = @idcompany";
            MySqlCommand cmdUpdate = new MySqlCommand(query, dbc.connection);
            cmdUpdate.Parameters.AddWithValue("@price", cpi.Price);
            cmdUpdate.Parameters.AddWithValue("lasttradedatetime", cpi.LastTradeDateTime);
            cmdUpdate.Parameters.AddWithValue("@idcompany", cpi.Id_Company);
            cmdUpdate.ExecuteNonQuery();
            dbc.CloseConnection();
        }
    }
}
