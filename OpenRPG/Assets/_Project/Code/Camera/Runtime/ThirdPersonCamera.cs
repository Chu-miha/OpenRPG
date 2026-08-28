using UnityEngine;
using Zenject;

public class ThirdPersonCamera : MonoBehaviour, ICameraMode
{
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 70f;
    
    private ICameraInput _cameraInput;
    private ICameraTarget _cameraTarget;
    
    private float _yaw;
    private float _pitch;
    private bool _active;
    
    public CameraModeType Type => CameraModeType.ThirdPerson;
    public Vector3 Position => transform.position;
    public Vector3 Forward => transform.forward;

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

        Quaternion rotation =
            Quaternion.Euler(_pitch, _yaw, 0f);

        Vector3 offset =
            rotation * Vector3.back * distance;

        transform.position =
            _cameraTarget.Position + offset;
        
        transform.rotation = rotation;

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
}
