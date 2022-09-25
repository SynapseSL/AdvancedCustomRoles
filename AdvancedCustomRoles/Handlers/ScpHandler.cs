using Synapse3.SynapseModule.Events;

namespace AdvancedCustomRoles.Handlers;

public class ScpHandler
{
    public ScpHandler(ScpEvents scp)
    {
        scp.Scp049Attack.Subscribe(ScpAttack);
        scp.Scp096Attack.Subscribe(ScpAttack);
        scp.Scp0492Attack.Subscribe(ScpAttack);
        scp.Scp173Attack.Subscribe(ScpAttack);
        scp.Scp939Attack.Subscribe(ScpAttack);
        scp.Scp106Attack.Subscribe(Scp106Attack);
    }

    private void ScpAttack(ScpAttackEvent ev)
    {
        if (ev.Scp.CustomRole is not AdvancedRoleScript script) return;
        if(script.RoleConfig?.Scp?.ScpAttackDamage == null) return;
        ev.Damage = script.RoleConfig.Scp.ScpAttackDamage;
    }

    private void Scp106Attack(Scp106AttackEvent ev)
    {
        if (ev.Scp.CustomRole is not AdvancedRoleScript script) return;
        if(script.RoleConfig?.Scp == null) return;
        ev.Damage = script.RoleConfig.Scp.ScpAttackDamage;
        ev.TakeToPocket = script.RoleConfig.Scp.Scp106TakeIntoPocket;
    }
}