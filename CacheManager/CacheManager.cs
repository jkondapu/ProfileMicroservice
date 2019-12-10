using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMicroservice.CacheManager
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;
        public CacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public T Add<T>(string key, T value, TimeSpan expiration) where T : class
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(expiration);

            return _memoryCache.Set<T>(key, value, cacheEntryOptions);
        }
        public T Get<T>(string key) where T : class
        {
            _memoryCache.TryGetValue(key, out T result);
            return result;
        }
        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
