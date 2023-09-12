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
            var cache = Factory.Instance.NewCacheComponent<int, string>(capacity);
            bool eventRaised = false;

            // Subscribe to the ItemEvicted event
            cache._ItemEvicted += (key, value) =>
            {
                eventRaised = true;
            };

            // Act
            cache.Add(1, "Value1");
            cache.Add(2, "Value2");
            cache.Add(3, "Value3");
            cache.Add(4, "Value4"); // it should trigger eviction, which in turn should raise the ItemEvicted event.

            // Assert
            Assert.True(eventRaised, "ItemEvicted event was not raised.");
        }
    }
}
