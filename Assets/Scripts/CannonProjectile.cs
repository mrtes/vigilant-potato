using UnityEngine;
using UnityEngine.Events;

public class CannonProjectile : ProjectileBase
{
    bool hasCollided = false;
    [SerializeField]
    private ParticleSystem _trail;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided)
        {
            hasCollided = true;
            _trail.Stop();
            Impact();
        }
    }
}


