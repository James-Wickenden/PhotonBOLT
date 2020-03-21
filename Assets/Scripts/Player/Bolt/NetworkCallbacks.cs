using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public static event System.Action OnSceneLoadDone = delegate { };

    private string username;
    private int score;

    public void Awake() 
    {
        RespawnTimer.OnRespawn += respawn;
        ReadyUpController.OnReadyUp += setUserName;
        ReadyUpController.OnAllPlayersReady += respawn;
        score = 0;
    }

    public override void SceneLoadLocalDone(string scene)
    {
        OnSceneLoadDone();
    }

    private void setUserName(string newUsername)
    {
        this.username = newUsername;
    }

    private void respawn()
    {
        var spawnPosition = new Vector3(Random.Range(-5,5),1,Random.Range(-5,5));
        var tank = BoltNetwork.Instantiate(BoltPrefabs.tank, spawnPosition, Quaternion.identity);
        tank.GetComponent<Username>().setUserName(username);

        tank.GetComponent<Scoring>().OnStoreScore += storeScore;

        // notify server that there's a new player
        NewPlayerEvent evnt = NewPlayerEvent.Create();
        evnt.username = username;
        evnt.Send();
    }

    private void storeScore(int newScore)
    {
        score = newScore;
        Debug.Log("Score stored as : " + score);
    }
}
