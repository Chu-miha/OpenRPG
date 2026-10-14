using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EquipmentView
{
    private readonly VisualElement _root;
    private readonly VisualTreeAsset _combatSlotTemplate;
    private readonly PlayerEquipment _equipment;

    private CombatSlotsView _combatSlotsView;
    
    public IReadOnlyList<CombatSlotView> CombatSlotViews => _combatSlotsView.SlotViews;

    public EquipmentView(
        VisualElement root,
        VisualTreeAsset combatSlotTemplate,
        PlayerEquipment equipment)
    {
        _root = root;
        _combatSlotTemplate = combatSlotTemplate;
        _equipment = equipment;
    }

    public void Build()
    {
        _root.Clear();

        VisualElement layout = new VisualElement();
        layout.AddToClassList("equipment-layout");

        VisualElement armorPlaceholder = new VisualElement();
        armorPlaceholder.AddToClassList("equipment-armor-placeholder");

        Label armorLabel = new Label("Armor");
        armorPlaceholder.Add(armorLabel);

        VisualElement combatSlotsRoot = new VisualElement();
        combatSlotsRoot.AddToClassList("combat-slots");

        layout.Add(armorPlaceholder);
        layout.Add(combatSlotsRoot);

        _root.Add(layout);

        _combatSlotsView = new CombatSlotsView(
            combatSlotsRoot,
            _combatSlotTemplate,
            _equipment);

        _combatSlotsView.Build();
    }

    public void Dispose()
    {
        _combatSlotsView?.Dispose();
    }
}
