namespace GoalSeek.Services.Caching
{
    public abstract record BaseCacheKey<T>(string Prefix, T uniqueKey, string Postfix)
    {
        public override string ToString()
        {
            return $"{Prefix}_{uniqueKey}_{Postfix}";
        }
    }
}
