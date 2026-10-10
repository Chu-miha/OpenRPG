using UnityEngine;

public class CombatController
{
    private readonly Transform _attacker;
    private readonly AttackDefinition _defaultAttack;

    private float _nextAttackTime;

    public CombatController(Transform attacker, AttackDefinition defaultAttack)
    {
        _attacker = attacker;
        _defaultAttack = defaultAttack;
    }
    
    public bool TryAttack(AttackDefinition definition = null)
    {
        AttackDefinition attack = definition != null ? definition : _defaultAttack;

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
