using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraController : ITickable , ICameraState
{
    private readonly Dictionary<CameraModeType, ICameraMode> _cameraModes;
    private ICameraMode _currentMode;
    private readonly ICameraModeInput _cameraModeInput;
    private readonly CameraTransition _cameraTransition;

    public ICameraMode CurrentMode => _currentMode;

    public CameraController(List<ICameraMode> cameraModes, CameraModeType defaultMode, ICameraModeInput cameraModeInput, CameraTransition cameraTransition)
    {
        _cameraModes = new Dictionary<CameraModeType, ICameraMode>();

        foreach (ICameraMode mode in cameraModes)
        {
            _cameraModes.Add(mode.Type, mode);
        }
        _cameraModeInput = cameraModeInput;
        
        _cameraTransition = cameraTransition;
        
        SetMode(defaultMode);
    }

    public void SetMode<T>() where T : ICameraMode
    {
        foreach (ICameraMode mode in _cameraModes.Values)
        {
            if (mode is T)
            {
                SetMode(mode.Type);
                return;
            }
        }

        throw new ArgumentException(
            $"Camera mode '{typeof(T).Name}' is not registered.");
    }
    
    public void SetMode(CameraModeType modeType)
    {
        if (!_cameraModes.TryGetValue(modeType, out ICameraMode newMode))
        {
            throw new ArgumentException(
                $"Camera mode '{modeType}' is not registered.");
        }

        if (_currentMode == newMode)
        {
            return;
        }

        _currentMode?.Deactivate();

        _currentMode = newMode;

        _currentMode.Activate();
    }

    public void Tick()
    {
        if (!_cameraModeInput.SwitchCameraPressed)
            return;

        if (_cameraTransition.IsRunning)
            return;

        if (_currentMode.Type == CameraModeType.FirstPerson)
        {
            _cameraTransition.ToThirdPerson(() =>
            {
                SetMode(CameraModeType.ThirdPerson);
            });

            return;
        }

        _cameraTransition.ToFirstPerson(() =>
        {
            SetMode(CameraModeType.FirstPerson);
        });
    }
}
