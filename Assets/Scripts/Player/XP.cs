using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : Bolt.EntityBehaviour<ICustomCubeState>
{
    [SerializeField]
    private int maxXP = 100;

    private float currentXP = 0;

    public event System.Action<float> OnXPPctChanged = delegate { };

    public event System.Action OnXPLost = delegate { };
    public event System.Action OnXPGained = delegate { };

    public static event System.Action<XP> OnXPAdded = delegate { };
    public static event System.Action<XP> OnXPRemoved = delegate { };


    private void OnEnable()
    {
        currentXP = 0;
        OnXPAdded(this);
    }

    private void Awake()
    {
        GetComponentInParent<TankListener>().OnXP += ModifyXP;
    }

    public void ModifyXP(int amount)
    {
        state.XP += amount;
        if (amount > 0) OnXPGained();
        else OnXPLost();
        //Debug.Log("XP increased to " + state.XP);
    }

    public void ModifyXP(int tankID, int amount)
    {
        if (entity.IsOwner && state.TankID == tankID)
        {
            state.XP += amount;
            if (amount > 0) OnXPGained();
            else OnXPLost();
            //Debug.Log("XP increased to " + state.XP);
        }
    }

    public void ModifyXP(Bolt.NetworkId networkID, int amount)
    {
        if (entity.IsOwner && entity.NetworkId.Equals(networkID))
        {
            state.XP += amount;
            if (amount > 0) OnXPGained();
            else OnXPLost();
            Debug.Log("XP increased to " + state.XP);
        }
    }

    public override void Attached()
    {
        state.XP = currentXP;
        state.AddCallback("XP", XPCallback);
        //Debug.Log("Start XP is: " + state.XP);
        ModifyXP(1);
        state.XP = 0;
    }

    private void XPCallback()
    {
        //Debug.Log("xp callback, currentXP is: " + currentXP);
        currentXP = state.XP;
        float currentXPPct = (float)currentXP / (float)maxXP;
        OnXPPctChanged(currentXPPct);

        if (currentXP <= 0)
        {
            //BoltNetwork.Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        OnXPRemoved(this);
    }

}