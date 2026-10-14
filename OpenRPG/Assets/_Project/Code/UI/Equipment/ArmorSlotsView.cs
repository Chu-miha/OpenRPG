using UnityEngine;
using UnityEngine.UIElements;

public class ArmorSlotsView
{
    public VisualElement Root { get; }

    public ArmorSlotsView(string title)
    {
        Root = new VisualElement();
        Root.AddToClassList("armor-slot");

        Label label = new Label(title);
        label.AddToClassList("armor-slot__label");

        Root.Add(label);
    }
}
