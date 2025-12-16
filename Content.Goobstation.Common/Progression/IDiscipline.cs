using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Goobstation.Common.Progression;

public interface IDiscipline : IPrototype
{
    /// <inheritdoc cref="IDiscipline" />
    [IdDataField]
    // ReSharper disable once InconsistentNaming
    new string ID { get; }

    [DataField("name", required: true)] string Name {get;}

    [DataField("color", required: true)] Color Color {get;}

    [DataField("icon")] SpriteSpecifier Icon {get;}

    [DataField("tierPrerequisites", required: true)]
    Dictionary<int, float> TierPrerequisites {get;}
    [DataField("lockoutTier")] int LockoutTier {get;}
    [DataField(required: true)] string UiName {get;}
}
