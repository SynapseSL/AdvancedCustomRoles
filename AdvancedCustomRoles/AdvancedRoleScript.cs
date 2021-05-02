using Synapse.Api;
using System.Collections.Generic;

namespace AdvancedCustomRoles
{
    public class AdvancedRoleScript : Synapse.Api.Roles.Role
    {
        public static PluginClass Plugin { private get; set; }

        public AdvancedRoleScript(CustomRole role) => CustomRole = role;

        public AdvancedRoleScript(int id) => CustomRole = Plugin.RoleHandler.GetCustomRole(id);

        public CustomRole CustomRole { get; }

        public override int GetRoleID() => CustomRole.RoleID;

        public override string GetRoleName() => CustomRole != null ? CustomRole.Name : "NULL";

        public override List<int> GetEnemiesID() => CustomRole != null ? CustomRole.Enemies : new List<int>();

        public override List<int> GetFriendsID() => CustomRole != null ? CustomRole.Friends : new List<int>();

        public override int GetTeamID() => CustomRole != null ? CustomRole.TeamID : 0;

        public override void Spawn()
        {
            Player.RoleType = CustomRole.Spawnrole;
            Player.MaxHealth = CustomRole.MaxHealth;
            Player.Health = CustomRole.SpawnHealth;
            CustomRole.Inventory.Apply(Player);

            if (!string.IsNullOrWhiteSpace(CustomRole.DisplayInfo))
                Player.DisplayInfo = CustomRole.DisplayInfo.Replace("\\n","\n");
            if (CustomRole.RemoveRoleName)
                Player.RemoveDisplayInfo(PlayerInfoArea.Role);

            if (!string.IsNullOrWhiteSpace(CustomRole.SpawnBroadcast))
                Player.SendBroadcast(CustomRole.SpawnMessageTime, CustomRole.SpawnBroadcast.Replace("\\n", "\n"));

            if (!string.IsNullOrWhiteSpace(CustomRole.SpawnHint))
                Player.GiveTextHint(CustomRole.SpawnHint.Replace("\\n", "\n"), CustomRole.SpawnMessageTime);

            if (!string.IsNullOrWhiteSpace(CustomRole.SpawnWindow))
                Player.OpenReportWindow(CustomRole.SpawnWindow.Replace("\\n", "\n"));
        }

        public override void Escape()
        {
            if (CustomRole.EscapeRole > 0)
                Player.RoleID = CustomRole.EscapeRole;
        }

        public override void DeSpawn()
        {
            if (!string.IsNullOrWhiteSpace(CustomRole.DisplayInfo))
                Player.DisplayInfo = string.Empty;
            if (CustomRole.RemoveRoleName)
                Player.AddDisplayInfo(PlayerInfoArea.Role);
        }
    }
}
