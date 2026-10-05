using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InventorySlotView
{
    private readonly VisualElement _root;
    private readonly VisualElement _itemIcon;
    private readonly Label _itemCount;

    public int Index { get; }

    public VisualElement Root => _root;

    public InventorySlotView(VisualElement root, int index)
    {
        _root = root;
        Index = index;

        _itemIcon = root.Q<VisualElement>("ItemIcon");

        _itemCount = root.Q<Label>("ItemCount");
    }

    public void SetSlot(InventorySlot slot)
    {
        if (slot == null || slot.IsEmpty)
        {
            Clear();
            return;
        }

        _itemIcon.style.backgroundImage = new StyleBackground(slot.Stack.Item.Icon);

        _itemCount.text = slot.Stack.Quantity.ToString();
    }

    public void Clear()
    {
        _itemIcon.style.backgroundImage = new StyleBackground();

        _itemCount.text = string.Empty;
    }
}
