using AgileActorsApp.ApiKey;
using Microsoft.Extensions.Options;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsAPI;
using AgileActorsApp.Cache;

namespace AgileActorsApp.Services.News
{
    public class NewsService : INewsService
    {
        private readonly ApiKeys _apiKeys;
        private readonly MemoryCacheService _cacheService;

        public NewsService(IOptions<ApiKeys> apiKeys, MemoryCacheService cacheService)
        {
            _apiKeys = apiKeys.Value;
            _cacheService = cacheService;
        }

        public async Task<ArticlesResult> GetTopHeadlinesAsync(Countries? country, Categories? category, string? q)
        {
            string cacheKey = $"news_{country}_{category}_{q}";

            return await _cacheService.GetOrSetAsync(cacheKey, async () =>
            {
                var newsApiClient = new NewsApiClient(_apiKeys.NewsApiKey);
                var request = new TopHeadlinesRequest();

                if (country.HasValue) request.Country = country.Value;
                if (category.HasValue) request.Category = category.Value;
                if (!string.IsNullOrWhiteSpace(q)) request.Q = q;

                var result = await newsApiClient.GetTopHeadlinesAsync(request);
                return result ?? new ArticlesResult();
            });
        }
    }
}
