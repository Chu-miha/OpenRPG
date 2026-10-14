using UnityEngine;

[CreateAssetMenu(fileName = "AttackDefinition", menuName = "RPG/Combat/Attack Definition")]
public sealed class AttackDefinition : ScriptableObject
{
    [field: SerializeField, Min(0)] 
    public int Damage { get; private set; } = 10;
    
    [field: SerializeField, Min(0f)] 
    public float Cooldown { get; private set; } = 0.5f;
    
    [SerializeField]
    private AttackBehavior _behavior;
    public AttackBehavior Behavior => _behavior;
}
