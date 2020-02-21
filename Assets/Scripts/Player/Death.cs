using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : Bolt.EntityEventListener<ICustomCubeState>
{
    private Renderer[] renderers;
    public void Awake()
    {
        Debug.Log("Death awake");
        // DeathMessage.OnRespawn += respawn;
    }

    public void TriggerDeathEvent()
    {
        var death = DeathEvent.Create(entity);
        death.Send();
    }

    public override void OnEvent(DeathEvent evnt)
    {
        BoltNetwork.Destroy(gameObject);
        // gameObject.SetActive(false);
    }

    // public void respawn()
    // {
    //     var spawnPosition = new Vector3(Random.Range(-5,5),1,Random.Range(-5,5));
    //     transform.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
    //     transform.rotation = Quaternion.identity;
    //     gameObject.SetActive(true);
    // }

    // respawn occurs in network callbacks
}