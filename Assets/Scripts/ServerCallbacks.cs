using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerCallbacks : Bolt.GlobalEventListener
{
    public override void Connected(BoltConnection connection)
    {
        //var log = TestEvent.Create();
        //log.Message = 10;
        //log.Send();
        //Debug.Log("sent!");
    }

    public override void Disconnected(BoltConnection connection)
    {
        //var log = TestEvent.Create();
        //log.Message = 10;
        //log.Send();
    }

    public override void EntityAttached(BoltEntity entity)
    {
        var log = TestEvent.Create(entity);
        log.Message = 10;
        log.Send();
        Debug.Log("sent!");
    }
}
