using System;
using System.Collections.Generic;
using PlayerRoles;
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

    public RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
    public RoleTypeId VisibleRole { get; set; } = RoleTypeId.None;
    public RoleTypeId OwnRole { get; set; } = RoleTypeId.None;
    public uint EscapeRole { get; set; } = uint.MaxValue;
    public float Health { get; set; } = 100;
    public float MaxHealth { get; set; } = 100;
    public float ArtificialHealth { get; set; } = 0;
    public float MaxArtificialHealth { get; set; } = 75;
    public RoomPoint[] PossibleSpawns { get; set; }
    public SerializedPlayerInventory[] PossibleInventories { get; set; }
    public bool CustomDisplay { get; set; } = true;
    public bool Hierarchy { get; set; } = false;
    public bool UseCustomUnitName { get; set; } = false;
    public string CustomUnitName { get; set; } = "";
    public List<uint> TeamsThatCanSeeUnit { get; set; } = new List<uint>();
    public SerializedVector3 Scale { get; set; } = new(1f, 1f, 1f);
    public bool GodMode { get; set; } = false;
    public string DisplayInfo { get; set; } = "";
    public ushort SpawnMessageTime { get; set; } = 5;
    public string SpawnBroadcast { get; set; } = "";
    public string SpawnHint { get; set; } = "";
    public string SpawnWindow { get; set; } = "";

    public float SetPlayerAtRoundStartChance { get; set; } = 1;

    public bool SpawnOneScpLessOnSpawn { get; set; } = false;

    public Dictionary<uint, float> RespawnReplace { get; set; } = new();

    public int MaxSpawnAmount { get; set; } = -1;

    public int MaxRespawnAmount { get; set; } = -1;

    public int MaxAmount { get; set; } = -1;

    [YamlIgnore] public ScpConfig Scp { get; set; }
    
    [YamlIgnore] public AdvancedConfig Advanced { get; set; }
}
