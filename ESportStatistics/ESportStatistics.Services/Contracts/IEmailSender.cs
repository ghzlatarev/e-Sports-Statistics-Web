using System.Threading.Tasks;

namespace ESportStatistics.Core.Providers.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
