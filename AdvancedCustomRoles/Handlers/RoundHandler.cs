using System.Collections.Generic;
using System.Linq;
using MEC;
using Synapse3.SynapseModule.Events;
using Synapse3.SynapseModule.Player;
using UnityEngine;

namespace AdvancedCustomRoles.Handlers;

public class RoundHandler
{
    private readonly AdvancedCustomRoles _plugin;

    public RoundHandler(AdvancedCustomRoles plugin, RoundEvents roundEvents)
    {
        _plugin = plugin;

        roundEvents.FirstSpawn.Subscribe(FirstSpawn);
        roundEvents.SpawnTeam.Subscribe(Respawn);
    }

    public List<SynapsePlayer> RespawnPlayers { get; private set; } = new();
    public List<uint> SpawnedRoles { get; } = new();

    private void Respawn(SpawnTeamEvent ev)
    {
        RespawnPlayers = ev.Players.ToList();
        Timing.CallDelayed(0.3f, () =>
        {
            RespawnPlayers.Clear();
            SpawnedRoles.Clear();
        });
    }

    private void FirstSpawn(FirstSpawnEvent ev)
    {
        var list = new List<uint>();

        foreach (var role in _plugin.RoleHandler.CustomRoles)
        {
            foreach (var spawn in ev.PlayerAndRoles.Where(x => role.RoundStartReplace.Keys.Contains(x.Value)).ToList())
            {
                //Before this Event it should be impossible to become and keep another Custom Role so we don't have to check the Amount of players with the Role currently
                if (role.MaxAmount >= 0 && list.Count(x => x == role.RoleId) >= role.MaxSpawnAmount) continue;
                if (role.MaxSpawnAmount >= 0 && list.Count(x => x == role.RoleId) >= role.MaxSpawnAmount) continue;

                if (Random.Range(1f, 100f) > role.RoundStartReplace[spawn.Value]) continue;
                ev.PlayerAndRoles[spawn.Key] = role.RoleId;
                list.Add(role.RoleId);
            }
        }
    }
}
