using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttackBehavior", menuName = "RPG/Combat/Behaviors/Melee Attack")]
public class MeleeAttackBehavior : AttackBehavior
{
    [SerializeField, Min(0f)]
    private float _forwardDistance = 1.2f;

    [SerializeField, Min(0f)]
    private float _verticalOffset = 1f;

    [SerializeField, Min(0.01f)]
    private float _hitRadius = 0.5f;

    [SerializeField]
    private LayerMask _targetLayers = ~0;

    public override void Execute(AttackDefinition definition, Transform attacker)
    {
        if (definition == null || attacker == null)
            return;

        Vector3 hitCenter = attacker.position + Vector3.up * _verticalOffset + attacker.forward * _forwardDistance;

        Collider[] hits = Physics.OverlapSphere(hitCenter, _hitRadius, _targetLayers, QueryTriggerInteraction.Collide);

        var hitTargets = new HashSet<IDamageable>();

        foreach (Collider hit in hits)
        {
            MonoBehaviour[] behaviours = hit.GetComponentsInParent<MonoBehaviour>();

            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour is not IDamageable target)
                    continue;

                if (behaviour.transform.root == attacker.root)
                    break;

                if (!hitTargets.Add(target))
                    break;

                target.TakeDamage(new DamageInfo(definition.Damage, attacker.gameObject));

                break;
            }
        }
    }
}
