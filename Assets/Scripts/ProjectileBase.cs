using UnityEngine;
using UnityEngine.Events;

public class ProjectileBase : MonoBehaviour
{
    public UnityEvent impacted = new UnityEvent();
    [SerializeField]
    private AudioSource _bounceAudio, _explosionAudio;

    protected void Start()
    {
        // for testing
        Invoke(nameof(Impact), 10f);
    }

    protected virtual void Impact()
    {
        _explosionAudio.Play();
        impacted.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _bounceAudio.Play();
    }
}
