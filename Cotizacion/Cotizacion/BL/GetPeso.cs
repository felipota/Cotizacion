using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cotizacion.BL
{
    public class GetPeso : IGetCotizacion
    {
        public OperadorCambio Operador => OperadorCambio.Peso;

        public List<string> Get()
        {
            return null;
        }
    }
}
