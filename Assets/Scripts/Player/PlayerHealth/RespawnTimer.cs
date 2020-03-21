using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnTimer : MonoBehaviour
{

    [SerializeField]
    private float respawnTime = 3.0f;
    private float remainingRespawnTime;
    Text respawnMessage;

    public static event System.Action<float> OnTimerTick = delegate { };
    public static event System.Action OnRespawn = delegate { };
    private void Awake()
    {
        Health.OnDeathOccured += startTimer;
        remainingRespawnTime = 0;
    }

    private void startTimer()
    {
        Debug.Log("Start Timer");
        remainingRespawnTime = respawnTime;
    }
    private void LateUpdate()
    {
        if (remainingRespawnTime > 0)
        {
            remainingRespawnTime -= Time.deltaTime;
            OnTimerTick(remainingRespawnTime);
            if (remainingRespawnTime <= 0)
            {
                Debug.Log("Respawn Triggered.");
                OnRespawn();
            }
        }
    }
}