using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankListener : Bolt.GlobalEventListener
{
    public event System.Action<Bolt.NetworkId, int> OnXP = delegate { };
    public event System.Action<string, int> OnPlayerAdded = delegate { };


    public override void OnEvent(XPClientEvent evnt)
    {
        Debug.Log("Received XPClientEvent for tank " + evnt.networkID);
        OnXP(evnt.networkID, evnt.xpVal);
    }

    public override void OnEvent(SetScoreEvent evnt)
    {
        OnPlayerAdded(evnt.username, evnt.score);
    }
}