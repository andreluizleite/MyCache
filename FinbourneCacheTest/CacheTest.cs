using FinbourneCache.Component;

namespace FinbourneCacheTest
{
    public class CacheTest
    {
        [Fact]
        public void Cache_Add_GetItem()
        {
            //Arrange
            var cache = Factory.Instance.NewCacheComponent<int, string>(1);

            //Act
            cache.Add(1, "Value1");

            //Assert
            Assert.True(cache.TryGet(1, out var value));
            Assert.Equal(value, "Value1");

        }

        [Fact]
        public void Cache_UpdateItem()
        {
            // Arrange
            var cache = Factory.Instance.NewCacheComponent<int, string>(2);

            // Act
            cache.Add(1, "Value1");
            cache.Add(2, "Value2");

            // Update "one"
            cache.Add(1, "Value3 updated");

            // Assert
            Assert.True(cache.TryGet(1, out var value));
            Assert.Equal("Value3 updated", value);
        }

        [Fact]
        public void Cache_NotFound()
        {
            // Arrange
            var cache = Factory.Instance.NewCacheComponent<int, string>(3);

            // Act & Assert
            Assert.False(cache.TryGet(1, out var value));
        }
    }
}