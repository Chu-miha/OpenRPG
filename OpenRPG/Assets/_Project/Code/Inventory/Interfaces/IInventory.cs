using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    int SlotCount { get; }
    bool Add(Item item, int amount = 1);
    bool Remove(Item item, int amount = 1);
    int GetQuantity(Item item);
    InventorySlot GetSlot(int index);
    bool Move(int fromIndex, int toIndex);
    void Expand(int amount);
}
