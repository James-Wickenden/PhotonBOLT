using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : Bolt.EntityBehaviour<ICustomCubeState>
{
    public Rigidbody bulletPrefab;
    public float bulletSpeed;
    public GameObject muzzle;

    private void Shoot()
    {

        Rigidbody bulletClone = Instantiate(bulletPrefab, muzzle.transform.position, this.transform.rotation);

        bulletClone.velocity = transform.TransformDirection(new Vector3(0,0, bulletSpeed));
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && entity.IsOwner)
        {
            Shoot();
        }
    }
}
