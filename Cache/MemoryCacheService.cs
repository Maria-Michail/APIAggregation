using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace AgileActorsApp.Cache
{
    public class MemoryCacheService
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _cacheOptions;

        public MemoryCacheService(IDistributedCache cache)
        {
            _cache = cache;
            _cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };
        }

        public async Task<T?> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getDataFunc)
        {
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<T>(cachedData);
            }

            var data = await getDataFunc();
            if (data != null)
            {
                var serializedData = JsonSerializer.Serialize(data);
                await _cache.SetStringAsync(cacheKey, serializedData, _cacheOptions);
            }

            return data;
        }
    }
}
