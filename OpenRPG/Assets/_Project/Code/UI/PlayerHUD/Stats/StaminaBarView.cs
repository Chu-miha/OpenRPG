using UnityEngine;
using UnityEngine.UIElements;

public class StaminaBarView : ResourceBarView
{
    public StaminaBarView(VisualElement root)
        : base(
            root.Q<VisualElement>("StaminaFill"),
            root.Q<Label>("StaminaValue"))
    { }
}
