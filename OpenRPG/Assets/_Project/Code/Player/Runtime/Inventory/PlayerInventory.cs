using UnityEngine;

public class PlayerInventory
{
    private IInventory _inventory;
    
    public PlayerInventory(IInventoryFactory inventoryFactory)
    {
        _inventory = inventoryFactory.Create();;
    }

    public bool AddItemToInventory(Item item)
    {
        return _inventory.Add(item);
    }
    
}
