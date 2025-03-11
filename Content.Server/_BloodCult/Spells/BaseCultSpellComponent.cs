namespace Content.Server._BloodCult.Spells;

[RegisterComponent]
public sealed partial class BaseCultSpellComponent : Component
{
    /// <summary>
    ///     If true will ignore protection like mindshield of holy magic.
    /// </summary>
    [DataField]
    public bool BypassProtection;
}
