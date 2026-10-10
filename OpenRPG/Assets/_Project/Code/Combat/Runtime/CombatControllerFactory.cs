using UnityEngine;

public class CombatControllerFactory : ICombatControllerFactory
{
    public CombatController Create(Transform attacker, AttackDefinition defaultAttack)
    {
        return new CombatController(attacker, defaultAttack);
    }
}
