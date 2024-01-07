namespace GoalSeek.Services.Caching
{
    public interface ICacheManager
    {
        Task<T> GetAsync<T>(GoalSeekCacheKeyGenerator cacheKey, Func<T> operation, TimeSpan expredTime, CancellationToken cancellationToken = default);
    }
}
