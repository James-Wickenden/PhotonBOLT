using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Bolt.EntityEventListener<ICustomCubeState>
{
    [SerializeField]
    private int maxHealth = 100;

    private float currentHealth;

    public event System.Action<float> OnHealthPctChanged = delegate { };

    public static event System.Action<Health> OnHealthAdded = delegate { };
    public static event System.Action<Health> OnHealthRemoved = delegate { };

    float resetColorTime;
    Renderer[] renderers;

    Color originalColor;

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

        if (amount < 0) {
            var flash = DamageTakenEvent.Create(entity);
            flash.FlashColor = Color.red;
            flash.Send();
        }
    }

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

            if (resetColorTime < Time.time)
            {
                foreach (Renderer r in renderers) r.material.color = originalColor;
            }
        }
    }

   public override void Attached()
   {
       state.Health = currentHealth;
       state.AddCallback("Health", HealthCallback);
       renderers = GetComponentsInChildren<Renderer>();
       foreach (Renderer r in renderers) r.material.color = Color.white;
       originalColor = Color.white;
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

    public override void OnEvent(DamageTakenEvent evnt) {
        resetColorTime = Time.time + 0.2f;
        foreach (Renderer r in renderers) r.material.color = evnt.FlashColor;
    }

}
