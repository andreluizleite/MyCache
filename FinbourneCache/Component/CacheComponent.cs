
namespace FinbourneCache.Component
{
    internal class CacheComponent<TKey, TValue> : ICacheComponent<TKey, TValue>
    {
        private readonly int _capacity;
        private readonly Dictionary<TKey, LinkedListNode<CacheItem>> _cacheMap;
        private readonly LinkedList<CacheItem> _cacheList;
        public event Action<TKey, TValue> _ItemEvicted;

        internal CacheComponent(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "The Capacity must be a positive integer.");

            _capacity = capacity;
            _cacheMap = new Dictionary<TKey, LinkedListNode<CacheItem>>(capacity);
            _cacheList = new LinkedList<CacheItem>();
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");

            if (_cacheMap.Count >= _capacity)
                Evict();

            var cacheItem = new CacheItem(key, value);
            var node = new LinkedListNode<CacheItem>(cacheItem);

            _cacheList.AddFirst(node);
            _cacheMap[key] = node;
        }

        public bool TryGet(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");

            if (_cacheMap.TryGetValue(key, out var node))
            {
                _cacheList.Remove(node);
                _cacheList.AddFirst(node);

                value = node.Value.Value;
                return true;
            }

            value = default;
            return false;
        }

        private void Evict()
        {
            var lastNode = _cacheList.Last;
            if (lastNode != null)
            {
                TKey evictedKey = lastNode.Value.Key;
                TValue evictedValue = lastNode.Value.Value;

                _cacheMap.Remove(lastNode.Value.Key);
                _cacheList.RemoveLast();

                // Raise the ItemEvicted event
                _ItemEvicted?.Invoke(evictedKey, evictedValue);
            }
        }

        private class CacheItem
        {
            public TKey Key { get; }
            public TValue Value { get; }

            public CacheItem(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}