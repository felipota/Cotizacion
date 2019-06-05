using Cotizacion.BL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cotizacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionController : ControllerBase
    {
        private readonly ICotizacionFac _clientFactory;
        public CotizacionController(ICotizacionFac clientFactory)
        {
            _clientFactory = clientFactory;
        }
        [HttpGet("{id}")]
        public ActionResult<List<string>>Get(string id)
        {
            List<string> response = _clientFactory.GetCotizacion(id);
            if (response == null)
                return Unauthorized();
            return Ok(response);
        }
    }
}