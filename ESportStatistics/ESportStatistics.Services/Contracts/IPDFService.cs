using System;
using System.Collections.Generic;
using System.Text;

namespace ESportStatistics.Services.Contracts
{
    public interface IPDFService
    {
       void ToPDF<T>(IEnumerable<T> query, List<string> requiredColumns, string fileName) where T : class;
    }
}
