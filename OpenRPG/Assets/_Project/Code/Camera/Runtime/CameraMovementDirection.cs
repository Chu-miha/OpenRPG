using UnityEngine;
using Zenject;

public class CameraMovementDirection : IMovementDirection
{
    private readonly CameraController _cameraController;

    public Vector3 Forward
    {
        get
        {
            Vector3 forward = _cameraController.CurrentMode.Forward;
            forward.y = 0f;

            return forward.normalized;
        }
    }

    public Vector3 Right
    {
        get
        {
            return Vector3.Cross(Vector3.up, Forward).normalized;
        }
    }

    [Inject]
    public CameraMovementDirection(CameraController cameraController)
    {
        _cameraController = cameraController;
    }
}
