using System;
using UnityEngine;

public abstract class Stat : IStat
{
    public int Value { get; protected set; }
    
    public event Action<int> ValueChanged;

    public abstract void SetValue(int value);
    public abstract void Modify(int amount);
    
    protected void NotifyValueChanged()
    {
        ValueChanged?.Invoke(Value);
    }


}
