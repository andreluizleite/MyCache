using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinbourneCache.Component
{
    public static class Factory
    {
        public static ICacheComponent<TKey, TValue> NewCacheComponent<TKey, TValue>(int capacity)
        {
            return new CacheComponent<TKey, TValue>(capacity);
        }
    }
}
