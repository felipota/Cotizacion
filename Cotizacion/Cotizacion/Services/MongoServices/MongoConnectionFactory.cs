using Cotizacion.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cotizacion.MongoServices
{
    public class MongoConnectionFactory : IMongoConnectionFactory
    {
        private readonly IOptions<MongoConnectionStrings> _connectionString;
        public MongoConnectionFactory(IOptions<MongoConnectionStrings> connectionString)
        {
            _connectionString = connectionString;
        }

        public IMongoDatabase MongoClient()
        {
            var conn = new MongoClient(_connectionString.Value.CotizacionDB);
            var database = conn.GetDatabase(_connectionString.Value.Database);
            return database;
        }

    }
}
