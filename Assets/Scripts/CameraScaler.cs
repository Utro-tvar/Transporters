using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScaler : MonoBehaviour
{
    [SerializeField] private Vector2 _defaultResolution = new Vector2(1000, 1000);

    private Camera _camera = null;

    private float _targetAspect;
    private float _initialSize;
    private float _lastCameraAspect;

    public void WakeUp()
    {
        _camera = GetComponent<Camera>();
        _initialSize = _camera.orthographicSize;
        _targetAspect = _defaultResolution.x / _defaultResolution.y;
        _lastCameraAspect = _targetAspect;

        if (_camera.aspect != _lastCameraAspect)
        {
            _camera.orthographicSize = GetCameraSize();
            _lastCameraAspect = _camera.aspect;
        }
    }
    /*
    private void Awake()
    {
        if(_camera == null)
        {
            WakeUp();
        }
    }*/

    void Update()
    {
        if(_camera.aspect != _lastCameraAspect)
        {
            _camera.orthographicSize = GetCameraSize();
            _lastCameraAspect = _camera.aspect;
        }
    }

    private float GetCameraSize()
    {
        float size = _initialSize * _targetAspect;
        size /= _camera.aspect;
        return size;
    }
}
