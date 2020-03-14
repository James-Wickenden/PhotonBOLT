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
        Health.OnStoreScore += storeScore;
        score = 0;
    }

    public override void SceneLoadLocalDone(string scene)
    {
        OnSceneLoadDone();
    }

    private void setUserName(string username)
    {
        this.username = username;
    }

    private void respawn()
    {
        var spawnPosition = new Vector3(Random.Range(-5,5),1,Random.Range(-5,5));
        var tank = BoltNetwork.Instantiate(BoltPrefabs.tank, spawnPosition, Quaternion.identity);
        tank.GetComponent<Username>().setUserName(username);

        Health.OnStoreScore += storeScore;
        tank.GetComponent<Scoring>().setScore(score);
    }

    //public override void OnEvent(KeepScoreEvent evnt)
    //{
    //    if (username == evnt.username)
    //    {
    //        Debug.Log("score received");
    //        score = evnt.score;
    //    }
    //}

    private void storeScore(int newScore)
    {
        score = newScore;
        Debug.Log("Score has been stored");
    }
}
