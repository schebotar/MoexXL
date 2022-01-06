using System;
using System.Runtime.Caching;
using MoexXL.MoexApi;

namespace MoexXL
{
    static class MemoryCacheExtensions
    {
        private const int LifetimeMinutes = 10;
        private static readonly MemoryCache memoryCache = AddIn.memoryCache;

        public static bool TryGetFromCache<TItem>(this string key, out TItem item)
            where TItem : IssResponse
        {
            if (memoryCache.Contains(key))
            {
                item = memoryCache[key] as TItem;
                return true;
            }

            else
            {
                item = null;
                return false;
            }
        }

        public static void CacheWithKey<TItem>(this TItem item, string ticker)
            where TItem : IssResponse
        {
            memoryCache.Add(ticker, item, DateTime.Now.AddMinutes(LifetimeMinutes));
        }
    }
}