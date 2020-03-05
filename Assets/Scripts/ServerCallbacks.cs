using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerCallbacks : Bolt.GlobalEventListener
{
    private int tankCount = 0;

    public override void EntityAttached(BoltEntity entity)
    {
        var log = TestEvent.Create();
        log.Message = tankCount;
        log.Send();
        //Debug.Log("assigning tankID of "+ tankCount + " to new tank");
        tankCount += 1;
    }

    public override void OnEvent(XPServerEvent evnt)
    {
        //Debug.Log("received XServerEvent from some tank");

        var newEvent = XPClientEvent.Create();
        newEvent.tankID = evnt.tankID;
        newEvent.xpVal = evnt.xpVal;
        newEvent.networkID = evnt.networkID;
        newEvent.Send();

        
    }

}
