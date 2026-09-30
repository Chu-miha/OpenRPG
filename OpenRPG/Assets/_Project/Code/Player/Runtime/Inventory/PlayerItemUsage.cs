using UnityEngine;

public class PlayerItemUsage
{
    private readonly PlayerInventory _playerInventory;

    public PlayerItemUsage(PlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;
    }

    public void UseQuickItem(int slotIndex, IItemUser user)
    {
        Item item = _playerInventory.GetQuickItem(slotIndex);

        if (item == null)
            return;

        if (_playerInventory.GetItemQuantity(item) <= 0)
        {
            _playerInventory.ClearQuickSlot(slotIndex);
            return;
        }

        item.Use(user);

        _playerInventory.RemoveItemFromInventory(item);

        if (_playerInventory.GetItemQuantity(item) <= 0)
        {
            _playerInventory.ClearQuickSlot(slotIndex);
        }
    }
}
