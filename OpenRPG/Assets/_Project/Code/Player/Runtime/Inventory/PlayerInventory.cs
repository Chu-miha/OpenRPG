using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private readonly IInventory _inventory;
    public int SlotCount => _inventory.SlotCount;

    private readonly QuickItemSlot[] _quickSlots =
    {
        new QuickItemSlot(),
        new QuickItemSlot()
    };

    public PlayerInventory(IInventoryFactory inventoryFactory)
    {
        _inventory = inventoryFactory.Create(30);
    }

    public bool AddItemToInventory(Item item)
    {
        return _inventory.Add(item);
    }

    public bool RemoveItemFromInventory(Item item)
    {
        return _inventory.Remove(item);
    }

    public int GetItemQuantity(Item item)
    {
        return _inventory.GetQuantity(item);
    }
    
    public InventorySlot GetSlot(int index)
    {
        return _inventory.GetSlot(index);
    }

    public bool MoveItem(int fromIndex, int toIndex)
    {
        return _inventory.Move(fromIndex, toIndex);
    }

    public void ExpandInventory(int amount)
    {
        _inventory.Expand(amount);
    }

    public Item GetQuickItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _quickSlots.Length)
            return null;

        return _quickSlots[slotIndex].Item;
    }

    public void AssignQuickItem(int slotIndex, Item item)
    {
        if (slotIndex < 0 || slotIndex >= _quickSlots.Length)
            return;

        _quickSlots[slotIndex].SetItem(item);
    }

    public void ClearQuickSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _quickSlots.Length)
            return;

        _quickSlots[slotIndex].Clear();
    }
    
}
