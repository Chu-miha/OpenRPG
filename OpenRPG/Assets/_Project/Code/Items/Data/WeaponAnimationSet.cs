using UnityEngine;

[CreateAssetMenu(fileName = "WeaponAnimationSet", menuName = "RPG/Items/Weapon Animation Set")]
public class WeaponAnimationSet : ScriptableObject
{
    [SerializeField] private AnimationClip _attackAnimation;

    public AnimationClip AttackAnimation => _attackAnimation;
}
