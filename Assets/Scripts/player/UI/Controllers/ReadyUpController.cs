using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReadyUpController : Bolt.GlobalEventListener
{
    public GameObject readyUpPanel;
    public GameObject controlPanel;
    public Text readyUpButtonText;
    public InputField usernameInput;

    public int minPlayers;

    public HashSet<string> readyPlayers = new HashSet<string>();

    public static event System.Action<string> OnReadyUp = delegate { };
    public static event System.Action OnAllPlayersReady = delegate { };

    private bool isReady = false;
    private string selectedName;

    void Start()
    {
        NetworkCallbacks.OnSceneLoadDone += showReadyUpMenu;
        queryUserNames();
    }

    public void showReadyUpMenu()
    {
        readyUpPanel.SetActive(true);
    }

    public void toggleReady()
    {
        selectedName = usernameInput.text;
        if (!isReady)
        {
            if (!readyPlayers.Contains(selectedName))
            {
                claimUsername(selectedName);
                OnReadyUp(selectedName);
                isReady = true;
                readyUpButtonText.text = "Unready";
            }
            else
            {
                // Show error msg
                Debug.Log("ERROR: username \'" + selectedName + "\' was taken.");
            }
            // else ready up failed.
        }
        else
        {
            // unready the player
            isReady = false;
            readyUpButtonText.text = "Ready Up!";
            // TODO: remove the player from the username and isready pools
            freeUsername(selectedName);
        }
    }

    private void queryUserNames()
    {
        var query = QueryReadyPlayersEvent.Create();
        query.Send();
    }

    private void claimUsername(string username)
    {
        var readyUp = ReadyUpEvent.Create();
        readyUp.Username = username;
        readyUp.Send();
    }

    private void freeUsername(string username)
    {
        readyPlayers.Remove(username);
    }

    public void Update()
    {
        if (readyPlayers.Count >= minPlayers)
        {
            readyUpPanel.SetActive(false);
            controlPanel.SetActive(true);

            OnAllPlayersReady();

            readyPlayers.Clear();
        }
    }

    public override void OnEvent(ReadyUpEvent evnt)
    {
        readyPlayers.Add(evnt.Username);
    }

    public override void OnEvent(QueryReadyPlayersEvent evnt)
    {
        if (isReady)
        {
            claimUsername(selectedName);
        }
    }
}
