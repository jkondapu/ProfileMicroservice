using System;

namespace ProfileMicroservice.CacheManager
{
    public interface ICacheManager
    {
        T Get<T>(string key) where T : class;
        T Add<T>(string key, T value, TimeSpan expiration) where T : class;
        void Remove(string key);
    }
}