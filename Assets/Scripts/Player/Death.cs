using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Bolt.EntityEventListener<ICustomCubeState>
{
    
    private float respawnTime;

    private Renderer[] renderers;

    public void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        // healthBarRenderer = state.
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
        for(int i = 0; i < renderers.Length; ++i)
            {
                Renderer r = renderers[i];
                for (int j = 0; j < r.materials.Length; ++j)
                {
                    r.enabled = false;
                }
            }

        respawnTime = 3;
    }

    public void OnGUI()
    {
        if (respawnTime > 0){
            GUI.Label(new Rect(Screen.width / 2, 100f, 200f, 200f), "You died");
        }

    }
}