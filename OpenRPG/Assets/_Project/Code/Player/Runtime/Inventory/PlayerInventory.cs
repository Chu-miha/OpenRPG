using UnityEngine;

public class PlayerInventory
{
    private IInventory _inventory;
    
    private readonly QuickItemSlot[] _quickSlots =
    {
        new QuickItemSlot(),
        new QuickItemSlot()
    };
    
    public PlayerInventory(IInventoryFactory inventoryFactory)
    {
        _inventory = inventoryFactory.Create();;
    }

    public bool AddItemToInventory(Item item)
    {
        if (!_inventory.Add(item))
            return false;

        AssignToQuickSlot(item);

        return true;
    }
    
    public bool RemoveItemFromInventory(Item item)
    {
        return _inventory.Remove(item);
    }

    public int GetItemQuantity(Item item)
    {
        return _inventory.GetQuantity(item);
    }

    public Item GetQuickItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _quickSlots.Length)
            return null;

        return _quickSlots[slotIndex].Item;
    }

    public void ClearQuickSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _quickSlots.Length)
            return;

        _quickSlots[slotIndex].Clear();
    }
//костыль так как нету ui для инвентаря 
    private void AssignToQuickSlot(Item item)
    {
        if (item == null)
            return;

        if (item.Id == "health_potion")
        {
            _quickSlots[0].SetItem(item);
            return;
        }

        if (item.Id == "mana_potion")
        {
            _quickSlots[1].SetItem(item);
        }
    }
}
