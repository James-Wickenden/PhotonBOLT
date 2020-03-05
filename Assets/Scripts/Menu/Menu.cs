using System;
using System.Collections.Generic;
using UdpKit;
using UnityEngine;
using UnityEngine.UI;

public class Menu : Bolt.GlobalEventListener
{
    public InputField roomNameInput;

    public Button joinGameButtonPrefab;
    public GameObject serverListPanel;


    public int maxPlayers = 1;

    public float buttonSpacing = -50;

    private List<Button> joinServerButtons = new List<Button>();

    public void StartServer() {
        BoltLauncher.StartServer();
    }

    public void StartClient() {
        BoltLauncher.StartClient();
    }

    public override void BoltStartDone() {
        BoltNetwork.EnableLanBroadcast();
        
        if (BoltNetwork.IsServer)
        {
            int randomInt = UnityEngine.Random.Range(0, 9999);
            string matchName = roomNameInput.text;

            //BoltNetwork.SetServerInfo(matchName, null);
            Bolt.Matchmaking.BoltMatchmaking.CreateSession(matchName, null, "SampleScene");
            //BoltNetwork.LoadScene("SampleScene");
        }

    }


    /*
    When the client is started, it connects to Photon Server and pulls information about any 
    visible room that has been registered on the cloud. To get this information we use the 
    SessionListUpdated handler, that is invoked by Bolt on a regular basis when a 
    new room info arrives internally.

    Below, we go through all available sessions, looking 
    for one Photon Session and connects to the first one found. 
    The client will start the punchthrough process with the Host and joins the game.
    */
    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList) {
        ClearSessions();
        foreach (var session in sessionList) {
            UdpSession photonSession = session.Value as UdpSession;

            if (photonSession.ConnectionsCurrent >= maxPlayers)
            {
                joinGameButtonPrefab.interactable = false;
            }

            Button joinGameButtonClone = Instantiate(joinGameButtonPrefab);

            joinGameButtonClone.transform.SetParent(serverListPanel.transform);
            joinGameButtonClone.transform.localPosition = new Vector3(0, buttonSpacing * joinServerButtons.Count, 0);

            joinGameButtonClone.onClick.AddListener(() => JoinGame(photonSession));
            joinGameButtonClone.GetComponentInChildren<Text>().text = photonSession.HostName
                                                                    + " ("
                                                                    + photonSession.ConnectionsCurrent
                                                                    + "/"
                                                                    + maxPlayers
                                                                    + ")";



            joinServerButtons.Add(joinGameButtonClone);
            Debug.LogFormat("Source is", photonSession.Source, "and endpoint is", photonSession.LanEndPoint);

            // if (photonSession.Source == UdpSessionSource.Lan) {
            //     BoltNetwork.Connect(photonSession);
            // }
            joinGameButtonPrefab.interactable = true;
        }
    }

    private void JoinGame(UdpSession photonSession)
    {
        BoltNetwork.Connect(photonSession, null);
    }

    private void ClearSessions()
    {

        foreach (Button button in joinServerButtons)
        {
            Destroy(button.gameObject);
        }

        joinServerButtons.Clear();
    }
}
