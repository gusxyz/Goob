using System.Linq;
using Content.Shared.Damage;
using Content.Goobstation.Maths.FixedPoint;
using Content.Shared._Shitmed.Body;
using Content.Shared.Body.Components;
using Content.Shared.Body.Part;

namespace Content.Shared.Mobs.Systems;

public sealed partial class MobThresholdSystem
{
    /// <summary>
    /// Calculates the total damage from vital body parts (Head, Chest, Groin), for complex-bodies.
    /// For non-complex bodies or if no vital parts are found, returns the total damage from the target entity.
    /// </summary>
    /// <param name="target">The entity to check for vital damage</param>
    /// <param name="damageableComponent">The damageable component of the target entity</param>
    /// <returns>Total damage from vital body parts, or total damage if not a complex body or no vital parts found</returns>
    public FixedPoint2 CheckVitalDamage(EntityUid target, DamageableComponent damageableComponent)
    {
        if (!TryComp(target, out BodyComponent? body) || body.BodyType != BodyType.Complex || body.RootContainer?.ContainedEntity is not { } rootPart)
            return damageableComponent.TotalDamage;

        var result = FixedPoint2.Zero;

        foreach (var (partId, _) in _wound.GetAllWoundableChildren(rootPart))
        {
            if (!TryComp(partId, out DamageableComponent? wdc)
                || !TryComp(partId, out BodyPartComponent? bpc))
                continue;

            var weight = bpc.PartType switch
            {
                BodyPartType.Head => 2.0f,
                BodyPartType.Chest => 1.75f,
                BodyPartType.Groin => 1.5f,
                BodyPartType.Arm => 0.5f,
                BodyPartType.Hand => 0.3f,
                BodyPartType.Leg => 0.5f,
                BodyPartType.Foot => 0.3f,
                _ => 1.0f,
            };

            result += wdc.TotalDamage * weight;
        }
        return result;
    }

}
