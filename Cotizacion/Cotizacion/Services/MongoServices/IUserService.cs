using Cotizacion.Models;
using System.Collections.Generic;

namespace Cotizacion.MongoServices
{
    public interface IUserService
    {
        List<User> Get();

        User Get(string id);

        User Create(User User);

        void Update(string id, User UserIn);

        void Remove(User UserIn);

        void Remove(string id);

    }
}
