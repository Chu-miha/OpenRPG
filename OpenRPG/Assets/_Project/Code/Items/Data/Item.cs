using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
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

    [field: SerializeField]
    public int Price { get; private set; }

    [field: SerializeField]
    public ItemEffect[] Effects { get; private set; }

    public void Use(IItemUser user)
    {
        foreach (ItemEffect effect in Effects)
        {
            effect.ApplyEffect(user);
        }
    }
}
