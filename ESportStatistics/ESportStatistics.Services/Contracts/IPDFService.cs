using System.Collections.Generic;

namespace ESportStatistics.Services.Contracts
{
    public interface IPDFService
    {
        string CreatePDF<T>(IEnumerable<T> query, IList<string> requiredColumns, string fileName) where T : class;

        bool DeleteFile(string fileName);
    }
}
