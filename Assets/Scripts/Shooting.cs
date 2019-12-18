using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : Bolt.EntityBehaviour<ICustomCubeState>
{
    public Rigidbody bulletPrefab;
    public float bulletSpeed;
    public GameObject muzzle;

    public override void Attached()
    {
        state.OnShoot = Shoot;
    }

    private void Awake()
    {
        transform.parent = GameObject.FindWithTag("image_target").transform;
    }

    private void Shoot()
    {

        Rigidbody bulletClone = Instantiate(bulletPrefab, muzzle.transform.position, this.transform.rotation);

        bulletClone.velocity = transform.TransformDirection(new Vector3(0,0, bulletSpeed));
    }

    private void Update()
    {
        if (Input.GetAxis("Fire1") > 0 && entity.IsOwner)
        {
            state.Shoot();
        }
    }
}
