using System;
using UnityEngine;

public class ResourceStat : Stat
{
    public int MinValue { get; private set; }
    public int MaxValue { get; private set; }
    
    public ResourceStat(int currentValue, int maxValue, int minValue = 0) : base(Math.Clamp(currentValue, minValue, maxValue))
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }
    
    public override void SetValue(int value)
    {
        int newValue = Math.Clamp(value, MinValue, MaxValue);

        if (newValue == Value)
            return;

        ValueProperty.Value = newValue;
    }

    public override void Modify(int amount)
    {
        SetValue(Value + amount);
    }
}
