using Cotizacion.Models;
using Cotizacion.MongoServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cotizacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _IUserService;

        public UsersController(IUserService IUserService)
        {
            _IUserService = IUserService;
        }
        // GET: api/Users
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return _IUserService.Get();
        }

        // GET: api/Users/5
        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            var user = _IUserService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            _IUserService.Create(user);

            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }

        // PUT: api/Users/5
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, User user)
        {
            User userv = _IUserService.Get(id);

            if (userv == null)
            {
                return NotFound();
            }

            _IUserService.Update(id, user);

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            User user = _IUserService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _IUserService.Remove(user.Id);

            return NoContent();
        }
    }
}
