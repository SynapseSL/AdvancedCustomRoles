
using AdvancedCustomRoles.Handlers;
using Neuron.Core.Plugins;
using Synapse3.SynapseModule;
using Synapse3.SynapseModule.Events;

namespace AdvancedCustomRoles;

[Plugin(
    Name = "AdvancedCustomRoles",
    Author = "Dimenzio",
    Description = "Allows to create new Role with just a few config files",
    Version = "1.0.0"
)]
public class AdvancedCustomRoles : ReloadablePlugin
{
    public CustomRoleHandler RoleHandler { get; private set; }
    public PlayerHandler PlayerHandler { get; private set; }
    public ScpHandler ScpHandler { get; private set; }
    public RoundHandler RoundHandler { get; private set; }

    public override void EnablePlugin()
    {
        RoleHandler = Synapse.GetAndBind<CustomRoleHandler>();
        RoleHandler.Load();

        PlayerHandler = Synapse.GetAndBind<PlayerHandler>();
        RoundHandler = Synapse.GetAndBind<RoundHandler>();
        ScpHandler = Synapse.GetAndBind<ScpHandler>();
        
        Logger.Info("AdvancedCustomRoles Loaded");
    }

    public override void Reload(ReloadEvent _ = null) => RoleHandler?.Reload();
}