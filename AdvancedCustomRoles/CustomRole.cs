using Synapse.Config;
using System.Collections.Generic;

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
        
        public bool GodMode { get; set; } = false;

        public float SpawnHealth { get; set; } = 100f;

        public int MaxHealth { get; set; } = 100;

        public string DisplayInfo { get; set; } = "";

        public bool RemoveRoleName { get; set; } = false;

        public ushort SpawnMessageTime { get; set; } = 5;

        public string SpawnBroadcast { get; set; } = "";

        public string SpawnHint { get; set; } = "";

        public string SpawnWindow { get; set; } = "";

        public SerializedPlayerInventory Inventory = new SerializedPlayerInventory();

        public List<SerializedMapPoint> Spawns { get; set; } = new List<SerializedMapPoint>();

        public Dictionary<int, float> RoundStartReplace { get; set; } = new Dictionary<int, float>();

        public Dictionary<int, float> RespawnReplace { get; set; } = new Dictionary<int, float>();

        public int MaxSpawnAmount { get; set; } = -1;

        public int MaxRespawnAmount { get; set; } = -1;

        public int MaxAmount { get; set; } = -1;
    }
}
