using Content.Goobstation.Shared.Identity3.Events;
using Content.Shared.Examine;
using Content.Shared.IdentityManagement;
using Content.Shared.IdentityManagement.Components;
using Robust.Shared.Random;

namespace Content.Goobstation.Shared.Identity3;
// TODO:
// need a separate voice and visual stack?
// move LRUCache to maths?
// move id cards to jumpsuit slot 😴
// implement as much of the document as possible

public sealed class Identity3System : EntitySystem
{
    [Dependency] private readonly EntityManager _entityManager = default!;
    [Dependency] private readonly ExamineSystemShared _examineSystem = default!;
    [Dependency] private readonly IRobustRandom _random = default!;

    /// <inheritdoc/>
    public override void Initialize()
    {
        SubscribeLocalEvent<IdentityKnowledgeComponent,HeardSpeechEvent>(OnHeardSpeech);
    }

    private void OnHeardSpeech(Entity<IdentityKnowledgeComponent> ent, ref HeardSpeechEvent args)
    {
        var speaker = args.Speaker;
        var identity = new SeeIdentityAttemptEvent();
        var knowledge = ent.Comp.KnowledgeCache;
        var val = knowledge.Get(speaker);

        RaiseLocalEvent(speaker, identity);
        if(speaker == ent.Owner || identity.Cancelled || _examineSystem.InRangeUnOccluded(ent.Owner, speaker))
            return;
        // place into short term memroy?

        if (val == default)
            knowledge.Set(speaker, 1);
        else
            knowledge.Set(speaker, val + 1);

        if (knowledge.Get(speaker) >= ent.Comp.LongTermRequirement)
        {
         // roll a chance to remember
        }
        // log debugging ftw
        Log.Info(Identity.Name(speaker, _entityManager) + "heard:");
        foreach (var cacheItem in knowledge.GetList())
        {
            Log.Info(Identity.Name(cacheItem.Key, _entityManager) + " " + cacheItem.Value));
        }
    }
}
