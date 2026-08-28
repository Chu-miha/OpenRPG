using UnityEngine;

public class CameraTarget : MonoBehaviour, ICameraTarget
{
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
}
