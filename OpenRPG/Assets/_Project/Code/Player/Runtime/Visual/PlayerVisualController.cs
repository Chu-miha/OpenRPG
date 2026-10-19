using System;
using UniRx;
using UnityEngine;
// рефакторить 
public class PlayerVisualController : IDisposable
{
    private readonly PlayerVisualReferences _firstPersonVisual;
    private readonly PlayerVisualReferences _thirdPersonVisual;
    private readonly PlayerEquipment _equipment;
    private readonly CompositeDisposable _disposables = new();
    private readonly PlayerAnimationController _animationController;

    private GameObject _firstPersonWeapon;
    private GameObject _thirdPersonWeapon;

    public PlayerVisualController(
        ICameraState cameraState,
        PlayerEquipment equipment,
        PlayerVisualReferences firstPersonVisual,
        PlayerVisualReferences thirdPersonVisual)
    {
        _equipment = equipment;
        _firstPersonVisual = firstPersonVisual;
        _thirdPersonVisual = thirdPersonVisual;
        
        _animationController = new PlayerAnimationController( 
            equipment,
            firstPersonVisual.Animator,
            thirdPersonVisual.Animator);

        cameraState.CameraModeChanged
            .Subscribe(SetCameraMode)
            .AddTo(_disposables);

        equipment.EquipmentChanged
            .Subscribe(_ => UpdateWeaponVisual())
            .AddTo(_disposables);

        SetCameraMode(cameraState.CurrentMode.Type);
        UpdateWeaponVisual();
    }

    private void SetCameraMode(CameraModeType mode)
    {
        bool isFirstPerson = mode == CameraModeType.FirstPerson;

        _firstPersonVisual.gameObject.SetActive(isFirstPerson);
        _thirdPersonVisual.gameObject.SetActive(!isFirstPerson);
        
        _animationController.SetCameraMode(mode);
    }

    private void UpdateWeaponVisual()
    {
        ClearWeaponVisual();

        if (_equipment.CurrentCombatItem is not WeaponItem weapon)
            return;

        if (weapon.VisualPrefab == null)
            return;

        _firstPersonWeapon = UnityEngine.Object.Instantiate(
            weapon.VisualPrefab,
            _firstPersonVisual.WeaponSocket,
            false);

        _thirdPersonWeapon = UnityEngine.Object.Instantiate(
            weapon.VisualPrefab,
            _thirdPersonVisual.WeaponSocket,
            false);
    }

    private void ClearWeaponVisual()
    {
        if (_firstPersonWeapon != null)
            UnityEngine.Object.Destroy(_firstPersonWeapon);

        if (_thirdPersonWeapon != null)
            UnityEngine.Object.Destroy(_thirdPersonWeapon);

        _firstPersonWeapon = null;
        _thirdPersonWeapon = null;
    }
    
    public void PlayAttack()
    {
        _animationController.PlayAttack();
    }

    public void Dispose()
    {
        ClearWeaponVisual();
        _animationController.Dispose();
        _disposables.Dispose();
    }
}
