using Microsoft.Extensions.Caching.Memory;

namespace AgileActorsApp.Cache
{
    public class MemoryCacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T GetOrSet<T>(string key, Func<T> getData, int expirationSeconds = 300)
        {
            if (!_cache.TryGetValue(key, out T data))
            {
                data = getData();
                _cache.Set(key, data, TimeSpan.FromSeconds(expirationSeconds));
            }
            return data;
        }
    }
}
