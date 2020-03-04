using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : Bolt.EntityEventListener<ICustomCubeState>
{
    public event System.Action<float> OnPlayerHit = delegate { };
    public event System.Action<int> OnXPGained = delegate { };

    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Hit!");
        if (entity.IsOwner)
        {
            //1. Determine if collision.gameObject is a bullet
            //2. Get the source of the bullet
            Debug.Log(collision.gameObject.GetComponent<Projectile>());
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();

            if (projectile != null)
            {
                OnPlayerHit(-projectile.damage);
                //Debug.Log("tank: " + entity.NetworkId + " was hit by projectile from tank: " + projectile.getSourceNetworkID());

                //var sourceTank = BoltNetwork.FindEntity(projectile.getSourceNetworkID());
                //int tankHashCode = sourceTank.GetHashCode();

                //foreach(BoltEntity e in BoltNetwork.Entities) {
                //    if (e == sourceTank) {
                //        var evnt = XPGainedEvent.Create(entity);
                //        evnt.tankID = tankHashCode;
                //        evnt.xpVal = 10;
                //        evnt.entity = sourceTank;
                //        evnt.Send();
                //        Debug.Log("sent xp to " + sourceTank);
                //    } 
                // }

                
            }
        }
    }

    //public override void OnEvent(XPGainedEvent evnt)
    //{
    //    Debug.Log("inside onEvent function, tank: tank in event: " + evnt.entity);
    //    //Debug.Log("My ID is " + this.GetHashCode());

    //    if (entity == evnt.entity)
    //    {
    //        Debug.Log("XP gained");
    //    }
    //}
}
