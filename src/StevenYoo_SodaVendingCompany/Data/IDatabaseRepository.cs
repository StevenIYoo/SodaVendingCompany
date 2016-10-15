using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Data
{
    public interface IDatabaseRepository
    {
        SqlConnection OpenConnection();
        void CloseConnection(SqlConnection connection);
    }
}
