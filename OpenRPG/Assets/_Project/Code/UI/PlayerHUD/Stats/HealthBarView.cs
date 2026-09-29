using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarView : ResourceBarView
{
    public HealthBarView(VisualElement healthBar) 
        : base(
            healthBar.Q<VisualElement>("HealthFill"), 
        healthBar.Q<Label>("HealthValue"))
    { }
   
}
