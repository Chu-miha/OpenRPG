using System;
using UniRx;
using UnityEngine;

public abstract class Stat : IStat
{
    protected readonly ReactiveProperty<int> ValueProperty;

    public int Value => ValueProperty.Value;
    public IReadOnlyReactiveProperty<int> ReactiveValue => ValueProperty;

    protected Stat(int value)
    {
        ValueProperty = new ReactiveProperty<int>(value);
    }

    public abstract void SetValue(int value);
    public abstract void Modify(int amount);

}
