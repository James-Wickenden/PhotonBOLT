using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPEventListener : Bolt.EntityEventListener<ICustomCubeState>
{
    public float temp = 1.5f;

    public void Awake()
    {
        //GetComponent<HitDetection>().OnXPGained += sendXP;
    }

    public void sendXP(float xpVal, int tankHashCode)
    {
        //Debug.Log("sending xp to " + tankHashCode);
        ////Debug.Log("My ID is " + entity.NetworkId);

        //var xpGainedEvent = XPGainedEvent.Create(entity, Bolt.EntityTargets.EveryoneExceptOwner);
        //xpGainedEvent.xpVal = xpVal;
        //xpGainedEvent.tankID = tankID;
        //xpGainedEvent.Send();
    }

    //public override void OnEvent(XPGainedEvent evnt)
    //{
    //    Debug.Log("inside onEvent function, tank: tank in event: " + evnt.tankID);
    //    Debug.Log("My ID is " + entity.NetworkId);
    //    if (entity.NetworkId.Equals(evnt.tankID)) {
    //        Debug.Log("XP gained");
    //    }
    //}
}
