using System.Collections.Generic;
using UnityEngine;

public class Inventory : IInventory
{
    private readonly Dictionary<string, ItemStack> _items = new();

    public bool Add(Item item, int amount = 1)
    {
        if (item == null || amount <= 0)
            return false;

        if (_items.TryGetValue(item.Id, out ItemStack stack))
        {
            stack.Add(amount);
            return true;
        }
        _items.Add(item.Id, new ItemStack(item, amount));
        return true;
    }

    public bool Remove(Item item, int amount = 1)
    {
        if (item == null || amount <= 0)
            return false;

        if (!_items.TryGetValue(item.Id, out ItemStack stack))
            return false;

        if (!stack.Remove(amount))
            return false;

        if (stack.Quantity == 0)
            _items.Remove(item.Id);

        return true;
    }

    public int GetQuantity(Item item)
    {
        if (item == null)
            return 0;

        return _items.TryGetValue(item.Id, out ItemStack stack) ? stack.Quantity : 0;
    }
}
