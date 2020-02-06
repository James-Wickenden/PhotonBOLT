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


    private void OnEnable()
    {
        currentHealth = maxHealth;
        OnHealthAdded(this);
    }

    private void Awake()
    {
        GetComponentInParent<HitDetection>().OnPlayerHit += ModifyHealth;
    }

    public void ModifyHealth(float amount)
    {
        state.Health += amount;
        if (amount > 0) OnHealthGained();
        else OnHealthLost();
    }

    //TODO: remove. This is for testing.
    private void Update()
    {
        if (entity.IsOwner)
        {
            if (Input.GetKey(KeyCode.H))
            {
                ModifyHealth(-1);
            }

            if (Input.GetKey(KeyCode.J))
            {
                ModifyHealth(1);
            }
        }
    }

   public override void Attached()
   {
       state.Health = currentHealth;
       state.AddCallback("Health", HealthCallback);
   }

   private void HealthCallback()
   {
       currentHealth = state.Health;
       float currentHealthPct = (float)currentHealth / (float)maxHealth;
       OnHealthPctChanged(currentHealthPct);
       
       if (currentHealth <= 0)
       {
            BoltNetwork.Destroy(gameObject);
       }
   }

    private void OnDisable()
    {
        OnHealthRemoved(this);
    }

}
