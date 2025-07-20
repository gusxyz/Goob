using Content.Goobstation.Common.LRUCache;

namespace Content.Goobstation.Shared.Identity3;

/// <summary>
/// This is used for storing
/// </summary>
[RegisterComponent]
public sealed partial class IdentityKnowledgeComponent : Component
{
    [ViewVariables(VVAccess.ReadOnly)]
    public readonly LRUCache<EntityUid, int> KnowledgeCache = new (2);

    [ViewVariables(VVAccess.ReadOnly)]
    public List<EntityUid> LongTermList;

    public int LongTermRequirement = 10;
}
