
namespace FinbourneCache.Component
{
    public class Cache<TKey, TValue>
    {
        private readonly int _capacity;
        private readonly Dictionary<TKey, LinkedListNode<CacheItem>> _cacheMap;
        private readonly LinkedList<CacheItem> _cacheList;

        public Cache(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("The Capacity shoud be positive.");

            _capacity = capacity;
            _cacheMap = new Dictionary<TKey, LinkedListNode<CacheItem>>(capacity);
            _cacheList = new LinkedList<CacheItem>();

        }

        public void Add(TKey key, TValue value)
        {
            if(_cacheMap.Count >= _capacity)
            {
                Evict();
             }

            var cacheItem = new CacheItem(key, value);
            var node = new LinkedListNode<CacheItem>(cacheItem);

            _cacheList.AddFirst(node);
            _cacheMap[key] = node;
        }

        public bool TryGet(TKey key, out TValue value)
        {
            if (_cacheMap.TryGetValue(key, out var node))
            {
                // Move accessed item to the front of the list
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
                _cacheMap.Remove(lastNode.Value.Key);
                _cacheList.RemoveLast();
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