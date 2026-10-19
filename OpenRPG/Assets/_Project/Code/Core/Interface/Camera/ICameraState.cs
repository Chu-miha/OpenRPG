using System;
using UnityEngine;

public interface ICameraState
{
    ICameraMode CurrentMode { get; }
    IObservable<CameraModeType> CameraModeChanged { get; }
}
