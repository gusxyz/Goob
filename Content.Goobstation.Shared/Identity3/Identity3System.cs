using Content.Goobstation.Shared.Identity3.Events;
using Content.Shared.IdentityManagement.Components;

namespace Content.Goobstation.Shared.Identity3;

/// <summary>
/// This handles...
/// </summary>
public sealed class Identity3System : EntitySystem
{
    /// <inheritdoc/>
    public override void Initialize()
    {
        SubscribeLocalEvent<IdentityKnowledgeComponent,HeardSpeechEvent>(OnHeardSpeech);
    }

    private void OnHeardSpeech(Entity<IdentityKnowledgeComponent> ent, ref HeardSpeechEvent args)
    {
        var speaker = args.Speaker;

        var identity = new SeeIdentityAttemptEvent();
        RaiseLocalEvent(speaker, identity);

        if(speaker == ent.Owner || identity.Cancelled)
            return;

        var val = ent.Comp.KnowledgeCache.Get(speaker);
        if (val == default)
            ent.Comp.KnowledgeCache.Set(speaker, 1);
        else
            ent.Comp.KnowledgeCache.Set(speaker, val + 1);
    }
}
