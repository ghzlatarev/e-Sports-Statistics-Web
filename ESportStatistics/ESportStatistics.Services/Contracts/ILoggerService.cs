namespace ESportStatistics.Core.Providers.Contracts
{
    public interface ILoggerService
    {
        void LogToFile(string message, string fileName);
    }
}
