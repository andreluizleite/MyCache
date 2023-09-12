using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinbourneCache.Component
{
    public class Factory
    {
        // Private static instance of the factory.
        private static Factory _instance;

        // Private constructor to prevent external instantiation.
        private Factory() { }

        public ICacheComponent<TKey, TValue> NewCacheComponent<TKey, TValue>(int capacity)
        {
            return new CacheComponent<TKey, TValue>(capacity);
        }

        // Public property to access the single instance of the factory.
        public static Factory Instance
        {
            get
            {
                // If the instance doesn't exist, create it.
                if (_instance == null)
                {
                    _instance = new Factory();
                }
                return _instance;
            }
        }
    }
}
