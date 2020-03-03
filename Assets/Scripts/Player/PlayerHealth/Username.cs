using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Username : Bolt.EntityBehaviour<ICustomCubeState>
{
    private string username;

    public static event System.Action<string> OnUsernameAdded = delegate { };
    public static event System.Action<string> OnUsernameRemoved = delegate { };

    void Awake()
    {
        ReadyUpController.OnReadyUp += setUserName;
    }

    private void Update()
    {
        Debug.Log(username);
    }

    public void setUserName(string username)
    {
        state.Username = username;
        OnUsernameAdded(username);
    }

    public override void Attached()
    {
        state.AddCallback("Username", UserNameCallback);
    }

    private void OnDisable()
    {
        OnUsernameRemoved(username);
    }

    private void UserNameCallback()
    {
        username = state.Username;
    }

}
