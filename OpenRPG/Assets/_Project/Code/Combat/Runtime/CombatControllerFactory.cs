using UnityEngine;

public class CombatControllerFactory : ICombatControllerFactory
{
    public CombatController Create(Transform attacker)
    {
        return new CombatController(attacker);
    }
}
