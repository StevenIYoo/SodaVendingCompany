using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Data
{
    public class CreateSodaData
    {
        private IDatabaseRepository _databaseRepository;
        private SqlConnection connection;
        private SqlCommand command;

        public CreateSodaData(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
            connection = _databaseRepository.OpenConnection();
        }

        public void InsertNewSoda(string sodaName, int insertAmount, decimal sodaPrice, string sodaUrl)
        {
            //

            using (var transaction = connection.BeginTransaction("NewSoda"))
            {
                var InsertNewSoda = new SqlCommand(
                  " INSERT INTO dbo.Soda(SodaName, SodaCount, SodaImageUrl) "
                + " VALUES(@SodaName, @InsertAmount, @SodaUrl) ", connection, transaction);

                InsertNewSoda.Parameters.Add(new SqlParameter("SodaName", sodaName));
                InsertNewSoda.Parameters.Add(new SqlParameter("InsertAmount", insertAmount));
                InsertNewSoda.Parameters.Add(new SqlParameter("SodaUrl", sodaUrl));

                var InsertNewSodaPrice = new SqlCommand(
                  " INSERT INTO dbo.SodaPrice(SodaPrice, SodaPriceStartDate, SodaID) "
                + " SELECT @SodaPrice, GETDATE(), Soda.SodaID "
                + " FROM dbo.Soda Soda "
                + " WHERE Soda.SodaName = @SodaName ", connection, transaction);

                InsertNewSodaPrice.Parameters.Add(new SqlParameter("SodaName", sodaName));
                InsertNewSodaPrice.Parameters.Add(new SqlParameter("SodaPrice", sodaPrice));

                try
                {
                    InsertNewSoda.ExecuteNonQuery();
                    InsertNewSodaPrice.ExecuteNonQuery();
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
        }
    }
}
