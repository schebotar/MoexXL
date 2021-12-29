using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace MoexXL
{
    static class MemoryCacheUtil
    {
        private const int LifetimeMinutes = 10;
        private static MemoryCache memoryCache = AddIn.memoryCache;

        public static void CacheWithKey(this Dictionary<string, object> attributes, string ticker)
        {
            memoryCache.Add(ticker, attributes, DateTime.Now.AddMinutes(LifetimeMinutes));
        }
    }
}