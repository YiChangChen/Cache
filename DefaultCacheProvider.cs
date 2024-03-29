using System;
using System.Runtime.Caching;

namespace Cache
{
    public class DefaultCacheProvider : ICacheProvider
    {
        private ObjectCache Cache { get { return MemoryCache.Default; } }
        public object Get(string key)
        {
            return Cache[key];
        }

        public void Invalidate(string key)
        {
            Cache.Remove(key);
        }

        public bool IsSet(string key)
        {
            return (Cache[key] != null);
        }

        public void Set(string key, object data, int cacheTime)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromSeconds(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }
    }
}
