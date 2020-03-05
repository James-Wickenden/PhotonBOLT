using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankListener : Bolt.GlobalEventListener
{
    public event System.Action<int> OnTankAdded = delegate { };
    public event System.Action<int, Bolt.NetworkId, int> OnXP = delegate { };

    private int tankID = -2;

    private void Awake()
    {
        tankID = -1;
    }

    public override void OnEvent(TestEvent evnt)
    {
        //Debug.Log("Received! value is " + evnt.Message);

        if (tankID == -1)
        {
            OnTankAdded(evnt.Message);
            tankID = evnt.Message;
        }
    }

    public override void OnEvent(XPClientEvent evnt)
    {
        Debug.Log("Received XPClientEvent from tank " + evnt.networkID);
        OnXP(evnt.tankID, evnt.networkID, evnt.xpVal);
    }
}
