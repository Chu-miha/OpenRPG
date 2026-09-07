using System;
using DG.Tweening;
using UnityEngine;

public class CameraTransition
{
    private readonly FirstPersonCamera _firstPersonCamera;
    private readonly ThirdPersonCamera _thirdPersonCamera;

    private Tween _transitionTween;

    public bool IsRunning =>
        _transitionTween != null && _transitionTween.IsActive();

    public CameraTransition(
        FirstPersonCamera firstPersonCamera,
        ThirdPersonCamera thirdPersonCamera)
    {
        _firstPersonCamera = firstPersonCamera;
        _thirdPersonCamera = thirdPersonCamera;
    }

    public void ToFirstPerson(Action onComplete)
    {
        if (IsRunning)
            return;

        _thirdPersonCamera.Activate();

        _transitionTween = DOTween.To(
                () => _thirdPersonCamera.Distance,
                value => _thirdPersonCamera.Distance = value,
                0f,
                _thirdPersonCamera.TransitionDuration)
            .SetEase(Ease.InOutSine)
            .OnUpdate(_thirdPersonCamera.UpdateCameraPosition)
            .OnComplete(() =>
            {
                _thirdPersonCamera.Deactivate();
                _firstPersonCamera.Activate();

                _transitionTween = null;

                onComplete?.Invoke();
            });
    }

    public void ToThirdPerson(Action onComplete)
    {
        _thirdPersonCamera.Distance = 0f;

        _firstPersonCamera.Deactivate();
        _thirdPersonCamera.Activate();

        _transitionTween = DOTween.To(
                () => _thirdPersonCamera.Distance,
                value => _thirdPersonCamera.Distance = value,
                _thirdPersonCamera.DefaultDistance,
                _thirdPersonCamera.TransitionDuration)
            .SetEase(Ease.InOutSine)
            .OnUpdate(_thirdPersonCamera.UpdateCameraPosition)
            .OnComplete(() =>
            {
                _transitionTween = null;

                onComplete?.Invoke();
            });
    }
}
