using UnityEngine;
using Zenject;

public class FirstPersonInteractionDetector : IInteractionDetector
{
    private const float INTERACTION_DISTANCE = 2f;
    
    private readonly ICameraRaycast _cameraRaycast;
    private readonly ICameraState _cameraState;

    [Inject]
    public FirstPersonInteractionDetector(ICameraState cameraState, ICameraRaycast cameraRaycast)
    {
        _cameraRaycast = cameraRaycast;
        _cameraState = cameraState;
    }
    
    public IInteractable Detect(IInteractor interactor)
    {

        ICameraMode camera = _cameraState.CurrentMode;

        if (!_cameraRaycast.Raycast(
                camera.Position,
                camera.Forward,
                INTERACTION_DISTANCE,
                out RaycastHit hit))
        {
            return null;
        }

        return hit.collider.GetComponent<IInteractable>();
    }
}
