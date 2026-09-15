using UnityEngine;

public interface ICameraRaycast
{
    bool Raycast(Vector3 origin, Vector3 direction,float distance, out RaycastHit hit);
}
