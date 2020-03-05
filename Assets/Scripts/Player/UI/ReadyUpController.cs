using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReadyUpController : Bolt.GlobalEventListener
{
    public GameObject readyUpPanel;
    public GameObject controlPanel;
    public InputField usernameInput;

    public List<string> readyPlayers;

    public static event System.Action<string> OnReadyUp = delegate { };
    public static event System.Action OnAllPlayersReady = delegate { };

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
        string selectedName = usernameInput.text;
        if (!readyPlayers.Contains(selectedName))
        {
            // name not taken
            //readyPlayers.Add(selectedName);


            var readyUp = ReadyUpEvent.Create();
            readyUp.Username = selectedName;
            readyUp.Send();
            OnReadyUp(selectedName);
        }
        else
        {
            // Show error msg
            Debug.Log("ERROR: username \'" + selectedName + "\' was taken.");
        }
        // else ready up failed.
    }

    public void Update()
    {
        int numSessions = Bolt.Matchmaking.BoltMatchmaking.CurrentSession.ConnectionsCurrent;
        Debug.Log("Num sessions: " + numSessions);
        if (readyPlayers.Count >= 2)
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
}
