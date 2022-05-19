using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _bloodPS;
    [SerializeField]
    private AudioSource _hitAudio, _callAudio;
    public UnityEvent targetHit = new UnityEvent();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody?.tag == "KillsTarget")
        {
            HandleHit(collision.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KillsTarget")
        {
            HandleHit(other.transform);
        }
    }

    private void HandleHit(Transform other)
    {
        _bloodPS.transform.LookAt(other);
        _bloodPS.Play();
        _hitAudio.Play();
        _callAudio.Play();
        targetHit.Invoke();
    }
}
