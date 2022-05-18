using DG.Tweening;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraTarget;
    [SerializeField]
    private float _smoothSpeed = 3f;
    [SerializeField]
    private CannonManager _cannon;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _cannon.stateChanged.AddListener(OnCamStateChanged);
    }

    protected void LateUpdate()
    {
        if (_cannon.State == CannonManager.CannonState.CanFire)
        {
            _camera.orthographicSize = 10f * _cannon.CalculateOrthographicCamFactor();
        }

        var newPos = Vector3.Lerp(transform.position, _cameraTarget.position, _smoothSpeed * Time.deltaTime);
        newPos.z = transform.position.z;
        transform.position = newPos;
    }

    private void OnCamStateChanged()
    {
        if (_cannon.State != CannonManager.CannonState.CanFire)
        {
            _camera.DOOrthoSize(10f, 1f);
        }
    }
}
