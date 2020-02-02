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
        // Get joystick
        Joystick joystick = GameObject.FindGameObjectWithTag("joystick").GetComponent<Joystick>();


        // ROTATION

        Vector3 originalPos = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 targetPos = Camera.main.transform.TransformDirection(new Vector3(joystick.Horizontal, 0, joystick.Vertical));
        targetPos.Normalize();
        Vector3 dirVec = targetPos - originalPos;
        dirVec = transform.InverseTransformDirection(dirVec);
        float angle = Vector3.Angle(originalPos, targetPos);


        if (dirVec.x >= 0) transform.localEulerAngles += new Vector3(0, angle * rotationSpeed * BoltNetwork.FrameDeltaTime, 0);
        else transform.localEulerAngles -= new Vector3(0, angle * rotationSpeed * BoltNetwork.FrameDeltaTime, 0);


        // POSITION

        float joystickMagnitude = joystick.Horizontal * joystick.Horizontal + joystick.Vertical * joystick.Vertical;
        transform.position += transform.forward * movementSpeed * joystickMagnitude * BoltNetwork.FrameDeltaTime;


        // respawning
        if (Input.GetKey(KeyCode.Return) && entity.IsOwner)
        {
            transform.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
            transform.rotation = Quaternion.identity;
        }
    }
}
