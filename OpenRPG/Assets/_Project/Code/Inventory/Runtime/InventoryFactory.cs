using UnityEngine;

public class InventoryFactory : IInventoryFactory
{
    public IInventory Create(int  slotCount)
    {
        return new Inventory(slotCount);
    }
}
