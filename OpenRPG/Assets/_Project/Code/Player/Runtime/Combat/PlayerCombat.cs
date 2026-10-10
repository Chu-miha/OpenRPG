using UnityEngine;

public class PlayerCombat
{
    private readonly CombatController _combatController;

    public PlayerCombat(ICombatControllerFactory combatControllerFactory, UnityEngine.Transform playerTransform, AttackDefinition defaultAttack)
    {
        _combatController = combatControllerFactory.Create(playerTransform, defaultAttack);
    }

    public bool TryAttack()
    {
        return _combatController.TryAttack();
    }
}
