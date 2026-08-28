using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IPlayerMovement, IPlayerRotation
{
    [SerializeField]
    private float moveSpeed = 5f;
    
    private CharacterController _characterController;
    private IMovementInput _movementInput;
    private IMovementDirection _movementDirection;

    public Vector3 Velocity { get; private set; }
    
    public bool IsGrounded => _characterController.isGrounded;

    [Inject]
    private void Construct(IMovementInput movementInput, IMovementDirection movementDirection)
    {
        _movementInput = movementInput;
        _movementDirection = movementDirection;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 movement = _movementInput.Move;
        
        Vector3 direction =
            _movementDirection.Forward * movement.y +
            _movementDirection.Right * movement.x;

        direction = Vector3.ClampMagnitude(direction, 1f);
        
        Velocity = direction.normalized * moveSpeed;
        
        _characterController.Move(Velocity * Time.deltaTime);
    }

    public void Rotate(float yaw)
    {
        transform.Rotate(0f, yaw, 0f);
    }
}
