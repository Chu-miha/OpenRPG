using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : IInventory
{
    private readonly List<InventorySlot> _slots = new();

    public int SlotCount => _slots.Count;

    public float TotalWeight
    {
        get
        {
            float weight = 0f;

            foreach (InventorySlot slot in _slots)
            {
                if (!slot.IsEmpty)
                    weight += slot.Stack.Item.Weight * slot.Stack.Quantity;
            }

            return weight;
        }
    }

    public Inventory(int initialSlotCount)
    {
        if (initialSlotCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(initialSlotCount));

        for (int i = 0; i < initialSlotCount; i++)
            _slots.Add(new InventorySlot());
    }

    public bool Add(Item item, int amount = 1)
    {
        if (item == null || amount <= 0)
            return false;

        if (GetAvailableCapacity(item) < amount)
            return false;

        int remaining = amount;

        AddToExistingStacks(item, ref remaining);

        if (remaining == 0)
            return true;

        AddToEmptySlots(item, ref remaining);

        return remaining == 0;
    }

    public bool Remove(Item item, int amount = 1)
    {
        if (item == null || amount <= 0)
            return false;

        if (GetQuantity(item) < amount)
            return false;

        int remaining = amount;

        foreach (InventorySlot slot in _slots)
        {
            if (!ContainsItem(slot, item))
                continue;

            int amountToRemove = Math.Min(
                slot.Stack.Quantity,
                remaining
            );

            slot.Stack.Remove(amountToRemove);
            remaining -= amountToRemove;

            if (slot.Stack.Quantity == 0)
                slot.Clear();

            if (remaining == 0)
                return true;
        }

        return true;
    }

    public bool RemoveFromSlot(int index, int amount = 1)
    {
        InventorySlot slot = GetSlot(index);

        if (slot == null || slot.IsEmpty)
            return false;

        if (!slot.Stack.Remove(amount))
            return false;

        if (slot.Stack.Quantity == 0)
            slot.Clear();

        return true;
    }

    public int GetQuantity(Item item)
    {
        if (item == null)
            return 0;

        int quantity = 0;

        foreach (InventorySlot slot in _slots)
        {
            if (ContainsItem(slot, item))
                quantity += slot.Stack.Quantity;
        }

        return quantity;
    }

    public InventorySlot GetSlot(int index)
    {
        if (!IsValidIndex(index))
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
            _slots.Add(new InventorySlot());
    }

    private void AddToExistingStacks(Item item, ref int remaining)
    {
        foreach (InventorySlot slot in _slots)
        {
            if (!ContainsItem(slot, item))
                continue;

            int freeSpace = item.MaxStackSize - slot.Stack.Quantity;

            int amountToAdd = Math.Min(freeSpace, remaining);

            if (amountToAdd <= 0)
                continue;

            slot.Stack.Add(amountToAdd);
            remaining -= amountToAdd;

            if (remaining == 0)
                return;
        }
    }

    private void AddToEmptySlots(Item item, ref int remaining)
    {
        if (remaining <= 0)
            return;
        
        foreach (InventorySlot slot in _slots)
        {
            if (!slot.IsEmpty)
                continue;

            int amountToAdd = Math.Min(
                item.MaxStackSize,
                remaining
            );

            slot.SetStack(new ItemStack(item, amountToAdd));
            remaining -= amountToAdd;

            if (remaining == 0)
                return;
        }
    }

    private int GetAvailableCapacity(Item item)
    {
        int capacity = 0;

        foreach (InventorySlot slot in _slots)
        {
            if (slot.IsEmpty)
            {
                capacity += item.MaxStackSize;
                continue;
            }

            if (ContainsItem(slot, item))
            {
                capacity += Math.Max(
                    0,
                    item.MaxStackSize - slot.Stack.Quantity
                );
            }
        }

        return capacity;
    }

    private bool ContainsItem(InventorySlot slot, Item item)
    {
        return !slot.IsEmpty && slot.Stack.Item.Id == item.Id;
    }

    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < _slots.Count;
    }
}
