using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : Bolt.EntityBehaviour<ICustomCubeState>
{
    [SerializeField]
    private int maxXP = 100;

    private int currentXP = 0;

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
        if (entity.IsOwner)
        {
            state.XP += amount;
            if (amount > 0) OnXPGained();
            else OnXPLost();
        }
    }

    public void ModifyXP(Bolt.NetworkId networkID, int amount)
    {
        Debug.Log("My ID is " + entity.NetworkId);
        if (entity.IsOwner && entity.NetworkId.Equals(networkID))
        {
            state.XP += amount;
            if (amount > 0) OnXPGained();
            else OnXPLost();
            Debug.Log("XP increased to " + state.XP);

            if (state.XP % maxXP == 0)
            {
                Debug.Log("Player obtains credit of 10");
                state.UpgradeCredit += 10;
            }
        }
    }

    public override void Attached()
    {
        if (entity.IsOwner) { 
            state.XP = currentXP;
            ModifyXP(1);
            state.XP = 0;
        }
        state.AddCallback("XP", XPCallback);

    }

    private void XPCallback()
    {
        //Debug.Log("xp callback, currentXP is: " + currentXP);
        currentXP = state.XP;
        float currentXPPct = ((float)currentXP % maxXP) / (float)maxXP;
        OnXPPctChanged(currentXPPct);
    }

    private void OnDisable()
    {
        OnXPRemoved(this);
    }

}