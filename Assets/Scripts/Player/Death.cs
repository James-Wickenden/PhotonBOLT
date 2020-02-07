using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Bolt.EntityEventListener<ICustomCubeState>
{
    private Renderer[] renderers;

    public void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        Debug.Log("Death awake");
        GetComponentInParent<Health>().OnDeath += TriggerDeathEvent;
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

    public override void OnEvent(RespawnEvent evnt){
        Debug.Log("Respawn!");
        gameObject.SetActive(true);
    }
}