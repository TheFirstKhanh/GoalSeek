using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace GoalSeek.Services.Caching
{
    public class CacheManager : ICacheManager
    {
        private readonly IDistributedCache _distributedCache;
        public CacheManager(IDistributedCache distributedCache) 
        {
            _distributedCache = distributedCache;
        }
        public async Task<T> GetAsync<T>(GoalSeekCacheKeyGenerator cacheKey, Func<T> operation, TimeSpan expredTime, CancellationToken cancellationToken = default)
        {
            
            byte[] cachedByteData = await _distributedCache.GetAsync(cacheKey.ToString(), cancellationToken);
            if (cachedByteData is { })
            {
                return JsonSerializer.Deserialize<T>(cachedByteData);
            }
            
   

            T data = operation();
            if (data == null) return default;
            await _distributedCache.SetAsync(cacheKey.ToString(), JsonSerializer.SerializeToUtf8Bytes(data), cancellationToken);
            return data;
        }
    }
}
