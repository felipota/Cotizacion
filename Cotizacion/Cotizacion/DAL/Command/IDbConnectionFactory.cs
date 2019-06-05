using System.Data;

namespace Cotizacion.DAL.Command
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
