using Cotizacion.Models;

namespace Cotizacion.BL
{
    public interface ISaveUsuario
    {
        int Insert(Usuario usuario);
        int Update(Usuario usuario);
    }
}
