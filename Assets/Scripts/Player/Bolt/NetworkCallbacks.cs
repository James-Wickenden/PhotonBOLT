using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public static event System.Action OnSceneLoadDone = delegate { };

    private string username;

    public void Awake() 
    {
        RespawnTimer.OnRespawn += respawn;
        ReadyUpController.OnReadyUp += setUserName;
        ReadyUpController.OnAllPlayersReady += respawn;
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
    }
}
