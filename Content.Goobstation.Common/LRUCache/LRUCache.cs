using System.Runtime.CompilerServices;

namespace Content.Goobstation.Common.LRUCache;

using System.Collections.Generic;

// ReSharper disable once InconsistentNaming
// ngl idk if this shits gonna work good
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
