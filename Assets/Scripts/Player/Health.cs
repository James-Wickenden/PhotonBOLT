using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Bolt.EntityBehaviour<ICustomCubeState>
{
    [SerializeField]
    private int maxHealth = 100;

    private float currentHealth;

    public event System.Action<float> OnHealthPctChanged = delegate { };

    public event System.Action OnHealthLost = delegate { };
    public event System.Action OnHealthGained = delegate { };

    public static event System.Action<Health> OnHealthAdded = delegate { };
    public static event System.Action<Health> OnHealthRemoved = delegate { };
    public static event System.Action OnDeathOccured = delegate { };


    private void OnEnable()
    {
        currentHealth = maxHealth;
        OnHealthAdded(this);
    }

    private void Awake()
    {
        GetComponentInParent<HitDetection>().OnPlayerHit += ModifyHealth;
        DeathMessage.OnRespawn += resetHealth;   
    }

    public void ModifyHealth(float amount)
    {
        state.Health += amount;
        if (amount > 0) OnHealthGained();
        else OnHealthLost();
    }

    private void resetHealth()
    {
        state.Health = maxHealth;
    }


   public override void Attached()
   {
       if (entity.IsOwner) state.Health = currentHealth;
       state.AddCallback("Health", HealthCallback);
   }

   public void HealthCallback()
   {
       currentHealth = state.Health;
       float currentHealthPct = (float)currentHealth / (float)maxHealth;
       OnHealthPctChanged(currentHealthPct);
       
       if (currentHealth <= 0)
       {
        //    OnDeath();
        var death = DeathEvent.Create(entity);
        death.Send();
        OnDeathOccured();
        //    BoltNetwork.Destroy(gameObject);
       }
   }

    private void OnDisable()
    {
        OnHealthRemoved(this);
    }

}
