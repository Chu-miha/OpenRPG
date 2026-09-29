using UnityEngine;
using UnityEngine.UIElements;

public class ManaBarView : ResourceBarView
{
    public ManaBarView(VisualElement root)
        : base(
            root.Q<VisualElement>("ManaFill"),
            root.Q<Label>("ManaValue"))
    { }
}
