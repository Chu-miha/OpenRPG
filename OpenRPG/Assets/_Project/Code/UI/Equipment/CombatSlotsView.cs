using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatSlotsView
{
    private readonly VisualElement _root;
    private readonly VisualTreeAsset _template;
    private readonly PlayerEquipment _equipment;

    private readonly List<CombatSlotView> _slotViews = new();
    private IDisposable _equipmentSubscription;

    public IReadOnlyList<CombatSlotView> SlotViews => _slotViews;

    public CombatSlotsView(
        VisualElement root,
        VisualTreeAsset template,
        PlayerEquipment equipment)
    {
        _root = root;
        _template = template;
        _equipment = equipment;
    }

    public void Build()
    {
        _root.Clear();
        _slotViews.Clear();

        for (int i = 0; i < PlayerEquipment.CombatSlotCount; i++)
        {
            CombatSlotView slotView = new CombatSlotView(_template, i);

            _root.Add(slotView.Root);
            _slotViews.Add(slotView);
        }

        UpdateAll();

        _equipmentSubscription?.Dispose();
        _equipmentSubscription = _equipment.EquipmentChanged
            .Subscribe(_ => UpdateAll());
    }

    private void UpdateAll()
    {
        for (int i = 0; i < _slotViews.Count; i++)
        {
            AttackableItem item = _equipment.GetCombatSlot(i);
            _slotViews[i].SetItem(item);
            _slotViews[i].SetActive(_equipment.ActiveCombatSlot == i);
        }
    }

    public void Dispose()
    {
        _equipmentSubscription?.Dispose();
        _equipmentSubscription = null;
    }
}
