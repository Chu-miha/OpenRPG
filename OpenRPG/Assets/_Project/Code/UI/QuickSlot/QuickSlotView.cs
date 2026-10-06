using UnityEngine;
using UnityEngine.UIElements;

public class QuickSlotView
{
    public int Index { get; }

    public VisualElement Root { get; }

    private readonly VisualElement _icon;
    private readonly Label _count;
    private readonly Label _key;

    public QuickSlotView(VisualElement root, int index, string key)
    {
        Root = root;
        Index = index;

        _icon = root.Q<VisualElement>("QuickSlotIcon");

        _count = root.Q<Label>("QuickSlotCount");

        _key = root.Q<Label>("QuickSlotKey");

        _key.text = key;
    }

    public void SetItem(Item item, int quantity)
    {
        _icon.style.backgroundImage = new StyleBackground(item.Icon);

        _count.text = quantity.ToString();
    }
    
    public void SetTarget(bool target)
    {
        if (target)
        {
            Root.AddToClassList("quick-slot--target");
        }
        else
        {
            Root.RemoveFromClassList("quick-slot--target");
        }
    }

    public void Clear()
    {
        _icon.style.backgroundImage = new StyleBackground();

        _count.text = string.Empty;
    }
}
