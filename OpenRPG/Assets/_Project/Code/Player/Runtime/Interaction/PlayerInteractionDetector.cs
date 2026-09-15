using UnityEngine;
using Zenject;

public class PlayerInteractionDetector : IInteractionDetector
{
    private readonly FirstPersonInteractionDetector _firstPersonDetector;
    private readonly ThirdPersonInteractionDetector _thirdPersonDetector;
    private readonly ICameraState _cameraState;

    [Inject]
    public PlayerInteractionDetector(FirstPersonInteractionDetector firstPersonDetector, ThirdPersonInteractionDetector thirdPersonDetector, ICameraState cameraState)
    {
        _firstPersonDetector = firstPersonDetector;
        _thirdPersonDetector = thirdPersonDetector;
        _cameraState = cameraState;
    }

    public IInteractable Detect(IInteractor interactor)
    {
        return _cameraState.CurrentMode.Type switch
        {
            CameraModeType.FirstPerson => _firstPersonDetector.Detect(interactor),

            CameraModeType.ThirdPerson => _thirdPersonDetector.Detect(interactor),

            _ => null
        };
    }

}
