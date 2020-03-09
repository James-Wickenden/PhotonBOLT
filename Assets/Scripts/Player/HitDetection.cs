using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : Bolt.EntityEventListener<ICustomCubeState>
{
    public event System.Action<float> OnPlayerHit = delegate { };
    private GameObject lastBullet;

    public void OnCollisionEnter(Collision collision)
    {
        if (entity.IsOwner)
        {
            //1. Determine if collision.gameObject is a bullet
            //2. Get the source of the bullet

            Projectile projectile = collision.gameObject.GetComponent<Projectile>();

            if (projectile != null && lastBullet != collision.gameObject)
            {
                OnPlayerHit(-projectile.damage);
       
                var xpServerEvent = XPServerEvent.Create(Bolt.GlobalTargets.OnlyServer);
                xpServerEvent.xpVal = 10;
                xpServerEvent.networkID = projectile.getSourceNetworkID();
                xpServerEvent.Send();

                lastBullet = collision.gameObject;
            }
        }
    }
}
