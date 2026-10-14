using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UIElements;

public class InventoryView
{
    private readonly VisualElement _root;
    private readonly VisualElement _itemGrid;
    private readonly VisualTreeAsset _itemSlotTemplate;

    private readonly List<InventorySlotView> _slotViews = new();

    private PlayerInventory _playerInventory;

    private InventoryDragAndDrop _dragAndDrop;
    
    private IDisposable _inventorySubscription;

    public InventoryView( VisualElement root, VisualTreeAsset itemSlotTemplate)
    {
        _root = root;
        _itemSlotTemplate = itemSlotTemplate;

        _itemGrid = root.Q<VisualElement>("ItemGrid");
    }

    public void SetInventory(PlayerInventory playerInventory, 
        QuickSlotsView quickSlotsView,  
        PlayerEquipmentService equipmentService, 
        PlayerEquipment equipment, 
        IReadOnlyList<CombatSlotView> combatSlotViews)
    {
        _playerInventory = playerInventory;
        _inventorySubscription?.Dispose();

        _inventorySubscription = _playerInventory.InventoryChanged
            .Subscribe(_ => UpdateAll(_playerInventory));
        
        _dragAndDrop = new InventoryDragAndDrop(_root, _playerInventory, _slotViews, quickSlotsView.SlotViews, equipmentService, equipment, combatSlotViews);;
    }

    public void Build(int slotCount)
    {
        _itemGrid.Clear();
        _slotViews.Clear();

        for (int i = 0; i < slotCount; i++)
        {
            VisualElement slotElement = _itemSlotTemplate.Instantiate();

            InventorySlotView slotView = new InventorySlotView(slotElement, i);

            _slotViews.Add(slotView);

            _itemGrid.Add(slotElement);
        }

        
    }

    public void UpdateSlot(int index, InventorySlot slot)
    {
        if (index < 0 || index >= _slotViews.Count)
        {
            return;
        }
        
        _slotViews[index].SetSlot(slot);
    }

    public void UpdateAll(PlayerInventory inventory)
    {
        for (int i = 0; i < inventory.SlotCount; i++)
        {
            UpdateSlot(i, inventory.GetSlot(i));
        }
    }
    
    public void Dispose()
    {
        _inventorySubscription?.Dispose();
        _inventorySubscription = null;
        
        _dragAndDrop?.Dispose();
        _dragAndDrop = null;
    }
}
