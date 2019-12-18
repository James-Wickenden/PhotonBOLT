using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : Bolt.EntityBehaviour<ICustomCubeState>
{
    public Rigidbody bulletPrefab;
    public float bulletSpeed;
    public GameObject muzzle;

    public override void Attached() {
        state.OnShoot = Shoot;
        if (entity.IsOwner) state.Health = 100.0F;
    }

    //public override void OnCollisionEnter(Collision collision) {

    //}

    private void Shoot()
    {
        Rigidbody bulletClone = Instantiate(bulletPrefab, muzzle.transform.position, this.transform.rotation);
        if (entity.IsOwner) state.Health -= 1F;
        bulletClone.velocity = transform.TransformDirection(new Vector3(0,0, bulletSpeed));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && entity.IsOwner)
        {
            state.Shoot();
            Debug.Log("health is " + state.Health.ToString() + "from tank " + this.GetInstanceID());
        }
    }
}
