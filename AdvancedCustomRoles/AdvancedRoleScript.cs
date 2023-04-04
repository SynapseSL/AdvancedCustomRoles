using System.Collections.Generic;
using Synapse3.SynapseModule.Player;
using Synapse3.SynapseModule.Role;

namespace AdvancedCustomRoles;
public class AdvancedRoleScript : SynapseAbstractRole
{
    public CustomRole RoleConfig;
    private CustomInfoList.CustomInfoEntry _customInfo;
    private readonly CustomRoleHandler _roleHandler;
    private readonly PlayerService _player;

    public AdvancedRoleScript(CustomRoleHandler roleHandler, PlayerService player)
    {
        _roleHandler = roleHandler;
        _player = player;
    }
        
    public override void Load()
    {
        RoleConfig = _roleHandler.GetCustomRole(Attribute.Id);
    }

    protected override IAbstractRoleConfig GetConfig() => RoleConfig;
    public override List<uint> GetEnemiesID() => RoleConfig.Enemies;
    public override List<uint> GetFriendsID() => RoleConfig.Friends;

    protected override bool CanSeeUnit(SynapsePlayer player) => RoleConfig.TeamsThatCanSeeUnit.Contains(player.TeamID);

    protected override void OnSpawn(IAbstractRoleConfig _)
    {
        if (!string.IsNullOrWhiteSpace(RoleConfig.DisplayInfo))
        {
            _customInfo = new CustomInfoList.CustomInfoEntry()
            {
                Info = RoleConfig.DisplayInfo,
                EveryoneCanSee = true
            };
            Player.CustomInfo.Insert(1,_customInfo);   
        }
        if (RoleConfig.GodMode)
            Player.GodMode = true;


        if (!string.IsNullOrWhiteSpace(RoleConfig.SpawnBroadcast))
            Player.SendBroadcast(RoleConfig.SpawnBroadcast.Replace("\\n", "\n"), RoleConfig.SpawnMessageTime);

        if (!string.IsNullOrWhiteSpace(RoleConfig.SpawnHint))
            Player.SendHint(RoleConfig.SpawnHint.Replace("\\n", "\n"), RoleConfig.SpawnMessageTime);

        if (!string.IsNullOrWhiteSpace(RoleConfig.SpawnWindow))
            Player.SendWindowMessage(RoleConfig.SpawnWindow.Replace("\\n", "\n"));
        
        if (RoleConfig.Advanced != null)
        {
            foreach (var command in RoleConfig.Advanced.CommandToExecuteAtSpawn ?? new())
            {
                _player.Host.ExecuteCommand(command.Replace(new Dictionary<string, string>()
                {
                    { "%player%", Player.PlayerId.ToString() },
                    { "%playername%", Player.NickName }
                }));
            }
        }
    }

    protected override void OnDeSpawn(DeSpawnReason reason)
    {
        if (_customInfo != null)
            Player.CustomInfo.Remove(_customInfo);
        
        if (RoleConfig.GodMode)
            Player.GodMode = false;

        if (RoleConfig.Advanced != null)
        {
            foreach (var command in RoleConfig.Advanced.CommandToExecuteAtDeSpawn ?? new())
            {
                _player.Host.ExecuteCommand(command.Replace(new Dictionary<string, string>()
                {
                    { "%player%", Player.PlayerId.ToString() },
                    { "%playername%", Player.NickName }
                }));
            }
        }
    }
}
