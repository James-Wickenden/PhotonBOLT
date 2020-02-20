using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : Bolt.EntityBehaviour<ICustomCubeState>
{
    [SerializeField]
    private int maxXP = 100;

    private float currentXP;

    public event System.Action<float> OnXPPctChanged = delegate { };

    public event System.Action OnXPLost = delegate { };
    public event System.Action OnXPGained = delegate { };

    public static event System.Action<XP> OnXPAdded = delegate { };
    public static event System.Action<XP> OnXPRemoved = delegate { };


    private void OnEnable()
    {
        currentXP = maxXP;
        OnXPAdded(this);
    }

    private void Awake()
    {
        GetComponentInParent<HitDetection>().OnPlayerHit += ModifyXP;
    }

    public void ModifyXP(float amount)
    {
        state.XP += amount;
        if (amount > 0) OnXPGained();
        else OnXPLost();
    }

    //TODO: remove. This is for testing.
    private void Update()
    {
        if (entity.IsOwner)
        {
            if (Input.GetKey(KeyCode.H))
            {
                ModifyXP(-1);
            }

            if (Input.GetKey(KeyCode.J))
            {
                ModifyXP(1);
            }
        }
    }

   public override void Attached()
   {
       state.XP = currentXP;
       state.AddCallback("XP", XPCallback);
   }

   private void XPCallback()
   {
       currentXP = state.XP;
       float currentXPPct = (float)currentXP / (float)maxXP;
       OnXPPctChanged(currentXPPct);
       
       if (currentXP <= 0)
       {
            BoltNetwork.Destroy(gameObject);
       }
   }

    private void OnDisable()
    {
        OnXPRemoved(this);
    }

}
