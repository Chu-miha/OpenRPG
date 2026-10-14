using UnityEngine;

public class PlayerCombat
{
    private readonly CombatController _combatController;
    private readonly PlayerEquipment _equipment;
    private readonly AttackDefinition _defaultAttack;

    public PlayerCombat(ICombatControllerFactory combatControllerFactory, PlayerEquipment equipment, Transform playerTransform,  AttackDefinition defaultAttack)
    {
        _combatController = combatControllerFactory.Create(playerTransform);
        _equipment = equipment;
        _defaultAttack = defaultAttack;
    }

    public bool TryAttack()
    {
        AttackDefinition attack = _equipment.CurrentAttack ?? _defaultAttack; 
       return _combatController.TryAttack(attack);
    }
}
