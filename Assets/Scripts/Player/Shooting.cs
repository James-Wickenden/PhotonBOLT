using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Button shootButton = GameObject.FindGameObjectWithTag("ShootButton").GetComponent<Button>();
        shootButton.onClick.AddListener(() => OnShootButtonClick());
    }

    private void Shoot()
    { 
        Rigidbody bulletClone = Instantiate(bulletPrefab, muzzle.transform.position, this.transform.rotation);
        

        Projectile projectile = bulletClone.GetComponent<Projectile>();
        projectile.setSourceID(GetInstanceID());

        bulletClone.velocity = muzzle.transform.forward * projectile.getSpeed();
    }

    private void OnShootButtonClick()
    {
        if (entity.IsOwner)
        {
            state.Shoot();
        }
    }
}
