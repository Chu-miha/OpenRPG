using UnityEngine;
using Zenject;

public class PlayerRotation : MonoBehaviour, IRotation
{
    [SerializeField] private float rotationSpeed = 10f;
    
    private IMovementInput _movementInput;
    private IMovementDirection _movementDirection;
    private ICameraState _cameraState;
    
    [Inject]
    private void Construct(IMovementInput movementInput, IMovementDirection movementDirection, ICameraState cameraState)
    {
        _movementInput = movementInput;
        _movementDirection = movementDirection;
        _cameraState = cameraState;
    }
    
    private void Update()
    {
        if (_cameraState.CurrentMode.Type != CameraModeType.ThirdPerson)
            return;

        Vector2 movement = _movementInput.Move;

        if (movement.sqrMagnitude < 0.001f)
            return;

        Vector3 direction =
            _movementDirection.Forward * movement.y +
            _movementDirection.Right * movement.x;

        direction = Vector3.ClampMagnitude(direction, 1f);

        RotateTowards(direction);
    }

    
    public void Rotate(float yaw)
    {
        transform.Rotate(0f, yaw, 0f);
    }
    
    public void RotateTowards(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
