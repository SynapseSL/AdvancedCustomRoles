using System;
using System.Collections.Generic;
using Syml;
using Synapse3.SynapseModule.Config;
using Synapse3.SynapseModule.Map.Rooms;
using Synapse3.SynapseModule.Role;
using YamlDotNet.Serialization;

namespace AdvancedCustomRoles;

[Serializable]
public class CustomRole : IDocumentSection, IAbstractRoleConfig
{
    public string Name { get; set; } = "Null";
    public uint RoleId { get; set; }
    public uint TeamId { get; set; }
    public List<uint> Enemies { get; set; } = new();
    public List<uint> Friends { get; set; } = new();

    public RoleType Role { get; set; } = RoleType.ClassD;
    public RoleType VisibleRole { get; set; } = RoleType.None;
    public uint EscapeRole { get; set; } = 0;
    public float Health { get; set; } = 100;
    public float MaxHealth { get; set; } = 100;
    public float ArtificialHealth { get; set; } = 0;
    public float MaxArtificialHealth { get; set; } = 75;
    public RoomPoint[] PossibleSpawns { get; set; }
    public SerializedPlayerInventory[] PossibleInventories { get; set; }
    public byte UnitId { get; set; } = 0;
    public string Unit { get; set; } = "";
    public SerializedVector3 Scale { get; set; } = new(1f, 1.25f, 1f);
    public bool GodMode { get; set; } = false;
    public string DisplayInfo { get; set; } = "";

    public ushort SpawnMessageTime { get; set; } = 5;
    public string SpawnBroadcast { get; set; } = "";
    public string SpawnHint { get; set; } = "";
    public string SpawnWindow { get; set; } = "";

    public Dictionary<uint, float> RoundStartReplace { get; set; } = new();

    public Dictionary<uint, float> RespawnReplace { get; set; } = new();

    public int MaxSpawnAmount { get; set; } = -1;

    public int MaxRespawnAmount { get; set; } = -1;

    public int MaxAmount { get; set; } = -1;
    
    [YamlIgnore] public HumanConfig Human { get; set; }
    
    [YamlIgnore] public ScpConfig Scp { get; set; }
    
    [YamlIgnore] public AdvancedConfig Advanced { get; set; }
}
