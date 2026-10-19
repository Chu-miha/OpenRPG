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
    public bool UseFirstQuickSlot => _inputActions.Player.FirstQuickSlot.WasPerformedThisFrame();
    public bool UseSecondQuickSlot => _inputActions.Player.SecondQuickSlot.WasPerformedThisFrame();
    public bool SwitchCameraPressed =>  _inputActions.Camera.SwitchCamera.WasPerformedThisFrame();
    public bool InventoryPressed =>  _inputActions.Player.Inventory.WasPerformedThisFrame();
    public int SelectedCombatSlot
    {
        get
        {
            if (_inputActions.Player.FirstCombatSlot.WasPerformedThisFrame()) return 0;
            if (_inputActions.Player.SecondCombatSlot.WasPerformedThisFrame()) return 1;
            if (_inputActions.Player.ThirdCombatSlot.WasPerformedThisFrame()) return 2;

            return -1;
        }
    }


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
