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
        private readonly IMemoryCacheService _cacheService;
        private readonly ILogger<NewsService> _logger;

        public NewsService(IOptions<ApiKeys> apiKeys, IMemoryCacheService cacheService, ILogger<NewsService> logger)
        {
            _apiKeys = apiKeys.Value;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<ArticlesResult> GetTopHeadlinesAsync(Countries? country, Categories? category, string? q)
        {
            string cacheKey = $"news_{country}_{category}_{q}";

            var articlesResult = await _cacheService.GetOrSetAsync(cacheKey, async () =>
            {
                var newsApiClient = new NewsApiClient(_apiKeys.NewsApiKey);
                var request = new TopHeadlinesRequest();

                if (country.HasValue) request.Country = country.Value;
                if (category.HasValue) request.Category = category.Value;
                if (!string.IsNullOrWhiteSpace(q)) request.Q = q;

                try
                {
                    var result = await newsApiClient.GetTopHeadlinesAsync(request);

                    if (result == null || result.Articles == null || !result.Articles.Any())
                    {
                        _logger.LogWarning("No articles found for the request with: Country - {country}, Category - {category}, Keyword - {q}", country, category, q);
                    }

                    return result ?? new ArticlesResult();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while fetching news data with: Country - {country}, Category - {category}, Keyword - {q}", country, category, q);
                    return new ArticlesResult();
                }
            });
            return articlesResult ?? new ArticlesResult();
        }
    }
}
