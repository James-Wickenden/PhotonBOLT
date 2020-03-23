using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReadyUpController : Bolt.GlobalEventListener
{
    public GameObject readyUpPanel;
    public GameObject scoreboardPanel;
    public GameObject controlPanel;
    public Text readyUpButtonText;
    public InputField usernameInput;

    public GameObject readyButton;
    public GameObject unReadyButton;


    public int minPlayers = 2;

    public HashSet<string> readyPlayers = new HashSet<string>();

    public static event System.Action<string> OnReadyUp = delegate { };
    public static event System.Action OnAllPlayersReady = delegate { };

    private bool isReady = false;
    private string selectedName;

    private bool gameStarted = false;

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
            claimUsername(selectedName);
            OnReadyUp(selectedName);
            isReady = true;

            readyButton.SetActive(false);
            unReadyButton.SetActive(true);

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

    public void unReadyUp()
    {
        if (isReady)
        {
            isReady = false;
            freeUsername(selectedName);

            readyButton.SetActive(true);
            unReadyButton.SetActive(false);
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
        var unReadyUp = UnReadyEvent.Create();
        unReadyUp.Username = username;
        unReadyUp.Send();
    }

    public void Update()
    {
        if (readyPlayers.Count >= minPlayers && !gameStarted)
        {
            readyUpPanel.SetActive(false);
            controlPanel.SetActive(true);
            scoreboardPanel.SetActive(true);
            OnAllPlayersReady();

            gameStarted = true;
        }
    }

    public override void OnEvent(ReadyUpEvent evnt)
    {
        readyPlayers.Add(evnt.Username);
    }

    public override void OnEvent(UnReadyEvent evnt)
    {
        readyPlayers.Remove(evnt.Username);
    }

    public override void OnEvent(QueryReadyPlayersEvent evnt)
    {
        if (isReady)
        {
            claimUsername(selectedName);
        }
    }
}
