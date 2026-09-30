using UnityEngine;

public class QuickItemSlot
{
    public Item Item { get; private set; }

    public void SetItem(Item item)
    {
        Item = item;
    }

    public void Clear()
    {
        Item = null;
    }
}
