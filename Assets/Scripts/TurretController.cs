using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : Bolt.EntityBehaviour<ICustomCubeState>
{
    public override void Attached()
    {
        state.SetTransforms(state.TurretTransform, gameObject.transform);
    }

    public override void SimulateOwner()
    { 
        // Ensure turret always points to centre of screen
        Vector3 forward = Camera.main.transform.forward;
        Vector3 n = new Vector3(0, 1, 0);
        Vector3 p_0 = new Vector3(0, transform.position.y, 0);
        Vector3 l_0 = Camera.main.transform.position;

        float denominator = Vector3.Dot(forward, n);

        if (!denominator.Equals(0))
        {
            float d = (Vector3.Dot((p_0 - l_0), n)) / denominator;
            Vector3 p = l_0 + forward * d;
            transform.LookAt(p);
        }

    }
}
