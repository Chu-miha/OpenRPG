using UnityEngine;

public class CameraModeEntry
{
    public CameraModeType Type { get; }
    public ICameraMode Mode { get; }

    public CameraModeEntry(CameraModeType type, ICameraMode mode)
    {
        Type = type;
        Mode = mode;
    }
}
