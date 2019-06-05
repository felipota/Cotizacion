using System;
using System.Data;

namespace Cotizacion.DAL.Command
{
    public class TransactionCustom : ITransactionCustom
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public TransactionCustom(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public int MakeTranssaction<T>(Func<T, IDbConnection, IDbTransaction, int> callback, T valueEntity)
        {
            int response = 0;
            using (IDbConnection dbConnection = _dbConnectionFactory.CreateConnection())
            {
                using (IDbTransaction transaction = dbConnection.BeginTransaction())
                {
                    response = callback(valueEntity, dbConnection, transaction);
                }
                if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }

                dbConnection.Dispose();
            }

            return response;
        }

    }
}
