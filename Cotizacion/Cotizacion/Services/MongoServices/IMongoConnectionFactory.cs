using MongoDB.Driver;

namespace Cotizacion.MongoServices
{
    public interface IMongoConnectionFactory
    {
        IMongoDatabase MongoClient();
    }
}
