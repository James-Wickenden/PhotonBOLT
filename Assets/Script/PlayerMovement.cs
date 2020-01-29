using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Bolt.EntityBehaviour<ICustomCubeState>
{
    public override void Attached(){
        state.SetTransforms(state.CubeTransform, gameObject.transform);
    }

    public override void SimulateOwner(){
        var moveDirection = Vector3.zero;
        var movementSpeed = 5f;
        
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        
        if (moveDirection != Vector3.zero) {
            transform.position = transform.position + (moveDirection.normalized * movementSpeed * BoltNetwork.FrameDeltaTime);
        }
    }
}
