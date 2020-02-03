using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : Bolt.EntityBehaviour<ICustomCubeState>
{
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float rotationSpeed;

    private Joystick joystick;

    public void Awake()
    {
        // Get joystick
        joystick = GameObject.FindGameObjectWithTag("joystick").GetComponent<Joystick>();
    }

    public override void Attached()
    {
        state.SetTransforms(state.CubeTransform, gameObject.transform);
    }

    public override void SimulateOwner()
    {
        // ROTATION
        Vector3 originalPos = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 targetPos = Camera.main.transform.TransformDirection(new Vector3(joystick.Horizontal, 0, joystick.Vertical));

        float dir = Mathf.Sign(Vector3.Dot(targetPos, originalPos)); //Enables reversing if direction angle is closer to rear of tank.
        targetPos *= dir; 

        Vector3 dirVec = targetPos - originalPos;
        dirVec = transform.InverseTransformDirection(dirVec);
        float angle = Vector3.Angle(originalPos, targetPos);

        
        if (dirVec.x >= 0) transform.localEulerAngles += new Vector3(0, angle * rotationSpeed * BoltNetwork.FrameDeltaTime, 0);
        else transform.localEulerAngles -= new Vector3(0, angle * rotationSpeed * BoltNetwork.FrameDeltaTime, 0);

        // POSITION
        float joystickMagnitude = joystick.Horizontal * joystick.Horizontal + joystick.Vertical * joystick.Vertical;
        transform.position += dir * transform.forward * movementSpeed * joystickMagnitude * BoltNetwork.FrameDeltaTime;

        // respawning
        if (Input.GetKey(KeyCode.Return) && entity.IsOwner)
        {
            transform.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
            transform.rotation = Quaternion.identity;
        }
    }
}