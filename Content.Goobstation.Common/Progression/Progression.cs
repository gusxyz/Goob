using Content.Goobstation.Common.Research;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Utility;

namespace Content.Goobstation.Common.Progression;

[Serializable, NetSerializable]
public abstract class ProgressionBoundInterfaceState(
    string prototypeTypeId,
    string disciplineTypeId,
    List<DisciplineState> disciplines)
    : BoundUserInterfaceState
{
    public readonly List<DisciplineState> Disciplines = disciplines;
    public readonly string PrototypeTypeId = prototypeTypeId;
    public readonly string DisciplineTypeId = disciplineTypeId;
}


[Serializable, NetSerializable]
public record struct DisciplineState(
    ProtoId<IDiscipline> Id,
    string UiName,
    SpriteSpecifier Icon,
    string PercentageText
);


[Serializable, NetSerializable]
public record struct ProgressionUnlock(
    string Name,
    string? Description,
    SpriteSpecifier Icon
);
