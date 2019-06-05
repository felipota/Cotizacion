#region Using
using Cotizacion.BL;
using Cotizacion.DAL.Command;
using Cotizacion.Models;
using COTIZACION.Core.BLDep.Cotizacion;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
#endregion


namespace COTIZACION.Core.Controllers.Cotizacion
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        #region Properties
        private readonly IUsuarioBL usuarioBL;
        private readonly ISaveUsuario _save;
        #endregion

        public UsuariosController(IUsuarioBL UsuarioBL, ISaveUsuario transactionCustom)
        {
            usuarioBL = UsuarioBL;
            _save = transactionCustom;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> Get()
        {
            List<Usuario>  usuarios = usuarioBL.Read();
            return usuarios;
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUsuario")]
        public ActionResult<Usuario> Get(string id)
        {
            Usuario usuario = usuarioBL.Read(int.Parse(id));

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<Usuario> Create(Usuario user)
        {
            int usuarioid = _save.Insert(user);
            user.id = usuarioid;
            return CreatedAtRoute("GetUsuario", new { id = usuarioid.ToString() }, user);
        }

        // PUT: api/Users/5
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Usuario user)
        {
            Usuario usuario = usuarioBL.Read(int.Parse(id));

            if (usuario == null)
            {
                return NotFound();
            }

            int usuarioid = _save.Update(user);

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            Usuario user = usuarioBL.Read(int.Parse(id));

            if (user == null)
            {
                return NotFound();
            }

            usuarioBL.Delete(user.id);

            return NoContent();
        }
    }
}
