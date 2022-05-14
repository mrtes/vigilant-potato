using UnityEngine;
using UnityEngine.Events;

public class ProjectileBase : MonoBehaviour
{
    public UnityEvent impacted = new UnityEvent();

    protected void Start()
    {
        // for testing
        Invoke(nameof(Impact), 10f);
    }

    protected virtual void Impact()
    {
        impacted.Invoke();
    }
}
