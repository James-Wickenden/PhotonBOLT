using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankListener : Bolt.GlobalEventListener
{
    public event System.Action<Bolt.NetworkId, int> OnXP = delegate { };


    public override void OnEvent(XPClientEvent evnt)
    {
        //Debug.Log("Received XPClientEvent from tank " + evnt.networkID);
        OnXP(evnt.networkID, evnt.xpVal);
    }
}