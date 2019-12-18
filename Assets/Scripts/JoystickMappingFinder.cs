// Arowx.com 2013 - free to use and improve!
using UnityEngine;
using System.Collections;

public class JoystickMappingFinder : MonoBehaviour
{

    public TextMesh joysticks;
    public TextMesh[] inputText;
    public TextMesh[] buttonText;

    public int numSticks;

    void Start()
    {
        int i = 0;

        string sticks = "Joysticks\n";

        foreach (string joyName in Input.GetJoystickNames())
        {
            sticks += i.ToString() + ":" + joyName + "\n";
            i++;
        }

        joysticks.text = sticks;

        numSticks = i;
    }

    /*
     * Read all axis of joystick inputs and display them for configuration purposes
     * Requires the following input managers
     *      Joy[N] Axis 1-9
     *      Joy[N] Button 0-20
     */
    void Update()
    {
       string inputs = "Joystick " + "4" + "\n";

       string stick = "Joy " + "4" + " Axis ";

       for (int a = 3; a <= 10; a++)
       {
           inputs += "Axis " + a + ":" + Input.GetAxis(stick + a).ToString("0.00") + "\n";
       }

       inputText[4 - 1].text = inputs;

        string buttons = "Buttons 3\n";

        for (int b = 0; b <= 10; b++)
        {
            buttons += "Btn " + b + ":" + Input.GetButton("Joy 4 Button " + b) + "\n";
        }

        buttonText[2].text = buttons;

    }
}
