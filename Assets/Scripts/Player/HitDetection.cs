using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : Bolt.EntityBehaviour<ICustomCubeState>
{
    public event System.Action<float> OnPlayerHit = delegate { };
    public static event System.Action OnPlayerHitByMe = delegate { };

    private static int ownerID = 0;

    void Start()
    {
        if (entity.IsOwner)
        {
            Debug.Log("ownerID': " + ownerID);
            ownerID = this.GetInstanceID();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        //Debug.Log("Hit!");
        if (projectile != null)
        {
            if (entity.IsOwner)
            {
                //1. Determine if collision.gameObject is a bullet
                //2. Get the source of the bullet

                Debug.Log("tank: " + this.GetInstanceID() + " was hit by projectile from tank: " + projectile.getSourceID());
                OnPlayerHit(-projectile.damage);
            }
            else
            {

                Debug.Log("Hit");
                Debug.Log("tank: " + this.GetInstanceID() + " was hit by projectile from tank: " + projectile.getSourceID());
                Debug.Log("ownerID: " + ownerID);
                if (projectile.getSourceID() == ownerID)
                {
                    Debug.Log("By me");
                    OnPlayerHitByMe();
                }
            }
            Destroy(projectile);
        }
    }
}
