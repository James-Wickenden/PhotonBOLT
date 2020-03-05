using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SetTankID : Bolt.EntityBehaviour<ICustomCubeState>
{
    //bool isInitialised = false;

    public override void Attached()
    {
        GetComponent<TankListener>().OnTankAdded += setID;

        if (entity.IsOwner)
        {
            //int tankID = GetComponent<TankListener>().tankID;


            //if (tankID == 0)
            //{
            //    state.TankID = 0;
            //}

            //else if (!isInitialised)
            //{
            //    // receive tank id
            //    state.TankID = 1;

            //}

            //isInitialised = true;

        }
    }

    private void setID(int tankID)
    {
        if (entity.IsOwner)
        {
            state.TankID = tankID;
            Debug.Log("Tank ID set to " + state.TankID);
        }
    }

}
