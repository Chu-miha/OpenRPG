using UnityEngine;

public interface  IInventoryFactory
{
    IInventory Create(int  slotCount);
}
