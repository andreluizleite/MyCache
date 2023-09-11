using FinbourneCache.Component;

namespace FinbourneCacheTest
{
    public class CacheTest
    {
        [Fact]
        public void Cache_Add_GetItem()
        {
            //Arrange
            var cache = new FinbourneCache.Component.Cache<string, int>(3);

            //Act
            cache.Add("one", 1);

            //Assert
            Assert.True(cache.TryGet("one", out var value));
            Assert.Equal(value, 1);

        }

        [Fact]
        public void Cache_UpdateItem()
        {
            // Arrange
            var cache = new FinbourneCache.Component.Cache<string, int>(2);

            // Act
            cache.Add("one", 1);
            cache.Add("two", 2);

            // Update "one"
            cache.Add("one", 10);

            // Assert
            Assert.True(cache.TryGet("one", out var value));
            Assert.Equal(10, value);
        }

        [Fact]
        public void Cache_NotFound()
        {
            // Arrange
            var cache = new FinbourneCache.Component.Cache<string, int>(3);

            // Act & Assert
            Assert.False(cache.TryGet("nonexistent", out _));
        }
    }
}