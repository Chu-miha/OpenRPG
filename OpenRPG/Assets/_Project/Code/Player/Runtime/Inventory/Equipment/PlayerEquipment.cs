using System;
using UniRx;
using UnityEngine;

public class PlayerEquipment
{
    public const int CombatSlotCount = 3;

    private readonly AttackableItem[] _combatSlots = new AttackableItem[CombatSlotCount];

    private readonly Subject<int> _equipmentChanged = new();

    private int _activeCombatSlot;

    public int ActiveCombatSlot => _activeCombatSlot;

    public AttackableItem CurrentCombatItem =>
        _combatSlots[_activeCombatSlot];

    public AttackDefinition CurrentAttack =>
        CurrentCombatItem != null
            ? CurrentCombatItem.AttackDefinition
            : null;

    public float TotalWeight
    {
        get
        {
            float weight = 0f;

            foreach (AttackableItem item in _combatSlots)
            {
                if (item != null)
                    weight += item.Weight;
            }

            return weight;
        }
    }

    public IObservable<int> EquipmentChanged =>
        _equipmentChanged;

    public AttackableItem GetCombatSlot(int index)
    {
        if (!IsValidSlot(index))
            return null;

        return _combatSlots[index];
    }

    public bool TryEquip(int index, AttackableItem item)
    {
        if (!IsValidSlot(index) || item == null)
            return false;

        if (_combatSlots[index] != null)
            return false;

        _combatSlots[index] = item;

        _equipmentChanged.OnNext(index);

        return true;
    }

    public bool TryUnequip(int index, out AttackableItem item)
    {
        item = null;

        if (!IsValidSlot(index))
            return false;

        if (_combatSlots[index] == null)
            return false;

        item = _combatSlots[index];
        _combatSlots[index] = null;

        _equipmentChanged.OnNext(index);

        return true;
    }

    public bool SetActiveCombatSlot(int index)
    {
        if (!IsValidSlot(index))
            return false;

        if (_activeCombatSlot == index)
            return true;

        _activeCombatSlot = index;

        _equipmentChanged.OnNext(index);

        return true;
    }

    private bool IsValidSlot(int index)
    {
        return index >= 0 && index < CombatSlotCount;
    }
}
