using UnityEngine;
using UnityEngine.UIElements;

public class CombatSlotView
{
    public VisualElement Root { get; }
    public int Index { get; }

    private readonly VisualElement _icon;
    private readonly Label _slotNumber;

    public CombatSlotView(VisualTreeAsset template, int index)
    {
        Index = index;

        Root = template.CloneTree();
        Root.AddToClassList("combat-slot");

        _icon = Root.Q<VisualElement>("ItemIcon");
        _slotNumber = Root.Q<Label>("SlotNumber");

        _slotNumber.text = (index + 1).ToString();
    }

    public void SetItem(AttackableItem item)
    {
        if (item == null || item.Icon == null)
        {
            _icon.style.backgroundImage = StyleKeyword.None;
            return;
        }

        _icon.style.backgroundImage = new StyleBackground(item.Icon);
    }

    public void SetTarget(bool isTarget)
    {
        if (isTarget)
            Root.AddToClassList("combat-slot--target");
        else
            Root.RemoveFromClassList("combat-slot--target");
    }

    public void SetDragging(bool isDragging)
    {
        if (isDragging)
            Root.AddToClassList("combat-slot--dragging");
        else
            Root.RemoveFromClassList("combat-slot--dragging");
    }

    public void SetActive(bool isActive)
    {
        if (isActive)
            Root.AddToClassList("combat-slot--active");
        else
            Root.RemoveFromClassList("combat-slot--active");
    }
}
