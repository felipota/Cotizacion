using Cotizacion.ExternalServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cotizacion.BL
{
    public class GetDolar : IGetCotizacion
    {
        public OperadorCambio Operador => OperadorCambio.Dolar;
        private readonly IBancoProvinciaService _clientFactory;
        public GetDolar(IBancoProvinciaService clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public List<string> Get()
        {
            List<string> response = _clientFactory.GetDolar().Result;
            return response;
        }
    }
}
