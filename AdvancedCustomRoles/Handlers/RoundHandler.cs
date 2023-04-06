using System.Collections.Generic;
using System.Linq;
using MEC;
using Neuron.Core.Events;
using Neuron.Core.Meta;
using Ninject;
using PlayerRoles.RoleAssign;
using Synapse3.SynapseModule.Enums;
using Synapse3.SynapseModule.Events;
using Synapse3.SynapseModule.Player;
using UnityEngine;

namespace AdvancedCustomRoles.Handlers;

[Automatic]
public class RoundHandler : Listener
{
    [Inject]
    public AdvancedCustomRoles Plugin { get; set; }
    [Inject]
    public PlayerService PlayerService { get; set; }

    public List<SynapsePlayer> RespawnPlayers { get; private set; } = new();
    public List<uint> SpawnedRoles { get; } = new();

    [EventHandler]
    public void Respawn(SpawnTeamEvent ev)
    {
        RespawnPlayers = ev.Players.ToList();
        Timing.CallDelayed(0.3f, () =>
        {
            RespawnPlayers.Clear();
            SpawnedRoles.Clear();
        });
    }

    [EventHandler]
    public void FirstSpawn(FirstSpawnEvent ev)
    {
        var list = new List<uint>();
        var players = PlayerService.GetPlayers(x => RoleAssigner.CheckPlayer(x.Hub) && !ev.PlayersBlockedFromSpawning.Contains(x), PlayerType.Player);

        foreach (var player in players.ToList())
        {
            foreach (var role in Plugin.RoleHandler.CustomRoles)
            {
                if (players.Count <= ev.AmountOfScpSpawns && !role.SpawnOneScpLessOnSpawn) continue;
                if (role.MaxAmount >= 0 && list.Count(x => x == role.RoleId) >= role.MaxSpawnAmount) continue;
                if (role.MaxSpawnAmount >= 0 && list.Count(x => x == role.RoleId) >= role.MaxSpawnAmount) continue;
                if (Random.Range(1f, 100f) > role.SetPlayerAtRoundStartChance) continue;

                list.Add(role.RoleId);
                ev.PlayersBlockedFromSpawning.Add(player);
                player.RoleID = role.RoleId;
                if (role.SpawnOneScpLessOnSpawn)
                    ev.AmountOfScpSpawns--;
                players.Remove(player);
                break;
            }
        }
    }
}
