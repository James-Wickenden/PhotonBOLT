using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : Bolt.EntityBehaviour<ICustomCubeState>
{

    public float movementSpeed;
    public float rotationSpeed;

    public override void Attached()
    {
        state.SetTransforms(state.CubeTransform, gameObject.transform);
    }

    public override void SimulateOwner()
    {
        float moveDirection = 0;
        var rotationDirection = Vector3.zero;

        moveDirection = System.Math.Sign(Input.GetAxis("Vertical"));
        rotationDirection = new Vector3(0.0f, Input.GetAxis("Horizontal"), 0.0f);

        if (moveDirection != 0)
        {
            transform.position += moveDirection * transform.forward * movementSpeed * BoltNetwork.FrameDeltaTime;
        }

        if (rotationDirection != Vector3.zero)
        {
            transform.Rotate(rotationDirection.normalized * rotationSpeed * BoltNetwork.FrameDeltaTime);
        }

        if (Input.GetKey(KeyCode.Return) && entity.IsOwner)
        {
            transform.position = new Vector3(Random.Range(-8, 8), 1, Random.Range(-8, 8));
            transform.rotation = Quaternion.identity;
        }
    }
}
