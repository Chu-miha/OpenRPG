using System;
using UnityEngine;

public interface IStat
{
    int Value { get; }

    void SetValue(int value);
    void Modify(int amount);

    event Action<int> ValueChanged;
}
