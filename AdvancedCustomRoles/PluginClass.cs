using Synapse.Api.Plugin;
using AdvancedCustomRoles.Handlers;

namespace AdvancedCustomRoles
{
    [PluginInformation(
        Name = "AdvancedCustomRoles",
        Author = "Dimenzio",
        Description = "A Plugin for creating new Roles without c# knowledge",
        LoadPriority = 100,
        SynapseMajor = 2,
        SynapseMinor = 8,
        SynapsePatch = 3,
        Version = "v.1.0.2"
        )]
    public class PluginClass : AbstractPlugin
    {
        public CustomRoleHandler RoleHandler { get; private set; }

        public PlayerHandler PlayerHandler { get; set; }

        public RoundHandler RoundHandler { get; set; }

        public override void Load()
        {
            AdvancedRoleScript.Plugin = this;
            RoleHandler = new CustomRoleHandler();
            RoleHandler.Load();
            PlayerHandler = new PlayerHandler(this);
            RoundHandler = new RoundHandler(this);
            base.Load();
        }

        public override void ReloadConfigs() => RoleHandler.Reload();
    }
}
