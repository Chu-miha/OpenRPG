using System;
using UnityEngine;

public class ResourceStat : Stat
{
    public int MinValue { get; private set; }
    public int MaxValue { get; private set; }
    
    public ResourceStat(int currentValue, int maxValue, int minValue = 0)
    {
        MinValue = minValue;
        MaxValue = maxValue;

        Value = Math.Clamp(currentValue, MinValue, MaxValue);
    }
    
    public override void SetValue(int value)
    {
        int newValue = Math.Clamp(value, MinValue, MaxValue);

        if (newValue == Value)
            return;

        Value = newValue;
        NotifyValueChanged();
    }

    public override void Modify(int amount)
    {
        SetValue(Value + amount);
    }
}
