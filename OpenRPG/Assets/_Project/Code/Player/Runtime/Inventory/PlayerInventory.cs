using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerInventory
{
    private readonly IInventory _inventory;
    private readonly Subject<Unit> _inventoryChanged = new();
    private readonly Subject<int> _quickSlotChanged = new();

    public int SlotCount => _inventory.SlotCount;
    public IObservable<Unit> InventoryChanged => _inventoryChanged;
    public IObservable<int> QuickSlotChanged => _quickSlotChanged;

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
        bool added = _inventory.Add(item);
        if (!added) return false;
        
        _inventoryChanged.OnNext(Unit.Default);
        return true;
    }

    public bool RemoveItemFromInventory(Item item)
    {
        bool removed = _inventory.Remove(item);
        if (!removed) return false;
        
        _inventoryChanged.OnNext(Unit.Default);
        return true;

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
        bool moved = _inventory.Move(fromIndex, toIndex);
        if (!moved) return false;

        _inventoryChanged.OnNext(Unit.Default);
        return true;
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

    public bool AssignQuickItem(int slotIndex, Item item)
    {
        if (slotIndex < 0 || slotIndex >= _quickSlots.Length)
        {
            return false;
        }

        if (item == null)
            return false;

        QuickItemSlot targetSlot = _quickSlots[slotIndex];

        if (targetSlot.Item == item)
            return true;

        for (int i = 0; i < _quickSlots.Length; i++)
        {
            if (i == slotIndex)
                continue;

            if (_quickSlots[i].Item != item)
                continue;

            _quickSlots[i].Clear();

            _quickSlotChanged.OnNext(i);
        }

        targetSlot.SetItem(item);

        _quickSlotChanged.OnNext(slotIndex);

        return true;
    }

    public void ClearQuickSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _quickSlots.Length)
            return;

        _quickSlots[slotIndex].Clear();
        
        _quickSlotChanged.OnNext(slotIndex);
    }
    
}
