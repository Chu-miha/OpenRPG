using UnityEngine;

public abstract class AttackBehavior : ScriptableObject
{
    public abstract void Execute(AttackDefinition definition, Transform attacker);
}
