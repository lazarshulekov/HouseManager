namespace BLL
{
    using System;

    using Microsoft.Extensions.Caching.Memory;

    public static class CacheExtensions
    {
        //public static T UseCache<T>(this MemoryCache cache, object key, Func<T> retrieveFunc, TimeSpan? timespan = null)
        //{
        //    T cacheEntry;

        //    // Look for cache key.
        //    if (!cache.TryGetValue(key, out cacheEntry))
        //    {
        //        // Key not in cache, so get data.
        //        cacheEntry = retrieveFunc();

        //        // Set cache options.
        //        var cacheEntryOptions = new MemoryCacheEntryOptions();

        //        // Keep in cache for this time, reset time if accessed.

        //        // Save data in cache.
        //        cache.Set(key, cacheEntry, cacheEntryOptions);
        //    }

        //    return cacheEntry;
        //}


        public static T UseCache<T>(
            this IMemoryCache cache,
            object key,
            Func<T> retrieveFunc,
            TimeSpan? timespan = null)
        {
            T cacheEntry;

            // Look for cache key.
            if (!cache.TryGetValue(key, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = retrieveFunc();

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions();

                // Keep in cache for this time, reset time if accessed.

                // Save data in cache.
                cache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }
    }
}

