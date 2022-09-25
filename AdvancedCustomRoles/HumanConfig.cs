using Syml;

namespace AdvancedCustomRoles;

public class HumanConfig : IDocumentSection
{
    public float WalkSpeed { get; set; } = 1.02f;
    public float SprintSpeed { get; set; } = 1.05f;
}