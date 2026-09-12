using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Scriptable Objects/Effect/HealEffect")]
public class HealEffect : ItemEffect
{
    [field: SerializeField]
    public int Amount { get; private set; }

    public override void ApplyEffect(IItemUser user)
    {
        if (!user.TryGet<IHealable>(out IHealable healable))
            return;

        healable.Health.Modify(Amount);
    }
}
