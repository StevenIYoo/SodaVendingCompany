using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StevenYoo_SodaVendingCompany.Models;

namespace StevenYoo_SodaVendingCompany.Data
{
    public class ConsumerVendingMachineData : IConsumerVendingMachineData
    {
        private IDatabaseRepository _databaseRepository;
        private SqlConnection connection;
        private SqlCommand command;

        public ConsumerVendingMachineData(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
            connection = _databaseRepository.OpenConnection();
        }

        public List<Soda> SelectSodasForSale()
        {
            List<Soda> sodas = new List<Soda>();

            command = new SqlCommand("SELECT SodaName, SodaCount, SodaPrice, SodaImageUrl "
                                    + " FROM dbo.Soda As Soda "
                                    + " INNER JOIN dbo.SodaPrice AS Price "
                                    + " ON Soda.SodaID = Price.SodaID "
                                    + " AND Price.SodaPriceEndDate is null ", connection);

            try
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var soda = new Soda();

                    //soda.Id = (Int32)reader["SodaID"];
                    soda.Name = reader["SodaName"].ToString();
                    soda.SodaCount = (Int32)reader["SodaCount"];
                    soda.Price = (Decimal)reader["SodaPrice"];
                    soda.PictureUrl = reader["SodaImageUrl"].ToString();

                    sodas.Add(soda);
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

            return sodas;
        }

        public void DecrementSodaCountByOne(string sodaName)
        {

            using (var transaction = connection.BeginTransaction("VendingTransaction"))
            {
                var InsertTransactionHistoryCommand = new SqlCommand(
                  " INSERT INTO dbo.SodaTransactionHistory(SodaID, SodaPriceID, TransactionDate) "
                + " SELECT Soda.SodaID, Price.SodaPriceID, TransactionDate = GETDATE() "
                + " FROM dbo.Soda AS Soda "
                + " INNER JOIN dbo.SodaPrice AS Price "
                + " ON Soda.SodaID = Price.SodaID "
                + " AND Price.SodaPriceEndDate is null "
                + " WHERE Soda.SodaName = @SodaName ", connection, transaction);

                InsertTransactionHistoryCommand.Parameters.Add(new SqlParameter("SodaName", sodaName));

                var UpdateCountCommand = new SqlCommand(
                      "UPDATE dbo.Soda "
                    + " SET SodaCount = SodaCount - 1 "
                    + " WHERE SodaName = @SodaName", connection, transaction);

                UpdateCountCommand.Parameters.Add(new SqlParameter("SodaName", sodaName));

                try
                {
                    InsertTransactionHistoryCommand.ExecuteNonQuery();
                    UpdateCountCommand.ExecuteScalar();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    _databaseRepository.CloseConnection(connection);
                }
            }

            return;
        }

        public int CheckRemainingSodaCount(string sodaName)
        {
            int sodaCount = 0;

            command = new SqlCommand("SELECT SodaCount FROM dbo.Soda WHERE SodaName=@SodaName", connection);

            command.Parameters.Add(new SqlParameter("SodaName", sodaName));

            try
            {
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    sodaCount = (Int32)reader["SodaCount"];
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

            return sodaCount;
        }
    }
}
