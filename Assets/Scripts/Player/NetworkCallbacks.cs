using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{

    public void Awake()
    {
        DeathMessage.OnRespawn += respawn;
    }

    public override void SceneLoadLocalDone(string scene) {

        var spawnPosition = new Vector3(Random.Range(-5,5),1,Random.Range(-5,5));
        BoltNetwork.Instantiate(BoltPrefabs.tank, spawnPosition, Quaternion.identity);
    }

    private void respawn() {
        var spawnPosition = new Vector3(Random.Range(-5,5),1,Random.Range(-5,5));
        BoltNetwork.Instantiate(BoltPrefabs.tank, spawnPosition, Quaternion.identity);
    }
}
