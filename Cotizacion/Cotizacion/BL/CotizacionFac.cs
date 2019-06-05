using System;
using System.Collections.Generic;
using System.Linq;

namespace Cotizacion.BL
{
    public class CotizacionFac : ICotizacionFac
    {
        private readonly IEnumerable<IGetCotizacion> _operators;
        public CotizacionFac(IEnumerable<IGetCotizacion> operators)
        {
            _operators = operators;
        }
        public List<string> GetCotizacion(string commodity)
        {
            OperadorCambio op;
            switch (commodity)
            {
                case "Dolar":
                    op = OperadorCambio.Dolar;
                    break;
                case "Peso":
                    op = OperadorCambio.Peso;
                    break;
                case "Real":
                    op = OperadorCambio.Real;
                    break;
                default:
                    return null;
            };
            return _operators.FirstOrDefault(x => x.Operador == op)?.Get() ?? null;
        }

    }
}
