using UnityEngine;

public class PlayerCarryWeight
{
    private readonly PlayerInventory _inventory;
    private readonly PlayerEquipment _equipment;

    public float InventoryWeight => _inventory.TotalWeight;
    public float EquipmentWeight => _equipment.TotalWeight;

    public float TotalWeight => InventoryWeight + EquipmentWeight;

    public PlayerCarryWeight(PlayerInventory inventory, PlayerEquipment equipment)
    {
        _inventory = inventory;
        _equipment = equipment;
    }
}
