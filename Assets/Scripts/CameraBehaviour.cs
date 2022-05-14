using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraTarget;
    [SerializeField]
    private float _smoothSpeed = 3f;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    protected void LateUpdate()
    {
        var sqDistance = ((Vector2)_cameraTarget.position - (Vector2)transform.position).sqrMagnitude;
        _camera.orthographicSize = 10f * (sqDistance * Time.deltaTime + 1);

        var newPos = Vector3.Lerp(transform.position, _cameraTarget.position, _smoothSpeed * Time.deltaTime);
        newPos.z = transform.position.z;
        transform.position = newPos;
    }
}
