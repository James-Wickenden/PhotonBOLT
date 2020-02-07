using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : Bolt.EntityBehaviour<ICustomCubeState>
{

    [SerializeField]
    private float rotationSpeed;

    public override void Attached()
    {
        state.SetTransforms(state.TurretTransform, gameObject.transform);
    }

    public override void SimulateOwner()
    {
        // Ensure turret always points to centre of screen
        Vector3 forward = Camera.main.transform.forward;
        Vector3 n = transform.TransformDirection(new Vector3(0, 1, 0));
        Vector3 p_0 = transform.position;
        Vector3 l_0 = Camera.main.transform.position;

        float denominator = Vector3.Dot(forward, n);

        if (!denominator.Equals(0))
        {
            float d = (Vector3.Dot((p_0 - l_0), n)) / denominator;
            Vector3 p = l_0 + forward * d;

            // Slowly rotate turret towards p
            Vector3 originalPos = transform.forward;
            Vector3 targetPos = p - transform.position;

            float singleStep = rotationSpeed * BoltNetwork.FrameDeltaTime;
            Vector3 newDirection = Vector3.RotateTowards(originalPos, targetPos, singleStep, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }

    }
}