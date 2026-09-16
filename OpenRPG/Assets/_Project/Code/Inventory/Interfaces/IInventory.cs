using UnityEngine;

public interface IInventory
{
    bool Add(Item item, int amount = 1);
    bool Remove(Item item, int amount = 1);
    int GetQuantity(Item item);
}
