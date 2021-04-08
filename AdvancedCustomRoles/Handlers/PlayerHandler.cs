using Synapse;
using System.Linq;

namespace AdvancedCustomRoles.Handlers
{
    public class PlayerHandler
    {
        private PluginClass Plugin { get; }

        public PlayerHandler(PluginClass plugin)
        {
            Plugin = plugin;

            Server.Get.Events.Player.PlayerSetClassEvent += SetClass;
        }

        private void SetClass(Synapse.Api.Events.SynapseEventArguments.PlayerSetClassEventArgs ev)
        {
            var id = ev.Player.CustomRole == null ? (int)ev.Role : ev.Player.RoleID;

            if (Plugin.RoundHandler.RespawnPlayers.Contains(ev.Player))
            {
                foreach(var respawnrole in Plugin.RoleHandler.CustomRoles)
                {
                    if(respawnrole.RespawnReplace != null && respawnrole.RespawnReplace.ContainsKey(id) && UnityEngine.Random.Range(1f,100f) <= respawnrole.RespawnReplace[id])
                    {
                        if (respawnrole.MaxAmount >= 0 && Server.Get.Players.Where(x => x.RoleID == respawnrole.RoleID).Count() >= respawnrole.MaxAmount) continue;
                        if (respawnrole.MaxRespawnAmount >= 0 && Plugin.RoundHandler.SpawnedRoles.Where(x => x == respawnrole.RoleID).Count() >= respawnrole.MaxRespawnAmount) continue;

                        ev.Allow = false;
                        Plugin.RoundHandler.RespawnPlayers.Remove(ev.Player);
                        Plugin.RoundHandler.SpawnedRoles.Add(respawnrole.RoleID);
                        ev.Player.RoleID = respawnrole.RoleID;
                        return;
                    }
                }
            }

            var role = Plugin.RoleHandler.CustomRoles.FirstOrDefault(x => x.RoleID == id);
            if (role != null)
            {
                ev.Items = role.Inventory.Select(x => x.Parse()).ToList();
                if(role.Spawns.Count > 0)
                    ev.Position = role.Spawns.ElementAt(UnityEngine.Random.Range(0, role.Spawns.Count)).Parse().Position;
            }
        }
    }
}
