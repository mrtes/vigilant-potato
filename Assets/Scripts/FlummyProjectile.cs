using UnityEngine;

public class FlummyProjectile : ProjectileBase
{
    public int collisionEndurance = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionEndurance == 0)
        {
            Impact();
        }
        collisionEndurance--;
    }
}
