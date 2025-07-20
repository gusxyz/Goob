namespace Content.Goobstation.Common.LRUCache;

// ReSharper disable InconsistentNaming
internal record LRUCacheItem<TK, TV>(TK key, TV value)
{
    public TK Key = key;
    public TV Value = value;
}
