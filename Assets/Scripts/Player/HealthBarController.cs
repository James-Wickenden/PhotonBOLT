using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBarPrefab;

    private Dictionary<Health, HealthBar> healthBars = new Dictionary<Health, HealthBar>();

    private void Awake()
    {
        Health.OnHealthAdded += AddHealthBar;
        Health.OnHealthRemoved += RemoveHealthBar;
    }

    private void AddHealthBar(Health health)
    {
        if (!healthBars.ContainsKey(health))
        {
            var healthBar = Instantiate(healthBarPrefab, transform);
            healthBars.Add(health, healthBar);
            healthBar.SetHealth(health);
        }
    }

    private void RemoveHealthBar(Health health)
    {
        if (healthBars.ContainsKey(health))
        {
            Destroy(healthBars[health].gameObject);
            healthBars.Remove(health);
        }
    }
}
