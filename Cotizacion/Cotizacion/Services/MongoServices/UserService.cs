using Cotizacion.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Cotizacion.MongoServices
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _Users;
        public UserService(IMongoConnectionFactory IDbConnectionFactory)
        {
            IMongoDatabase database = IDbConnectionFactory.MongoClient();
            _Users = database.GetCollection<User>("Users");
        }
        public List<User> Get()
        {
            return _Users.Find(User => true).ToList();
        }

        public User Get(string id)
        {
            return _Users.Find<User>(User => User.Id == id).FirstOrDefault();
        }

        public User Create(User User)
        {
            _Users.InsertOne(User);
            return User;
        }

        public void Update(string id, User UserIn)
        {
            _Users.ReplaceOne(User => User.Id == id, UserIn);
        }

        public void Remove(User UserIn)
        {
            _Users.DeleteOne(User => User.Id == UserIn.Id);
        }

        public void Remove(string id)
        {
            _Users.DeleteOne(User => User.Id == id);
        }
    }
}
