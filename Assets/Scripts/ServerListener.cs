using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// A script that's only run on the server
[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerListener : Bolt.GlobalEventListener
{ 
    public Dictionary<string, int> scoreMap;


    private void Awake()
    {
        scoreMap = new Dictionary<string, int>();
    }

    public override void OnEvent(NewPlayerEvent evnt)
    {
        string username = evnt.username;
        if (!scoreMap.Keys.Contains(username)) {
            scoreMap.Add(username, 0);
            Debug.Log("New player joined, score set to 0");
        }

        // send all players' scores to everyone to make sure everyone is up to date (there's probably a better way to do this...)
        foreach (var player in scoreMap.Keys)
        {
            SetScoreEvent setScoreEvent = SetScoreEvent.Create();
            setScoreEvent.username = player;
            setScoreEvent.score = scoreMap[player];
            setScoreEvent.Send();
        }
        
    }

    public override void OnEvent(PlayerScoreEvent evnt)
    {
        scoreMap[evnt.username] = evnt.score;
    }

    public override void OnEvent(XPServerEvent evnt)
    {
        Debug.Log("Received XPServerEvent from some tank");

        var newEvent = XPClientEvent.Create();
        newEvent.xpVal = evnt.xpVal;
        newEvent.networkID = evnt.networkID;
        newEvent.Send();
    }
}