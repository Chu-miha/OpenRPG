using UnityEngine;

public interface IMovement
{
    Vector3 Velocity { get; }
    bool IsGrounded { get; }
}
