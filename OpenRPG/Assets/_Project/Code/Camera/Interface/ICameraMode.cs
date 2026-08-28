using UnityEngine;

public interface ICameraMode
{
    CameraModeType Type { get; }
    Vector3 Position { get; }
    Vector3 Forward { get; }
    
    void Activate();
    void Deactivate();
}
