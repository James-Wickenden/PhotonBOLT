using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPlayerHandler : Bolt.EntityBehaviour<ICustomCubeState>
{
    /**
     * what I'm trying to do:
     *
     * - leave gravity on for all tanks at every time
     * - whenever tracking of img target is lost, store the velocity and position of the tank, in here (?)
     * - ^ when this happens, the tank will fall into the void because gravity is on and the img target is lost
     * - we can restore the position and velocity once tracking is obtained.
     *
     * - don't turn off gravity because in everyone else's devices your tank should still be there and should respond to collisions and stuff
     * - maybe when the img target is found again the other tanks can send the location and velocity of the tank that just came back (???)
     *
     * - oh no tank falls when one img target disappears - linked to the img target on the phone
    **/


    ////private Vector3 lastVelocity;

    private void Update()
    {
        if (!entity.IsOwner && !entity.GetComponent<Rigidbody>().useGravity)
        {

        }
    }

}
