using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerListener : Bolt.GlobalEventListener
{

    public override void OnEvent(XPServerEvent evnt)
    {
        //Debug.Log("received XServerEvent from some tank");

        var newEvent = XPClientEvent.Create();
        newEvent.xpVal = evnt.xpVal;
        newEvent.networkID = evnt.networkID;
        newEvent.Send();

        
    }

}
