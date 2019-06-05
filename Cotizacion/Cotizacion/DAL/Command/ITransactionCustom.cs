using System;
using System.Data;

namespace Cotizacion.DAL.Command
{
    public interface ITransactionCustom
    {
        int MakeTranssaction<T>(Func<T, IDbConnection, IDbTransaction, int> callback, T valueEntity);
    }
}
