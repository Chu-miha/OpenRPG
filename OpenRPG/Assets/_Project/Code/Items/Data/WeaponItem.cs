using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItem", menuName = "RPG/Items/Weapon")]
public class WeaponItem : AttackableItem
{
    [SerializeField] private GameObject _visualPrefab;
    [SerializeField] private WeaponAnimationSet _animationSet;
    public GameObject VisualPrefab => _visualPrefab;
    public WeaponAnimationSet AnimationSet => _animationSet;
}
