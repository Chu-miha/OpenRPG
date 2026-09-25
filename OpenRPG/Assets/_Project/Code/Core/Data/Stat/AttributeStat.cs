using UnityEngine;

public class AttributeStat : Stat
{
    public AttributeStat(int value) : base(value)
    {
    }

    public override void SetValue(int value)
    {
        if (Value == value)
            return;

        ValueProperty.Value = value;
    }

    public override void Modify(int amount)
    {
        SetValue(Value + amount);
    }
}
