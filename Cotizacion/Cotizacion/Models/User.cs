﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cotizacion.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre")]
        public string nombre { get; set; }

        [BsonElement("apellido")]
        public string apellido { get; set; }

        [BsonElement("email")]
        public string email { get; set; }

        [BsonElement("password")]
        public string password { get; set; }
    }
}
