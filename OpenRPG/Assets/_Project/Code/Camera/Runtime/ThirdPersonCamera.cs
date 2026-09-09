using System;
using UnityEngine;
using Zenject;

public class ThirdPersonCamera : MonoBehaviour, ICameraMode
{
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float defaultPitch = 20f;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 70f;
    [SerializeField] private float transitionDuration = 0.35f;
    
    private ICameraInput _cameraInput;
    private ICameraTarget _cameraTarget;
    
    private float _yaw;
    private float _pitch;
    private bool _active;
    private float _defaultDistance;
    
    public CameraModeType Type => CameraModeType.ThirdPerson;
    public Vector3 Position => transform.position;
    public Vector3 Forward => transform.forward;
    public float DefaultDistance => _defaultDistance;
    public float TransitionDuration => transitionDuration;
    public float Distance
    {
        get => distance;
        set => distance = value;
    }
    

   private void Awake()
   {
       _defaultDistance = distance;
   }

   [Inject]
    private void Construct(ICameraInput cameraInput, ICameraTarget cameraTarget)
    {
        _cameraInput = cameraInput;
        _cameraTarget = cameraTarget;
    }
    
    private void LateUpdate()
    {
        if (!_active)
            return;

        Vector2 look = _cameraInput.Look;

        _yaw += look.x * sensitivity;
        _pitch -= look.y * sensitivity;

        _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);

        UpdateCameraPosition();

    }
    
    public void Activate()
    {
        _active = true;
        Debug.Log("THIRD PERSON ACTIVATED");
    }

    public void Deactivate()
    {
        _active = false;
    }
    
    public void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0f);

        Vector3 offset = rotation * Vector3.back * distance;

        transform.position = _cameraTarget.Position + offset;
        
        transform.rotation = rotation;
    }
    
    public void SetOrbit()
    {
        Vector3 direction = _cameraTarget.Rotation * Vector3.forward;

        _yaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        _pitch = defaultPitch;
    }
    
}
