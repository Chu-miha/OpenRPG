using UnityEngine;

[CreateAssetMenu(fileName = "RestoreManaEffect", menuName = "Scriptable Objects/Effect/RestoreManaEffect")]
public class RestoreManaEffect : ItemEffect
{
    [field: SerializeField]
    public int Amount { get; private set; }

    public override void ApplyEffect(IItemUser user)
    {
        if (!user.TryGet<IManaUser>(out IManaUser manaUser))
            return;

        manaUser.Mana.Modify(Amount);
    }
}
