using AgileActorsApp.ApiKey;
using Microsoft.Extensions.Options;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsAPI;

namespace AgileActorsApp.Services.News
{
    public class NewsService : INewsService
    {
        private readonly ApiKeys _apiKeys;

        public NewsService(IOptions<ApiKeys> apiKeys)
        {
            _apiKeys = apiKeys.Value;
        }

        public async Task<ArticlesResult> GetTopHeadlinesAsync(Countries? country, Categories? category, string? q)
        {
            string apiKey = _apiKeys.NewsApiKey;
            var newsApiClient = new NewsApiClient(apiKey);

            var request = new TopHeadlinesRequest();

            if (country.HasValue)
                request.Country = country.Value;

            if (category.HasValue)
                request.Category = category.Value;

            if (!string.IsNullOrWhiteSpace(q))
                request.Q = q;

            return await newsApiClient.GetTopHeadlinesAsync(request);
        }
    }
}
