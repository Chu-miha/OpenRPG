using UnityEngine;

public interface IRotation
{
    void Rotate(float yaw);
    void RotateTowards(Vector3 direction);
}
