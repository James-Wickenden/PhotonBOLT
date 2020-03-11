using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : Bolt.EntityBehaviour<ICustomCubeState>
{
    [SerializeField]
    private float forwardSpeed;

    [SerializeField]
    private float reverseSpeed;

    [SerializeField]
    private float rotationSpeed;

    private Joystick joystick;

    private Rigidbody rb;

    public void Start()
    {
        // Get joystick
        DeathMessage.OnRespawn += resetTransforms;
        joystick = GameObject.FindGameObjectWithTag("joystick").GetComponent<Joystick>();
        rb = GetComponent<Rigidbody>();
    }

    private void resetTransforms()
    {
        state.SetTransforms(state.CubeTransform, gameObject.transform);
    }
    public override void Attached()
    {
        Debug.Log("Player movement attached");
        state.SetTransforms(state.CubeTransform, gameObject.transform);
    }

    //public void OnEnable()
    //{
    //    state.SetTransforms(state.CubeTransform, gameObject.transform);
    //}

    public override void SimulateOwner()
    {
        //Velocity
        Vector3 velocity = transform.forward * BoltNetwork.FrameDeltaTime;

        //Controller
        float controllerMagnitude = Input.GetAxis("Vertical");
        if (controllerMagnitude != 0.0f)
        {
            velocity *= controllerMagnitude;
            if (controllerMagnitude > 0)
            {
                velocity *= forwardSpeed;
            }
            else
            {
                velocity *= reverseSpeed;
            }
        }
        transform.localEulerAngles += new Vector3(0, rotationSpeed * Input.GetAxis("Horizontal"), 0);
        
        
        //TouchScreen
        //ROTATION:
        Vector3 originalPos = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 targetPos = Camera.main.transform.TransformDirection(new Vector3(joystick.Horizontal, 0, joystick.Vertical));
        targetPos.y = 0; // project targetPos onto X-Z plane

        float dir = Mathf.Sign(Vector3.Dot(targetPos, originalPos)); //Enables reversing if direction angle is closer to rear of tank.
        targetPos *= dir;

        Vector3 dirVec = targetPos - originalPos;
        dirVec = transform.InverseTransformDirection(dirVec);
        float angle = Vector3.Angle(originalPos, targetPos);

        Vector3 deltaAngle = new Vector3(0, angle * rotationSpeed * BoltNetwork.FrameDeltaTime, 0);
        if (dirVec.x >= 0) transform.localEulerAngles += deltaAngle;
        else transform.localEulerAngles -= deltaAngle;
        
        //VELOCITY:
        float joystickMagnitude = joystick.Horizontal * joystick.Horizontal + joystick.Vertical * joystick.Vertical;
        if (joystickMagnitude > 0)
        {
            velocity *= dir * joystickMagnitude;
            if (dir > 0) velocity *= forwardSpeed;
            else velocity *= reverseSpeed;
        }


        //ADD FORCE
        rb.AddForce(velocity, ForceMode.VelocityChange);
        

        // respawning
        if (Input.GetKey(KeyCode.Return) && entity.IsOwner)
        {
            transform.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
            transform.rotation = Quaternion.identity;
        }
    }
}