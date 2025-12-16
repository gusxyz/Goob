using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.Set;
using Robust.Shared.Utility;

namespace Content.Goobstation.Common.Progression;

public interface IProgressionNode : IPrototype
{
    /// <inheritdoc cref="IProgressionNode"/>
    ///     // ReSharper disable once InconsistentNaming
    [IdDataField]
    string ID { get; }

    /// <summary>
    /// The name to be displayed in the UI.
    /// </summary>
    [DataField("name")]
    LocId Name { get; }

    /// <summary>
    /// The description shown in the info panel.
    /// </summary>
    [DataField("description")]
    LocId Description { get; }

    /// <summary>
    /// What research discipline this technology belongs to.
    /// </summary>
    [DataField(required: true)]
    ProtoId<IDiscipline> Discipline { get; }

    /// <summary>
    /// The icon for the node in the tree.
    /// </summary>
    [DataField("icon")]
    SpriteSpecifier Icon { get; }

    /// <summary>
    /// The cost to unlock this node.
    /// </summary>
    [DataField("cost")]
    int Cost { get; }

    /// <summary>
    /// The set of node IDs that must be unlocked before this one.
    /// </summary>
    [DataField(customTypeSerializer: typeof(PrototypeIdHashSetSerializer<IProgressionNode>))]
    HashSet<string> Prerequisites { get; }

    /// <summary>
    /// The position of this node in the UI tree.
    /// </summary>
    [DataField("position")]
    Vector2i Position { get; }
}
