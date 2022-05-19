using UnityEngine;
using UnityEngine.Events;

public class CannonProjectile : ProjectileBase
{
    bool hasCollided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided)
        {
            hasCollided = true;
            Impact();
        }
    }
}


