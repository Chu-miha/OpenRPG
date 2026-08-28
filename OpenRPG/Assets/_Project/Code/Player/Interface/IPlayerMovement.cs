using UnityEngine;

public interface IPlayerMovement
{
    Vector3 Velocity { get; }
    bool IsGrounded { get; }
}
