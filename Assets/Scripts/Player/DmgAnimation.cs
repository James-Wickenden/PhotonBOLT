using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgAnimation : Bolt.EntityEventListener<ICustomCubeState>
{
    float resetColorTime;
    Renderer[] renderers;

    Color originalColor;

    public void Awake()
    {
        GetComponentInParent<Health>().OnHealthLost += StartAnimation;
        renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers) r.material.color = Color.white;
        originalColor = Color.white;
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
            foreach (Renderer r in renderers) r.material.color = originalColor;
        }
    }

    public override void OnEvent(DamageTakenEvent evnt)
    {
        resetColorTime = Time.time + 0.2f;
        foreach (Renderer r in renderers) r.material.color = evnt.FlashColor;
    }
}
