using System.Collections.Generic;
using Neuron.Core.Logging;
using Synapse3.SynapseModule;
using Synapse3.SynapseModule.Map;
using Synapse3.SynapseModule.Player;
using Synapse3.SynapseModule.Role;

namespace AdvancedCustomRoles;
public class AdvancedRoleScript : SynapseAbstractRole
{
    public CustomRole RoleConfig;
    private CustomInfoList.CustomInfoEntry _customInfo;
    private readonly CustomRoleHandler _roleHandler;
    private readonly MapService _map;
    private readonly PlayerService _player;

    public AdvancedRoleScript(CustomRoleHandler roleHandler, MapService map, PlayerService player)
    {
        _roleHandler = roleHandler;
        _map = map;
        _player = player;
    }
        
    public override void Load()
    {
        RoleConfig = _roleHandler.GetCustomRole(Attribute.Id);
    }

    protected override IAbstractRoleConfig GetConfig() => RoleConfig;
    public override List<uint> GetEnemiesID() => RoleConfig.Enemies;
    public override List<uint> GetFriendsID() => RoleConfig.Friends;
        
    protected override void OnSpawn(IAbstractRoleConfig _)
    {
        if (!string.IsNullOrWhiteSpace(RoleConfig.DisplayInfo))
        {
            _customInfo = new CustomInfoList.CustomInfoEntry()
            {
                Info = RoleConfig.DisplayInfo,
                EveryoneCanSee = true
            };
            Player.CustomInfo.Add(_customInfo);   
        }
        if (RoleConfig.GodMode)
            Player.GodMode = true;

        if (RoleConfig.Human != null)
        {
            Player.WalkSpeed = RoleConfig.Human.WalkSpeed;
            Player.SprintSpeed = RoleConfig.Human.SprintSpeed;
        }


        if (!string.IsNullOrWhiteSpace(RoleConfig.SpawnBroadcast))
            Player.SendBroadcast(RoleConfig.SpawnBroadcast.Replace("\\n", "\n"), RoleConfig.SpawnMessageTime);

        if (!string.IsNullOrWhiteSpace(RoleConfig.SpawnHint))
            Player.SendHint(RoleConfig.SpawnHint.Replace("\\n", "\n"), RoleConfig.SpawnMessageTime);

        if (!string.IsNullOrWhiteSpace(RoleConfig.SpawnWindow))
            Player.SendWindowMessage(RoleConfig.SpawnWindow.Replace("\\n", "\n"));

        NeuronLogger.For<Synapse>().Warn("Found Advanced");
        if (RoleConfig.Advanced != null)
        {
            NeuronLogger.For<Synapse>().Warn("Found Advanced");
            foreach (var command in RoleConfig.Advanced.CommandToExecuteAtSpawn ?? new())
            {
                NeuronLogger.For<Synapse>().Warn("Executed Command " + command);
                _player.Host.ExecuteCommand(command.Replace("%player%", Player.PlayerId.ToString()));
            }
        }
    }

    protected override void OnDeSpawn(DeSpawnReason reason)
    {
        if (_customInfo != null)
            Player.CustomInfo.Remove(_customInfo);
        
        if (RoleConfig.GodMode)
            Player.GodMode = false;

        if (RoleConfig.Human != null)
        {
            Player.WalkSpeed = _map.HumanWalkSpeed;
            Player.SprintSpeed = _map.HumanSprintSpeed;
        }
    }
}
