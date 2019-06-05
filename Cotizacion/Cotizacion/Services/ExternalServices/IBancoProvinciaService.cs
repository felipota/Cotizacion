using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cotizacion.ExternalServices
{
    public interface IBancoProvinciaService
    {
        Task<List<string>> GetDolar();
    }
}
