using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "RPG/Items/Item")]
public class Item : ScriptableObject
{
    [field: SerializeField]
    public string Id { get; private set; }

    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    [TextArea]
    public string Description { get; private set; }

    [field: SerializeField]
    public Sprite Icon { get; private set; }

    [field: SerializeField]
    public float Weight { get; private set; }

    [field: SerializeField, Min(1)] 
    public int MaxStackSize { get; private set; } = 1;

    [field: SerializeField]
    public int Price { get; private set; }

    [SerializeField]
    private ItemEffect[] _effects;
    public ItemEffect[] Effects => _effects;

    public void Use(IItemUser user)
    {
        foreach (ItemEffect effect in Effects)
        {
            effect.ApplyEffect(user);
        }
    }
}
