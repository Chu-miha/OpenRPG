using UnityEngine;

[CreateAssetMenu(fileName = "AttackableItem", menuName = "RPG/Items/Attackable Item")]
public class AttackableItem : Item
{
    [field: SerializeField]
    public AttackDefinition AttackDefinition { get; private set; }
}
