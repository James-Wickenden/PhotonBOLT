using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool controllerConnected = false;

    private Joystick joystick;
    private Button shootButton;

    public event System.Action OnShoot = delegate { };
    public event System.Action ControllerConnected = delegate { };
    void Awake()
    {
        // Get joystick
        if (Input.GetJoystickNames().Length > 2)
        {
            controllerConnected = true;
            
            ControllerConnected();
            Debug.Log("Controller.");
            Debug.Log(Input.GetJoystickNames());
        }
        else
        {
            joystick = GameObject.FindGameObjectWithTag("joystick").GetComponent<Joystick>();
            shootButton = GameObject.FindGameObjectWithTag("ShootButton").GetComponent<Button>();
            shootButton.onClick.AddListener(() => OnShootButtonClick());

            Debug.Log("No Controller.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controllerConnected)
        {
            if (Input.GetKeyDown("joystick button 0") ||
                Input.GetKeyDown("joystick button 7") ||
                Input.GetKeyDown("joystick button 9"))
            {
                OnShoot();
            }
        }
    }

    public float getHorizontal()
    {
        if (controllerConnected)
        {
            return Input.GetAxis("Horizontal");
        }
        else
        {
            return joystick.Horizontal;
        }
    }

    public float getVertical()
    {
        if (controllerConnected)
        {
            return Input.GetAxis("Vertical");
        }
        else
        {
            return joystick.Vertical;
        }
    }

    public bool isControllerConnected()
    {
        return controllerConnected;
    }

    public void OnShootButtonClick()
    {
        OnShoot();
    }

}
