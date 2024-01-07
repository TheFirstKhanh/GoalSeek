namespace GoalSeek.Services.Caching
{
    public record GoalSeekCacheKeyGenerator(string uniqueKey) : BaseCacheKey<string>("GoalSeek", uniqueKey, "Post");
}
