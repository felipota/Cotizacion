using System.Collections.Generic;

namespace Cotizacion.BL
{
    public interface ICotizacionFac
    {
        List<string> GetCotizacion(string commodity);
    }
}
