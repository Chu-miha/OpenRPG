using UnityEngine;

public class CameraRaycast : ICameraRaycast
{
    public bool Raycast(Vector3 origin, Vector3 direction, float distance, out RaycastHit hit)
    {
        return Physics.Raycast(origin, direction, out hit, distance);
    }
}
