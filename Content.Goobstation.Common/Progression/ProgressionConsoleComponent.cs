namespace Content.Goobstation.Common.Progression;

/// <summary>
/// This handles...
/// </summary>
public abstract partial class ProgressionConsoleComponent : Component
{
    [DataField]
    public string PrototypeType = "technology";

    [DataField]
    public string DisciplineType = "techDiscipline";
}
