using UnityEngine;

public class ItemStack
{
    public Item Item { get; }
    public int Quantity { get; private set; }

    public ItemStack(Item item, int quantity)
    {
        if (item == null)
            throw new System.ArgumentNullException(nameof(item));

        if (quantity <= 0 || quantity > item.MaxStackSize)
            throw new System.ArgumentOutOfRangeException(nameof(quantity));
        
        Item = item;
        Quantity = quantity;
    }

    public void Add(int amount)
    {
        if (amount <= 0 || Quantity + amount > Item.MaxStackSize)
            return;
        
        Quantity += amount;
    }

    public bool Remove(int amount)
    {
        if (amount <= 0 || amount > Quantity)
            return false;

        Quantity -= amount;
        return true;
    }
}
