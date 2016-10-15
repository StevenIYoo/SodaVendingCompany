using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace StevenYoo_SodaVendingCompany.Data
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private IConfigurationRoot _config;
        private SqlConnection connection;

        public DatabaseRepository(IConfigurationRoot config)
        {
            _config = config;
        }

        public SqlConnection OpenConnection()
        {
            connection = new SqlConnection();

            try
            {
                connection.ConnectionString = _config["Data:VendingCompanyConnection"];
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

            return connection;
        }

        public void CloseConnection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
