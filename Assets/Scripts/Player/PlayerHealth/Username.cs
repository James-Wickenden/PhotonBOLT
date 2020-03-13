using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Username : Bolt.EntityBehaviour<ICustomCubeState>
{
    private string username;

    public static event System.Action<Username> OnUsernameAdded = delegate { };
    public static event System.Action<Username> OnUsernameRemoved = delegate { };
    public event System.Action<string> OnUsernameSet = delegate { };


    private void OnEnable()
    {
        Debug.Log("On enable username");
        OnUsernameAdded(this);
    }
    public void setUserName(string username)
    {
        state.Username = username;
        Debug.Log("username set: " + username);
    }

    public override void Attached()
    {
        state.AddCallback("Username", UserNameCallback);
    }

    private void OnDisable()
    {
        OnUsernameRemoved(this);
    }

    private void UserNameCallback()
    {
        username = state.Username;
        OnUsernameSet(username);
    }

}
