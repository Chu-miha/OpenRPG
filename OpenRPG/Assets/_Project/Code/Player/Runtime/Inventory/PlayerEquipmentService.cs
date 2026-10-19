using UnityEngine;

public class PlayerEquipmentService
{
    private readonly PlayerInventory _inventory;
    private readonly PlayerEquipment _equipment;

    public PlayerEquipmentService(PlayerInventory inventory, PlayerEquipment equipment)
    {
        _inventory = inventory;
        _equipment = equipment;
    }

    public bool EquipFromInventory(int inventorySlotIndex, int equipmentSlotIndex)
    {
        InventorySlot inventorySlot = _inventory.GetSlot(inventorySlotIndex);

        if (inventorySlot == null || inventorySlot.IsEmpty)
            return false;

        if (inventorySlot.Stack.Quantity != 1)
            return false;

        if (inventorySlot.Stack.Item is not AttackableItem item)
            return false;

        if (!_equipment.TryEquip(equipmentSlotIndex, item))
            return false;

        if (!_inventory.RemoveItemFromSlot(inventorySlotIndex))
        {
            _equipment.TryUnequip(equipmentSlotIndex, out _);
            return false;
        }

        return true;
    }

    public bool UnequipToInventory(int equipmentSlotIndex)
    {
        AttackableItem item = _equipment.GetCombatSlot(equipmentSlotIndex);

        if (item == null)
            return false;

        if (!_inventory.AddItemToInventory(item))
            return false;

        if (!_equipment.TryUnequip(equipmentSlotIndex, out _))
        {
            _inventory.RemoveItemFromInventory(item);
            return false;
        }

        return true;
    }
    
    public bool UnequipToInventory(int equipmentSlotIndex, int inventorySlotIndex)
    {
        AttackableItem item = _equipment.GetCombatSlot(equipmentSlotIndex);

        if (item == null)
            return false;

        if (!_inventory.AddItemToSlot(inventorySlotIndex, item))
            return false;

        if (!_equipment.TryUnequip(equipmentSlotIndex, out _))
        {
            _inventory.RemoveItemFromSlot(inventorySlotIndex);
            return false;
        }

        return true;
    }
}
