using UnityEngine;
using UnityEngine.Events;

public class ProjectileBase : MonoBehaviour
{
    public UnityEvent impacted = new UnityEvent();

    protected void Start()
    {
        // for testing
        // Invoke(nameof(OnCollisionEnter), 10f);
    }

    protected virtual void OnCollisionEnter()
    {
        Debug.Log("Impact");
        // Timeout??
        impacted.Invoke();
    }
}
