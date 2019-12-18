using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adds gravity to the DefaultTrackableEventHandler

public class GravityTrackableEventHandler : DefaultTrackableEventHandler
{

    private Dictionary<Rigidbody, Vector3> lastVelocities;  // Stores the most recent velocity for each RigidBody before the image target is lost

    protected override void OnTrackingFound()
    {
        // TODO: make this nicer :)
        var rigidbodies = GetComponentsInChildren<Rigidbody>(true);
        lastVelocities = new Dictionary<Rigidbody, Vector3>();

        foreach (var rb in rigidbodies)
        {
            lastVelocities.Add(rb, new Vector3(0, 0, 0));
        }

        base.OnTrackingFound();

        // Turn on gravity when the image target is detected
        foreach (var rbPair in lastVelocities)
        {
            rbPair.Key.useGravity = true;
            rbPair.Key.velocity = rbPair.Value;
        }
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        // Turn off gravity when the image target is no longer detected
        foreach (var rbPair in lastVelocities)
        {
            rbPair.Value.Set(rbPair.Key.velocity.x, rbPair.Key.velocity.y, rbPair.Key.velocity.z);
            rbPair.Key.useGravity = false;
            rbPair.Key.velocity = new Vector3(0, 0, 0);
        }
    }
}