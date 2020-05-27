using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}