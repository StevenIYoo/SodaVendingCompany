using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Data
{

    public class AdminVendingMachineData : IAdminVendingMachineData
    {
        private IDatabaseRepository _databaseRepository;
        private SqlConnection connection;
        private SqlCommand command;

        public AdminVendingMachineData(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
            connection = _databaseRepository.OpenConnection();
        }

        public int BulkUpdateSodaCount(string sodaName, int updateAmount)
        {
            int sodaCount = 0;

            var BulkUpdateSodaCommand = new SqlCommand(
               " UPDATE dbo.Soda "
             + " SET SodaCount = SodaCount - @UpdateAmount "
             + " WHERE SodaName = @SodaName ", connection);

            BulkUpdateSodaCommand.Parameters.Add(new SqlParameter("SodaName", sodaName));
            BulkUpdateSodaCommand.Parameters.Add(new SqlParameter("UpdateAmount", updateAmount));

            //
            var GetSodaCountCommand = new SqlCommand(
                  " SELECT SodaCount "
                + " FROM dbo.Soda "
                + " WHERE SodaName = @SodaName ", connection);

            GetSodaCountCommand.Parameters.Add(new SqlParameter("SodaName", sodaName));

            try
            {
                BulkUpdateSodaCommand.ExecuteNonQuery();

                var reader = GetSodaCountCommand.ExecuteReader();

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

        public int DecrementSodaCount(string sodaName)
        {
            int sodaCount = 0;

            var DecrementSodaCommand = new SqlCommand(
               " UPDATE dbo.Soda "
             + " SET SodaCount = SodaCount - 1 "
             + " WHERE SodaName = @SodaName ", connection);

            DecrementSodaCommand.Parameters.Add(new SqlParameter("SodaName", sodaName));

            //
            var GetSodaCountCommand = new SqlCommand(
                  " SELECT SodaCount "
                + " FROM dbo.Soda "
                + " WHERE SodaName = @SodaName ", connection);

            GetSodaCountCommand.Parameters.Add(new SqlParameter("SodaName", sodaName));

            try
            {
                DecrementSodaCommand.ExecuteNonQuery();

                var reader = GetSodaCountCommand.ExecuteReader();

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

        public int IncrementSodaCount(string sodaName)
        {
            int sodaCount = 0;

            var IncrementSodaCommand = new SqlCommand(
               " UPDATE dbo.Soda "
             + " SET SodaCount = SodaCount + 1 "
             + " WHERE SodaName = @SodaName ", connection);

            IncrementSodaCommand.Parameters.Add(new SqlParameter("SodaName", sodaName));


            var GetSodaCountCommand = new SqlCommand(
                  " SELECT SodaCount "
                + " FROM dbo.Soda "
                + " WHERE SodaName = @SodaName ", connection);

            GetSodaCountCommand.Parameters.Add(new SqlParameter("SodaName", sodaName));

            try
            {
                IncrementSodaCommand.ExecuteNonQuery();

                SqlDataReader reader = GetSodaCountCommand.ExecuteReader();

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

        public void ChangeSodaCost(string sodaName, decimal newPrice)
        {
            using (var transaction = connection.BeginTransaction("ChangeSodaCost"))
            {
                var RemoveOldSodaCostCommand = new SqlCommand(
                  "  UPDATE price "
                + "  SET SodaPriceEndDate = GETDATE() "
                + "  FROM dbo.SodaPrice As price "
                + "  INNER JOIN dbo.Soda As soda "
                + "  ON price.SodaID = soda.SodaID "
                + "  AND price.SodaPriceEndDate is null "
                + "  WHERE soda.SodaName = @SodaName ", connection, transaction);

                RemoveOldSodaCostCommand.Parameters.Add(new SqlParameter("SodaName", sodaName));


                var InsertNewPriceCommand = new SqlCommand(
                  "    INSERT INTO dbo.SodaPrice(SodaPrice, SodaID, SodaPriceStartDate) "
                + "    SELECT @SodaPrice, soda.SodaID, GETDATE() "
                + "    FROM dbo.Soda soda "
                + "    WHERE soda.SodaName = @sodaName ", connection, transaction);

                InsertNewPriceCommand.Parameters.Add(new SqlParameter("SodaName", sodaName));
                InsertNewPriceCommand.Parameters.Add(new SqlParameter("SodaPrice", newPrice));

                try
                {
                    RemoveOldSodaCostCommand.ExecuteScalar();
                    InsertNewPriceCommand.ExecuteNonQuery();
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
    }
}
