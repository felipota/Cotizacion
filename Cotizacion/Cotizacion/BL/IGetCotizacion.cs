using System.Collections.Generic;

namespace Cotizacion.BL
{
    public interface IGetCotizacion
    {
        OperadorCambio Operador { get; }
        List<string> Get();
    }
}
