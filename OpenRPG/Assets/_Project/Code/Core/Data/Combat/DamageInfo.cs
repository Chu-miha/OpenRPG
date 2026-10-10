using UnityEngine;

public readonly struct DamageInfo
{
    public int Amount { get; }
    public GameObject Source { get; }
    
    public DamageInfo(int amount, GameObject source)
    {
        Amount = Mathf.Max(0, amount);
        Source = source;
    }
}
