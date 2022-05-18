using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    public UnityEvent targetHit = new UnityEvent();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody?.tag == "KillsTarget")
        {
            targetHit.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KillsTarget")
        {
            targetHit.Invoke();
        }
    }
}
