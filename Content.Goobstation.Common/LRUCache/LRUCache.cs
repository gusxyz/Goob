using System.Runtime.CompilerServices;

namespace Content.Goobstation.Common.LRUCache;

// Least Recently Used (LRU) Cache
// TODO:
// Move this to maths
// See if I need to make any more helpers.

// ReSharper disable once InconsistentNaming
public sealed class LRUCache<TK, TV>(int capacity) where TK : notnull
{
    private readonly Dictionary<TK, LinkedListNode<LRUCacheItem<TK, TV>>> _cacheMap = new();
    private readonly LinkedList<LRUCacheItem<TK, TV>> _lruList = [];

    [MethodImpl(MethodImplOptions.Synchronized)]
    public TV? Get(TK key)
    {
        if (!_cacheMap.TryGetValue(key, out var node))
            return default;

        var value = node.Value.Value;
        _lruList.Remove(node);
        _lruList.AddLast(node);
        return value;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public LinkedList<LRUCacheItem<TK, TV>> GetList()
    {
        return _lruList;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Set(TK key, TV val)
    {
        if (_cacheMap.TryGetValue(key, out var existingNode))
            _lruList.Remove(existingNode);
        else if (_cacheMap.Count >= capacity)
            RemoveFirst();

        var cacheItem = new LRUCacheItem<TK, TV>(key, val);
        var node = new LinkedListNode<LRUCacheItem<TK, TV>>(cacheItem);
        _lruList.AddLast(node);
        _cacheMap[key] = node;
    }

    private void RemoveFirst()
    {
        var node = _lruList.First;
        _lruList.RemoveFirst();

        if (node != null)
            _cacheMap.Remove(node.Value.Key);
        else
            throw new InvalidOperationException("Node is null");
    }
}
