using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace AgileActorsApp.Cache
{
    public class MemoryCacheService: IMemoryCacheService
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _cacheOptions;
        private readonly ILogger<MemoryCacheService> _logger;

        public MemoryCacheService(IDistributedCache cache, ILogger<MemoryCacheService> logger)
        {
            _cache = cache;
            _cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };
            _logger = logger;
        }

        public async Task<T?> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getDataFunc)
        {
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedValue = JsonSerializer.Deserialize<T>(cachedData);
                if (cachedValue == null)
                {
                    _logger.LogError("Failed to deserialize cached data for cacheKey: {CacheKey}", cacheKey);
                    return default;
                }
                return cachedValue;
            }

            var data = await getDataFunc();
            if (data == null)
            {
                return default;
            }

            var serializedData = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(cacheKey, serializedData, _cacheOptions);

            return data;
        }

    }
}
