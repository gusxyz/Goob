namespace Content.Goobstation.Shared.Identity3.Events;

/// <summary>
/// A message that tells you that you've been spoken to inrange
/// </summary>
[Serializable]
public sealed class HeardSpeechEvent(EntityUid speaker) : EntityEventArgs
{
    public readonly EntityUid Speaker = speaker;
}
