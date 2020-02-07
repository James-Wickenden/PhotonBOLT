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

    private void Awake()
    {
        respawnMessage = GetComponent<Text>();
        remainingRespawnTime = respawnTime;
    }
    private void Update()
    {  
        if (remainingRespawnTime > 0){
            remainingRespawnTime -= Time.deltaTime;
            respawnMessage.text = "Respawning in " + Mathf.Round(remainingRespawnTime).ToString();

            if (remainingRespawnTime <= 0){
                Debug.Log("Respawn now");
                gameObject.SetActive(false);

                var respawn = RespawnEvent.Create(entity);
                respawn.Send();
                
                remainingRespawnTime = respawnTime;
            }    
        }
        
    }
}