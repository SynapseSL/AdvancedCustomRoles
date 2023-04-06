using System.Linq;
using Neuron.Core.Events;
using Neuron.Core.Meta;
using Ninject;
using PlayerRoles;
using Synapse3.SynapseModule.Dummy;
using Synapse3.SynapseModule.Events;
using Synapse3.SynapseModule.Player;
using UnityEngine;

namespace AdvancedCustomRoles.Handlers;

[Automatic]
public class PlayerHandler : Listener
{
    [Inject]
    public AdvancedCustomRoles Plugin { get; set; }
    
    [Inject]
    public PlayerService Player { get; set; }

    [EventHandler]
    public void SetClass(SetClassEvent ev)
    {
        //CustomRoles are to unpredictable to replace
        if (ev.Player.CustomRole != null) return;
        var id = (uint)ev.Role;

        //This handles the Players that will be respawned
        if (Plugin.RoundHandler.RespawnPlayers.Contains(ev.Player))
        {
            foreach(var respawnRole in Plugin.RoleHandler.CustomRoles)
            {
                if (respawnRole.RespawnReplace == null || !respawnRole.RespawnReplace.ContainsKey(id) ||
                    UnityEngine.Random.Range(1f, 100f) > respawnRole.RespawnReplace[id]) continue;
                
                if (respawnRole.MaxAmount >= 0 && Player.GetPlayers(x => x.RoleID == respawnRole.RoleId).Count() >=
                    respawnRole.MaxAmount) continue;

                if (respawnRole.MaxRespawnAmount >= 0 &&
                    Plugin.RoundHandler.SpawnedRoles.Count(x => x == respawnRole.RoleId) >=
                    respawnRole.MaxRespawnAmount) continue;

                ev.Allow = false;
                Plugin.RoundHandler.RespawnPlayers.Remove(ev.Player);
                Plugin.RoundHandler.SpawnedRoles.Add(respawnRole.RoleId);
                ev.Player.RoleID = respawnRole.RoleId;
                return;
            }
        }
    }
}
