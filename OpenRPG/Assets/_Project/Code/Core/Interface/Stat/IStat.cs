using System;
using UniRx;
using UnityEngine;

public interface IStat
{
    int Value { get; }

    void SetValue(int value);
    void Modify(int amount);

    IReadOnlyReactiveProperty<int> ReactiveValue { get; }
}
