using UnityEngine;

public class CannonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _cannonBallPrefab;
    [SerializeField]
    private Transform _firePoint;
    [SerializeField]
    private LineRenderer _lineRenderer;

    private Camera _cam;
    private bool _pressingMouse = false;
    private Vector3 _initialDirection;
    private float _pressStartTime = 0f;

    private const int N_TRAJECTORY_POINTS = 10;

    private void Start()
    {
        _cam = Camera.main;
        _lineRenderer.positionCount = N_TRAJECTORY_POINTS;
        _lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pressingMouse = true;
            _lineRenderer.enabled = true;
            _pressStartTime = Time.time;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _pressingMouse = false;
            _lineRenderer.enabled = false;
            Fire();
        }

        if (_pressingMouse)
        {
            var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            mousePos.x = Mathf.Max(mousePos.x, transform.position.x);
            mousePos.y = Mathf.Max(mousePos.y, transform.position.y);

            transform.LookAt(mousePos);
            var eulers = new Vector3(transform.localEulerAngles.x, 90f, 0f);
            transform.localEulerAngles = eulers;

            _initialDirection = (mousePos - _firePoint.position).normalized;

            UpdateLineRenderer();
        }
    }

    private void Fire()
    {
        var velocity = CalculateInitialVelocity();
        var cannonBall = Instantiate(
            _cannonBallPrefab,
            _firePoint.position,
            Quaternion.identity);
        var rb = cannonBall.GetComponent<Rigidbody>();
        rb.AddForce(_initialDirection * velocity, ForceMode.Impulse);
    }

    private void UpdateLineRenderer()
    {
        float g = Physics.gravity.magnitude;
        var velocity = CalculateInitialVelocity();
        float angle = Mathf.Atan2(_initialDirection.y, _initialDirection.x);

        Vector3 start = _firePoint.position;

        float timeStep = 0.1f;
        float fTime = 0f;
        for (int i = 0; i < N_TRAJECTORY_POINTS; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle);
            float dy = velocity * fTime * Mathf.Sin(angle) - (g * fTime * fTime / 2f);
            Vector3 pos = new Vector3(start.x + dx, start.y + dy, 0);
            _lineRenderer.SetPosition(i, pos);
            fTime += timeStep;
        }
    }

    private float CalculateInitialVelocity()
    {
        float factor = 30f;
        float min = 3f;
        float max = 300f;
        var d = min / factor;
        return Mathf.Clamp((Time.time - _pressStartTime + d) * factor, min, max);
    }
}
