using UnityEngine;

public class ItemStack
{
    public Item Item { get; }
    public int Quantity { get; private set; }

    public ItemStack(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public void Add(int amount)
    {
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
