using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : Bolt.EntityBehaviour<ICustomCubeState>
{
    //public HashSet<Rigidbody> bullets = new HashSet<Rigidbody>();
    public Rigidbody bulletPrefab;
    public float bulletSpeed;
    public GameObject muzzle;
    public Rigidbody lastShot;

    public override void Attached() {
        state.OnShoot = Shoot;
        if (entity.IsOwner) state.Health = 100.0F;
    }

    public void OnCollisionEnter(Collision collision) {
        //1. Determine if collision.gameObject is a bullet
        //2. Get the source of the bullet
        //3. If source != this, take damage


        //Debug.Log("Hit!");
        if (entity.IsOwner) {
            Debug.Log((collision.gameObject.GetComponent<Rigidbody>()).ToString());
            if (collision.gameObject.GetComponent<Rigidbody>() != lastShot) {
                state.Health -= 1F;
                Debug.Log("Hit! " + this.GetInstanceID() + " was hit.");
            }
            
        }
    }

    private void Shoot()
    {
        Rigidbody bulletClone = Instantiate(bulletPrefab, muzzle.transform.position, this.transform.rotation);
        this.lastShot = bulletClone;
        if (entity.IsOwner) state.Health -= 1F;
        bulletClone.velocity = transform.TransformDirection(new Vector3(0,0, bulletSpeed));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && entity.IsOwner)
        {
            state.Shoot();
            Debug.Log("Shot from tank " + this.GetInstanceID());
            //Debug.Log("bullets contains " + this.bullets.Count + " from tank " + this.GetInstanceID());
        }

    }
}
