
using AdvancedCustomRoles.Handlers;
using Neuron.Core.Plugins;
using Synapse3.SynapseModule;
using Synapse3.SynapseModule.Events;

namespace AdvancedCustomRoles;

[Plugin(
    Name = "AdvancedCustomRoles",
    Author = "Dimenzio",
    Description = "Allows to create new Role with just a few config files",
    Version = "3.0.0"
)]
[HeavyModded]
public class AdvancedCustomRoles : ReloadablePlugin
{
    public CustomRoleHandler RoleHandler { get; private set; }
    public PlayerHandler PlayerHandler { get; private set; }
    public ScpHandler ScpHandler { get; private set; }
    public RoundHandler RoundHandler { get; private set; }

    public override void EnablePlugin()
    {
        RoleHandler = Synapse.GetOrCreate<CustomRoleHandler>();
        RoleHandler.Load();

        PlayerHandler = Synapse.GetOrCreate<PlayerHandler>();
        RoundHandler = Synapse.GetOrCreate<RoundHandler>();
        ScpHandler = Synapse.GetOrCreate<ScpHandler>();
        
        Logger.Info("AdvancedCustomRoles Loaded");
    }

    public override void Reload(ReloadEvent _ = null) => RoleHandler?.Reload();
}