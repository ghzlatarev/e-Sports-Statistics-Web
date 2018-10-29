using System.Threading.Tasks;

namespace ESportStatistics.Core.Providers.Contracts
{
    public interface ILoggerService
    {
        Task LogToFileAsync(string message, string fileName);
    }
}
