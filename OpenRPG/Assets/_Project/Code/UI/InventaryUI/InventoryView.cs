using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryView
{
    private readonly VisualElement _root;
    private readonly VisualElement _itemGrid;
    private readonly VisualTreeAsset _itemSlotTemplate;

    private readonly List<InventorySlotView> _slotViews = new();

    private PlayerInventory _playerInventory;

    private InventoryDragAndDrop _dragAndDrop;

    public InventoryView( VisualElement root, VisualTreeAsset itemSlotTemplate)
    {
        _root = root;
        _itemSlotTemplate = itemSlotTemplate;

        _itemGrid = root.Q<VisualElement>("ItemGrid");
    }

    public void SetInventory(PlayerInventory playerInventory, QuickSlotsView quickSlotsView)
    {
        _playerInventory = playerInventory;
        _dragAndDrop = new InventoryDragAndDrop(_root, _playerInventory, _slotViews, quickSlotsView.SlotViews);
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
        _dragAndDrop?.Dispose();
        _dragAndDrop = null;
    }
}
