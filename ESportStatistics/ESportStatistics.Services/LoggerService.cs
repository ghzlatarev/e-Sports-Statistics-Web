using ESportStatistics.Core.Providers.Contracts;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ESportStatistics.Core.Providers
{
    public class LoggerService : ILoggerService
    {
        public async Task LogToFileAsync(string message, string fileName = "logs.txt")
        {
            if (message == null)
            {
                throw new ArgumentNullException();
            }

            StringBuilder log = new StringBuilder();

            log.Append("Exception: ");
            log.Append(message);
            log.Append(" | Date:");
            log.Append(DateTime.Now.ToString());
            log.Append(Environment.NewLine);

            await File.AppendAllTextAsync(fileName, log.ToString());
        }
    }
}
