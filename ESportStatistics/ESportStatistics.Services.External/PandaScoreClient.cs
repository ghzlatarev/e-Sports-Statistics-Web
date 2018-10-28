using ESportStatistics.Data.Models.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ESportStatistics.Services.External
{
    public class PandaScoreClient : IPandaScoreClient
    {
        private readonly HttpClient client;

        public PandaScoreClient()
        {
            this.client = new HttpClient();
        }

        public async Task<IEnumerable<T>> GetEntities<T>(string accessToken, string entityName, int pageSize = 100, int pageNumber = 1)
            where T : PandaScoreBaseEntity
        {
            var response = await client.GetAsync("https://api.pandascore.co/lol/" + entityName +
                "?page[size]=" + pageSize +
                "&page[number]=" + pageNumber +
                "&token=" + accessToken)
                .ConfigureAwait(false);

            var entities = JsonConvert.DeserializeObject<IEnumerable<T>>(await response.Content.ReadAsStringAsync());

            return entities;
        }

        public async Task<IEnumerable<T>> GetEntitiesParallel<T>(string accessToken, string entityName, int pageSize = 100)
            where T : PandaScoreBaseEntity
        {
            var tasks = new List<Task<IEnumerable<T>>>();

            int numberOfElements = await this.GetNumberOfAvaliableElements(accessToken, entityName);
            int numberOfPages = (int)Math.Ceiling((double)numberOfElements / pageSize);

            for (int pageNumber = 1; pageNumber <= numberOfPages; pageNumber++)
            {
                tasks.Add(this.GetEntities<T>(accessToken, entityName, pageSize, pageNumber));
            }

            return (await Task.WhenAll(tasks)).SelectMany(e => e);
        }

        public async Task<int> GetNumberOfAvaliableElements(string accessToken, string entityName)
        {
            var response = await client.GetAsync("https://api.pandascore.co/lol/" + entityName +
                "?page[size]=" + 1 +
                "&token=" + accessToken)
                .ConfigureAwait(false);

            int numberOfElements = 0;

            if (response.Headers.Contains("X-Total") && response.Headers.GetValues("X-Total").Count() != 0)
            {
                numberOfElements = int.Parse(response.Headers.GetValues("X-Total").ToArray()[0]);
            }

            return numberOfElements;
        }
    }
}
