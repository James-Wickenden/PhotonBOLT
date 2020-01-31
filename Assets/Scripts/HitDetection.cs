using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : Bolt.EntityBehaviour<ICustomCubeState>
{
    public event System.Action<float> OnPlayerHit = delegate { };

    public void OnCollisionEnter(Collision collision)
    {


        //Debug.Log("Hit!");
        if (entity.IsOwner)
        {
            //1. Determine if collision.gameObject is a bullet
            //2. Get the source of the bullet
            //3. If source != this, take damage
            Debug.Log(collision.gameObject.GetComponent<Projectile>());
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();

            if (projectile != null)
            {
                Debug.Log("tank: " + this.GetInstanceID() + " was hit by projectile from tank: " + projectile.getSourceID());
                OnPlayerHit(-projectile.damage);
            }
        }
    }
}
