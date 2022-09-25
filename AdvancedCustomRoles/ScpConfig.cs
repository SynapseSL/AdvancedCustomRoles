using Syml;

namespace AdvancedCustomRoles;

public class ScpConfig : IDocumentSection
{
    public float ScpAttackDamage { get; set; } = 200f;
    public bool Scp106TakeIntoPocket { get; set; } = true;
}