using System.Collections.Generic;
using Syml;

namespace AdvancedCustomRoles;

public class AdvancedConfig : IDocumentSection
{
    public List<string> CommandToExecuteAtSpawn = new();
}