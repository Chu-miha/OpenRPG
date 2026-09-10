using UnityEngine;

public class AttributeStat : Stat
{
    public AttributeStat(int value)
    {
        Value = value;
    }

    public override void SetValue(int value)
    {
        if (Value == value)
            return;

        Value = value;
        NotifyValueChanged();
    }

    public override void Modify(int amount)
    {
        SetValue(Value + amount);
    }
}
