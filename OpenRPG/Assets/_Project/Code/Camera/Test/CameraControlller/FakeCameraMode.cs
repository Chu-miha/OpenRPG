using UnityEngine;

public class FakeCameraMode : ICameraMode
{
    public CameraModeType Type { get; }

    public bool IsActivated { get; private set; }
    public bool IsDeactivated { get; private set; }
    
    public int ActivateCount { get; private set; }
    public int DeactivateCount { get; private set; }

    public Vector3 Position => Vector3.zero;
    public Vector3 Forward => Vector3.forward;

    public FakeCameraMode(CameraModeType type)
    {
        Type = type;
    }

    public void Activate()
    {
        IsActivated = true;
        IsDeactivated = false;
        ActivateCount++;
    }

    public void Deactivate()
    {
        IsDeactivated = true;
        IsActivated = false;
        DeactivateCount++;
    }
}

public class FakeFirstPersonCamera : FakeCameraMode
{
    public FakeFirstPersonCamera()
        : base(CameraModeType.FirstPerson)
    {
    }
}

public class FakeThirdPersonCamera : FakeCameraMode
{
    public FakeThirdPersonCamera()
        : base(CameraModeType.ThirdPerson)
    {
    }
}  
    

