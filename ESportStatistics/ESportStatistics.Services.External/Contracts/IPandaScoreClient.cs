using ESportStatistics.Data.Models.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESportStatistics.Services.External
{
    public interface IPandaScoreClient
    {
        Task<IEnumerable<T>> GetEntities<T>(string accessToken, string entityName, int pageSize = 100, int pageNumber = 1)
            where T : PandaScoreBaseEntity;

        Task<IEnumerable<T>> GetEntitiesParallel<T>(string accessToken, string entityName, int pageSize = 100)
            where T : PandaScoreBaseEntity;

        Task<int> GetNumberOfAvaliableElements(string accessToken, string entityName);
    }
}
