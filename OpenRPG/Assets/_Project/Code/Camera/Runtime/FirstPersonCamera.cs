using System;
using UnityEngine;
using Zenject;

public class FirstPersonCamera : MonoBehaviour, ICameraMode
{
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;
    
    private ICameraInput _cameraInput;
    private ICameraTarget _cameraTarget;
    private IPlayerRotation _playerRotation;
    private float _pitch;
    private bool _active;
    
    public CameraModeType Type => CameraModeType.FirstPerson;
    public Vector3 Position => transform.position;
    public Vector3 Forward => transform.forward;

    [Inject]
    private void Construct(ICameraInput cameraInput, ICameraTarget cameraTarget, IPlayerRotation playerRotation)
    {
        _cameraInput = cameraInput;
        _cameraTarget = cameraTarget;
        _playerRotation = playerRotation;
    }
    
    private void Update()
    {
        if (!_active)
            return;

        Vector2 look = _cameraInput.Look;

        float yaw = look.x * sensitivity;
        _pitch -= look.y * sensitivity;

        _pitch = Mathf.Clamp(
            _pitch,
            minPitch,
            maxPitch);

        _playerRotation.Rotate(yaw);
    }

    private void LateUpdate()
    {
        if (!_active)
            return;
        
        transform.position = _cameraTarget.Position;

        transform.rotation  = _cameraTarget.Rotation * Quaternion.Euler(_pitch, 0, 0);
    }

    public void Activate()
    {
        _active = true;
        Debug.Log("FIRST PERSON ACTIVATED");
    }

    public void Deactivate()
    {
        _active = false;
    }
}
