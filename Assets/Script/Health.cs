using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Bolt.EntityEventListener<ICustomCubeState>
{
    [SerializeField]
    private int maxHealth = 100;

    private float currentHealth;

    float resetColorTime;
    Renderer renderer;

    Color originalColor;

    public event System.Action<float> OnHealthPctChanged = delegate { };

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Awake()
    {
        GetComponentInParent<HitDetection>().OnPlayerHit += ModifyHealth;
    }

    public void ModifyHealth(float amount)
    {
        state.Health += amount;
        if (amount < 0)
        {
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
        }

           if (resetColorTime < Time.time)
            {
                renderer.material.color = originalColor;
            }
    }

   public override void Attached()
   {
       
       state.Health = currentHealth;
       state.AddCallback("Health", HealthCallback);
       renderer = GetComponent<Renderer>();
       renderer.material.color = Color.white;
       originalColor = renderer.material.color;
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

   public override void OnEvent(DamageTakenEvent evnt)
    {
        resetColorTime = Time.time + 0.2f;
        renderer.material.color = evnt.FlashColor;
    }

}
