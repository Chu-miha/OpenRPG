using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
//рефакторить 
public class PlayerAnimationController 
{
    private const string AttackClipName = "Fist_Punch";

    private readonly Animator _firstPersonAnimator;
    private readonly Animator _thirdPersonAnimator;

    private readonly AnimatorOverrideController _firstPersonOverride;
    private readonly AnimatorOverrideController _thirdPersonOverride;

    private readonly AnimationClip _defaultAttackAnimation;
    private readonly CompositeDisposable _disposables = new();

    private Animator _activeAnimator;

    public PlayerAnimationController(PlayerEquipment equipment, Animator firstPersonAnimator, Animator thirdPersonAnimator)
    {
        _firstPersonAnimator = firstPersonAnimator;
        _thirdPersonAnimator = thirdPersonAnimator;

        _firstPersonOverride = CreateOverrideController(firstPersonAnimator);
        _thirdPersonOverride = CreateOverrideController(thirdPersonAnimator);

        _defaultAttackAnimation = FindAttackClip(_firstPersonOverride);

        _firstPersonAnimator.runtimeAnimatorController = _firstPersonOverride;
        _thirdPersonAnimator.runtimeAnimatorController = _thirdPersonOverride;

        equipment.EquipmentChanged
            .Subscribe(_ => UpdateAttackAnimation(equipment))
            .AddTo(_disposables);

        UpdateAttackAnimation(equipment);
    }

    public void SetCameraMode(CameraModeType mode)
    {
        _activeAnimator = mode == CameraModeType.FirstPerson
            ? _firstPersonAnimator
            : _thirdPersonAnimator;
    }

    public void PlayAttack()
    {
        if (_activeAnimator == null)
            return;

        _activeAnimator.SetTrigger("Attack");
    }

    private void UpdateAttackAnimation(PlayerEquipment equipment)
    {
        AnimationClip attackAnimation = _defaultAttackAnimation;

        if (equipment.CurrentCombatItem is WeaponItem weapon)
        {
            AnimationClip weaponAnimation =
                weapon.AnimationSet?.AttackAnimation;

            if (weaponAnimation != null)
                attackAnimation = weaponAnimation;
        }

        _firstPersonOverride[AttackClipName] = attackAnimation;
        _thirdPersonOverride[AttackClipName] = attackAnimation;
    }

    private AnimatorOverrideController CreateOverrideController(Animator animator)
    {
        return new AnimatorOverrideController(animator.runtimeAnimatorController);
    }

    private AnimationClip FindAttackClip(AnimatorOverrideController controller)
    {
        var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>();

        controller.GetOverrides(overrides);

        foreach (var animationOverride in overrides)
        {
            if (animationOverride.Key.name == AttackClipName)
                return animationOverride.Key;
        }

        Debug.LogError(
            $"Animation clip '{AttackClipName}' was not found in Animator Controller.");

        return null;
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
