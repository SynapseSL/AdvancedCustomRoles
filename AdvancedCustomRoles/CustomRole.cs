using Synapse.Config;
using System.Collections.Generic;
using UnityEngine;

namespace AdvancedCustomRoles
{
    public class CustomRole : IConfigSection
    {
        public string Name { get; set; } = "Null";

        public int RoleID { get; set; } = 0;

        public int TeamID { get; set; } = 0;

        public List<int> Enemies { get; set; } = new List<int>();

        public List<int> Friends { get; set; } = new List<int>();

        public RoleType Spawnrole { get; set; } = RoleType.ClassD;

        public int EscapeRole { get; set; } = -1;

        public float SpawnHealth { get; set; } = 100f;

        public int MaxHealth { get; set; } = 100;

        public string DisplayInfo { get; set; } = "";

        public bool RemoveRoleName { get; set; } = false;

        public ushort SpawnMessageTime { get; set; } = 5;

        public string SpawnBroadcast { get; set; } = "";

        public string SpawnHint { get; set; } = "";

        public string SpawnWindow { get; set; } = "";

        public List<SerializedItem> Inventory { get; set; } = new List<SerializedItem>();

        public Ammo Ammo { get; set; } = new Ammo();

        public List<SerializedMapPoint> Spawns { get; set; } = new List<SerializedMapPoint>();

        public Dictionary<int, float> RoundStartReplace { get; set; } = new Dictionary<int, float>();

        public Dictionary<int, float> RespawnReplace { get; set; } = new Dictionary<int, float>();

        public int MaxSpawnAmount { get; set; } = -1;

        public int MaxRespawnAmount { get; set; } = -1;

        public int MaxAmount { get; set; } = -1;
    }

    public class Ammo
    {
        public uint Ammo5 { get; set; } = 0;
        public uint Ammo7 { get; set; } = 0;
        public uint Ammo9 { get; set; } = 0;
    }
}
