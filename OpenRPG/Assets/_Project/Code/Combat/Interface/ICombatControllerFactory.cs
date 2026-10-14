using UnityEngine;

public interface ICombatControllerFactory
{
    CombatController Create(Transform attacker);
}
