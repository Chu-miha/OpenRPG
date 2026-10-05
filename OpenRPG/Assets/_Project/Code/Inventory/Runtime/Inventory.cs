using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : IInventory
{
    private readonly List<InventorySlot> _slots = new();

    public int SlotCount => _slots.Count;

    public Inventory(int initialSlotCount)
    {
        if (initialSlotCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(initialSlotCount));

        for (int i = 0; i < initialSlotCount; i++)
        {
            _slots.Add(new InventorySlot());
        }
    }

    public bool Add(Item item, int amount = 1)
    {
        if (item == null || amount <= 0)
            return false;

        InventorySlot existingSlot = FindSlot(item);

        if (existingSlot != null)
        {
            existingSlot.Stack.Add(amount);
            return true;
        }

        InventorySlot emptySlot = FindEmptySlot();

        if (emptySlot == null)
            return false;

        emptySlot.SetStack(new ItemStack(item, amount));

        return true;
    }

    public bool Remove(Item item, int amount = 1)
    {
        if (item == null || amount <= 0)
            return false;

        InventorySlot slot = FindSlot(item);

        if (slot == null)
            return false;

        if (!slot.Stack.Remove(amount))
            return false;

        if (slot.Stack.Quantity == 0)
        {
            slot.Clear();
        }

        return true;
    }

    public int GetQuantity(Item item)
    {
        if (item == null)
            return 0;

        InventorySlot slot = FindSlot(item);

        if (slot == null)
            return 0;

        return slot.Stack.Quantity;
    }

    public InventorySlot GetSlot(int index)
    {
        if (index < 0 || index >= _slots.Count)
            return null;

        return _slots[index];
    }

    public bool Move(int fromIndex, int toIndex)
    {
        if (!IsValidIndex(fromIndex) || !IsValidIndex(toIndex))
            return false;

        if (fromIndex == toIndex)
            return false;

        InventorySlot fromSlot = _slots[fromIndex];
        InventorySlot toSlot = _slots[toIndex];

        ItemStack fromStack = fromSlot.Stack;
        ItemStack toStack = toSlot.Stack;

        fromSlot.SetStack(toStack);
        toSlot.SetStack(fromStack);

        return true;
    }

    public void Expand(int amount)
    {
        if (amount <= 0)
            return;

        for (int i = 0; i < amount; i++)
        {
            _slots.Add(new InventorySlot());
        }
    }

    private InventorySlot FindSlot(Item item)
    {
        foreach (InventorySlot slot in _slots)
        {
            if (!slot.IsEmpty &&
                slot.Stack.Item.Id == item.Id)
            {
                return slot;
            }
        }

        return null;
    }

    private InventorySlot FindEmptySlot()
    {
        foreach (InventorySlot slot in _slots)
        {
            if (slot.IsEmpty)
                return slot;
        }

        return null;
    }

    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < _slots.Count;
    }
}
