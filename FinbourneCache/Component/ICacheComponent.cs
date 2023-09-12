namespace FinbourneCache.Component
{
    public interface ICacheComponent<TKey, TValue>
    {
        event Action<TKey, TValue> _ItemEvicted;

        void Add(TKey key, TValue value);
        bool TryGet(TKey key, out TValue value);
    }
}