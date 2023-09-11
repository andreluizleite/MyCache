using FinbourneCache.Component;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinbourneCacheTest
{
    public class CachePerformanceTests
    {
        [Fact]
        public void Cache_PerformanceTest()
        {
            const int capacity = 1000;
            var cache = new Cache<int, string>(capacity);

            // Populate the cache with 1000 items
            for (int i = 0; i < capacity; i++)
            {
                cache.Add(i, $"Value-{i}");
            }

            var stopwatch = Stopwatch.StartNew();

            // Retrieve all 1000 items from the cache
            for (int i = 0; i < capacity; i++)
            {
                if (!cache.TryGet(i, out _))
                {
                    Assert.Fail($"Item with key {i} not found in the cache.");
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"Time taken to retrieve {capacity} items: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
