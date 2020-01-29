using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : Bolt.EntityBehaviour<ICustomCubeState>
{
    //public HashSet<Rigidbody> bullets = new HashSet<Rigidbody>();
    public Rigidbody bulletPrefab;
    public float bulletSpeed;
    public GameObject muzzle;
    

    public override void Attached() {
        state.OnShoot = Shoot;
    }



    private void Shoot()
    {
        Rigidbody bulletClone = Instantiate(bulletPrefab, muzzle.transform.position, this.transform.rotation);
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
