using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPriceDownloaderService
{
    public class DBConnection
    {
        public MySqlConnection connection;
        private string server;
        private string database;
        private string username;
        private string password;

        public DBConnection()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "";
            database = "";
            username = "";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            connection.Open();
        }

        public bool CloseConnection()
        {
            connection.Close();
        }
    }
}
