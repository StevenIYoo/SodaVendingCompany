using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using StevenYoo_SodaVendingCompany.Models;

namespace StevenYoo_SodaVendingCompany.Data
{

    public class ReportVendingMachineData : IReportVendingMachineData
    {
        private IDatabaseRepository _databaseRepository;
        private SqlConnection connection;
        private SqlCommand command;

        public ReportVendingMachineData(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
            connection = _databaseRepository.OpenConnection();
        }

        public List<SodaReportTransaction> GetReport()
        {
            var transactionList = new List<SodaReportTransaction>();

            command = new SqlCommand(
                  " SELECT history.TransactionDate, soda.SodaName, price.SodaPrice "
                + " FROM dbo.SodaTransactionHistory history "
                + " INNER JOIN dbo.Soda soda "
                + " ON history.SodaID = soda.SodaID "
                + " INNER JOIN dbo.SodaPrice price "
                + " ON history.SodaPriceID = price.SodaPriceID ", connection);

            try
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var transaction = new SodaReportTransaction();

                    transaction.SodaName = reader["SodaName"].ToString();
                    transaction.SodaPrice = (Decimal)reader["SodaPrice"];
                    transaction.TransactionDate = (DateTime)reader["TransactionDate"];

                    transactionList.Add(transaction);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _databaseRepository.CloseConnection(connection);
            }

            return transactionList;

        }
    }
}
