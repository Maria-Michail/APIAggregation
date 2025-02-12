namespace AgileActorsApp.Cache
{
    public interface IMemoryCacheService
    {
        Task<T?> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getDataFunc);
    }
}
