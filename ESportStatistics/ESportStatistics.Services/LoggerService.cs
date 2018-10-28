using ESportStatistics.Core.Providers.Contracts;
using System;
using System.IO;
using System.Text;

namespace ESportStatistics.Core.Providers
{
    public class LoggerService : ILoggerService
    {
        public void LogToFile(string message, string fileName = "logs.txt")
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

            File.AppendAllText(fileName, log.ToString());
        }
    }
}
