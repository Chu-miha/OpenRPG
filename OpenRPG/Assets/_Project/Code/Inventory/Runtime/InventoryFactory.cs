using UnityEngine;

public class InventoryFactory : IInventoryFactory
{
    public IInventory Create()
    {
        return new Inventory();
    }
}
