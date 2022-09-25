using System.Linq;
using Synapse3.SynapseModule.Events;
using Synapse3.SynapseModule.Player;

namespace AdvancedCustomRoles.Handlers;

public class PlayerHandler
{
    private readonly AdvancedCustomRoles _plugin;
    private readonly PlayerService _player;

    public PlayerHandler(AdvancedCustomRoles plugin, PlayerEvents playerEvents, PlayerService player)
    {
        _plugin = plugin;
        _player = player;

        playerEvents.SetClass.Subscribe(SetClass);
    }

    private void SetClass(SetClassEvent ev)
    {
        //CustomRoles are to unpredictable to replace
        if (ev.Player.CustomRole != null) return;
        var id = (uint)ev.Role;

        //This handles the Players that will be respawned
        if (_plugin.RoundHandler.RespawnPlayers.Contains(ev.Player))
        {
            foreach(var respawnRole in _plugin.RoleHandler.CustomRoles)
            {
                if (respawnRole.RespawnReplace == null || !respawnRole.RespawnReplace.ContainsKey(id) ||
                    UnityEngine.Random.Range(1f, 100f) > respawnRole.RespawnReplace[id]) continue;
                
                if (respawnRole.MaxAmount >= 0 && _player.GetPlayers(x => x.RoleID == respawnRole.RoleId).Count() >=
                    respawnRole.MaxAmount) continue;

                if (respawnRole.MaxRespawnAmount >= 0 &&
                    _plugin.RoundHandler.SpawnedRoles.Count(x => x == respawnRole.RoleId) >=
                    respawnRole.MaxRespawnAmount) continue;

                ev.Allow = false;
                _plugin.RoundHandler.RespawnPlayers.Remove(ev.Player);
                _plugin.RoundHandler.SpawnedRoles.Add(respawnRole.RoleId);
                ev.Player.RoleID = respawnRole.RoleId;
                return;
            }
        }
    }
}
