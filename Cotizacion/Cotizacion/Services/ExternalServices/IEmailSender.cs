using System.Threading.Tasks;

namespace Cotizacion.ExternalServices
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

}

