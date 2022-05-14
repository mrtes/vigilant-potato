using UnityEngine;

public class CannonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cannonBallPrefab;

    public GameObject prefab
    {
        get
        {
            return cannonBallPrefab;
        }
        set
        {
            cannonBallPrefab = value;
            dummyBall = Instantiate(value, firePoint.position, Quaternion.identity);
            dummyRigid = dummyBall.GetComponent<Rigidbody>();
        }
    }
    public Transform firePoint;
    public LineRenderer guide;

    private Camera _cam;
    private bool _pressingMouse = false;
    private Vector3 _direction;

    private float _pressedStart;
    private float _pressedEnd;

    private const int N_TRAJECTORY_POINTS = 10;
    private const int MAGNITUDE_FACTOR = 5;
    private const int MAXIMAL_MAGNITUDE = 30;
    private const int MINIMAL_MAGNITUDE = 2;


    private GameObject dummyBall;
    private Rigidbody dummyRigid;



    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        guide.positionCount = N_TRAJECTORY_POINTS;
        guide.enabled = false;

        dummyBall = Instantiate(cannonBallPrefab, firePoint.position, Quaternion.identity);
        dummyRigid = dummyBall.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pressedStart = Time.time;
            _pressingMouse = true;
            guide.enabled = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _pressingMouse = false;
            guide.enabled = false;
            Fire();
            _pressedStart = 0;
        }

        if (_pressingMouse)
        {
            Debug.Log("pressing mouse");
            // gedrückte zeit hochzählen 
            // coordinate transform screen > world
            Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            mousePos.x = Mathf.Max(mousePos.x, transform.position.x);
            mousePos.y = Mathf.Max(mousePos.y, transform.position.y);

            transform.LookAt(mousePos);
            var eulers = new Vector3(transform.localEulerAngles.x, 90f, 0f);
            transform.localEulerAngles = eulers;

            _direction = Vector3.Normalize(mousePos - firePoint.position);

            _UpdateLineRenderer();
        }
    }

    private void Fire()
    {
        var velocity = CalculateVelocity();
        var cannonBall = Instantiate(cannonBallPrefab, firePoint.position, Quaternion.identity);
        var rb = cannonBall.GetComponent<Rigidbody>();
        rb.AddForce(_direction * velocity, ForceMode.Impulse);
    }

    private void _UpdateLineRenderer()
    {
        var velocity = CalculateVelocity() / dummyRigid.mass;

        float g = Physics.gravity.magnitude;
        float angle = Mathf.Atan2(_direction.y, _direction.x);

        Vector3 start = firePoint.position;

        float timeStep = 0.1f;
        float fTime = 0f;
        for (int i = 0; i < N_TRAJECTORY_POINTS; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle);
            float dy = velocity * fTime * Mathf.Sin(angle) - (g * fTime * fTime / 2f);
            Vector3 pos = new Vector3(start.x + dx, start.y + dy, 0);
            guide.SetPosition(i, pos);
            fTime += timeStep;
        }
    }

    private float CalculateVelocity()
    {
        return ((Time.time - _pressedStart) * MAGNITUDE_FACTOR);
    }
}
