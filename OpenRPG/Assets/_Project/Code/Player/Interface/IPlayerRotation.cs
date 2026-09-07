using UnityEngine;

public interface IPlayerRotation
{
    void Rotate(float yaw);
    void RotateTowards(Vector3 direction);
}
