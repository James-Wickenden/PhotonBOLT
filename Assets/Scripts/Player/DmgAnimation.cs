using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DmgAnimation : Bolt.EntityEventListener<ICustomCubeState>
{
    private float resetColorTime;
    private Renderer[] renderers;

    public void Awake()
    {
        GetComponentInParent<Health>().OnHealthLost += StartAnimation;
        renderers = GetComponentsInChildren<Renderer>();
    }

    public void StartAnimation()
    {
        var flash = DamageTakenEvent.Create(entity);
        flash.FlashColor = Color.red;
        flash.Send();
    }

    public void Update()
    {
        if (resetColorTime < Time.time)
        {
            for(int i = 0; i < renderers.Length; ++i)
            {
                Renderer r = renderers[i];
                for (int j = 0; j < r.materials.Length; ++j)
                {
                    r.materials[j].color = Color.grey;
                }
            }
        }
    }

    public override void OnEvent(DamageTakenEvent evnt)
    {
        resetColorTime = Time.time + 0.2f;
        foreach (Renderer r in renderers)
        {
            for (int i = 0; i < r.materials.Length; ++i)
            {
                r.materials[i].color = evnt.FlashColor;
            }
        }
    }
}
