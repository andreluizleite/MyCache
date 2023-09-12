using FinbourneCache.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinbourneCacheTest
{
    public class CacheEventTest
    {
        [Fact]
        public void CacheComponent_ItemEvictedEvent()
        {
            // Arrange
            int capacity = 3;
            var cache = Factory.NewCacheComponent<string, int>(capacity);
            bool eventRaised = false;

            // Subscribe to the ItemEvicted event
            cache._ItemEvicted += (key, value) =>
            {
                eventRaised = true;
            };

            // Act
            cache.Add("Key1", 1);
            cache.Add("Key2", 2);
            cache.Add("Key3", 3);
            cache.Add("Key4", 4); // it should trigger eviction, which in turn should raise the ItemEvicted event.

            // Assert
            Assert.True(eventRaised, "ItemEvicted event was not raised.");
        }
    }
}
