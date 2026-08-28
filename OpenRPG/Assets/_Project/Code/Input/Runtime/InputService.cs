using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputService : IMovementInput, ICameraInput, IActionInput, ICameraModeInput, IDisposable, IInitializable
{
    private readonly InputActions _inputActions;
    
    public Vector2 Move => _inputActions.Player.Move.ReadValue<Vector2>();
    public Vector2 Look => _inputActions.Player.Look.ReadValue<Vector2>();
    public bool JumpPressed => _inputActions.Player.Jump.WasPerformedThisFrame();
    public bool AttackPressed => _inputActions.Player.Attack.WasPerformedThisFrame();
    public bool InteractPressed => _inputActions.Player.Interact.WasPerformedThisFrame();
    public bool SwitchCameraPressed =>  _inputActions.Camera.SwitchCamera.WasPerformedThisFrame();


    public InputService()
    {
        _inputActions = new InputActions();
    }

    public void Enable()
    {
        _inputActions.Player.Enable();
    }

    public void Disable()
    {
        _inputActions.Player.Disable();
    }
    
    public void Initialize()
    {
        _inputActions.Enable();
        Debug.Log("Input Service initialized");
    }

    public void Dispose()
    {
        _inputActions.Disable();
        Debug.Log("Input Service disposed");
    }
}
    // //для тестов 
    // public bool IsEnabled => _inputActions.Player.enabled;
