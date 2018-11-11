using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Services.Contracts
{
    public interface IPDFService
    {
        Task<byte[]> GetFileBytesAsync(string filePath);

        string CreatePDF<T>(IEnumerable<T> query, IList<string> requiredColumns, string fileName) where T : class;

        void DeleteFile(string fileName);
    }
}
