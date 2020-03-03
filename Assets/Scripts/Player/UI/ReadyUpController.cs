using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReadyUpController : MonoBehaviour
{
    public GameObject readyUpPanel;
    public GameObject controlPanel;
    public InputField usernameInput;

    public static event System.Action<string> OnReadyUp = delegate { };
    void Start()
    {
        NetworkCallbacks.OnSceneLoadDone += showReadyUpMenu;
    }

    public void showReadyUpMenu()
    {
        readyUpPanel.SetActive(true);
    }

    public void readyUp()
    {
        readyUpPanel.SetActive(false);
        controlPanel.SetActive(true);
        OnReadyUp(usernameInput.text);
    }
}
