using UnityEngine;

public class CombatController
{
    private readonly Transform _attacker;

    private float _nextAttackTime;

    public CombatController(Transform attacker)
    {
        _attacker = attacker;
    }
    
    public bool TryAttack(AttackDefinition attack)
    {

        if (_attacker == null || attack == null || attack.Behavior == null)
        {
            return false;
        }

        if (Time.time < _nextAttackTime)
            return false;

        _nextAttackTime = Time.time + attack.Cooldown;

        attack.Behavior.Execute(attack, _attacker);
        
        return true;
    }
}
