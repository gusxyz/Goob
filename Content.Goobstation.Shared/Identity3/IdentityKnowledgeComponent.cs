using Content.Goobstation.Common.LRUCache;

namespace Content.Goobstation.Shared.Identity3;

/// <summary>
/// This is used for storing
/// </summary>
[RegisterComponent]
public sealed partial class IdentityKnowledgeComponent : Component
{
    public LRUCache<EntityUid, int> KnowledgeCache = new (5);
    public List<EntityUid> FixedKnowledgeList;
}
