using Synapse;
using Synapse.Api;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedCustomRoles.Handlers
{
    public class RoundHandler
    {
        public PluginClass PluginClass { get; set; }

        public List<Player> RespawnPlayers { get; set; } = new List<Player>();

        public List<int> SpawnedRoles { get; set; } = new List<int>();

        public RoundHandler(PluginClass plugin)
        {
            PluginClass = plugin;

            Server.Get.Events.Round.SpawnPlayersEvent += SpawnPlayers;
            Server.Get.Events.Round.TeamRespawnEvent += Respawn;
        }

        private void Respawn(Synapse.Api.Events.SynapseEventArguments.TeamRespawnEventArgs ev)
        {
            RespawnPlayers = ev.Players;
            MEC.Timing.CallDelayed(0.3f, () =>
            {
                RespawnPlayers.Clear();
                SpawnedRoles.Clear();
            });
        }

        private void SpawnPlayers(Synapse.Api.Events.SynapseEventArguments.SpawnPlayersEventArgs ev)
        {
            var list = new List<int>();

            foreach(var role in PluginClass.RoleHandler.CustomRoles)
            {
                foreach (var spawn in ev.SpawnPlayers.Where(x => role.RoundStartReplace.Keys.Contains(x.Value)).ToList())
                {
                    if (role.MaxAmount >= 0 && list.Where(x => x == role.RoleID).Count() >= role.MaxSpawnAmount) continue;
                    if (role.MaxSpawnAmount >= 0 && list.Where(x => x == role.RoleID).Count() >= role.MaxSpawnAmount) continue;

                    if (UnityEngine.Random.Range(1f,100f) <= role.RoundStartReplace.FirstOrDefault(x => x.Key == spawn.Value).Value)
                    {
                        ev.SpawnPlayers[spawn.Key] = role.RoleID;
                        list.Add(role.RoleID);
                    }
                }
            }
        }
    }
}
