using UnityEngine;

public class ThirdPersonInteractionDetector : IInteractionDetector
{
    private const float INTERACTION_DISTANCE = 2f;
    private const float INTERACTION_ANGLE = 60f;

    public IInteractable Detect(IInteractor interactor)
    {
        Collider[] colliders = Physics.OverlapSphere(interactor.Transform.position, INTERACTION_DISTANCE);

        IInteractable closestInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();

            if (interactable == null) continue;

            Vector3 direction = collider.transform.position - interactor.Transform.position;

            direction.y = 0f;

            if (direction.sqrMagnitude < 0.001f) continue;

            float angle = Vector3.Angle(interactor.Transform.forward, direction.normalized);

            if (angle > INTERACTION_ANGLE) continue;

            float distance = direction.sqrMagnitude;

            if (distance >= closestDistance) continue;

            closestDistance = distance;
            closestInteractable = interactable;
        }

        return closestInteractable;
    }
}
