using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : Bolt.EntityBehaviour<ICustomCubeState>
{
    public Rigidbody lastShot;

    public event System.Action<float> OnPlayerHit = delegate { };

    public void OnCollisionEnter(Collision collision)
    {
        //1. Determine if collision.gameObject is a bullet
        //2. Get the source of the bullet
        //3. If source != this, take damage
        Debug.Log(collision.gameObject.name);


        //Debug.Log("Hit!");
        if (entity.IsOwner)
        {
            Debug.Log((collision.gameObject.GetComponent<Rigidbody>()).ToString());
            if (collision.gameObject.GetComponent<Rigidbody>() != lastShot)
            {
                Debug.Log("Hit! " + this.GetInstanceID() + " was hit.");
                OnPlayerHit(-1.0F);
            }

        }
    }
}
