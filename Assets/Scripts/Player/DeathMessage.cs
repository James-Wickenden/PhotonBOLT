using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathMessage : Bolt.EntityEventListener<ICustomCubeState>
{

    [SerializeField]
    private float respawnTime;
    private float remainingRespawnTime;
    Text respawnMessage;

    public static event System.Action OnRespawn = delegate { };
    private void Awake()
    {
        remainingRespawnTime = respawnTime;
        respawnMessage = GetComponent<Text>();
    }
    private void LateUpdate()
    {  
        if (remainingRespawnTime > 0){
            remainingRespawnTime -= Time.deltaTime;
            respawnMessage.text = "Respawning in " + Mathf.Round(remainingRespawnTime).ToString();

            if (remainingRespawnTime <= 0){
                remainingRespawnTime = respawnTime;
                Debug.Log("Respawn now");
                gameObject.SetActive(false);

                OnRespawn();

                Debug.Log("Called on respawn");
            }    
        }
        
    }
}