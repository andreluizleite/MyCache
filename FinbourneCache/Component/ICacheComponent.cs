namespace FinbourneCache.Component
{
    public interface ICacheComponent<TKey, TValue>
    {
        void Add(TKey key, TValue value);
        bool TryGet(TKey key, out TValue value);
    }
}