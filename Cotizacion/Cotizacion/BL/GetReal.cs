using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cotizacion.BL
{
    public class GetReal : IGetCotizacion
    {
        public OperadorCambio Operador => OperadorCambio.Real;

        public  List<string> Get()
        {
            return null;
        }
    }
}
