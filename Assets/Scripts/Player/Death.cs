using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Bolt.EntityEventListener<ICustomCubeState>
{
    private Renderer[] renderers;

    public void Awake()
    {
        Debug.Log("Death awake");
        DeathMessage.OnRespawn += respawn;
    }

    public void TriggerDeathEvent()
    {
        var death = DeathEvent.Create(entity);
        death.Send();
    }

    public override void OnEvent(DeathEvent evnt)
    {
        gameObject.SetActive(false);
    }

    public void respawn()
    {
        gameObject.SetActive(true);
    }
}